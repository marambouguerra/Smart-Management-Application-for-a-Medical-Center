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
    public partial class dossieradministratif : Form
    {
        OleDbConnection conn = new OleDbConnection(
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
);
        public dossieradministratif()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            secretaire s = new secretaire();
            s.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                conn.Open();

                int idpat = 0;

                // 1. Récupérer l'ID du patient à partir de la CIN
                string query = "SELECT [N°patient] FROM patient WHERE cin = '" + comboBox1.Text + "'";
                OleDbCommand command = new OleDbCommand(query, conn);
                OleDbDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    idpat = reader.GetInt32(0);
                }
                reader.Close();

                // 2. Mise à jour dans dossieradministratif via idpatient
                string reqUpdate = "UPDATE dossieradministratif SET " +
                    "NumSecuriteSociale = '" + textBox1.Text.Replace("'", "''") + "', " +
                    "NomMutuelle = '" + textBox2.Text.Replace("'", "''") + "', " +
                    "TelUrgence = '" + textBox3.Text.Replace("'", "''") + "' " +
                    "WHERE idpatient = " + idpat;

                OleDbCommand cmd = new OleDbCommand(reqUpdate, conn);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Enregistrement réussi !");
                }
                else
                {
                    MessageBox.Show("Aucune ligne mise à jour. Vérifiez l’existence du dossier.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void dossieradministratif_Load(object sender, EventArgs e)
        {




            conn.Open();
            string query = "SELECT CIN FROM utilisateurs WHERE role = 'patient'";
            OleDbCommand command = new OleDbCommand(query, conn);

            OleDbDataReader reader = command.ExecuteReader();
            comboBox1.Items.Clear();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader["CIN"].ToString());
            }

            conn.Close();        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {conn.Open();
            try { 
            string req = "SELECT * FROM utilisateurs WHERE CIN = '" + comboBox1.Text + "'";
            OleDbCommand cmd1 = new OleDbCommand(req, conn);
            OleDbDataReader reader1 = cmd1.ExecuteReader();

            if (reader1.Read())
            {
                textBox4.Text = reader1[1].ToString() + " " + reader1[2].ToString();
            }
            reader1.Close();

            string req1 = "SELECT * FROM patient WHERE cin = '" + comboBox1.Text + "'";
            OleDbCommand cmd = new OleDbCommand(req1, conn);
            OleDbDataReader reade1 = cmd.ExecuteReader();

            if (reade1.Read())
            {
                textBox5.Text = reade1.GetDateTime(2).ToString("dd/MM/yyyy");
                textBox7.Text = reade1.GetString(3); 
            }

            reade1.Close();
        }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        }
    }

    