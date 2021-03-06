﻿using ComediaCore.Domain;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Mapping
{  
    public class NoteMap : ClassMap<Note>
    {
        public NoteMap()
        {
            Id(x => x.Id);
            
            Component(x => x.Loc, m =>
            {
                m.Map(x => x.Book);
                m.Map(x => x.Canto);
                m.Map(x => x.Start);
                m.Map(X => X.End);
                m.Map(x => x.Pos);
            });

            Map(x => x.Name);
            Map(x => x.Commentary);
                       
            References(x => x.Term, "TermId")
                .Cascade.All();
        }
    }
}
