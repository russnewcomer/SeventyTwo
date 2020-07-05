using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeventyTwoDesktop.Controllers;

namespace SeventyTwoDesktop
{
    public partial class CtlPermanentRecord : UserControl
    {

        public ProfileController Profile { get; set; } = new ProfileController();

        public CtlPermanentRecord()
        {
            InitializeComponent();
            ClearData();
        }

        public event EventHandler ProfileNameChange;

        public event EventHandler ProfileSaved;

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            Profile.Profile.name = txtName.Text;
            this.ProfileNameChange?.Invoke( this, e );
        }

        private void CtlPermanentRecord_Load(object sender, EventArgs e)
        {

        }

        private void BtnSaveChanges_Click(object sender, EventArgs e)
        {
            //Naive update.
            SaveData();
            this.ProfileSaved?.Invoke( this, e );
            
        }

        public void LoadData( string guid ) {
            if( Profile == null ) {
                Profile.LoadProfileData( guid );
            }
            txtName.Text = Profile.Profile.name;
            txtNumber.Text = Profile.Profile.number;
            txtAddress.Text = Profile.Profile.address;
            txtCommunity.Text = Profile.Profile.community;
            txtLocation.Text = Profile.Profile.location;
            txtPhone.Text = Profile.Profile.phonenumber;
            cbGender.SelectedItem = Profile.Profile.gender;
            dtpBirthDate.Value = Profile.Profile.birthdate;
        }
        public void SaveData() {
            Profile.Profile.name = txtName.Text;
            Profile.Profile.number = txtNumber.Text;
            Profile.Profile.address = txtAddress.Text;
            Profile.Profile.community = txtCommunity.Text;
            Profile.Profile.location = txtLocation.Text;
            Profile.Profile.phonenumber = txtPhone.Text;
            Profile.Profile.gender = cbGender.SelectedItem?.ToString();
            Profile.Profile.birthdate = dtpBirthDate.Value;
            Profile.SaveProfileData();
        }
        public void ClearData()
        {
            Profile.InitializeProfile();

            Profile.Profile.name = "";
            Profile.Profile.number = "";
            Profile.Profile.address = "";
            Profile.Profile.community = "";
            Profile.Profile.location = "";
            Profile.Profile.phonenumber = "";
            Profile.Profile.gender = "";
            Profile.Profile.birthdate = DateTime.Now;
        }

        public string GetProfileName( ) {
            return Profile.Profile.name;
        }

        public string GetProfileNumber( )
        {
            return Profile.Profile.number;
        }
    }
}
