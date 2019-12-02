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
        public static double TimeDiff( DateTime date1, DateTime date2, string unit )
        {
            TimeSpan diff = ( date1 - date2 );
            double nowDiffTargetValue = 0;
            switch( unit ) {
                case "s":
                    nowDiffTargetValue = Math.Floor( diff.TotalSeconds );
                    break;
                case "m":
                    nowDiffTargetValue = Math.Floor( diff.TotalMinutes );
                    break;
                case "h":
                    nowDiffTargetValue = Math.Floor( diff.TotalHours );
                    break;
                case "d":
                    nowDiffTargetValue = Math.Floor( diff.TotalDays );
                    break;
                case "M":
                    nowDiffTargetValue = (double) Math.Ceiling( ( decimal )diff.TotalDays / 30 );
                    break;
                case "y":
                    nowDiffTargetValue = (double) Math.Ceiling( ( decimal )diff.TotalDays / 365 );
                    break;
            }
            return nowDiffTargetValue;
        }
        public static DateTime AddTime( DateTime beginDate, string units, int timeToAdd ) {
            DateTime retVal;
            switch( units ) {
                case "s":
                    retVal = beginDate.AddSeconds( timeToAdd );
                    break;
                case "m":
                    retVal = beginDate.AddMinutes( timeToAdd );
                    break;
                case "h":
                    retVal = beginDate.AddHours( timeToAdd );
                    break;
                case "d":
                    retVal = beginDate.AddDays( timeToAdd );
                    break;
                case "M":
                    retVal = beginDate.AddMonths( timeToAdd );
                    break;
                case "y":
                    retVal = beginDate.AddYears( timeToAdd );
                    break;
                default:
                    retVal = beginDate;
                    break;
            }
            return retVal;
        }

        //This is our basic list of dates
        private Dictionary<string, CalendarDateController> Calendar { get; set; } = new Dictionary<string, CalendarDateController>( );

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

        public CalendarDateController GetCalendarItemsForDate( DateTime dt ) {
            string dateString = GetDateString( dt );
            VerifyCalendarItemForDate( dateString );
            return Calendar[ dateString ];
        }

        public void AddCalendarItem( CalendarItem ci ) {
            VerifyCalendarItemForDate( ci.item_date );
            Calendar[ ci.item_date ].AddCalendarItem( ci );
            this.CalendarItemAdded?.Invoke( this, new CalendarItemEventArgs( ci ) );
        }

        public void UpdateCalendarItem( CalendarItem ci ) {
            VerifyCalendarItemForDate( ci.item_date );
            Calendar[ ci.item_date ].UpdateCalendarItem( ci );
            this.CalendarItemAdded?.Invoke( this, new CalendarItemEventArgs( ci ) );
        }
       

    }

    public class CalendarItemEventArgs : EventArgs {
        public CalendarItem Item { get; set; }
        public CalendarItemEventArgs( CalendarItem item ) { Item = item; }
    }
}
