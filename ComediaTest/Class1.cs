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
    public class LineDomainTest
    {
        [Test]
        public void newLine_checkId_shouldBeZero()
        {
            Line f = new Line
            {
            };
            Assert.That(f.Id, Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class LineRespositoryTestWithSqliteSessionFactory
    {
        bool CloseTo(float x, float x0)
        {
            if (Math.Abs(x - x0) < 0.00001)
            {
                return true;
            }
            else return false;
        }
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
        public void createRoll_byDefault_shouldCreate()
        {
            Line f = new Line
            {
                Text = "S"
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
