using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Styles;

namespace GoldMantis.Web.HtmlControl
{
    public static class TableExtensions
    {
        public static MvcHtmlString Table<T>(this HtmlHelper htmlHelper, TableAttribute<T> attribute)
        {
            return MvcHtmlString.Create(htmlHelper.TableString(attribute));
        }

        public static string TableString<T>(this HtmlHelper htmlHelper, TableAttribute<T> attribute)
        {
            TagBuilder containerTagBuilder = new TagBuilder("div");
            containerTagBuilder.MergeAttribute("style", "width: 100%; overflow: auto;", true);
            containerTagBuilder.MergeAttribute("id", "common-table-container", true);
            containerTagBuilder.AddCssClass("common-table-container");

            TagBuilder tableTagBuilder = new TagBuilder("table");
            tableTagBuilder.MergeAttributes(attribute.GetHtmlAttribute());
            tableTagBuilder.MergeAttribute("style", "table-layout: fixed;", true);

            TagBuilder colgroupTagBuilder = new TagBuilder("colgroup");
            StringBuilder colgroupBuilder = new StringBuilder();

            TagBuilder theadTagBuilder = new TagBuilder("thead");
            TagBuilder tbodyTagBuilder = new TagBuilder("tbody");
            StringBuilder rowBuilder = new StringBuilder();

            /** start init colgroup tag */
            TagBuilder colTagBuilder = null;

            foreach (TableCellAttribute<T> tableCell in attribute.TableBodyRowTemplete.TableCells)
            {
                colTagBuilder = new TagBuilder("col");

                if (tableCell.CellWidth > 0)
                {
                    colTagBuilder.MergeAttribute("style", string.Format("width: {0}px;", tableCell.CellWidth), true);
                }

                colgroupBuilder.Append(colTagBuilder.ToString(TagRenderMode.SelfClosing));
            }

            colgroupTagBuilder.InnerHtml = colgroupBuilder.ToString();
            /* end init colgroup tag */


            /** start init thead tag */
            if (attribute.TableHeaderRow.IsNotNull())
            {
                foreach (TableCellAttribute<T> cell in attribute.TableHeaderRow.TableCells)
                {
                    cell.IsHeadCell = true;

                    if (!cell.Class.Contains(Bootstrap.Text.TextCenter))
                    {
                        cell.Class.Add(Bootstrap.Text.TextCenter);
                    }
                }

                theadTagBuilder.InnerHtml = htmlHelper.TableRowString(attribute.TableHeaderRow);
            }
            /* end thead colgroup tag */


            /** start init tbody tag */
            if (attribute.DataSource.HasItems())
            {
                foreach (T data in attribute.DataSource)
                {
                    attribute.TableBodyRowTemplete.DelegateObject = data;

                    foreach (TableCellAttribute<T> cell in attribute.TableBodyRowTemplete.TableCells)
                    {
                        cell.DelegateObject = data;
                    }

                    rowBuilder.Append(htmlHelper.TableRowString(attribute.TableBodyRowTemplete));
                }
            }
            

            if (attribute.TableFooterRow.IsNotNull())
            {
                rowBuilder.Append(htmlHelper.TableRowString(attribute.TableFooterRow));
            }

            tbodyTagBuilder.InnerHtml = rowBuilder.ToString();
            /* end thead tbody tag */


            tableTagBuilder.InnerHtml = string.Format("{0}{1}{2}",
                colgroupTagBuilder.ToString(TagRenderMode.Normal),
                theadTagBuilder.ToString(TagRenderMode.Normal),
                tbodyTagBuilder.ToString(TagRenderMode.Normal));

            containerTagBuilder.InnerHtml = tableTagBuilder.ToString(TagRenderMode.Normal);

            return containerTagBuilder.ToString(TagRenderMode.Normal);
        }

