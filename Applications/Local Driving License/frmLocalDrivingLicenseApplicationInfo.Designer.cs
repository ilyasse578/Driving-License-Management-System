namespace DVLD_Version_3.Applications.Local_Driving_License
{
    partial class frmLocalDrivingLicenseApplicationInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.ctrlDrivingLicenseApplicationInfo2 = new DVLD_Version_3.Applications.Local_Driving_License.ctrlDrivingLicenseApplicationInfo();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::DVLD_Version_3.Properties.Resources.Close_32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(811, 376);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrlDrivingLicenseApplicationInfo2
            // 
            this.ctrlDrivingLicenseApplicationInfo2.Location = new System.Drawing.Point(3, 12);
            this.ctrlDrivingLicenseApplicationInfo2.Name = "ctrlDrivingLicenseApplicationInfo2";
            this.ctrlDrivingLicenseApplicationInfo2.Size = new System.Drawing.Size(907, 377);
            this.ctrlDrivingLicenseApplicationInfo2.TabIndex = 0;
            this.ctrlDrivingLicenseApplicationInfo2.Load += new System.EventHandler(this.ctrlDrivingLicenseApplicationInfo2_Load);
            // 
            // frmLocalDrivingLicenseApplicationInfo
            // 
            this.ClientSize = new System.Drawing.Size(918, 430);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ctrlDrivingLicenseApplicationInfo2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLocalDrivingLicenseApplicationInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Driving License Application";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplicationInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlDrivingLicenseApplicationInfo ctrlDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.Button btnClose;
        private ctrlDrivingLicenseApplicationInfo ctrlDrivingLicenseApplicationInfo2;
        private System.Windows.Forms.Button button1;
    }
}