using System;
using System.Collections.Generic;
using OfficeOpenXml;
using SeventyTwoDesktop.Controllers;
using SeventyTwoDesktop.Models;
using System.IO;


namespace SeventyTwoDesktop.Controllers {
    class SpreadsheetExportController {
        
        public static void CreateExportFile( string filename ) {

            Dictionary<string, string> TemplateTypes = TemplateController.GetTemplateTypes( );

            using ( ExcelPackage excel = new ExcelPackage( ) ) {

                //Do profile page first
                excel.Workbook.Worksheets.Add( "Profiles" );

                Dictionary<string, List<string[]>> data = new Dictionary<string, List<string[]>>( );


                data[ "Profiles" ] = new List<string[]>( ) {
                    new string[] { "Type", "Title", "Guid", "Name", "Number", "Address", "Community", "Location", "Gender", "Birthdate", "Phone Number" }
                };

                foreach ( string template in TemplateTypes.Keys ) {

                    if ( template != "permanent" ) {
                        excel.Workbook.Worksheets.Add( template );

                        data[ template ] = new List<string[]>( );

                        TemplateController tc = new TemplateController( template, TemplateStyle.Blank );


                        string[] rowData = new string[ tc.GetTemplateItems( ).Count ];

                        int i = 0;
                        foreach ( TemplateItem ti in tc.GetTemplateItems( ).Values ) {
                            rowData[ i ] = ti.title;
                            i++;
                        }
                        data[ template ].Add( rowData );
                    }
                }

                //popoulate cells

                foreach ( ProfileListItem pli in ProfileListController.ProfileList ) {
                    ProfileController x = new ProfileController( pli.GUID );
                    data[ "Profiles" ].Add( new string[] {
                        x.Profile.type,
                        x.Profile.title,
                        x.Profile.guid,
                        x.Profile.name,
                        x.Profile.number,
                        x.Profile.address,
                        x.Profile.community,
                        x.Profile.location,
                        x.Profile.gender,
                        x.Profile.birthdate.ToString(),
                        x.Profile.phonenumber
                    } );

                    foreach ( RecordController R in x.Records.Values ) {


                        string[] rowData = new string[ R.GetTemplateItems( ).Count ];

                        int i = 0;
                        foreach ( TemplateItem ti in R.GetTemplateItems( ).Values ) {
                            rowData[ i ] = ti.value;
                            i++;
                        }

                        data[ R.GetTemplateType( ) ].Add( rowData );
                    }
                }

                //Create header range
                foreach ( KeyValuePair<string, List<string[]>> p in data ) {
                    //loop through fields
                    excel.Workbook.Worksheets[ p.Key ].Cells[ 2, 1 ].LoadFromArrays( p.Value );
                }

                excel.SaveAs( new FileInfo( filename ) );
            }
        }
    }
}
