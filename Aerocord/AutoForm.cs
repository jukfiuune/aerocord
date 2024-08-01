using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAero;

namespace Aerocord
{
    public class AutoForm : WindowsFormsAero.AeroForm
    {
        public bool AutoColorMode = false;
        public bool DarkMode = false;
        public string RenderMode = "Aero";

        public Padding GlassMarginsLight = new Padding(-1, -1, -1, -1);
        public Padding GlassMarginsDark = new Padding(-1, -1, -1, -1);
    }
}
