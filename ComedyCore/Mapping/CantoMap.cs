using ComediaCore.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Mapping
{
   
    public class CantoMap : ClassMap<Canto>
    {
        public CantoMap()
        {
            Id(x => x.Id);

            Map(x => x.Number);

            HasMany(x => x.Lines)
                .Inverse()
                .Cascade.AllDeleteOrphan();

            References(x => x.Book);
        }
    }
}
