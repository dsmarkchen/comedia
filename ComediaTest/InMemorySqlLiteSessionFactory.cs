using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComediaTest
{    

    public class InMemorySqLiteSessionFactory : IDisposable
    {
        private Configuration _configuration;
        private ISessionFactory _sessionFactory;

        public ISession Session { get; set; }

        public InMemorySqLiteSessionFactory()
        {
            _sessionFactory = CreateSessionFactory();
            Session = _sessionFactory.OpenSession();
            ExportSchema();
        }
        public ISession reopen()
        {
            return _sessionFactory.OpenSession();
        }
        private void ExportSchema()
        {
            var export = new SchemaExport(_configuration);

            using (var file = new FileStream(@"c:\temp\createcomedia.objects.sql", FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(file))
                {
                    export.Execute(true, true, false, Session.Connection, sw);
                    sw.Close();
                }
            }
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                     .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
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
                     .ExposeConfiguration(cfg => _configuration = cfg)
                     .BuildSessionFactory();
        }

        public void Dispose()
        {
            Session.Dispose();
            _sessionFactory.Close();
        }
    }
}
