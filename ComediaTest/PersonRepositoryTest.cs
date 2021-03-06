﻿using ComediaCore.Domain;
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
    public class PersonRespositoryTestWithSqliteSessionFactory
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
        public void createPerson_byDefault_shouldThrowException()
        {
            Person f = new Person
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
        public void createPerson_withName_shouldCreate()
        {
            Person f = new Person
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

        [Test]
        public void createAugustus_withPlace_shouldCreate()
        {
            Place bornPlace = new Place
            {
                Name = "Rome"
            };
            Person augustus = new Person
            {
                Name = "Augustus"
            };
            bornPlace.AddPerson(augustus);

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(augustus.Id == 0);

                    session.SaveOrUpdate(augustus);
                    session.SaveOrUpdate(bornPlace);
                    transaction.Commit();

                    Assert.That(augustus.Id > 0);
                    Assert.That(augustus.BornPlace != null);
                }

            }
        }

        [Test]
        public void createAugustus_withPlaceBornAndDeath_shouldCreate()
        {
            Place bornPlace = new Place
            {
                Name = "Rome"
            };
            Place deadPlace = new Place
            {
                Name = "Nola"
            };
            Person augustus = new Person
            {
                Name = "Augustus"
            };
            bornPlace.AddPerson(augustus);
            deadPlace.AddPersonDead(augustus);

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(augustus.Id == 0);

                    
                    session.SaveOrUpdate(bornPlace);
                    session.SaveOrUpdate(deadPlace);
                    session.SaveOrUpdate(augustus);
                    transaction.Commit();

                    Assert.That(augustus.Id > 0);
                    Assert.That(augustus.BornPlace != null);
                    Assert.That(augustus.DeadPlace != null);
                }

            }
        }


        [Test]
        public void createAugustus_withSpouses_shouldCreate()
        {
            Place bornPlace = new Place
            {
                Name = "Rome"
            };
            Person augustus = new Person
            {
                Name = "Augustus"
            };
            bornPlace.AddPerson(augustus);

            Person claudia = new Person
            {
                Name = "Claudia"
            };
            Person scribonia = new Person
            {
                Name = "Scribonia"
            };
            Person livia = new Person
            {
                Name = "Livia"
            };
            augustus.AddSpouse(claudia);
            augustus.AddSpouse(scribonia);
            augustus.AddSpouse(livia);

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(augustus.Id == 0);

                    session.SaveOrUpdate(augustus);
                    session.SaveOrUpdate(claudia);
                    session.SaveOrUpdate(scribonia);
                    session.SaveOrUpdate(livia);
                    transaction.Commit();

                    Assert.That(augustus.Id > 0);
                    Assert.That(augustus.BornPlace != null);
                    Assert.That(livia.Id > 0);
                    Assert.That(livia.Spouse[0].Name == "Augustus");
                    Assert.That(augustus.Spouse[0].Name == "Claudia");
                    Assert.That(augustus.Spouse.Count == 3);

                }

            }
        }


        [Test]
        public void createCavalcanti_withSonAndFather_shouldCreate()
        {
            Person guido = new Person
            {
                Name = "Guido"
            };
            Person cavalcanti = new Person
            {
                Name = "Cavalcanti"
            };
            cavalcanti.AddChild(guido);
            

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(cavalcanti.Id == 0);

                    session.SaveOrUpdate(cavalcanti);
                    session.SaveOrUpdate(guido);
                    transaction.Commit();

                    Assert.That(cavalcanti.Id > 0);
                    Assert.That(cavalcanti.Children.Count == 1);
                    Assert.That(guido.Father.Name == "Cavalcanti");
                }

            }
        }

        [Test]
        public void createSilivius_withMotherAndFather_shouldCreate()
        {
            Person silivius = new Person
            {
                Name = "Silvius"
            };
            Person aenaes = new Person
            {
                Name = "Aenaes"
            };
            Person lavania = new Person
            {
                Name = "Lavania"
            };

            aenaes.AddChild(silivius);
            lavania.AddMotherChild(silivius);

            using (ISession session = sqliteSessionFactory.Session)
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Assert.That(silivius.Id == 0);

                    session.SaveOrUpdate(silivius);
                    session.SaveOrUpdate(aenaes);
                    session.SaveOrUpdate(lavania);
                    transaction.Commit();

                    Assert.That(aenaes.Id > 0);
                    Assert.That(lavania.Children.Count == 1);
                    Assert.That(silivius.Father.Name == "Aenaes");
                    Assert.That(silivius.Mother.Name == "Lavania");
                }

            }
        }
    }
}
