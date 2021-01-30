using ComediaCore.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Mapping
{
    public class PoliticianMap : SubclassMap<Politician>
    {
        public PoliticianMap()
        {
            KeyColumn("Person_id");

            Map(x => x.Role);
            Map(x => x.Reign);
        }
    }
}
