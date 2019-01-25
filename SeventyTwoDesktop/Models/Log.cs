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
                string logFileName = "log/" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt";
                File.AppendAllText(logFileName, exc.ToString());
                
            } catch ( Exception logExc ) {
                string errText = Environment.NewLine + Environment.NewLine + exc.ToString() + Environment.NewLine + logExc.ToString();
                var result = System.Windows.Forms.MessageBox.Show("Sorry about this.  A really strange error has happened.  You probably want to email whoever is helping you this error.  If you click yes, I'll copy this to your clipboard, then you can paste it into an email to your technical person." +errText , "Argh. Sorry.", System.Windows.Forms.MessageBoxButtons.YesNo);
                if ( result == System.Windows.Forms.DialogResult.Yes )
                {
                    System.Windows.Forms.Clipboard.SetText(errText);
                } 
            }
        } 
    }
}
