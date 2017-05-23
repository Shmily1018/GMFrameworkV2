/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         ListGroupStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 列表组
** VersionInfo:
*********************************************************/
namespace GoldMantis.Web.HtmlControl.Styles
{
    /**
     * 创建一个基本的列表组的步骤如下：

        向元素 <ul> 添加 class .list-group。
        向 <li> 添加 class .list-group-item。

     */

    public class ListGroupStyle
    {
        private static ListGroupStyle _instance;

        public readonly string List = "list";

        public readonly string ListGroup = "list-group";

        public readonly string ListGroupItem = "list-group-item";

        public readonly string ListGroupItemDanger = "list-group-item-danger";

        public readonly string ListGroupItemHeading = "list-group-item-heading";

        public readonly string ListGroupItemInfo = "list-group-item-info";

        public readonly string ListGroupItemSuccess = "list-group-item-success";

        public readonly string ListGroupItemText = "list-group-item-text";

        public readonly string ListGroupItemWarning = "list-group-item-warning";

        private ListGroupStyle()
        {
        }

        static ListGroupStyle()
        {
            _instance = new ListGroupStyle();
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static ListGroupStyle Instance
        {
            get { return _instance; }
        }
    }
}