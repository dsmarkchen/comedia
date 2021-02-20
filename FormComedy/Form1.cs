using ComediaCore.Helper;
using ComediaCore.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NHibernate.Criterion;
using System.Drawing;
using System.Runtime.Serialization;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FormComedia
{
    public partial class Form1 : Form
    {
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        public Form1()
        {
            InitializeComponent();            
        }
        public void SetResult(string s)
        {
            toolStripStatusLabelResult.Text = s;
        }
        private void initToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = DBHelper.Initdb();
            SetResult("Initdb " + ((res == true) ? "ok" : "fail"));
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool res = true;
            string file = Path.Combine(AssemblyDirectory, "inferno.txt");
            string purgatorio = Path.Combine(AssemblyDirectory, "purgatorio.txt");
            string paradiso = Path.Combine(AssemblyDirectory, "paradiso.txt");

            try
            {
                var text0 = File.ReadAllText(file);
                DBHelper.Import_Book("Inferno", 1, text0);
                var text1 = File.ReadAllText(purgatorio);
                DBHelper.Import_Book("Purgatorio", 2, text1);

                var text2 = File.ReadAllText(paradiso);
                DBHelper.Import_Book("Paradiso", 3, text2);
            }
            catch (IOException)
            {
                res = false;
            }

            SetResult("import books: " + ((res == true) ? "ok" : "fail"));
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            int bookNumber = int.Parse(textBoxBook.Text);
            int cantoNumber = int.Parse(textBoxCanto.Text);
            int start = int.Parse(textBoxStart.Text);
            int end = int.Parse(textBoxEnd.Text);
            var books = DBHelper.GetAll<Book>();
            if (books == null) return;

            textBox1.Text = "";
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                if (book.Number != bookNumber) continue;

                foreach (var canto in book.Cantos)
                {
                    if(canto.Number == cantoNumber)
                    {
                        foreach(var line in canto.Lines)
                        {   
                            if(line.Number >= start && line.Number <= end)
                            {
                                sb.Append(ComediaHelper.CantoReformat(line.Text, line.Number));
                            }
                        }
                    }
                    
                }
            }
            textBox1.Text = sb.ToString();
       
            var bookName = "Inferno";
            if(bookNumber == 2)
            {
                bookName = "Purgatorio";
            }
            if (bookNumber == 3)
            {
                bookName = "Paradiso";
            }
            loadNotes(bookName, cantoNumber, start, end);
        }

       

        private void buttonNext_Click(object sender, EventArgs e)
        {
            int start0 = int.Parse(textBoxStart.Text);
            int end0 = int.Parse(textBoxEnd.Text);
            int start = end0 + 1;
            int len = end0 - start0 + 1;
            int end = end0 + len;
            textBoxStart.Text = start.ToString();
            textBoxEnd.Text = end.ToString();

            buttonGet.PerformClick();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            int start0 = int.Parse(textBoxStart.Text);
            int end0 = int.Parse(textBoxEnd.Text);
            
            int len = end0 - start0 + 1;
            int end = start0 - 1;
            int start = start0 - len;

            textBoxStart.Text = start.ToString();
            textBoxEnd.Text = end.ToString();

            buttonGet.PerformClick();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            Icon = Icon.FromHandle(Properties.Resources.book_icon.GetHicon());
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanelNotesContainer.Dock = DockStyle.Fill;
            flowLayoutPanelNotes.Dock = DockStyle.Fill;
            textBoxNoteInfo.Dock = DockStyle.Fill;

            panel2.Dock = DockStyle.Fill;
            panel3.Dock = DockStyle.Fill;
            panel4.Dock = DockStyle.Fill;
            textBox1.Dock = DockStyle.Fill;
            textBox3.Dock = DockStyle.Fill;
            _formViewTable.TopLevel = false;
            var t1 = _formViewTable;
            t1.FormBorderStyle = FormBorderStyle.None;
            t1.Dock = DockStyle.Fill;
            t1.Visible = true;
            t1.AutoScroll = false;
            formNoteAdd.TopLevel = false;
            formNoteAdd.FormBorderStyle = FormBorderStyle.None;
            formNoteAdd.Dock = DockStyle.Fill;
            formNoteAdd.Visible = true;
            formNoteAdd.AutoScroll = false;
            panel3.Controls.Add(formNoteAdd);

            /*_formNotes.TopLevel = false;
            _formNotes.FormBorderStyle = FormBorderStyle.None;
            _formNotes.Dock = DockStyle.Fill;
            _formNotes.Visible = true;
            _formNotes.AutoScroll = false;
            panel4.Controls.Add(_formNotes);
            */
           // loadNotes("Inferno", 1, 1, 18);
        }
        FormViewTable _formViewTable = new FormViewTable();
        FormNotes _formNotes = new FormNotes();
        FormNoteAdd formNoteAdd = new FormNoteAdd();

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool res = true;
            try
            {
                Place place_fl = new Place { Name = "Florence" };
                Place place_rome = new Place { Name = "Rome" };
                Place place_nola = new Place { Name = "Nola" };
                Politician politician_augustus = new Politician
                {
                    Name = "Augustus"
                };
                Person claudia = new Person
                {
                    Name = "Claudia"
                };
                Person scribonia = new Person
                {
                    Name = "Scribonia"
                };
                Person livia = new Person
                {
                    Name = "Livia"
                };
                politician_augustus.AddSpouse(claudia);
                politician_augustus.AddSpouse(scribonia);
                politician_augustus.AddSpouse(livia);

                place_rome.AddPerson(politician_augustus);
                place_nola.AddPersonDead(politician_augustus);




                DBHelper.save<Politician>(politician_augustus);
                DBHelper.save<Person>(claudia);
                DBHelper.save<Person>(livia);
                DBHelper.save<Person>(scribonia);

                ComediaBuilder.build_places();
                ComediaBuilder.build_characters_bible();

                ComediaBuilder.build_poets_elite_six();
                ComediaBuilder.build_characters_aenaes();
                ComediaBuilder.build_characters_electra();
                ComediaBuilder.build_characters_minos();
                ComediaBuilder.build_characters_erinyes(); // three furies
                ComediaBuilder.build_politician();
                ComediaBuilder.build_characters_comedy();

                //ComediaBuilder.build_terms();
                //ComediaBuilder.build_metaphors_inferno();
                //ComediaBuilder.build_notes();
            }
            catch(Exception ex)
            {
                res = false;
            }
            SetResult("build characters: " + ((res == true) ? "ok" : "fail"));
        }

        private void textBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (textBox1.SelectedText.Length > 0)
                {
                    var text = textBox1.SelectedText;
                    text = ComediaHelper.TrimUnecessaryCharacters(text);
                    textBoxKey.Text = text;
                    if(_formViewTable.Visible)
                    {
                        _formViewTable.SearchKey = text;
                    }
                    if(formNoteAdd.Visible)
                    {
                        var booknum = int.Parse(textBoxBook.Text);
                        var cantonum = int.Parse(textBoxCanto.Text);

                        var linenum = textBox1.GetLineFromCharIndex(textBox1.SelectionStart);
                        
                        int modnum = linenum / 4;
                        var start = int.Parse(textBoxStart.Text);
                        var pos = start + linenum - modnum;
                        formNoteAdd.SetNoteName(text);
                        formNoteAdd.SetLocStartEnd(booknum, cantonum, pos, pos);
                        formNoteAdd.ResetNote();
                    }
                }
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var key_word = textBoxKey.Text;
            if (string.IsNullOrEmpty(key_word))
                return;

            DBUtil<Note> dBUtil = new DBUtil<Note>();
            var notes = dBUtil.GetAllWithOrRestrictionsStringInsentiveLike("Name", key_word);
            if (notes.Count > 0)
            {
                foreach (var note in notes)
                {
                    sb.AppendLine(note.ToString());
                }

            }
            DBUtil<Person> dBUtilPerson = new DBUtil<Person>();
            var people = dBUtilPerson.GetAllWithOrRestrictionsStringInsentiveLike("Name", key_word);
            if (people.Count > 0)
            {
                foreach (var person in people)
                {
                    sb.AppendLine(person.ToString());
                }
            }

            DBUtil<Place> dBUtilPlace = new DBUtil<Place>();
            var places = dBUtilPlace.GetAllWithOrRestrictionsStringInsentiveLike("Name", key_word);
            if (places.Count > 0)
            {
                foreach (var place in places)
                {
                    sb.AppendLine(place.ToString());
                }
            }
            textBox3.Text = sb.ToString();

                    
        }
        void btn_click(object sender, EventArgs e)
        {
            Term term = (Term)(((Button)sender).Tag);
            if(term != null)
                textBoxNoteInfo.Text = term.ToString();
                
         }
        public void loadNotes(string book, int canto, int begin, int end)
        {
            DBUtil<Note> dBUtil = new DBUtil<Note>();
            try
            {
                flowLayoutPanelNotes.Controls.Clear();
                textBoxNoteInfo.Text = "";


                var notes = dBUtil.GetAllWithRestrictionsLoc("Loc.Book", "Loc.Canto", "Loc.Start", "Loc.End", 
                    book, canto, begin, end);
                if (notes == null) return;

                
                foreach (var note in notes)
                {
                    Button btn = new Button
                    {
                        
                    };
                    btn.Click += new System.EventHandler(this.btn_click);
                    btn.Text = note.Name;
                    if(note.Term != null)
                    {
                        if(!string.IsNullOrEmpty(note.Term.Name))
                        {
                            if(string.IsNullOrEmpty(btn.Text))
                                btn.Text = note.Term.Name;
                            btn.Tag = note.Term;
                            if(note.Commentary != null)
                                btn.SetToolTip(note.Commentary);
                        }
                    }

                    flowLayoutPanelNotes.Controls.Add(btn);

                }
            }
            catch
            {

            }
        }
        private void summeryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("## Places");
            var places = DBHelper.GetAll<Place>();
            foreach (var place in places)
            {
                sb.AppendLine(place.ToString());
            }


            sb.AppendLine("## Person Table");
            var people = DBHelper.GetAll<Person>();
            foreach(var person in people)
            {
                sb.AppendLine(person.ToString());
            }

            sb.AppendLine("## Poets");
            var poets = DBHelper.GetAll<Poet>();
            foreach (var poet in poets)
            {
                sb.AppendLine(poet.ToString());
            }

            sb.AppendLine("## Characters");
            var characters = DBHelper.GetAll<Character>();
            foreach (var character in characters)
            {
                sb.AppendLine(character.ToString());
            }


            sb.AppendLine("## Poems");
            var poems = DBHelper.GetAll<Poem>();
            foreach (var poem in poems)
            {
                sb.AppendLine(poem.ToString());
            }
            MessageBox.Show(sb.ToString());
        }

        private void tableViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormViewTable x = new FormViewTable();
            x.Icon = this.Icon;
            x.ShowDialog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//ComediaNotes.json";

            ISession session = SQLiteSessionManager.GetCurrentSession();
            {

                using (session.BeginTransaction())
                {
                    var resnotes = session.CreateCriteria(typeof(Note))
                            .List<Note>();

                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        foreach (var note in resnotes)
                        {
                            string x = JsonConvert.SerializeObject(note, new JsonSerializerSettings()
                            {
                                ContractResolver = new NHibernateContractResolver()
                            });
                            
                            sw.WriteLine(x);
                        }

                    }

                }
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//ComediaNotes.json";
            string input_string = File.ReadAllText(path);

            JsonSerializer serializer = new JsonSerializer();
            
            using (StreamReader sr = new StreamReader(path))
            {
                ISession session = SQLiteSessionManager.GetCurrentSession();
                using (ITransaction transaction = session.BeginTransaction())
                {

                    while (sr.Peek() >= 0)
                    {
                        var jsonString = sr.ReadLine();
                        var resnote = JsonConvert.DeserializeObject<Note>(jsonString);
                        if(resnote.Term != null)
                        {
                            Term term = DBHelper.GetAllWithRestrictionsEq<Term>("Name", resnote.Term.Name)[0];
                            term.AddNote(resnote);
                            DBUtil<Term> dBUtilTerm = new DBUtil<Term>();
                            dBUtilTerm.save(term);

                        }

                        session.Merge(resnote);
                    }

                    transaction.Commit();
                }
            }
        }

        private void managementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNotes formNotes = new FormNotes();
            formNotes.Show();
        }

        private void exportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool res = true;

            // Export Notes            
            ISession session = SQLiteSessionManager.GetCurrentSession();
            using (session.BeginTransaction())
            {
                var resnotes = session.CreateCriteria(typeof(Note))
                        .List<Note>();
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//ComediaNotes.json";
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (var note in resnotes)
                    {
                        string x = JsonConvert.SerializeObject(note, new JsonSerializerSettings()
                        {
                            ContractResolver = new NHibernateContractResolver()
                        });

                        sw.WriteLine(x);
                    }

                }

            }

            // Export Terms
            session = SQLiteSessionManager.GetCurrentSession();
            using (session.BeginTransaction())
            {
                var resterms = session.CreateCriteria(typeof(Term))
                        .List<Term>();
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//ComediaTerms.json";
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (var term in resterms)
                    {
                        string x = JsonConvert.SerializeObject(term, new JsonSerializerSettings()
                        {
                            ContractResolver = new NHibernateContractResolver()
                        });

                        sw.WriteLine(x);
                    }

                }

            }
            SetResult("export: " + ((res == true) ? "ok" : "fail"));
        }

        private void importToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Import term first
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//ComediaTerms.json";

                using (StreamReader sr = new StreamReader(path))
                {
                    ISession session = SQLiteSessionManager.GetCurrentSession();

                    while (sr.Peek() >= 0)
                    {
                        var jsonString = sr.ReadLine();
                        var resnote = JsonConvert.DeserializeObject<Term>(jsonString);
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            resnote.Id = 0;
                            


                            session.SaveOrUpdate(resnote);
                            transaction.Commit();

                        }
                    }

                    
                }
            }
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//ComediaNotes.json";

                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        var jsonString = sr.ReadLine();
                        var resnote = JsonConvert.DeserializeObject<Note>(jsonString);
                        resnote.Id = 0;
                        ISession session = SQLiteSessionManager.GetCurrentSession();
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            if (resnote.Term != null)
                            {
                                Term term = DBHelper.GetAllWithRestrictionsEq<Term>("Name", resnote.Term.Name)[0];
                                term.AddNote(resnote);
                                session.SaveOrUpdate(term);
                            }


                            session.SaveOrUpdate(resnote);
                            transaction.Commit();
                        }


                    }
                }
            }
        }
    }
    public class NHibernateContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            if (typeof(NHibernate.Proxy.INHibernateProxy).IsAssignableFrom(objectType))
                return base.CreateContract(objectType.BaseType);
            else
                return base.CreateContract(objectType);
        }
    }
    public static class comediahelperUtilExtention
    {
        public static void SetToolTip(this Control control, string txt)
        {
            new ToolTip().SetToolTip(control, txt);
        }
    }

}
