using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
        

            String query = "select * from UserCredential where UserName='"+txtUserName.Text+"' and Password='"+txtPass.Text+"'";
            DataTable dt = DataAccess.GetData(query);
            if (dt==null)
            return;

            if (dt.Rows.Count!=1)
            {
                MessageBox.Show("Invalid UserName or Password");
                return;
            }
            else
            {
                Dashboard d = new Dashboard();
                d.Show();
                this.Hide();
            }
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (txtPass.PasswordChar == '*')
            {
                chkShow.BringToFront();
                txtPass.PasswordChar = '\0';
            }
            else if (txtPass.PasswordChar == '\0')
            {
                chkShow.BringToFront();
                txtPass.PasswordChar = '*';
            }
        }
    }
}
