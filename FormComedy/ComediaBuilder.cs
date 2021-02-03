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
        public static void build_places()
        {
            
        }
        public static void build_poets_elite_four()
        {
            string poet_str = "Homer, Horace, Ovid, Lucan";
            string born_str = ",Venusia, Sumona, ";
            string dead_str = ",Rome, , Rome";
            string write_str = "Ulysses,, The Metamorphoses, Pharsalia";

            string[] arr_poets = poet_str.Split(',');
            string[] arr_born = born_str.Split(',');
            string[] arr_dead = dead_str.Split(',');
            string[] arr_writing = dead_str.Split(',');
            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                int ind = 0;
                for(int i =0; i < arr_poets.Length; i++)
                {
                    if (string.IsNullOrEmpty(arr_poets[i])) continue;

                    var str_born = arr_born[i].Trim(new char[] { ' ' });
                    var str_dead = arr_dead[i].Trim(new char[] { ' ' });
                    Place pl_born;
                    Place pl_dead;
                    if (string.IsNullOrEmpty(str_born))
                        pl_born = null;
                    else
                    {
                        var places = DBHelper.GetAllWithRestrictionsEq<Place>("Name", str_born);
                        if(places != null)
                            pl_born = places[0];
                        else
                        {
                            pl_born = new Place
                            {
                                Name = str_born
                            };
                        }
                    }
                    if (string.IsNullOrEmpty(str_dead))
                        pl_dead = null;
                    else
                    {
                        var places = DBHelper.GetAllWithRestrictionsEq<Place>("Name", str_dead);
                        if (places != null)
                        {
                            pl_dead = places[0];
                        }
                        else { 
                            pl_dead = new Place
                            {
                                Name = str_dead
                            };
                        }
                    }

                    var objPoet = new Poet
                    {
                        Name = arr_poets[i].Trim(new char[] { ',', ' ' }),
                        
                    };
                    if(pl_born!=null)
                    {
                        pl_born.AddPerson(objPoet);
                    }
                    if (pl_dead != null)
                    {
                        pl_dead.AddPersonDead(objPoet);
                    }
                    
                    session1.SaveOrUpdate(objPoet);

                    if (pl_born != null)
                    {
                        session1.SaveOrUpdate(pl_born);
                    }
                    if (pl_dead != null)
                    {
                        session1.SaveOrUpdate(pl_dead);
                    }
                }


                transaction.Commit();
            }
        }

        public static void build_characters_aenaes()
        {
            var aeneid = DBHelper.GetAllWithRestrictionsEq<Poem>("Name", "Aeneid")[0];
            var ch_silvius = new Character
            {
                Name = "Silvius"
            };
            var ch_aeneas = new Character
            {
                Name = "Aeneas"
            };

            var ch_anchises = new Character
            {
                Name = "Anchises"
            };
            var ch_aphrodite = new Character
            {
                Name = "Aphrodite"
            };
            ch_anchises.AddChild(ch_aeneas);
            ch_aphrodite.AddMotherChild(ch_aeneas);

            var ch_latinus = new Character
            {
                Name = "Latinus"
            };
            

            var ch_lavinia = new Character
            {
                Name = "Lavinia"
            };
            ch_latinus.AddChild(ch_lavinia);
            ch_lavinia.AddMotherChild(ch_silvius);
            ch_aeneas.AddChild(ch_silvius);

            ch_aeneas.AddSpouse(ch_lavinia);


            aeneid.AddCharacter(ch_aeneas);
            aeneid.AddCharacter(ch_latinus);
            aeneid.AddCharacter(ch_lavinia);

            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.SaveOrUpdate(aeneid);
                session1.SaveOrUpdate(ch_aeneas);
                session1.SaveOrUpdate(ch_lavinia);
                session1.SaveOrUpdate(ch_silvius);

                transaction.Commit();
            }
        }
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
