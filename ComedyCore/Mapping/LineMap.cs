using ComediaCore.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Mapping
{
    
    public class LineMap : ClassMap<Line>
    {
        public LineMap()
        {
            Id(x => x.Id);

            Map(x => x.Number);
            Map(x => x.Text);
        }
    }
}
