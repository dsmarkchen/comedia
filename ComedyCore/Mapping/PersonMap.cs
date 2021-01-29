using ComediaCore.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Mapping
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);

            HasOne(x => x.Place)
                .Cascade.All();

            /*
            References(x => x.Poet).Cascade.All();

            References(x => x.Character).Cascade.All();
            */
        }
    }

    public class PlaceMap : ClassMap<Place>
    {
        public PlaceMap()
        {
            Id(x => x.Id);

            Map(x => x.Name)
                .Not.Nullable();

            References(x => x.Person).Cascade.All();            
        }
    }
}
