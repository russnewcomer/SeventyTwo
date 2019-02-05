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

    public class RecordController {

        private TemplateController TC { get; set; }
        private string TemplateName { get; set; }
        //private JObject RecordData { get; set; }
        private string _RecordGUID { get; set; }
        public string RecordGUID { get { return _RecordGUID; } }
        public string ProfileGUID { get; set; }
        
        private FileReadWriteController FileController { get; set; }

        //This is a base constructor
        public RecordController( ) {

        }

        //This constructor is for loading a JOBject record in and determining if it is the full template, or just the record.
        public RecordController( string TypeOrFile, string _ProfileGUID, TemplateStyle Style ) {
            try {
                ProfileGUID = _ProfileGUID;
 
                if ( Style == TemplateStyle.Blank ) {
                    TC = new TemplateController( TypeOrFile, Style );
                    _RecordGUID = Guid.NewGuid( ).ToString( );
                    TC.SetRecordGUID( _RecordGUID );
                    TC.SetProfileGUID( ProfileGUID );
                } else {
                    //Create a new template based on the type the record.
                    JObject template = JsonConvert.DeserializeObject<JObject>( File.ReadAllText( TypeOrFile ) );
                    _RecordGUID = template[ "record_guid" ].ToString( );

                    //Create a new template instance from here.
                    TC = new TemplateController( File.ReadAllText( TypeOrFile ), Style );

                }
                    
              
              
            } catch( Exception errMsg ) {
                //Log Exception
                Models.Log.WriteToLog( errMsg );
                //Throw it up to the constructor;
                throw errMsg;
            }
        }


        public string GetTemplateType() {
            return TC.TemplateType;
        }

        public string GetRecordDisplayText( ) {
            string title = TemplateController.GetTemplateTypes( ).Where( T => T.Key == TC.TemplateType ).First( ).Value;
            string suffix = " - " + TC.GetTemplateDateEntered( ).ToString( "dd-MMM-yyyy" );
            return title + suffix;
        }

        public Dictionary<string, Models.TemplateItem> GetTemplateItems() {
            return TC.GetTemplateItems( );
        }

        public Models.TemplateItem GetTemplateItem( string key ) {
            return TC.GetTemplateItem( key );
        }

        public string GetGroupDisplayName( string groupKey ) {
            return TC.GetGroupDisplayName( groupKey );
        }
   

        //public JObject RenderDataToSimpleJSON( ) {
        //    return TC.TemplateToSimpleRecordObject();
        //    /*
        //      try {

        //        //This is the basic stuff
        //        recordData[ "type" ] = curTemplate.GetTemplateType( );
        //        recordData[ "template_guid" ] = Template.jsonTemplate[ "template_guid" ];
        //        recordData[ "record_guid" ] = Profile.guid;
        //        recordData[ "date_entered" ] = Template.jsonTemplate[ "date_entered" ];
        //        recordData[ "notes" ] = Template.jsonTemplate[ "notes" ];
        //        recordData[ "record_attachment" ] = Template.jsonTemplate[ "record_attachment" ];


        //        JObject items = ( JObject )Template.jsonTemplate[ "items" ];
        //        foreach( KeyValuePair<string, JToken> property in items )
        //        {
        //            //Write the record data
        //            string groupName = items[ property.Key ][ "group" ].ToString( );
        //            if( !recordData.ContainsKey( groupName ) )
        //            {
        //                recordData[ groupName ] = new JObject( );
        //            }
        //            recordData[ groupName ][ property.Key ] = items[ property.Key ][ "value" ];
        //            JObject optionalFields = ( JObject )items[ property.Key ][ "optional_fields" ];
        //            foreach( KeyValuePair<string, JToken> optField in optionalFields )
        //            {
        //                recordData[ groupName ][ optField.Key ] = items[ property.Key ][ "optional_fields" ][ optField.Key ][ "value" ];
        //            }
        //            //Console.WriteLine( property.Key + " - " + property.Value );
        //        }

        //        if( fileName != "" )
        //        {
        //            File.WriteAllText( fileName, JsonConvert.SerializeObject( recordData ) );
        //        }
           
        //    } catch(Exception errMsg ) {
        //        //Figure out how to log these somewhere.
        //        Models.Log.WriteToLog(errMsg );
        //    }
        //    */
        //}


        public string GetData( string Key ) {
            string retVal = "";

            try {
                retVal = TC.GetTemplateItemValue( Key );
            } catch ( Exception exc ) {
                Models.Log.WriteToLog( exc );
            }

            return retVal;
        }

        public int UpdateTemplateItemSubRecord( string Key, JObject Value, int Index = -1 ) {
            return TC.UpdateTemplateItemSubRecord( Key, Value, Index );
        }

        public JObject GetTemplateItemSubRecord( string Key, int Index ) {
            return TC.GetTemplateItemSubRecord( Key, Index );
        }

        public RecordDataUpdate UpdateData( string Key, string Value) {
            RecordDataUpdate retVal = new RecordDataUpdate {
                AdditionalValuesUpdated = new Dictionary<string, string>( )
            };

            try {

                TC.UpdateTemplateItemValue( Key, Value );
                //RecordData[ Key ] = Value;


                //Check to see if I need to do a calculation
                Models.TemplateItem ti = TC.GetTemplateItem( Key );
                for( int i = 0; i < ti.calculation.Count; i++ ) {
                    JObject calcDef = ( JObject )ti.calculation[ i ];
                    string destField = calcDef[ "destination_field" ].ToString( );
                    switch ( calcDef["type"].ToString() ) {
                        case "add":
                            if ( calcDef["display"].ToString() == "date" ) {
                                DateTime calcTime = DateTime.Parse( Value );
                                switch ( calcDef["units"].ToString()) {
                                    case "s":
                                        calcTime = calcTime.AddSeconds( int.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                    case "m":
                                        calcTime = calcTime.AddMinutes( int.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                    case "h":
                                        calcTime = calcTime.AddHours( int.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                    case "d":
                                        calcTime = calcTime.AddDays( double.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                    case "M":
                                        calcTime = calcTime.AddMonths( int.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                    case "y":
                                        calcTime = calcTime.AddYears( int.Parse( calcDef[ "value" ].ToString( ) ) );
                                        break;
                                }
                                string addValToUpdate = calcTime.ToString( "dd-MMM-yyyy" );
                                TC.UpdateTemplateItemValue( destField, addValToUpdate );
                               // RecordData[ destField ] = addValToUpdate;
                                retVal.AdditionalValuesUpdated.Add( destField, addValToUpdate );
                            }
                            break;
                        case "nowdiff":
                            
                            TimeSpan diff = ( DateTime.Now - DateTime.Parse( Value ) );
                            string nowDiffTargetValue = "0";
                            switch( calcDef[ "units" ].ToString( ) ) {
                                case "s":
                                    nowDiffTargetValue = Math.Floor( diff.TotalSeconds ).ToString();
                                    break;
                                case "m":
                                    nowDiffTargetValue = Math.Floor( diff.TotalMinutes ).ToString( );
                                    break;
                                case "h":
                                    nowDiffTargetValue = Math.Floor( diff.TotalHours ).ToString( );
                                    break;
                                case "d":
                                    nowDiffTargetValue = Math.Floor( diff.TotalDays ).ToString( );
                                    break;
                                case "M":
                                    nowDiffTargetValue = Math.Ceiling( ( decimal )diff.TotalDays / 30 ).ToString( );
                                    break;
                                case "y":
                                    nowDiffTargetValue = Math.Ceiling( ( decimal )diff.TotalDays / 365 ).ToString( );
                                    break;
                            }
                            
                            TC.UpdateTemplateItemValue( destField, nowDiffTargetValue );
                           //RecordData[ destField ] = nowDiffTargetValue;
                            retVal.AdditionalValuesUpdated.Add( destField, nowDiffTargetValue );
                           
                            break;
                    }
                }

               
                WriteRecord( );
                retVal.UpdateSuccess = true;
            } catch ( Exception er ) { Models.Log.WriteToLog( er ); }
            return retVal;
        }

        public void WriteRecord() {
            
            if( !string.IsNullOrEmpty( ProfileGUID ) && !string.IsNullOrEmpty( RecordGUID ) ) {
                SaveFullRecordObject( "profiles/" + ProfileGUID + "/" + RecordGUID + ".json"  );
            }
        }

        private bool SaveFullRecordObject( string fileNameToSaveFileTo )
        {
            bool retVal = false;
            try {
                if( FileController == null || ( FileController != null && ( FileController.TargetFile != fileNameToSaveFileTo ) ) ) {
                    FileController = new FileReadWriteController( fileNameToSaveFileTo );
                }
                TC.SetRecordGUID( _RecordGUID );
                TC.SetProfileGUID( ProfileGUID );
                FileController.WriteDataToFile( TC.TemplateToFullJSONString( ) );
                retVal = true;
            } catch( Exception err ) {
                Models.Log.WriteToLog( err );
            }
            return retVal;
        }

    }

    public struct RecordDataUpdate {
        public bool UpdateSuccess;
        public Dictionary<string, string> AdditionalValuesUpdated;
    }
}
