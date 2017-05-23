using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using GoldMantis.Common;
using GoldMantis.Entity;
using GoldMantis.Web.ViewModel;

namespace GoldMantis.Web.HtmlControl
{
    public static class MenuExtensions
    {
        public static MvcHtmlString Menu(this HtmlHelper htmlHelper, IList<SAMenu> menuModels)
        {
            MenuTree menuTree = MenuTree.CreateTree(menuModels);

            StringBuilder menuBuilder = new StringBuilder();
            menuBuilder.AppendLine("<ul class=\"page-sidebar-menu page-sidebar-menu-hover-submenu\" data-keep-expanded=\"false\" data-auto-scroll=\"true\" data-slide-speed=\"200\">");

            foreach (MenuTree tree in menuTree.ChildMenus)
            {
                MenuBuilder(tree, menuBuilder);
            }

            menuBuilder.AppendLine("</ul>");

            return MvcHtmlString.Create(menuBuilder.ToString());
        }

        private static void MenuBuilder(MenuTree tree, StringBuilder menuBuilder)
        {
            menuBuilder.AppendLine("<li>");
            menuBuilder.AppendLine(MenuItem(tree.Menu));

            if (tree.ChildMenus != null && tree.ChildMenus.Count > 0)
            {
                menuBuilder.AppendLine("<ul class=\"sub-menu\">");

                foreach (MenuTree childTree in tree.ChildMenus)
                {
                    MenuBuilder(childTree, menuBuilder);
                }

                menuBuilder.AppendLine("</ul>");
            }

            menuBuilder.AppendLine("</li>");
        }

        private static string MenuItem(SAMenu menu)
        {
            // 构建a标签
            TagBuilder builder = new TagBuilder("a");
            string path = menu.MenuURL;

            if (path.IsNullOrEmpty())
            {
                builder.MergeAttribute("href", "javascript:;");
            }
            else
            {
                builder.MergeAttribute("onclick", string.Format("menu.addTabs('{0}',{1}')", menu.MenuName, path));
            }

            // 构建i标签
            string iTag = string.Empty;

            if (!menu.MenuImage.IsNullOrEmpty())
            {
                iTag = string.Format("<i class=\"{0}\"></i>", menu.MenuImage);
            }
            else
            {

                iTag = string.Format("<i class=\"icon-paper-plane\"></i>");
            }

            // 构建title标签
            string titleSpan = string.Format("<span class=\"title\">{0}</span>", menu.MenuName);

            builder.InnerHtml = string.Format("{0}{1}", iTag, titleSpan);

            return builder.ToString(TagRenderMode.Normal);
        }
    }
}