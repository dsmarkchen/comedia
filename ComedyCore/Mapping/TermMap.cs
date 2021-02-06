using ComediaCore.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Mapping
{
    public class TermMap : ClassMap<Term>
    {
        public TermMap()
        {
            Table("Term");
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name)
                .Not.Nullable();
            Map(x => x.Alias);

            HasMany(x => x.TermItems)
                .AsMap(x => x.Type)
                .Cascade.All();


        }
    }

    public class TermItemmap : ClassMap<TermItem>
    {
        public TermItemmap()
        {
            Table("TermItem");

            Id(x => x.Type).GeneratedBy.Assigned();
            Map(x => x.Meaning);
                
                
        }
    }
}
