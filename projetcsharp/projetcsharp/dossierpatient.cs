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
    public partial class dossierpatient : Form
    {
        OleDbConnection conn = new OleDbConnection(
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
);
        public dossierpatient()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            patient patient = new patient();
            patient.Show();
            this.Hide();
        }

        private void dossierpatient_Load(object sender, EventArgs e)
        {
            conn.Open();
            textBox1.Text = stringajout.cin.ToString();

            string patid = "SELECT * FROM patient WHERE cin = '" + stringajout.cin + "'";
            OleDbCommand cmdpat = new OleDbCommand(patid, conn);
            OleDbDataReader reader = cmdpat.ExecuteReader();

            if (reader.Read())
            {
                int idpat = reader.GetInt32(0);

                string reqdossier = "SELECT * FROM DossiersMedicaux WHERE [N°patient] = " + idpat;
                OleDbCommand cmddossier = new OleDbCommand(reqdossier, conn);
                OleDbDataReader reader1 = cmddossier.ExecuteReader();

                if (reader1.Read())
                {
                    // Vérifie si la colonne contient une valeur avant d'essayer de la lire
                    textBox5.Text = !reader1.IsDBNull(5) ? reader1.GetString(5) : "";
                    textBox2.Text = !reader1.IsDBNull(6) ? reader1.GetString(6) : "";
                    textBox4.Text = !reader1.IsDBNull(7) ? reader1.GetString(7) : "";
                    textBox3.Text = !reader1.IsDBNull(4) ? reader1.GetString(4) : "";
                }
            }

            conn.Close();
        }
    }
    }
