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
using System.Data.OleDb;
using Org.BouncyCastle.Crypto;
namespace projetcsharp
{
    public partial class dossier_medical : Form
    {
        OleDbConnection conn = new OleDbConnection(
      @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
  );
        public dossier_medical()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            medecin m = new medecin();
            m.Show();
            this.Hide();
        }

        private void dossier_medical_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                int numed = 0;
                string q = "SELECT  N°médecin  FROM médecins  WHERE cin ='" + stringajout.cin + "'";
                OleDbCommand com = new OleDbCommand(q, conn);

                OleDbDataReader r = com.ExecuteReader();
                while (r.Read())
                { numed = r.GetInt32(0); }
                string query1 = "SELECT  N°patient  FROM DossiersMedicaux WHERE N°medecin = " + numed;
                OleDbCommand command1 = new OleDbCommand(query1, conn);

                OleDbDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    int idpat = idpat = reader1.GetInt32(0);
                    string query = "SELECT cin FROM patient WHERE N°patient  = " + idpat;
                    OleDbCommand command = new OleDbCommand(query, conn);

                    OleDbDataReader reader = command.ExecuteReader();
                    comboBox1.Items.Clear();

                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["cin"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement : " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int idpat = 0;
            try
            {
                conn.Open();

                // 1. Récupérer l'ID du patient à partir de la CIN
                string query1 = "SELECT [N°patient] FROM patient WHERE cin = '" + comboBox1.Text + "'";
                OleDbCommand cmd1 = new OleDbCommand(query1, conn);
                OleDbDataReader reader1 = cmd1.ExecuteReader();

                if (reader1.Read())
                {
                    idpat = reader1.GetInt32(0);
                }
                reader1.Close(); // On ferme le lecteur précédent

                string reqSelect = "SELECT [notes], [Groupesanguin], [Maladieschroniques], [Traitementsréguliers] " +
                                   "FROM DossiersMedicaux WHERE [N°patient] = " + idpat;

                OleDbCommand cmd = new OleDbCommand(reqSelect, conn);
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    textBox3.Text = reader["notes"].ToString();
                    textBox1.Text = reader["Groupesanguin"].ToString();
                    textBox2.Text = reader["Maladieschroniques"].ToString();
                    textBox4.Text = reader["Traitementsréguliers"].ToString();

                    MessageBox.Show("Données récupérées avec succès !");
                }
                else
                {
                    MessageBox.Show("Aucun dossier trouvé pour cet ID de patient.");
                }

                reader.Close();
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


        private void button1_Click(object sender, EventArgs e)
        {

            int idpat = 0;

            try
            {
                conn.Open();

                // 1. Récupérer l'ID du patient à partir de la CIN
                string query1 = "SELECT [N°patient] FROM patient WHERE cin = '" + comboBox1.SelectedItem + "'";
                OleDbCommand cmd1 = new OleDbCommand(query1, conn);
                OleDbDataReader reader1 = cmd1.ExecuteReader();

                if (reader1.Read())
                {
                    idpat = reader1.GetInt32(0);
                   
                }
                reader1.Close();
                string reqUpdate = "UPDATE DossiersMedicaux SET " +
                      "notes = '" + textBox3.Text + "', " +
                      "Groupesanguin = '" + textBox1.Text + "', " +
                      "Maladieschroniques = '" + textBox2.Text + "', " +
                      "Traitementsréguliers = '" + textBox4.Text + "' " +
                      "WHERE [N°patient] = " + idpat;


                OleDbCommand cmd = new OleDbCommand(reqUpdate, conn);


                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Mise à jour réussie !");
                }
                else
                {
                    MessageBox.Show("Aucune ligne mise à jour. Vérifiez l'ID du patient.");
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
