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
    public partial class administrateur : Form
    {

        public administrateur()
        {
            InitializeComponent();
        }

        private void admin_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            reg_med m = new reg_med();
            this.Hide();
            m.Show();
            stringajout.roleutilisteuteur2 = "medecin";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            gerSE gs = new gerSE();
            this.Hide();
            gs.Show();
            stringajout.roleutilisteuteur2 = "secretaire";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            gerpa g = new gerpa();
            g.Show();
            this.Hide();
            stringajout.roleutilisteuteur2 = "patient";
        }

        private void button5_Click(object sender, EventArgs e)
        {
           exel x = new exel();
            x.Show();
            this.Close();
        }
    }
}
