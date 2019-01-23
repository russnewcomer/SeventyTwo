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
    class ProfileController
    {

        //Static Methods and Properties
        private static List<KeyValuePair<string, string>> RecordTypes { get; set; }

        public static List<KeyValuePair<string, string>> GetRecordTypes( bool refreshRecordTypes = false )
        {
            if( RecordTypes == null || refreshRecordTypes )
            {
                RecordTypes = new List<KeyValuePair<string, string>>( );
                JArray templates = JArray.Parse( File.ReadAllText( "config/templates.json" ) );
                foreach( JToken x in templates )
                {
                    foreach( KeyValuePair<string, JToken> property in ( JObject )x )
                    {
                        RecordTypes.Add( new KeyValuePair<string, string>( property.Key, property.Value.ToString( ) ) );
                    }
                }
            }

            return RecordTypes;
        }




        public ProfileItem Profile { get; set; }
        public Dictionary<string, RecordController> Records { get; set; } = new Dictionary<string, RecordController>( );
       

        public ProfileController() {
        }


        public ProfileController( string guid ) {
            LoadProfileData( guid );
        }
        
        public string InitializeProfile()
        {
            //Set the guid
            string guid = Guid.NewGuid().ToString();
            Profile = new ProfileItem();
            Profile.guid = guid;
            Profile.name = "New Profile";
   
            return guid;

        }

        public void LoadProfileData(string guid)
        {
            try
            {
                //Read and load the permanent JSON item.
                Profile = JsonConvert.DeserializeObject<ProfileItem>( File.ReadAllText( "Profiles/" + guid + "/permanent.json" ) );

                //Read and load all other JSON templates.
                IEnumerable<string> records = Directory.EnumerateFiles( "Profiles/guid" );

                foreach ( string filename in records) {
                    string record_guid = filename.Replace( ".json", "" );
                       


                }

            }
            catch (Exception errMsg)
            {
                //Figure out how to log these somewhere.
                Log.writeToLog(errMsg);
                Profile.guid = "";
            }
        }

        public void saveProfileData()
        {
            try
            {

                if( !System.IO.Directory.Exists( "profiles/" + Profile.guid ) )
                {
                    System.IO.Directory.CreateDirectory( "profiles/" + Profile.guid );
                }

                //Overwrites existing changes
                File.WriteAllText("profiles/" + Profile.guid + "/permanent.json", JsonConvert.SerializeObject(Profile));
                
            }
            catch (Exception errMsg)
            {
                //Figure out how to log these somewhere.
                Log.writeToLog(errMsg);
            }
        }

        public JObject ProfileDataToSimpleRecord( ) {

            JObject recordData = new JObject( );

            try {

                foreach( KeyValuePair<string, RecordController> Record in Records )
                {

                    RecordController curRecord = Record.Value;

                    curRecord.RenderDataToSimpleJSON( );
                }
            } catch( Exception errMsg ) {
                //Figure out how to log these somewhere.
                Log.writeToLog( errMsg );
            }
            return recordData;
        }


    }
}
