using ComediaCore.Domain;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaTest
{
    [TestFixture]
    public class NoteRespositoryTestWithSqliteSessionFactory
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
                Text = "I found myself astray in a dark wood,"
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


        [Test]
        public void createNote_fromTheFirst3LinesOfInferno()
        {
            Line line1 = new Line
            {
                Number = 1,
                Text = "Halfway along our journey to life's end"
            };
            Line line2 = new Line
            {
                Number = 2,
                Text = "I found myself astray in a dark wood,"
            };
            Line line3 = new Line
            {
                Number = 3,
                Text = "Since the rightway was no where to be found."
            };
            Line line4 = new Line
            {
                Number = 4,
                Text = "How hard a thing it is to express the horr"
            };
            Line line5 = new Line
            {
                Number = 5,
                Text = "of that wild wood, so difficult, so dense"
            };
            Line line6 = new Line
            {
                Number = 6,
                Text = "Even to think of it renews my terror."
            };
            Canto canto1 = new Canto
            {
                Number = 1
            };
            canto1.AddLine(line1);
            canto1.AddLine(line2);
            canto1.AddLine(line3);
            canto1.AddLine(line4);
            canto1.AddLine(line5);
            canto1.AddLine(line6);

            Book inferno = new Book
            {
                Number = 1,
                Name = "Inferno"
            };
            inferno.AddCanto(canto1);

            Note note = new Note
            {                
                Commentary = "Life span is 70 years, at the midpoint is 1300 CE, Dante was 35 years old."
            };
            note.Loc = new Loc
            {
                Book = "Inferno",
                Canto = 1,
                Start = 1
            };

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(note.Id == 0);
                    session.SaveOrUpdate(inferno);
                    session.SaveOrUpdate(note);

                    transaction.Commit();

                    Assert.That(note.Id > 0);
                }

                using (session.BeginTransaction())
                {
                    var resnotes = session.CreateCriteria(typeof(Note)).List<Note>();
                    var resnote = resnotes[0];

                    var resCanto = session.CreateCriteria(typeof(Canto))
                        .CreateCriteria("Book")
                                .Add(Restrictions.Eq("Id", 1))
                        .List<Canto>();
                    var resCanto2 = session.CreateCriteria(typeof(Canto))
                        .CreateCriteria("Book")
                                .Add(Restrictions.InsensitiveLike("Name", "Inferno"))
                        .List<Canto>();

                    var search = session.CreateCriteria(typeof(Line))
                        .Add(Restrictions.Eq("Number", 2))
                        .CreateCriteria("Canto")
                            .CreateCriteria("Book")
                                .Add(Restrictions.InsensitiveLike("Name", "Inferno"))
                            .Add(Restrictions.Eq("Number", 1))
                       
                         .List<Line>();
                    Assert.That(search[0].Text.Contains("wood") ==  true);
                }
            }
        }
    }
}
