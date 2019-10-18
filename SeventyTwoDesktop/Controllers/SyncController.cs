using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeventyTwoDesktop.Controllers;
using SeventyTwoDesktop.Models;
using System.IO;
using System.IO.Compression;

namespace SeventyTwoDesktop.Controllers
{
    class SyncController
    {

        /*
         * The purpose of the SyncController is to handle synchronization of profiles
         * 
         * We need to read and create a single large JSON file with all changes made since last sync
         * We will zip this file up and then it will be sent out by a manual process.
         * Eventually, it would be great to send this up to a webservice and sync records that way.
         * 
         */


        public static void CreateExportZip( string DestinationZipFile ) {
            ZipFile.CreateFromDirectory( "data", DestinationZipFile );
        }


        public static void ImportData( string importFileName ) {
            try {
                string importDirectoryName = importFileName.Split( '\\' ).Last( ).Replace( ".zip", "" );

                //delete our temp directory, incase it wasn't 
                if ( Directory.Exists( "temp/unzip" ) ) {
                    Directory.Delete( "temp/unzip", true );
                }

                //Unzip the files from the export list
                ZipFile.ExtractToDirectory( importFileName, "temp/unzip" );
                //Then check list of users

                //Add New Users as necessary
                CheckUserList( importDirectoryName );

                //Then start walking the profiles and take the newest data
                IterateThroughProfileList( importDirectoryName );
                //Also walk the calendar

                //delete our temp directory
                Directory.Delete( "temp/unzip", true );

            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
        }

        private static void CheckUserList( string importDirName ) {
            try { 
                if ( File.Exists( "temp/unzip/" + importDirName + "/users.json" ) ) {

                    List<UserItem> users = JsonConvert.DeserializeObject<List<UserItem>>( File.ReadAllText( "temp/unzip/" + importDirName + "/users.json" ) );

                    foreach ( UserItem user in users ) {
                        UserController.AddItemToListIfNotExists( user );
                    }
                }

            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
        }

        private static void IterateThroughProfileList( string importDirName ) {

            List<ProfileListItem> ProfileList;
            string JSON = File.ReadAllText( "temp/unzip/" + importDirName + "/list.json" );
            if ( string.IsNullOrEmpty( JSON ) ) {
                ProfileList = new List<ProfileListItem>( );
            } else {
                ProfileList = JsonConvert.DeserializeObject<List<ProfileListItem>>( JSON );
            }
            foreach ( ProfileListItem pli in ProfileList ) {
                CheckProfileDelta( importDirName, pli );
            }
        }

        private static void CheckProfileDelta( string importDirName, ProfileListItem pli ) {
            //We will check to see if the profile exists.  
            try {
                if ( ProfileController.ProfileExists( pli.GUID ) ) {
                    //The Profile exists, we will walk it to get the newest changes.
                    ProfileController ImportingProfile = new ProfileController( );
                    ImportingProfile.LoadProfileData( pli.GUID, "temp/unzip/" + importDirName + "/" );

                    ProfileController ExistingProfile = new ProfileController( pli.GUID );

                    foreach (KeyValuePair<string, RecordController> rec in ImportingProfile.Records) {
                        if ( ExistingProfile.Records.Keys.Contains( rec.Key ) && ( rec.Value.GetDateUpdated() > ExistingProfile.Records[rec.Key].GetDateUpdated() ) ) {
                            ExistingProfile.Records[ rec.Key ] = rec.Value;
                            Log.WriteImport( "Imported " + rec.Value.GetType( ) + " (" + rec.Value.RecordGUID + ") for " +pli.Name );
                        }
                    }

                } else {
                    //The profile does not exist, we will just copy it over
                    if ( !Directory.Exists( "data/" + pli.GUID ) ) {
                        Directory.CreateDirectory( "data/" + pli.GUID );
                    }
                    IEnumerable<string> records = Directory.EnumerateFiles( "temp/unzip/" + importDirName + "/" + pli.GUID );
                    foreach ( string filename in records ) {
                        File.Copy( filename, "data/" + pli.GUID + "/" + filename.Split( '\\' ).Last( ) );
                    }

                    //Now that we have copied it over, add it to the list
                    ProfileListController.AddItemToList( pli );
                    
                }

            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
        }
    }
}
