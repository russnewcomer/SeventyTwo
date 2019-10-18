using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop.Models
{
    class Template
    {

        public string type { get; }
        public string nature { get; }
        public string template_guid { get; }
        public string title { get; }
        public string profile_guid { get; set; }
        public string record_guid { get; set; }
        public string record_attachment { get; set; }
        public string notes { get; set; }
        public DateTime date_entered { get; set; }
        public DateTime date_updated { get; set; }
        public Dictionary<string, string> followup { get; set; }
        public Dictionary<string, string> groups { get; set; }
        public Dictionary<string, TemplateItem> items { get; set; }
        public string last_modified_guid { get; set; }

        public Template() {

        }

        public Template( JObject jsonTemplate ) {

            type = jsonTemplate[ "type" ].ToString( );
            nature = jsonTemplate[ "nature" ].ToString( );
            template_guid = jsonTemplate[ "template_guid" ].ToString( );
            title = jsonTemplate[ "title" ].ToString( );


            BuildFromJObject( jsonTemplate );

        }

        public Template( string templateString ) {

            JObject jsonTemplate = JObject.Parse( templateString );
        
            type = jsonTemplate[ "type" ].ToString();
            nature = jsonTemplate[ "nature" ].ToString( );
            template_guid = jsonTemplate[ "template_guid" ].ToString( );
            title = jsonTemplate[ "title" ].ToString( );

            BuildFromJObject( jsonTemplate );
        }


        public void BuildFromJObject( JObject jsonTemplate ) {
            
            followup = new Dictionary<string, string>( );

            try {

                if( jsonTemplate.ContainsKey( "followup" ) && jsonTemplate[ "followup" ].HasValues ) {
                    foreach( KeyValuePair<string, JToken> property in ( JObject )jsonTemplate[ "followup" ] ) {
                        followup[ property.Key ] = property.Value.ToString( );
                    }
                }
            } catch ( Exception exc ) {
                Log.WriteToLog( exc );
            } 

            if( jsonTemplate.ContainsKey( "profile_guid" ) ) {
                profile_guid = jsonTemplate[ "profile_guid" ].ToString( );
            }
            if( jsonTemplate.ContainsKey( "record_guid" ) ) {
                record_guid = jsonTemplate[ "record_guid" ].ToString( );
            }
            if( jsonTemplate.ContainsKey( "date_entered" ) ) {
                date_entered = DateTime.Parse( jsonTemplate[ "date_entered" ].ToString( ) );
            }
            if ( jsonTemplate.ContainsKey("date_updated") ) {
                date_updated = DateTime.Parse( jsonTemplate[ "date_updated" ].ToString( ) );
            }
            if ( jsonTemplate.ContainsKey( "notes" ) ) {
                notes = jsonTemplate[ "notes" ].ToString( );
            }
            if( jsonTemplate.ContainsKey( "record_attachment" ) ) {
                record_attachment = jsonTemplate[ "record_attachment" ].ToString( );
            }

            groups = new Dictionary<string, string>( );
            try {

                foreach( KeyValuePair<string, JToken> property in ( JObject )jsonTemplate[ "groups" ] ) {
                    groups[ property.Key ] = property.Value.ToString( );
                }
            } catch( Exception exc ) {
                Log.WriteToLog( exc );
            }

            items = new Dictionary<string, TemplateItem>( );
            try { 

                foreach( KeyValuePair<string, JToken> property in ( JObject )jsonTemplate[ "items" ] ) {
                    items[ property.Key ] = ( new TemplateItem( property.Value ) );
                }
            } catch( Exception exc ) {
                Log.WriteToLog( exc );
            }
        }


        //public JObject TemplateToSimpleRecordObject( ) {
        //    JObject recordData = new JObject {

        //        //This is the basic stuff
        //        [ "type" ] = type,
        //        [ "profile_guid" ] = profile_guid,
        //        [ "template_guid" ] = template_guid,
        //        [ "record_guid" ] = record_guid,
        //        [ "date_entered" ] = date_entered.ToString( "dd-MMM-yyyy" ),
        //        [ "notes" ] = notes,
        //        [ "record_attachment" ] = record_attachment
        //    };
        //    JArray groupArray = new JArray();

        //    foreach ( KeyValuePair<string, TemplateItem> kvp in items ) {
        //        string groupName = kvp.Value.group;
        //        if( !recordData.ContainsKey( groupName ) ) {
        //            recordData[ groupName ] = new JObject( );
        //            groupArray.Add( groupName );
        //        }
        //        recordData[ groupName ][ kvp.Value.name ] = kvp.Value.value;
        //        foreach( TemplateItem optField in kvp.Value.optional_fields ) {
        //            recordData[ groupName ][ optField.name ] = optField.value;
        //        }

        //    }

        //    //Add in a list of groups
        //    recordData[ "groups" ] = groupArray;



        //    return recordData;
        //}




    }
}
