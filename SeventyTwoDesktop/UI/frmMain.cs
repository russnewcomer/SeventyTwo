using Newtonsoft.Json.Linq;
using SeventyTwoDesktop.Controllers;
using SeventyTwoDesktop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace SeventyTwoDesktop
{
    public partial class FrmMain : Form
    {

        private CtlTemplateItem ActiveGuidanceItem { get; set; }
        private Dictionary< string, ProfileController > LoadedProfiles { get; set; } = new Dictionary< string, ProfileController >( );
        private readonly Dictionary< string, string > TemplateTypes = TemplateController.GetTemplateTypes( );
        private Dictionary< string, Dictionary< string, TemplateItem > > SubrecordTemplates { get; set; }
                

        public FrmMain() {
            InitializeComponent();

            LoadAllProfiles( );

            ctlCalendar1.SetStartingDate( );
            if ( UserController.ActiveUser == null ) {
                setCurrentUser();
            } else {
                btnCurrUser.Text = "User: " + UserController.ActiveUser.Name + ( UserController.ActiveUser.Number.Length > 0 ? " - " + UserController.ActiveUser.Number : "" );
            }

        }





        private void BtnSearch_Click(object sender, EventArgs e) {
            if( TxtSearch.Text != "" ) {
                SearchProfileListForString( TxtSearch.Text );
            } else {
                LoadAllProfiles( );
            }
        }

        private void BtnNewProfile_Click( object sender, EventArgs e ) {
            string guidToLoad = CreateNewProfile( );
            tabMain.TabPages.Add( CreateProfileTab( guidToLoad, false ) );
            tabMain.SelectTab( guidToLoad );
            
        }

        private void LstProfiles_DoubleClick( object sender, EventArgs e ) {
            if( LstProfiles.SelectedIndex > -1 ) {
                RequestProfilePage( ProfileListController.ProfileList[ LstProfiles.SelectedIndex ].GUID );
            }
        }

        private void BtnLoadSelectedProfile_Click( object sender, EventArgs e ) {
            if( LstProfiles.SelectedIndex > -1 ) {
                RequestProfilePage( ProfileListController.ProfileList[ LstProfiles.SelectedIndex ].GUID );
            }
        }


        private void BtnCloseAllOpenProfiles_Click( object sender, EventArgs e )
        {
            //Clear out all tabs that aren't the main two.
            while( tabMain.TabPages.Count > 2 ) {
                tabMain.TabPages.RemoveAt( tabMain.TabPages.Count - 1 );
            }
            LoadedProfiles = new Dictionary<string, ProfileController>( );
        }

        private void BtnShowAll_Click( object sender, EventArgs e ) {
            LoadAllProfiles( );
        }
        private void BtnSendLogs_Click( object sender, EventArgs e )
        {

            
            try {
                TabPage tabPageToCreate = new TabPage { Name = "Logs", Text = "Logs" };

                Button btnClose = new Button( ) { Text = "Press to close", Top = 455, Left = 5, Width = 800 };
                btnClose.Click += delegate ( object o, EventArgs evt ) {
                    tabMain.TabPages.RemoveByKey( "Logs" );
                };
                tabPageToCreate.Controls.Add( new RichTextBox( ) { Top = 50, Height = 400, Width = 800, Left = 5, Text = Log.GetRecentLogFiles( ) } );
                tabPageToCreate.Controls.Add( new Label( ) { Top = 5, Left = 5, Width = 800, Text = "Copy the text below to your clipboard.  Then open your email program and paste it into an email to whoever is helping you with SeventyTwo" } );
                tabPageToCreate.Controls.Add( btnClose );

                tabMain.TabPages.Add( tabPageToCreate );
                tabMain.SelectTab( "Logs" );

            } catch( Exception exc ) { Log.WriteToLog( exc ); }


        }

        private void ctlCalendar1_AppointmentClicked( object sender, EventArgs e ) {
            UI.AppointmentHandlingEventArgs appt = ( UI.AppointmentHandlingEventArgs )e;
            RequestProfilePage( 
                guidToLoad: appt.Node.linked_profile_guid, 
                recordToOpen: appt.Node.linked_record_guid,
                //If we have a record, we want to open the record, otherwise, we will create a template item
                templateTypeToOpen: ( string.IsNullOrEmpty(appt.Node.linked_record_guid ) ? appt.Node.record_type : "" )
            );
        }

        #region Profiles

        private void LoadAllProfiles( ) {
            //First, we need to get the profiles
            LstProfiles.Items.Clear( );
            foreach( ProfileListItem pli in ProfileListController.ProfileList ) {
                LstProfiles.Items.Add( pli );
            }
            LblProfileList.Text = "Profile List";
        }

        private void SearchProfileListForString( string SearchString ) {
            LstProfiles.Items.Clear( );
            foreach( ProfileListItem pli in ProfileListController.ProfileList.Where( T => T.Name.Contains( SearchString ) ) ) {
                LstProfiles.Items.Add( pli );
            }
            LblProfileList.Text = "Profile List: " + SearchString;
        }
        

        private void RequestProfilePage( string guidToLoad, string templateTypeToOpen = "", string recordToOpen = "" )
        {
            if( !tabMain.TabPages.ContainsKey( guidToLoad ) ) {
                //Tab has not been loaded, create a new copy and load it.
                LoadProfile( guidToLoad );
                tabMain.TabPages.Add( CreateProfileTab( ProfileGUID:guidToLoad, IsExistingProfile:true, templateTypeToInitiate:templateTypeToOpen, recordToOpen:recordToOpen ) );
            }
            tabMain.TabPages[ guidToLoad ].Tag = recordToOpen;
            tabMain.SelectTab( guidToLoad );
        }

        private string CreateNewProfile( ) {
            string currentGUID = "";

            try {
                ProfileController np = new ProfileController( );
                currentGUID = np.InitializeProfile( );
                LoadedProfiles.Add( currentGUID, np );

            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
            return currentGUID;
        }

        private void LoadProfile( string guidToLoad ) {
            //We may not need to load this profile.
            try {
                if( !LoadedProfiles.ContainsKey( guidToLoad ) ) {
                    LoadedProfiles.Add( guidToLoad, new ProfileController( guidToLoad ) );
                }
            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
        }

        private TabPage CreateProfileTab( string ProfileGUID, bool IsExistingProfile, string templateTypeToInitiate = "", string recordToOpen = "" ) {

            TabPage tabPageToCreate = new TabPage { Name = ProfileGUID, Text = LoadedProfiles[ ProfileGUID ].Profile.name };

            try {
            #region Controls
                bool hasProfileBeenInitiallySaved = IsExistingProfile;
                ProfileListItem curProfile = new ProfileListItem { Name = "New Profile", Number = "", GUID = ProfileGUID };


                Label lblProfileName = new Label {
                    Top = 5,
                    Left = 5,
                    Width = 300,
                    Text = "Profile",
                    AutoSize = true,
                    Font = new Font( "Segoe UI", 20 ),
                    TextAlign = ContentAlignment.MiddleCenter
                };


                Button btnEditProfile = new Button {
                    Left = 450,
                    Top = 5,
                    Height = 30,
                    Width = 150,
                    Text = "Back To Profile",
                    Visible = false
                };

                

                CtlPermanentRecord permRecordControl = new CtlPermanentRecord {
                    Name = "permRecordControl",
                    Top = 5,
                    Left = 5,
                    Height = 300,
                    Width = 600
                };

                permRecordControl.Height = permRecordControl.LogicalToDeviceUnits( permRecordControl.Height );
                permRecordControl.Width = permRecordControl.LogicalToDeviceUnits( permRecordControl.Width );


                ComboBox cmbNewRecord = new ComboBox {
                    Name = "cmbNewRecord",
                    Top = 5,
                    Left = 700,
                    Width = 250,
                    Height = 50
                };

                Button btnCreateNewRecord = new Button {
                    Top = 5,
                    Left = 605,
                    Width = 80,
                    Height = 40,
                    Text = "New"
                };



                TreeView tvExistingRecords = new TreeView {
                    Name = "tvExistingRecords",
                    Top = 55,
                    Left = 605,
                    Width = 350,
                    Height = 400,
                    Font = new Font( "Segoe UI", 12 ),
                    Visible = true
                };


                
                TreeView tvTemplateItems = new TreeView {
                    Name = "tvTemplateItems",
                    Top = 5,
                    Left = 605,
                    Width = 350,
                    Height = 450,
                    Font = new Font( "Segoe UI", 12 ),
                    Visible = false
                };

                Panel pnlGuidanceControls = new Panel {
                    Name = "pnlGuidanceControls",
                    Top = 50,
                    Height = 200,
                    Left = 5,
                    Width = 550, 
                    Visible = false
                };


                Button btnPreviousGuidanceItem = new Button { Left = 15, Top = 250, Height = 90, Width = 170, Text = "&Previous", Visible = false, Font = new Font( "Segoe UI", 20 ) };
                Button btnNextGuidanceItem = new Button { Left = 205, Top = 250, Height = 90, Width = 170, Text = "&Next", Visible = false, Font = new Font( "Segoe UI", 20 ) };


                //Followup panel and related controls

                Panel pnlFollowup = new Panel {
                    Name = "pnlFollowup",
                    Left = 10,
                    Top = 355,
                    Height = 110,
                    Width = 550,
                    Visible = true
                };


                DateTimePicker dtpFollowupDate = new DateTimePicker {
                    Left = 5,
                    Top = 30,
                    Height = 23,
                    Width = 110,
                    CustomFormat = "dd-MMM-yyyy",
                    Format = DateTimePickerFormat.Custom
                };


                Button btnAddApptDate = new Button { Left = 5, Top = 55, Height = 40, Width = 110, Text = "Add Date", Font = new Font( "Segoe UI", 9 ) };
                ListBox lstAppointmentDates = new ListBox { Top = 30, Width = 200, Height = 65, Left = 130 };
                Button btnSaveAppointments = new Button { Left = 340, Top = 30, Height = 65, Width = 110, Text = "Schedule Appointments", Font = new Font( "Segoe UI", 9 ) };



                #endregion


                #region Scope Functions

                

                List<DateTime> AppointmentDates = new List<DateTime>( );
                
                void _ChangeToRecordView( ) {
                    permRecordControl.Hide( );
                    pnlGuidanceControls.Show( );

                    btnPreviousGuidanceItem.Show( );
                    btnNextGuidanceItem.Show( );

                    tvExistingRecords.Hide( );
                    tvTemplateItems.Show( );
                    btnCreateNewRecord.Hide( );
                    cmbNewRecord.Hide( );
                    btnEditProfile.Show( );
                }

                void _ChangeToProfileView( ) {
                    permRecordControl.Show( );
                    pnlGuidanceControls.Hide( );

                    tvExistingRecords.Show( );
                    tvTemplateItems.Hide( );
                    btnCreateNewRecord.Show( );
                    cmbNewRecord.Show( );
                    btnPreviousGuidanceItem.Hide( );
                    btnNextGuidanceItem.Hide( );
                    btnEditProfile.Hide();

                    _PopulateExistingRecordsTreeView( );
                }

                void _ForceProfileSave() {

                    try
                    {
                        string curRecordGuid = tvTemplateItems.Nodes[0].Name;
                        LoadedProfiles[ProfileGUID].Records[curRecordGuid].WriteRecord();
                    }
                    catch (Exception exc) { Log.WriteToLog(exc); }

                 

                      _ChangeToProfileView( );

                }

                void _HandleProfileNameChange() {
                    string profileName = permRecordControl.GetProfileName( );
                    profileName = ( profileName != "" ) ? profileName : "New Profile";
                    tabPageToCreate.Text = profileName;
                    lblProfileName.Text = profileName;
                }

                void _HandleProfileUpdate() {
                    curProfile.Name = permRecordControl.GetProfileName( );
                    curProfile.Number = permRecordControl.GetProfileNumber( );

                    if( !hasProfileBeenInitiallySaved ) {
                        ProfileListController.AddItemToList( curProfile );
                        hasProfileBeenInitiallySaved = true;
                    } else {
                        ProfileListController.AlterExistingItem( curProfile );
                    }
                    LoadAllProfiles( );
                }

                void _EnableGuidanceItem( string ControlKey ) {

                    ActiveGuidanceItem?.Hide( );
                    btnPreviousGuidanceItem.Show( );
                    btnNextGuidanceItem.Show( );

                    foreach( CtlTemplateItem ctl in pnlGuidanceControls.Controls.Find( "ucti" + ControlKey, false ) ) {
                        ActiveGuidanceItem = ctl;
                        ActiveGuidanceItem.Show( );
                        ActiveGuidanceItem.FocusMVC( );
                    }

                }

                void _CheckPrevNextButtons( TreeNode CurrentNode ) {
                    try {
                        //Check to see if the 'Previous' button should be enabled.
                        if( CurrentNode != null && ( CurrentNode.PrevNode != null || ( CurrentNode.Parent != null && CurrentNode.Parent.PrevNode != null ) ) ) {
                            btnPreviousGuidanceItem.Show( );
                        } else {
                            btnPreviousGuidanceItem.Hide( );
                        }
                        //Check to see if the next button should be enabled.
                        if( CurrentNode != null && ( CurrentNode.NextNode != null || ( CurrentNode.Parent != null && CurrentNode.Parent.NextNode != null ) ) ) {
                            btnNextGuidanceItem.Show( );
                        } else {
                            btnNextGuidanceItem.Hide( );
                        }
                    } catch ( Exception exc ) {
                        //Yes, we are kind of swallowing errors, but we want to default to being able to see the UI.
                        Log.WriteToLog( exc );
                        btnPreviousGuidanceItem.Show( );
                        btnNextGuidanceItem.Show( );
                    }
                }

                void _GoToPreviousGuidanceItem( ) {
                    //Go previous
                    bool hasNode = false;
                    //Check to see if we have a previous node we can go to.
                    if( tvTemplateItems.SelectedNode.PrevNode == null && tvTemplateItems.SelectedNode.Parent.PrevNode != null ) {
                        tvTemplateItems.SelectedNode = tvTemplateItems.SelectedNode.Parent.PrevNode.LastNode;
                        hasNode = true;
                    } else if( tvTemplateItems.SelectedNode.PrevNode != null ) {
                        tvTemplateItems.SelectedNode = tvTemplateItems.SelectedNode.PrevNode;
                        hasNode = true;
                    }
                    //If we do have a node, move there.
                    if( hasNode ) {
                        _EnableGuidanceItem( tvTemplateItems.SelectedNode.Name );
                    }
                    //Check to see if we should show these.
                    _CheckPrevNextButtons( tvTemplateItems.SelectedNode );
                }

                void _GoToNextGuidanceItem() {
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
                        _EnableGuidanceItem( tvTemplateItems.SelectedNode.Name );
                    }
                    //Check to see if we should show these.
                    _CheckPrevNextButtons( tvTemplateItems.SelectedNode );
                }

                void _CreateNewRecord( string templateType ) {
                    try {
                        RecordController rc = new RecordController( templateType, ProfileGUID, TemplateStyle.Blank );
                        string currentRecordGUID = rc.RecordGUID;
                        LoadedProfiles[ ProfileGUID ].Records.Add( rc.RecordGUID, rc );
                        _PopulateRecordUI( currentRecordGUID );
                    
                    } catch( Exception exc ) { Log.WriteToLog( exc ); }
                }

                void _HandleTemplateNodeSelection( TreeNode CurrentNode ) {
                    if( CurrentNode.Name != "" && !CurrentNode.Name.Contains( "tv_subrecord_" ) ) {
                        //MessageBox.Show( e.Node.Name );
                        _EnableGuidanceItem( CurrentNode.Name );
                        _CheckPrevNextButtons( CurrentNode );
                    } else if( CurrentNode.Name != "" && CurrentNode.Name.Contains( "tv_subrecord_" ) ) {
                        //Pop up FrmSubRecord instance with this in it.
                        UI.FrmSubRecord frmSub = new UI.FrmSubRecord( );
                        frmSub.SetTitleText( CurrentNode.Parent.Text );
                        string curRecordGuid = tvTemplateItems.Nodes[ 0 ].Name;
                        TreeNode curNode = CurrentNode;
                        int idx = CurrentNode.Index - 1;
                        frmSub.RecordChanged += delegate ( object snd, EventArgs snde ) {
                            //e.Node.Index - 1 works because -1 is defined as a new record, and everything else should update automatically without promblems.
                            JObject subRec = frmSub.GetRecordResults( );
                            int newIdx = LoadedProfiles[ ProfileGUID ].Records[ curRecordGuid ].UpdateTemplateItemSubRecord( CurrentNode.Parent.Name, subRec, idx );
                            if( idx != newIdx ) {
                                string NodeName = ( subRec.ContainsKey( "name" ) ? subRec[ "name" ].ToString( ) : "New Record" );
                                curNode = CurrentNode.Parent.Nodes.Add( "tv_subrecord_" + CurrentNode.Parent.Name + newIdx.ToString( ), NodeName );
                                idx = newIdx;
                            } else {
                                curNode.Text = ( subRec.ContainsKey( "name" ) ? subRec[ "name" ].ToString( ) : "New Record" );
                            }
                        };

                        //Load the template
                        frmSub.LoadTemplate( SubrecordTemplates[ CurrentNode.Parent.Name ], LoadedProfiles[ ProfileGUID ].Records[ curRecordGuid ] );

                        //If we are loading items that already exist (they don't have the _add_ in their name)
                        if( !CurrentNode.Name.Contains( "tv_subrecord_add_" ) ) {
                            frmSub.LoadTemplateData( LoadedProfiles[ ProfileGUID ].Records[ curRecordGuid ].GetTemplateItemSubRecord( CurrentNode.Parent.Name, idx ) );
                        }

                        frmSub.ShowDialog( );
                    }
                }
                

                void _PopulateRecordUI( string RecordGUID ) {
                    try {


                        //Clear out our followup dates.
                        AppointmentDates = new List<DateTime>( );
                        

                        TreeNode firstNode = null;
                        RecordController rc = LoadedProfiles[ ProfileGUID ].Records[ RecordGUID ];

                        pnlFollowup.Visible = !rc.GetFollowupScheduled( );

                        //Get the GUID for the profile
                        string templateType = rc.GetTemplateType( );
                        
                        Dictionary<string, int> dStrIntNodeIndex = new Dictionary<string, int>( );

                        //Clear out the subrecord templates.
                        SubrecordTemplates = new Dictionary<string, Dictionary<string, TemplateItem>>( );
                        tvTemplateItems.Nodes.Clear( );

                        //Clear out all old controls
                        foreach( Control ctrl in pnlGuidanceControls.Controls ) {
                            if( ctrl.Name.Contains( "ucti" ) ) {
                                pnlGuidanceControls.Controls.Remove( ctrl );
                            }
                        }
                        if( pnlGuidanceControls.Controls.Count > 0 ) {
                            pnlGuidanceControls.Controls.Clear( );
                        }


                        TreeNode rootNode = new TreeNode( TemplateTypes[ templateType ] ) {
                            Name = RecordGUID
                        };
                        tvTemplateItems.Nodes.Add( rootNode );

                       

                        foreach( KeyValuePair<string, TemplateItem> ti in rc.GetTemplateItems( ) ) {

                            //Create the node first.
                            if( !dStrIntNodeIndex.ContainsKey( ti.Value.group ) ) {
                                rootNode.Nodes.Add( rc.GetGroupDisplayName( ti.Value.group ) );
                                dStrIntNodeIndex.Add( ti.Value.group, rootNode.Nodes.Count - 1 );
                            }

                            //We want to see if we should put in values in the node title or not.
                            string nodeTitle = ( string.IsNullOrEmpty( ti.Value.value ) ) ? ti.Value.title : ti.Value.title + " - " + _DisplayTextFormatter( ti.Value.value );

                            rootNode.Nodes[ dStrIntNodeIndex[ ti.Value.group ] ].Nodes.Add( ti.Value.name, nodeTitle );
                            if ( firstNode == null ) {
                                firstNode = rootNode.Nodes[ dStrIntNodeIndex[ ti.Value.group ] ].Nodes[ 0 ];
                            }

                            if( ti.Value.subrecord_items.Count == 0 ) {
                                CtlTemplateItem guidanceItem = new CtlTemplateItem {
                                    OutlineMode = false,
                                    Name = "ucti" + ti.Value.name,
                                    RecordInstance = rc,
                                    Visible = false
                                };
                                guidanceItem.ItemValueChanged += delegate ( object o, EventArgs e ) {
                                    //MessageBox.Show( guidanceItem.ItemValue );
                                    TemplateItemEventArgs tiea = ( TemplateItemEventArgs )e;
                                    TemplateItem displayItem = rc.GetTemplateItem( tiea.Key );
                                    rootNode.Nodes[ dStrIntNodeIndex[ ti.Value.group ] ].Nodes[ tiea.Key ].Text = displayItem.title + " - " + _DisplayTextFormatter(tiea.Value);

                                    //check to see if the guidance item value is the Followup Schedule Field
                                    if ( tiea.Key == rc.GetFollowupFieldUntilName() && !rc.GetFollowupScheduled() ) {
                                        _PopulateAppointmentDates( );
                                    }
                                };

                                guidanceItem.LoadTemplateItem( ti.Value );

                                pnlGuidanceControls.Controls.Add( guidanceItem );
                            } else {
                                //This should probably create a new form and pop it up.  There's certainly a better way, but I'm not thinking of it right now.

                                SubrecordTemplates.Add( ti.Key, ti.Value.subrecord_items );
                                rootNode.Nodes[ dStrIntNodeIndex[ ti.Value.group ] ].Nodes[ ti.Value.name ].Nodes.Add( "tv_subrecord_add_" + ti.Key, "Add " + ti.Value.title );

                                int i = 0;
                                foreach( JObject record in ti.Value.subrecords ) {
                                    rootNode.Nodes[ dStrIntNodeIndex[ ti.Value.group ] ].Nodes[ ti.Value.name ].Nodes.Add( "tv_subrecord_" + ti.Key + i.ToString( ), record[ "name" ].ToString( ) );
                                    i++;
                                }

                            }

                        }

                        //Expand all items
                        rootNode.ExpandAll( );
                        //Select the first node.
                        tvTemplateItems.SelectedNode = firstNode;
                        _EnableGuidanceItem( tvTemplateItems.SelectedNode.Name );

                        _ChangeToRecordView( );

                    } catch( Exception exc ) { Models.Log.WriteToLog( exc ); }
                }

                
                void _PopulateRecordTypesComboBox() {
                    cmbNewRecord.Items.Clear( );

                    foreach( KeyValuePair<string, string> record in TemplateTypes ) {
                        //We don't want to show the permanent option.
                        if( record.Key != "permanent" ) {
                            cmbNewRecord.Items.Add( record.Value );
                        }
                    };
                }

                void _PopulateExistingRecordsTreeView() {
                    tvExistingRecords.Nodes.Clear( );
                    foreach( KeyValuePair<string, RecordController> rc in LoadedProfiles[ ProfileGUID ].Records ) {
                        //Adding items to the tab
                        TreeNode curRecNode = new TreeNode( rc.Value.GetRecordDisplayText( ) ) {
                            Name = rc.Key
                        };

                        //Show the first 5 items
                        foreach( KeyValuePair<string, TemplateItem> item in rc.Value.GetTemplateItems( ).Take( 3 ) ) {
                            curRecNode.Nodes.Add( new TreeNode( item.Value.title + " - " + _DisplayTextFormatter( item.Value.value ) ) { Name = rc.Key } );
                        }
                        tvExistingRecords.Nodes.Add( curRecNode );
                    }
                }



                string _DisplayTextFormatter( string TextToFormat ) {
                    //We only show the first 50 characters.
                    string retVal = ( TextToFormat.Length > 50 ) ? TextToFormat.Substring( 0,50 ) : TextToFormat;
                    //Terning right around... to show yes/no instead of True/False
                    if( TextToFormat == "true" ) {
                        retVal = "Yes";
                    } else if( TextToFormat == "false" ) {
                        retVal = "No";
                    }

                    return retVal;
                }

                void _PopulateAppointmentDates( ) {
                    /*
            		"interval":"1",
		            "field_until":"due_date",
		            "units":"m",
		            "record_type":"maternal_antenatal_visit",
		            "scheduled":"false",
                     */
                    
                    RecordController rc = LoadedProfiles[ ProfileGUID ].Records[ tvTemplateItems.Nodes[ 0 ].Name ];

                    //Check to see if we have scheduled the followup yet.
                    if ( !rc.GetFollowupScheduled() ) {
                        AppointmentDates.Clear( );
                        Dictionary<string, string> FollowupSchedule = rc.GetFollowupSchedule( );
                        _AddDatesToAppointmentListUntil( CalendarListController.GetDateTime( rc.GetData( FollowupSchedule[ "field_until" ] ) ), FollowupSchedule[ "units" ], int.Parse( FollowupSchedule[ "interval" ] ) );
                    }

                }

                void _AddDatesToAppointmentListUntil( DateTime finalDate, string units, int interval ) {
                    DateTime incrementer = CalendarListController.AddTime( DateTime.Now, units, interval );
                    while( incrementer < finalDate ) {

                        AppointmentDates.Add( incrementer );
                        incrementer = CalendarListController.AddTime( incrementer, units, interval );
                    }
                    AppointmentDates.Sort( );
                    lstAppointmentDates.Items.Clear( );
                    foreach( DateTime dt in AppointmentDates ) {
                        lstAppointmentDates.Items.Add( dt.ToString( "dd-MMM-yyyy" ) );
                    }
                }

                void _AddDateToAppointmentList( DateTime dateToAdd ) {
                    AppointmentDates.Add( dateToAdd );
                    AppointmentDates.Sort( );
                    lstAppointmentDates.Items.Clear( );
                    foreach( DateTime dt in AppointmentDates ) {
                        lstAppointmentDates.Items.Add( dt.ToString( "dd-MMM-yyyy" ) );
                    }
                }

                void _RemoveDateFromAppointmentList( ) {
                    AppointmentDates.RemoveAt( lstAppointmentDates.SelectedIndex );
                    lstAppointmentDates.Items.RemoveAt( lstAppointmentDates.SelectedIndex );
                }

                void _SaveAppointments() {
                    string curRecordGuid = tvTemplateItems.Nodes[ 0 ].Name;
                    RecordController rc = LoadedProfiles[ ProfileGUID ].Records[ curRecordGuid ];
                    foreach (DateTime dt in AppointmentDates ) {
                        Dictionary<string, string> FollowupSchedule = rc.GetFollowupSchedule( );
                        string recordType = FollowupSchedule[ "record_type" ];
                        string itemTitle = LoadedProfiles[ ProfileGUID ].Profile.name + " - " + LoadedProfiles[ ProfileGUID ].Profile.phonenumber + " - " + TemplateController.GetTemplateTypes( )[ recordType ] + " - ";
                        try {
                            ctlCalendar1.AddCalendarItem( new CalendarItem {
                                item_date = CalendarListController.GetDateString( dt ),
                                item_title = itemTitle,
                                record_type = recordType,
                                linked_record_guid = curRecordGuid,
                                linked_profile_guid = ProfileGUID,
                                responsible_party = "",
                                item_notes = ""
                            } );
                        } catch ( Exception exc ) {
                            Log.WriteToLog( exc );
                        }
                    }
                    rc.SetFollowupScheduled( true );
                };

                void _ResetControlDPISizes( Control ctrl ) {
                    ctrl.Height = ctrl.LogicalToDeviceUnits( ctrl.Height );
                    ctrl.Width = ctrl.LogicalToDeviceUnits( ctrl.Width );
                    ctrl.Top = ctrl.LogicalToDeviceUnits( ctrl.Top );
                    ctrl.Left = ctrl.LogicalToDeviceUnits( ctrl.Left );
                    foreach ( Control subCtrl in ctrl.Controls ) {
                        _ResetControlDPISizes( subCtrl );
                    }
                };

                #endregion

                #region Event Handlers
                permRecordControl.ProfileNameChange += delegate ( object o, EventArgs e ) {
                    _HandleProfileNameChange( );
                };

                permRecordControl.ProfileSaved += delegate ( object o, EventArgs e ) {
                    _HandleProfileNameChange( );
                    _HandleProfileUpdate( );  
                };

                btnEditProfile.Click += delegate ( object o, EventArgs e ) {
                    _ForceProfileSave( );
                };


                btnPreviousGuidanceItem.Click += delegate ( object o, EventArgs e ) {
                    _GoToPreviousGuidanceItem( );
                };

                btnNextGuidanceItem.Click += delegate ( object o, EventArgs e ) {
                    _GoToNextGuidanceItem( );
                };

                btnCreateNewRecord.Click += delegate ( object o, EventArgs e ) {
                    try {
                        if ( cmbNewRecord.SelectedItem != null ) {
                            string templateType = TemplateTypes.First( T => T.Value == cmbNewRecord.SelectedItem.ToString( ) ).Key;
                            DialogResult result = MessageBox.Show( "Do you want to create a new " + cmbNewRecord.SelectedItem.ToString( ) + " record?", "Confirmation", MessageBoxButtons.YesNo );
                            if ( result == DialogResult.Yes ) {
                                //If we confirm we want to create, create a new one
                                _CreateNewRecord( templateType );
                            }

                            _ChangeToRecordView( );
                        }
                    } catch ( Exception exc ) { Log.WriteToLog( exc ); }
                    
                };

                tvTemplateItems.NodeMouseClick += delegate ( object o, TreeNodeMouseClickEventArgs e ) {
                    _HandleTemplateNodeSelection( e.Node );
                };

                tvExistingRecords.NodeMouseClick += delegate ( object o, TreeNodeMouseClickEventArgs e ) {
                    //If the selected node has no children, treat a click like an open.  Otherwise, we only do so on double-click.
                    if( e.Node.Nodes.Count == 0 ) {
                        _PopulateRecordUI( e.Node.Name );
                    }
                };

                tvExistingRecords.NodeMouseDoubleClick += delegate ( object o, TreeNodeMouseClickEventArgs e ) {
                    _PopulateRecordUI( e.Node.Name );
                };

                btnAddApptDate.Click += delegate ( object o, EventArgs e ) {
                    _AddDateToAppointmentList( dtpFollowupDate.Value );
                };

                lstAppointmentDates.KeyDown += delegate ( object o, KeyEventArgs e ) {
                    if( e.KeyCode == Keys.Delete ) {
                        _RemoveDateFromAppointmentList( );
                    }
                };

                btnSaveAppointments.Click += delegate ( object o, EventArgs e ) {
                    _SaveAppointments();
                };


                
                tabPageToCreate.Enter += delegate ( object o, EventArgs e ) {
                    /*
                     * This where the only real 'clever' part of this exists.  
                     * To ensure that the functions stay in scope, we will add an event listener to 'open'
                     * When we have a specific record to open, we will assign that to the .Tag for the tab
                     * Then switch to the tab.  Switching to the tab will trigger this event handler
                     * So if we have a string in the tag, we will populate the record, then clear it out
                     */
                    if ( !string.IsNullOrEmpty( tabPageToCreate.Tag.ToString( ) ) ) {
                        _PopulateRecordUI( tabPageToCreate.Tag.ToString( ) );
                        tabPageToCreate.Tag = null;
                    }
                };


                #endregion

                #region Loading Data For Tab
                //Loading record template items
                permRecordControl.Profile = LoadedProfiles[ ProfileGUID ];
                permRecordControl.LoadData( ProfileGUID );


                //May end up moving this somewhere else to handle 'record natures' if we get that far.
                _PopulateRecordTypesComboBox( );
                
                //load existing records in the profile.
                _PopulateExistingRecordsTreeView( );


                if ( !string.IsNullOrEmpty( templateTypeToInitiate ) ) {
                    _CreateNewRecord( templateTypeToInitiate );
                }

                if ( !string.IsNullOrEmpty( recordToOpen ) ) {
                    //Assign this to the tag so the .Enter event handler will pick this up
                    tabPageToCreate.Tag = recordToOpen;
                }

                #endregion

                #region Add Controls To Page






                    tabPageToCreate.Controls.Add( btnEditProfile );
                tabPageToCreate.Controls.Add( lblProfileName );
                tabPageToCreate.Controls.Add( btnPreviousGuidanceItem );
                tabPageToCreate.Controls.Add( btnNextGuidanceItem );
                tabPageToCreate.Controls.Add( tvExistingRecords );
                tabPageToCreate.Controls.Add( tvTemplateItems );
                tabPageToCreate.Controls.Add( pnlGuidanceControls );
                tabPageToCreate.Controls.Add( cmbNewRecord );
                tabPageToCreate.Controls.Add( btnCreateNewRecord );
                

                _ResetControlDPISizes( tabPageToCreate );


                pnlFollowup.Controls.Add(new Label { Top = 5, Left = 5, Width = 200, Font = new Font("Segoe UI", 9), Text = "Schedule Followup Appointments" });
                pnlFollowup.Controls.Add(dtpFollowupDate);
                pnlFollowup.Controls.Add(btnAddApptDate);
                pnlFollowup.Controls.Add(lstAppointmentDates);
                pnlFollowup.Controls.Add(btnSaveAppointments);
                tabPageToCreate.Controls.Add(pnlFollowup);
                _ResetControlDPISizes( pnlFollowup );


                tabPageToCreate.Controls.Add(permRecordControl);
                #endregion
            } catch ( Exception exc ) { Log.WriteToLog( exc );  }

            return tabPageToCreate;
        }

        private void BtnExportProfiles_Click( object sender, EventArgs e ) {
            SaveFileDialog zipDialog = new SaveFileDialog {
                //Filter = "72 Profiles (zip)|.zip",
                Title = "Export File",
                FileName = DateTime.Now.ToString( "yyyy-MM-dd" ) + "-72-Sync-Out-" + UserController.ActiveUser.Name + ".zip"
            };
            
            if ( zipDialog.ShowDialog( ) == DialogResult.OK ) { 
                SyncController.CreateExportZip( zipDialog.FileName );
            }
        }


        private void BtnImportProfiles_Click( object sender, EventArgs e ) {
            OpenFileDialog zipDialog = new OpenFileDialog {
                //Filter = "72 Profiles (zip)|.zip",
                Title = "Import File"
            };

            if ( zipDialog.ShowDialog( ) == DialogResult.OK ) {
                SyncController.ImportData( zipDialog.FileName );
            }

        }

        #endregion 

        //Look at the users list.  Pick the current user as necessary
        private void setCurrentUser( ) {
            Form userSelDlg = new Form( ) {
                Text = "Select a name",
                Width = 600,
                Height = 600,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen
            };

            userSelDlg.Controls.Add( new Label( ) { Left = 50, Top = 20, Width = 500, Text = "Enter a user name" } );
            TextBox txtNewUser = new TextBox( ) { Left = 50, Width = 500, Top = 50, Height = 50 };

            userSelDlg.Controls.Add( new Label( ) { Left = 50, Top = 120, Width = 500, Text = "Enter a user number" } );
            TextBox txtNewNumber = new TextBox( ) { Left = 50, Width = 500, Top = 150, Height = 50 };

            userSelDlg.Controls.Add( new Label( ) { Left = 50, Top = 200, Width = 500, Text = "Or select a user" } );
            ListBox lstUsers = new ListBox( ) { Left = 50, Top = 230, Width = 500, Height = 250 };
            Button confirmation = new Button( ) { Text = "Ok", Left = 350, Height = 50, Width = 100, Top = 480, DialogResult = DialogResult.OK };
            confirmation.Click += ( sender, e ) => {
                if ( !checkCanClose( ) ) { userSelDlg.Close( ); }
            };
            userSelDlg.FormClosing += ( sender, e ) => {
                e.Cancel = !checkCanClose( );
            };
            bool checkCanClose() {
                if ( lstUsers.SelectedIndex > -1 ) {
                    UserItem activeUsr =(UserItem)(lstUsers.SelectedItem);

                    UserController.SetActiveUser( activeUsr.GUID );

                    btnCurrUser.Text = "User: " + activeUsr.Name + ( activeUsr.Number.Length > 0 ? " - " + activeUsr.Number : "" );
                    return true;
                } else if ( txtNewUser.TextLength > 0 ) {
                    UserItem newUsr = UserController.AddItemToList( txtNewUser.Text, txtNewNumber.Text );
                    lstUsers.Items.Add( newUsr );
                    lstUsers.SelectedItem = newUsr;
                    btnCurrUser.Text = "User: " + newUsr.Name + ( newUsr.Number.Length > 0 ? " - " + newUsr.Number : "" );
                    return true;
                } else {
                    MessageBox.Show( "You must select a user or add a new one" );
                    return false;
                }
            }


            foreach ( UserItem u in UserController.UserList ) {
                lstUsers.Items.Add( u );
            }


            userSelDlg.Controls.Add( txtNewUser );
            userSelDlg.Controls.Add( txtNewNumber );
            userSelDlg.Controls.Add( lstUsers );
            userSelDlg.Controls.Add( confirmation );
            userSelDlg.AcceptButton = confirmation;

            userSelDlg.ShowDialog( );

        }
        private void BtnCurrUser_Click( object sender, EventArgs e ) {
            setCurrentUser( );
        }

        private void btnExcelExport_Click( object sender, EventArgs e ) {

            SaveFileDialog excelSaveDialog = new SaveFileDialog {
                Title = "Export File",
                FileName = DateTime.Now.ToString( "yyyy-MM-dd" ) + "-72-Excel-Out.xlsx"
            };

            if ( excelSaveDialog.ShowDialog( ) == DialogResult.OK ) {
                SpreadsheetExportController.CreateExportFile( excelSaveDialog.FileName );
            }

          
        }
    }
}
