using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeventyTwoDesktop.Models;


namespace SeventyTwoDesktop.Controllers
{
    class TemplateController {
        public string TemplateType { get; set; }
        private JObject JsonTemplate { get; set; }
        private string _fileName { get; set; }
        private Template TemplateInstance { get; set; }
        private List<string> OrderedKeys { get; set; }

        public TemplateController( JObject _SourceTemplate ) {
            JsonTemplate = _SourceTemplate;
        }

        public TemplateController( string templateTypeName ) {
            try {
                BaseConstructor( templateTypeName );
            } catch ( Exception err ) {
                throw err;
            }
        }

        public void BaseConstructor( string templateTypeName ) {
            TemplateType = templateTypeName;
            _fileName = "templates/" + TemplateType + ".json";
            if( !File.Exists( _fileName ) ) {
                throw new Exception( "No template with that name exists." );
            }

            string templateRawString = File.ReadAllText( _fileName );
            JsonTemplate = JObject.Parse( templateRawString );

            TemplateInstance = new Template( templateRawString );

        }

        public bool ExportTemplateToFullRecordObject( ) {
            bool retVal = false;
            try {
                string fileName = "export/" + JsonTemplate[ "template_guid" ].ToString( ) + ".json";
                File.WriteAllText( fileName, JsonConvert.SerializeObject( JsonTemplate ) );
                retVal = true;
            } catch ( Exception err ) {
                Models.Log.WriteToLog( err );
            }
            return retVal;
        }

        public bool SaveSimpleRecordObject( string fileNameToSaveFileTo ) {
            bool retVal = false;
            try {
                File.WriteAllText( fileNameToSaveFileTo, JsonConvert.SerializeObject( TemplateToSimpleRecordObject() ) );
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

            //JObject recordData = new JObject {

            //    //This is the basic stuff
            //    [ "type" ] = JsonTemplate[ "type" ],
            //    [ "profile_guid" ] = JsonTemplate[ "profile_guid" ],
            //    [ "template_guid" ] = JsonTemplate[ "template_guid" ],
            //    [ "record_guid" ] = Guid.NewGuid( ).ToString( ),
            //    [ "date_entered" ] = JsonTemplate[ "date_entered" ],
            //    [ "notes" ] = JsonTemplate[ "notes" ],
            //    [ "record_attachment" ] = JsonTemplate[ "record_attachment" ]
            //};


            //JObject items = ( JObject )JsonTemplate[ "items" ];
            //foreach( KeyValuePair<string, JToken> property in items ) {
            //    //Write the record data
            //    string groupName = items[ property.Key ][ "group" ].ToString();
            //    if ( !recordData.ContainsKey( groupName ) ) {
            //        recordData[ groupName ] = new JObject( );
            //    }
            //    recordData[ groupName ][ property.Key ] = items[ property.Key ][ "value" ];
            //    JObject optionalFields = ( JObject )items[ property.Key ][ "optional_fields" ];
            //    foreach( KeyValuePair<string, JToken> optField in optionalFields ) {
            //        recordData[ groupName ][ optField.Key ] = items[ property.Key ][ "optional_fields" ][ optField.Key ][ "value" ];
            //    }
            //    //Console.WriteLine( property.Key + " - " + property.Value );
            //}

            //return recordData;
        }

        public Dictionary<string, TemplateItem> GetTemplateItems(  ) {
            return TemplateInstance.Items;
        }

        public TemplateItem GetTemplateItem( string itemName ) {

   
            TemplateItem retVal = new TemplateItem();

            try {
                retVal = TemplateInstance.Items[ itemName ]; ;
            } catch( Exception exc ) { Log.WriteToLog( exc ); }

            return retVal;
        }

        public List<string> GetTemplateKeysInOrder() {
            if( OrderedKeys == null ) {
                OrderedKeys = new List<string>( );

                JObject items = ( JObject )JsonTemplate[ "items" ];
                try {
                    foreach( KeyValuePair<string, JToken> property in items ) {
                        OrderedKeys.Add( property.Key );
                    }
                } catch( Exception er ) { Log.WriteToLog( er ); }

            }
            return OrderedKeys;
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
