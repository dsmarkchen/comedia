using ComediaCore.Domain;
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
    public partial class Form1 : Form
    {
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
            DBHelper.Import_line(1, "In the middle of our life");
            DBHelper.Import_line(2, "I found myself astray to a darkwood");
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            var lines = DBHelper.GetAll<Line>();
            textBox1.Text = "";
            foreach (var line in lines)
            {
                textBox1.Text += line.Text;
            }
        }
    }

    public class DBHelper {

        private static void CloseSession()
        {
            var session2 = xSessionManager.Unbind();
            if (session2 != null)
            {
                if (session2.Transaction.IsActive)
                {
                    try
                    {
                        session2.Transaction.Commit();
                    }
                    catch
                    {
                        session2.Transaction.Rollback();
                    }
                }
                session2.Close();
            }
        }
        public static bool Updatedb()
        {
            CloseSession();
            xSessionManager.UpdateSchema();
            return true;
        }
        public static bool Initdb()
        {
            CloseSession();
            xSessionManager.ExportSchema();
            return true;
        }


        public static void Import_line(int number, string text)
        {
            Line run = new Line { };
            run.Number = number;
            run.Text = text;


            ISession session1 = xSessionManager.GetCurrentSession();

            using (ITransaction transaction = session1.BeginTransaction())
            {
                session1.Save(run);

                transaction.Commit();
            }
        }

        public static IList<T> GetAll<T>()
        {
            ISession session = xSessionManager.GetCurrentSession();
            var runs = session.CreateCriteria(typeof(T)).List<T>();
            if (runs.Count > 0)
                return runs;

            return null;
        }
    }
}
