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
            string guidToLoad = CreateNewProfile();
            tabMain.TabPages.Add( CreateProfileTab( guidToLoad));
        }

        private string CreateNewProfile( )
        {
            string currentGUID = "";

            ProfileController np = new ProfileController();
            currentGUID = np.InitializeProfile();
            Console.WriteLine(currentGUID);

            return currentGUID;
        }

        private TabPage CreateProfileTab( string guidToLoad ) {
            TabPage tabPageToCreate = new TabPage();


            Label lblProfileName = new Label {
                Top = 4,
                Left = 4,
                Width = 300,
                Text = "Profile",
                AutoSize = true,
                Font = new Font( "Segoe UI", 20 ),
                TextAlign = ContentAlignment.MiddleCenter
            };

            ucPermanentRecord permRecordControl = new ucPermanentRecord
            {
                Top = 5,
                Left = 5,
                Height = 220,
                Width = 600, Visible = false
            };

            tabPageToCreate.Controls.Add(permRecordControl);
            permRecordControl.ProfileNameChange += delegate (object o, EventArgs e)
            {
                tabPageToCreate.Text = permRecordControl.GetProfileName();
                lblProfileName.Text = permRecordControl.GetProfileName( );
            };

            permRecordControl.ProfileSaved += delegate ( object o, EventArgs e ) {
                tabPageToCreate.Text = permRecordControl.GetProfileName( );
                lblProfileName.Text = permRecordControl.GetProfileName( );
                permRecordControl.Hide( );
            };


            lblProfileName.Click += delegate ( Object o, EventArgs e ) {
                permRecordControl.Show( );
                
            };

            Button btnEditProfile = new Button { Left = 400, Top = 4, Height = 30, Width = 150, Text = "Edit Profile" };
            btnEditProfile.Click += delegate ( Object o, EventArgs e ) {
                permRecordControl.Show( );
            };

            tabPageToCreate.Controls.Add( btnEditProfile );
            tabPageToCreate.Controls.Add( lblProfileName );


            ScrollableControl scPanel = new ScrollableControl {
                Top = 5,
                Left = 605,
                Width = 250,
                Height = 420,
                AutoScroll = true
                
            };

            Panel pnlGuidanceControls = new Panel {
                Top = 250,
                Height = 220,
                Left = 5,
                Width = 400
            };
            

            
            TemplateController tmp = new TemplateController( "maternal_antenatal_visit" );
            Dictionary<string, TemplateItem> tilist = tmp.GetTemplateItems( );
            
            int countOfItems = 0;
            foreach( KeyValuePair<string, TemplateItem> ti in tilist ) {
                ucTemplateItem outlineItem = new ucTemplateItem( );
                outlineItem.OutlineMode = true;
                outlineItem.LoadTemplateItem( ti.Value );
                outlineItem.Left = 0;
                //I'm doing some non-obvious postfix incrementing here 
                //because I want to write a long comment explaining it instead of having another line of code.
                outlineItem.Top = ( countOfItems++ * 60 );

                outlineItem.OutlineModeClick += delegate ( object o, EventArgs e ) {
                    TemplateItemEventArgs  args = ( TemplateItemEventArgs  )e;
                    foreach ( ucTemplateItem ctl in pnlGuidanceControls.Controls ) {
                        if ( ctl.Visible ) {
                            //save this 
                            //ctl.ItemValue
                        }
                        ctl.Visible = ctl.ItemName == args.Key;
                    }
                };
                scPanel.Controls.Add( outlineItem );

                ucTemplateItem guidanceItem = new ucTemplateItem {
                    OutlineMode = false,
                    Visible = false
                };
                guidanceItem.ItemValueChanged += delegate ( object o, EventArgs e ) {
                    MessageBox.Show( guidanceItem.ItemValue );
                };
                guidanceItem.LoadTemplateItem( ti.Value );

                pnlGuidanceControls.Controls.Add( guidanceItem );

            }
            
            tabPageToCreate.Controls.Add( scPanel );

            tabPageToCreate.Controls.Add( pnlGuidanceControls );




            tabPageToCreate.Name = guidToLoad;
            tabPageToCreate.Text = "New Profile";

            return tabPageToCreate;
        }

    }
}
