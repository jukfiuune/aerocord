using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Aerocord
{
    [DesignerCategory("code")]
    public partial class FlowLayoutPanelNoScrollbars : FlowLayoutPanel, IMessageFilter
    {
        public FlowLayoutPanelNoScrollbars()
        {
            SetStyle(ControlStyles.UserMouse | ControlStyles.Selectable, true);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Application.AddMessageFilter(this);

            VerticalScroll.LargeChange = 60;
            VerticalScroll.SmallChange = 20;
            HorizontalScroll.LargeChange = 60;
            HorizontalScroll.SmallChange = 20;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Application.RemoveMessageFilter(this);
            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_PAINT:
                case WM_ERASEBKGND:
                case WM_NCCALCSIZE:
                    if (DesignMode || !AutoScroll) break;
                    ShowScrollBar(this.Handle, SB_SHOW_BOTH, false);
                    break;
                case WM_MOUSEWHEEL:
                    // Handle Mouse Wheel for other specific cases
                    int delta = (int)(m.WParam.ToInt64() >> 16);
                    int direction = Math.Sign(delta);
                    ShowScrollBar(this.Handle, SB_SHOW_BOTH, false);
                    break;
            }
        }

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEWHEEL:
                case WM_MOUSEHWHEEL:
                    if (DesignMode || !AutoScroll) return false;
                    if (VerticalScroll.Maximum <= ClientSize.Height) return false;
                    // Should also check whether the ForegroundWindow matches the parent Form.
                    if (RectangleToScreen(ClientRectangle).Contains(MousePosition))
                    {
                        SendMessage(this.Handle, WM_MOUSEWHEEL, m.WParam, m.LParam);
                        return true;
                    }
                    break;
                case WM_LBUTTONDOWN:
                    // Pre-handle Left Mouse clicks for all child Controls
                    //Console.WriteLine($"WM_LBUTTONDOWN");
                    if (RectangleToScreen(ClientRectangle).Contains(MousePosition))
                    {
                        var mousePos = MousePosition;
                        if (GetForegroundWindow() != TopLevelControl.Handle) return false;
                        // The hosted Control that contains the mouse pointer 
                        var ctrl = FromHandle(ChildWindowFromPoint(this.Handle, PointToClient(mousePos)));
                        // A child Control of the hosted Control that will be clicked 
                        // If no child Controls at that position the Parent's handle
                        var child = FromHandle(WindowFromPoint(mousePos));
                    }
                    return false;
                    // Eventually, if you don't want the message to reach the child Control
                    // return true; 
            }
            return false;
        }

        private const int WM_PAINT = 0x000F;
        private const int WM_ERASEBKGND = 0x0014;
        private const int WM_NCCALCSIZE = 0x0083;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_MOUSEHWHEEL = 0x020E;
        private const int SB_SHOW_VERT = 0x1;
        private const int SB_SHOW_BOTH = 0x3;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll")]
        internal static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, Point point);
    }
}