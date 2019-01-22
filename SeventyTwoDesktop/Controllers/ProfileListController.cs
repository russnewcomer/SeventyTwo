using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SeventyTwoDesktop.Models;

namespace SeventyTwoDesktop.Controllers
{
    class ProfileListController
    {

        public List<ProfileListItem> ProfileList {get; set;} = new List<ProfileListItem>();
        public bool LoadList()
        {
            bool retVal = false;
            try
            {
                //Open the list and deserialize it
                string json = File.ReadAllText("Profiles/list.json");
                ProfileList = JsonConvert.DeserializeObject<List<ProfileListItem>>(json);
                retVal = true;
            }
            catch (Exception errMsg)
            {
                Log.writeToLog(errMsg);
                ProfileList = new List<ProfileListItem>();
            }
            return retVal;
        }

        public bool SaveList()
        {
            bool retVal = false;
            try
            {
                if (ProfileList.Count > 0)
                {
                    //Make a quick copy of the old list
                    if (File.Exists("Profiles/old-list.json") ) {
                        File.Delete("Profiles/old-list.json");
                    }
                    //Make a quick copy of the old list
                    File.Copy("Profiles/list.json", "Profiles/old-list.json");
                    File.WriteAllText("Profiles/list.json", JsonConvert.SerializeObject(ProfileList));
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

        public bool AddItemToList(ProfileListItem newItem) {
            bool retVal = false;
            try
            {
                ProfileList.Add(newItem);
                SaveList();
            }
            catch (Exception errMsg)
            {
                //Figure out how to log these somewhere.
                Log.writeToLog(errMsg);
            }
            return retVal;
        }

        public bool AlterExistingItem( ProfileListItem itemToAlter )
        {
            bool retVal = false;
            try
            {
                int idx = ProfileList.FindIndex( pli => pli.GUID == itemToAlter.GUID );
                if (idx > -1)
                {
                    ProfileList[idx] = itemToAlter;
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
