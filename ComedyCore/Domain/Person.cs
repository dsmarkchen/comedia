using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Domain
{
    public class Person : IEntity
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Person.Name:   " + Name);
            if(!string.IsNullOrEmpty(FullName))
            {
                sb.AppendLine("Person.FullName:  " + FullName);
            }
            if (Place != null)
            {
                sb.AppendLine("Person.Born:      " + Place.ToString());
            }
            return sb.ToString();
        }
        /*
        public virtual Poet Poet
        {
            get;
            set;
        }
        public virtual Character Character
        {
            get;
            set;
        }
        */
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
        public virtual Person Person
        {
            get;
            set;
        }

        

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Place.Name: " + Name);
            
            return sb.ToString();
        }
    }

}
