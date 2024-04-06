using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Renting_System
{
    public partial class Baocao : Form
    {
        public Baocao()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            try
            {


                string strConnection = System.Configuration.ConfigurationSettings.AppSettings["Rent"].ToString();

                string strCommand = "select * from tempThue";
                SqlConnection myConnection = new SqlConnection(strConnection);
                myConnection.Open();
                
                SqlCommand myCommand = new SqlCommand(strCommand, myConnection);
                SqlDataAdapter da = new SqlDataAdapter(myCommand);

                //Nhận dữ liệu từ data set
                DataSet ds = new DataSet();
                da.Fill(ds, "Car_Rent");
                ds.WriteXml("Renting.xml");

                //Đưa lên Report và View
                rptBaocao rpt = new rptBaocao();

                rpt.SetDataSource(ds);
                this.crystalReportViewer2.ReportSource = rpt;
                myConnection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra, " + ex.Message.ToString(), "Có lỗi");
            }
        }

        private void Baocao_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WelcomeScreen w = new WelcomeScreen();
            w.Show();
            this.Hide();
        }
    }
}
