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

        public PatientItem Patient { get; set; }
        private Dictionary<string, RecordController> Records { get; set; }
        

        public PatientController() {
        }


        public PatientController( string guid ) {
            loadPatientData( guid );
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

        public JObject PatientDataToSimpleRecord( ) {

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
