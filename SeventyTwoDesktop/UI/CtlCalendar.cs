using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeventyTwoDesktop.UI
{
    public partial class CtlCalendar : UserControl
    {
        public CtlCalendar( ) {
            InitializeComponent( );
            LoadData( );
        }

        public void LoadData() {
            CalDate1.LoadData( new Controllers.CalendarDateController( new DateTime( 2019, 02, 09 ) ) );
            CalDate2.LoadData( new Controllers.CalendarDateController( new DateTime( 2019, 02, 10 ) ) );
            CalDate3.LoadData( new Controllers.CalendarDateController( new DateTime( 2019, 02, 11 ) ) );
            CalDate4.LoadData( new Controllers.CalendarDateController( new DateTime( 2019, 02, 12 ) ) );
            CalDate5.LoadData( new Controllers.CalendarDateController( new DateTime( 2019, 02, 13 ) ) );
            CalDate6.LoadData( new Controllers.CalendarDateController( new DateTime( 2019, 02, 14 ) ) );
            CalDate7.LoadData( new Controllers.CalendarDateController( new DateTime( 2019, 02, 15 ) ) );
            
        }

        private void CalDate_ClickedEventHandler( object sender, EventArgs e ) {
            CalDateClickEventArgs cdcea = ( CalDateClickEventArgs )e;
            MessageBox.Show( cdcea.Date );
        }
    }
}
