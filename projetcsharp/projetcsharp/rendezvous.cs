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
using System.Runtime.Intrinsics.Arm;
namespace projetcsharp
{
    public partial class rendezvous : Form
    {
        OleDbConnection conn = new OleDbConnection(
      @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
  );
        public rendezvous()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string cin = stringajout.cin;
                string reqPatient = "SELECT [N°patient] FROM patient WHERE CIN = '" + cin + "'";
                OleDbCommand cmdPatient = new OleDbCommand(reqPatient, conn);
                OleDbDataReader rdPatient = cmdPatient.ExecuteReader();

                if (!rdPatient.Read())
                {
                    MessageBox.Show("Patient non trouvé !");
                    return;
                }

                int idPatient = Convert.ToInt32(rdPatient["N°patient"]);
                rdPatient.Close();

                int idMedecin = int.Parse(comboBox2.SelectedItem.ToString());
                string dateRdv = dateTimePicker1.Value.ToString("MM/dd/yyyy HH:mm:ss");
                string heure = textBox1.Text;
                string statut = "en attente";

                string insertReq = "INSERT INTO RendezVous ([N°patient], [N°médecin], [date], [statut], [heure]) " +
                                   "VALUES (" + idPatient + ", " + idMedecin + ", #" + dateRdv + "#, '" + statut + "', '" + heure + "')";

                OleDbCommand cmdInsert = new OleDbCommand(insertReq, conn);
                cmdInsert.ExecuteNonQuery();

                MessageBox.Show("Rendez-vous ajouté avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout du rendez-vous : " + ex.Message);
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
                string specialite = comboBox1.Text;
                string reqMedecins = "SELECT * FROM médecins WHERE specialite = '" + specialite + "'";
                OleDbCommand cmdMedecins = new OleDbCommand(reqMedecins, conn);
                OleDbDataReader rdMedecins = cmdMedecins.ExecuteReader();

                comboBox2.Items.Clear();

                while (rdMedecins.Read())
                {
                    int numMed = Convert.ToInt32(rdMedecins["N°médecin"]);
                    comboBox2.Items.Add(numMed);

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



        private void rendezvous_Load(object sender, EventArgs e)
        {
            conn.Open();
            string req = "select DISTINCT specialite from médecins ";
            OleDbCommand cmd = new OleDbCommand(req, conn);
            OleDbDataReader rd = cmd.ExecuteReader();
            comboBox1.Items.Clear();

            // Ajouter les éléments retournés par la requête
            while (rd.Read())
            {
                comboBox1.Items.Add(rd["specialite"].ToString());
            }
            conn.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                int nummed = int.Parse(comboBox2.SelectedItem.ToString());

                string req = "SELECT * FROM médecins WHERE [N°médecin] = " + nummed;
                OleDbCommand cmd = new OleDbCommand(req, conn);
                OleDbDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    string cin = rd.GetString(1); // On suppose que le CIN est à l'index 1
                    string req1 = "SELECT * FROM utilisateurs WHERE CIN = '" + cin + "'"; // ✅ CIN entre guillemets
                    OleDbCommand cmd1 = new OleDbCommand(req1, conn);
                    OleDbDataReader rdd = cmd1.ExecuteReader();

                    if (rdd.Read())
                    {
                        string nom = rdd["nom"].ToString() + " " + rdd["prenom"].ToString();
                        textBox2.Text = nom;
                    }

                    rdd.Close();
                }

                rd.Close();
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
            patient p = new patient();
            p.Show();
            this.Hide();

        }
    }
}
