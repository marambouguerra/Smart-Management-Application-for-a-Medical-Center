using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetcsharp
{
    public partial class historiquecons : Form
    {
        OleDbConnection conn = new OleDbConnection(
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
);
        public historiquecons()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            choixconc c = new choixconc();
            c.Show();
            this.Hide();
        }

        private void historiquecons_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string req2 = "SELECT * FROM médecins WHERE cin = '" + stringajout.cin + "'";
                OleDbCommand command2 = new OleDbCommand(req2, conn);
                OleDbDataReader reader2 = command2.ExecuteReader();

                bool trouve = false; // Ajout d'un booléen pour vérifier si on a trouvé un médecin

                while (reader2.Read())
                {
                    trouve = true; // Médecin trouvé
                    int nummed = reader2.GetInt32(0);

                    string statut = "deja";

                    string query = "SELECT DISTINCT [cin] FROM patient WHERE [N°patient] IN " +
                                   "(SELECT NumPatient FROM consultation WHERE [N°médecin] = " + nummed + " AND statut='" + statut + "')";

                    OleDbCommand command = new OleDbCommand(query, conn);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string cinPatient = reader.GetString(0);
                        comboBox1.Items.Add(cinPatient);
                    }

                    reader.Close();
                }

                reader2.Close();

                if (!trouve)
                {
                    MessageBox.Show("Médecin non trouvé !");
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





        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                int idPatient;
                int idMedecin = 0; 
                string getIdMedecinQuery = "SELECT [N°médecin] FROM médecins WHERE cin = ?";
                OleDbCommand getMedecinCmd = new OleDbCommand(getIdMedecinQuery, conn);
                getMedecinCmd.Parameters.AddWithValue("?", stringajout.cin);
                OleDbDataReader rMed = getMedecinCmd.ExecuteReader();
                if (rMed.Read())
                {
                    idMedecin = Convert.ToInt32(rMed["N°médecin"]);
                }
                rMed.Close();

                string getIdPatientQuery = "SELECT [N°patient] FROM patient WHERE cin = ?";
                OleDbCommand getIdCmd = new OleDbCommand(getIdPatientQuery, conn);
                getIdCmd.Parameters.AddWithValue("?", comboBox1.Text);

                OleDbDataReader rdddd = getIdCmd.ExecuteReader();

                if (rdddd.Read())
                {
                    idPatient = Convert.ToInt32(rdddd["N°patient"]);
                    rdddd.Close();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("N°consultation", typeof(int));
                    dt.Columns.Add("N°rendezvous", typeof(int));
                    dt.Columns.Add("N°médecin", typeof(int));
                    dt.Columns.Add("NumPatient", typeof(int));
                    dt.Columns.Add("date_consultation", typeof(DateTime));
                    dt.Columns.Add("diagnostic", typeof(string));
                    dt.Columns.Add("traitement", typeof(string));
                    dt.Columns.Add("saisons", typeof(string));
                    dt.Columns.Add("payee", typeof(string));
                    dt.Columns.Add("Ordonnance", typeof(string));

                    string rend = "SELECT [N°rendezvous] FROM RendezVous WHERE [N°patient] = ?";
                    OleDbCommand getRendezvousCmd = new OleDbCommand(rend, conn);
                    getRendezvousCmd.Parameters.AddWithValue("?", idPatient);
                    OleDbDataReader rdddd1 = getRendezvousCmd.ExecuteReader();

                    while (rdddd1.Read())
                    {
                        int renv = Convert.ToInt32(rdddd1["N°rendezvous"]);

                        string req = "SELECT * FROM consultation WHERE [N°rendezvous] = ? AND [N°médecin] = ?";
                        OleDbCommand cmd = new OleDbCommand(req, conn);
                        cmd.Parameters.AddWithValue("?", renv);
                        cmd.Parameters.AddWithValue("?", idMedecin); 

                        OleDbDataReader da = cmd.ExecuteReader();

                        while (da.Read())
                        {
                            int idcons = Convert.ToInt32(da["N°consultation"]);

                            if (idcons == 0)
                            {
                                MessageBox.Show("⚠️ Avertissement : idcons = 0 pour le rendez-vous n° " + renv);
                            }

                            string ordonnance = "Aucune ordonnance";
                            string req2 = "SELECT fichier FROM DocumentsMedicaux WHERE nconsultation = ? AND type_document = 'Ordonnance Médicale'";
                            OleDbCommand cmd2 = new OleDbCommand(req2, conn);
                            cmd2.Parameters.AddWithValue("?", idcons);

                            OleDbDataReader dah = cmd2.ExecuteReader();
                            if (dah.Read())
                            {
                                ordonnance = dah["fichier"].ToString();
                            }
                            dah.Close();

                            dt.Rows.Add(
                                idcons,
                                Convert.ToInt32(da["N°rendezvous"]),
                                Convert.ToInt32(da["N°médecin"]),
                                Convert.ToInt32(da["NumPatient"]),
                                Convert.ToDateTime(da["date_consultation"]),
                                da["diagnostic"].ToString(),
                                da["traitement"].ToString(),
                                da["saisons"].ToString(),
                                da["payee"].ToString(),
                                ordonnance
                            );
                        }
                        da.Close();
                    }

                    rdddd1.Close();
                    dataGridView1.DataSource = dt;
                    dataGridView1.Refresh();
                }
                else
                {
                    MessageBox.Show("Aucun patient trouvé avec ce CIN.");
                }

                rdddd.Close();
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


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Récupérer le chemin du fichier depuis la cellule "Ordonnance"
                string chemin = dataGridView1.Rows[e.RowIndex].Cells["Ordonnance"].Value?.ToString();

                if (!string.IsNullOrEmpty(chemin) && File.Exists(chemin))
                {
                    try
                    {
                        var process = new System.Diagnostics.Process();
                        process.StartInfo = new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = chemin,         // Le chemin du fichier PDF
                            UseShellExecute = true     // Utilise l'application par défaut
                        };
                        process.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de l'ouverture du fichier : " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Le fichier PDF n’existe pas !");
                }
            }
        }
    }
}