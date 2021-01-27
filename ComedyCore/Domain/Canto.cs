using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Domain
{
    public class Canto: IEntity
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


        
        public virtual IList<Line> Lines
        {
            get;
            set;
        }
        public virtual Book Book
        {
            get;
            set;
        }

        public Canto()
        {
            Lines = new List<Line>();
        }

        public virtual void AddLine(Line m)
        {
            m.Canto = this;
            Lines.Add(m);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var line in Lines)
            {
                sb.AppendLine(line.Text);
            }
            return sb.ToString();
        }
    }
}
