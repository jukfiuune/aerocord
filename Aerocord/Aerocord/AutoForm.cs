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

        public Dictionary<string, Dictionary<string, Padding>> glassMargins = new Dictionary<string, Dictionary<string, Padding>>
        {
            { "Light", new Dictionary<string, Padding>{
                { "Default", new Padding(66, 0, 0, 0) }
            } },
            { "Dark", new Dictionary<string, Padding>{
                { "Default", new Padding(66, 0, 0, 0) }
            } }
        };

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            switch (RenderMode)
            {
                case "Aero":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 1);
                    break;
                case "Mica":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 2);
                    break;
                case "Acrylic":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 3);
                    break;
                case "Mica Alt":
                    PInvoke.Methods.SetWindowAttribute(Handle, PInvoke.ParameterTypes.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 4);
                    break;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            GlassMargins = glassMargins["Light"]["Default"];
        }
    }
}
