using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using SeventyTwoDesktop.Models;

namespace SeventyTwoDesktop.Controllers
{
    class PatientController
    {

        public PatientItem Patient { get; set; }

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
                string json = File.ReadAllText("patients/" + guid + "/permanent.json");

                Patient = JsonConvert.DeserializeObject<PatientItem>(json);
                //Overwrites existing changes
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
                Patient.guid = "";
            }
        }



    }
}
