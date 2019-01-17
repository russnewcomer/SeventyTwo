using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop.Models
{
    class TemplateItem
    {

        public string Name { get; set; }
        public string FieldType { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string[] DropDownItems { get; set; }
        public string ShowOn { get; set; }
        public string Value { get; set; }
        public List<TemplateItem> OptionalFields { get; set; }

        public TemplateItem( ) {

        }

        public JObject toJObject()
        {
            JObject retVal = new JObject( );
            try {

                //retVal.Add()

            } catch (Exception errMsg) {
                Models.Log.writeToLog( errMsg );
            }
            return retVal;
        }

    }
}
