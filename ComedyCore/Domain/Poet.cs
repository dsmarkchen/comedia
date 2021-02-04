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
            sb.Append(base.ToString());

            if (Story != null)
            {
                sb.AppendLine("Character.Story: " + Story.ToString());
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
            if(Author != null) {             
                  sb.AppendLine("Poem.Author:   " + Author.Name.ToString());                
            }
            foreach (var character in Characters)
            {
                sb.AppendLine("Poem.Character: " + character.Name);
            }


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
            sb.Append(base.ToString());

            foreach (var poem in Poems)
            {
                sb.AppendLine("Poet.Poem: " + poem.Name);
            }
            return sb.ToString();
        }
    }


    
}
