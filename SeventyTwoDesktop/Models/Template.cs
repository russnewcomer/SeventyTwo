using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop.Models
{
    class Template
    {

        public string Type { get; }
        public string Nature { get; }
        public string TemplateGUID { get; }
        public string Title { get; }
        public string ProfileGUID { get; set; }
        public string RecordGUID { get; set; }
        public string RecordAttachmentGUID { get; set; }
        public string Notes { get; set; }
        public DateTime DateEntered { get; set; }
        public Dictionary<string, string> Groups { get; set; }
        public Dictionary<string, TemplateItem> Items { get; set; }

        public Template() {

        }

        public Template( string templateString ) {

            JObject jsonTemplate = JObject.Parse( templateString );

            Type = jsonTemplate[ "type" ].ToString();
            Nature = jsonTemplate[ "nature" ].ToString( );
            TemplateGUID = jsonTemplate[ "template_guid" ].ToString( );
            Title = jsonTemplate[ "title" ].ToString( );

            if( jsonTemplate.ContainsKey( "profile_guid" ) ) {
                ProfileGUID = jsonTemplate[ "profile_guid" ].ToString( );
            }
            if( jsonTemplate.ContainsKey( "record_guid" ) ) {
                RecordGUID = jsonTemplate[ "record_guid" ].ToString( );
            }
            if( jsonTemplate.ContainsKey( "date_entered" ) ) {
                DateEntered = DateTime.Parse( jsonTemplate[ "date_entered" ].ToString( ) );
            }
            if( jsonTemplate.ContainsKey( "notes" ) ) {
                Notes = jsonTemplate[ "notes" ].ToString( );
            }
            if( jsonTemplate.ContainsKey( "record_attachment" ) ) {
                RecordAttachmentGUID = jsonTemplate[ "record_attachment" ].ToString( );
            }

            Groups = new Dictionary<string, string>();

            foreach( KeyValuePair<string, JToken> property in ( JObject )jsonTemplate[ "groups" ] ) {
                Groups[ property.Key ] = property.Value.ToString();
            }

            Items = new Dictionary<string, TemplateItem>( );

            foreach( KeyValuePair<string, JToken> property in ( JObject )jsonTemplate[ "items" ] ) {
                Items[ property.Key ] = ( new TemplateItem( property.Value ) );
            }
        }
        

        public JObject TemplateToSimpleRecordObject( ) {
            JObject recordData = new JObject {

                //This is the basic stuff
                [ "type" ] = Type,
                [ "profile_guid" ] = ProfileGUID,
                [ "template_guid" ] = TemplateGUID,
                [ "record_guid" ] = RecordGUID,
                [ "date_entered" ] = DateEntered.ToString( "dd-MMM-yyyy" ),
                [ "notes" ] = Notes,
                [ "record_attachment" ] = RecordAttachmentGUID
            };
            JArray groupArray = new JArray();

            foreach ( KeyValuePair<string, TemplateItem> kvp in Items ) {
                string groupName = kvp.Value.Group;
                if( !recordData.ContainsKey( groupName ) ) {
                    recordData[ groupName ] = new JObject( );
                    groupArray.Add( groupName );
                }
                recordData[ groupName ][ kvp.Value.Name ] = kvp.Value.Value;
                foreach( TemplateItem optField in kvp.Value.OptionalFields ) {
                    recordData[ groupName ][ optField.Name ] = optField.Value;
                }

            }

            //Add in a list of groups
            recordData[ "groups" ] = groupArray;



            return recordData;
        }


    }
}
