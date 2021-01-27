using ComediaCore.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
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
                                if(line.Number % 3 != 1)
                                {
                                    sb.Append("    ");
                                }
                                sb.AppendLine(line.ToString());
                                if (line.Number % 3 == 0)
                                {
                                    sb.AppendLine("");
                                }
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
    }

}
