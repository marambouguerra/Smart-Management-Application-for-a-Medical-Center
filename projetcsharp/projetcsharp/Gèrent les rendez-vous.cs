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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Mail;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Org.BouncyCastle.Crypto.Macs;
using System.Reflection;
namespace projetcsharp
{
    public partial class Gèrent_les_rendez_vous : Form
    {
        OleDbConnection conn = new OleDbConnection(
     @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
 );
        public Gèrent_les_rendez_vous()
        {
            InitializeComponent();
        }

        private void Gèrent_les_rendez_vous_Load(object sender, EventArgs e)
        {
            conn.Open();
            string req = "select *from RendezVous where statut = 'en attente'";
            OleDbCommand cmd = new OleDbCommand(req, conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {

                comboBox1.Items.Add(dr["N°rendezvous"].ToString());
            }
            conn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            int rv = int.Parse(comboBox1.SelectedItem.ToString());
            string req = "select * from RendezVous where N°rendezvous=" + rv;
            OleDbCommand cmd = new OleDbCommand(req, conn);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                string jour = dr.GetDateTime(3).ToString("dddd", new System.Globalization.CultureInfo("fr-FR"));
                int heureSouhaitee = dr.GetInt32(5); // Heure de consultation souhaitée
                int numMedecin = dr.GetInt32(2);     // N°médecin
                int numPatient = dr.GetInt32(1);     // NumPatient si besoin

                textBox1.Text = dr.GetDateTime(3).ToString();

                textBox2.Text = heureSouhaitee.ToString();
                // Vérifier la disponibilité du médecin pour ce jour
                string req2 = "SELECT * FROM Disponibilites WHERE IdMedecin=" + numMedecin + " AND Jour='" + jour + "'";
                OleDbCommand cmd2 = new OleDbCommand(req2, conn);
                OleDbDataReader drr = cmd2.ExecuteReader();

                if (drr.Read())
                {
                    int hd = drr.GetInt32(3); // HeureDebut
                    int hf = drr.GetInt32(4); // HeureFin

                    if (heureSouhaitee >= hd && heureSouhaitee < hf)
                    {
                        string dateStr = dr.GetDateTime(3).ToString("dd/MM/yyyy"); // même format que celui enregistré en texte
                        string req3 = "SELECT COUNT(*) FROM Consultation WHERE [N°médecin]=" + numMedecin +
                                      " AND date_consultation = '" + dateStr + "'" +
                                      " AND heure = " + heureSouhaitee;


                        OleDbCommand cmd3 = new OleDbCommand(req3, conn);
                        int nbConsult = Convert.ToInt32(cmd3.ExecuteScalar());

                        if (nbConsult == 0)
                        {
                            textBox3.Text = "Le médecin est disponible.";
                        }
                        else
                        {
                            textBox3.Text = "Le médecin a déjà une consultation à cette heure.";
                        }
                    }
                    else
                    {
                        textBox3.Text = "Le médecin n’est pas disponible ce jour à cette heure.";
                    }
                }
                else
                {
                    textBox3.Text = "Aucune disponibilité trouvée pour ce jour.";
                }
            }


            conn.Close();
        }


      
       private void button1_Click(object sender, EventArgs e)
        {
            string reponse = "";

            if (radioButton1.Checked)
            {
                reponse = "confirmé";
            }
            else if (radioButton2.Checked)
            {
                reponse = "annulé";
            }

            if (!string.IsNullOrEmpty(reponse))
            {
                try
                {
                    conn.Open();
                    int numRv = int.Parse(comboBox1.Text);

                    string req = "UPDATE RendezVous SET statut = '" + reponse + "' WHERE [N°rendezvous] = " + numRv;
                    OleDbCommand cmd = new OleDbCommand(req, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Le statut a été mis à jour avec succès.");

                    if (radioButton1.Checked)
                    {
                        string req2 = "SELECT * FROM RendezVous WHERE [N°rendezvous] = " + numRv;
                        OleDbCommand cmd2 = new OleDbCommand(req2, conn);
                        OleDbDataReader dr = cmd2.ExecuteReader();

                        if (dr.Read())
                        {
                            int numPatient = dr.GetInt32(1);
                            int numMedecin = dr.GetInt32(2);
                            
                            DateTime dateConsult = dr.GetDateTime(3);
                            int heureConsult = int.Parse(textBox2.Text);
                            string statut = "enattente";
                            string req3 = "INSERT INTO consultation (N°rendezvous, N°médecin, NumPatient, date_consultation, heure,statut) VALUES (" +
                                          numRv + ", " + numMedecin + ", " + numPatient + ", '" + dateConsult + "', '" + heureConsult + "', '" +statut + "')";
                            OleDbCommand cmd3 = new OleDbCommand(req3, conn);
                            cmd3.ExecuteNonQuery();
                            string req6 = "UPDATE DossiersMedicaux SET [N°medecin] = " + numMedecin + " WHERE [N°patient] = " + numPatient;
                            OleDbCommand cmd6 = new OleDbCommand(req6, conn);

                            cmd6.ExecuteNonQuery();
                            string req4 = "SELECT cin FROM patient WHERE N°patient = " + numPatient;
                            OleDbCommand cmd4 = new OleDbCommand(req4, conn);
                            OleDbDataReader r = cmd4.ExecuteReader();

                            if (r.Read())
                            {
                                string cin = r.GetString(0);
                                string req5 = "SELECT * FROM utilisateurs WHERE CIN = '" + cin + "'";
                                OleDbCommand cmd5 = new OleDbCommand(req5, conn);
                                OleDbDataReader d = cmd5.ExecuteReader();

                                if (d.Read())
                                {
                                    string destinataire = d.GetString(3);
                                    string sujet = "Confirmation de rendez-vous";
                                    string nomprenom = d.GetString(1) + " " + d.GetString(2);
                                    string message = "Bonjour " + nomprenom + ", votre rendez-vous a été confirmé pour le " + dateConsult + " à " + heureConsult + ".";

                                    try
                                    {
                                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                                        client.UseDefaultCredentials = false;
                                        client.Credentials = new NetworkCredential("bouzidmariem971@gmail.com", "wwyl oabc xqko xinc");
                                        client.EnableSsl = true;

                                        MailMessage mail = new MailMessage();
                                        mail.From = new MailAddress("bouzidmariem971@gmail.com");
                                        mail.To.Add(destinataire);
                                        mail.Subject = sujet;
                                        mail.Body = message;

                                        client.Send(mail);

                                        string messageNotif = message.Replace("'", "''");
                                        string reqNotif = "INSERT INTO notifications (numpatient, Type, Message, date_envoi) VALUES (" +
                                                          numPatient + ", 'Email', '" + messageNotif + "', '" + DateTime.Now.ToString() + "')";
                                        OleDbCommand cmdNotif = new OleDbCommand(reqNotif, conn);
                                        cmdNotif.ExecuteNonQuery();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Erreur lors de l'envoi du mail : " + ex.Message);
                                    }
                                }
                            }
                        }
                    }

                    if (radioButton2.Checked)
                    {
                        string req2 = "SELECT * FROM RendezVous WHERE [N°rendezvous] = " + numRv;
                        OleDbCommand cmd2 = new OleDbCommand(req2, conn);
                        OleDbDataReader dr = cmd2.ExecuteReader();

                        if (dr.Read())
                        {
                            int numPatient = dr.GetInt32(1);

                            string destinataire = dr.GetString(3);
                            string sujet = "Annulation de rendez-vous";
                            string message = "Bonjour, nous vous informons que votre rendez-vous a été annulé.";

                            try
                            {
                                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                                client.UseDefaultCredentials = false;
                                client.Credentials = new NetworkCredential("bouzidmariem971@gmail.com", "wwyl oabc xqko xinc");
                                client.EnableSsl = true;

                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress("bouzidmariem971@gmail.com");
                                mail.To.Add(destinataire);
                                mail.Subject = sujet;
                                mail.Body = message;

                                client.Send(mail);

                                string messageNotif = message.Replace("'", "''");
                                string reqNotif = "INSERT INTO notifications (numpatient, Type, Message, date_envoi) VALUES (" +
                                                  numPatient + ", 'Email', '" + messageNotif + "', '" + DateTime.Now.ToString() + "')";
                                OleDbCommand cmdNotif = new OleDbCommand(reqNotif, conn);
                                cmdNotif.ExecuteNonQuery();
                               
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Erreur lors de l'envoi de l'email d'annulation : " + ex.Message);
                            }
                           
                        }
                    }
                    comboBox1.Items.Remove(comboBox1.Text);
                    comboBox1.Text = "";
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
            else
            {
                MessageBox.Show("Veuillez sélectionner un statut.");
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            secretaire s  = new secretaire();   
            s.Show();   
            this.Hide();
        }
    }
}