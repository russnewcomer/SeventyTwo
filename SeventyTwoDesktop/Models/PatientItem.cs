using System;
using System.Collections.Generic;

namespace SeventyTwoDesktop
{
    class PatientItem
    {
        public string type = "permanent";
        public string title = "Patient Permanent Record";
        public string guid { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string spouse { get; set; }
        public DateTime birthdate { get; set; }
        public string phonenumber { get; set; }
        public List<string> recordArray { get; set; }

        public PatientItem()
        {
        }
    }
}
