using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventyTwoDesktop.Models
{
    class PatientListItem: IComparable
    {
        public string Name { get; set; } = "";
        public string Number { get; set; } = "";

        public string DisplayText
        {
            get { return Name + " - " + Number; }
        }
        public string GUID { get; set; } = "";

        public PatientListItem()
        {

        }

        public PatientListItem( string name, string number, string itemGUID )
        {
            Name = name;
            Number = number;
            GUID = itemGUID;
        }

        public override string ToString()
        {
            return DisplayText;
        }

        public int CompareTo(Object o)
        {
            PatientListItem pli = o as PatientListItem;
            if ( pli == null )
            {
                throw new Exception("Object is not a PatientListItem");
            }
            return GUID.CompareTo(pli.GUID);
        }
    }
}
