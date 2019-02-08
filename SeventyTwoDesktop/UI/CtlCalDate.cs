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

namespace SeventyTwoDesktop.UI
{
    public partial class CtlCalDate : UserControl
    {

        private CalendarDateController CDC { get; set; }

        public event EventHandler Clicked;
           
        public CtlCalDate( )
        {
            InitializeComponent( );
        }

        public void LoadData( CalendarDateController Cal ) {
            CDC = Cal;
            LblDay.Text = CDC.Date.ToString( "d-MMM" );
            switch( CDC.Date.DayOfWeek ) {
                case DayOfWeek.Friday:
                    lblDayOfWeek.Text = "F";
                    break;
                case DayOfWeek.Saturday:
                    lblDayOfWeek.Text = "Sa";
                    break;
                case DayOfWeek.Sunday:
                    lblDayOfWeek.Text = "Su";
                    break;
                case DayOfWeek.Monday:
                    lblDayOfWeek.Text = "M";
                    break;
                case DayOfWeek.Tuesday:
                    lblDayOfWeek.Text = "Tu";
                    break;
                case DayOfWeek.Wednesday:
                    lblDayOfWeek.Text = "W";
                    break;
                case DayOfWeek.Thursday:
                    lblDayOfWeek.Text = "Th";
                    break;
                default:
                    lblDayOfWeek.Text = "";
                    break;
            }
            LblScheduled.Text = CDC.GetNumberOfScheduledItemsForToday( ).ToString( ) + " Sched.";
            LblConfirmed.Text = CDC.GetNumberOfConfirmedItemsForToday( ).ToString( ) + " Conf.";
            LblCompleted.Text = CDC.GetNumberOfCompletedItemsForToday( ).ToString( ) + " Complt.";

        }

        private void CtlCalDate_Click( object sender, EventArgs e ) {
            if ( CDC != null ) {
                this.Clicked?.Invoke( this, new CalDateClickEventArgs( CDC.JSONDate ) );
            } else {
                this.Clicked?.Invoke( this, new CalDateClickEventArgs( CalendarListController.GetDateString( DateTime.Now ) ) );
            }
        }

        private void Control_Click( object sender, EventArgs e ) {
            if( CDC != null ) {
                this.Clicked?.Invoke( this, new CalDateClickEventArgs( CDC.JSONDate ) );
            } else {
                this.Clicked?.Invoke( this, new CalDateClickEventArgs( CalendarListController.GetDateString( DateTime.Now ) ) );
            }
        }

    }
    public class CalDateClickEventArgs : EventArgs
    {
        public string Date { get; set; }
        public CalDateClickEventArgs( string dt ) {
            Date = dt;
        }
    }
}
