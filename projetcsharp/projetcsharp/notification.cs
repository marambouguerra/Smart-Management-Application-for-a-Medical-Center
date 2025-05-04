using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace projetcsharp
{
    public partial class notification : Form
    {
        OleDbConnection conn = new OleDbConnection(
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
);
        public notification()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void notification_Load(object sender, EventArgs e)
        {
            conn.Open();
            string patid = "SELECT * FROM patient WHERE cin = '" + stringajout.cin + "'";
            OleDbCommand cmdpat = new OleDbCommand(patid, conn);
            OleDbDataReader reader = cmdpat.ExecuteReader();

            if (reader.Read())
            {
                int idpat = reader.GetInt32(0);    

                DataTable dt = new DataTable();
                dt.Columns.Add("message", typeof(string));
                dt.Columns.Add("date_envoi", typeof(DateTime));
                dt.Columns.Add("type", typeof(string));

                string reqnot = "SELECT * FROM notifications WHERE numpatient = " + idpat;
                OleDbCommand cmdnot = new OleDbCommand(reqnot, conn);
                OleDbDataReader reader2 = cmdnot.ExecuteReader();

                int rowCount = 0;
                while (reader2.Read())
                {
                    string message = reader2.GetString(2);
                    DateTime date = reader2.GetDateTime(3);
                    string type = reader2.GetString(4);

                    dt.Rows.Add(message, date, type);
                    rowCount++;
                }

                reader2.Close();

                if (rowCount == 0)
                {
                    MessageBox.Show("Aucune notifaction recus  pour ce moment.");
                }

                dataGridView1.DataSource = dt;
                dataGridView1.Refresh();
            }
            else
            {
                MessageBox.Show("patient introuvable !");
            }

            reader.Close();
        }
    
        private void button4_Click(object sender, EventArgs e)
        {
            patient patient = new patient();
            patient.Show();
            this.Hide();
        }
    }
}
