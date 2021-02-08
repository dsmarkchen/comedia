using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Domain
{
    public class Book : IEntity
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual int Number
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


        public virtual IList<Canto> Cantos
        {
            get;
            set;
        }

        public Book()
        {
            Cantos = new List<Canto>();
        }

        public virtual void AddCanto(Canto m)
        {
            m.Book = this;
            Cantos.Add(m);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var canto in Cantos)
            {
                sb.AppendLine(canto.ToString());
            }
            return sb.ToString();
        }
    }
}
