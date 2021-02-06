using ComediaCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormComedia
{
    public partial class FormViewTable : Form
    {
        public string SearchKey
        {
            get
            {
                return textBoxKey.Text;
            }
            set
            {
                textBoxKey.Text = value;
            }
        }
        

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
            string typeString;
            if (string.IsNullOrEmpty(item))
                typeString = "Term";
            else
                typeString = item;

            StringBuilder sb = new StringBuilder();

            foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(currentassembly.ToString());
                if (currentassembly.FullName.Contains("ComediaCore"))
                {
                    Type typeArgument = currentassembly.GetType(string.Format("ComediaCore.Domain.{0}", typeString));
                    Type template = typeof(DBUtil<>);
                    Type genericType = template.MakeGenericType(typeArgument);
                    object instance = Activator.CreateInstance(genericType);
                    IEnumerable<object> items;
                    if (string.IsNullOrEmpty(textBoxKey.Text))
                    {
                        MethodInfo method = genericType.GetMethod("GetAll");
                        items = (IEnumerable<object>)method.Invoke(instance, new object[0]);

                    }
                    else
                    {
                        MethodInfo method2 = genericType.GetMethod("GetAllWithOrRestrictionsStringInsentiveLike");
                        var params_ = new object[3]
                        {
                                        "Name",
                                        textBoxKey.Text,
                                        "Alias"
                        };
                        items = (IEnumerable<object>)method2.Invoke(instance, params_);
                    }


                    if (items != null)
                    {
                        foreach (var itm in items)
                        {
                            sb.AppendLine(itm.ToString());
                        }
                    }


                    textBox1.Text = sb.ToString();
                    break;
                }


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

    public class GenericClass<T>
    {
        public static object GetInstance(Type type)
        {
            var genericType = typeof(GenericClass<>).MakeGenericType(type);
            return Activator.CreateInstance(genericType);
        }

        public static GenericClass<T> GetInstance<T>()
            where T : class
        {
            return new GenericClass<T>();
        }
    }
    public class DbHelperFactory {
        private  readonly Dictionary<string, Func<object>>  factory = new Dictionary<string, Func<object>>();

        public DbHelperFactory()
        {
            factory.Add("Term", () => new List<Term>());
            factory.Add("Person", () => new List<Person>());
            factory.Add("Place", () => new List<Place>());
        }
            
    }

    
}
