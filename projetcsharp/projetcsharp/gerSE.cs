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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace projetcsharp
{
    public partial class gerSE : Form
    {


        OleDbConnection conn = new OleDbConnection(
     @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
 );
        public gerSE()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string checkQuery = "SELECT * FROM utilisateurs WHERE CIN = '" + textBox4.Text + "' AND role = '" + stringajout.roleutilisteuteur2 + "'";
                OleDbCommand checkCmd = new OleDbCommand(checkQuery, conn);
                OleDbDataReader rdddd = checkCmd.ExecuteReader();

                if (!rdddd.Read())
                {
                    string sexe = radioButton1.Checked ? "femme" : "homme";

                    string insertQuery = "INSERT INTO utilisateurs(CIN, nom, prenom, email, sexe, telephone, role) " +
                                         "VALUES('" + textBox4.Text + "', '" +
                                                    textBox1.Text + "', '" +
                                                    textBox2.Text + "', '" +
                                                    textBox3.Text + "', '" +
                                                    sexe + "', '" +
                                                    textBox5.Text + "', '" +
                                                    stringajout.roleutilisteuteur2 + "')";

                    OleDbCommand insertCmd = new OleDbCommand(insertQuery, conn);
                    int result = insertCmd.ExecuteNonQuery();

                    if (result != -1)
                    {
                        MessageBox.Show("Utilisateur enregistré avec succès !");
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


      

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string checkQuery = "SELECT * FROM utilisateurs WHERE CIN = '" + textBox4.Text + "' AND role = '" + stringajout.roleutilisteuteur2 + "'";
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
                                               "WHERE CIN = '" + textBox4.Text + "' AND role = '" + stringajout.roleutilisteuteur2 + "'";

                    OleDbCommand updateUserCmd = new OleDbCommand(updateUtilisateur, conn);
                    int result1 = updateUserCmd.ExecuteNonQuery();

                 

                    if (result1 != -1 )
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                OleDbCommand checkCmd = new OleDbCommand("SELECT * FROM utilisateurs WHERE CIN = '" + textBox4.Text + "' AND role = '" + stringajout.roleutilisteuteur2 + "'", conn);
                OleDbDataReader reader = checkCmd.ExecuteReader();

                if (reader.Read())
                {
                   

                    // Supprimer l'utilisateur
                    OleDbCommand delUtilisateur = new OleDbCommand("DELETE FROM utilisateurs WHERE CIN = '" + textBox4.Text + "' AND role = '" + stringajout.roleutilisteuteur2 + "'", conn);
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


        private void button4_Click(object sender, EventArgs e)
        {
            administrateur ad = new administrateur();
            this.Hide();
            ad.Show();
        }
    }
}
