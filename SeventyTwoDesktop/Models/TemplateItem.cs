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

        public string Name { get; set; }
        public string FieldType { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public JArray DropDownOptions{ get; set; }
        public string ShowOn { get; set; }
        public string Value { get; set; }
        public List<TemplateItem> OptionalFields { get; set; }
        public string Nature { get; set; }
        public Dictionary<string, TemplateItem> SubrecordItems{ get; set; }
        public JArray Subrecords { get; set; }
        public JArray Calculation { get; set; }

        public TemplateItem( ) {

        }

        public TemplateItem( JToken ti ){

            string[ ] pathSplit = ti.Path.Split( '.' );
            Name = pathSplit[ pathSplit.Length - 1 ];

            FieldType = ti[ "field_type" ] != null ? ti[ "field_type" ].ToString( ) : "";
            Group = ti[ "group" ] != null ? ti[ "group" ].ToString( ) : "";
            Title = ti[ "title" ] != null ? ti[ "title" ].ToString( ) : "";
            Order = ti[ "order" ] != null ? int.Parse( ti[ "order" ].ToString( ) ) : 0;
            ShowOn = ti[ "show_on" ] != null ? ti[ "show_on" ].ToString( ) : "";
            Value = ti[ "value" ] != null ? ti[ "value" ].ToString( ) : ""; 
            DropDownOptions = ti[ "dropdown_options" ] != null ? new JArray( ti[ "dropdown_options" ].ToString( ) ) : new JArray( );
            OptionalFields = new List<TemplateItem>( );
            if( ti[ "optional_fields" ] != null ) {
                foreach( KeyValuePair<string, JToken> property in (JObject)ti[ "optional_fields" ] )
                {
                    OptionalFields.Add( new TemplateItem( property.Value ) );
                }
            }
            Nature = ti[ "nature" ] != null ? ti[ "nature" ].ToString( ) : "";
            Subrecords = ti[ "subrecords" ] != null ? ( JArray )ti[ "subrecords" ] : new JArray( );
            SubrecordItems = new Dictionary<string,TemplateItem>( );
            if( ti[ "subrecord_items" ] != null ) {
                foreach( KeyValuePair<string, JToken> property in ( JObject )ti[ "subrecord_items" ] ) {
                    SubrecordItems.Add( property.Key, new TemplateItem( property.Value ) );
                }
            }
            Calculation = ti[ "calculation" ] != null ? ( JArray ) ti[ "calculation" ] : new JArray( );
        }

        public JObject ToJObject() {
            JObject retVal = new JObject( );
            JObject item = new JObject( );
            try {

                //retVal.Add()
                item.Add( "group", Group );
                item.Add( "field_type", FieldType );
                item.Add( "title", Title );
                item.Add( "order", Order );
                item.Add( "show_on", ShowOn );
                item.Add( "value", Value );
                item.Add( "dropdown_options", new JArray( DropDownOptions ) );
                item.Add( "optional_fields", new JObject( OptionalFields ) );
                item.Add( "value", Value );
                item.Add( "subrecords", new JArray( Subrecords ) );
                item.Add( "subrecord_items", new JObject( SubrecordItems ) );
                item.Add( "calculation", new JArray( Calculation ) );

                retVal.Add( Name, item );

            } catch (Exception errMsg) {
                Models.Log.WriteToLog( errMsg );
            }
            return retVal;
        }
        

    }
}
