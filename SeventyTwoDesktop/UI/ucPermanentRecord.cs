using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeventyTwoDesktop
{
    public partial class ucPermanentRecord : UserControl
    {

        private PatientController PatientInfo { get; set; } = new PatientController();

        public ucPermanentRecord()
        {
            InitializeComponent();
            clearData();
        }

        public event EventHandler PatientNameChange;

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            PatientInfo.Patient.name = txtName.Text;
            if (this.PatientNameChange != null)
            {
                this.PatientNameChange(this, e);
            }   
        }

        private void ucPermanentRecord_Load(object sender, EventArgs e)
        {

        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            //Naive update.
            saveData();
        }

        public void loadData( string guid ) {
            //PatientInfo.loadPatientData(guid);
            PatientInfo.loadPatientData("4eea8a97-38bd-478a-9f6b-4b963ee779ce");
            txtName.Text = PatientInfo.Patient.name;
            txtNumber.Text = PatientInfo.Patient.number;
            txtSpouse.Text = PatientInfo.Patient.spouse;
            txtAddress.Text = PatientInfo.Patient.address;
            txtPhone.Text = PatientInfo.Patient.phonenumber;
            cbGender.SelectedText = PatientInfo.Patient.gender;
            dtpBirthDate.Value = PatientInfo.Patient.birthdate;
        }
        public void saveData() {
            PatientInfo.Patient.name = txtName.Text;
            PatientInfo.Patient.number = txtNumber.Text;
            PatientInfo.Patient.spouse = txtSpouse.Text;
            PatientInfo.Patient.address = txtAddress.Text;
            PatientInfo.Patient.phonenumber = txtPhone.Text;
            PatientInfo.Patient.gender = cbGender.ValueMember.ToString();
            PatientInfo.Patient.birthdate = dtpBirthDate.Value;
            PatientInfo.savePatientData();
        }
        public void clearData()
        {
            PatientInfo.InitializePatient();

            PatientInfo.Patient.name = "";
            PatientInfo.Patient.number = "";
            PatientInfo.Patient.spouse = "";
            PatientInfo.Patient.address = "";
            PatientInfo.Patient.phonenumber = "";
            PatientInfo.Patient.gender = "";
            PatientInfo.Patient.birthdate = DateTime.Now;
        }
        public string GetPatName()
        {
            return PatientInfo.Patient.name;
        } 
    }
}
