using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeventyTwoDesktop.Controllers;
using SeventyTwoDesktop.Models;

namespace SeventyTwoDesktop.UI
{


    public partial class CtlCalendar : UserControl
    {

        private Dictionary<string, CalendarItem> NodeInfo = new Dictionary<string, CalendarItem>();

        private CalendarListController clc = new CalendarListController( );

        private Guid SelectedNode;

        private DateTime _firstDayOfActiveWeek { get; set; } = new DateTime( 2019, 10, 26 );
        public DateTime ActiveWeekBeginningDate {
            get { return _firstDayOfActiveWeek; }
            set {
                _firstDayOfActiveWeek = value;
                LblApptWeek.Text = "Week Beginning " + CalendarListController.GetDateString( _firstDayOfActiveWeek );
                LoadData( );
            }
        }


        public event EventHandler AppointmentClicked;


        public CtlCalendar( ) {
            InitializeComponent( );
            SetStartingDate( );
            LoadData( );
        }

        public void SetStartingDate() {
            ActiveWeekBeginningDate = DateTime.Now;
            while ( ActiveWeekBeginningDate.DayOfWeek != DayOfWeek.Saturday ) {
                ActiveWeekBeginningDate = ActiveWeekBeginningDate.AddDays( -1 );
            }
        }

        public void LoadData() {

            //Clear out the existing data
            TvCalendarItems.Nodes.Clear( );
            NodeInfo.Clear( );

            //Load data based on _firstDayOfActiveWeek
            for ( var i = 0; i < 7; i++ ) {
                CalendarDateController cdc = clc.GetCalendarItemsForDate( _firstDayOfActiveWeek.AddDays( i ) );
                string rootNodeName = cdc.DisplayDate + "-All";
                foreach ( CalendarItem calItem in cdc.Data.calendar_items.Where( cal => !cal.item_cancelled ) ) {
                    //Terning right around - into the type of item.
                    var itemType = ( calItem.item_cancelled ? "Cancelled" : ( calItem.item_completed ) ? "Completed" : ( ( calItem.item_confirmed ) ? "Comfirmed" : "Scheduled" ) );
                    var dateItemNodeName = cdc.DisplayDate + "-" + itemType;

                    //Check to see if we have the root 'Scheduled/Confirmed/Completed' nodes.
                    if ( !TvCalendarItems.Nodes.ContainsKey( rootNodeName ) ) {
                        TvCalendarItems.Nodes.Add( rootNodeName, cdc.DisplayDate );
                        TvCalendarItems.Nodes[ rootNodeName ].ToolTipText = "Right-click nodes below to change status";
                    }
                    if( !TvCalendarItems.Nodes[ rootNodeName ].Nodes.ContainsKey( dateItemNodeName ) ) {
                        TvCalendarItems.Nodes[ rootNodeName ].Nodes.Add( dateItemNodeName, itemType );
                    }
                    //Add in the appropriate nodes.
                    
                    TvCalendarItems.Nodes[ rootNodeName ].Nodes[ dateItemNodeName ].Nodes.Add( calItem.item_guid, calItem.item_title );
                    TvCalendarItems.Nodes[ rootNodeName ].Nodes[ dateItemNodeName ].Nodes[ calItem.item_guid ].ToolTipText = "Right-click to change status";
                    NodeInfo.Add( calItem.item_guid, calItem );
                }
            }

            CalDate1.LoadData( clc.GetCalendarItemsForDate( _firstDayOfActiveWeek ) );
            CalDate2.LoadData( clc.GetCalendarItemsForDate( _firstDayOfActiveWeek.AddDays( 1 ) ) );
            CalDate3.LoadData( clc.GetCalendarItemsForDate( _firstDayOfActiveWeek.AddDays( 2 ) ) );
            CalDate4.LoadData( clc.GetCalendarItemsForDate( _firstDayOfActiveWeek.AddDays( 3 ) ) );
            CalDate5.LoadData( clc.GetCalendarItemsForDate( _firstDayOfActiveWeek.AddDays( 4 ) ) );
            CalDate6.LoadData( clc.GetCalendarItemsForDate( _firstDayOfActiveWeek.AddDays( 5 ) ) );
            CalDate7.LoadData( clc.GetCalendarItemsForDate( _firstDayOfActiveWeek.AddDays( 6 ) ) );


        }

        public void AddCalendarItem( CalendarItem itm ) {
            clc.AddCalendarItem( itm );
        }

        private void AppointmentCalendarItemClicked( string NodeName ) {
            if( NodeInfo.ContainsKey( NodeName ) ) {
                this.AppointmentClicked?.Invoke( this, new AppointmentHandlingEventArgs( NodeInfo[ NodeName ] ) );
            }
        }

        #region Event Handlers

