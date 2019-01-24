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

        private ucTemplateItem ActiveGuidanceItem { get; set; }
        private Dictionary<string, ProfileController> LoadedProfiles { get; set; } = new Dictionary<string, ProfileController>( );
        private List<KeyValuePair<string, string>> recordTypes = ProfileController.GetRecordTypes( );
        public frmMain() {
            InitializeComponent();
        }


        private void btnSearch_Click(object sender, EventArgs e) {
           
        }

        private void btnNewProfile_Click( object sender, EventArgs e ) {
            string guidToLoad = CreateNewProfile( );
            tabMain.TabPages.Add( CreateProfileTab( guidToLoad ) );
        }

        private string CreateNewProfile( ) {
            string currentGUID = "";

            try {
                ProfileController np = new ProfileController( );
                currentGUID = np.InitializeProfile( );
                LoadedProfiles.Add( currentGUID, np );

            } catch ( Exception exc ) { Log.writeToLog( exc ); }
            return currentGUID;
        }

        private void LoadProfile( string guidToLoad ) {
            LoadedProfiles.Add( guidToLoad, new ProfileController( guidToLoad ) );
        }

        private TabPage CreateProfileTab( string guidToLoad ) {

            //Controls
            TabPage tabPageToCreate = new TabPage { Name = guidToLoad, Text = LoadedProfiles[ guidToLoad ].Profile.name };

            Label lblProfileName = new Label {
                Top = 4,
                Left = 4,
                Width = 300,
                Text = "Profile",
                AutoSize = true,
                Font = new Font( "Segoe UI", 20 ),
                TextAlign = ContentAlignment.MiddleCenter
            };

            ucPermanentRecord permRecordControl = new ucPermanentRecord {
                Name = "permRecordControl",
                Top = 5,
                Left = 5,
                Height = 300,
                Width = 600
            };

            ComboBox cmbNewRecord = new ComboBox {
                Name = "cmbNewProfile",
                Top = 5,
                Left = 650,
                Width = 280,
                Height = 40
            };

            Button btnCreateNewRecord = new Button {
                Top = 5,
                Left = 605,
                Width = 40,
                Text = "New"
            };

            TreeView tvTemplateItems = new TreeView {
                Name = "tvTemplateItems",
                Top = 55,
                Left = 605,
                Width = 325,
                Height = 400,
                Font = new Font( "Segoe UI", 9 ),
            };


            Panel pnlGuidanceControls = new Panel {
                Name = "pnlGuidanceControls",
                Top = 50,
                Height = 200,
                Left = 5,
                Width = 550
            };


            Button btnEditProfile = new Button { Left = 400, Top = 4, Height = 30, Width = 150, Text = "Edit Profile" };
            Button btnPreviousGuidanceItem = new Button { Left = 15, Top = 250, Height = 30, Width = 150, Text = "Previous", Visible = false };
            Button btnNextGuidanceItem = new Button { Left = 225, Top = 250, Height = 30, Width = 150, Text = "Next", Visible = false };


            //Event Handlers

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

            btnEditProfile.Click += delegate ( object o, EventArgs e ) {
                permRecordControl.Show( );
                pnlGuidanceControls.Hide( );
            };


            btnPreviousGuidanceItem.Click += delegate ( object o, EventArgs e ) {
                //Go previous
                bool hasNode = false;
                //Check to see if we have a previous node we can go to.
                if( tvTemplateItems.SelectedNode.PrevNode == null && tvTemplateItems.SelectedNode.Parent.PrevNode != null ) {
                    tvTemplateItems.SelectedNode = tvTemplateItems.SelectedNode.Parent.PrevNode.LastNode;
                    hasNode = true;
                } else if ( tvTemplateItems.SelectedNode.PrevNode != null ) {
                    tvTemplateItems.SelectedNode = tvTemplateItems.SelectedNode.PrevNode;
                    hasNode = true;
                }
                //If we do have a node, move there.
                if ( hasNode ) {
                    EnableGuidanceItem( tvTemplateItems.SelectedNode.Name, pnlGuidanceControls, btnPreviousGuidanceItem, btnNextGuidanceItem );
                }
                //Check to see if we should show these.
                CheckPrevNextButtons( tvTemplateItems, btnNextGuidanceItem, btnPreviousGuidanceItem );
            };

            btnNextGuidanceItem.Click += delegate ( object o, EventArgs e ) {
                //Go Next
                bool hasNode = false;
                //Check to see if we have a previous node we can go to.
                if( tvTemplateItems.SelectedNode.NextNode == null && tvTemplateItems.SelectedNode.Parent.NextNode != null ) {
                    tvTemplateItems.SelectedNode = tvTemplateItems.SelectedNode.Parent.NextNode.FirstNode;
                    hasNode = true;
                } else if( tvTemplateItems.SelectedNode.NextNode != null ) {
                    tvTemplateItems.SelectedNode = tvTemplateItems.SelectedNode.NextNode;
                    hasNode = true;
                }
                //If we do have a node, move there.
                if( hasNode ) {
                    EnableGuidanceItem( tvTemplateItems.SelectedNode.Name, pnlGuidanceControls, btnPreviousGuidanceItem, btnNextGuidanceItem );
                }
                //Check to see if we should show these.
                CheckPrevNextButtons( tvTemplateItems, btnNextGuidanceItem, btnPreviousGuidanceItem );
            };
            
            btnCreateNewRecord.Click += delegate ( object o, EventArgs e ) {
                try {
                    string templateType = recordTypes.Find( x => x.Value == cmbNewRecord.SelectedItem.ToString( ) ).Key;
                    DialogResult result = MessageBox.Show( "Do you want to create a new " + cmbNewRecord.SelectedItem.ToString( ) + " record?", "Confirmation", MessageBoxButtons.YesNo );
                    if( result == DialogResult.Yes ) {
                        //If we confirm we want to create, create a new one

                        //First, find the item by the value in the KVPair
                        CreateNewRecordItem( templateType, tabPageToCreate );

                    } else {
                        //Otherwise, put the index back.
                    }
                } catch ( Exception exc ) { Log.writeToLog( exc ); }
            };

            tvTemplateItems.NodeMouseClick += delegate ( object o, TreeNodeMouseClickEventArgs e ) {
                if( e.Node.Name != "" ) {
                    //MessageBox.Show( e.Node.Name );
                    EnableGuidanceItem( e.Node.Name, pnlGuidanceControls, btnPreviousGuidanceItem, btnNextGuidanceItem );
                    CheckPrevNextButtons( tvTemplateItems, btnNextGuidanceItem, btnPreviousGuidanceItem );
                }
            };
            

            //Loading items
            cmbNewRecord.Items.Clear( );
            foreach( KeyValuePair<string, string> record in recordTypes ) {
                cmbNewRecord.Items.Add( record.Value );
            };

            //Adding items to the tab

            tabPageToCreate.Controls.Add( permRecordControl );
            tabPageToCreate.Controls.Add( btnEditProfile );
            tabPageToCreate.Controls.Add( lblProfileName );
            tabPageToCreate.Controls.Add( btnPreviousGuidanceItem );
            tabPageToCreate.Controls.Add( btnNextGuidanceItem );
            tabPageToCreate.Controls.Add( tvTemplateItems );
            tabPageToCreate.Controls.Add( pnlGuidanceControls );
            tabPageToCreate.Controls.Add( cmbNewRecord );
            tabPageToCreate.Controls.Add( btnCreateNewRecord );


            return tabPageToCreate;
        }

        private void CreateNewRecordItem( string recordType, TabPage curTabPage ) {
            try {

                //Get references to the controls.
                ucPermanentRecord permRecordControl = ( ucPermanentRecord )curTabPage.Controls.Find( "permRecordControl", false ).First<Control>( x=> true );
                TreeView tvTemplateItems = ( TreeView )curTabPage.Controls.Find( "tvTemplateItems", false ).First<Control>( x => true );
                Panel pnlGuidanceControls = ( Panel )curTabPage.Controls.Find( "pnlGuidanceControls", false ).First<Control>( x => true );

                //Get the GUID for the profile
                string profileGUID = curTabPage.Name;



                RecordController rc = new RecordController( "pregnancy_permanent" );


                string currentRecordGUID = rc.RecordGUID;
                rc.ProfileGUID = profileGUID;
                LoadedProfiles[ profileGUID ].Records.Add( rc.RecordGUID, rc );
                Dictionary<string, TemplateItem> tilist = LoadedProfiles[ profileGUID ].Records[ currentRecordGUID ].GetTemplateItems( );


                Dictionary<string, int> dStrIntNodeIndex = new Dictionary<string, int>( );

                foreach( KeyValuePair<string, TemplateItem> ti in tilist )
                {

                    ucTemplateItem guidanceItem = new ucTemplateItem {
                        OutlineMode = false,
                        Name = "ucti" + ti.Value.Name,
                        RecordInstance = rc,
                        Visible = false
                    };
                    guidanceItem.ItemValueChanged += delegate ( object o, EventArgs e ) {
                        //MessageBox.Show( guidanceItem.ItemValue );
                        TemplateItemEventArgs tiea = ( TemplateItemEventArgs )e;
                        string displayText = tiea.Value;
                        
                        TemplateItem displayItem = rc.GetTemplateItem( tiea.Key );

                        //Terning right around... to show yes/no instead of True/False
                        displayText = ( displayText == "true" ) ? "Yes" : ( displayText == "false" ) ? "No" : displayText;
                        
                        tvTemplateItems.Nodes[ dStrIntNodeIndex[ ti.Value.Group ] ].Nodes[ tiea.Key ].Text =  displayItem.Title + " - " + displayText;
                    };

                    guidanceItem.LoadTemplateItem( ti.Value );

                    pnlGuidanceControls.Controls.Add( guidanceItem );


                    if( !dStrIntNodeIndex.ContainsKey( ti.Value.Group ) )
                    {
                        tvTemplateItems.Nodes.Add( rc.GetGroupDisplayName( ti.Value.Group ) );
                        dStrIntNodeIndex.Add( ti.Value.Group, tvTemplateItems.Nodes.Count - 1 );
                    }
                    tvTemplateItems.Nodes[ dStrIntNodeIndex[ ti.Value.Group ] ].Nodes.Add( ti.Value.Name, ti.Value.Title );



                }


            } catch ( Exception exc ) { Models.Log.writeToLog( exc ); }
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

        private void CheckPrevNextButtons( TreeView tv, Button btnN, Button btnP ) {
            //Check to see if the 'Previous' button should be enabled.
            if( tv.SelectedNode != null || ( tv.SelectedNode != null && tv.SelectedNode.PrevNode == null && tv.SelectedNode.Parent.PrevNode != null ) ) {
                btnP.Enabled = true;
            }
            //Check to see if the next button should be enabled.
            if( tv.SelectedNode != null || ( tv.SelectedNode != null &&  tv.SelectedNode.NextNode == null && tv.SelectedNode.Parent.NextNode != null ) ) {
                btnN.Enabled = true;
            }
        }


    }
}
