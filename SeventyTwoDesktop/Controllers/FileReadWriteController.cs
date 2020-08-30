using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Threading.Tasks;
using System.IO;
using System.Timers;

namespace SeventyTwoDesktop.Controllers
{


    class FileReadWriteController
    {

        public static readonly object WriteLocker = new object( );

        public static readonly object TimerLocker = new object( );


        public static string addPath( string directoryName, string extraPath1 = "", string extraPath2 = "" ) {
            string[] paths = { Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments ), "SeventyTwo", directoryName, extraPath1, extraPath2 };
            return Path.Combine( paths );
        }

        public FileReadWriteController( string path, string backupPath = "", double interval = 5000 ) {
            FileName = addPath(path);
            TimerInterval = interval;
            BackupFileName = backupPath;
            if( File.Exists( FileName ) ) {
                FileContentsToWrite = File.ReadAllText( FileName );
            } else {
                FileContentsToWrite = string.Empty;
            }
        }

        private string FileName { get; set; } = string.Empty;
        private string BackupFileName { get; set; } = string.Empty;
        private string FileContentsToWrite { get; set; } = string.Empty;
        private System.Timers.Timer MainTimer { get; set; }
        private double TimerInterval { get; set; }
        private bool Dirty { get; set; } = false;
        private bool WriteSuccess { get; set; }
        private int IntervalsSinceLastWrite { get; set; }
        public bool LastWriteSucceeded { get { return WriteSuccess; } }
        public string FileContents { get { return FileContentsToWrite; } }
        public string TargetFile { get { return FileName; } }



        private void InitTimer() {
            //If we don't have a timer, start one.
            if( MainTimer == null ) {
                MainTimer = new System.Timers.Timer( TimerInterval );
                MainTimer.Elapsed += new ElapsedEventHandler( TimerEvent ); 
                MainTimer.Start( );
                IntervalsSinceLastWrite = 0;
            //If our timer isn't running, start it and reset our intervals.
            } else if ( !MainTimer.Enabled ) {
                MainTimer.Start( );
                IntervalsSinceLastWrite = 0;
            }
        }

        private void TimerEvent( object ElapsedEventArgs, EventArgs e ) {
            //If the data has changed, we definitely need to write it.
            if ( Dirty ) {
                lock( TimerLocker ){
                    PerformWrite( );
                }
            //If we have had 5 intervals since we last wrote, we should stop this timer and wait for another text event.
            } else if ( IntervalsSinceLastWrite >= 5 ) {
                MainTimer.Stop( );
            //We haven't had data changed, but we wrote data recently.  Wait for another write event.
            } else {
                IntervalsSinceLastWrite++;
            }
            
        }

        private void PerformWrite() {

            try {

                if( Dirty ) {
                    lock( WriteLocker ) {
                        //Should we be backing this up?
                        if( !string.IsNullOrEmpty( BackupFileName ) ) {
                            //Make a quick copy of the old list
                            if( File.Exists( FileName ) ) {
                                File.Copy( FileName, BackupFileName, true );
                            }
                        }

                        File.WriteAllText( FileName, FileContentsToWrite );

                        Dirty = false;
                        WriteSuccess = true;
                        IntervalsSinceLastWrite = 0;
                    }
                }

            } catch ( Exception exc ) { Models.Log.WriteToLog( exc ); }
        }

        public void WriteDataToFile( string dataToWrite ) {
            Dirty = true;
            FileContentsToWrite = dataToWrite;
            WriteSuccess = false;
            InitTimer( );
        }

        public void ForceWrite( string dataToWrite ) {
            //Stop the timer to prevent any possible race conditions.
            MainTimer.Stop( );
            Dirty = true;
            WriteSuccess = false;
            FileContentsToWrite = dataToWrite;
            PerformWrite( );
        }

        public void ForceRead() {
            if( File.Exists( FileName ) ) {
                FileContentsToWrite = File.ReadAllText( FileName );
            }
        }

    }
}
