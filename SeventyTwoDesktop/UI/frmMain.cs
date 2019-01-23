﻿using System;
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

        private ucTemplateItem ActiveGuidanceItem { get; set; }

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
                Height = 300,
                Width = 600
            };



            TreeView tvTemplateItems = new TreeView {
                Top = 55,
                Left = 605,
                Width = 325,
                Height = 400,
                Font = new Font( "Segoe UI", 9 ),
            };
            Dictionary<string, int> dStrIntNodeIndex = new Dictionary<string, int>( );

            Panel pnlGuidanceControls = new Panel {
                Top = 50,
                Height = 200,
                Left = 5,
                Width = 550
            };




            tabPageToCreate.Controls.Add(permRecordControl);
            permRecordControl.ProfileNameChange += delegate ( object o, EventArgs e ) {
                string profileName = permRecordControl.GetProfileName( );
                profileName = ( profileName != "" ) ? profileName : "New Profile";
                tabPageToCreate.Text = profileName;
                lblProfileName.Text = profileName;
            };

            permRecordControl.ProfileSaved += delegate ( object o, EventArgs e ) {
                string profileName = permRecordControl.GetProfileName( );
                profileName = ( profileName != "" ) ? profileName : "New Profile";
                tabPageToCreate.Text = profileName;
                lblProfileName.Text = profileName;
                permRecordControl.Hide( );
                pnlGuidanceControls.Show( );
            };


            lblProfileName.Click += delegate ( object o, EventArgs e ) {
                permRecordControl.Show( );
                pnlGuidanceControls.Hide( );
            };

            Button btnEditProfile = new Button { Left = 400, Top = 4, Height = 30, Width = 150, Text = "Edit Profile" };
            btnEditProfile.Click += delegate ( object o, EventArgs e ) {
                permRecordControl.Show( );
                pnlGuidanceControls.Hide( );
            };

            tabPageToCreate.Controls.Add( btnEditProfile );
            tabPageToCreate.Controls.Add( lblProfileName );

            Button btnPreviousGuidanceItem = new Button { Left = 15, Top = 250, Height = 30, Width = 150, Text = "Previous", Visible = false };
            btnPreviousGuidanceItem.Click += delegate ( object o, EventArgs e ) {
                //Go previous
            };

            Button btnNextGuidanceItem = new Button { Left = 225, Top = 250, Height = 30, Width = 150, Text = "Next", Visible = false };
            btnNextGuidanceItem.Click += delegate ( object o, EventArgs e ) {
                //Go Next
            };

            tabPageToCreate.Controls.Add( btnPreviousGuidanceItem );
            tabPageToCreate.Controls.Add( btnNextGuidanceItem );

            TemplateController tmp = new TemplateController( "pregnancy_permanent" );
            Dictionary<string, TemplateItem> tilist = tmp.GetTemplateItems( );
            

            foreach( KeyValuePair<string, TemplateItem> ti in tilist ) {

                ucTemplateItem guidanceItem = new ucTemplateItem {
                    OutlineMode = false,
                    Visible = false,
                    Name = "ucti" + ti.Value.Name
                };
                guidanceItem.ItemValueChanged += delegate ( object o, EventArgs e ) {
                    //MessageBox.Show( guidanceItem.ItemValue );
                    string displayText = guidanceItem.ItemValue;
                    //Terning right around... to show yes/no instead of True/False
                    displayText = ( displayText == "true" ) ? "Yes" : ( displayText == "False" ) ? "No" : displayText;
                    tvTemplateItems.Nodes[ dStrIntNodeIndex[ ti.Value.Group ] ].Nodes[ guidanceItem.ItemName ].Text = ti.Value.Title + " - " + displayText;
                };

                guidanceItem.LoadTemplateItem( ti.Value );

                pnlGuidanceControls.Controls.Add( guidanceItem );


                if( !dStrIntNodeIndex.ContainsKey( ti.Value.Group ) ) {
                    tvTemplateItems.Nodes.Add( tmp.GetGroupDisplayName( ti.Value.Group ) );
                    dStrIntNodeIndex.Add( ti.Value.Group, tvTemplateItems.Nodes.Count - 1 );
                }
                tvTemplateItems.Nodes[ dStrIntNodeIndex[ ti.Value.Group ] ].Nodes.Add( ti.Value.Name, ti.Value.Title );



            }
            

            tabPageToCreate.Controls.Add( tvTemplateItems );

            tabPageToCreate.Controls.Add( pnlGuidanceControls );




            tabPageToCreate.Name = guidToLoad;
            tabPageToCreate.Text = "New Profile";


            tvTemplateItems.NodeMouseClick += delegate ( object o, TreeNodeMouseClickEventArgs e ) {
                if( e.Node.Name != "" ) {
                    MessageBox.Show( e.Node.Name );
                    EnableGuidanceItem( e.Node.Name, pnlGuidanceControls, btnPreviousGuidanceItem, btnNextGuidanceItem );
                }
            };

            return tabPageToCreate;
        }


        private void EnableGuidanceItem( string ControlKey, Panel CurrentPanel, Button Next, Button Previous ) {
            
            ActiveGuidanceItem?.Hide( );
            Previous.Show( );
            Next.Show( );

            foreach( ucTemplateItem ctl in CurrentPanel.Controls.Find( "ucti" + ControlKey, false ) ) {
                ActiveGuidanceItem = ctl;
                ActiveGuidanceItem.Show( );
            }
        }
    }
}
