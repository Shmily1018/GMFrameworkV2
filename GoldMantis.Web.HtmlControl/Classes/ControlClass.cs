using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoldMantis.Common;

namespace GoldMantis.Web.HtmlControl.Classes
{
    public class ControlClass
    {
        protected static string _classAttribute = "class";
        protected static string _classSeprator = " ";
        protected List<string> _classes;

        public ControlClass()
        {
            _classes = new List<string>();
        }

        public virtual ControlClass Add(params string[] items)
        {
            return AddRange(items);
        }

        public virtual ControlClass AddRange(IEnumerable<string> classes)
        {
            if (classes.HasItems())
            {
                _classes.AddRange(classes);
            }
            
            return this;
        }

        public virtual ControlClass Clear()
        {
            _classes.Clear();

            return this;
        }

        public virtual bool Contains(string item)
        {
            return _classes.Contains(item);
        }

        public virtual ControlClass Remove(string item)
        {
            _classes.Remove(item);

            return this;
        }

        public virtual ControlClass Remove(IEnumerable<string> classes)
        {
            List<string> items = classes.ToList();

            for (int i = 0; i < items.Count(); i++)
            {
                _classes.Remove(items[i]);
            }
            

            return this;
        }

        public virtual IEnumerable<string> GetGridClasses()
        {
            string match = "^col-[a-z]{2}-[1-9][1|2]*$";

            return Classes.Where(c => c.IsMatch(match));
        }

        public virtual IDictionary<string, object> InflateClass(IDictionary<string, object> htmlAttributes)
        {
            if (Classes.HasItems())
            {
                if (htmlAttributes.ContainsKey(ClassAttribute))
                {
                    string[] htmlClass = htmlAttributes[ClassAttribute].ToString().Split(_classSeprator[0]);
                    AddRange(htmlClass);

                    htmlAttributes[ClassAttribute] = ToString();
                }
                else
                {
                    htmlAttributes.Add(ClassAttribute, ToString());
                }
            }

            return htmlAttributes;
        }

        public override string ToString()
        {
            return string.Join(_classSeprator, _classes.Distinct());
        }

        public virtual int Count
        {
            get { return _classes.Count; }
        }

        public IEnumerable<string> Classes
        {
            get { return _classes; }
        }

        public static string ClassAttribute
        {
            get { return _classAttribute; }
        }

        public static string ClassSeprator
        {
            get { return _classSeprator; }
        }
    }
}