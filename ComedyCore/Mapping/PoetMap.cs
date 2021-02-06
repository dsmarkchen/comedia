using ComediaCore.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Mapping
{

    public class CharacterMap : SubclassMap<Character>
    {
        public CharacterMap()
        {
            KeyColumn("Person_id");
            
            Map(x => x.Story);

            References(x => x.Poem);
        }
    }

    public class PoemMap : ClassMap<Poem>
    {
        public PoemMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Alias);

            HasMany(x => x.Characters)
                .AsSet()
                .Inverse()
                .Cascade.All();

            References(x => x.Author)
                 .Column("Person_id")
                 .Cascade.All();

        }
    }

    public class PoetMap : SubclassMap<Poet>
    {
        public PoetMap()
        {            
            KeyColumn("Person_id");

            HasMany(x => x.Poems)                
                .Inverse()
                .Cascade.AllDeleteOrphan();
                

        }
    }
}
