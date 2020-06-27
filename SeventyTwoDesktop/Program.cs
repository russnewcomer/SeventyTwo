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
            
            ConfirmDirectory( "data" );
            ConfirmDirectory( "data/calendar" );
            ConfirmDirectory( "templates" );
            ConfirmDirectory( "log" );
            ConfirmDirectory( "config" );
            ConfirmDirectory( "reconcile" );
            ConfirmDirectory( "temp" );

            GetProfileTypesAndLoadIntoConfigFile( );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }



        static void ConfirmDirectory( string directoryName ) {
            //make sure the named directory exists.
            string path = Controllers.FileReadWriteController.addPath( directoryName );
            if ( !Directory.Exists( path ) )
            {
                try { 
                    Directory.CreateDirectory( path );
                } catch (Exception errMsg){
                    Console.WriteLine( errMsg.Message );
                    Models.Log.WriteToLog( errMsg );
                }
                
            }
        }

        
        //Look at the templates directory, and get the list.  Compare it to a file in 'config' and write the new files if necessary.
        static void GetProfileTypesAndLoadIntoConfigFile() {
            bool configListDirty = false;
            string configFilePath = Controllers.FileReadWriteController.addPath( "config", "templates.json" );
            bool templateConfigFileExists = File.Exists( configFilePath );
            JArray configList = templateConfigFileExists ? JArray.Parse( File.ReadAllText( configFilePath ) ) : new JArray();
            List<string> profileNamesFromConfigList = new List<string>();
            foreach( JToken x in configList ) {
                foreach( KeyValuePair<string, JToken> property in ( JObject )x ) {
                    profileNamesFromConfigList.Add( property.Key );
                }
            }

            IEnumerable<string> ProfileTypes = Directory.EnumerateFiles( Controllers.FileReadWriteController.addPath("templates") );

            foreach( string profileFileName in ProfileTypes ) {

                string[] fileSplit = profileFileName.Split( '\\' );
                string name = fileSplit[ fileSplit.Length - 1 ].Replace( ".json", "" );
                if ( !profileNamesFromConfigList.Contains( name ) ) {
                    JObject template = JObject.Parse( File.ReadAllText( profileFileName ) );
                    JObject item = JObject.Parse( "{\"" +name + "\":\"" + template[ "title" ].ToString( ) + "\"} " );
                    configList.Add( item );
                    configListDirty = true;
                }
            }
            
            if ( configListDirty || !templateConfigFileExists ) {
                File.WriteAllText( configFilePath, configList.ToString( ) );
            }
        }

    }
}
