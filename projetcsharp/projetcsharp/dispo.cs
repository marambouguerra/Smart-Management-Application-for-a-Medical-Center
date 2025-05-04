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
    public partial class dispo : Form
    {
        OleDbConnection conn = new OleDbConnection(
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
);
        public dispo()
        {
            InitializeComponent();
        }

        private bool DisponibiliteExiste(string jour)
        {
            string query = "SELECT COUNT(*) FROM Disponibilites WHERE IdMedecin = " + stringajout.lastmedid + " AND Jour = '" + jour + "'";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string jour = comboBox1.Text;
            int heure_debut = 0;
            int heure_fin = 0;

            if (r1.Checked)
            {
                heure_debut = 6;
                heure_fin = 14;
            }
            else if (r2.Checked)
            {
                heure_debut = 14;
                heure_fin = 23;
            }

            if (heure_debut != 0 && heure_fin != 0)
            {
                if (!DisponibiliteExiste(jour))
                {
                    string reqDispo = "INSERT INTO Disponibilites (IdMedecin, Jour, HeureDébut, HeureFin) VALUES (" +
                                      stringajout.lastmedid + ", '" + jour + "', '" + heure_debut + "', '" + heure_fin + "')";
                    OleDbCommand cmdDispo = new OleDbCommand(reqDispo, conn);
                    cmdDispo.ExecuteNonQuery();
                    MessageBox.Show("Disponibilité ajoutée avec succès.");
                }
                else
                {
                    MessageBox.Show("Une disponibilité existe déjà pour ce jour.");
                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            string jour = comboBox1.Text;

            if (DisponibiliteExiste(jour))
            {
                // Supprimer l'ancienne disponibilité
                string deleteDispo = "DELETE FROM Disponibilites WHERE IdMedecin = " + stringajout.lastmedid + " AND Jour = '" + jour + "'";
                OleDbCommand cmdDeleteDispo = new OleDbCommand(deleteDispo, conn);
                cmdDeleteDispo.ExecuteNonQuery();

                // Ajout de la nouvelle disponibilité
                int heure_debut = 0;
                int heure_fin = 0;

                if (r1.Checked)
                {
                    heure_debut = 6;
                    heure_fin = 14;
                }
                else if (r2.Checked)
                {
                    heure_debut = 14;
                    heure_fin = 23;
                }

                if (heure_debut != 0 && heure_fin != 0)
                {
                    string reqDispo = "INSERT INTO Disponibilites (IdMedecin, Jour, HeureDébut, HeureFin) VALUES (" +
                                      stringajout.lastmedid + ", '" + jour + "', '" + heure_debut + "', '" + heure_fin + "')";
                    OleDbCommand cmdDispo = new OleDbCommand(reqDispo, conn);
                    cmdDispo.ExecuteNonQuery();
                    MessageBox.Show("Disponibilité modifiée avec succès.");
                }
            }
            else
            {
                MessageBox.Show("Aucune disponibilité trouvée pour ce jour.");
            }
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            string jour = comboBox1.Text;

            if (DisponibiliteExiste(jour))
            {
                string deleteDispo = "DELETE FROM Disponibilites WHERE IdMedecin = " + stringajout.lastmedid + " AND Jour = '" + jour + "'";
                OleDbCommand cmdDeleteDispo = new OleDbCommand(deleteDispo, conn);
                cmdDeleteDispo.ExecuteNonQuery();
                MessageBox.Show("Disponibilité supprimée avec succès.");
            }
            else
            {
                MessageBox.Show("Aucune disponibilité trouvée pour ce jour.");
            }

            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reg_med r = new reg_med();  
            r.Show();   
            this.Hide();
        }
    }
}