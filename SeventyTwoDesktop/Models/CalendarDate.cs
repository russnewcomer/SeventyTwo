using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventyTwoDesktop.Models
{
    public class CalendarDate {
        public List<CalendarItem> calendar_items = new List<CalendarItem>();
        public string calendar_date;
        public CalendarDate( string dt ) {
            calendar_date = dt;
        }
    }
}
