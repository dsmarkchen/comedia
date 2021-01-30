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

            Map(x => x.Name)
                .Not.Nullable();

            HasMany(x => x.Spouse)
                .KeyColumn("Person_id")
                .KeyNullable()
                .AsBag()
                .Inverse()
                .Cascade.All()
                .ForeignKeyConstraintName("FK_Person_PersonId");
            

            References(x => x.Place)    
                .Column("Place_id")
                .Cascade.All();

            References(x => x.DeadPlace)
                .Column("DeadPlace_id")
               .Cascade.All();

        }
    }

    public class PlaceMap : ClassMap<Place>
    {
        public PlaceMap()
        {
            Id(x => x.Id);

            Map(x => x.Name)
                .Not.Nullable();

            HasMany(x => x.People )
                .KeyColumn("Person_id")
                .Inverse()                
                .Cascade.All();            
        }
    }
}
