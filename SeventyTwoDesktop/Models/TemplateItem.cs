using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop.Models
{
    public class TemplateItem
    {

        public string name { get; set; }
        public string field_type { get; set; }
        public string group { get; set; }
        public string title { get; set; }
        public int order { get; set; }
        public List<string> dropdown_options{ get; set; }
        public string show_on { get; set; }
        public string value { get; set; }
        public Dictionary<string,TemplateItem> optional_fields { get; set; }
        public string nature { get; set; }
        public Dictionary<string, TemplateItem> subrecord_items{ get; set; }
        public JArray subrecords { get; set; }
        public JArray calculation { get; set; }

        public TemplateItem( ) {

        }

        public TemplateItem( JToken ti ){

            string[ ] pathSplit = ti.Path.Split( '.' );
            name = pathSplit[ pathSplit.Length - 1 ];

            field_type = ti[ "field_type" ] != null ? ti[ "field_type" ].ToString( ) : "";
            group = ti[ "group" ] != null ? ti[ "group" ].ToString( ) : "";
            title = ti[ "title" ] != null ? ti[ "title" ].ToString( ) : "";
            order = ti[ "order" ] != null ? int.Parse( ti[ "order" ].ToString( ) ) : 0;
            show_on = ti[ "show_on" ] != null ? ti[ "show_on" ].ToString( ) : "";
            value = ti[ "value" ] != null ? ti[ "value" ].ToString( ) : ""; 
            //I need to make sure we don't write random jazz into here.
            dropdown_options = ti[ "dropdown_options" ] != null ? JsonConvert.DeserializeObject<List<string>>( ti[ "dropdown_options" ].ToString() ) : new List<string>( );
            optional_fields = new Dictionary<string, TemplateItem>( );
            if( ti[ "optional_fields" ] != null ) {
                Console.WriteLine( ti[ "optional_fields" ].ToString( ) );
                foreach( KeyValuePair<string, JToken> property in ( JObject )ti[ "optional_fields" ] ) {
                    optional_fields.Add( property.Key, new TemplateItem( property.Value ) );
                }
            } 
            nature = ti[ "nature" ] != null ? ti[ "nature" ].ToString( ) : "";
            subrecords = ti[ "subrecords" ] != null ? ( JArray )ti[ "subrecords" ] : new JArray( );
            subrecord_items = new Dictionary<string,TemplateItem>( );
            if( ti[ "subrecord_items" ] != null ) {
                foreach( KeyValuePair<string, JToken> property in ( JObject )ti[ "subrecord_items" ] ) {
                    subrecord_items.Add( property.Key, new TemplateItem( property.Value ) );
                }
            }
            calculation = ti[ "calculation" ] != null ? ( JArray ) ti[ "calculation" ] : new JArray( );
        }

        public JObject ToJObject() {
            JObject retVal = new JObject( );
            JObject item = new JObject( );
            try {

                //retVal.Add()
                item.Add( "group", group );
                item.Add( "field_type", field_type );
                item.Add( "title", title );
                item.Add( "order", order );
                item.Add( "show_on", show_on );
                item.Add( "value", value );
                item.Add( "dropdown_options", new JArray( dropdown_options ) );
                item.Add( "optional_fields", new JObject( optional_fields ) );
                item.Add( "value", value );
                item.Add( "subrecords", new JArray( subrecords ) );
                item.Add( "subrecord_items", new JObject( subrecord_items ) );
                item.Add( "calculation", new JArray( calculation ) );

                retVal.Add( name, item );

            } catch (Exception errMsg) {
                Models.Log.WriteToLog( errMsg );
            }
            return retVal;
        }
        

    }
}
