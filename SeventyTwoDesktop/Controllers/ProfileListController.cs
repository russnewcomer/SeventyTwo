using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SeventyTwoDesktop.Models;

namespace SeventyTwoDesktop.Controllers
{
    static class ProfileListController {

        private static string LIST_FILE_PATH = FileReadWriteController.addPath( "data", "profile_list.json" );
        private static string LIST_DUPLICATE_FILE_PATH = FileReadWriteController.addPath( "data", "backup_user_list.json" );

        public static List<ProfileListItem> ProfileList {get; set;} = new List<ProfileListItem>();
        private static FileReadWriteController FileController { get; set; } = new FileReadWriteController( LIST_FILE_PATH, LIST_DUPLICATE_FILE_PATH );

        static ProfileListController( ) {
            LoadList( );
        }

        public static bool LoadList() {
            bool retVal = false;
            try {
                //Open the list and deserialize it
                FileController.ForceRead( );
                string JSON = FileController.FileContents;
                if( string.IsNullOrEmpty( JSON ) ) {
                    ProfileList = new List<ProfileListItem>( );
                } else {
                    ProfileList = JsonConvert.DeserializeObject<List<ProfileListItem>>( JSON );
                }
                retVal = true;
            } catch (Exception exc) {
                Log.WriteToLog(exc);
                ProfileList = new List<ProfileListItem>( );
            }
            return retVal;
        }

        public static void SaveList() {
            try { 
                if ( ProfileList.Count > 0 ) {
                    FileController.WriteDataToFile( JsonConvert.SerializeObject( ProfileList ) );
                }
            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
        }

        public static bool AddItemToList( ProfileListItem newItem ) {
            bool retVal = false;
            try {
                ProfileList.Add( newItem );
                SaveList();
                retVal = true;
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
