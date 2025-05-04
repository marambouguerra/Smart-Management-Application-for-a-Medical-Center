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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iTextSharp.text;
using iTextSharp.text.pdf;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace projetcsharp
{
    public partial class consultation : Form
    {
        OleDbConnection conn = new OleDbConnection(
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
);
        public consultation()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            choixconc m = new choixconc();
            m.Show();
            this.Hide();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }





        private void button1_Click(object sender, EventArgs e)
      
        {
            try
            {
                conn.Open();

                int idpatient = 0;
                int idMedecin = 0;
                int payee = 0;
                int lastcon = 0;

                // 1. Récupérer le CIN du patient
                string cinpatient = comboBox1.Text;

                // 2. Récupérer l'ID du patient
                string req1 = "SELECT [N°patient] FROM patient WHERE cin = '" + cinpatient + "'";
                OleDbCommand cmd1 = new OleDbCommand(req1, conn);
                OleDbDataReader rd = cmd1.ExecuteReader();
                if (rd.Read())
                {
                    idpatient = rd.GetInt32(0);
                }
                rd.Close();

                // 3. Récupérer l'ID du médecin
                string req2 = "SELECT [N°médecin] FROM médecins WHERE cin = '" + stringajout.cin + "'";
                OleDbCommand cmd2 = new OleDbCommand(req2, conn);
                OleDbDataReader rd2 = cmd2.ExecuteReader();
                if (rd2.Read())
                {
                    idMedecin = rd2.GetInt32(0);
                }
                rd2.Close();

                string diagnostic = textBox3.Text.Replace("'", "''");  // Pour éviter les erreurs d'apostrophes
                string traitement = textBox2.Text.Replace("'", "''");
                string saisons = comboBox2.Text.Replace("'", "''");

                // 5. Déterminer paiement
                if (radioButton1.Checked)
                {
                    payee = 70;
                }
                else if (radioButton2.Checked)
                {
                    payee = 0;
                }

                // 6. Récupérer la dernière consultation
                string req3 = "SELECT * FROM consultation WHERE [N°médecin] = " + idMedecin + " AND NumPatient = " + idpatient;
                OleDbCommand cmd3 = new OleDbCommand(req3, conn);
                OleDbDataReader rdd = cmd3.ExecuteReader();
                if (rdd.Read())
                {
                    lastcon = rdd.GetInt32(0);
                }
                rdd.Close();

                string statut = "deja";
                string updateQuery = "UPDATE consultation SET diagnostic = '" + diagnostic +
                                     "', traitement = '" + traitement +
                                     "', saisons = '" + saisons +
                                     "', payee = " + payee +
                                     ", statut = '" + statut +
                                     "' WHERE [N°médecin] = " + idMedecin +
                                     " AND NumPatient = " + idpatient;

                OleDbCommand cmdUpdate = new OleDbCommand(updateQuery, conn);
                int rowsAffected = cmdUpdate.ExecuteNonQuery();

                if (rowsAffected != -1)
                {
                    if (radioButton1.Checked)
                    {
                        string reqq = "SELECT * FROM utilisateurs WHERE CIN = '" + cinpatient + "'";
                        OleDbCommand cmdd = new OleDbCommand(reqq, conn);
                        OleDbDataReader r = cmdd.ExecuteReader();
                        if (r.Read())
                        {
                            decimal montant = Convert.ToDecimal(textBox4.Text);
                            string modePaiement = comboBox3.Text.Replace("'", "''");

                            // Créer la facture PDF
                            Document doc = new Document();
                            string filePath = "facture.pdf";
                            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                            doc.Open();
                            doc.Add(new Paragraph("Centre Médical"));
                            doc.Add(new Paragraph($"Date : {DateTime.Now}"));
                            doc.Add(new Paragraph("Facture pour consultation " + lastcon));
                            doc.Add(new Paragraph("Nom et prénom : " + r.GetString(1) + " " + r.GetString(2)));
                            doc.Add(new Paragraph("Mode de paiement : " + modePaiement));
                            doc.Add(new Paragraph("Montant : " + montant.ToString("F2")));
                            doc.Close();

                            // Insertion de la facture dans la base
                            string reqFacture = "INSERT INTO factures (N°patient, montant, date_emission, mode_paiement, nconsultation, pdf) " +
                                                "VALUES (" + idpatient + ", " + montant.ToString().Replace(",", ".") +
                                                ", #" + DateTime.Now.ToString("yyyy-MM-dd") + "#, '" + modePaiement +
                                                "', " + lastcon + ", '" + filePath + "')";
                            OleDbCommand cmdFacture = new OleDbCommand(reqFacture, conn);
                            cmdFacture.ExecuteNonQuery();

                            // Ouvrir la facture
                            string getf = "SELECT MAX([N°factures]) FROM factures";
                            OleDbCommand cmdgetf = new OleDbCommand(getf, conn);
                            int lastf = Convert.ToInt32(cmdgetf.ExecuteScalar());

                            string ch = "SELECT pdf FROM factures WHERE [N°factures] = " + lastf;
                            OleDbCommand cmd = new OleDbCommand(ch, conn);
                            OleDbDataReader reader = cmd.ExecuteReader();

                            if (reader.Read())
                            {
                                string cheminFacture = reader.GetString(0);
                                if (File.Exists(cheminFacture))
                                {
                                    var process = new System.Diagnostics.Process();
                                    process.StartInfo = new System.Diagnostics.ProcessStartInfo
                                    {
                                        FileName = Path.GetFullPath(cheminFacture),
                                        UseShellExecute = true
                                    };
                                    process.Start();
                                }
                                reader.Close();
                            }
                        }

                        MessageBox.Show("Consultation et facture ajoutées avec succès !");
                    }
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

        }

        private void consultation_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                comboBox1.Items.Clear();

                DateTime today = DateTime.Now.Date;
                string formattedDate = today.ToString("dd/MM/yyyy");  // Format correct pour Access

                string req2 = "SELECT * FROM médecins WHERE cin = '" + stringajout.cin + "'";
                OleDbCommand command2 = new OleDbCommand(req2, conn);
                OleDbDataReader reader2 = command2.ExecuteReader();

                while (reader2.Read())
                {
                    int nummed = reader2.GetInt32(0);

                    string statut = "enattente";
                    // UTILISER FORMAT pour ne prendre en compte que la date sans l'heure
                    string query = "SELECT * FROM consultation WHERE FORMAT(date_consultation, 'dd/MM/yyyy') = #" + formattedDate + "# AND [N°médecin] = " + nummed + "  AND statut='" + statut +"'";

                    OleDbCommand command = new OleDbCommand(query, conn);
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int num = reader.GetInt32(3); // Assure-toi que c’est bien l’index du champ N°patient
                        string req = "SELECT DISTINCT cin FROM patient WHERE [N°patient] = " + num;
                        OleDbCommand command1 = new OleDbCommand(req, conn);
                        OleDbDataReader reader1 = command1.ExecuteReader();

                        while (reader1.Read())
                        {
                            
                            comboBox1.Items.Add(reader1.GetString(0));  
                        }

                        reader1.Close();
                    }

                    reader.Close();
                }

                reader2.Close();
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