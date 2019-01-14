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
    class PatientController
    {

        private PatientItem Patient { get; set; }
        private Dictionary<string, RecordController> Records { get; set; }
        

        public PatientController() {
        }

        //Initialize the patient
        public string InitializePatient()
        {
            //Set the guid
            string guid = Guid.NewGuid().ToString();
            Patient = new PatientItem();
            Patient.guid = guid;
   
            return guid;

        }

        public void loadPatientData(string guid)
        {
            try
            {
                //Read and load the permanent JSON item.
                Patient = JsonConvert.DeserializeObject<PatientItem>( File.ReadAllText( "patients/" + guid + "/permanent.json" ) );

                //Read and load all other JSON templates.
                IEnumerable<string> records = Directory.EnumerateFiles( "patients/guid" );

                foreach ( string filename in records) {
                    string record_guid = filename.Replace( ".json", "" );
                       


                }

            }
            catch (Exception errMsg)
            {
                //Figure out how to log these somewhere.
                Log.writeToLog(errMsg);
                Patient.guid = "";
            }
        }

        public void savePatientData()
        {
            try
            {
                //Create the directory if necessary.
                if (!Directory.Exists("patients/" + Patient.guid))
                {
                    Directory.CreateDirectory("patients/" + Patient.guid);
                }

                //Overwrites existing changes
                File.WriteAllText("patients/" + Patient.guid + "/permanent.json", JsonConvert.SerializeObject(Patient));
                

            }
            catch (Exception errMsg)
            {
                //Figure out how to log these somewhere.
                Log.writeToLog(errMsg);
            }
        }

        public JObject PatientDataToSimpleRecord( string fileName = "" ) {

            JObject recordData = new JObject( );

            try {

            
                //This is the basic stuff
                recordData[ "type" ] = Template.jsonTemplate[ "type" ];
                recordData[ "template_guid" ] = Template.jsonTemplate[ "template_guid" ];
                recordData[ "record_guid" ] = Patient.guid;
                recordData[ "date_entered" ] = Template.jsonTemplate[ "date_entered" ];
                recordData[ "notes" ] = Template.jsonTemplate[ "notes" ];
                recordData[ "record_attachment" ] = Template.jsonTemplate[ "record_attachment" ];


                JObject items = ( JObject )Template.jsonTemplate[ "items" ];
                foreach( KeyValuePair<string, JToken> property in items ) {
                    //Write the record data
                    string groupName = items[ property.Key ][ "group" ].ToString( );
                    if( !recordData.ContainsKey( groupName ) )
                    {
                        recordData[ groupName ] = new JObject( );
                    }
                    recordData[ groupName ][ property.Key ] = items[ property.Key ][ "value" ];
                    JObject optionalFields = ( JObject )items[ property.Key ][ "optional_fields" ];
                    foreach( KeyValuePair<string, JToken> optField in optionalFields ) {
                        recordData[ groupName ][ optField.Key ] = items[ property.Key ][ "optional_fields" ][ optField.Key ][ "value" ];
                    }
                    //Console.WriteLine( property.Key + " - " + property.Value );
                }

                if( fileName != "" ) {
                    File.WriteAllText( fileName, JsonConvert.SerializeObject( recordData ) );
                }

            } catch( Exception errMsg ) {
                //Figure out how to log these somewhere.
                Log.writeToLog( errMsg );
            }
            return recordData;
        }

    }
}
