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
    public class NoteTermRespositoryTestWithSqliteSessionFactory
    {

        InMemorySqLiteSessionFactory sqliteSessionFactory;
        ISession session;
        [SetUp]
        public void init()
        {
            sqliteSessionFactory = new InMemorySqLiteSessionFactory();


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
                Loc = new Loc
                 {
                     Book = "Inferno",
                     Canto = 1,
                     Start = 2,
                     End = 2
                 }
            };

            Term wildness = new Term
            {
                Name = "wood",
                Alias = "forest,wildness"
            };
            wildness.AddNote(note);
            wildness.SetMetaphorItem("metaphor", "");

            session = sqliteSessionFactory.Session;
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(note.Id == 0);
                    session.SaveOrUpdate(inferno);
                    session.SaveOrUpdate(note);
                    session.SaveOrUpdate(wildness);

                    transaction.Commit();
                }

            }
        }
        [TearDown]
        public void free()
        {
            sqliteSessionFactory.Dispose();
        }

       
       

        [Test]
        public void note_contains_term()
        {

            using (ISession session = sqliteSessionFactory.Session)
            {

                using (session.BeginTransaction())
                {
                    var resnotes = session.CreateCriteria(typeof(Note)).List<Note>();
                    var resnote = resnotes[0];

                    Assert.IsTrue(resnote.Term.Name == "wood");

                }
            }
        }
    }
}
