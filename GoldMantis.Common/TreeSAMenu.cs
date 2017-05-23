using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoldMantis.Common
{
    public class TreeSAMenu
    {

        public static String Menu(IList<SAMenu> menuModels)
        {

            SAMenuTree menuTree = SAMenuTree.CreateTree(menuModels);

            StringBuilder menuBuilder = new StringBuilder();

            foreach (SAMenuTree tree in menuTree.ChildMenus)
            {
                MenuBuilder(tree, menuBuilder);
            }
            menuBuilder.Remove(menuBuilder.Length - 1, 1);
            return menuBuilder.ToString();
            
        }

        private static void MenuBuilder(SAMenuTree tree, StringBuilder menuBuilder)
        {
            menuBuilder.Append("{");

            menuBuilder.Append(MenuItem(tree.Menu));

            if (tree.ChildMenus != null && tree.ChildMenus.Count > 0)
            {
                menuBuilder.Append(",\"children\":[");

                for (int index = 0; index < tree.ChildMenus.Count; index++)
                {
                    SAMenuTree childTree = tree.ChildMenus[index];
                    MenuBuilder(childTree, menuBuilder);
                    if (index == tree.ChildMenus.Count - 1)
                    {
                        menuBuilder=menuBuilder.Remove(menuBuilder.Length - 1, 1);
                    }
                    
                }

                menuBuilder.Append("]");
            }

            menuBuilder.Append("},");
            
        }

        private static string MenuItem(SAMenu menu)
        {
            string format ="\"text\":\"{0}\"";
            return String.Format(format, menu.MenuName);
        }
    }

    public class SAMenuTree
    {
        public SAMenu Menu { get; set; }

        public List<SAMenuTree> ChildMenus { get; set; }

        public static SAMenuTree CreateTree(IList<SAMenu> menus)
        {
            SAMenuTree tree = new SAMenuTree();
            AddChild(tree, menus);

            return tree;
        }

        private static void AddChild(SAMenuTree parentNode, IList<SAMenu> menus)
        {
            int parentId = parentNode.Menu != null ? parentNode.Menu.ID : 0;
            var childs = menus.Where(m => m.ParentID == parentId && m.IsOn).OrderBy(o => o.Sort).Select(m => new SAMenuTree() { Menu = m });

            if (childs.HasItems())
            {
                parentNode.ChildMenus = new List<SAMenuTree>(childs);

                foreach (SAMenuTree childMenu in parentNode.ChildMenus)
                {
                    AddChild(childMenu, menus);
                }
            }
        }
    }
}
