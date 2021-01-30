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
    public class PoliticianDomainTest
    {
        [Test]
        public void newPolitician_checkId_shouldBeZero()
        {
            Politician f = new Politician
            {
            };

            Assert.That(f.Id, Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class PoliticianRespositoryTestWithSqliteSessionFactory
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
        public void createPolitician_byDefault_shouldThrowException()
        {
            Politician f = new Politician
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
        public void createPolitician_withName_shouldCreate()
        {
            Politician f = new Politician
            {
                Name = "Augustus"
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





    }
}
