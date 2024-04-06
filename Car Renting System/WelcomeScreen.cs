using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Renting_System
{
    public partial class WelcomeScreen : Form
    {
        public WelcomeScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thuexe a = new Thuexe();
            a.Show();
            this.Hide();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Danhsachxe d = new Danhsachxe();
            d.Show();
            this.Hide();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Baocao b = new Baocao();
            b.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bienlai bienlai = new Bienlai();
            bienlai.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }
    }
}
