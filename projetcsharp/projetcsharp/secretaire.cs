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
    public partial class secretaire : Form
    {
        public secretaire()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gèrent_les_rendez_vous g = new Gèrent_les_rendez_vous();
            g.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dossieradministratif d = new dossieradministratif();
            d.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 x = new Form1();   
            x.Show();   
            this.Hide();    

        }
    }
}
