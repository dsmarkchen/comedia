using ComediaCore.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormComedia
{
    public class ComediaBuilder
    {
        public static void build_politician()
        {
            var guido = new Politician
            {
                Name = "Guido",
                FullName = "Guido Da Montefeltro"
            };
            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.SaveOrUpdate(guido);

                transaction.Commit();
            }
        }

    }
}
