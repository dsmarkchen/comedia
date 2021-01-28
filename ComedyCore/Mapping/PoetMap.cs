using ComediaCore.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Mapping
{

    public class CharacterMap : ClassMap<Character>
    {
        public CharacterMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
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

            References(x => x.Author)
                 .Column("Poet_id")
                 .Cascade.All();

            HasMany(x => x.Characters)
                .AsSet()
                .Inverse();                

        }
    }

    public class PoetMap : ClassMap<Poet>
    {
        public PoetMap()
        {
            Id(x => x.Id);

            Map(x => x.Name);
            Map(x => x.Place);

            
            HasMany(x => x.Poems)                
                .Inverse()
                .Cascade.AllDeleteOrphan();
                

        }
    }
}
