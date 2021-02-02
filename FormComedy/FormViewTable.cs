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
    public partial class FormViewTable : Form
    {
        BindingSource _bs = new BindingSource();
        public FormViewTable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;
            var item = (string)comboBox1.SelectedItem;
            switch (item) {
                case "Poet":
                    {
                        var people = DBHelper.GetAll<Poet>();
                        _bs.DataSource = people;
                    }
                    break;
                case "Person":
                    {
                        IList<Person> items;
                        if(string.IsNullOrEmpty(textBoxKey.Text))
                            items= DBHelper.GetAll<Person>();
                        else
                        {
                            items = DBHelper.GetAllWithOrRestrictionsInsentiveLike<Person>("Name", textBoxKey.Text, "FullName");
                        }
                        StringBuilder sb = new StringBuilder();
                        try
                        {
                            BindingList<EntityPerson> lst = new BindingList<EntityPerson>();
                            foreach (var itm in items)
                            {
                                sb.AppendLine(itm.ToString());
                                EntityPerson entity = new EntityPerson
                                {
                                    id = itm.Id,
                                    name = itm.Name,
                                    fullname = itm.FullName,
                                };
                                if (itm.BornPlace != null)
                                {
                                    entity.Born = itm.BornPlace.Name;
                                }
                                if (itm.DeadPlace != null)
                                {
                                    entity.Dead = itm.DeadPlace.Name;
                                }

                                if (itm.Spouse != null)
                                {
                                    foreach (var person in itm.Spouse)
                                    {
                                        entity.Spouse += person.Name;
                                    }
                                }
                                if (itm.Children != null)
                                {
                                    foreach (var person in itm.Children)
                                    {
                                        entity.Spouse += person.Name;
                                    }
                                }
                                if (itm.Parent != null)
                                {
                                    entity.Parent = itm.Parent.Name;
                                }


                                lst.Add(entity);
                            }
                            _bs.DataSource = lst;
                            dataGridView1.DataSource = _bs;
                        }
                        catch
                        {

                        }
                        textBox1.Text = sb.ToString();
                    }
                        break;
                case "Politician":
                    {
                        var people = DBHelper.GetAll<Politician>();
                        _bs.DataSource = people.ToList();
                        
                        dataGridView1.DataSource =_bs;
                    }
                    break;

                case "Place":
                    {
                        var items = DBHelper.GetAll<Place>();
                        

                        BindingList<EntityPlace> lst = new BindingList<EntityPlace>();
                        foreach (var itm in items)
                        {
                            EntityPlace entity = new EntityPlace
                            {
                                id = itm.Id,
                                name = itm.Name,

                            };
                            if (itm.People != null) {
                                foreach (var person in itm.People)
                                {
                                    entity.Born += person.Name;
                                }
                            }
                            if(itm.DeadPeople != null)
                            {
                                foreach (var person in itm.DeadPeople)
                                {
                                    entity.Dead += person.Name;
                                }
                            }

                            lst.Add(entity);
                        }
                        _bs.DataSource = lst;
                        dataGridView1.DataSource = _bs;
                    }
                    break;

                case "Poem":
                    {
                        var items = DBHelper.GetAll<Poem>();
                        _bs.DataSource = items;
                    }
                    break;
                default:
                    break;
            }
            

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormViewTable_Load(object sender, EventArgs e)
        {
            tableLayoutPanel1.Dock = DockStyle.Fill;
        }
    }


    public  class EntityPlace
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Born { get; set; }

        public string Dead { get; set; }
    };
    public class EntityPerson
    {
        public int id { get; set; }
        public string name { get; set; }

        public string fullname { get; set; }
        public string Born { get; set; }
        public string Dead { get; set; }
        public string Spouse { get; set; }

        public string Children { get; set; }

        public string Parent { get; set; }
    };
}
