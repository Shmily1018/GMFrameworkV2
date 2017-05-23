/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         BizDalMapping.cs
** Creator:          Joe
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      Biz对象里Dal字段存储器
** VersionInfo:
*********************************************************/
namespace GoldMantis.DI
{
    public class BizDalMapping: FieldMapping
    {
        private static BizDalMapping _instance;

        static BizDalMapping()
        {
            _instance = new BizDalMapping();
        }

        private BizDalMapping()
        {
        }

        public static BizDalMapping Instance 
        {
            get { return _instance; }
        }
    }
}