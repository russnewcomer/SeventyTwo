using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop.Controllers
{
    class TemplateController
    {
        private string _templateType;
        public string TemplateType { get { return _templateType; } set { _templateType = value; } }
        public JObject jsonTemplate;
        private string _fileName;

        public TemplateController( JObject _SourceTemplate ) {
            jsonTemplate = _SourceTemplate;
        }

        public TemplateController( string templateTypeName ) {
            try {
                BaseConstructor( templateTypeName );
            } catch ( Exception err ) {
                throw err;
            }
        }

        public void BaseConstructor( string templateTypeName ) {
            _templateType = templateTypeName;
            _fileName = "templates/" + _templateType + ".json";
            if( !File.Exists( _fileName ) ) {
                throw new Exception( "No template with that name exists." );
            }

            string templateRawString = File.ReadAllText( _fileName );
            jsonTemplate = JObject.Parse( templateRawString );
        }

        public bool SaveTemplateToFullRecordObject( ) {
            bool retVal = false;
            try {
                string fileName = "patients/" + jsonTemplate[ "patient_guid" ].ToString( ) + "/" + jsonTemplate[ "record_guid" ].ToString( ) + ".json";
                File.WriteAllText( fileName, JsonConvert.SerializeObject( jsonTemplate ) );
                retVal = true;
            } catch ( Exception err ) {
                Models.Log.writeToLog( err );
            }
            return retVal;
        }

        public bool SaveSimpleRecordObject( string fileNameToSaveFileTo ) {
            bool retVal = false;
            try {
                File.WriteAllText( fileNameToSaveFileTo, JsonConvert.SerializeObject( TemplateToSimpleRecordObject() ) );
                retVal = true;
            } catch( Exception err ) {
                Models.Log.writeToLog( err );
            }
            return retVal;
        }

        public JObject TemplateToSimpleRecordObject( ) {
            JObject recordData = new JObject( );
            
            //This is the basic stuff
            recordData[ "type" ]= jsonTemplate[ "type" ];
            recordData[ "patient_guid" ] = jsonTemplate[ "patient_guid" ];
            recordData[ "template_guid" ] = jsonTemplate[ "template_guid" ];
            recordData[ "record_guid" ] = Guid.NewGuid( ).ToString( );
            recordData[ "date_entered" ] = jsonTemplate[ "date_entered" ];
            recordData[ "notes" ] = jsonTemplate[ "notes" ];
            recordData[ "record_attachment" ] = jsonTemplate[ "record_attachment" ];


            JObject items = ( JObject )jsonTemplate[ "items" ];
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


        public JObject TemplateUserInterfaceSpecifications( )
        {
            JObject uiSpecs = new JObject( );

            //This is the basic stuff
            uiSpecs[ "type" ] = jsonTemplate[ "type" ];
            uiSpecs[ "title" ] = jsonTemplate[ "title" ];


            JObject items = ( JObject )jsonTemplate[ "items" ];
            foreach( KeyValuePair<string, JToken> property in items )
            {
                //Write the record data
                string groupName = items[ property.Key ][ "group" ].ToString( );
                if( !uiSpecs.ContainsKey( groupName ) )
                {
                    uiSpecs[ groupName ] = new JObject( );
                }
                uiSpecs[ groupName ][ property.Key ] = 
                JObject optionalFields = ( JObject )items[ property.Key ][ "optional_fields" ];
                foreach( KeyValuePair<string, JToken> optField in optionalFields )
                {
                    uiSpecs[ groupName ][ optField.Key ] = items[ property.Key ][ "optional_fields" ][ optField.Key ][ "value" ];
                }
                //Console.WriteLine( property.Key + " - " + property.Value );
            }

            return uiSpecs;
        }
    }
}
