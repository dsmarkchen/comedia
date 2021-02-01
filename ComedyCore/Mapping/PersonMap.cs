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
            

            References(x => x.BornPlace, "BornPlace_id")   
                .Column("BornPlace_id")
                .ForeignKey("Born_PlaceId")
                .Cascade.All();

            References(x => x.DeadPlace, "DeadPlace_id")
                .Column("DeadPlace_id")
                .ForeignKey("Dead_PlaceId")
               .Cascade.All();

        }
    }

    public class PlaceMap : ClassMap<Place>
    {
        public PlaceMap()
        {
            Table("Place");
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name)
                .Not.Nullable();

            HasMany(x => x.People )   
                .KeyColumn("BornPlace_id")
                .Inverse()                
                .Cascade.All()
                .ForeignKeyConstraintName("FK_Born_PlaceId");

            HasMany(x => x.DeadPeople)
                .KeyColumn("DeadPlace_id")
                .Inverse()
                .Cascade.All()                
            ;
        }
    }
}
