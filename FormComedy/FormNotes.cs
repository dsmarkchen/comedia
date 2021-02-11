using ComediaCore.Domain;
using ComediaCore.Helper;
using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void load(IList<Note> xxx)
        {
            DataTable dt = xxxhelper<Note>.ToDataTable(xxx.ToList());
            _bs.DataSource = dt;

            dt.Columns.Add("Book", typeof(System.String));
            dt.Columns.Add("Canto", typeof(System.Int32));
            dt.Columns.Add("Start", typeof(System.Int32));
            dt.Columns.Add("End", typeof(System.Int32));
            dt.Columns.Add("Name", typeof(System.String));

            dataGridView1.DataSource = _bs;

            foreach (DataRow dr in dt.Rows)
            {
                dr["Book"] = ((Loc)(dr["Loc"])).Book;
                dr["Canto"] = ((Loc)(dr["Loc"])).Canto;
                dr["Start"] = ((Loc)(dr["Loc"])).Start;
                dr["End"] = ((Loc)(dr["Loc"])).End;
                if (dr["Term"] != DBNull.Value)
                {
                    dr["Name"] = ((Term)dr["Term"]).Name;
                }
            }
        }
        

        private void buttonRead_Click(object sender, EventArgs e)
        {
            IList< Note >  xxx = dBUtilNote.GetAll();
            load(xxx);            
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

      

        private void buttonExport_Click(object sender, EventArgs e)
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

        private void buttonImport_Click(object sender, EventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//ComediaNotes.json";
            /*
            var notes = new NoteList { };
            using (StreamReader sr = new StreamReader(path))
            {
            
                    while (sr.Peek() >= 0)
                    {
                        var jsonString = sr.ReadLine();
                        var resnote = JsonConvert.DeserializeObject<Note>(jsonString);
                        notes.add(resnote);
                    }

                    
                }
                */
            var jsonString = File.ReadAllText(path);
            var dataTable = xxxhelper<Note>.ConvertJSONToDataTable(jsonString);
           // DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(jsonString, (typeof(DataTable)));
            dataGridView1.DataSource = dataTable;
            //dataGridView1.DataSource = notes.Notes;
        }
        
    }

    public class NoteList
    {
        public List<Note> Notes { get; set; }
        public void add(Note note)
        {
        Notes.Add(note);
        }
        public NoteList()
        {
            Notes = new List<Note>();
        }
    }
}
