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
                    TemplateInstance = new Template( File.ReadAllText( "templates/" + TemplateType + ".json" ) );
                } else {
                    TemplateInstance = new Template( RawData );
                    TemplateType = TemplateInstance.Type;
                }
                

            } catch ( Exception err ) {
                throw err;
            }
        }

        public void BaseConstructor( string templateTypeName ) {
            



        }

        public void LoadTemplateFromJSONString( string RawData ) {

            

        }



        public JObject TemplateToFullJSONObject( )
        {

            JObject _groups = ( JObject ) JToken.FromObject( TemplateInstance.Groups );
            JObject _items = ( JObject )JToken.FromObject( TemplateInstance.Items );


            JObject jsonTemplate = new JObject {
                [ "type" ] = TemplateInstance.Type,
                [ "nature" ] = TemplateInstance.Nature,
                [ "template_guid" ] = TemplateInstance.TemplateGUID,
                [ "title" ] = TemplateInstance.Title,

                [ "profile_guid" ] = TemplateInstance.ProfileGUID,
                [ "record_guid" ] = TemplateInstance.RecordGUID,
                [ "date_entered" ] = TemplateInstance.DateEntered.ToString( "dd-MMM-yyyy" ),
                [ "notes" ] = TemplateInstance.Notes,
                [ "record_attachment" ] = TemplateInstance.RecordAttachmentGUID,
                [ "groups" ] = _groups,
                [ "items" ] = _items
            };

            return jsonTemplate;
        }


        public bool SaveSimpleRecordObject( string fileNameToSaveFileTo ) {
            bool retVal = false;
            try {
                if ( FileController == null || ( FileController != null && (FileController.TargetFile == fileNameToSaveFileTo) ) ) {
                    FileController = new FileReadWriteController( fileNameToSaveFileTo );
                }
                FileController.WriteDataToFile( JsonConvert.SerializeObject( TemplateToSimpleRecordObject( ) ) );
                retVal = true;
            } catch( Exception err ) {
                Models.Log.WriteToLog( err );
            }
            return retVal;
        }

        


        public JObject TemplateToSimpleRecordObject( ) {

            JObject recordData = new JObject {

                //This is the basic stuff
                [ "type" ] = TemplateInstance.Type,
                [ "profile_guid" ] = TemplateInstance.ProfileGUID,
                [ "template_guid" ] = TemplateInstance.TemplateGUID,
                [ "record_guid" ] = TemplateInstance.RecordGUID,
                [ "date_entered" ] = TemplateInstance.DateEntered.ToString( "dd-MMM-yyyy" ),
                [ "notes" ] = TemplateInstance.Notes,
                [ "record_attachment" ] = TemplateInstance.RecordAttachmentGUID
            };

            foreach( KeyValuePair<string, TemplateItem> kvp in TemplateInstance.Items ) {
                string groupName = kvp.Value.Group;
                if( !recordData.ContainsKey( groupName ) ) {
                    recordData[ groupName ] = new JObject( );
                }
                recordData[ groupName ][ kvp.Value.Name ] = kvp.Value.Value;
                foreach( TemplateItem optField in kvp.Value.OptionalFields ) {
                    recordData[ groupName ][ optField.Name ] = optField.Value;
                }
            }

            return recordData;
        }

        public Dictionary<string, TemplateItem> GetTemplateItems(  ) {
            return TemplateInstance.Items;
        }

        public string GetRecordGUID() {
            return TemplateInstance.RecordGUID;
        }

        public void SetRecordGUID( string RecordGUID ) {
            TemplateInstance.RecordGUID = RecordGUID;
        }


        public string GetProfileGUID( ) {
            return TemplateInstance.ProfileGUID;
        }

        public void SetProfileGUID( string ProfileGUID ) {
            TemplateInstance.ProfileGUID = ProfileGUID;
        }

        public TemplateItem GetTemplateItem( string itemName ) {

   
            TemplateItem retVal = new TemplateItem();

            try {
                retVal = TemplateInstance.Items[ itemName ]; ;
            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;
        }
        

        public string GetGroupDisplayName( string groupKey ) {
            string retVal = "";

            try {
                retVal = TemplateInstance.Groups[ groupKey ].ToString( );
            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;
        }

        public string GetTemplateItemValue( string Key )
        {
            string retVal = "";

            try {
                retVal = TemplateInstance.Items[ Key ].Value;
            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;
        }


        public bool UpdateTemplateItemValue( string Key, string Value ) {
            bool retVal = false;

            try {
                TemplateInstance.Items[ Key ].Value = Value;
                retVal = true;
            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;
        }

        public int UpdateTemplateItemSubRecord( string Key, JObject SubRecord, int Index = -1) {
            int retVal = Index;

            try {
                try {
                    if( Index == -1 ) {
                        TemplateInstance.Items[ Key ].Subrecords.Add( SubRecord );
                        retVal = TemplateInstance.Items[ Key ].Subrecords.Count - 1;
                    } else {
                        TemplateInstance.Items[ Key ].Subrecords[ Index ] = SubRecord;
                    }
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
                    retVal = ( JObject ) TemplateInstance.Items[ Key ].Subrecords[ Index ];
                } catch( Exception exc ) {
                    Models.Log.WriteToLog( exc );
                }

            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;

        }

    }
}
