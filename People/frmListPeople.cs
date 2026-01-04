using DVLD_Version_3.People;
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
    public partial class frmListPeople : Form
    {


        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        //only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "GenderCaption", "DateOfBirth", "CountryName",
                                                         "Phone", "Email");

        private void _RefreshPeoplList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GenderCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvShowPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvShowPeople.Rows.Count.ToString();
        }

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvShowPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0; // it is empty 
            lblRecordsCount.Text = dgvShowPeople.Rows.Count.ToString();
            if (dgvShowPeople.Rows.Count > 0)
            {

                dgvShowPeople.Columns[0].HeaderText = "Person ID";
                dgvShowPeople.Columns[0].Width = 90;

                dgvShowPeople.Columns[1].HeaderText = "National No.";
                dgvShowPeople.Columns[1].Width = 95;


                dgvShowPeople.Columns[2].HeaderText = "First Name";
                dgvShowPeople.Columns[2].Width = 90;

                dgvShowPeople.Columns[3].HeaderText = "Second Name";
                dgvShowPeople.Columns[3].Width = 90;


                dgvShowPeople.Columns[4].HeaderText = "Third Name";
                dgvShowPeople.Columns[4].Width = 90;

                dgvShowPeople.Columns[5].HeaderText = "Last Name";
                dgvShowPeople.Columns[5].Width = 90;

                dgvShowPeople.Columns[6].HeaderText = "Gender";
                dgvShowPeople.Columns[6].Width = 70;

                dgvShowPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvShowPeople.Columns[7].Width = 120;

                dgvShowPeople.Columns[8].HeaderText = "Nationality";
                dgvShowPeople.Columns[8].Width = 85;


                dgvShowPeople.Columns[9].HeaderText = "Phone";
                dgvShowPeople.Columns[9].Width = 100;


                dgvShowPeople.Columns[10].HeaderText = "Email";
                dgvShowPeople.Columns[10].Width = 170;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void editPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvShowPeople.CurrentRow.Cells[0].Value);
            frmAddUpdatePerson frm = new frmAddUpdatePerson(PersonID);
            frm.ShowDialog();
            _RefreshPeoplList();

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeoplList();

        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            // Delete Person
            int PersonID = Convert.ToInt32(dgvShowPeople.CurrentRow.Cells[0].Value);

           clsPerson person = clsPerson.Find(PersonID);

            if (person != null)
            {
                if (person.Delete(PersonID))
                    MessageBox.Show("Person deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Person was not deleted due to linked Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            _RefreshPeoplList();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvShowPeople.CurrentRow.Cells[0].Value);

            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();

            _RefreshPeoplList();
        }

        private void txbFilterBy_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txbFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvShowPeople.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txbFilterBy.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txbFilterBy.Text.Trim());

            lblRecordsCount.Text = dgvShowPeople.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbFilterBy.Visible = (cbFilterBy.Text != "None");

            if (txbFilterBy.Visible)
            {
                txbFilterBy.Text = "";
                txbFilterBy.Focus();
            }
        }

        private void txbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number in case person id is selected.
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }



    }
}
