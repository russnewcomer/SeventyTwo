using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventyTwoDesktop.Models
{
    public class CalendarItem {
        public string item_date;
        public string item_title;
        public string item_notes;
        public bool item_confirmed = false;
        public bool item_completed = false;
        public bool item_deleted = false;
        public string responsible_party;
        public string record_type;
        public string linked_record_guid;
        public string linked_profile_guid;
    }
}