            //This event handler is for all of the calendar date controls
        private void CalDate_ClickedEventHandler( object sender, EventArgs e ) {
            CalDateClickEventArgs cdcea = ( CalDateClickEventArgs )e;
            string baseNodeKey = cdcea.Date + "-All";
            //If we are clicking the base node
            if( cdcea.AppointmentType == CalendarAppointmentType.All ) {
                //Expand or contract as necessary
                if( TvCalendarItems.Nodes[ baseNodeKey ].IsExpanded ) {
                    TvCalendarItems.Nodes[ baseNodeKey ].Collapse( );
                } else {
                    TvCalendarItems.Nodes[ baseNodeKey ].Expand( );
                }
                //Set the top node
                TvCalendarItems.TopNode = TvCalendarItems.Nodes[ baseNodeKey ];
            } else {
                //The subkey is for the sub appointments.  We need to to differentiate from the baseKey
                string subKey = cdcea.Date + "-" + cdcea.AppointmentType.ToString( );
                if( TvCalendarItems.Nodes[ baseNodeKey ].Nodes.ContainsKey( subKey ) ) {
                    if ( TvCalendarItems.Nodes[ baseNodeKey ].Nodes[ subKey ].IsExpanded ) {
                        TvCalendarItems.Nodes[ baseNodeKey ].Nodes[ subKey ].Collapse( );
                    } else {
                        TvCalendarItems.Nodes[ baseNodeKey ].Nodes[ subKey ].Expand( );
                        TvCalendarItems.TopNode = TvCalendarItems.Nodes[ baseNodeKey ].Nodes[ subKey ];
                    }
                } else {
                    TvCalendarItems.Nodes[ baseNodeKey ].Expand( );
                }
            }
        }

        private void BtnNextWeek_Click( object sender, EventArgs e ) {
            ActiveWeekBeginningDate = _firstDayOfActiveWeek.AddDays( 7 );
        }

        private void BtnPrevWeek_Click( object sender, EventArgs e ) {
            ActiveWeekBeginningDate = _firstDayOfActiveWeek.AddDays( -7 );
        }

        private void LblApptWeek_Click( object sender, EventArgs e ) {
            TvCalendarItems.CollapseAll( );
        }
       
        private void TvCalendarItems_NodeMouseDoubleClick( object sender, TreeNodeMouseClickEventArgs e ) {
            if ( Guid.TryParse( e.Node.Name, out SelectedNode ) ) {
                AppointmentCalendarItemClicked( SelectedNode.ToString( ) );
            }
        }

        private void TvCalendarItems_NodeMouseClick( object sender, TreeNodeMouseClickEventArgs e ) {

            //Since we are storing the GUID in the name, see if the name parses out to a GUID
            if ( Guid.TryParse( e.Node.Name, out SelectedNode ) ) {
                //Have to add the Calendar left/right so that the menu spawns in the correct spot.
                cmsCalStrip.Show( this, new Point( e.X + TvCalendarItems.Left, e.Y + TvCalendarItems.Top ) );
            }
        }

        private void tsmOpen_Click( object sender, EventArgs e ) {
            AppointmentCalendarItemClicked( SelectedNode.ToString( ) );
        }

        #endregion

        private void scheduledToolStripMenuItem_Click( object sender, EventArgs e ) {
            NodeInfo[ SelectedNode.ToString( ) ].item_confirmed = false;
            NodeInfo[ SelectedNode.ToString( ) ].item_completed = false;
            clc.UpdateCalendarItem( NodeInfo[ SelectedNode.ToString( ) ] );
            LoadData( );
        }

        private void confirmedToolStripMenuItem_Click( object sender, EventArgs e ) {
            NodeInfo[ SelectedNode.ToString( ) ].item_confirmed = true;
            NodeInfo[ SelectedNode.ToString( ) ].item_completed = false;
            clc.UpdateCalendarItem( NodeInfo[ SelectedNode.ToString( ) ] );
            LoadData( );
        }

        private void completedToolStripMenuItem_Click( object sender, EventArgs e ) {
            NodeInfo[ SelectedNode.ToString( ) ].item_confirmed = true;
            NodeInfo[ SelectedNode.ToString( ) ].item_completed = true;
            clc.UpdateCalendarItem( NodeInfo[ SelectedNode.ToString( ) ] );
            LoadData( );
        }

        private void deletedToolStripMenuItem_Click( object sender, EventArgs e ) {
            if ( MessageBox.Show("Are you sure you want to cancel this appointment?", "Delete", MessageBoxButtons.YesNo ) == DialogResult.Yes ) { 
                NodeInfo[ SelectedNode.ToString( ) ].item_cancelled = true;
                clc.UpdateCalendarItem( NodeInfo[ SelectedNode.ToString( ) ] );
                LoadData( );
            }
        }
    }

    public class AppointmentHandlingEventArgs : EventArgs
    {
        public CalendarItem Node;
        public AppointmentHandlingEventArgs( CalendarItem creationNode )
        {
            Node = creationNode;
        }
    }

}
