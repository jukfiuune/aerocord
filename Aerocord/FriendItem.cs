using System;
using System.Windows.Forms;

namespace Aerocord
{
    public partial class FriendItem : UserControl
    {
        public event EventHandler Clicked;

        protected virtual void OnClicked(EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }

        public FriendItem()
        {
            InitializeComponent();
            this.Click += (sender, e) => OnClicked(e);
        }

        public string Username
        {
            get => usernameLabel.Text;
            set => usernameLabel.Text = value;
        }

        public string ServerName
        {
            get => usernameLabel.Text;
            set => usernameLabel.Text = value;
        }

        public string ProfilePictureUrl
        {
            get => profilePictureFriend.ImageLocation;
            set => profilePictureFriend.ImageLocation = value;
        }
        public System.Drawing.Color LabelColor
        {
            get => usernameLabel.ForeColor;
            set => usernameLabel.ForeColor = value;
        }
    }
}
