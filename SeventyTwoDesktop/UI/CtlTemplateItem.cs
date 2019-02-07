using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using SeventyTwoDesktop.Models;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop
{

    public partial class CtlTemplateItem : UserControl
    {
        private TemplateItem _ti { get; set; }
        public Controllers.RecordController RecordInstance { get; set; }

        private Control MainValueControl { get; set; }
        private FlowLayoutPanel OptionalFieldsFlowPanel { get; set; }

        public bool OutlineMode { get; set; } = false;
        public bool CanEditInOutlineMode { get; set; } = false;

        private string _name { get; set; }
        public string ItemName {  get { return _name; } }

        private string _value { get; set; }
        public string ItemValue { get { return _value; } }


        private string _group { get; set; }
        public string ItemGroup { get { return _group; } }

        private bool HasOptionalFields { get; set; } = false;
        public bool HandleRecordDataUpdate { get; set; } = true;

        public event EventHandler OutlineModeClick;
        public event EventHandler ItemValueChanged;
       

        public CtlTemplateItem()
        {
            InitializeComponent();
        }


        public void LoadTemplateItem( TemplateItem template ) {
            _ti = template;
            _name = _ti.name;
            _group = _ti.group;
            if ( OutlineMode ) {
                GenerateOutlineControl( );
            } else {
                GenerateGuidanceControl( );
            }
        }

        private void GenerateOutlineControl( )
        {
            this.Controls.Add( new Label { Top =4, Left = 4, Text = _ti.title, AutoSize = true } );
         
            switch( _ti.field_type ) {
                case "true_false":
                    MainValueControl = new CheckBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Checked = (_ti.value == "true")
                    };
                    CheckBox cbMVC = ( CheckBox )MainValueControl;
                    cbMVC.CheckStateChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _ti.value = cbMVC.Checked ? "true" : "false";
                        if( this.Focused ) {
                            HandleItemValueChange( _ti.value );
                        }
                    };
                    
                    break;
                case "numeric":
                    MainValueControl = new NumericUpDown {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = _ti.value,
                        Maximum = 10000,
                        Minimum = -10000
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _ti.value = MainValueControl.Text;
                        if( this.Focused ) {
                            HandleItemValueChange( _ti.value );
                        }
                    };
                    break;
                case "notes":
                    MainValueControl = new RichTextBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = _ti.value,
                        Multiline = true,
                        WordWrap = true
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        MainValueControl.Size = MainValueControl.GetPreferredSize( new Size( 192, 300 ) );
                        this.Height = MainValueControl.Height + 20;
                        _ti.value = MainValueControl.Text;
                        if( this.Focused ) {
                            HandleItemValueChange( _ti.value );
                        }
                    };
                    break;
                case "dropdown":
                    MainValueControl = new ComboBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = _ti.value
                    };
                    ComboBox cmbMVC = ( ComboBox )MainValueControl;
                    for( int i = 0; i < _ti.dropdown_options.Count; i++ ) {
                        cmbMVC.Items.Add( _ti.dropdown_options[ i ] );
                    }

                    cmbMVC.SelectedIndexChanged += delegate ( object o, EventArgs e ) {
                        _ti.value = cmbMVC.Items[ cmbMVC.SelectedIndex ].ToString();
                        if( this.Focused ) {
                            HandleItemValueChange( _ti.value );
                        }
                    };
                    break;
                case "text":
                default:
                    MainValueControl = new TextBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = _ti.value,
                        Multiline = false
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _ti.value = MainValueControl.Text;
                        if( this.Focused ) {
                            HandleItemValueChange( _ti.value );
                        }
                    };
                    break;

            }
            this.Controls.Add( MainValueControl );
            //Disable all controls if necessary.
            foreach (Control ctl in Controls) {
                ctl.Enabled = CanEditInOutlineMode;
            }
            Click += delegate ( object o, EventArgs e ) {
                if( !CanEditInOutlineMode ) {
                    this.OutlineModeClick( o, new TemplateItemEventArgs( _ti.name, _ti.value ) );
                }
            };

        }

        private void GenerateGuidanceControl( ) {
            this.Controls.Add( new Label { Top = 4, Left = 4, Width = 500, Text = _ti.title, AutoSize = true, Font = new Font( "Segoe UI", 20 ), TextAlign = ContentAlignment.MiddleCenter } );

            switch( _ti.field_type )
            {
                
                case "true_false":
                    MainValueControl = new CheckBox {
                        Top = 0,
                        Left = 0,
                        Checked = ( _ti.value == "true" ),
                        Visible = false
                    };
                    CheckBox cbMVC = ( CheckBox )MainValueControl;
                    
                    Button btnYes = new Button { Name="BtnYes", Left = 10, Top = 40, Height = 100, Width = 150, Text = "YES", Font = new Font( "Segoe UI", 20 ) };
                    Button btnNo = new Button { Name = "BtnNo", Left = 170, Top = 40, Height = 100, Width = 150, Text = "NO", Font = new Font( "Segoe UI", 20 ) };

                    btnYes.Click += delegate ( object o, EventArgs e ) {
                        cbMVC.Checked = true;
                        btnYes.BackColor = Color.Green;
                        btnNo.BackColor = Color.Transparent;
                        if( HasOptionalFields ) {
                            OptionalFieldsFlowPanel.Show( );
                        }
                        HandleItemValueChange( "true" );
                    };

                    btnNo.Click += delegate ( object o, EventArgs e ) {
                        
                        cbMVC.Checked = false;
                        btnYes.BackColor = Color.Transparent;
                        btnNo.BackColor = Color.Red;
                        if( HasOptionalFields ) {
                            OptionalFieldsFlowPanel.Hide( );
                        }

                        HandleItemValueChange( "false" );
                    };

                    
                    this.Controls.Add( btnYes );
                    this.Controls.Add( btnNo );

                    foreach( KeyValuePair<string, TemplateItem> optField in _ti.optional_fields ) {
                        HasOptionalFields = true;

                        if( OptionalFieldsFlowPanel == null ) {
                            OptionalFieldsFlowPanel = new FlowLayoutPanel {
                                Visible = false, Top = 40, Left = 330, Width = 150, Height = 100
                            };
                            this.Controls.Add( OptionalFieldsFlowPanel );
                            OptionalFieldsFlowPanel.BringToFront( );
                        }

                        OptionalFieldsFlowPanel.Controls.Add( new Label { Top = 4, Left = 4, Text = optField.Value.title, AutoSize = true } );

                        switch ( optField.Value.field_type ) {
                            case "notes":

                                RichTextBox notesCtl = new RichTextBox {
                                    Left = 4,
                                    Width = 130,
                                    Height = 75,
                                    Text = optField.Value.value,
                                    Multiline = true,
                                    WordWrap = true,
                                    ScrollBars = RichTextBoxScrollBars.Vertical
                                };
                                notesCtl.TextChanged += delegate ( object o, EventArgs e ) {
                                    optField.Value.value = notesCtl.Text;
                                    if( this.Focused ) {
                                        HandleOptionalItemValueChange( optField.Value, notesCtl.Text );
                                    }
                                };
                                OptionalFieldsFlowPanel.Controls.Add( notesCtl );
                                break;
                            case "numeric":
                                NumericUpDown nudCtl = new NumericUpDown {
                                    Left = 4,
                                    Width = 130,
                                    Text = optField.Value.value,
                                    Font = new Font( "Segoe UI", 20 ),
                                    Maximum = 10000,
                                    Minimum = -10000
                                };
                                nudCtl.TextChanged += delegate ( object o, EventArgs e ) {
                                    //Raise the textchanged event.
                                    if( this.Focused ) {
                                        HandleOptionalItemValueChange( optField.Value, nudCtl.Text );
                                    }
                                };
                                OptionalFieldsFlowPanel.Controls.Add( nudCtl );
                                break;
                        }

                        if ( _ti.value == "true" ) {
                            OptionalFieldsFlowPanel.Show( );
                        }

                    }

                    break;
                case "numeric":
                    MainValueControl = new NumericUpDown {
                        Top = 50,
                        Left = 4,
                        Width = 300,
                        Text = _ti.value,
                        Font = new Font( "Segoe UI", 20 ),
                        Maximum = 10000,
                        Minimum = -10000,
                        DecimalPlaces = 2,
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        HandleItemValueChange( MainValueControl.Text );
                    };
                    break;
                case "notes":
                    MainValueControl = new RichTextBox {
                        Top = 50,
                        Left = 4,
                        Width = 300,
                        Text = _ti.value,
                        Multiline = true,
                        WordWrap = true,
                        
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        HandleItemValueChange( MainValueControl.Text );
                    };
                    break;

                case "date":
                    MainValueControl = new DateTimePicker {
                        Top = 50,
                        Left = 4,
                        Width = 120,
                        Text = _ti.value,
                        CustomFormat = "dd-MMM-yyyy",
                        Format = DateTimePickerFormat.Custom
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        DateTimePicker dtp = ( DateTimePicker )MainValueControl;
                        HandleItemValueChange( dtp.Value.ToString( "dd-MMM-yyyy" ) );
                    };
                    break;
                case "dropdown":
                    MainValueControl = new ComboBox {
                        Top = 50,
                        Left = 4,
                        Width = 300,
                        Text = _ti.value,
                        Font = new Font( "Segoe UI", 20 )
                    };
                    ComboBox cmbMVC = ( ComboBox )MainValueControl;
                    for( int i = 0; i < _ti.dropdown_options.Count; i++ )
                    {
                        cmbMVC.Items.Add( _ti.dropdown_options[ i ] );
                    }

                    cmbMVC.SelectedIndexChanged += delegate ( object o, EventArgs e ) {
                        HandleItemValueChange( cmbMVC.Text );
                    };
                    break;
                case "text":
                default:
                    MainValueControl = new TextBox {
                        Top = 50,
                        Left = 4,
                        Width = 300,
                        Text = _ti.value,
                        Multiline = false,
                        Font = new Font( "Segoe UI", 20 )
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        HandleItemValueChange( MainValueControl.Text );
                        
                    };
                    break;

            }
            this.Controls.Add( MainValueControl );

        }

        private void HandleItemValueChange( string value ) {
            _value = value;
            if( HandleRecordDataUpdate ) {
                //Do the update here.
                Controllers.RecordDataUpdate rdu = RecordInstance.UpdateData( _ti.name, _value );
                if( rdu.UpdateSuccess ) {
                    this.ItemValueChanged( this, new TemplateItemEventArgs( _ti.name, _value ) );
                    //Emit events if we changed more than one value (usually related to calculations)
                    foreach( KeyValuePair<string, string> avi in rdu.AdditionalValuesUpdated ) {
                        this.ItemValueChanged( this, new TemplateItemEventArgs( avi.Key, avi.Value ) );
                    }
                }
            } else {
                //Just fire the event, don't save it.
                this.ItemValueChanged( this, new TemplateItemEventArgs( _ti.name, _value ) );
            }
        }
        private void HandleOptionalItemValueChange( TemplateItem optItem, string value ) {
            //Do the update here.
            this.ItemValueChanged( this, new TemplateItemEventArgs( optItem.name, value ) );
        }

        private void CtlTemplateItem_Load( object sender, EventArgs e ) {
            LoadData( );
        }
        private void CtlTemplateItem_VisibleChanged( object sender, EventArgs e ) {
            LoadData( );
        }

        private void LoadData( ) {
            if( RecordInstance != null && MainValueControl != null && HandleRecordDataUpdate ) {
                _value = RecordInstance.GetData( _ti.name );
                PopulateControls( );
            }
        }

        public void LoadData( string ValToLoad ) {
            _value = ValToLoad;
            PopulateControls( );
        }

        public void PopulateControls() {
            MainValueControl.Text = _value;
            if ( _ti.field_type == "true_false" ) {
                if ( _value == "true" ) {
                    Controls.Find( "BtnYes", true ).First().BackColor = Color.Green;
                    Controls.Find( "BtnNo", true ).First( ).BackColor = Color.Transparent;
                } else if ( _value == "false" ) {
                    Controls.Find( "BtnYes", true ).First( ).BackColor = Color.Transparent;
                    Controls.Find( "BtnNo", true ).First( ).BackColor = Color.Red;
                }
            } 
        }

        public void FocusMVC() {
            MainValueControl.Focus( );
        }
    }


    public class TemplateItemEventArgs : EventArgs {
        public string Key { get; set; }
        public string Value { get; set; }
        public TemplateItemEventArgs( string key, string val )
        {
            Key = key;
            Value = val;
        }
    }


}
