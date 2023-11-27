using System.Diagnostics;
using System.Drawing;
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
            linkLabel1.Text = "https://github.com/jvanlangen/Launcher";
        }


        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/jvanlangen/Launcher");
            base.DialogResult = DialogResult.OK;
        }
    }
}