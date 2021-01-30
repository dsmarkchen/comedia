using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Domain
{
    public class Politician : Person
    {
        public virtual string Role
        {
            get;
            set;
        }
        public virtual string Reign
        {
            get;
            set;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            if(!string.IsNullOrEmpty(Role))
            {
                sb.AppendLine("Role:    " + Role);
            }
            if (!string.IsNullOrEmpty(Reign))
            {
                sb.AppendLine("Reign:   " + Reign);
            }
            return base.ToString();
        }
    }
}
