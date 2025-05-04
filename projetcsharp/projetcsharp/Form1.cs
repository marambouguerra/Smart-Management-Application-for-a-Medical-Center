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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void administrateur_Click(object sender, EventArgs e)
        {
            login log = new login();
            this.Hide();
            log.Show();
            stringajout.roleutilisteuteur = "administrateur";

        }

        private void medecin_Click(object sender, EventArgs e)
        {
            login log = new login();
            this.Hide();
            log.Show();
            stringajout.roleutilisteuteur = "medecin";
        }

        private void patient_Click(object sender, EventArgs e)
        {
            login log = new login();
            this.Hide();
            log.Show();
            stringajout.roleutilisteuteur = "patient";
        }

        private void secretiare_Click(object sender, EventArgs e)
        {
            login log = new login();
            this.Hide();
            log.Show();
            stringajout.roleutilisteuteur = "secretaire";
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}