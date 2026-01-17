using DVLD_Version_3.Global_Classes;
using DVLD_Version_3_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Version_3
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());


            if (User != null)
            {
                if (chkRememberMe.Checked)
                {
                    //store the username and password
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                }

                else
                {
                    //store empty username and password
                    clsGlobal.RememberUsernameAndPassword("", "");

                }

                //in case the use is not active
                if(!User.IsActive)
                {
                    txtUserName.Focus();

                    MessageBox.Show("Your account is not active contact your admin", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;

                }
                clsGlobal.CurrentUser = User;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();



            }


            else 
            {
                MessageBox.Show("UserName or password incorrect !!!", "Error credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtUserName.Focus();
            }


        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string Username = "", Password = "";
            if (clsGlobal.GetStoredCredential(ref Username, ref Password))
            {
                txtUserName.Text = Username;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }

            else
                chkRememberMe.Checked = false;



        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
