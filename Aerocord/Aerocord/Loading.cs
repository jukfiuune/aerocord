using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aerocord
{
    public partial class Loading : AutoForm
    {
        public Loading(Main main)
        {
            InitializeComponent();
            
            main.Hide();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GlassMargins = new Padding(-1, -1, -1, -1);
        }
    }
}
