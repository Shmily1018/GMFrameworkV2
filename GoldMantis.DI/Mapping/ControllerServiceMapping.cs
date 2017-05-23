/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         ControllerServiceMapping.cs
** Creator:          Joe
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      Controller对象里Service字段存储器
** VersionInfo:
*********************************************************/
namespace GoldMantis.DI
{
    public class ControllerServiceMapping : FieldMapping
    {
        private static ControllerServiceMapping _instance;

        static ControllerServiceMapping()
        {
            _instance = new ControllerServiceMapping();
        }

        private ControllerServiceMapping()
        {
        }

        public static ControllerServiceMapping Instance 
        {
            get { return _instance; }
        }
    }
}