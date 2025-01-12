using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Aerocord
{
    public partial class ServerEntry : PictureBox
    {
        public ServerEntry()
        {
            this.BackColor = Color.Black;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Cursor = Cursors.Hand;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            using (var gp = new GraphicsPath())
            {
                Rectangle r = new Rectangle(-1, -1, this.Width + 1, this.Height + 1);
                /*int d = 25;
                gp.AddArc(r.X, r.Y, d, d, 180, 90);
                gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
                gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
                gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);*/
                gp.AddEllipse(r);
                this.Region = new Region(gp);
            }
        }

        public string ServerId;
    }
}