using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SeventyTwoDesktop
{
    class Patient
    {

        private string GUID { get; set; }

        public Patient()
        {

        }

        public string initializePatient()
        {
            string retVal = Guid.NewGuid().ToString();
            try
            {
                //Need to create a new patient permanent record from template

                string permanentTemplate = File.ReadAllText("../templates/permanent.json");
                Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>( permanentTemplate );
            } catch ( Exception errMsg ) {
                //Figure out how to log these somewhere.
                Log.writeToLog(errMsg);
            }
            return retVal;
        }
    }
}
