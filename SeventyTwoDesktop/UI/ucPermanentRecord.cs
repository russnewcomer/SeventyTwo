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
    public partial class ucPermanentRecord : UserControl
    {

        private ProfileController ProfileInfo { get; set; } = new ProfileController();

        public ucPermanentRecord()
        {
            InitializeComponent();
            ClearData();
        }

        public event EventHandler ProfileNameChange;

        public event EventHandler ProfileSaved;

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            ProfileInfo.Profile.name = txtName.Text;
            this.ProfileNameChange?.Invoke( this, e );
        }

        private void ucPermanentRecord_Load(object sender, EventArgs e)
        {

        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            //Naive update.
            SaveData();
            this.ProfileSaved?.Invoke( this, e );
            
        }

        public void LoadData( string guid ) {
            //ProfileInfo.loadProfileData(guid);
            ProfileInfo.LoadProfileData("4eea8a97-38bd-478a-9f6b-4b963ee779ce");
            txtName.Text = ProfileInfo.Profile.name;
            txtNumber.Text = ProfileInfo.Profile.number;
            txtSpouse.Text = ProfileInfo.Profile.spouse;
            txtAddress.Text = ProfileInfo.Profile.address;
            txtPhone.Text = ProfileInfo.Profile.phonenumber;
            cbGender.SelectedText = ProfileInfo.Profile.gender;
            dtpBirthDate.Value = ProfileInfo.Profile.birthdate;
        }
        public void SaveData() {
            ProfileInfo.Profile.name = txtName.Text;
            ProfileInfo.Profile.number = txtNumber.Text;
            ProfileInfo.Profile.spouse = txtSpouse.Text;
            ProfileInfo.Profile.address = txtAddress.Text;
            ProfileInfo.Profile.phonenumber = txtPhone.Text;
            ProfileInfo.Profile.gender = cbGender.ValueMember.ToString();
            ProfileInfo.Profile.birthdate = dtpBirthDate.Value;
            ProfileInfo.saveProfileData();
        }
        public void ClearData()
        {
            ProfileInfo.InitializeProfile();

            ProfileInfo.Profile.name = "";
            ProfileInfo.Profile.number = "";
            ProfileInfo.Profile.spouse = "";
            ProfileInfo.Profile.address = "";
            ProfileInfo.Profile.phonenumber = "";
            ProfileInfo.Profile.gender = "";
            ProfileInfo.Profile.birthdate = DateTime.Now;
        }
        public string GetProfileName( )
        {
            return ProfileInfo.Profile.name;
        } 
    }
}
