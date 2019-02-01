using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventyTwoDesktop.Models
{
    class ProfileListItem: IComparable
    {
        public string Name { get; set; } = "";
        public string Number { get; set; } = "";

        public string DisplayText {
            get { return Name + " - " + Number; }
        }
        public string GUID { get; set; } = "";

        public ProfileListItem() {

        }

        public ProfileListItem( string name, string number, string itemGUID ) {
            Name = name;
            Number = number;
            GUID = itemGUID;
        }

        public override string ToString() {
            return DisplayText;
        }

        public int CompareTo(object o) {
            if( !( o is ProfileListItem pli ) ) {
                throw new Exception( "Object is not a ProfileListItem" );
            }
            return GUID.CompareTo(pli.GUID);
        }
    }
}
