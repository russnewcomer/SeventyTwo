using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeventyTwoDesktop.Models;


namespace SeventyTwoDesktop.Controllers
{


    public enum TemplateStyle { Blank, HasValues }

    class TemplateController {


        //Static Methods and Properties
        private static Dictionary<string, string> TemplateTypes { get; set; }

        public static Dictionary<string, string> GetTemplateTypes( bool RefreshTemplateTypes = false )
        {
            if( TemplateTypes == null || RefreshTemplateTypes ) {
                TemplateTypes = new Dictionary<string, string>( );
                JArray templates = JArray.Parse( File.ReadAllText( "config/templates.json" ) );
                foreach( JToken x in templates ) {
                    foreach( KeyValuePair<string, JToken> property in ( JObject )x ) {
                        TemplateTypes.Add( property.Key, property.Value.ToString( ) );
                    }
                }
            }

            return TemplateTypes;
        }


        public string TemplateType { get; set; }
        private JObject JsonTemplate { get; set; }
        private string _fileName { get; set; }
        private Template TemplateInstance { get; set; }
        private List<string> OrderedKeys { get; set; }
        public FileReadWriteController FileController { get; set; }

        public TemplateController( ) { }

        public TemplateController( string RawData, TemplateStyle style ) {
            try {
                if ( style == TemplateStyle.Blank ) {
                    //If this is a simple record, we first need to set the 
                    TemplateType = RawData;
                    TemplateInstance = new Template( File.ReadAllText( "templates/" + TemplateType + ".json" ) ) {
                        date_entered = DateTime.Now //Set the date to the current date.
                    };
                } else {
                    //JObject templateData = 
                    TemplateInstance = new Template( RawData );
                    TemplateType = TemplateInstance.type;
                }
                

            } catch ( Exception err ) {
                throw err;
            }
        }


        public string TemplateToFullJSONString( ) {
          
            return JsonConvert.SerializeObject( TemplateInstance, Formatting.Indented );
        }


        //public bool SaveSimpleRecordObject( string fileNameToSaveFileTo ) {
        //    bool retVal = false;
        //    try {
        //        if ( FileController == null || ( FileController != null && (FileController.TargetFile == fileNameToSaveFileTo) ) ) {
        //            FileController = new FileReadWriteController( fileNameToSaveFileTo );
        //        }
        //        FileController.WriteDataToFile( JsonConvert.SerializeObject( TemplateToSimpleRecordObject( ) ) );
        //        retVal = true;
        //    } catch( Exception err ) {
        //        Models.Log.WriteToLog( err );
        //    }
        //    return retVal;
        //}

        //public JObject TemplateToSimpleRecordObject( ) {

        //    JObject recordData = new JObject {

        //        //This is the basic stuff
        //        [ "type" ] = TemplateInstance.type,
        //        [ "profile_guid" ] = TemplateInstance.profile_guid,
        //        [ "template_guid" ] = TemplateInstance.template_guid,
        //        [ "record_guid" ] = TemplateInstance.record_guid,
        //        [ "date_entered" ] = TemplateInstance.date_entered.ToString( "dd-MMM-yyyy" ),
        //        [ "notes" ] = TemplateInstance.notes,
        //        [ "record_attachment" ] = TemplateInstance.record_attachment
        //    };

        //    foreach( KeyValuePair<string, TemplateItem> kvp in TemplateInstance.items ) {
        //        string groupName = kvp.Value.group;
        //        if( !recordData.ContainsKey( groupName ) ) {
        //            recordData[ groupName ] = new JObject( );
        //        }
        //        recordData[ groupName ][ kvp.Value.name ] = kvp.Value.value;
        //        if ( kvp.Value.optional_fields.Value != null ) { 
        //            recordData[ groupName ][ kvp.Value.optional_fields.Key ] = kvp.Value.optional_fields.Value.ToString();
        //        }
        //    }

        //    return recordData;
        //}

        public Dictionary<string, TemplateItem> GetTemplateItems(  ) {
            return TemplateInstance.items;
        }

        public string GetRecordGUID() {
            return TemplateInstance.record_guid;
        }

        public void SetRecordGUID( string RecordGUID ) {
            TemplateInstance.record_guid = RecordGUID;
        }


        public string GetProfileGUID( ) {
            return TemplateInstance.profile_guid;
        }

        public void SetProfileGUID( string ProfileGUID ) {
            TemplateInstance.profile_guid = ProfileGUID;
        }

        public DateTime GetTemplateDateEntered() {
            return TemplateInstance.date_entered;
        }


        public Dictionary<string, string> GetFollowupSchedule( ) {
            return TemplateInstance.followup;
        }

        public void SetFollowupScheduled( bool Scheduled ) {
            TemplateInstance.followup[ "scheduled" ] = Scheduled.ToString( );
            TemplateInstance.date_updated = DateTime.Now;
        }

        public TemplateItem GetTemplateItem( string itemName ) {

   
            TemplateItem retVal = new TemplateItem();

            try {
                retVal = TemplateInstance.items[ itemName ]; ;
            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;
        }
        

        public string GetGroupDisplayName( string groupKey ) {
            string retVal = "";

            try {
                retVal = TemplateInstance.groups[ groupKey ].ToString( );
            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;
        }

        public string GetTemplateItemValue( string Key )
        {
            string retVal = "";

            try {
                retVal = TemplateInstance.items[ Key ].value;
            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;
        }


        public bool UpdateTemplateItemValue( string Key, string Value ) {
            bool retVal = false;

            try {
                TemplateInstance.items[ Key ].value = Value;
                TemplateInstance.date_updated = DateTime.Now;
                retVal = true;
            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;
        }

        public int UpdateTemplateItemSubRecord( string Key, JObject SubRecord, int Index = -1) {
            int retVal = Index;

            try {
                try {
                    if( Index == -1 ) {
                        TemplateInstance.items[ Key ].subrecords.Add( SubRecord );
                        retVal = TemplateInstance.items[ Key ].subrecords.Count - 1;
                    } else {
                        TemplateInstance.items[ Key ].subrecords[ Index ] = SubRecord;
                    }
                    TemplateInstance.date_updated = DateTime.Now;
                } catch( Exception exc ) {
                    Models.Log.WriteToLog( exc );
                }

            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;

        }

        public JObject GetTemplateItemSubRecord( string Key, int Index ) {
            JObject retVal = new JObject();

            try {
                try {
                    //Get the object for this key
                    retVal = ( JObject ) TemplateInstance.items[ Key ].subrecords[ Index ];
                } catch( Exception exc ) {
                    Models.Log.WriteToLog( exc );
                }

            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;

        }

    }
}
