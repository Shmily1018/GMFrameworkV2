using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NHibernate;
using NHibernate.Properties;
using NHibernate.Transform;

namespace GoldMantis.Common
{
    [Serializable]
    public class CustomAliasToBeanResultTransformer : IResultTransformer
    {
        // Fields
        private readonly ConstructorInfo _constructor;
        private const BindingFlags _flags = (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        private readonly IPropertyAccessor _propertyAccessor;
        private readonly System.Type _resultClass;
        private ISetter[] _setters;

        // Methods
        public CustomAliasToBeanResultTransformer(System.Type resultClass)
        {
            if (resultClass == null)
            {
                throw new ArgumentNullException("resultClass");
            }


            _resultClass = resultClass;
            _constructor =
                resultClass.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null,
                    Type.EmptyTypes, null);

            if (_constructor.IsNull() && resultClass.IsClass)
            {
                throw new ArgumentException(
                    "The target class of a AliasToBeanResultTransformer need a parameter-less constructor",
                    "resultClass");
            }


            _propertyAccessor =
                new ChainedPropertyAccessor(new IPropertyAccessor[]
                {
                    PropertyAccessorFactory.GetPropertyAccessor(null), PropertyAccessorFactory.GetPropertyAccessor("field")
                });
        }

        public bool Equals(CustomAliasToBeanResultTransformer other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return (ReferenceEquals(this, other) || Equals(other._resultClass, _resultClass));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj.As<AliasToBeanResultTransformer>());
        }

        public override int GetHashCode()
        {
            return _resultClass.GetHashCode();
        }

        public IList TransformList(IList collection)
        {
            return collection;
        }

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            ProcessCSharpKeywords(aliases);
            object result;

            if (aliases == null)
            {
                throw new ArgumentNullException("aliases");
            }


            try
            {
                if (_setters == null)
                {
                    _setters = new ISetter[aliases.Length];

                    for (int j = 0; j < aliases.Length; j++)
                    {
                        string propertyName = aliases[j];

                        if (propertyName != null)
                        {
                            _setters[j] = _propertyAccessor.GetSetter(_resultClass, propertyName);
                        }
                    }
                }


                result = _resultClass.IsClass
                    ? _constructor.Invoke(null)
                    : NHibernate.Cfg.Environment.BytecodeProvider.ObjectsFactory.CreateInstance(_resultClass, true);

                for (int i = 0; i < aliases.Length; i++)
                {
                    if (_setters[i] != null)
                    {
                        _setters[i].Set(result, tuple[i]);
                    }
                }
            }
            catch (InstantiationException exception)
            {
                throw new HibernateException("Could not instantiate result class: " + this._resultClass.FullName, exception);
            }
            catch (MethodAccessException exception2)
            {
                throw new HibernateException("Could not instantiate result class: " + this._resultClass.FullName, exception2);
            }


            return result;
        }

        private string[] ProcessCSharpKeywords(string[] strArray)
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                if (_CSharpKeywords.Contains(strArray[i]))
                {
                    strArray[i] = strArray[i] + "_Keyword";
                }
            }


            return strArray;
        }

        private static IList<string> _CSharpKeywords = new List<string>()
        {
            "abstract",
            "as",
            "base",
            "bool",
            "break",
            "byte",
            "case",
            "catch",
            "char",
            "checked",
            "class",
            "const",
            "continue",
            "decimal",
            "default",
            "delegate",
            "do",
            "double",
            "else",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "finally",
            "fixed",
            "float",
            "for",
            "foreach",
            "goto",
            "if",
            "implicit",
            "in",
            "int",
            "interface",
            "internal",
            "is",
            "lock",
            "long",
            "namespace",
            "new",
            "null",
            "object",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "return",
            "sbyte",
            "sealed",
            "short",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "struct",
            "switch",
            "this",
            "throw",
            "true",
            "try",
            "typeof",
            "uint",
            "ulong",
            "unchecked",
            "unsafe",
            "ushort",
            "using",
            "virtual",
            "void",
            "volatile",
            "while"
        };

    }
}