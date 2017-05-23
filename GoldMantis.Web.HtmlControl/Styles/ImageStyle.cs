/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         ImageStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 图片
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    public class ImageStyle
    {
        private static ImageStyle _instance;

        // 将图片变为圆形 (IE8 不支持)
        public readonly string ImgCircle = "img-circle";

        public readonly string ImgPolaroid = "img-polaroid";

        //图片响应式 (将很好地扩展到父元素)
        public readonly string ImgResponsive = "img-responsive";

        // 为图片添加圆角 (IE8 不支持)
        public readonly string ImgRounded = "img-rounded";

        //缩略图功能
        public readonly string ImgThumbnail = "img-thumbnail";

        static ImageStyle()
        {
            _instance = new ImageStyle();
        }

        private ImageStyle()
        {
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static ImageStyle Instance
        {
            get { return _instance; }
        }
    }
}