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
        public void createPoet_byDefault_shouldCreate()
        {
            Poet f = new Poet
            {
            };

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(f.Id == 0);

                    session.SaveOrUpdate(f);

                    transaction.Commit();

                    Assert.That(f.Id > 0);
                }

            }
        }
        [Test]
        public void createPoetVirgil_withAeniedAndSomeCharacters_shouldCreate()
        {
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
                }
            }
        }
    }
}
