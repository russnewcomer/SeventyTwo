﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SeventyTwoDesktop.Models;

namespace SeventyTwoDesktop.Controllers
{
    static class UserController {

        private static string LIST_FILE_PATH = FileReadWriteController.addPath( "data", "users.json" );
        private static string LIST_DUPLICATE_FILE_PATH = FileReadWriteController.addPath( "data", "backup_user_list.json" );

        public static UserItem ActiveUser { get; set; }
        public static List<UserItem> UserList {get; set;} = new List<UserItem>();
        private static FileReadWriteController FileController { get; set; } = new FileReadWriteController( LIST_FILE_PATH, LIST_DUPLICATE_FILE_PATH );

        static UserController( ) {
            LoadList( );
        }

        public static bool LoadList() {
            bool retVal = false;
            try {
                //Open the list and deserialize it
                FileController.ForceRead( );
                string JSON = FileController.FileContents;
                if( string.IsNullOrEmpty( JSON ) ) {
                    UserList = new List<UserItem>( );
                } else {
                    UserList = JsonConvert.DeserializeObject<List<UserItem>>( JSON );
                }
                foreach (UserItem ui in UserList) {
                    if ( ui.Active ) {
                        ActiveUser = ui;
                    }
                }
                retVal = true;
            } catch (Exception exc) {
                Log.WriteToLog(exc);
                UserList = new List<UserItem>( );
            }
            return retVal;
        }

        public static void SaveList() {
            try { 
                if ( UserList.Count > 0 ) {
                    FileController.WriteDataToFile( JsonConvert.SerializeObject( UserList ) );
                }
            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
        }

        public static void AddItemToListIfNotExists( UserItem itm ) {
            
            try {
                List<string> guidList = new List<string>( );
                foreach (UserItem user in UserList) {
                    guidList.Add( user.GUID );
                }
                if ( !guidList.Contains( itm.GUID ) ) {
                    UserList.Add( itm );
                    SaveList( );
                }
            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
        }
        public static UserItem AddItemToList( string name, string number ) {
            UserItem retVal = null;
            try {
                retVal = new UserItem( name, number );
                UserList.Add( retVal );
                SaveList();
            } catch( Exception exc ) { Log.WriteToLog( exc ); }
            return retVal;
        }

        public static bool AlterExistingItem( UserItem itemToAlter ) {
            bool retVal = false;
            try {
                int idx = UserList.FindIndex( u => u.GUID == itemToAlter.GUID );
                if ( idx > -1 ) {
                    UserList[idx] = itemToAlter;
                    SaveList();
                    retVal = true;
                }
            } catch( Exception exc ) { Log.WriteToLog( exc ); }
            return retVal;
        }

        public static void SetActiveUser( string ActiveGuid ) {
            try {
                foreach ( UserItem ui in UserList ) {
                    ui.Active = ui.GUID == ActiveGuid;
                    ActiveUser = ui;
                }
                SaveList( );
            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
        }
    }
}
