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
using System.Data.OleDb;

namespace ProgramMenu
{
    public partial class ChangePassword : Form
    {

       // SqlConnection conn = new SqlConnection("Server=192.168.4.4;Database=PST_SUBDATA;User Id=sa;Password=p@ssw0rd;MultipleActiveResultSets=True");
        //SqlConnection conn = new SqlConnection("Server=" + MyGlobal.GlobalServer + ";Database=" + MyGlobal.GlobalDataBase + ";User Id= '" + MyGlobal.GlobalDataBaseUserID + "';Password= '" + MyGlobal.GlobalDataBasePassword + "';MultipleActiveResultSets=True");
        private OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source = |DataDirectory|\\Database.accdb");

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            conn.Open();
        }

        private void CheckError()
        {
            string sql = " Select * FROM UserMaster where USERID = '" + txtUserID.Text + "' and Password='" + txtPassword.Text + "' ";
            OleDbCommand com = new OleDbCommand(sql, conn);
            OleDbDataReader dr = com.ExecuteReader();
            int checkCount = 0;

            while (dr.Read())
            {
                MyGlobal.GlobalUserID = dr[0].ToString();
                MyGlobal.GlobalUserName = dr[1].ToString();
                checkCount = +1;
            }
            dr.Close();

            if (checkCount == 0)
            {
                MessageBox.Show("Invalid User and Password");
            }
            else if (txtNewPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please input new password");
            }
            else if (txtConfirmPassword.Text.Trim() == "")
            {
                MessageBox.Show("Please input confirm password");
            }
            else if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Confirm password is not match");
            }
            else
            {
                sql = "Update UserMaster SET Password='" + txtNewPassword.Text + "' where UserID='" + txtUserID.Text + "' ";
                com = new OleDbCommand(sql, conn);
                com.ExecuteNonQuery();
                MessageBox.Show("Update completed");
                this.Hide();

                Login SApplication = new Login();
                SApplication.ShowDialog();
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckError();
        }
    }
}
