using ComediaCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormComedia
{
    public partial class FormNoteAdd : Form
    {
        private readonly DBUtil<Note> _dBUtilNote = new DBUtil<Note>();
        private readonly DBUtil<Term> _dBUtilTerm = new DBUtil<Term>();

        public void SetNoteName(string name)
        {
            textBoxName.Text = name;
        }
        public void SetLocStartEnd(int book, int canto, int start, int end)
        {
            textBoxBook.Text = book.ToString();
            textBoxCanto.Text = canto.ToString();
            textBoxStart.Text = start.ToString();
            textBoxEnd.Text = end.ToString();
        }

        public FormNoteAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = textBoxName.Text;
            var bookName = "Inferno";
            if (textBoxBook.Text == "2")
            {
                bookName = "Purgatorio";
            }
            if (textBoxBook.Text == "3")
            {
                bookName = "Paridiso";
            }
            Note note = new Note
            {
                Name = name,
                Commentary = textBoxCommentary.Text
            };
            note.Loc = new Loc
            {
                Book = bookName,
                Canto = int.Parse(textBoxCanto.Text),
                Start = int.Parse(textBoxStart.Text),
                End = int.Parse(textBoxEnd.Text),
            };
            if (checkBoxTerm.Checked == true)
            {
                var terms = _dBUtilTerm.GetAllWithOrRestrictionsStringInsentiveLike("Name", name, "Alias");
                if (terms == null || terms.Count == 0)
                {
                    var term = new Term
                    {
                        Name = name
                    };
                    term.AddNote(note);
                    _dBUtilTerm.save(term);
                }
                else
                {
                    var term = terms[0];
                    term.AddNote(note);
                    _dBUtilTerm.save(term);
                }
            }
            _dBUtilNote.save(note);
        }
    }
}
