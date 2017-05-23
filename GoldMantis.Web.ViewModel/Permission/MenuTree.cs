using System.Collections.Generic;
using System.Linq;
using GoldMantis.Common;
using GoldMantis.Entity;

namespace GoldMantis.Web.ViewModel
{
    public class MenuTree
    {
        public SAMenu Menu { get; set; }

        public List<MenuTree> ChildMenus { get; set; }

        public static MenuTree CreateTree(IList<SAMenu> menus)
        {
            MenuTree tree = new MenuTree() ;

            AddChild(tree, menus);

            return tree;
        }

        private static void AddChild(MenuTree parentNode, IList<SAMenu> menus)
        {
            int parentId = parentNode.Menu != null ? parentNode.Menu.ID : 0;
            IEnumerable<MenuTree> childs = menus.Where(m => m.ParentID == parentId && m.IsOn).OrderBy(o => o.Sort).Select(m => new MenuTree() { Menu = m });

            if (childs.HasItems())
            {
                parentNode.ChildMenus = new List<MenuTree>(childs);

                foreach (MenuTree childMenu in parentNode.ChildMenus)
                {
                    AddChild(childMenu, menus);
                }
            }
        }
    }
}