using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoldMantis.Common;
using GoldMantis.DI;

namespace GoldMantis.ServiceNew.ServiceInterface
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