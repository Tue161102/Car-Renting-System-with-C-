using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Renting_System
{
    public partial class Bienlai : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Bienlai()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DGVPrinter p = new DGVPrinter();
            dataGridView1.Columns["ID"].Visible = false;
            p.printDocument = printDocument1;
            p.Title = "Biên lai thuê xe";
            p.SubTitle = string.Format("Date:{0}", DateTime.Now);
            p.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            p.printDocument = printDocument1;
            p.PageNumbers = true;
            p.PageNumberInHeader = true;
            p.PorportionalColumns = true;
            p.HeaderCellAlignment = StringAlignment.Near;
            p.Footer = "CarRent System";
            p.FooterSpacing = 15;
            p.PrintDataGridView(dataGridView1);
        }

        private void Bienlai_Load(object sender, EventArgs e)
        {
            comboBoxcarno.DataSource = db.tempThues.ToList();
            comboBoxcarno.ValueMember = "Ten_KH";
            comboBoxcarno.DisplayMember = "Ten_KH";
        }

        private void comboBoxcarno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                tempThue obj = comboBoxcarno.SelectedItem as tempThue;
                if (obj != null)
                {
                    labeldname.Text = obj.Ten_KH.ToString();
                    labeltype.Text = obj.Chon_Xe.ToString();
                    labelentrytime.Text = obj.Tu_Ngay.ToString();
                    labelamount.Text = obj.Thanh_Tien.ToString();
                    labelcarno.Text = obj.CCCD.ToString();
                    labelday.Text = obj.Thoi_Gian.ToString();

                }

                Cursor.Current = Cursors.Default;
                var chk = db.tempThues.Where(o => o.Ten_KH == comboBoxcarno.Text);
                if (chk != null)
                {
                    dataGridView1.DataSource = chk;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WelcomeScreen w = new WelcomeScreen();
            w.Show();
            this.Hide();
        }
    }
}
