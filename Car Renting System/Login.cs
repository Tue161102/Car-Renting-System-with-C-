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
    public partial class Login : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonlogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {
                    var item = db.Accounts.FirstOrDefault(s => s.Password == textBox2.Text && s.Email == textBox1.Text);
                    if (item != null)
                    {
                        WelcomeScreen wc = new WelcomeScreen();

                        wc.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản của bạn không tồn tại");
                    }
                }
                else
                {
                    MessageBox.Show("Email hoặc password không hợp lệ! Thử lại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

    }
}
