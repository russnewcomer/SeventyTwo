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

            //make sure the 'patients' directory exists.
            if (!System.IO.Directory.Exists("patients"))
            {
                System.IO.Directory.CreateDirectory("patients");
            }

            //make sure the 'templates' directory exists.
            if (!System.IO.Directory.Exists("templates"))
            {
                System.IO.Directory.CreateDirectory("templates");
            }


            //make sure the 'templates' directory exists.
            if (!System.IO.Directory.Exists("log"))
            {
                System.IO.Directory.CreateDirectory("log");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
