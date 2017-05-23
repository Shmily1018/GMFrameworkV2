/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         BaseService.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      Service基类
** VersionInfo:
*********************************************************/
using GoldMantis.Common;
using GoldMantis.DI;

namespace GoldMantis.Service
{
    public abstract class BaseService : IService
    {
        /// <summary>
        /// 注入Biz对象
        /// </summary>
        public BaseService()
        {
            ObjectInjection.Inject<IService, IBiz>(this);
        }
    }
}