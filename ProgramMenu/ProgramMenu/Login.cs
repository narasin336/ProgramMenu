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
    public partial class Login : Form
    {
       // SqlConnection conn = new SqlConnection("Server=192.168.4.4;Database=PST_SUBDATA;User Id=sa;Password=p@ssw0rd;MultipleActiveResultSets=True");
        //SqlConnection conn = new SqlConnection("Server=" + MyGlobal.GlobalServer + ";Database=" + MyGlobal.GlobalDataBase + ";User Id= '" + MyGlobal.GlobalDataBaseUserID + "';Password= '" + MyGlobal.GlobalDataBasePassword + "';MultipleActiveResultSets=True");
        private OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source = |DataDirectory|\\Database.accdb");

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            conn.Open();
        }

        private void CheckError()
        {
            string sql = " Select * From UserMaster where USERID='" + txtUserID.Text + "' and Password='" + txtPassword.Text + "' ";
            OleDbCommand com = new OleDbCommand(sql, conn);
            OleDbDataReader dr = com.ExecuteReader();
            int checkCount = 0;

            while (dr.Read())
            {
                MyGlobal.GlobalUserID = dr[0].ToString();
                MyGlobal.GlobalUserName = dr[1].ToString();
                MyGlobal.GlobalSection = dr[3].ToString();
                MyGlobal.GlobalUserAuthority = dr[4].ToString();
                MyGlobal.GlobalPosition = dr[5].ToString();
                checkCount = +1;
            }
            dr.Close();

            if (checkCount == 0)
            {
                MessageBox.Show("Invalid User and Password");
            }
            else
            {

                this.Hide();
                ProgramMenu SApplication = new ProgramMenu();
                SApplication.ShowDialog();
                Application.Exit();

            }
        }      

        private void bttChangePass_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChangePassword SApplication = new ChangePassword();
            SApplication.ShowDialog();
            Application.Exit();
        }

        private void bttLogin_Click_1(object sender, EventArgs e)
        {
            CheckError();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
               this.bttLogin_Click_1(sender, e);
            }
        }

        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.bttLogin_Click_1(sender, e);
            }
        }
    }
}
