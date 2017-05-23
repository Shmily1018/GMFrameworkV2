/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         ServiceBizMapping.cs
** Creator:          Joe
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      Service对象里Biz字段存储器
** VersionInfo:
*********************************************************/
namespace GoldMantis.DI
{
    public class ServiceBizMapping: FieldMapping
    {
        private static ServiceBizMapping _instance;

        static ServiceBizMapping()
        {
            _instance = new ServiceBizMapping();
        }

        private ServiceBizMapping()
        {
        }

        public static ServiceBizMapping Instance 
        {
            get { return _instance; }
        }
    }
}