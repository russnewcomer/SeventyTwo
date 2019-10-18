using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            ConfirmDirectory( "profiles" );
            ConfirmDirectory( "templates" );
            ConfirmDirectory( "log" );
            ConfirmDirectory( "config" );
            ConfirmDirectory( "export" );
            ConfirmDirectory( "reconcile" );
            ConfirmDirectory( "calendar" );

            GetProfileTypesAndLoadIntoConfigFile( );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

        static void ConfirmDirectory( string directoryName ) {
            //make sure the named directory exists.
            if( !Directory.Exists( directoryName ) )
            {
                Directory.CreateDirectory( directoryName );
            }
        }

        
        //Look at the templates directory, and get the list.  Compare it to a file in 'config' and write the new files if necessary.
        static void GetProfileTypesAndLoadIntoConfigFile() {
            bool configListDirty = false;
            bool templateConfigFileExists = File.Exists( "config/templates.json" );
            JArray configList = templateConfigFileExists ? JArray.Parse( File.ReadAllText( "config/templates.json" ) ) : new JArray();
            List<string> profileNamesFromConfigList = new List<string>();
            foreach( JToken x in configList ) {
                foreach( KeyValuePair<string, JToken> property in ( JObject )x ) {
                    profileNamesFromConfigList.Add( property.Key );
                }
            }

            IEnumerable<string> ProfileTypes = Directory.EnumerateFiles( "templates" );

            foreach( string profileFileName in ProfileTypes ) {
                string name = profileFileName.Replace( ".json", "" ).Replace("templates\\", "");
                if ( !profileNamesFromConfigList.Contains( name ) ) {
                    JObject template = JObject.Parse( File.ReadAllText( profileFileName ) );
                    JObject item = JObject.Parse( "{\"" +name + "\":\"" + template[ "title" ].ToString( ) + "\"} " );
                    configList.Add( item );
                    configListDirty = true;
                }
            }
            
            if ( configListDirty || !templateConfigFileExists ) {
                File.WriteAllText( "config/templates.json", configList.ToString( ) );
            }
        }
    }
}
