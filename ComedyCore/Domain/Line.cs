using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Domain
{
    public class Line
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

        public virtual string Text
        {
            get;
            set;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
