using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SeventyTwoDesktop.Controllers;
using SeventyTwoDesktop.Models;
using System.IO.Compression;

namespace SeventyTwoDesktop.Controllers
{
    class SyncController
    {

        /*
         * The purpose of the SyncController is to handle synchronization of profiles
         * 
         * We need to read and create a single large JSON file with all changes made since last sync
         * We will zip this file up and then it will be sent out by a manual process.
         * Eventually, it would be great to send this up to a webservice and sync records that way.
         * 
         */


        public static void createExportZip( string DestinationZipFile ) {

            ZipFile.CreateFromDirectory( "/profiles", DestinationZipFile );

        }
        

        private void compareData()
        {
            readFiles( );
        }

        private void readFiles( ) {
           
        }

        private void writeNewFiles( ) {

        }

    }
}
