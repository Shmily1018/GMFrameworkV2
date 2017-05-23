using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldMantis.Common.Data;
using NHibernate;
using NHibernate.Cfg;


namespace GoldMantis.Common
{
    public class NHibernateHelperWithConnection
    {
        public static ISession GetSessionWithConnection(string selfConnectionString,EnumDBType dbType)
        {
            //共性属性
            var cfg = new Configuration().Configure();
            cfg.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            cfg.SetProperty("connection.connection_string", selfConnectionString);

            if (dbType == EnumDBType.SQL_SERVER)
            {
                cfg.SetProperty("dialect", "NHibernate.Dialect.MsSql2005Dialect");
                cfg.SetProperty("connection.driver_class", "NHibernate.Driver.SqlClientDriver");
            }
            else if (dbType == EnumDBType.ORACLE)
            {
                cfg.SetProperty("dialect", "NHibernate.Dialect.Oracle10gDialect");
                cfg.SetProperty("connection.driver_class", "NHibernate.Driver.OracleClientDriver");
            }


            return cfg.BuildSessionFactory().OpenSession();
        }

    }
}
