using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Win32;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormComedia
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    class xSessionManager
    {
        private static readonly ISessionFactory sessionFactory;
        private static Configuration _configuration;

        static xSessionManager()
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
            string db_fileName = RegistryHelper.CheckDBFile(); // Path.Combine(_pathDatabase, @"rolltest_" + jobname + @".db");
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
    public class Utilities
    {
        public static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        public static string GetProductVersion()
        {
            return System.Windows.Forms.Application.ProductName + System.Windows.Forms.Application.ProductVersion;
        }

    }


    public class RegistryHelper
    {
        private const string APP_NODENAME = @"SOFTWARE\Dante\Comedia";
        private const string KEY_DB_PATH = "dbFile";
        private const string VALUE_DB_PATH = @"c:\111\comedia.db";

        private static string read_key_value(string keyname, string default_value)
        {
            string keyValue = RegHelper.GetValue(APP_NODENAME, keyname);
            if (keyValue == "")
            {
                keyValue = default_value;
                RegHelper.SetValue(APP_NODENAME, keyname, keyValue);
            }
            return keyValue;
        }


        public static string CheckDBFile()
        {
            return read_key_value(KEY_DB_PATH, VALUE_DB_PATH);
        }


        public static void SaveDBFile(string dbName)
        {
            string keyValue = RegHelper.GetValue(APP_NODENAME, KEY_DB_PATH);
            if (keyValue != dbName)
            {
                RegHelper.SetValue(APP_NODENAME, KEY_DB_PATH, dbName);
            }
        }
    }


    public class RegHelper
    {
        public static string GetValue(string subKey, string valName, string defValue = "")
        {
            string resValue = "";

            RegistryKey rk = Registry.CurrentUser.OpenSubKey(subKey, false);
            if (rk == null)
            {
                rk = Registry.CurrentUser.CreateSubKey(subKey);
                rk.SetValue(valName, defValue);
            }
            else
            {
                var v = rk.GetValue(valName);
                if (v != null)
                {
                    resValue = v.ToString();
                }
            }

            return resValue;
        }

        public static bool SetValue(string subKey, string valName, string value)
        {
            bool resValue = false;

            RegistryKey rk = Registry.CurrentUser.OpenSubKey(subKey, true);
            if (rk == null)
            {
                rk = Registry.CurrentUser.CreateSubKey(subKey);
            }

            if (rk != null)
            {
                rk.SetValue(valName, value);
                resValue = true;
            }

            return resValue;
        }
    }
}
