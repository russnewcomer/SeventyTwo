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

        private TemplateController TC { get; set; }
        private string TemplateName { get; set; }
        private JObject RecordData { get; set; }
        private string _RecordGUID { get; set; }
        public string RecordGUID { get { return _RecordGUID; } }
        public string ProfileGUID { get; set; }
        public List<string> OrderedTemplateKeys { get { return TC.GetTemplateKeysInOrder( ); } }

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

            TC = new TemplateController( recordData[ "type" ].ToString( ) );
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
                

                TC = new TemplateController( recordJObj[ "type" ].ToString( ) );
            } catch( Exception errMsg ) {
                //Log Exception
                Models.Log.writeToLog( errMsg );
                //Throw it up to the constructor;
                throw errMsg;
            }
        }

        //This constructor is for loading a new object based on template.
        public RecordController( string templateName ) {
            TC = new TemplateController( templateName );
            StartNewRecord( );
        }
        

        //Load a record from a full template record.
        public bool LoadFromFullTemplateRecord( JObject fullRecordData ) {
            bool retVal = false;
            try {

                //Create a new template based on the type from the record.
                TC = new TemplateController( fullRecordData );

                //Now get the simple record object.
                RecordData = TC.TemplateToSimpleRecordObject( );

                retVal = true;
            } catch( Exception errMsg ) {
                //Log Exception;
                Models.Log.writeToLog( errMsg );
            }
            return retVal;
        }

        //Load a record from a full template record.
        public bool LoadFromSimpleRecord( JObject simpleData ) {
            bool retVal = false;
            try {

                //Now get the simple record object.
                RecordData = simpleData;

                //Create a new template based on the type from the record.
                TC = new TemplateController( simpleData["type"].ToString() );

                retVal = true;
            } catch( Exception errMsg )
            {
                //Log Exception;
                Models.Log.writeToLog( errMsg );
            }
            return retVal;
        }

        public string GetTemplateType() {
            return TC.TemplateType;
        }

        public Dictionary<string, Models.TemplateItem> GetTemplateItems() {
            return TC.GetTemplateItems( );
        }

        public string GetGroupDisplayName( string groupKey ) {
            return TC.GetGroupDisplayName( groupKey );
        }
   
        public string StartNewRecord() {
            try {
                _RecordGUID = Guid.NewGuid( ).ToString( );
                RecordData = new JObject( );
            } catch ( Exception er ) { Models.Log.writeToLog( er ); }
            return _RecordGUID;
        }

        public JObject RenderDataToSimpleJSON( ) {
            return RecordData;
            /*
              try {

                //This is the basic stuff
                recordData[ "type" ] = curTemplate.GetTemplateType( );
                recordData[ "template_guid" ] = Template.jsonTemplate[ "template_guid" ];
                recordData[ "record_guid" ] = Profile.guid;
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


        public RecordDataUpdate UpdateData( string Key, string Value) {
            RecordDataUpdate retVal = new RecordDataUpdate();
            try {

                RecordData[ Key ] = Value;
                //Check to see if I need to do a calculation
                Models.TemplateItem ti = TC.GetTemplateItem( Key );
                for( int i = 0; i < ti.Calculation.Count; i++ ) {
                    JObject calcDef = ( JObject )ti.Calculation[ i ];
                    switch ( calcDef["type"].ToString() ) {
                        case "add":
                            if ( calcDef["display"].ToString() == "date" ) {
                                DateTime calcTime = DateTime.Parse( Value );
                                switch ( calcDef["units"].ToString()) {
                                    case "s":
                                        calcTime.AddSeconds( int.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                    case "m":
                                        calcTime.AddMinutes( int.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                    case "h":
                                        calcTime.AddHours( int.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                    case "d":
                                        calcTime.AddDays( double.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                    case "M":
                                        calcTime.AddMonths( int.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                    case "y":
                                        calcTime.AddYears( int.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                }
                                RecordData[ calcDef[ "destination_field" ] ] = calcTime.ToString( "dd-MMM-yyyy" );
                            }
                            break;
                        case "nowdiff":
                            
                            TimeSpan diff = ( DateTime.Now - DateTime.Parse( Value ) );
                           
                            switch( calcDef[ "units" ].ToString( ) ) {
                                case "s":
                                    RecordData[ calcDef[ "destination_field" ].ToString() ] = Math.Floor( diff.TotalSeconds );
                                    break;
                                case "m":
                                    RecordData[ calcDef[ "destination_field" ].ToString( ) ] = Math.Floor( diff.TotalMinutes );
                                    break;
                                case "h":
                                    RecordData[ calcDef[ "destination_field" ].ToString( ) ] = Math.Floor( diff.TotalHours );
                                    break;
                                case "d":
                                    RecordData[ calcDef[ "destination_field" ].ToString( ) ] = Math.Floor( diff.TotalDays );
                                    break;
                                case "M":
                                    RecordData[ calcDef[ "destination_field" ].ToString( ) ] = Math.Ceiling( ( decimal )diff.TotalDays / 30 );
                                    break;
                                case "y":
                                    RecordData[ calcDef[ "destination_field" ].ToString( ) ] = Math.Ceiling( ( decimal )diff.TotalDays / 365 );
                                    break;
                            }
                            
                            break;
                    }
                }

               
                retVal.UpdateSuccess = true;
            } catch ( Exception er ) { Models.Log.writeToLog( er ); }
            return retVal;
        }
    }

    public struct RecordDataUpdate {
        public bool UpdateSuccess;
        public Dictionary<string, string> AdditionalValuesUpdated;
    }
}
