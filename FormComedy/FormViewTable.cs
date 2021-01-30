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
                        var items = DBHelper.GetAll<Person>();


                        BindingList<EntityPerson> lst = new BindingList<EntityPerson>();
                        foreach (var itm in items)
                        {
                            EntityPerson entity = new EntityPerson
                            {
                                id = itm.Id,
                                name = itm.Name,
                            };
                            if (itm.Place != null)
                            {
                                entity.Born = itm.Place.Name;
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

                            lst.Add(entity);
                        }
                        _bs.DataSource = lst;
                        dataGridView1.DataSource = _bs;
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
                                    entity.People += person.Name;
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
    }


    public  class EntityPlace
    {
        public int id { get; set; }
        public string name { get; set; }
        public string People { get; set; }
    };
    public class EntityPerson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Born { get; set; }
        public string Dead { get; set; }
        public string Spouse { get; set; }
    };
}
