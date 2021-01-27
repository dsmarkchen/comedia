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
    public class BookRespositoryTestWithSqliteSessionFactory
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
        public void createBoook_byDefault_shouldCreate()
        {
            Book f = new Book
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
        public void createBook_withFirst3LinesOfInferno_shouldCreate()
        {
            Line line1 = new Line
            {
                Number = 1,
                Text = "Halfway along our journey to life's end"
            };
            Line line2 = new Line
            {
                Number = 2,
                Text = "I found myself astray in a darkwood,"
            };
            Line line3 = new Line
            {
                Number = 3,
                Text = "Since the rightway was no where to be found."
            };
            Canto canto1 = new Canto
            {
                Number = 1
            };
            canto1.AddLine(line1);
            canto1.AddLine(line2);
            canto1.AddLine(line3);

            Book inferno = new Book
            {
                Number = 1,
                Name = "Inferno"
            };
            inferno.AddCanto(canto1);


            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(inferno.Id == 0);

                    session.SaveOrUpdate(inferno);

                    transaction.Commit();

                    Assert.That(inferno.Id > 0);
                }

                using (session.BeginTransaction())
                {
                    var resbooks = session.CreateCriteria(typeof(Book)).List<Book>();
                    var resbook = resbooks[0];
                    var rescantos = resbooks[0].Cantos;
                    var text = rescantos[0].ToString();
                    Assert.That(resbook.Name == "Inferno");
                    Assert.That(text.StartsWith("Halfway"));
                }

            }
        }
    }
}
