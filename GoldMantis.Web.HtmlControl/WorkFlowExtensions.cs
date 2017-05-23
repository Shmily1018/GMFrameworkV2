using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.Web.ViewModel;

namespace GoldMantis.Web.HtmlControl
{
    public static class WorkFlowExtensions
    {
        public static MvcHtmlString WorkFlow(this HtmlHelper htmlHelper, List<WfCommandButton> buttons, WorkflowModel model)
        {
            StringBuilder buttonsBuilder = new StringBuilder();

            if (buttons.HasItems())
            {
                buttons.ForEach(b=>buttonsBuilder.Append(b.ToString()));
            }

            return MvcHtmlString.Create(buttonsBuilder.ToString());
        }
    }
}