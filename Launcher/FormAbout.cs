using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            Text = Program.GetTitle();
            base.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            pictureBox1.Image = base.Icon.ToBitmap();
            linkLabel1.Text = "http://launcher.vanlangen.biz";
        }
    }
}
