using System;
using System.Collections.Generic;

namespace SeventyTwoDesktop.Models
{
    public class ProfileItem
    {
        public string type = "permanent";
        public string title = "Profile Permanent Record";
        public string guid { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string address { get; set; }
        public string community { get; set; }
        public string location { get; set; }
        public string gender { get; set; }
        public DateTime birthdate { get; set; } = new DateTime( 2000, 1, 1 );
        public string phonenumber { get; set; }
        public List<string> recordArray { get; set; }

        public ProfileItem()
        {
        }
    }
}
