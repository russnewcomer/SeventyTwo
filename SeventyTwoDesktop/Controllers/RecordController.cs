using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop.Controllers
{

    public enum RecordStyleEnum { Template, Simple }

    class RecordController {

        private TemplateController Template { get; set; }
        private JObject RecordData { get; set; }
        public string RecordGUID { get; }
        public string PatientGUID { get; set; }

        //This is a base constructor
        public RecordController() {

        }

        //This constructor is for loading a JOBject record in and determining if it is the full template, or just the record.
        public RecordController( JObject recordData, RecordStyleEnum recordStyle = RecordStyleEnum.Template ) {
            //If this record is just the simple record, we can write it into this. 
            if ( recordStyle == RecordStyleEnum.Simple) {
                RecordData = recordData;
            } else {
                LoadFromFullTemplateRecord( recordData );
            }
        }

        //This constructor is for loading a JOBject record in and determining if it is the full template, or just the record.
        public RecordController( string fileName, RecordStyleEnum recordStyle = RecordStyleEnum.Template )
        {
            try
            {
                JObject recordJObj = JsonConvert.DeserializeObject<JObject>( File.ReadAllText( fileName ) );
                //If this record is just the simple record, we can write it into this. 
                if( recordStyle == RecordStyleEnum.Template ) {
                    //Read a full record in from the file.;
                    LoadFromFullTemplateRecord( recordJObj );
                } else {
                    //Read a simple record in from the file.
                    LoadFromSimpleRecord( recordJObj );
                }
            } catch( Exception errMsg ) {
                //Log Exception
                Models.Log.writeToLog( errMsg );
                //Throw it up to the constructor;
                throw errMsg;
            }
        }

        //This constructor is for loading a new object based on template.
        public RecordController( string templateName ) {
            TemplateController tmpTemplate = new TemplateController( templateName );
        }
        

        //Load a record from a full template record.
        public bool LoadFromFullTemplateRecord( JObject fullRecordData ) {
            bool retVal = false;
            try {

                //Create a new template based on the type from the record.
                Template = new TemplateController( fullRecordData );

                //Now get the simple record object.
                RecordData = Template.TemplateToSimpleRecordObject( );

                retVal = true;
            } catch( Exception errMsg ) {
                //Log Exception;
                Models.Log.writeToLog( errMsg );
            }
            return retVal;
        }

        //Load a record from a full template record.
        public bool LoadFromSimpleRecord( JObject simpleData )
        {
            bool retVal = false;
            try {

                //Now get the simple record object.
                RecordData = simpleData;

                //Create a new template based on the type from the record.
                Template = new TemplateController( simpleData["type"].ToString() );

                retVal = true;
            } catch( Exception errMsg )
            {
                //Log Exception;
                Models.Log.writeToLog( errMsg );
            }
            return retVal;
        }

        public string GetTemplateType()
        {
            return Template.TemplateType;
        }

        public JObject RenderDataToSimpleJSON( )
        {
            return RecordData;
            /*
              try {

                //This is the basic stuff
                recordData[ "type" ] = curTemplate.GetTemplateType( );
                recordData[ "template_guid" ] = Template.jsonTemplate[ "template_guid" ];
                recordData[ "record_guid" ] = Patient.guid;
                recordData[ "date_entered" ] = Template.jsonTemplate[ "date_entered" ];
                recordData[ "notes" ] = Template.jsonTemplate[ "notes" ];
                recordData[ "record_attachment" ] = Template.jsonTemplate[ "record_attachment" ];


                JObject items = ( JObject )Template.jsonTemplate[ "items" ];
                foreach( KeyValuePair<string, JToken> property in items )
                {
                    //Write the record data
                    string groupName = items[ property.Key ][ "group" ].ToString( );
                    if( !recordData.ContainsKey( groupName ) )
                    {
                        recordData[ groupName ] = new JObject( );
                    }
                    recordData[ groupName ][ property.Key ] = items[ property.Key ][ "value" ];
                    JObject optionalFields = ( JObject )items[ property.Key ][ "optional_fields" ];
                    foreach( KeyValuePair<string, JToken> optField in optionalFields )
                    {
                        recordData[ groupName ][ optField.Key ] = items[ property.Key ][ "optional_fields" ][ optField.Key ][ "value" ];
                    }
                    //Console.WriteLine( property.Key + " - " + property.Value );
                }

                if( fileName != "" )
                {
                    File.WriteAllText( fileName, JsonConvert.SerializeObject( recordData ) );
                }
           
            } catch(Exception errMsg ) {
                //Figure out how to log these somewhere.
                Models.Log.writeToLog(errMsg );
            }
            */
        }

    }
}
