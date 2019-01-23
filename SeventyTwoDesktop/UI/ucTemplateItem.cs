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

    public partial class ucTemplateItem : UserControl
    {
        private TemplateItem ti { get; set; }

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

        public event EventHandler OutlineModeClick;
        public event EventHandler ItemValueChanged;
        public event EventHandler OptionalValueChanged;

        public ucTemplateItem()
        {
            InitializeComponent();
        }


        public void LoadTemplateItem( TemplateItem template ) {
            ti = template;
            _name = ti.Name;
            _group = ti.Group;
            if ( OutlineMode ) {
                GenerateOutlineControl( );
            } else {
                GenerateGuidanceControl( );
            }
        }

        private void GenerateOutlineControl( )
        {
            this.Controls.Add( new Label { Top =4, Left = 4, Text = ti.Title, AutoSize = true } );
         
            switch( ti.FieldType ) {
                case "true_false":
                    MainValueControl = new CheckBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Checked = (ti.Value == "true")
                    };
                    CheckBox cbMVC = ( CheckBox )MainValueControl;
                    cbMVC.CheckStateChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        ti.Value = cbMVC.Checked ? "true" : "false";
                        if( this.Focused ) {
                            this.ItemValueChanged( o, e );
                        }
                    };
                    
                    break;
                case "numeric":
                    MainValueControl = new NumericUpDown {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = ti.Value
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        ti.Value = MainValueControl.Text;
                        if( this.Focused ){
                            this.ItemValueChanged( o, e );
                        }
                    };
                    break;
                case "notes":
                    MainValueControl = new RichTextBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = ti.Value,
                        Multiline = true,
                        WordWrap = true
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        MainValueControl.Size = MainValueControl.GetPreferredSize( new Size( 192, 300 ) );
                        this.Height = MainValueControl.Height + 20;
                        ti.Value = MainValueControl.Text;
                        if( this.Focused ) {
                            this.ItemValueChanged( o, e );
                        }
                    };
                    break;
                case "dropdown":
                    MainValueControl = new ComboBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = ti.Value
                    };
                    ComboBox cmbMVC = ( ComboBox )MainValueControl;
                    for( int i = 0; i < ti.DropDownOptions.Count; i++ ) {
                        cmbMVC.Items.Add( ti.DropDownOptions[ i ] );
                    }

                    cmbMVC.SelectedIndexChanged += delegate ( object o, EventArgs e ) {
                        ti.Value = cmbMVC.Items[ cmbMVC.SelectedIndex ].ToString();
                        if( this.Focused ) {
                            this.ItemValueChanged( o, e );
                        }
                    };
                    break;
                case "text":
                default:
                    MainValueControl = new TextBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = ti.Value,
                        Multiline = false
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        ti.Value = MainValueControl.Text;
                        if( this.Focused ) {
                            this.ItemValueChanged( o, e );
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
                    this.OutlineModeClick( o, new TemplateItemEventArgs( ti.Name, ti.Value ) );
                }
            };

        }

        private void GenerateGuidanceControl( ) {
            this.Controls.Add( new Label { Top = 4, Left = 4, Width = 500, Text = ti.Title, AutoSize = true, Font = new Font( "Segoe UI", 20 ), TextAlign = ContentAlignment.MiddleCenter } );

            switch( ti.FieldType )
            {
                
                case "true_false":
                    MainValueControl = new CheckBox {
                        Top = 0,
                        Left = 0,
                        Checked = ( ti.Value == "true" ),
                        Visible = false
                    };
                    CheckBox cbMVC = ( CheckBox )MainValueControl;
                    cbMVC.CheckedChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _value = cbMVC.Checked ? "true" : "false";
                        //if( this.Focused ){
                            this.ItemValueChanged( o, new TemplateItemEventArgs( ti.Name, _value ) );
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

                    foreach( TemplateItem optVal in ti.OptionalFields ) {
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
                                        this.ItemValueChanged( o, new TemplateItemEventArgs( optVal.Name, notesCtl.Text ) );
                                    }
                                };
                                OptionalFieldsFlowPanel.Controls.Add( notesCtl );
                                break;
                            case "numeric":
                                NumericUpDown nudCtl = new NumericUpDown {
                                    Left = 4,
                                    Width = 130,
                                    Text = ti.Value,
                                    Font = new Font( "Segoe UI", 20 )
                                };
                                nudCtl.TextChanged += delegate ( object o, EventArgs e ) {
                                    //Raise the textchanged event.
                                    optVal.Value = nudCtl.Text;
                                    if( this.Focused ) {
                                        this.ItemValueChanged( o, new TemplateItemEventArgs( optVal.Name, nudCtl.Text ) );
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
                        Text = ti.Value,
                        Font = new Font( "Segoe UI", 20 )
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _value = MainValueControl.Text;
                        this.ItemValueChanged( o, new TemplateItemEventArgs( ti.Name, _value ) );
                    };
                    break;
                case "notes":
                    MainValueControl = new RichTextBox {
                        Top = 50,
                        Left = 4,
                        Width = 300,
                        Text = ti.Value,
                        Multiline = true,
                        WordWrap = true,
                        
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        MainValueControl.Size = MainValueControl.GetPreferredSize( new Size( 192, 300 ) );
                        this.Height = MainValueControl.Height + 20;
                        //Raise the textchanged event.
                        _value = MainValueControl.Text;
                        this.ItemValueChanged( o, new TemplateItemEventArgs( ti.Name, _value ) );
                    };
                    break;

                case "date":
                    MainValueControl = new DateTimePicker {
                        Top = 50,
                        Left = 4,
                        Width = 300,
                        Text = ti.Value
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _value = MainValueControl.Text;
                        this.ItemValueChanged( o, new TemplateItemEventArgs( ti.Name, _value ) );
                    };
                    break;
                case "dropdown":
                    MainValueControl = new ComboBox {
                        Top = 50,
                        Left = 4,
                        Width = 300,
                        Text = ti.Value,
                        Font = new Font( "Segoe UI", 20 )
                    };
                    ComboBox cmbMVC = ( ComboBox )MainValueControl;
                    for( int i = 0; i < ti.DropDownOptions.Count; i++ )
                    {
                        cmbMVC.Items.Add( ti.DropDownOptions[ i ] );
                    }

                    cmbMVC.SelectedIndexChanged += delegate ( object o, EventArgs e ) {
                        _value = cmbMVC.Text;
                        this.ItemValueChanged( o, new TemplateItemEventArgs( ti.Name, _value ) );
                    };
                    break;
                case "text":
                default:
                    MainValueControl = new TextBox {
                        Top = 50,
                        Left = 4,
                        Width = 300,
                        Text = ti.Value,
                        Multiline = false,
                        Font = new Font( "Segoe UI", 20 )
                    };
                    MainValueControl.TextChanged += delegate ( object o, EventArgs e ) {
                        //Raise the textchanged event.
                        _value = MainValueControl.Text;
                        this.ItemValueChanged( o, new TemplateItemEventArgs( ti.Name, _value ) );
                    };
                    break;

            }
            this.Controls.Add( MainValueControl );

        }


        private void ucTemplateItem_Load( object sender, EventArgs e )
        {

        }
        
    }


    public class TemplateItemEventArgs : EventArgs
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public TemplateItemEventArgs( string key, string val )
        {
            Key = key;
            Value = val;
        }
    }


}
