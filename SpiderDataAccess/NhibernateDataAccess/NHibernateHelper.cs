#region ----------------备注----------------

// Author:TQ 
// FileName:NHibernateHelper.cs 
// Create Date:2017-06-13
// Create Time:16:34 

#endregion

using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;

namespace SpiderDataAccess.NhibernateDataAccess
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        /// <summary>
        ///     Nhibernate 配置文件
        /// </summary>
        public static Configuration Configuration { get; set; }

        public static ISessionFactory SessionFactory => _sessionFactory ?? (_sessionFactory = CreateSessionFactory());

        private static ISessionFactory CreateSessionFactory()
        {
            if (Configuration == null)
            {
                Configuration = new Configuration();
                Configuration.BeforeBindMapping += Configuration_BeforeBindMapping;
#if DEBUG
                Configuration.Configure("config/hibernate.cfg.xml");
                NHibernateProfiler.Initialize();
#else
                  Configuration.Configure();
#endif
            }
            var sessionFactory = Configuration.BuildSessionFactory();
            return sessionFactory;
        }

        /// <summary>
        ///     强制使用完全限定名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Configuration_BeforeBindMapping(object sender, BindMappingEventArgs e)
        {
            e.Mapping.autoimport = false;
        }

        public static IStatelessSession OpenStatelessSession()
        {
            return SessionFactory.OpenStatelessSession();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}