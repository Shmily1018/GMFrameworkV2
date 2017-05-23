using System.Text;
using System.Web.Mvc;

namespace GoldMantis.Web.HtmlControl
{
    public static class TableRowExtensions
    {
        public static MvcHtmlString TableRow<T>(this HtmlHelper htmlHelper, TableRowAttribute<T> attribute)
        {
            return MvcHtmlString.Create(htmlHelper.TableRowString(attribute));
        }

        internal static string TableRowString<T>(this HtmlHelper htmlHelper, TableRowAttribute<T> attribute)
        {
            TagBuilder trTagBuilder = new TagBuilder("tr");
            trTagBuilder.MergeAttributes(attribute.GetHtmlAttribute());

            StringBuilder tdBuilder = new StringBuilder();

            foreach (TableCellAttribute<T> cell in attribute.TableCells)
            {
                tdBuilder.Append(htmlHelper.TableCell(cell));
            }

            trTagBuilder.InnerHtml = tdBuilder.ToString();

            return trTagBuilder.ToString(TagRenderMode.Normal);
        }

        public static MvcHtmlString TableRowFor(this HtmlHelper htmlHelper, TableRowAttribute attribute)
        {
            TagBuilder trTagBuilder = new TagBuilder("tr");
            trTagBuilder.MergeAttributes(attribute.GetHtmlAttribute());

            StringBuilder tdBuilder = new StringBuilder();

            foreach (TableCellAttribute cell in attribute.TableCells)
            {
                tdBuilder.Append(htmlHelper.TableCellFor(cell));
            }

            trTagBuilder.InnerHtml = tdBuilder.ToString();

            return MvcHtmlString.Create(trTagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}