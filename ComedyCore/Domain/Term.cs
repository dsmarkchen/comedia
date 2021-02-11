using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComediaCore.Domain
{
    public class Term : IEntity
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
        public virtual void SetMetaphorItem(string keyStr, string keyValue)
        {
            string composite_key = Name + "_" + keyStr;
            if (TermItems.ContainsKey(composite_key))
            {
                TermItems[composite_key].Meaning = keyValue;
            }
            else
            {
                TermItems.Add(composite_key, new TermItem
                {
                    Type = composite_key,
                    Meaning = keyValue
                });
            }
        }

        public virtual IDictionary<string, TermItem> TermItems { get; set; }

        [JsonIgnore]
        public virtual IList<Note> Notes { get; set; }
        public virtual void AddNote(Note note)
        {
            note.Term = this;
            Notes.Add(note);
            
        }


        public Term()
        {
            TermItems = new Dictionary<string, TermItem>();
            Notes = new List<Note>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Term.Id:     " + Id);
            sb.AppendLine("Term.Name:   " + Name);
            sb.AppendLine("Term.Alias:  " + Alias);
            if (TermItems != null)
            {
                foreach (var item in TermItems)
                {
                    sb.AppendLine("Term.TermItem:      " + item.ToString());
                }
            }
            if(Notes != null)
            {
                foreach (var item in Notes)
                {
                    sb.AppendLine("Term.Note:      " + item.Loc.ToString());
                }
            }
            return sb.ToString();

        }
    }
        
    public class TermItem
    {
        public virtual string Type { get; set;}
        public virtual string Meaning { get; set; }

        public override string ToString()
        {
            return Meaning;
        }
    }

    

   
}
