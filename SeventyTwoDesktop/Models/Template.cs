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
        public Dictionary<string, string> Groups { get; set; }
        public Dictionary<string, TemplateItem> Items { get; set; }

        public Template() {

        }

        public Template( string templateString )
        {
            JObject jsonTemplate = JObject.Parse( templateString );

            Groups = new Dictionary<string, string>();

            foreach( KeyValuePair<string, JToken> property in ( JObject )jsonTemplate[ "groups" ] )
            {
                Groups[ property.Key ] = property.Value.ToString();
            }

            Items = new Dictionary<string, TemplateItem>( );

            foreach( KeyValuePair<string, JToken> property in ( JObject )jsonTemplate[ "items" ] )
            {
                Items[ property.Key ] =  ( new TemplateItem( property.Value ) );
            }
        }




    }
}
