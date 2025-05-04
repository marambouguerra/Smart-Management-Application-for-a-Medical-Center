using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace projetcsharp
{
    public partial class reg_med : Form
    {
        OleDbConnection conn = new OleDbConnection(
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
);
        public reg_med()
        {
            InitializeComponent();
        }
        private void reg_med_Load(object sender, EventArgs e)
        {

        }



        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            administrateur rd = new administrateur();
            this.Hide();
            rd.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Vérification de l'existence du CIN
                string cin = textBox6.Text;
                OleDbCommand cmdd = new OleDbCommand("SELECT * FROM utilisateurs WHERE CIN = '" + cin + "';", conn);
                OleDbDataReader rdddd = cmdd.ExecuteReader();

                if (!rdddd.Read())
                {
                    rdddd.Close();

                    string sexe = radioButton1.Checked ? "femme" : "homme";

                    // Requête d'insertion dans utilisateurs
                    string req = "INSERT INTO utilisateurs (CIN, nom, prenom, email, sexe, telephone, role) " +
                                 "VALUES ('" + cin + "','" + textBox1.Text + "','" + textBox2.Text + "','" +
                                 textBox3.Text + "','" + sexe + "','" + textBox5.Text + "','" +
                                 stringajout.roleutilisteuteur2 + "');";

                    OleDbCommand cmd = new OleDbCommand(req, conn);
                    cmd.ExecuteNonQuery();

                    // Insertion dans médecins
                    string req2 = "INSERT INTO médecins (cin, specialite) VALUES ('" + cin + "', '" + textBox4.Text + "')";
                    OleDbCommand cmd2 = new OleDbCommand(req2, conn);
                    cmd2.ExecuteNonQuery();

                    // Récupération du dernier ID médecin
                    string getMedId = "SELECT MAX([N°médecin]) FROM médecins";
                    OleDbCommand getMedIdCmd = new OleDbCommand(getMedId, conn);
                    stringajout.lastmedid = Convert.ToInt32(getMedIdCmd.ExecuteScalar());

                    

                    MessageBox.Show("Utilisateur enregistré avec succès !");
                }
                else
                {
                    MessageBox.Show("CIN déjà existant !");
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


                string cin = textBox6.Text;
                OleDbCommand cmdd = new OleDbCommand("SELECT * FROM utilisateurs WHERE CIN = '" + cin + "';", conn);
                OleDbDataReader rdddd = cmdd.ExecuteReader();

                if (rdddd.Read())
                {
                    string sexe = radioButton1.Checked ? "femme" : "homme";

                    string updateUtil = "UPDATE utilisateurs SET nom = '" + textBox1.Text + "', prenom = '" + textBox2.Text +
                                        "', email = '" + textBox3.Text + "', sexe = '" + sexe + "', telephone = '" + textBox5.Text +
                                        "' WHERE CIN = '" + textBox6.Text + "';";
                    OleDbCommand cmdUpdateUtil = new OleDbCommand(updateUtil, conn);
                    cmdUpdateUtil.ExecuteNonQuery();

                    // Mise à jour dans la table médecins
                    string updateMedecin = "UPDATE médecins SET specialite = '" + textBox4.Text + "' WHERE cin = '" + textBox6.Text + "';";
                    OleDbCommand cmdUpdateMed = new OleDbCommand(updateMedecin, conn);
                    cmdUpdateMed.ExecuteNonQuery();

                    // Récupération de l'ID du médecin
                    string getMedId = "SELECT [N°médecin] FROM médecins WHERE cin = '" + textBox6.Text + "'";
                    OleDbCommand getIdCmd = new OleDbCommand(getMedId, conn);
                    stringajout.lastmedid = Convert.ToInt32(getIdCmd.ExecuteScalar());



                    MessageBox.Show("Médecin modifié avec succès !");
                }
                else
                {
                    MessageBox.Show("CIN nexiste pas  !");
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show("Erreur lors de la modification : " + ex.Message);
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

                // Récupérer l'ID du médecin à partir du CIN
                string getMedId = "SELECT [N°médecin] FROM médecins WHERE cin = '" + textBox6.Text + "'";
                OleDbCommand getIdCmd = new OleDbCommand(getMedId, conn);
                object result = getIdCmd.ExecuteScalar();

                if (result != null)
                {
                    stringajout.lastmedid = Convert.ToInt32(result);

           
                   
                    // Supprimer le médecin
                    string deleteMed = "DELETE FROM médecins WHERE [N°médecin] = " + stringajout.lastmedid;
                    OleDbCommand cmdDeleteMed = new OleDbCommand(deleteMed, conn);
                    cmdDeleteMed.ExecuteNonQuery();

                    // Supprimer l’utilisateur lié
                    string deleteUser = "DELETE FROM utilisateurs WHERE CIN = '" + textBox6.Text + "'";
                    OleDbCommand cmdDeleteUser = new OleDbCommand(deleteUser, conn);
                    cmdDeleteUser.ExecuteNonQuery();

                    MessageBox.Show("Médecin supprimé avec succès !");
                }
                else
                {
                    MessageBox.Show("Médecin introuvable !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            string getMedId = "SELECT [N°médecin] FROM médecins WHERE cin = '" + textBox6.Text + "'";
            OleDbCommand getIdCmd = new OleDbCommand(getMedId, conn);
            stringajout.lastmedid = Convert.ToInt32(getIdCmd.ExecuteScalar());
            conn.Close();
            dispo d = new dispo();
            d.Show();
            this.Hide();
        }
    }
}
