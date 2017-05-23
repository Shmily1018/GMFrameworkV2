using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GoldMantis.Common
{
    public class TreeSCDepartment
    {

        public static String Department(IList<SCDepartment> departmentModels)
        {

            SCDepartmentTree departmentTree = SCDepartmentTree.CreateTree(departmentModels);

            StringBuilder departmentBuilder = new StringBuilder();

            foreach (SCDepartmentTree tree in departmentTree.ChildDepartments)
            {
                DepartmentBuilder(tree, departmentBuilder);
            }
            departmentBuilder.Remove(departmentBuilder.Length - 1, 1);
            return departmentBuilder.ToString();
            
        }

        private static void DepartmentBuilder(SCDepartmentTree tree, StringBuilder departmentBuilder)
        {
            departmentBuilder.Append("{");

            departmentBuilder.Append(DepartmentItem(tree.Department));

            if (tree.ChildDepartments != null && tree.ChildDepartments.Count > 0)
            {
                departmentBuilder.Append(",\"children\":[");

                for (int index = 0; index < tree.ChildDepartments.Count; index++)
                {
                    SCDepartmentTree childTree = tree.ChildDepartments[index];
                    DepartmentBuilder(childTree, departmentBuilder);
                    if (index == tree.ChildDepartments.Count - 1)
                    {
                        departmentBuilder.Remove(departmentBuilder.Length - 1,1);
                    }
                    
                }

                departmentBuilder.Append("]");
            }
            else
            {
                departmentBuilder.Append(",\"icon\":\"fa fa-file icon-state-warning\"");
            }

            departmentBuilder.Append("},");
            
        }

        private static string DepartmentItem(SCDepartment department)
        {
            string format ="\"text\":\"{0}\"";
            return String.Format(format, department.DeptName);
        }
    }

    public class SCDepartmentTree
    {
        public SCDepartment Department { get; set; }

        public List<SCDepartmentTree> ChildDepartments { get; set; }

        public static SCDepartmentTree CreateTree(IList<SCDepartment> departments)
        {
            SCDepartmentTree tree = new SCDepartmentTree();
            AddChild(tree, departments);

            return tree;
        }

        private static void AddChild(SCDepartmentTree parentNode, IList<SCDepartment> departments)
        {
            int parentId = parentNode.Department != null ? parentNode.Department.ID : 0;
            var childs = departments.Where(m => m.ParentID == parentId && m.IsOn).OrderBy(o => o.SortNum).Select(m => new SCDepartmentTree() { Department = m });

            if (childs.HasItems())
            {
                parentNode.ChildDepartments = new List<SCDepartmentTree>(childs);

                foreach (SCDepartmentTree childDepartment in parentNode.ChildDepartments)
                {
                    AddChild(childDepartment, departments);
                }
            }
        }
    }
}
