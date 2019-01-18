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

        public ucTemplateItem()
        {
            InitializeComponent();
        }
        

        public void LoadTemplateItem( TemplateItem template ) {
            ti = template;
           
            lblTitle.Text = ti.Title;
           

            switch( ti.FieldType ) {
                case "true_false":
                    MainValueControl = new CheckBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Checked = (ti.Value == "true")
                    };
                    break;
                case "numeric":
                    MainValueControl = new NumericUpDown {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = ti.Value
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
                    };
                    break;
                case "dropdown":
                    MainValueControl = new ComboBox {
                        Top = 20,
                        Left = 4,
                        Width = 192,
                        Text = ti.Value
                    };
                    ComboBox cbMVC = ( ComboBox )MainValueControl;
                    for( int i = 0; i < ti.DropDownOptions.Count; i++ ) {
                        cbMVC.Items.Add( ti.DropDownOptions[ i ] );
                    }
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
                    break;

            }
            this.Controls.Add( MainValueControl );

        }

        private void ucTemplateItem_Load( object sender, EventArgs e )
        {

        }
    }
}
