using ComediaCore.Domain;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormComedia
{   
    public class DBHelper
    {
        public static void save<T>(T t)
        {

            ISession session1 = xSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.SaveOrUpdate(t);

                transaction.Commit();
            }
        }
        public static IList<T> GetAll<T>()
        {
            ISession session = xSessionManager.GetCurrentSession();
            var runs = session.CreateCriteria(typeof(T)).List<T>();
            if (runs.Count > 0)
                return runs;

            return null;
        }
        public static IList<T> GetAllWithRestrictionsLike<T>(string name, string value)
        {
            ISession session = xSessionManager.GetCurrentSession();
            var runs = session.CreateCriteria(typeof(T))
                .Add(Restrictions.Like(name, value, MatchMode.Anywhere))
                .List<T>();
            if (runs.Count > 0)
                return runs;

            return null;
        }
        public static IList<T> GetAllWithRestrictionsEq<T>(string name, object value)
        {
            ISession session = xSessionManager.GetCurrentSession();
            var runs = session.CreateCriteria(typeof(T))
                .Add(Restrictions.Eq(name, value))
                .List<T>();
            if (runs.Count > 0)
                return runs;

            return null;
        }


        public static IList<T> GetAllWithCriteriaEq<T>(string criteria, string name, object value)
        {
            ISession session = xSessionManager.GetCurrentSession();
            return session.CreateCriteria(typeof(T))
                                .CreateCriteria(criteria)
                                .Add(Restrictions.Eq(name, value))
                                .List<T>();
        }

        private static void CloseSession()
        {
            var session2 = xSessionManager.Unbind();
            if (session2 != null)
            {
                if (session2.Transaction.IsActive)
                {
                    try
                    {
                        session2.Transaction.Commit();
                    }
                    catch
                    {
                        session2.Transaction.Rollback();
                    }
                }
                session2.Close();
            }
        }
        public static bool Updatedb()
        {
            CloseSession();
            xSessionManager.UpdateSchema();
            return true;
        }
        public static bool Initdb()
        {
            CloseSession();
            xSessionManager.ExportSchema();
            return true;
        }
        public static void Import_Book(string name, int number, string text0)
        {
            Book book = new Book
            {
                Number = number,
                Name = name,
            };
            Canto canto = new Canto { };


            using (var reader = new StringReader(text0))
            {
                string txtline = string.Empty;
                var cantoNumber = 1;
                int lineNumber = 1;
                do
                {
                    txtline = reader.ReadLine();
                    if (txtline == null) break;
                    txtline = txtline.Replace("\t", "").Trim(' ');
                    if (!string.IsNullOrEmpty(txtline))
                    {
                        if (txtline.StartsWith("##"))
                        {
                            cantoNumber = int.Parse(Regex.Replace(txtline, "## (.*)", "$1", RegexOptions.IgnoreCase));
                            lineNumber = 1;
                            canto = new Canto
                            {
                                Number = cantoNumber
                            };
                            book.AddCanto(canto);
                        }
                        else
                        {
                            var line = new Line
                            {
                                Number = lineNumber,
                                Text = txtline
                            };
                            canto.AddLine(line);
                            lineNumber++;
                        }

                    }
                }
                while (txtline != null);
            }

            ISession session1 = xSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.Save(book);

                transaction.Commit();
            }
        }

        public static void Import_Poet(string name, string poem_name)
        {
            Poet poet = new Poet { 
                Name = name
            };            
            Poem poem = new Poem
            {
                Name = poem_name,
                Author = poet
            };
            poet.Poems.Add(poem);

            ISession session1 = xSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.SaveOrUpdate(poet);

                transaction.Commit();
            }
        }
        public static void Import_line(int number, string text)
        {
            Line run = new Line { };
            run.Number = number;
            run.Text = text;


            ISession session1 = xSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.Save(run);

                transaction.Commit();
            }
        }

       
    }
}
