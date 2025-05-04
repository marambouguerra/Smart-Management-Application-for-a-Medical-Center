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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace projetcsharp
{
    public partial class login : Form
    {
        OleDbConnection conn = new OleDbConnection(
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
);

        public login()
        {
            InitializeComponent();

        }
        private void login_Load(object sender, EventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string req = "SELECT * FROM utilisateurs WHERE role = '" + stringajout.roleutilisteuteur + 
                "' AND email = '" + textBox1.Text + "' AND motdepasse = '" + textBox2.Text + "'";
           
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(req, conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                stringajout.cin = rd.GetString(0);
                switch (stringajout.roleutilisteuteur)
                {
                    case "administrateur":
                        administrateur admin = new administrateur();
                        admin.Show();
                        this.Hide();
                       
                        break;
                    case "secretaire":
                        secretaire sec = new secretaire();
                        sec.Show();
                        this.Hide();
                        break;
                    case "patient":
                        patient patient = new patient();
                        patient.Show();
                        this.Hide();
                        break;
                    case "medecin":
                        medecin medecin = new medecin();
                        medecin.Show();
                        this.Hide();
                        break;

                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("inncorect email or passs tray again");
                conn.Close();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            signup signup = new signup();
            this.Hide();
            signup.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            this.Hide();
            fr.Show();

        }
    }
}
