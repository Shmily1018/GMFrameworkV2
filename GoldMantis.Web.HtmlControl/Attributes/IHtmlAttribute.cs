using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Web.HtmlControl.Classes;

namespace GoldMantis.Web.HtmlControl
{
    public interface IHtmlAttribute
    {
        ControlClass Class { get;}

        object ObjectAttribute { get; set; }

        IDictionary<string, object> HtmlAttributes { get;}

        IDictionary<string, object> GetHtmlAttribute();
    }
}
