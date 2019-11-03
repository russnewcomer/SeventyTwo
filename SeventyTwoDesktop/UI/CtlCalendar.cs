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

        private Dictionary<string, NodeInfoStruct> NodeInfo = new Dictionary<string, NodeInfoStruct>();

        private CalendarListController clc = new CalendarListController( );

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
            ActiveWeekBeginningDate = DateTime.Now;
            while( ActiveWeekBeginningDate.DayOfWeek != DayOfWeek.Saturday ) {
                ActiveWeekBeginningDate.AddDays( -1 );
            }
            LoadData( );
        }

        public void LoadData() {

            //Clear out the existing data
            TvCalendarItems.Nodes.Clear( ); 
            
            //Load data based on _firstDayOfActiveWeek
            for ( var i = 0; i < 7; i++ ) {
                CalendarDateController cdc = clc.GetCalendarItemsForDate( _firstDayOfActiveWeek.AddDays( i ) );
                string rootNodeName = cdc.DisplayDate + "-All";
                foreach ( CalendarItem calItem in cdc.Data.calendar_items ) {
                    //Terning right around - into the type of item.
                    var itemType = ( calItem.item_completed ) ? "Completed" : ( ( calItem.item_confirmed ) ? "Comfirmed" : "Scheduled" );
                    var dateItemNodeName = cdc.DisplayDate + "-" + itemType;

                    //Check to see if we have the root 'Scheduled/Confirmed/Completed' nodes.
                    if ( !TvCalendarItems.Nodes.ContainsKey( rootNodeName ) ) {
                        TvCalendarItems.Nodes.Add( rootNodeName, cdc.DisplayDate );
                    }
                    if( !TvCalendarItems.Nodes[ rootNodeName ].Nodes.ContainsKey( dateItemNodeName ) ) {
                        TvCalendarItems.Nodes[ rootNodeName ].Nodes.Add( dateItemNodeName, itemType );
                    }
                    //Add in the appropriate nodes.
                    string nodeInfoKey = Guid.NewGuid( ).ToString( );
                    TvCalendarItems.Nodes[ rootNodeName ].Nodes[ dateItemNodeName ].Nodes.Add( nodeInfoKey, calItem.item_title );
                    Console.WriteLine( nodeInfoKey );
                    NodeInfo.Add( nodeInfoKey, new NodeInfoStruct { Profile_guid = calItem.linked_profile_guid, Template_type = calItem.record_type, Record_guid = calItem.linked_record_guid } );
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

        private void CalDate_ClickedEventHandler( object sender, EventArgs e ) {
            CalDateClickEventArgs cdcea = ( CalDateClickEventArgs )e;
            string baseNodeKey = cdcea.Date + "-All";
            if( cdcea.AppointmentType == CalendarAppointmentType.All ) {
                if( TvCalendarItems.Nodes[ baseNodeKey ].IsExpanded ) {
                    TvCalendarItems.Nodes[ baseNodeKey ].Collapse( );
                } else {
                    TvCalendarItems.Nodes[ baseNodeKey ].Expand( );
                }
                TvCalendarItems.TopNode = TvCalendarItems.Nodes[ baseNodeKey ];
            } else {
                string subKey = cdcea.Date + "-" + cdcea.AppointmentType.ToString( );
                if( TvCalendarItems.Nodes[ baseNodeKey ].Nodes.Count > 0 ) {
                    TvCalendarItems.Nodes[ baseNodeKey ].Nodes[ subKey ].Expand( );
                    TvCalendarItems.TopNode = TvCalendarItems.Nodes[ baseNodeKey ].Nodes[ subKey ];
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
            AppointmentCalendarItemClicked( e.Node.Name );
        }

        #endregion
    }

    public class AppointmentHandlingEventArgs : EventArgs
    {
        public NodeInfoStruct Node;
        public AppointmentHandlingEventArgs( NodeInfoStruct creationNode )
        {
            Node = creationNode;
        }
    }
    
    public struct NodeInfoStruct
    {
        public string Profile_guid;
        public string Template_type;
        public string Record_guid;
    }

}
