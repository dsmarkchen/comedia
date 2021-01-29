using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Domain
{
    public class Character : Person
    {
       

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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Character.Id:     " + Id);
            sb.AppendLine("Character.Name: " + Name);
            if (Place != null && Place.Name != null)
            {
                sb.AppendLine("Character.Place: " + Place.Name);
            }
            if (Story != null)
            {
                sb.AppendLine(Story.ToString());
            }
            return sb.ToString();
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Poem.Id:     " + Id);

            sb.AppendLine("Poem.Name:   " + Name.ToString());
            return sb.ToString();
        }
    }

    public class Poet : Person
    {
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Poet.Id:     " + Id);
            sb.AppendLine("Poet.Name: " + Name);
            if (Place != null && Place.Name != null)
            {
                sb.AppendLine("Poet.Place: " + Place.Name);
            }
            foreach (var poem in Poems)
            {
                sb.AppendLine(poem.ToString());
            }
            return sb.ToString();
        }
    }
}
