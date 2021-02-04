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

        private void initToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBHelper.Initdb();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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
            }
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
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel2.Dock = DockStyle.Fill;
            panel2.Dock = DockStyle.Fill;
            panel3.Dock = DockStyle.Fill;
            textBox1.Dock = DockStyle.Fill;
            textBox3.Dock = DockStyle.Fill;
            _formViewTable.TopLevel = false;
            var t1 = _formViewTable;
            t1.FormBorderStyle = FormBorderStyle.None;
            t1.Dock = DockStyle.Fill;
            t1.Visible = true;
            t1.AutoScroll = false;
            panel3.Controls.Add(t1);
        }
        FormViewTable _formViewTable = new FormViewTable();

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
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
                }
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var key_word = textBoxKey.Text;
            var poets = DBHelper.GetAllWithRestrictionsInsentiveLike<Poet>("Name", key_word);
            if(poets != null && poets.Count > 0)
            {   
                sb.AppendLine("Poet" + " " +  poets[0].ToString());
                if (poets[0].Poems != null &&  poets[0].Poems.Count > 0)
                {
                    sb.AppendLine("  Poem " + poets[0].Poems[0].ToString());
                }
            }

            var poems = DBHelper.GetAllWithRestrictionsInsentiveLike<Poem>("Name", key_word);
            if (poems != null && poems.Count > 0)
            {
                sb.AppendLine("Poem" + " " + poems[0].Name);
                sb.AppendLine("  Auther " + poems[0].Author.ToString());

            }

            var characters = DBHelper.GetAllWithRestrictionsLike<Character>("Name", key_word);
            if (characters != null && characters.Count > 0)
            {
                sb.AppendLine("Charactor" + " " + characters[0].ToString());
                if (characters[0].Poem != null)
                {
                    sb.AppendLine("  Poem " + characters[0].Poem.ToString());
                }


            }
            var xxx = DBHelper.GetAll<Person>();
            textBox3.Text = sb.ToString();
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
    }

}
