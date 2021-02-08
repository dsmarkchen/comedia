﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Domain
{
    public class Note : IEntity
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Alias
        {
            get;
            set;
        }

        public virtual string Commentary
        {
            get;
            set;
        }

        public virtual Loc Loc
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Commentary;
        }
    }

    public class Loc
    {
        public virtual string Book { get; set; }

        public virtual int Canto { get; set; }

        public virtual int Start { get; set; }
        public virtual int End   { get; set; }
        public virtual int Pos
        {
            get;
            set;
        }
    }
}