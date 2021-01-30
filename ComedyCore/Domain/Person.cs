﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Domain
{
    public class Person : IEntity
    {
        public Person()
        {
            Spouse = new List<Person>();
        }
        public virtual int Id
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string FullName
        {
            get;
            set;
        }
        public virtual Place Place
        {
            get;
            set;
        }
        public virtual Place DeadPlace
        {
            get;
            set;
        }

        public virtual IList<Person> Spouse
        {
            get;
            set;
        }
        public virtual void AddSpouse(Person person)
        {

            if (person.Spouse != null)
            {
                bool found = false;
                foreach(var spouse in person.Spouse)
                {
                    if(person.Name == this.Name)
                    {
                        found = true;
                        break;
                    }
                }
                if(!found)
                    person.Spouse.Add(this);
            }
            Spouse.Add(person);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Person.Id:     " + Id);
            sb.AppendLine("Person.Name:   " + Name);
            if(!string.IsNullOrEmpty(FullName))
            {
                sb.AppendLine("Person.FullName:  " + FullName);
            }
            if (Place != null)
            {
                sb.AppendLine("Person.Born:      " + Place.ToString());
            }
            if (DeadPlace != null)
            {
                sb.AppendLine("Person.Dead:      " + DeadPlace.ToString());
            }
            if(Spouse.Count > 0)
            {
                foreach(var spouse in Spouse)
                {
                    sb.AppendLine("Person.Spouse:      " + spouse.Name);
                }
            }
            return sb.ToString();
        }
        
    }

    public class Place
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }
        public virtual IList<Person> People
        {
            get;
            set;
        } 

        public Place()
        {
            People = new List<Person>();
        }

        public virtual void AddPerson(Person person)
        {
            person.Place = this;
            People.Add(person);
        }
        public virtual void AddPersonDead(Person person)
        {
            person.DeadPlace = this;
            People.Add(person);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Place.Id:     " + Id);
            sb.AppendLine("Place.Name: " + Name);
            
            return sb.ToString();
        }
    }

}
