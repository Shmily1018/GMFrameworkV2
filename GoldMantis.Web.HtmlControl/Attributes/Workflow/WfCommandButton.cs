/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         WfCommandButton.cs
** Creator:          Joe
** CreateDate:       2015-11-10
** Changer:
** LastChangeDate:
** Description:      WorkFlow命令按钮
** VersionInfo:
*********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace GoldMantis.Web.HtmlControl
{
    public class WfCommandButton
    {
        public WfCommandButton(WfCommandButtonTypeEnum commandButtonType)
        {
            CommandButtonType = commandButtonType;
            CommandClass = "btn btn-info";
            InitCommandButtonAttribute(commandButtonType);
        }

        private void InitCommandButtonAttribute(WfCommandButtonTypeEnum commandButtonType)
        {
            Type buttonType = commandButtonType.GetType();
            string buttonTypeName = Enum.GetName(buttonType, commandButtonType);
            FieldInfo buttonTypeFiled = buttonType.GetField(buttonTypeName);
            IEnumerable<Attribute> attributes = buttonTypeFiled.GetCustomAttributes(typeof(WfCommandButtonAttribute));

            if (attributes == null || !attributes.Any())
            {
                throw new ApplicationException("WfCommandButtonTypeEnum字段必须被WfCommandButtonAttribute修饰...");
                ;
            }


            CommandButtonAttribute = attributes.First() as WfCommandButtonAttribute;
        }

        public WfCommandButtonAttribute CommandButtonAttribute { get; private set; }

        public WfCommandButtonTypeEnum CommandButtonType { get; private set; }

        public string CommandClass { get; private set; }

        public override string ToString()
        {
            string buttonHtml =
                string.Format("<button type=\"button\" class=\"{0} {1}\" id=\"{2}\" title=\"{3}\"><i class=\"{4}\"></i>&nbsp;&nbsp;{5}</button>",
                    this.CommandClass, this.CommandButtonAttribute.ExtendClass,
                    this.CommandButtonAttribute.CommandClientID, this.CommandButtonAttribute.CommandTitle,
                    this.CommandButtonAttribute.IconClass, this.CommandButtonAttribute.CommandName);

            return buttonHtml;
        }

        public MvcHtmlString ToHtmlString()
        {
            return MvcHtmlString.Create(this.ToString());
        }
    }
}