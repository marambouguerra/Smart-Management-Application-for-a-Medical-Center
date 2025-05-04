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
    public partial class patient : Form
    {
        public patient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rendezvous r = new rendezvous();
            r.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dossierpatient d = new dossierpatient();
            d.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            dossierpatient ds = new dossierpatient();
            ds.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            notification n= new notification();
            n.Show();
            this.Hide();    

        }
    }
}
