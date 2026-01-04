using DVLD.Classes;
using DVLD_Version_3_Business;
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

namespace DVLD_Version_3
{
    public partial class frmAddUpdatePerson : Form
    {

        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew = 0, Update = 1};
        public enum enGender { Male = 0, Female = 1};

        private enMode _Mode ;
        private enGender _Gender ;
        private int _PersonID;
        clsPerson _Person;

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
           
        }

       public frmAddUpdatePerson(int PersonID)
       {
            InitializeComponent();
            _PersonID = PersonID;
            _Mode = enMode.Update;
       }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach(DataRow row in dtCountries.Rows )
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        
        }

        private void _ResetDefaultValues()
        {
            _FillCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }

            else
            {
                lblTitle.Text = "Update Person";
            }

            if (rbMale.Checked)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else
                pbPersonImage.Image = Properties.Resources.Female_512;

            // hide / show the link label Remove if the image is not exists;
            llRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            // Set maximum allowed date of birth (user must be at least 18 years old)
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            // Default country is Morocco
            cbCountries.SelectedIndex = cbCountries.FindString("Morocco");

            txbFirstName.Text = "";
            txbSecondName.Text = "";
            txbThirdName.Text = "";
            txbLastName.Text = "";
            txbNationalNo.Text = "";
            txbPhone.Text = "";
            txbEmail.Text = "";
            txbAddress.Text = "";
            rbMale.Checked = true;


        }

        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("Person with ID = [" + _PersonID + "] Not found , this window will close");
                this.Close();
                return; 
            }


            lblPersonID.Text = _Person.PersonID.ToString();
            txbFirstName.Text = _Person.FirstName;
            txbSecondName.Text = _Person.SecondName;
            txbThirdName.Text = _Person.ThirdName;
            txbLastName.Text = _Person.LastName;
            txbNationalNo.Text = _Person.NationalNo;
            txbEmail.Text = _Person.Email;
            txbPhone.Text = _Person.Phone;
            txbAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            cbCountries.SelectedIndex = cbCountries.FindString(_Person.CountryInfo.CountryName);

            if (_Person.ImagePath != "")
                pbPersonImage.ImageLocation = _Person.ImagePath;

            if (_Person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            llRemoveImage.Visible = (_Person.ImagePath != "");


        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if(_Mode == enMode.Update)
                _LoadData();
        }

        private bool _HandlePersonImage()
        { 
          //this procedure will handle the person image,
          //it will take care of deleting the old image from the folder
          //in case the image changed. and it will rename the new image with GUID and 
          // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbPersonImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid !, put the mouse over the red icon(s)");
                return;
            }

            if (!_HandlePersonImage()) // you need to understand it deeply
                return;


            int Nationality_Country_ID = clsCountry.Find(cbCountries.Text).CountryID;

            _Person.FirstName = txbFirstName.Text.Trim();
            _Person.SecondName = txbSecondName.Text.Trim();
            _Person.ThirdName = txbThirdName.Text.Trim();
            _Person.LastName = txbLastName.Text.Trim();
            _Person.NationalNo  = txbNationalNo.Text.Trim();
            _Person.Email  = txbEmail.Text.Trim();
            _Person.Phone  = txbPhone.Text.Trim();
            _Person.Address  = txbAddress.Text.Trim();
            _Person.DateOfBirth  = dtpDateOfBirth.Value;


            if (rbMale.Checked)
                _Person.Gender = (short) enGender.Male;
            else
                _Person.Gender = (short) enGender.Female;

            _Person.NationalityCountryID = Nationality_Country_ID;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImagePath = pbPersonImage.ImageLocation;
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                lblTitle.Text = "Update Person";
                _Mode = enMode.Update;

                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _Person.PersonID);
            
            }

            else    
                MessageBox.Show("Error : Data was not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      

        }

        private void txbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txbNationalNo,null);
            }

            // Make sure the NationalNo is not used for another person
            if (txbNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExist(txbNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbNationalNo, "This NationalNo is used for another person !");
                
            }
            else
            {
                errorProvider1.SetError(txbNationalNo,null);
            }


        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Properties.Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Properties.Resources.Female_512;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true ;//why 

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedFile = openFileDialog1.FileName;
                pbPersonImage.Load(SelectedFile);
                llRemoveImage.Visible = true; 
            }

        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else
                pbPersonImage.Image = Properties.Resources.Female_512;

            llRemoveImage.Visible = false;

        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void txbEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email in case it's empty.
            if (txbEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidation.ValidateEmail(txbEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txbEmail, null);
            }
            ;

        }

        private void txbFirstName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void txbSecondName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void txbLastName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }
    }





}
