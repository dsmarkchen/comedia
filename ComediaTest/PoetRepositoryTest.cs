using ComediaCore.Domain;
using NHibernate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaTest
{
    [TestFixture]
    public class PoetDomainTest
    {
        [Test]
        public void newPoet_checkId_shouldBeZero()
        {
            Poet f = new Poet
            {
            };
            
            Assert.That(f.Id, Is.EqualTo(0));
        }
    }


    [TestFixture]
    public class PoetRespositoryTestWithSqliteSessionFactory
    {

        InMemorySqLiteSessionFactory sqliteSessionFactory;

        [SetUp]
        public void init()
        {
            sqliteSessionFactory = new InMemorySqLiteSessionFactory();
        }
        [TearDown]
        public void free()
        {
            sqliteSessionFactory.Dispose();
        }

        [Test]
        public void createPoet_byDefault_shouldThrowException()
        {
            Poet f = new Poet
            {
            };

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(f.Id == 0);

                    Assert.Throws<NHibernate.PropertyValueException>(() => session.SaveOrUpdate(f));
                    
                }

            }
        }
       
        [Test]
        public void createPoetVirgil_withAeniedAndSomeCharacters_shouldCreate()
        {
            /*Person p1 = new Person
            {
                Name = "Camilla"
            };
            Person p2 = new Person
            {
                Name = "Nisus"
            };
            Person p3 = new Person
            {
                Name = "Virgil"
            };*/
            Character camilla = new Character
            {
                Name = "Camilla"   
            };
            Character nisus = new Character
            {
                Name = "Nisus"
            };
            Poem m = new Poem
            {
                Name = "Aeneid"
            };
            camilla.Poem = m;
            nisus.Poem = m;
            m.Characters.Add(camilla);
            m.Characters.Add(nisus);
            Poet f = new Poet
            {
                Name = "Virgil"
            };
            m.Author = f;
            f.Poems.Add(m);

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(f.Id == 0);

                    session.SaveOrUpdate(f);

                    transaction.Commit();

                    Assert.That(f.Id > 0);
                }
                using (session.BeginTransaction())
                {
                    var poets = session.CreateCriteria(typeof(Poet)).List<Poet>();
                    var virgil = poets[0];
                    Assert.That(virgil.Poems[0].Name == "Aeneid");
                    Assert.That(virgil.Poems[0].Characters.FirstOrDefault<Character>().Name == "Camilla");


                    var people = session.CreateCriteria(typeof(Person)).List<Person>();
                    Assert.That(people.Count == 3);
                }
            }
        }


        [Test]
        public void createPoetDante_withVilaNouvaandSomeCharacters_shouldCreate()
        {
            Character dante = new Character
            {
                Name = "Dante"
            };
            Character beatrice = new Character
            {
                Name = "Beatrice"
            };
            Poem m = new Poem
            {
                Name = "Vila Nouva"
            };
            dante.Poem = m;
            beatrice.Poem = m;
            m.Characters.Add(dante);
            m.Characters.Add(beatrice);
            Poet f = new Poet
            {
                Name = "Dante"
            };
            m.Author = f;
            f.Poems.Add(m);

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(f.Id == 0);

                    session.SaveOrUpdate(f);

                    transaction.Commit();

                    Assert.That(f.Id > 0);
                }
                using (session.BeginTransaction())
                {
                    var poets = session.CreateCriteria(typeof(Poet)).List<Poet>();
                    var poet = poets[0];
                    Assert.That(poet.Poems[0].Name == "Vila Nouva");
                    Assert.That(poet.Poems[0].Characters.FirstOrDefault<Character>().Name == "Dante");
                    
                    var people = session.CreateCriteria(typeof(Person)).List<Person>();
                    Assert.That(people.Count == 3);
                }
            }
        }


        [Test]
        public void createPoetsVirgilandDante_SomeCharacters_shouldCreate()
        {
            Place fl = new Place
            {
                Name = "Florence"
            };
            Place mantuan = new Place
            {
                Name = "Mantuan"
            };

            Character camilla = new Character
            {
                Name = "Camilla"
            };
            Character nisus = new Character
            {
                Name = "Nisus"
            };
            Poem m = new Poem
            {
                Name = "Aeneid"
            };
            camilla.Poem = m;
            nisus.Poem = m;
            m.Characters.Add(camilla);
            m.Characters.Add(nisus);
            Poet f = new Poet
            {
                Name = "Virgil",
                Place = mantuan
            };
            m.Author = f;
            f.Poems.Add(m);

            Character dante = new Character
            {
                Name = "Dante",
                Place = fl
            };
            Character beatrice = new Character
            {
                Name = "Beatrice",
                Place = fl
            };
            Poem m2 = new Poem
            {
                Name = "Vila Nouva"
            };
            dante.Poem = m2;
            beatrice.Poem = m2;
            m2.Characters.Add(dante);
            m2.Characters.Add(beatrice);
            Poet f2 = new Poet
            {
                Name = "Dante",
                Place = fl
            };
            m2.Author = f2;
            f2.Poems.Add(m2);

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(f.Id == 0);

                    session.SaveOrUpdate(f);
                    session.SaveOrUpdate(f2);
                    transaction.Commit();

                    Assert.That(f.Id > 0);
                    Assert.That(f2.Id > 0);
                }

                using (session.BeginTransaction())
                {
                    var poets = session.CreateCriteria(typeof(Poet)).List<Poet>();
                    var virgil = poets[0];
                    Assert.That(virgil.Poems[0].Name == "Aeneid");
                    Assert.That(virgil.Poems[0].Characters.FirstOrDefault<Character>().Name == "Camilla");

                    var dante_poet = poets[1];
                    Assert.That(dante_poet.Poems[0].Name == "Vila Nouva");
                    Assert.That(dante_poet.Poems[0].Characters.FirstOrDefault<Character>().Name == "Dante");

                    var people = session.CreateCriteria(typeof(Person)).List<Person>();
                    Assert.That(people.Count == 6);

                    var sites = session.CreateCriteria(typeof(Place)).List<Place>();
                    Assert.That(sites.Count == 2);
                }
            }
        }
    }
}
