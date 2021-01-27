using ComediaCore.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Mapping
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Id(x => x.Id);

            Map(x => x.Number);

            HasMany(x => x.Cantos)
                .Inverse()
                .Cascade.AllDeleteOrphan();
            
        }
    }
}
