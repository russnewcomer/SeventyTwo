using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SeventyTwoDesktop.Models;

namespace SeventyTwoDesktop.Controllers
{
    class PatientListController
    {

        public List<PatientListItem> PatientList {get; set;} = new List<PatientListItem>();
        public bool LoadList()
        {
            bool retVal = false;
            try
            {
                //Open the list and deserialize it
                string json = File.ReadAllText("patients/list.json");
                PatientList = JsonConvert.DeserializeObject<List<PatientListItem>>(json);
                retVal = true;
            }
            catch (Exception errMsg)
            {
                Log.writeToLog(errMsg);
                PatientList = new List<PatientListItem>();
            }
            return retVal;
        }

        public bool SaveList()
        {
            bool retVal = false;
            try
            {
                if (PatientList.Count > 0)
                {
                    //Make a quick copy of the old list
                    if (File.Exists("patients/old-list.json") ) {
                        File.Delete("patients/old-list.json");
                    }
                    //Make a quick copy of the old list
                    File.Copy("patients/list.json", "patients/old-list.json");
                    File.WriteAllText("patients/list.json", JsonConvert.SerializeObject(PatientList));
                    retVal = true;
                }
            }
            catch (Exception errMsg)
            {
                //Figure out how to log these somewhere.
                Log.writeToLog(errMsg);
            }
            return retVal;
        }

        public bool AddItemToList(PatientListItem newItem) {
            bool retVal = false;
            try
            {
                PatientList.Add(newItem);
                SaveList();
            }
            catch (Exception errMsg)
            {
                //Figure out how to log these somewhere.
                Log.writeToLog(errMsg);
            }
            return retVal;
        }

        public bool AlterExistingItem( PatientListItem itemToAlter )
        {
            bool retVal = false;
            try
            {
                int idx = PatientList.FindIndex( pli => pli.GUID == itemToAlter.GUID );
                if (idx > -1)
                {
                    PatientList[idx] = itemToAlter;
                    SaveList();
                    retVal = true;
                }
            }
            catch (Exception errMsg)
            {
                //Figure out how to log these somewhere.
                Log.writeToLog(errMsg);
            }
            return retVal;
        }
    }
}
