/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         DalRepository.cs
** Creator:          Joe
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      dal对象仓储
** VersionInfo:
*********************************************************/
using GoldMantis.Common;

namespace GoldMantis.DI
{
    public class DalRepository : Repository<IDal>
    {
        private static DalRepository _instance;

        static DalRepository()
        {
            _instance = new DalRepository();
        }

        private DalRepository()
        {
        }

        public static DalRepository Instance
        {
            get { return _instance; }
        }
    }
}