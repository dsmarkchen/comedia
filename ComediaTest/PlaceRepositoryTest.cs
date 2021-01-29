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
    public class PlaceDomainTest
    {
        [Test]
        public void newPlace_checkId_MustCreate()
        {
            Place f = new Place
            {
            };
            Assert.That(f.Id == 0);
        }
    }


    [TestFixture]
    public class PlaceRespositoryTestWithSqliteSessionFactory
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
        
        public void createPlace_byDefault_expectedExeception()
        {
            Place f = new Place
            {                
            };

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(f.Id == 0);

                    Assert.Throws<NHibernate.PropertyValueException>( () => session.SaveOrUpdate(f));
                }

            }
        }

        [Test]
        public void createPlace_WithName_shouldCreate()
        {
            Place f = new Place
            {
                Name = "Mantua"
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
        public void createMultiplePlaces_WithName_shouldCreate()
        {
            Place m = new Place
            {
                Name = "Mantua"
            };
            Place f = new Place
            {
                Name = "Florence"
            };
            Person person = new Person
            {
                Name = "Virgil",
                Place = m
            };
            Person person2 = new Person
            {
                Name = "Dante",
                Place = f
            };

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(f.Id == 0);

                    session.SaveOrUpdate(person);
                    session.SaveOrUpdate(person2);
                    transaction.Commit();
                    Assert.That(person.Id > 0);
                    Assert.That(person2.Id > 0);
                    Assert.That(m.Id > 0);
                    Assert.That(f.Id > 0);
                }

            }
        }



    }
}
