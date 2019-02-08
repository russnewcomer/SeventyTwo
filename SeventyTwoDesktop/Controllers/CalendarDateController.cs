using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeventyTwoDesktop.Models;

namespace SeventyTwoDesktop.Controllers
{
    public class CalendarDateController {
        public CalendarDate Data { get; set; }
        public string JSONDate { get; }
        private string FileName { get; set; } 
        public DateTime Date { get; }
        private FileReadWriteController FileController { get; set; }
        
        public CalendarDateController( DateTime CalDate ) {
            Date = CalDate;
            JSONDate = Date.ToString( "yyyy-MM-dd" );
            FileName = "calendar/" + JSONDate + ".json";
            FileController = new FileReadWriteController( FileName );
            ReadFile( );
        }

        private void ReadFile( ) {
            FileController.ForceRead( );
            if ( string.IsNullOrEmpty( FileController.FileContents ) ) {
                Data = new CalendarDate( );
            } else {
                Data = JsonConvert.DeserializeObject<CalendarDate>( FileController.FileContents );
            }

        }


        public void WriteFile( ) {
            FileController.WriteDataToFile( JsonConvert.SerializeObject( Data ) );
        }


        public void AddCalendarItem( CalendarItem ci ) {

            //Add the item to the date
            Data.calendar_items.Add( ci );
            //Save the date
            WriteFile( );

        }

        public int GetNumberOfScheduledItemsForToday( ) {
            int retVal = 0;
            try {
                //Get the number of schedule items for today.
            } catch ( Exception exc ) { Models.Log.WriteToLog( exc ); }
            return retVal;
        }

        public int GetNumberOfConfirmedItemsForToday( )
        {
            int retVal = 0;
            try {
                //Get the number of schedule items for today.
            } catch( Exception exc ) { Models.Log.WriteToLog( exc ); }
            return retVal;
        }

        public int GetNumberOfCompletedItemsForToday( )
        {
            int retVal = 0;
            try {
                //Get the number of schedule items for today.
            } catch( Exception exc ) { Models.Log.WriteToLog( exc ); }
            return retVal;
        }

    }
}
