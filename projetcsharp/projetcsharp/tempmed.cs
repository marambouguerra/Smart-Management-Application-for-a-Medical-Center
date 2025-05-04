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
    public partial class tempmed : Form
    {
        OleDbConnection conn = new OleDbConnection(
      @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
  );
        public tempmed()
        {
            InitializeComponent();
        }

        private void tempmed_Load(object sender, EventArgs e)


        {
            try
            {
                conn.Open();

                // Requête Médecin (cin entre apostrophes car c’est une chaîne de caractères)
                string reqMedecin = "SELECT * FROM Médecins WHERE cin = '" + stringajout.cin + "'";
                OleDbCommand cmdMedecin = new OleDbCommand(reqMedecin, conn);
                OleDbDataReader reader = cmdMedecin.ExecuteReader();

                if (reader.Read())
                {
                    int idMedecin = reader.GetInt32(0);

                    // Requête Utilisateur
                    string reqUtilisateur = "SELECT * FROM utilisateurs WHERE CIN = '" + stringajout.cin + "'";
                    OleDbCommand cmdUtilisateur = new OleDbCommand(reqUtilisateur, conn);
                    OleDbDataReader reader1 = cmdUtilisateur.ExecuteReader();

                    if (reader1.Read())
                    {
                        string nom = reader1.GetString(1);
                        string prenom = reader1.GetString(2);

                        textBox1.Text = reader["CIN"].ToString();
                        textBox2.Text = "Docteur " + nom + " " + prenom;
                    }

                    reader1.Close();

                    // Préparer le tableau pour les disponibilités
                    DataTable dt = new DataTable();
                    dt.Columns.Add("jour", typeof(string));
                    dt.Columns.Add("heure debut", typeof(int));
                    dt.Columns.Add("heure fin", typeof(int));

                    string reqDisp = "SELECT * FROM Disponibilites WHERE IdMedecin = " + idMedecin;
                    OleDbCommand cmdDisp = new OleDbCommand(reqDisp, conn);
                    OleDbDataReader reader2 = cmdDisp.ExecuteReader();

                    int rowCount = 0;
                    while (reader2.Read())
                    {
                        string jour = reader2.GetString(2);
                        int heureDebut = reader2.GetInt32(3);
                        int heureFin = reader2.GetInt32(4);

                        dt.Rows.Add(jour, heureDebut, heureFin);
                        rowCount++;
                    }

                    reader2.Close();

                    if (rowCount == 0)
                    {
                        MessageBox.Show("Aucune disponibilité trouvée pour ce médecin.");
                    }

                    dataGridView1.DataSource = dt;
                    dataGridView1.Refresh();
                }
                else
                {
                    MessageBox.Show("Médecin introuvable !");
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

        private void button4_Click(object sender, EventArgs e)
        {
            medecin m = new medecin();
            m.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
