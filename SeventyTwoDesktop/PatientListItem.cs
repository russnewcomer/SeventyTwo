using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventyTwoDesktop
{
    class PatientListItem
    {

        private string DisplayText { get; set; } = "";
        public string GUID { get; set; } = "";

        public PatientListItem()
        {

        }

        public PatientListItem( string text, string itemGUID )
        {
            DisplayText = text;
            GUID = itemGUID;
        }

        public override string ToString()
        {

            return DisplayText;
        }

    }
}
