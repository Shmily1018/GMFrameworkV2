using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GoldMantis.Entity.ValidateAttribute
{

    public class DateTimeNotLessThanAttribute : ValidationAttribute,IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} 不得小于 {1}.";
        public string OtherProperty { get; private set; }
        private string OtherPropertyName { get; set; }

        public DateTimeNotLessThanAttribute(string otherProperty, string otherPropertyName)
            : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;
            OtherPropertyName = otherPropertyName;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherPropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);
                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                DateTime dtThis = Convert.ToDateTime(value);
                DateTime dtOther = Convert.ToDateTime(otherPropertyValue);

                if (dtThis < dtOther)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "notlessthan"
            };

            clientValidationRule.ValidationParameters.Add("otherproperty", OtherProperty);

            return new[] { clientValidationRule };
        }

    }
}
