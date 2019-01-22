using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace SeventyTwoDesktop.Models
{
    class Search
    {

        public Search(){
            // initialized
        }

        public List<ProfileListItem> SearchByProfileName( string searchTerm ) {

            List<ProfileListItem> retVal = new List<ProfileListItem>();
            
            //Here we need to read in a list of patients
            
            //Then compare them against the search term

            //Then add them into the list to later put into the listbox.

            //for ( var i = 0; i < searchMatches.Count; i++ )
            //{
            //    retVal.Add( searchMatches[i] );
            //}

           

            return retVal;
        }

    }
}
