using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Renting_System
{
    public partial class Danhsachxe : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Danhsachxe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtbienso.Text) && !string.IsNullOrEmpty(txtname.Text))
                {
                    string sno = txtbienso.Text;
                    string slo = txtname.Text;
                    var chk = db.Xes.Where(o => o.Bien_So == sno && o.Ten_Xe == slo).FirstOrDefault();
                    if (chk == null)
                    {


                        Xe s = new Xe();
                        s.Ten_Xe = txtname.Text;
                        s.Bien_So = txtbienso.Text;
                        db.Xes.InsertOnSubmit(s);
                        db.SubmitChanges();
                        MessageBox.Show("Xe đã được thêm vào");
                        reset();
                        load();
                    }
                    else
                    {
                        MessageBox.Show("Xe được thêm trước đó rồi!","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập Bien_so hoặc Ten_Xe, yêu cầu nhập lại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }
        public void reset()
        {
            txtbienso.Text = "";
            txtname.Text = "";
            labelId.Text = "";
        }

        public void load()
        {
            var lod = db.Xes.ToList();
            dataGridView1.DataSource = lod;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelId.Text != null & txtbienso.Text != null & txtname.Text != null)
                {

                    int a = Convert.ToInt32(labelId.Text);
                    var s = db.Xes.Where(o => o.ID == a).FirstOrDefault();
                    if (MessageBox.Show("Bạn có muốn sửa bản ghi " +s.ID+ " không?", "Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        
                        s.Ten_Xe = txtname.Text;
                        s.Bien_So = txtbienso.Text;

                        db.SubmitChanges();
                        MessageBox.Show("Sửa thành công");
                        reset();
                        load();
                    }

                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập mã vị trí hoặc vị trí, yêu cầu nhập lại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int ir = e.RowIndex;
            labelId.Text = dataGridView1.Rows[ir].Cells[0].Value.ToString();
            txtbienso.Text = dataGridView1.Rows[ir].Cells[1].Value.ToString();
            txtname.Text = dataGridView1.Rows[ir].Cells[2].Value.ToString();
        }

        private void Danhsachxe_Load(object sender, EventArgs e)
        {
            load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelId.Text != null)
                {
                    int st = Convert.ToInt32(labelId.Text);
                    var s = db.Xes.Where(o => o.ID == st).FirstOrDefault();
                    if (MessageBox.Show("Bạn có muốn xóa bản ghi " +s.ID+ " không", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        

                        db.Xes.DeleteOnSubmit(s);
                        db.SubmitChanges();
                        MessageBox.Show("Xóa thành công");
                        reset();
                        load();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi, yêu cầu chọn và nhấn XÓA!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void textsearch_TextChanged(object sender, EventArgs e)
        {
            if (textsearch.Text == "")
            {
                load();
            }
            else
            {
                searchdata();
            }
        }
        public void searchdata()
        {
            try
            {
                if (textsearch.Text != null)
                {
                    string sk = textsearch.Text;
                    var chk = db.Xes.Where(o => o.Ten_Xe.Contains(sk) || o.Bien_So.Contains(sk)).ToList();
                    if (chk != null)
                    {
                        dataGridView1.DataSource = chk;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WelcomeScreen w = new WelcomeScreen();
            w.Show();
            this.Hide();
        }
    }
}
