using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop.Models
{
    class DynTemplate : DynamicObject
    {
        private string templateType;
        public dynamic jsonTemplate;
        private string fileName;

        public DynTemplate( string templateTypeName ) {
            templateType = templateTypeName;
            fileName = "templates/" + templateTypeName + ".json";
            if ( !File.Exists( fileName ) ) {
                throw new Exception( "No template with that name exists." );
            }
            string templateRawString = File.ReadAllText( fileName );
            jsonTemplate = JObject.Parse( templateRawString );
        }

        public JObject TemplateToSimpleRecord( )
        {
            JObject recordData = new JObject( );

            //This is the basic stuff
            recordData[ "type" ] = jsonTemplate[ "type" ];
            recordData[ "template_guid" ] = jsonTemplate[ "template_guid" ];
            recordData[ "record_guid" ] = jsonTemplate[ "record_guid" ];
            recordData[ "date_entered" ] = jsonTemplate[ "date_entered" ];
            recordData[ "notes" ] = jsonTemplate[ "notes" ];
            recordData[ "record_attachment" ] = jsonTemplate[ "record_attachment" ];

            //Now pull out all the groups
            //Now pull out all the items and values, and create objects for groups, putting values in.
            //Then write them all out.

            return recordData;
        }
    }
}
