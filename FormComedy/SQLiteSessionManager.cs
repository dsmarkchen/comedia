using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FormComedia
{   
    class SQLiteSessionManager
    {
        private static readonly ISessionFactory sessionFactory;
        private static Configuration _configuration;

        static SQLiteSessionManager()
        {
            sessionFactory = CreateSessionFactory("default");
        }

        public static ISession GetCurrentSession()
        {
            if (CurrentSessionContext.HasBind(sessionFactory))
            {
                return sessionFactory.GetCurrentSession();
            }

            var session = sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
            return session;
        }

        public static ISession Unbind()
        {
            return CurrentSessionContext.Unbind(sessionFactory);
        }


        private static ISessionFactory CreateSessionFactory(string jobname)
        {
            string db_fileName = RegistryHelper.CheckDBFile(); 
            return Fluently.Configure()
                     .Database(SQLiteConfiguration.Standard.UsingFile(db_fileName))
                     .Mappings(m =>
                     {
                         m.FluentMappings.Conventions.Setup(c => c.Add(AutoImport.Never()));
                         m.FluentMappings.Conventions.AddAssembly(Assembly.GetExecutingAssembly());
                         m.HbmMappings.AddFromAssembly(Assembly.GetExecutingAssembly());

                         var assembly = Assembly.Load("ComediaCore");
                         m.FluentMappings.Conventions.AddAssembly(assembly);
                         m.FluentMappings.AddFromAssembly(assembly);
                         m.HbmMappings.AddFromAssembly(assembly);

                     })
                     .ExposeConfiguration(cfg =>
                     {
                         cfg.SetProperty("current_session_context_class", "thread_static");
                         _configuration = cfg;
                     })
                     .BuildSessionFactory();
        }
        public static void ExportSchema()
        {
            string pathDatabase = Path.GetDirectoryName(RegistryHelper.CheckDBFile());
            string pathName = Path.Combine(pathDatabase, @"initial.database");
            Utilities.CreateIfMissing(pathDatabase);
            Utilities.CreateIfMissing(pathName);
            var export = new SchemaExport(_configuration);
            string fileName = Path.Combine(pathDatabase, @"initial.database\create.objects.sql");

            using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(file))
                {
                    export.Execute(true, true, false, GetCurrentSession().Connection, sw);
                    sw.Close();
                }
            }
        }

        public static void UpdateSchema()
        {
            string pathDatabase = Path.GetDirectoryName(RegistryHelper.CheckDBFile());
            string pathName = Path.Combine(pathDatabase, @"update.database");
            Utilities.CreateIfMissing(pathDatabase);
            Utilities.CreateIfMissing(pathName);
            var update = new SchemaUpdate(_configuration);
            update.Execute(false, true);
        }
    }
}
