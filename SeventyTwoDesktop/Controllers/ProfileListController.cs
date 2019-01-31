using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SeventyTwoDesktop.Models;

namespace SeventyTwoDesktop.Controllers
{
    static class ProfileListController {
        
        public static List<ProfileListItem> ProfileList {get; set;} = new List<ProfileListItem>();
        private static DateTime TimeSinceLastWrite { get; set; } = DateTime.Now;
        private static bool RequestedWrite { get; set; } = false;


        static ProfileListController( ) {
            LoadList( );
        }

        public static bool LoadList() {
            bool retVal = false;
            try {
                //Open the list and deserialize it
                string json = File.ReadAllText( "profiles/list.json" );
                ProfileList = JsonConvert.DeserializeObject<List<ProfileListItem>>(json);
                retVal = true;
            } catch (Exception exc) {
                Log.WriteToLog(exc);
                ProfileList = new List<ProfileListItem>();
            }
            return retVal;
        }

        public static bool RequestSave() {
            bool retVal = false;
            try {
                //Do the saving of profiles here, only writing at a max once every 5 seconds.
            } catch( Exception exc ) { Log.WriteToLog( exc ); }
            return retVal;
        }

        public static bool SaveList() {
            bool retVal = false;
            try { 
                if ( ProfileList.Count > 0 ) {
                    //Make a quick copy of the old list
                    if (File.Exists( "profiles/old-list.json" ) ) {
                        File.Delete( "profiles/old-list.json" );
                    }
                    //Make a quick copy of the old list
                    File.Copy( "profiles/list.json", "profiles/old-list.json" );
                    File.WriteAllText( "profiles/list.json", JsonConvert.SerializeObject(ProfileList));
                    TimeSinceLastWrite = DateTime.Now;
                    retVal = true;
                }
            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
            return retVal;
        }

        public static bool AddItemToList( ProfileListItem newItem ) {
            bool retVal = false;
            try {
                ProfileList.Add( newItem );
                SaveList();
            } catch( Exception exc ) { Log.WriteToLog( exc ); }
            return retVal;
        }

        public static bool AlterExistingItem( ProfileListItem itemToAlter ) {
            bool retVal = false;
            try {
                int idx = ProfileList.FindIndex( pli => pli.GUID == itemToAlter.GUID );
                if ( idx > -1 ) {
                    ProfileList[idx] = itemToAlter;
                    SaveList();
                    retVal = true;
                }
            } catch( Exception exc ) { Log.WriteToLog( exc ); }
            return retVal;
        }
    }
}
