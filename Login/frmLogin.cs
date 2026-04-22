using DVLD.Classes;
using DVLD_Version_3.Global_Classes;
using DVLD_Version_3_Business;
using DVLD_Version_3_DataAccess;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
             string hashedPassword = clsHash.ComputeHash(txtPassword.Text.Trim());
             //clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), hashedPassword);

            if (user != null)
            {

                if (chkRememberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUserNameAndPasswordInRegistry(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                }
                else
                {
                    //store empty username and password
                    clsGlobal.RememberUserNameAndPasswordInRegistry("", "");

                }

                //incase the user is not active
                if (!user.IsActive)
                {

                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();


            }
            else
            {
                txtUserName.Focus();
                txtUserName.Clear();
                txtPassword.Clear();
                
                
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string Username = "", Password = "";
            if (clsGlobal.GetSortedCredentialFromRegistry(ref Username, ref Password))
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
