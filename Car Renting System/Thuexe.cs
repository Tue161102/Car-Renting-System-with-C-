using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Renting_System
{
    public partial class Thuexe : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Thuexe()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelId.Text != null & txtdriver.Text != null & txtcccd.Text != null & txtrtime.Text != null & checkedListBox1.Text != null & comboBox1.Text != null)
                {
                    int st = Convert.ToInt32(labelId.Text);
                    var s = db.Thues.Where(o => o.ID == st).FirstOrDefault();
                    if (MessageBox.Show("Bạn có muốn sửa bản ghi " +s.ID+ " không?", "Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {

                        s.Ten_KH = txtdriver.Text;
                        s.CCCD = txtcccd.Text;
                        s.Chon_Xe = checkedListBox1.Text;
                        s.Thoi_Gian = int.Parse(txtrtime.Text);
                        s.Chon_Xe = comboBox1.Text;
                        s.Tu_Ngay = DateTime.Now;
                        int str = int.Parse(textpay.Text);
                        int amt = int.Parse(txtrtime.Text);
                        int amttotal = amt * str;
                        s.Thanh_Tien = amttotal;

                        var a = db.tempThues.Where(o => o.ID == st).FirstOrDefault();
                        a.Ten_KH = txtdriver.Text;
                        a.CCCD = txtcccd.Text;
                        a.Chon_Xe = checkedListBox1.Text;
                        a.Thoi_Gian = int.Parse(txtrtime.Text);
                        a.Chon_Xe = comboBox1.Text;
                        a.Thanh_Tien = amttotal;
                      
                        db.SubmitChanges();
                        MessageBox.Show("Sửa thành công");

                        load();

                    }

                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void Thuexe_Load(object sender, EventArgs e)
        {
            load();

            comboBox1.DataSource = db.Xes.Select(x => $"{x.Bien_So} - {x.Ten_Xe}").ToList();
            comboBox1.DisplayMember = "DisplayText";
        }
        public void load()
        {
            var ld = db.Thues.ToList();
            dataGridView1.DataSource = ld;
            labelId.Text = "";
            txtdriver.Text = "";
            txtcccd.Text = "";
            txtrtime.Text = "";
            checkedListBox1.Text = "";
            textpay.Enabled = false;


            var total = db.Thues.Count();
            lbltotal.Text = total.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtdriver.Text != null && txtcccd.Text != null && txtrtime.Text != null && checkedListBox1.Text != null && comboBox1.Text != null)
                {
                    string selectedCar = comboBox1.Text;

                    // Kiểm tra xem giá trị đã tồn tại trong DataGridView chưa
                    bool isSlotExist = dataGridView1.Rows.Cast<DataGridViewRow>().Any(row => row.Cells["Chon_Xe"].Value != null && row.Cells["Chon_Xe"].Value.ToString() == selectedCar);

                    if (isSlotExist)
                    {
                        MessageBox.Show("Xe " + selectedCar.ToString() + " đã được thuê rồi! Mời chọn xe khác");

                    }
                    else
                    {
                        string sno = txtcccd.Text;
                        var chk = db.Thues.Where(o => o.CCCD == sno).FirstOrDefault();
                        if (chk == null)
                        {
                            Thue s = new Thue();
                            s.Ten_KH = txtdriver.Text;
                            s.CCCD = txtcccd.Text;
                            s.Phan_Loai = checkedListBox1.Text;
                            s.Thoi_Gian = int.Parse(txtrtime.Text);
                            s.Chon_Xe = selectedCar;
                            s.Tu_Ngay = DateTime.Now;
                            int str = int.Parse(textpay.Text);
                            int amt = int.Parse(txtrtime.Text);
                            int amttotal = amt * str;
                            s.Thanh_Tien = amttotal;

                            db.Thues.InsertOnSubmit(s);
                            db.SubmitChanges();
                            tempThue a = new tempThue();
                            a.Ten_KH = txtdriver.Text;
                            a.CCCD = txtcccd.Text;
                            a.Phan_Loai = checkedListBox1.Text;
                            a.Thoi_Gian = int.Parse(txtrtime.Text);
                            a.Chon_Xe = selectedCar;
                            a.Tu_Ngay = DateTime.Now;
                            a.Thanh_Tien = amttotal;
                            db.tempThues.InsertOnSubmit(a);
                            db.SubmitChanges();
                            MessageBox.Show("Thuê xe thành công!");

                            load();
                        }
                        else
                        {
                            MessageBox.Show("Người này đã thuê xe rồi!");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Có vẻ bạn chưa điền đủ thông tin rồi!");
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
            txtdriver.Text = dataGridView1.Rows[ir].Cells[1].Value.ToString();
            txtcccd.Text = dataGridView1.Rows[ir].Cells[2].Value.ToString();
            lblcarno.Text = dataGridView1.Rows[ir].Cells[5].Value.ToString();
            txtrtime.Text = dataGridView1.Rows[ir].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[ir].Cells[5].Value.ToString();
            checkedListBox1.Text = dataGridView1.Rows[ir].Cells[4].Value.ToString();
            lblarrivaltm.Text = dataGridView1.Rows[ir].Cells[6].Value.ToString();
            lblfee.Text = dataGridView1.Rows[ir].Cells[7].Value.ToString();
          
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thue obj = new Thue();

            string selectedOption = checkedListBox1.SelectedItem.ToString();

            obj.Phan_Loai = selectedOption;

            if (obj.Phan_Loai == "Theo giờ")
            {
                textpay.Text = "100000";
            }
            else if (obj.Phan_Loai == "Theo ngày")
            {
                textpay.Text = "800000";
            }
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelId.Text != null)
                {
                    int st = Convert.ToInt32(labelId.Text);
                    var s = db.Thues.Where(o => o.ID == st).FirstOrDefault();
                    if (MessageBox.Show("Xe " +s.Chon_Xe+ " đã được trả phải không", "Return", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        
                        db.Thues.DeleteOnSubmit(s);
                        db.SubmitChanges();
                       
                        MessageBox.Show("Trả xe thành công");

                        load();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chọn bản ghi, yêu cầu chọn và nhấn Trả Xe");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WelcomeScreen w = new WelcomeScreen();
            w.Show();
            this.Hide();
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

        private void searchdata()
        {
            try
            {
                if (textsearch.Text != null)
                {
                    string sk = textsearch.Text;
                    var chk = db.Thues.Where(o => o.Ten_KH.Contains(sk) || o.Chon_Xe.Contains(sk) || o.Phan_Loai.Contains(sk));
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
    }
}
