using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetcsharp
{
    public partial class medecin : Form
    {
        public medecin()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            fr.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tempmed t = new tempmed();
            t.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dossier_medical d = new dossier_medical();
            d.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           choixconc c = new choixconc();
            c.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            document_medicaux d = new document_medicaux();
            d.Show();
            this.Hide();
        }
    }
}
