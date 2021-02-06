using System;
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

            Children = new List<Person>();
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
        public virtual string Alias
        {
            get;
            set;
        }

        public virtual string FullName
        {
            get;
            set;
        }
        public virtual Place BornPlace
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
            protected set;
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
                if (!found)
                {
                    person.Spouse.Add(this);
                    Spouse.Add(person);
                }
            }
            
        }

        public virtual IList<Person> Children
        {
            get;
            protected set;
        }

        public virtual Person Father
        {
            get;
            protected set;
        }

        public virtual Person Mother
        {
            get;
            protected set;
        }

        public virtual void AddChild(Person child)
        {

            if (this.Children != null)
            {
                bool found = false;
                foreach (var person in this.Children)
                {
                    if (person.Name == this.Name)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    child.Father = this;
                    this.Children.Add(child);                    
                }
            }
            
        }
        public virtual void AddMotherChild(Person child)
        {

            if (this.Children != null)
            {
                bool found = false;
                foreach (var person in this.Children)
                {
                    if (person.Name == this.Name)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    child.Mother = this;
                    this.Children.Add(child);
                }
            }

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Person.Id:     " + Id);
            sb.AppendLine("Person.Name:   " + Name);
            sb.AppendLine("Person.FullName:  " + FullName);
            if(BornPlace != null)
                sb.AppendLine("Person.Born:      " + BornPlace.Name);
            else
                sb.AppendLine("Person.Born:      " );

            if (DeadPlace != null)
                sb.AppendLine("Person.Dead:      " + DeadPlace.Name);
            else
            {
                sb.AppendLine("Person.Dead:      " );
            }

            if(Spouse.Count > 0)
            {
                foreach(var spouse in Spouse)
                {
                    sb.AppendLine("Person.Spouse:      " + spouse.Name);
                }
            }
            else
            {
                sb.AppendLine("Person.Spouse:      " );
            }

            if(Father != null || Mother != null)
            {
                StringBuilder sbParent = new StringBuilder();
                if(Father != null)
                {
                    sbParent.Append(Father.Name);                    
                }
                if (Mother != null)
                {
                    if(!string.IsNullOrEmpty(sbParent.ToString()))
                    {
                        sbParent.Append(", ");
                    }
                    sbParent.Append(Mother.Name);
                }
                sb.AppendLine("Person.Parent:      " + sbParent.ToString());
            }
            else
            {

            }


            if (Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    sb.AppendLine("Person.Child:      " + child.Name);
                }
            }
            else
            {
                sb.AppendLine("Person.Child:      ");
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
        public virtual string Alias
        {
            get;
            set;
        }

        public virtual IList<Person> People
        {
            get;
            protected set;
        }
        public virtual IList<Person> DeadPeople
        {
            get;
            protected set;
        }

        public Place()
        {
            People = new List<Person>();
            DeadPeople = new List<Person>();
        }

        public virtual void AddPerson(Person person)
        {
            person.BornPlace = this;
            People.Add(person);
        }
        public virtual void AddPersonDead(Person person)
        {
            person.DeadPlace = this;
            DeadPeople.Add(person);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Place.Id:     " + Id);
            sb.AppendLine("Place.Name: " + Name);
            foreach (var person in People)
            {
                sb.AppendLine("Place.BornPeople: " + person.Name);
            }
            foreach (var person in DeadPeople)
            {
                sb.AppendLine("Place.DeadPeople: " + person.Name);
            }
            return sb.ToString();
        }
    }

}
