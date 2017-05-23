/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         BizRepository.cs
** Creator:          Joe
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      Biz对象仓储
** VersionInfo:
*********************************************************/
using GoldMantis.Common;

namespace GoldMantis.DI
{
    public class BizRepository : Repository<IBiz>
    {
        private static BizRepository _instance;

        static BizRepository()
        {
            _instance = new BizRepository();
        }

        private BizRepository()
        {
        }

        public static BizRepository Instance
        {
            get { return _instance; }
        }
    }
}