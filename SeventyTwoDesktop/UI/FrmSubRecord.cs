using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeventyTwoDesktop.Models;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop.UI
{

    public partial class FrmSubRecord : Form
    {
        
        private CtlTemplateItem ActiveGuidanceItem { get; set; }
        private JObject Record { get; set; } = new JObject( );
        private Dictionary<string, TemplateItem> SubrecordItems { get; set; }
        

        public event EventHandler RecordChanged;

        public FrmSubRecord( )
        {
            InitializeComponent( );
        }

        private void BtnPrevious_Click( object sender, EventArgs e ) {
            if( TvViewNodes.SelectedNode.PrevNode != null ) {
                TvViewNodes.SelectedNode = TvViewNodes.SelectedNode.PrevNode;
                EnableGuidanceItem( TvViewNodes.SelectedNode.Name );
            }
            
        }

        private void BtnNext_Click( object sender, EventArgs e ) {
            if( TvViewNodes.SelectedNode.NextNode != null ) {
                TvViewNodes.SelectedNode = TvViewNodes.SelectedNode.NextNode;
                EnableGuidanceItem( TvViewNodes.SelectedNode.Name );
            }
        }

        public JObject GetRecordResults() {
            return Record;
        }

        public void SetTitleText( string TitleToSet ) {
            LblRecordTitle.Text = TitleToSet;
        }

        public bool LoadTemplate( Dictionary<string, Models.TemplateItem> sri, Controllers.RecordController rc ){
            bool retVal = false;

            try {
                SubrecordItems = sri;
                foreach( KeyValuePair<string, Models.TemplateItem> ti in SubrecordItems ) {
                    if( ti.Value.SubrecordItems.Count == 0 ) {
                        CtlTemplateItem guidanceItem = new CtlTemplateItem {
                            OutlineMode = false,
                            Name = "ucti" + ti.Value.Name,
                            RecordInstance = rc,
                            HandleRecordDataUpdate = false,
                            Visible = false
                        };
                        guidanceItem.ItemValueChanged += delegate ( object o, EventArgs e ) {
                            //MessageBox.Show( guidanceItem.ItemValue );
                            TemplateItemEventArgs tiea = ( TemplateItemEventArgs )e;
                            string displayText = tiea.Value;

                            TemplateItem displayItem = sri[ tiea.Key ];

                            //Terning right around... to show yes/no instead of True/False
                            displayText = ( displayText == "true" ) ? "Yes" : ( displayText == "false" ) ? "No" : displayText;

                            TvViewNodes.Nodes[ tiea.Key ].Text = displayItem.Title + " - " + displayText;
                            Record[ tiea.Key ] = tiea.Value;

                            //Send the event that the record changed.
                            this.RecordChanged?.Invoke( this, e );

                        };

                        guidanceItem.LoadTemplateItem( ti.Value );

                        PnlControls.Controls.Add( guidanceItem );
                    } else {
                        //This should probably create a new form and pop it up.  There's certainly a better way, but I'm not thinking of it right now.

                    }
                    
                    TvViewNodes.Nodes.Add( ti.Value.Name, ti.Value.Title );

                }

                //Select the first node.
                TvViewNodes.SelectedNode = TvViewNodes.Nodes[ 0 ];
                EnableGuidanceItem( TvViewNodes.Nodes[ 0 ].Name );

            } catch ( Exception exc ) { Models.Log.WriteToLog( exc ); }


            return retVal;
        }

        public void LoadTemplateData( JObject RecordData ) {
            Record = RecordData;
            foreach( KeyValuePair<string, JToken> kvp in RecordData ) {
                string valueToDisplay = kvp.Value.ToString( );
                TvViewNodes.Nodes[ kvp.Key ].Text = SubrecordItems[kvp.Key].Title + " - " + valueToDisplay;
            } 
        }

        private void TvViewNodes_NodeMouseClick( object sender, TreeNodeMouseClickEventArgs e ) {
            if( e.Node.Name != "" ) {
                EnableGuidanceItem( e.Node.Name );
            }
        }

        private void EnableGuidanceItem( string Key ) {

            ActiveGuidanceItem?.Hide( );

            foreach( CtlTemplateItem ctl in PnlControls.Controls.Find( "ucti" + Key, false ) ) {
                ActiveGuidanceItem = ctl;
                //Load the record data
                if( Record.ContainsKey( Key ) ) {
                    ActiveGuidanceItem.LoadData( Record[ Key ].ToString( ) );
                }
                ActiveGuidanceItem.Show( );
                ActiveGuidanceItem.FocusMVC( );
            }
            
        }

        private void BtnFinished_Click( object sender, EventArgs e ) {

            //Fire the form that the record changed.
            this.RecordChanged?.Invoke( this, e );
            //Close the form
            Close( );
        }
    }
}
