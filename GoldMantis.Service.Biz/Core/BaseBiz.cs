/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         BaseBiz.cs
** Creator:          
** CreateDate:       2015-08-13
** Changer:
** LastChangeDate:
** Description:      数据访问基类
** VersionInfo:
*********************************************************/
using GoldMantis.Common;
using GoldMantis.DI;
using GoldMantis.Service.Dal;

namespace GoldMantis.Service.Biz
{
    public class BaseBiz:IBiz
    {
        public BaseBiz()
        {
            ObjectInjection.Inject<IBiz, IDal>(this);
            //ObjectInjection.Inject<IBiz, IBiz>(this);
        }
    }

    public abstract class BaseBiz<T> : IBiz where T : class 
    {
        /// <summary>
        /// 注入数据访问对象
        /// </summary>
        public BaseBiz()
        {
            ObjectInjection.Inject<IBiz, IDal>(this);
            //ObjectInjection.Inject<IBiz, IBiz>(this);
        }

        /// <summary>
        /// DAL实例
        /// </summary>
        protected abstract IRepository<T> Repository { get; set; }
    }
}