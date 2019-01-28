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
            _name = _ti.Name;
            _group = _ti.Group;
            if ( OutlineMode ) {
                GenerateOutlineControl( );
            } else {
                GenerateGuidanceControl( );
            }
        }

        private void GenerateOutlineControl( )
        {
            this.Controls.Add( new Label { Top =4, Left = 4, Text = _ti.Title, AutoSize = true } );
         
            switch( _ti.FieldType ) {
                case "true_false":
                    MainValueControl = new CheckBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Checked = (_ti.Value == "true")
                    };
                    CheckBox cbMVC = ( CheckBox )MainValueControl;
                    cbMVC.CheckStateChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _ti.Value = cbMVC.Checked ? "true" : "false";
                        if( this.Focused ) {
                            HandleItemValueChange( _ti.Value );
                        }
                    };
                    
                    break;
                case "numeric":
                    MainValueControl = new NumericUpDown {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = _ti.Value
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _ti.Value = MainValueControl.Text;
                        if( this.Focused ) {
                            HandleItemValueChange( _ti.Value );
                        }
                    };
                    break;
                case "notes":
                    MainValueControl = new RichTextBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = _ti.Value,
                        Multiline = true,
                        WordWrap = true
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        MainValueControl.Size = MainValueControl.GetPreferredSize( new Size( 192, 300 ) );
                        this.Height = MainValueControl.Height + 20;
                        _ti.Value = MainValueControl.Text;
                        if( this.Focused ) {
                            HandleItemValueChange( _ti.Value );
                        }
                    };
                    break;
                case "dropdown":
                    MainValueControl = new ComboBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = _ti.Value
                    };
                    ComboBox cmbMVC = ( ComboBox )MainValueControl;
                    for( int i = 0; i < _ti.DropDownOptions.Count; i++ ) {
                        cmbMVC.Items.Add( _ti.DropDownOptions[ i ] );
                    }

                    cmbMVC.SelectedIndexChanged += delegate ( object o, EventArgs e ) {
                        _ti.Value = cmbMVC.Items[ cmbMVC.SelectedIndex ].ToString();
                        if( this.Focused ) {
                            HandleItemValueChange( _ti.Value );
                        }
                    };
                    break;
                case "text":
                default:
                    MainValueControl = new TextBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = _ti.Value,
                        Multiline = false
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _ti.Value = MainValueControl.Text;
                        if( this.Focused ) {
                            HandleItemValueChange( _ti.Value );
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
                    this.OutlineModeClick( o, new TemplateItemEventArgs( _ti.Name, _ti.Value ) );
                }
            };

        }

        private void GenerateGuidanceControl( ) {
            this.Controls.Add( new Label { Top = 4, Left = 4, Width = 500, Text = _ti.Title, AutoSize = true, Font = new Font( "Segoe UI", 20 ), TextAlign = ContentAlignment.MiddleCenter } );

            switch( _ti.FieldType )
            {
                
                case "true_false":
                    MainValueControl = new CheckBox {
                        Top = 0,
                        Left = 0,
                        Checked = ( _ti.Value == "true" ),
                        Visible = false
                    };
                    CheckBox cbMVC = ( CheckBox )MainValueControl;
                    cbMVC.CheckedChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _value = cbMVC.Checked ? "true" : "false";
                        //if( this.Focused ){
                            HandleItemValueChange( _value );
                        //}
                    };
                    Button btnYes = new Button { Left = 10, Top = 40, Height = 100, Width = 150, Text = "YES", Font = new Font( "Segoe UI", 20 ) };
                    Button btnNo = new Button { Left = 170, Top = 40, Height = 100, Width = 150, Text = "NO", Font = new Font( "Segoe UI", 20 ) };

                    btnYes.Click += delegate ( object o, EventArgs e ) {
                        cbMVC.Checked = true;
                        btnYes.BackColor = Color.Green;
                        btnNo.BackColor = Color.Transparent;
                        if( HasOptionalFields ) {
                            OptionalFieldsFlowPanel.Show( );
                        }
                    };

                    btnNo.Click += delegate ( object o, EventArgs e ) {
                        cbMVC.Checked = false;
                        btnYes.BackColor = Color.Transparent;
                        btnNo.BackColor = Color.Red;
                        if( HasOptionalFields ) {
                            OptionalFieldsFlowPanel.Hide( );
                        }
                    };

                    
                    this.Controls.Add( btnYes );
                    this.Controls.Add( btnNo );

                    foreach( TemplateItem optVal in _ti.OptionalFields ) {
                        HasOptionalFields = true;

                        if( OptionalFieldsFlowPanel == null ) {
                            OptionalFieldsFlowPanel = new FlowLayoutPanel {
                                Visible = false, Top = 40, Left = 330, Width = 150, Height = 100
                            };
                            this.Controls.Add( OptionalFieldsFlowPanel );
                            OptionalFieldsFlowPanel.BringToFront( );
                        }

                        OptionalFieldsFlowPanel.Controls.Add( new Label { Top = 4, Left = 4, Text = optVal.Title, AutoSize = true } );

                        switch ( optVal.FieldType ) {
                            case "notes":

                                RichTextBox notesCtl = new RichTextBox {
                                    Left = 4,
                                    Width = 130,
                                    Height = 75,
                                    Text = optVal.Value,
                                    Multiline = true,
                                    WordWrap = true,
                                    ScrollBars = RichTextBoxScrollBars.Vertical
                                };
                                notesCtl.TextChanged += delegate ( object o, EventArgs e ) {
                                    optVal.Value = notesCtl.Text;
                                    if( this.Focused ) {
                                        HandleOptionalItemValueChange( optVal, notesCtl.Text );
                                    }
                                };
                                OptionalFieldsFlowPanel.Controls.Add( notesCtl );
                                break;
                            case "numeric":
                                NumericUpDown nudCtl = new NumericUpDown {
                                    Left = 4,
                                    Width = 130,
                                    Text = _ti.Value,
                                    Font = new Font( "Segoe UI", 20 )
                                };
                                nudCtl.TextChanged += delegate ( object o, EventArgs e ) {
                                    //Raise the textchanged event.
                                    if( this.Focused ) {
                                        HandleOptionalItemValueChange( optVal, nudCtl.Text );
                                    }
                                };
                                OptionalFieldsFlowPanel.Controls.Add( nudCtl );
                                break;
                        }

                    }

                    break;
                case "numeric":
                    MainValueControl = new NumericUpDown {
                        Top = 50,
                        Left = 4,
                        Width = 300,
                        Text = _ti.Value,
                        Font = new Font( "Segoe UI", 20 )
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
                        Text = _ti.Value,
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
                        Width = 300,
                        Text = _ti.Value,
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
                        Text = _ti.Value,
                        Font = new Font( "Segoe UI", 20 )
                    };
                    ComboBox cmbMVC = ( ComboBox )MainValueControl;
                    for( int i = 0; i < _ti.DropDownOptions.Count; i++ )
                    {
                        cmbMVC.Items.Add( _ti.DropDownOptions[ i ] );
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
                        Text = _ti.Value,
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
                Controllers.RecordDataUpdate rdu = RecordInstance.UpdateData( _ti.Name, _value );
                if( rdu.UpdateSuccess ) {
                    this.ItemValueChanged( this, new TemplateItemEventArgs( _ti.Name, _value ) );
                    //Emit events if we changed more than one value (usually related to calculations)
                    foreach( KeyValuePair<string, string> avi in rdu.AdditionalValuesUpdated ) {
                        this.ItemValueChanged( this, new TemplateItemEventArgs( avi.Key, avi.Value ) );
                    }
                }
            }
        }
        private void HandleOptionalItemValueChange( TemplateItem optItem, string value ) {
            //Do the update here.
            this.ItemValueChanged( this, new TemplateItemEventArgs( optItem.Name, value ) );
        }

        private void CtlTemplateItem_Load( object sender, EventArgs e ) {
            LoadData( );
        }
        private void CtlTemplateItem_VisibleChanged( object sender, EventArgs e ) {
            LoadData( );
        }

        private void LoadData( ) {
            if( RecordInstance != null && MainValueControl != null) {
                MainValueControl.Text = RecordInstance.GetData( _ti.Name );
            }
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
