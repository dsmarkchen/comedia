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
            var florence = DBHelper.GetAllWithRestrictionsEq<Place>("Name", "Florence")[0];

            var guido = new Poet
            {
                Name = "Guido",
                FullName = "Guido Cavalcanti"
            };
            florence.AddPerson(guido);
            florence.AddPersonDead(guido);

            var cavalcanti10 = new Politician
            {
                Name = "Cavalcante",
                FullName = "Cavalcante de' Cavalcanti"
            };
            cavalcanti10.AddChild(guido);

            var guido27 = new Politician
            {
                Name = "Guido",
                FullName = "Guido Da Montefeltro"
            };
            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.SaveOrUpdate(guido);
                session1.SaveOrUpdate(cavalcanti10);
                session1.SaveOrUpdate(guido27);
                transaction.Commit();
            }
        }

    }
}
