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
    public partial class ChannelEntry : UserControl
    {
        public event EventHandler Clicked;

        protected virtual void OnClicked(EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }

        public ChannelEntry()
        {
            InitializeComponent();
            this.Click += (sender, e) => OnClicked(e);
        }

        public Image ChannelType
        {
            get => type.Image;
            set => type.Image = value;
        }
        public string Channel
        {
            get => label.Text;
            set => label.Text = value;
        }

        public string ChatId;
        public string FriendId;
    }
}
