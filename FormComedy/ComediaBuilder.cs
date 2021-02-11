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
        public static void build_poets_elite_six()
        {
            string poet_str = "Dante,          Virgil,      Homer,   Horace,        Ovid,          Lucan";
            string born_str = "Florence,      Mantuan,   Venusia,    Sumona,            ,               ";
            string dead_str = "Ravenna,              ,      Rome,          ,        Rome,               ";
            string write_str ="Divine Comedy,  Aeneid,     Iliad,          , The Metamorphoses, Pharsalia";

            string[] arr_poets = poet_str.Split(',');
            string[] arr_born = born_str.Split(',');
            string[] arr_dead = dead_str.Split(',');
            string[] arr_writing = write_str.Split(',');
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
                    var poem_name = arr_writing[i].Trim(new char[] { ',', ' ' });
                    if(!string.IsNullOrEmpty(poem_name))
                    {
                        var poem = new Poem
                        {
                            Name = poem_name
                        };
                        objPoet.AddPoem(poem);
                        session1.SaveOrUpdate(poem);
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
        public static void build_characters_bible()
        {
            var poem = new Poem
            {
                Name = "Bible"
            };
            var ch_adam = new Character
            {
                Name = "Adam",
                Story = "Our first father"
            };
            var ch_eve = new Character
            {
                Name = "Eve",
                
            };
            var ch_Cain = new Character
            {
                Name = "Cain",
                Story = "The first murder of human history."
            };


            var ch_Abel = new Character
            {
                Name = "Abel",
                Story = "Killed by his brother. The first murder of human history."
            };

            var ch_Seth = new Character
            {
                Name = "Seth",                
            };
            var ch_Noah = new Character
            {
                Name = "Noah",
            };


            ch_adam.AddSpouse(ch_eve);

            ch_adam.AddChild(ch_Cain);
            ch_adam.AddChild(ch_Abel);
            ch_adam.AddChild(ch_Seth);

            ch_eve.AddMotherChild(ch_Cain);
            ch_eve.AddMotherChild(ch_Abel);
            ch_eve.AddMotherChild(ch_Seth);

            poem.AddCharacter(ch_adam);
            poem.AddCharacter(ch_eve);
            poem.AddCharacter(ch_Cain);
            poem.AddCharacter(ch_Abel);
            poem.AddCharacter(ch_Seth);

            poem.AddCharacter(ch_Noah);

            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.SaveOrUpdate(ch_adam);
                session1.SaveOrUpdate(ch_eve);
                session1.SaveOrUpdate(ch_Cain);
                session1.SaveOrUpdate(ch_Abel);

                session1.SaveOrUpdate(ch_Seth);
                
                session1.SaveOrUpdate(ch_Noah);

                session1.SaveOrUpdate(poem);
                transaction.Commit();
            }

        }

        public static void build_characters_minos()
        {
            var ch_Minos = new Character
            {
                Name = "Minos"
            };

            var ch_pasiphae = new Character
            {
                Name = "Pasiphaë"
            };
            ch_Minos.AddSpouse(ch_pasiphae);

            var ch_Minotaur = new Character
            {
                Name = "Minotaur"
            };
            ch_pasiphae.AddMotherChild(ch_Minotaur);

            var ch_Theseus = new Character
            {
                Name = "Theseus",
                Story = "Kills Minotaur"
            };
            var ch_Ariadne = new Character
            {
                Name = "Ariadne",
                Story = "Madly loves Theseus"
            };
            ch_Minos.AddChild(ch_Ariadne);
            ch_pasiphae.AddMotherChild(ch_Ariadne);


            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
               
                session1.SaveOrUpdate(ch_Minos);
                session1.SaveOrUpdate(ch_pasiphae);
                session1.SaveOrUpdate(ch_Minotaur);

                session1.SaveOrUpdate(ch_Theseus);
                session1.SaveOrUpdate(ch_Ariadne);

                transaction.Commit();
            }
        }
        public static void build_characters_erinyes()
        {
            
            var ch_Tisiphone = new Character
            {
                Name = "Tisiphone",   
                Story = "Punisher of murderers"
            };
            var ch_Alecto = new Character
            {
                Name = "Alecto",
                Story = "Punisher of moral crimes (angre, etc)"
            };
            var ch_Megaera = new Character
            {
                Name = "Megaera",
                Story = "Punisher of infidelity, oath breakers, and theft"
            };

            var ch_Medusa = new Character
            {
                Name = "Medusa",
                Story = "The ability to turn anyone to stone with her gaze"
            };

            var ch_Perseus = new Character
            {
                Name = "Perseus",
                Story = "Kill Medusa"
            };
            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {

                session1.SaveOrUpdate(ch_Tisiphone);
                session1.SaveOrUpdate(ch_Alecto);
                session1.SaveOrUpdate(ch_Megaera);

                session1.SaveOrUpdate(ch_Medusa);
                session1.SaveOrUpdate(ch_Perseus);
                transaction.Commit();
            }
        }

        public static void build_characters_electra()
        {
            var poem_Iliad= DBHelper.GetAllWithRestrictionsEq<Poem>("Name", "Iliad")[0];

            var ch_electra = new Character
            {
                Name = "Electra"
            };
            var ch_Agamemnon = new Character
            {
                Name = "Agamemnon"
            };
            var ch_Clytemnestra = new Character
            {
                Name = "Clytemnestra"
            };
            var ch_Orestes = new Character
            {
                Name = "Orestes"                    
            };



            var ch_Iphigenia = new Character
            {
                Name = "Iphigenia"
            };
            var ch_Chrysothemis = new Character
            {
                Name = "Chrysothemis"
            };
            ch_Agamemnon.AddSpouse(ch_Clytemnestra);

            ch_Agamemnon.AddChild(ch_Iphigenia);
            ch_Agamemnon.AddChild(ch_electra);
            ch_Agamemnon.AddChild(ch_Chrysothemis);
            ch_Agamemnon.AddChild(ch_Orestes);


            ch_Clytemnestra.AddMotherChild(ch_Iphigenia);
            ch_Clytemnestra.AddMotherChild(ch_electra);
            ch_Clytemnestra.AddMotherChild(ch_Chrysothemis);
            ch_Clytemnestra.AddMotherChild(ch_Orestes);

            poem_Iliad.AddCharacter(ch_Agamemnon);
            poem_Iliad.AddCharacter(ch_Iphigenia);
            poem_Iliad.AddCharacter(ch_electra);
            poem_Iliad.AddCharacter(ch_Chrysothemis);
            poem_Iliad.AddCharacter(ch_Orestes);

            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.SaveOrUpdate(ch_Iphigenia);
                session1.SaveOrUpdate(ch_electra);
                session1.SaveOrUpdate(ch_Chrysothemis);
                session1.SaveOrUpdate(ch_Orestes);


                session1.SaveOrUpdate(ch_Agamemnon);
                session1.SaveOrUpdate(ch_Clytemnestra);
                
                session1.SaveOrUpdate(poem_Iliad);
                transaction.Commit();
            }
        }

        public static void build_characters_comedy()
        {
            var comedy = DBHelper.GetAllWithRestrictionsEq<Poem>("Name", "Divine Comedy")[0];

            var ch_dante = new Character
            {
                Name = "Dante-Pilgrim"
            };
            var ch_dante2 = new Character
            {
                Name = "Dante-Narrator"
            };

            var ch_virgil = new Character
            {
                Name = "Virgil"
            };
            var ch_beatrice = new Character
            {
                Name = "Beatrice"
            };

            var ch_Erichtho = new Character
            {
                Name = "Erichtho",
                Story = "A Witch. Summon Virgil to the lowest hell. (Virgil's dark past)"
            };
            comedy.AddCharacter(ch_dante);
            comedy.AddCharacter(ch_dante2);
            comedy.AddCharacter(ch_beatrice);
            comedy.AddCharacter(ch_virgil);

            comedy.AddCharacter(ch_Erichtho);


            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.SaveOrUpdate(ch_Erichtho);
                session1.SaveOrUpdate(comedy);

                transaction.Commit();
            }
        }
        public static void build_notes()
        {
            var midway = new Note
            {
                Name = "midway",
                Commentary = "Dante was 35 years old, 1300 C.E.",
                Loc = new Loc
                {
                    Book = "Inferno",
                    Canto = 1,
                    Start = 1,
                    End = 1,
                }
                
            };
            var forest = new Note
            {   
                Name = "wood",
                Loc = new Loc
                {
                    Book = "Inferno",
                    Canto = 1,
                    Start = 2,
                    End = 2,
                }

            };
            Term wood = DBHelper.GetAllWithRestrictionsEq<Term>("Name", "wood")[0];
            if(wood != null)
            {
                wood.AddNote(forest);
            }

            var leopardNote = new Note
            {                
                Loc = new Loc
                {
                    Book = "Inferno",
                    Canto = 1,
                    Start = 33,
                    End = 33,
                }
            };
            Term leopard = DBHelper.GetAllWithRestrictionsEq<Term>("Name", "leopard")[0];
            leopard.AddNote(leopardNote);

            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.SaveOrUpdate(midway);
                session1.SaveOrUpdate(forest);

                session1.SaveOrUpdate(leopard);
                if(wood!=null)
                {
                    session1.SaveOrUpdate(wood);
                }
                session1.SaveOrUpdate(leopardNote);
                transaction.Commit();
            }
        }
        public static void build_terms()
        {
            string poets_str = "dante,  virgil,  homer, horice, ovid, lucan";
            string character_str = "beatrice";

            string[] arr_metaphors = poets_str.Split(',');
            string[] arr_characters = character_str.Split(',');
            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                for (int i = 0; i < arr_metaphors.Length; i++)
                {
                    if (string.IsNullOrEmpty(arr_metaphors[i])) continue;


                    var term = new Term
                    {
                        Name = arr_metaphors[i].Trim(new char[] { ',', ' ' }),

                    };
                    term.SetMetaphorItem("category", "poet");
                    session1.SaveOrUpdate(term);
                }

                for (int i = 0; i < arr_characters.Length; i++)
                {
                    if (string.IsNullOrEmpty(arr_characters[i])) continue;


                    var term = new Term
                    {
                        Name = arr_characters[i].Trim(new char[] { ',', ' ' }),

                    };
                    term.SetMetaphorItem("category", "character");
                    session1.SaveOrUpdate(term);
                }

                transaction.Commit();
            }
        }
        public static void build_metaphors_inferno()
        {
            string metaphor_str =  "wood,  leopard,  lion, she-wolf, dove ";
            
            string[] arr_metaphors = metaphor_str.Split(',');
            ISession session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {   
                for (int i = 0; i < arr_metaphors.Length; i++)
                {
                    if (string.IsNullOrEmpty(arr_metaphors[i])) continue;


                    var term = new Term
                    {
                        Name = arr_metaphors[i].Trim(new char[] { ',', ' ' }),
                    
                    };

                    session1.SaveOrUpdate(term);

                    
                }
                
                transaction.Commit();
            }
            Term wood = DBHelper.GetAllWithRestrictionsEq<Term>("Name", "wood")[0];
            wood.Alias = "forest";

            Term leopard = DBHelper.GetAllWithRestrictionsEq<Term>("Name", "leopard")[0];
            Term lion = DBHelper.GetAllWithRestrictionsEq<Term>("Name", "lion")[0];
            Term shewolf = DBHelper.GetAllWithRestrictionsEq<Term>("Name", "she-wolf")[0];
            Term dove = DBHelper.GetAllWithRestrictionsEq<Term>("Name", "dove")[0];

            leopard.SetMetaphorItem("sin", "lust");
            leopard.SetMetaphorItem("trinity", "leopard, lion, she-wolf");

            lion.SetMetaphorItem("sin", "pride");
            lion.SetMetaphorItem("trinity", "leopard, lion, she-wolf");

            shewolf.Alias = "shewolf";
            shewolf.SetMetaphorItem("sin", "avarice");
            shewolf.SetMetaphorItem("trinity", "leopard, lion, she-wolf");


            dove.SetMetaphorItem("aphrodite", "love");
            dove.SetMetaphorItem("christian", "holy spirit");

            session1 = SQLiteSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.SaveOrUpdate(wood);
                session1.SaveOrUpdate(leopard);
                session1.SaveOrUpdate(lion);
                session1.SaveOrUpdate(shewolf);

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
