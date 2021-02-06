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
    public class TermDomainTest
    {
        [Test]
        public void newTerm_checkId_shouldBeZero()
        {
            Term f = new Term
            {
            };

            Assert.That(f.Id, Is.EqualTo(0));
        }
        [Test]
        public void toString_checkName_containsName()
        {
            Term term = new Term
            {
                Name = "leopard"
            };

            Assert.That(term.Id, Is.EqualTo(0));
            Assert.That(term.ToString().Contains("leopard"));
        }
    }

    [TestFixture]
    public class TermRespositoryTestWithSqliteSessionFactory
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
        public void createTerm_byDefault_termMustHasAName()
        {
            Term f = new Term
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
        public void createTerm_withName_shouldCreate()
        {
            Term f = new Term
            {
                Name = "Wood"
            };

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(f.Id == 0);

                    session.SaveOrUpdate(f);
                    Assert.That(f.Id > 0);
                }

            }


        }

        [Test]
        public void createTerm_woodIsATermMetaphor_shouldCreate()
        {
            // dark wood is a metaphor 
            // enterace of the dark gete, those words is metaphor ->  soul's joul

            Term wood = new Term
            {
                Name = "Wood",
                Alias = "Forest,Wildness",

            };

            wood.SetMetaphorItem("Metaphor", "Night journey of soul");
            

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(wood.Id == 0);

                    session.SaveOrUpdate(wood);
                    Assert.That(wood.Id > 0);
                }

            }


        }

        [Test]
        public void createTerm_beasts_shouldCreate()
        {
            // leopard, she-wolf and lion are all metaphors
            // leopard, she-wolf and lion together, they are trinity (source)
            // optimence, primal love and wisdom ------> trinity (source)

            // dove metaphor (adiph) -> love
            //               (jesus) -> holy spirit descent into his body

            Term leopard = new Term
            {
                Name = "Leopard",
                Alias = ""
            };
            Term lion = new Term
            {
                Name = "Lion",
                Alias = ""
            };
            Term shewolf = new Term
            {
                Name = "She-Wolf",
                Alias = "Shewolf, She wolf",
            };

            leopard.SetMetaphorItem("Metaphor", "");
            leopard.SetMetaphorItem("Trinity", "leopard, lion, shewolf");

            lion.SetMetaphorItem("Metaphor", "");
            lion.SetMetaphorItem("Trinity", "leopard, lion, shewolf");

            shewolf.SetMetaphorItem("Metaphor", "");
            shewolf.SetMetaphorItem("Trinity", "leopard, lion, shewolf");


            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(leopard.Id == 0);

                    session.SaveOrUpdate(leopard);
                    session.SaveOrUpdate(lion);
                    session.SaveOrUpdate(shewolf);
                    Assert.That(leopard.Id > 0);
                }

            }


        }


        [Test]
        public void createTerm_dove_shouldCreate()
        {
            // dove metaphor (adiph) -> love
            //               (jesus) -> holy spirit descent into his body

            Term dove = new Term
            {
                Name = "dove",
                Alias = ""
            };

           


            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(dove.Id == 0);

                    session.SaveOrUpdate(dove);
                    Assert.That(dove.Id > 0);
                }

            }


        }
    }
}
