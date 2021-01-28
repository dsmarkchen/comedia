using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Domain
{
    public class Character : IEntity
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

        public virtual string Story
        {
            get;
            set;
        }

        public virtual Poem Poem
        {
            get;
            set;
        }
    }

    public class Poem : IEntity
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
        public virtual void AddCharacter(Character m)
        {
            m.Poem = this;
            Characters.Add(m);
        }


        public virtual ICollection<Character> Characters
        {
            get;
            protected set;
        }
        public virtual Poet Author
        {
            get;
            set;
        }
        public Poem()
        {
            Characters = new HashSet<Character>();
        }
    }

    public class Poet
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
        public virtual string Place
        {
            get;
            set;
        }
        public virtual void AddPoem(Poem m)
        {
            m.Author = this;
            Poems.Add(m);
        }

        public virtual IList<Poem> Poems
        {
            get;
            set;
        }

        public Poet()
        {
            Poems = new List<Poem>();
        }


    }
}
