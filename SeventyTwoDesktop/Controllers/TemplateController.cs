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
                [ "type" ] = JsonTemplate[ "type" ],
                [ "profile_guid" ] = JsonTemplate[ "profile_guid" ],
                [ "template_guid" ] = JsonTemplate[ "template_guid" ],
                [ "record_guid" ] = Guid.NewGuid( ).ToString( ),
                [ "date_entered" ] = JsonTemplate[ "date_entered" ],
                [ "notes" ] = JsonTemplate[ "notes" ],
                [ "record_attachment" ] = JsonTemplate[ "record_attachment" ]
            };


            JObject items = ( JObject )JsonTemplate[ "items" ];
            foreach( KeyValuePair<string, JToken> property in items ) {
                //Write the record data
                string groupName = items[ property.Key ][ "group" ].ToString();
                if ( !recordData.ContainsKey( groupName ) ) {
                    recordData[ groupName ] = new JObject( );
                }
                recordData[ groupName ][ property.Key ] = items[ property.Key ][ "value" ];
                JObject optionalFields = ( JObject )items[ property.Key ][ "optional_fields" ];
                foreach( KeyValuePair<string, JToken> optField in optionalFields ) {
                    recordData[ groupName ][ optField.Key ] = items[ property.Key ][ "optional_fields" ][ optField.Key ][ "value" ];
                }
                //Console.WriteLine( property.Key + " - " + property.Value );
            }

            return recordData;
        }

        public Dictionary<string, TemplateItem> GetTemplateItems(  )
        {
            return TemplateInstance.Items;
        }

        public TemplateItem GetTemplateItem( string itemName )
        {
            return TemplateInstance.Items[ itemName ];
        }

        public List<string> GetTemplateKeysInOrder() {
            if( OrderedKeys == null ) {
                OrderedKeys = new List<string>( );

                JObject items = ( JObject )JsonTemplate[ "items" ];
                try
                {
                    foreach( KeyValuePair<string, JToken> property in items )
                    {
                        OrderedKeys.Add( property.Key );
                    }
                } catch( Exception er ) { Log.WriteToLog( er ); }

            }
            return OrderedKeys;
        }

        public string GetGroupDisplayName( string groupKey ) {
            return TemplateInstance.Groups[ groupKey ].ToString( );
        }


    }
}
