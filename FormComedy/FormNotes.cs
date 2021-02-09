using ComediaCore.Domain;
using ComediaCore.Helper;
using NHibernate;
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
    public partial class FormNotes : Form
    {
        private readonly DBUtil<Note> dBUtilNote = new DBUtil<Note>();
        private readonly BindingSource _bs = new BindingSource();
        public FormNotes()
        {
            InitializeComponent();
        }
        

        private void buttonRead_Click(object sender, EventArgs e)
        {
            IList< Note >  xxx = dBUtilNote.GetAll();
        

            DataTable dt = xxxhelper<Note>.ToDataTable(xxx.ToList());
            _bs.DataSource = dt;

            dt.Columns.Add("Book", typeof(System.String));
            dt.Columns.Add("Canto", typeof(System.Int32));
            dt.Columns.Add("Start", typeof(System.Int32));
            dt.Columns.Add("End", typeof(System.Int32));

            
            dataGridView1.DataSource = _bs;

            foreach(DataRow dr in dt.Rows)
            {
                dr["Book"] = ((Loc)(dr["Loc"])).Book;
                dr["Canto"] = ((Loc)(dr["Loc"])).Canto;
                dr["Start"] = ((Loc)(dr["Loc"])).Start;
                dr["End"] = ((Loc)(dr["Loc"])).End;
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.dataGridView1.Columns["Id"].Visible = false;
            this.dataGridView1.Columns["Loc"].Visible = false;
            this.dataGridView1.Columns["Term"].Visible = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var bookName = "Inferno";
            if(textBoxBook.Text != "1")
            {
                bookName = textBoxBook.Text;
            }
            Note note = new Note
            {
                Name = textBoxName.Text,
                Commentary = textBoxCommentary.Text
            };
            note.Loc = new Loc
            {
                Book = bookName,
                Canto = int.Parse(textBoxCanto.Text),
                Start = int.Parse(textBoxStart.Text),
                End = int.Parse(textBoxEnd.Text),
            };
            dBUtilNote.save(note);
            
        }

        private void FormNotes_Load(object sender, EventArgs e)
        {
            tableLayoutPanel2.Dock = DockStyle.Fill;
            dataGridView1.Dock = DockStyle.Fill;

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable )_bs.DataSource;
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Id"]);
                Console.WriteLine(dr["Name"]);
                Console.WriteLine(dr["Commentary"]);
                Console.WriteLine(dr["Book"]);
            }

        }
    }

    
}
