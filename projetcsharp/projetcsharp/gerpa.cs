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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace projetcsharp
{
    public partial class gerpa : Form
    {
        OleDbConnection conn = new OleDbConnection(
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
);
        public gerpa()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                OleDbCommand cmdd = new OleDbCommand("SELECT * FROM utilisateurs WHERE CIN = '" + textBox6.Text + "' AND role ='" + stringajout.roleutilisteuteur2 + "';", conn);
                OleDbDataReader rdddd = cmdd.ExecuteReader();
                if (!rdddd.Read())
                {
                    string sexe;
                    if (radioButton1.Checked)
                    {
                        sexe = "femme";
                    }
                    else
                    {
                        sexe = "homme";
                    }

                    string req = "INSERT INTO utilisateurs(CIN, nom, prenom, email, sexe, telephone, role ) " +
                                 "VALUES('" + textBox6.Text + "','" +
                                              textBox1.Text + "','" +
                                              textBox2.Text + "','" +
                                              textBox3.Text + "','" +
                                              sexe + "','" +
                                              textBox5.Text + "','" +
                                              stringajout.roleutilisteuteur2 + "');";

                    OleDbCommand cmd = new OleDbCommand(req, conn);
                    int rd = cmd.ExecuteNonQuery();
                    if (rd != -1)
                    {
                        MessageBox.Show("Utilisateur enregistré avec succès !");
                    }

                   
                    string req2 = "INSERT INTO patient (cin, date_naissance, adresse) VALUES ('" + textBox6.Text + "', '" + dateTimePicker1.Value.ToString() + "','" + textBox4.Text + "')";
                    OleDbCommand cmd2 = new OleDbCommand(req2, conn);
                    int rd2 = cmd2.ExecuteNonQuery();
                    if (rd2 != -1)
                    {
                        string getpatdId = "SELECT MAX([N°patient]) FROM patient";
                        OleDbCommand gepatIdCmd = new OleDbCommand(getpatdId, conn);
                        int lastMedId = Convert.ToInt32(gepatIdCmd.ExecuteScalar());

                        String req3 = "INSERT INTO DossiersMedicaux ([N°patient], datecreation) " +
                "VALUES (" + lastMedId + ", Now())";

                        OleDbCommand cmd3 = new OleDbCommand(req3, conn);
                        cmd3.ExecuteNonQuery();
                        string req4 = "INSERT INTO dossieradministratif ( idpatient) " +
                                      "VALUES('" + lastMedId + "')";

                        OleDbCommand cmd4 = new OleDbCommand(req4, conn);
                        cmd4.ExecuteNonQuery();

                    }
                }
                else
                {
                    MessageBox.Show("Utilisateur déjà existant !");
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
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                OleDbCommand checkCmd = new OleDbCommand("SELECT * FROM utilisateurs WHERE CIN = '" + textBox6.Text + "' AND role = '" + stringajout.roleutilisteuteur2 + "'", conn);
                OleDbDataReader reader = checkCmd.ExecuteReader();

                if (reader.Read())
                {
                    // Récupérer le N°patient correspondant au CIN
                    OleDbCommand getPatIdCmd = new OleDbCommand("SELECT [N°patient] FROM patient WHERE cin = '" + textBox6.Text + "'", conn);
                    object patIdObj = getPatIdCmd.ExecuteScalar();

                    if (patIdObj != null)
                    {
                        int patientId = Convert.ToInt32(patIdObj);

                        // Supprimer les lignes associées dans les tables dépendantes
                        OleDbCommand delDossierMed = new OleDbCommand("DELETE FROM DossiersMedicaux WHERE [N°patient] = " +  patientId, conn);
                        delDossierMed.ExecuteNonQuery();

                        OleDbCommand delDossierAdmin = new OleDbCommand("DELETE FROM dossieradministratif WHERE idpatient = " + patientId, conn);
                        delDossierAdmin.ExecuteNonQuery();
                    }

                    // Supprimer le patient
                    OleDbCommand delPatient = new OleDbCommand("DELETE FROM patient WHERE cin = '" + textBox6.Text + "'", conn);
                    delPatient.ExecuteNonQuery();

                    // Supprimer l'utilisateur
                    OleDbCommand delUtilisateur = new OleDbCommand("DELETE FROM utilisateurs WHERE CIN = '" + textBox6.Text + "' AND role = '" + stringajout.roleutilisteuteur2 + "'", conn);
                    delUtilisateur.ExecuteNonQuery();

                    MessageBox.Show("Utilisateur supprimé avec succès !");
                }
                else
                {
                    MessageBox.Show("Utilisateur introuvable !");
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string checkQuery = "SELECT * FROM utilisateurs WHERE CIN = '" + textBox6.Text + "' AND role = '" + stringajout.roleutilisteuteur2 + "'";
                OleDbCommand checkCmd = new OleDbCommand(checkQuery, conn);
                OleDbDataReader reader = checkCmd.ExecuteReader();

                if (reader.Read())
                {
                    string sexe = radioButton1.Checked ? "femme" : "homme";

                    // Mise à jour dans la table utilisateurs
                    string updateUtilisateur = "UPDATE utilisateurs SET " +
                                               "nom = '" + textBox1.Text + "', " +
                                               "prenom = '" + textBox2.Text + "', " +
                                               "email = '" + textBox3.Text + "', " +
                                               "sexe = '" + sexe + "', " +
                                               "telephone = '" + textBox5.Text + "' " +
                                               "WHERE CIN = '" + textBox6.Text + "' AND role = '" + stringajout.roleutilisteuteur2 + "'";

                    OleDbCommand updateUserCmd = new OleDbCommand(updateUtilisateur, conn);
                    int result1 = updateUserCmd.ExecuteNonQuery();

                    // Mise à jour dans la table patient
                    string formattedDate = "#" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "#";
                    string updatePatient = "UPDATE patient SET " +
                                           "adresse = '" + textBox4.Text + "', " +
                                           "date_naissance = " + formattedDate + " " +
                                           "WHERE cin = '" + textBox6.Text + "'";

                    OleDbCommand updatePatientCmd = new OleDbCommand(updatePatient, conn);
                    int result2 = updatePatientCmd.ExecuteNonQuery();

                    if (result1 != -1 && result2 != -1)
                    {
                        MessageBox.Show("Utilisateur modifié avec succès !");
                    }
                    else
                    {
                        MessageBox.Show("Échec de la mise à jour.");
                    }
                }
                else
                {
                    MessageBox.Show("Utilisateur introuvable !");
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




        private void button4_Click(object sender, EventArgs e)
        {
            administrateur a = new administrateur();
            a.Show();
            this.Hide();

        }
    }
}


