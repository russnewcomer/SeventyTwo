using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        static void ConfirmDirectory( string directoryName ) {
            //make sure the 'config' directory exists.
            if( !System.IO.Directory.Exists( directoryName ) )
            {
                System.IO.Directory.CreateDirectory( directoryName );
            }
        }
    }
}
