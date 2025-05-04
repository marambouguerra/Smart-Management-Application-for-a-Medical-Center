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
using System.ComponentModel; // pour des composants WinForms
using OfficeOpenXml;
using EPPlusLicenseContext = OfficeOpenXml.LicenseContext;
using System.Drawing; // Namespace pour System.Drawing.Font
using iTextSharp.text; // Namespace pour iTextSharp.text.Font
using iTextSharp.text.pdf;

namespace projetcsharp
{
    public partial class exel : Form
    {
        OleDbConnection conn = new OleDbConnection(
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Lenovo\OneDrive\Bureau\c#\projet c# 2025\Centre Médical.accdb"
);
        public exel()
        {
            InitializeComponent();

        }

        private void exel_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = @"SELECT 
                        utilisateurs.nom & ' ' & utilisateurs.prenom AS NomComplet, 
                        médecins.specialite, 
                        COUNT(consultation.[N°consultation]) AS NbConsultations 
                    FROM 
                        (médecins 
                        INNER JOIN utilisateurs ON médecins.cin = utilisateurs.[CIN]) 
                    LEFT JOIN 
                        consultation ON médecins.[N°médecin] = consultation.[N°médecin] 
                    GROUP BY 
                        utilisateurs.nom, utilisateurs.prenom, médecins.specialite 
                    ORDER BY 
                        COUNT(consultation.[N°consultation]) desc";

                OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
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
                string query = "SELECT saisons, COUNT(*) AS NbConsultations FROM consultation GROUP BY saisons";

                OleDbCommand command = new OleDbCommand(query, conn);

                OleDbDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader); // Utilisez Load pour remplir le DataTable directement avec le reader.
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la récupération des données : " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close(); // Assurez-vous que la connexion est fermée après l'exécution.
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                DateTime today = DateTime.Now.Date;
                string formattedDate = today.ToString("dd/MM/yyyy");
                string statutt = "deja";
                string query = "SELECT NumPatient, date_consultation, heure, payee FROM consultation WHERE FORMAT(date_consultation, 'dd/MM/yyyy')  <= #" + formattedDate + "# and statut = '" + statutt +"'";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;

                // Mise en couleur selon le statut de paiement
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["payee"].Value != null)
                    {
                        string statut = row.Cells["payee"].Value.ToString().ToLower();

                       
                        if (statut == "70")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightBlue; 
                        }
                             else 
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral; 
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }





        private void button4_Click(object sender, EventArgs e)
        {
            // Spécifier la licence avant d'utiliser ExcelPackage
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Feuille1");

                        // Entêtes
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = dataGridView1.Columns[i].HeaderText;
                        }

                        // Données
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.Columns.Count; j++)
                            {
                                worksheet.Cells[i + 2, j + 1].Value = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                            }
                        }

                        package.SaveAs(new FileInfo(sfd.FileName));
                        MessageBox.Show("Fichier Excel exporté avec succès !");
                    }
                }
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || dataGridView1.Columns.Count == 0)
            {
                MessageBox.Show("Aucune donnée à exporter !");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Fichiers PDF|*.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(sfd.FileName))
                {
                    try
                    {
                        Document doc = new Document(PageSize.A4, 10f, 10f, 20f, 20f); // marges
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();

                        // Titre
                        iTextSharp.text.Font titleFont = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD);
                        Paragraph titre = new Paragraph("Exportation des données", titleFont);
                        titre.Alignment = Element.ALIGN_CENTER;
                        titre.SpacingAfter = 20f;
                        doc.Add(titre);

                        // Table
                        PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
                        table.WidthPercentage = 100;

                        // Entêtes
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)));
                            cell.BackgroundColor = new BaseColor(230, 230, 250); // léger violet
                            table.AddCell(cell);
                        }

                        // Données
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    string value = cell?.Value?.ToString() ?? "";
                                    table.AddCell(new Phrase(value, FontFactory.GetFont("Arial", 11)));
                                }
                            }
                        }

                        doc.Add(table);
                        doc.Close();

                        MessageBox.Show("PDF exporté avec succès !");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de l'export PDF : " + ex.Message);
                    }
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            administrateur ad= new administrateur();
            ad.Show();
            this.Hide();
        }
    }
} 