using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeventyTwoDesktop.Models
{
    public static class Log
    {
        public static void WriteToLog( Exception exc )
        {
            try
            {
                string logFileName = "log/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                StringBuilder sb = new StringBuilder( DateTime.Now.ToString( "dd-MMM-yyyy hh:mm" ) );
                sb.AppendLine( "Exception" );
                sb.Append( exc.ToString( ) );
                sb.AppendLine( );
                File.AppendAllText(logFileName, sb.ToString());
                
            } catch ( Exception logExc ) {
                string errText = Environment.NewLine + Environment.NewLine + exc.ToString() + Environment.NewLine + logExc.ToString();
                var result = System.Windows.Forms.MessageBox.Show("Sorry about this.  A really strange error has happened.  You probably want to email whoever is helping you with SeventyTwo this error.  If you click yes, I'll copy this to your clipboard, then you can paste it into an email to your technical person." +errText , "Argh. Sorry.", System.Windows.Forms.MessageBoxButtons.YesNo);
                if ( result == System.Windows.Forms.DialogResult.Yes ) {
                    System.Windows.Forms.Clipboard.SetText(errText);
                } 
            }
        }

        public static void WriteImport( String importRecord ) {
            try {
                string logFileName = "log/Import-" + DateTime.Now.ToString( "yyyy-MM-dd" ) + ".txt";
                StringBuilder sb = new StringBuilder( DateTime.Now.ToString( "dd-MMM-yyyy hh:mm" ) );
                sb.AppendLine( "Import Log:" );
                sb.Append( importRecord );
                sb.AppendLine( );
                File.AppendAllText( logFileName, sb.ToString( ) );

            } catch ( Exception logExc ) {
                string errText = Environment.NewLine + Environment.NewLine + importRecord + Environment.NewLine + logExc.ToString( );
                var result = System.Windows.Forms.MessageBox.Show( "Sorry about this.  A really strange error has happened.  You probably want to email whoever is helping you with SeventyTwo this error.  If you click yes, I'll copy this to your clipboard, then you can paste it into an email to your technical person." + errText, "Argh. Sorry.", System.Windows.Forms.MessageBoxButtons.YesNo );
                if ( result == System.Windows.Forms.DialogResult.Yes ) {
                    System.Windows.Forms.Clipboard.SetText( errText );
                }
            }
        }

        public static string GetRecentLogFiles( int numberOfDaysToRetrieve = 3) {
            StringBuilder logText = new StringBuilder( );
            try {

                DateTime logDay = DateTime.Now.AddDays( -numberOfDaysToRetrieve );


                logText.AppendLine( "Current Date/Time: " + DateTime.Now.ToString( "dd-MMM-yyyy hh:mm" ) );
                logText.AppendLine( );
                logText.AppendLine( "Current Profiles" );
                foreach( ProfileListItem pli in Controllers.ProfileListController.ProfileList ) {
                    logText.AppendLine( pli.ToString( ) );
                }
                logText.AppendLine( );
                logText.AppendLine( "Current Templates" );
                foreach( KeyValuePair<string,string> pli in Controllers.TemplateController.GetTemplateTypes() ) {
                    logText.AppendLine( pli.ToString( ) );
                }
                logText.AppendLine( );

                for( var i = 0; i <= numberOfDaysToRetrieve; i++ ) {
                    if( File.Exists( "log/" + logDay.ToString( "yyyy-MM-dd" ) + ".txt" ) ) {
                        logText.AppendLine( );
                        logText.AppendLine( "Logs for " + logDay.ToString( "yyyy-MM-dd" ) );
                        logText.AppendLine( );
                        logText.Append( File.ReadAllText( "log/" + logDay.ToString( "yyyy-MM-dd" ) + ".txt" ) );
                        logText.AppendLine( );
                    } else {
                        logText.AppendLine( );
                        logText.AppendLine( "No logs exist for " + logDay.ToString( "yyyy-MM-dd" ) );
                        logText.AppendLine( );
                    }
                    logDay = logDay.AddDays( 1 );
                }
            } catch ( Exception exc ) {
                logText.Clear( );
                logText.Append( "Something really strange happened, and a log could not be generated." );
                logText.Append( "You should probably send this to whoever is helping you with SeventyTwo." );

                logText.Append( exc.ToString( ) );

            }

            return logText.ToString( );
        } 
    }
}
