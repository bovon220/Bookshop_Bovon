using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Bookshop_Bovon
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection();

        private void frmLogin_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "server = .\\sqlexpress;" +
                "database = dbBookshop; integrated security = true";
            con.Open();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("ไม่ได้เข้าสู่ระบบภายใน 15 วินาที",
                "แจ้งเตือน",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            this.Close();   
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //ตรวจสอบการป้อนข้อมูล
            if(textUsername.Text == "" || textPassword.Text == "")
            {
                MessageBox.Show("ป้อนข้อมูลให้ครบ", "ผิดพลาด",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //นำ username กับ password ค้นในตาราง tbemployee
            //คำสั่ง SQL 
            string sql = "SELECT * FROM tbEmployee " +
            //    "WHERE username = '"+textUsername.Text+"' " +
            //    "AND password= '"+textPassword+"' ";
            "WHERE username =@username AND " +
            "password=@password";
            //ใช้คลาส sqlDataAdapter ส่งคำสั่ง sql ไปประมวลผล
            SqlDataAdapter da = new SqlDataAdapter(sql,con);
            da.SelectCommand.Parameters.AddWithValue("username", textUsername.Text);
            da.SelectCommand.Parameters.AddWithValue("password", textPassword.Text);
            //เก็บผลลัพธ์ ด้วยคลาส DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            //ตรวจสอบ จำนวนเเถวที่ได้
            if (dt.Rows.Count > 0)
                MessageBox.Show("พบข้อมูล" + dt.Rows.Count.ToString() + "รายการ");
            else 
                MessageBox.Show("ไม่พบข้อมูล");
        }
    }
}
