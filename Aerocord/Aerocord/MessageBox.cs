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
    public partial class MessageBox : UserControl
    {
        public MessageBox()
        {
            InitializeComponent();
        }

        public string Username
        {
            get => name.Text;
            set => name.Text = value;
        }

        public string Content
        {
            get => content.Text;
            set => content.Text = value;
        }

        public Size LabelMaximumSize
        {
            get => content.MaximumSize;
            set { 
                name.MaximumSize = value;
                content.MaximumSize = value;

            }
        }
    }
}
