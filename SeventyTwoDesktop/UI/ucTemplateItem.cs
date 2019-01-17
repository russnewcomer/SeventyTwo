using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeventyTwoDesktop.Models;
using Newtonsoft.Json.Linq;

namespace SeventyTwoDesktop
{
    public partial class ucTemplateItem : UserControl
    {
        private TemplateItem ti { get; set; }

        public ucTemplateItem()
        {
            InitializeComponent();
        }

        public void LoadTemplateItem( JObject template ) {
            ti = template;
        }
        
    }
}
