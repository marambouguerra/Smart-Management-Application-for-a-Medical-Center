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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace projetcsharp
{
    public partial class document_medicaux : Form
    {
        OleDbConnection conn = new OleDbConnection(
      @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
  ); string fichierPDF = "";

        public document_medicaux()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichiers PDF (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fichierPDF = openFileDialog.FileName;
                MessageBox.Show("Fichier sélectionné : " + fichierPDF);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            medecin m = new medecin();
            m.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un patient.");
                return;
            }

            if (string.IsNullOrEmpty(fichierPDF))
            {
                MessageBox.Show("Veuillez d'abord sélectionner un fichier PDF.");
                return;
            }

            try
            {
                conn.Open();
                int idcons = 0;
                int idDossier = 0;
                int idpat = 0;
                int idMedecin = 0;
                // Rechercher l'ID du patient par CIN
                string cin = comboBox1.SelectedItem.ToString();
                string query1 = "SELECT [N°patient] FROM patient WHERE cin  = '"+cin+"'";
                OleDbCommand cmd1 = new OleDbCommand(query1, conn);
                OleDbDataReader reader1 = cmd1.ExecuteReader();

                if (reader1.Read())
                {
                    idpat = reader1.GetInt32(0);
                }
                else
                {
                    MessageBox.Show("Patient introuvable.");
                    return;
                }
                reader1.Close();

                string req2 = "SELECT [N°médecin] FROM médecins WHERE cin = ?";
                OleDbCommand cmd4 = new OleDbCommand(req2, conn);
                cmd4.Parameters.AddWithValue("?", stringajout.cin);
                OleDbDataReader rd2 = cmd4.ExecuteReader();
                if (rd2.Read())
                {
                    idMedecin = rd2.GetInt32(0);
                }
                rd2.Close();

                string req3 = "SELECT * FROM consultation WHERE N°médecin = " + idMedecin + "and NumPatient ="+ idpat;
                OleDbCommand cmd3 = new OleDbCommand(req3, conn);
                
                OleDbDataReader rd3 = cmd3.ExecuteReader();
                if (rd3.Read())
                {
                    idcons = rd3.GetInt32(0);
                }


                // Rechercher le numéro de dossier médical
                string query2 = "SELECT [N°dossiersmedicaux] FROM DossiersMedicaux WHERE N°patient = "+idpat;
                OleDbCommand cmd2 = new OleDbCommand(query2, conn);
                OleDbDataReader reader2 = cmd2.ExecuteReader();

                if (reader2.Read())
                {
                    idDossier = reader2.GetInt32(0);
                }
                else
                {
                    MessageBox.Show("Dossier médical introuvable.");
                    return;
                }
                reader2.Close();

                // Déterminer le type de document
                string typeDocument = "";
                if (radioButton1.Checked) typeDocument = "Ordonnance Médicale";
                else if (radioButton2.Checked) typeDocument = "Certificat Médical";
                else if (radioButton3.Checked) typeDocument = "Facture Médicale";
                else if (radioButton4.Checked) typeDocument = "Résultats d’analyses biologiques";
                else if (radioButton5.Checked) typeDocument = "Résultats d’imagerie médicale";

                if (typeDocument == "")
                {
                    MessageBox.Show("Veuillez sélectionner un type de document.");
                    return;
                }

                // Échapper les apostrophes pour Access
                string typeDocSafe = typeDocument.Replace("'", "''");
                string fichierSafe = fichierPDF.Replace("'", "''");

                // Requête d'insertion sans ?
                string insertQuery = $"INSERT INTO DocumentsMedicaux ([N°dossiersmedicaux], type_document,nconsultation, fichier) " +
                                     $"VALUES ({idDossier}, '{typeDocSafe}', {idcons}, '{fichierSafe}')";

                OleDbCommand cmdInsert = new OleDbCommand(insertQuery, conn);
                int result = cmdInsert.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Document médical enregistré avec succès.");
                    fichierPDF = ""; // reset
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'enregistrement.");
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
            // Optionnel : tu peux afficher automatiquement les documents du patient sélectionné ici
        }

        private void document_medicaux_Load(object sender, EventArgs e)
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
                        string cinPatient = reader.GetString(0); // CIN unique du patient
                        comboBox1.Items.Add(cinPatient); // Ajouter CIN unique au comboBox1
                    }

                    reader.Close();
                }

                reader2.Close();

                if (!trouve) // Si aucun médecin trouvé
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

    }
}