        public static MvcHtmlString TableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, TableAttribute<TModel, TProperty> attribute)
        {
            string name = ExpressionHelper.GetExpressionText(attribute.Expression);
            name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            ModelMetadata valueMetadata = ModelMetadata.FromLambdaExpression(attribute.Expression, htmlHelper.ViewData);
            ICollection<TProperty> value = valueMetadata.Model as ICollection<TProperty>;

            attribute.PrepareTableAttribute(name);

            TagBuilder containerTagBuilder = new TagBuilder("div");
            containerTagBuilder.MergeAttribute("style", "width: 100%; overflow: auto;", true);
            containerTagBuilder.MergeAttribute("id", "common-table-container", true);
            containerTagBuilder.AddCssClass("common-table-container");

            TagBuilder tableTagBuilder = new TagBuilder("table");
            tableTagBuilder.MergeAttributes(attribute.GetHtmlAttribute());
            tableTagBuilder.MergeAttribute("style", "table-layout: fixed;", true);

            TagBuilder colgroupTagBuilder = new TagBuilder("colgroup");
            StringBuilder colgroupBuilder = new StringBuilder();

            TagBuilder theadTagBuilder = new TagBuilder("thead");
            TagBuilder tbodyTagBuilder = new TagBuilder("tbody");
            StringBuilder rowBuilder = new StringBuilder();

            /** start init colgroup tag */
            TagBuilder colTagBuilder = null;

            foreach (TableCellAttribute tableCell in attribute.TableBodyRowTemplete.TableCells)
            {
                colTagBuilder = new TagBuilder("col");


                if (tableCell.CellWidth > 0)
                {
                    colTagBuilder.MergeAttribute("style", string.Format("width: {0}px;", tableCell.CellWidth), true); 
                }
                else
                {
                    //update by chenjd 增加width="auto"的情况
                    //colTagBuilder.MergeAttribute("style", string.Format("width: auto;"), true);
                }

                colgroupBuilder.Append(colTagBuilder.ToString(TagRenderMode.SelfClosing));
            }

            colgroupTagBuilder.InnerHtml = colgroupBuilder.ToString();
            /* end init colgroup tag */


            /** start init thead tag */
            if (attribute.TableHeaderRow.IsNotNull())
            {
                foreach (TableCellAttribute cell in attribute.TableHeaderRow.TableCells)
                {
                    cell.IsHeadCell = true;

                    if (!cell.Class.Contains(Bootstrap.Text.TextCenter))
                    {
                        cell.Class.Add(Bootstrap.Text.TextCenter);
                    }
                }

                theadTagBuilder.InnerHtml = htmlHelper.TableRowFor(attribute.TableHeaderRow).ToString();
            }
            /* end thead colgroup tag */


            /** start init tbody tag */
            string tableRow = string.Empty;
            if (value.HasItems())
            {
                for (int i = 0; i < value.Count; i++)
                {
                    attribute.TableBodyRowTemplete.Index = i;

                    foreach (TableCellAttribute cell in attribute.TableBodyRowTemplete.TableCells)
                    {
                        cell.Index = i;
                    }

                    tableRow = htmlHelper.TableRowFor(attribute.TableBodyRowTemplete).ToString();

                    if (i < value.Count - 1)
                    {
                        rowBuilder.Append(tableRow);
                    }
                }
            }


            if (attribute.TableFooterRow.IsNotNull())
            {
                rowBuilder.Append(htmlHelper.TableRowFor(attribute.TableFooterRow));
            }

            tbodyTagBuilder.InnerHtml = rowBuilder.ToString();
            /* end thead tbody tag */


            tableTagBuilder.InnerHtml = string.Format("{0}{1}{2}",
                colgroupTagBuilder.ToString(TagRenderMode.Normal),
                theadTagBuilder.ToString(TagRenderMode.Normal),
                tbodyTagBuilder.ToString(TagRenderMode.Normal));


            string rowCount = string.Format("<input type='hidden' id='grid_{0}_rows' value='{1}'>", name, value.Count);
            //string rowTemplete = string.Format("<table class='hidden' id='grid_{0}_templete'>{1}</table>", name, tableRow);
            string rowTemplete = string.Format("<script class='' id='grid_{0}_templete'>{1}</script>", name, tableRow);
            

            containerTagBuilder.InnerHtml = string.Format("{0}{1}{2}", tableTagBuilder.ToString(TagRenderMode.Normal), rowCount, rowTemplete);

            return MvcHtmlString.Create(containerTagBuilder.ToString(TagRenderMode.Normal));
        }
    }
}