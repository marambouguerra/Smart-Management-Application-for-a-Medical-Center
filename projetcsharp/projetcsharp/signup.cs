using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetcsharp
{
    public partial class signup : Form

    {
        OleDbConnection conn = new OleDbConnection(
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
        );

        private void signup_Load(object sender, EventArgs e)
        {
           
        }
        public signup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();

            string req = "UPDATE utilisateurs SET motdepasse = '" + textBox2.Text + "' WHERE email = '" + textBox1.Text + "' and role = '"+stringajout.roleutilisteuteur+"';";
            OleDbCommand cmd = new OleDbCommand(req, conn);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Password created/updated successfully.");
            }
            else
            {
                MessageBox.Show("This email is not in the database.");
            }

            conn.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            login log = new login();
            this.Hide();
            log.Show();
        }

       
    }
}
