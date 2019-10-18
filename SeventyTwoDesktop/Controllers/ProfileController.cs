using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeventyTwoDesktop.Models;

namespace SeventyTwoDesktop.Controllers
{
    public class ProfileController
    {

        //Static Method to check if profile exists
        public static bool ProfileExists( string guid ) {
            bool retVal = false;
            try {
                //Read and load the permanent JSON item.
                retVal = Directory.Exists( "data/" + guid );

            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
            return retVal;
        }

        //Non-Static Methods and Properties


        public ProfileItem Profile { get; set; }
        public Dictionary< string, RecordController > Records { get; set; } = new Dictionary< string, RecordController >( );
       

        public ProfileController() {
        }


        public ProfileController( string guid ) {
            LoadProfileData( guid );
        }
        
        public string InitializeProfile() {
            //Set the guid
            string guid = Guid.NewGuid().ToString();
            Profile = new ProfileItem {
                guid = guid,
                name = "New Profile"
            };
   
            return guid;

        }

        public void LoadProfileData( string guid, string rootFileLocation = "data/" ) {
            try {
                //Read and load the permanent JSON item.
                if( Directory.Exists( rootFileLocation + guid ) ) {
                    Profile = JsonConvert.DeserializeObject<ProfileItem>( File.ReadAllText( rootFileLocation + guid + "/permanent.json" ) );
                    
                    //Read and load all other JSON templates.
                    IEnumerable<string> records = Directory.EnumerateFiles( rootFileLocation + guid );

                    foreach ( string filename in records) {

                        //We don't want to try to load permanent.json
                        if( !filename.Contains( "\\permanent.json" ) && filename.Contains(".json" ) ) {
                            //Get the record contents
                            RecordController rc = new RecordController( filename, Profile.guid, TemplateStyle.HasValues );
                            Records.Add( rc.RecordGUID, rc );
                        }

                    }

                } else {
                    Profile = new ProfileItem( );
                }


            } catch( Exception exc ) { Log.WriteToLog( exc ); }
        }



        public void SaveProfileData() {
            try {

                if( !System.IO.Directory.Exists( "data/" + Profile.guid ) ) {
                    System.IO.Directory.CreateDirectory( "data/" + Profile.guid );
                }
                Profile.modifydate = DateTime.Now;
                Profile.last_modified_guid = UserController.ActiveUser.GUID;

                //Overwrites existing changes
                File.WriteAllText("data/" + Profile.guid + "/permanent.json", JsonConvert.SerializeObject(Profile));
                
            } catch ( Exception exc ) { Log.WriteToLog( exc ); }
        }

    }
}
