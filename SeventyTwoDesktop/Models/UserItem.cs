using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventyTwoDesktop.Models
{
    class UserItem: IComparable
    {
        public string Name { get; set; } = "";
        public string Number { get; set; } = "";

        public string DisplayText {
            //Display Text should only add the number in if they have a number
            get { return Name + ( Number != "" ? " - " + Number : "" ); }
        }
        public string GUID { get; set; } = "";
        public bool Active { get; set; } = false;

        public DateTime LastModified { get; set; } = DateTime.Now;

        public UserItem() {

        }

        public UserItem( string name, string number, string itemGUID ) {
            Name = name;
            Number = number;
            GUID = itemGUID;
        }

        public UserItem( string name, string number ) {
            Name = name;
            Number = number;
            GUID = Guid.NewGuid( ).ToString( );
            Active = true;
        }

        public override string ToString() {
            return DisplayText;
        }

        public int CompareTo(object o) {
            if( !( o is UserItem u ) ) {
                throw new Exception( "Object is not a UserItem" );
            }
            return GUID.CompareTo(u.GUID);
        }
    }
}
