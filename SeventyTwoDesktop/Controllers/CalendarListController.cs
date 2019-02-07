using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeventyTwoDesktop.Models;
using Newtonsoft.Json;

namespace SeventyTwoDesktop.Controllers
{
    class CalendarListController {

        public static string GetDateString( DateTime DateToGetStringFor ) {
            return DateToGetStringFor.ToString( "dd-MMM-yyyy" );
        }
        public static DateTime GetDateTime( string StringToGetDateFor ) {
            return DateTime.Parse( StringToGetDateFor );
        }

        //This is our basic list of dates
        private Dictionary<string, CalendarDateController> Calendar { get; set; }
        private List<DateTime> DaysWithScheduledAppointments { get; set; }

        public event EventHandler CalendarItemAdded;

        //Basic controller
        public CalendarListController(  ) {
            //We need to read a list of dates into DaysWithScheduledAppointments

        }

        private void VerifyCalendarItemForDate( string dateString ) {
            if( !Calendar.ContainsKey( dateString ) ) {
                Calendar.Add( dateString, new CalendarDateController( GetDateTime( dateString ) ) ); 
            }
        }

        public CalendarDateController GetCalendarItemsForDate( string dateString ) {
            VerifyCalendarItemForDate( dateString );
            return Calendar[ dateString ];
        }

        public void AddCalendarItem( CalendarItem ci ) {
            VerifyCalendarItemForDate( ci.item_date );
            Calendar[ ci.item_date ].AddCalendarItem( ci );
            this.CalendarItemAdded?.Invoke( this, new CalendarItemEventArgs( ci ) );
        }

    }

    public class CalendarItemEventArgs : EventArgs {
        public CalendarItem Item { get; set; }
        public CalendarItemEventArgs( CalendarItem item ) { Item = item; }
    }
}
