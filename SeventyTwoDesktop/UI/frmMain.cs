using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeventyTwoDesktop.Controllers;
using SeventyTwoDesktop.Models;

namespace SeventyTwoDesktop
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
        }

        private void btnNewPatient_Click(object sender, EventArgs e)
        {
            string guidToLoad = createNewPatient();
            tabMain.TabPages.Add(createPatientTab(guidToLoad));
        }

        private string createNewPatient()
        {
            string currentGUID = "";

            PatientController np = new PatientController();
            currentGUID = np.InitializePatient();
            Console.WriteLine(currentGUID);

            return currentGUID;
        }

        private TabPage createPatientTab(string guidToLoad)
        {
            TabPage tabPageToCreate = new TabPage();

            ucPermanentRecord permRecordControl = new ucPermanentRecord
            {
                Top = 5,
                Left = 5,
                Height = 220,
                Width = 600
            };

            tabPageToCreate.Controls.Add(permRecordControl);
            permRecordControl.PatientNameChange += delegate (object o, EventArgs e)
            {
                tabPageToCreate.Text = permRecordControl.GetPatName();
            };
            
            
            FlowLayoutPanel flowPanel = new FlowLayoutPanel {
                Top = 230,
                Left = 5,
                Width = 600,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                BackColor = Color.White
               
            };

            
            TemplateController tmp = new TemplateController( "maternal_antenatal_visit" );
            Dictionary<string, TemplateItem> tilist = tmp.GetTemplateItems( );

            foreach(KeyValuePair<string, TemplateItem> ti in tilist) {
                ucTemplateItem item = new ucTemplateItem( );
                item.LoadTemplateItem( ti.Value );
                flowPanel.Controls.Add( item );
            }
            
            tabPageToCreate.Controls.Add( flowPanel );


            tabPageToCreate.Name = guidToLoad;
            tabPageToCreate.Text = "New Patient";

            return tabPageToCreate;
        }

    }
}
