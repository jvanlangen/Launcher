using Shell32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Launcher
{
    public partial class FormMain : Form
    {
        private static readonly Font _font = new Font("Consolas", 14f);

        private static readonly Shell _shell = (Shell)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("13709620-C279-11CE-A49E-444553540000")));

        private static readonly Dictionary<string, ShortcutInfo> _shortcutInfos = new Dictionary<string, ShortcutInfo>();

        private static SolidBrush HighlightBrush = new SolidBrush(SystemColors.Highlight);

        private static SolidBrush GradientInactiveCaptionBrush = new SolidBrush(SystemColors.GradientInactiveCaption);

        private static SolidBrush GrayBrush = new SolidBrush(Color.Gray);

        private static SolidBrush DarkGrayBrush = new SolidBrush(Color.DarkGray);

        private static LinearGradientBrush _backgroundBrush;

        private static LinearGradientBrush _backgroundBrush2;

        private static SolidBrush WhiteBrush = new SolidBrush(Color.White);

        private static SolidBrush BlackBrush = new SolidBrush(Color.Black);

        private const int ItemHeight = 26;

        private const double MaxOpacity = 0.9;

        private int hotItem = -1;

        private int _downIndex = -1;

        private bool _fadeOut;

        public FormMain()
        {
            InitializeComponent();
            listBox1.ItemHeight = 26;
            label1.Text = Program.GetTitlePrefix();
            label2.Text = Program.GetVersion();
            Text = Program.GetTitle();
            base.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            BuildMenu();
        }

        protected int HotItem
        {
            get
            {
                return hotItem;
            }
            set
            {
                if (hotItem != value)
                {
                    hotItem = value;
                    listBox1.Invalidate();
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern int MapVirtualKey(uint uCode, uint uMapType);

        public static void RefreshShortcuts()
        {
            foreach (FolderItem item in _shell.NameSpace(Program.GetShortcutsPath()).Items())
            {
                try
                {
                    if (item.IsLink && !_shortcutInfos.TryGetValue(item.Path, out ShortcutInfo value))
                    {
                        value = new ShortcutInfo();
                        ShellLinkObject shellLinkObject = (ShellLinkObject)(dynamic)item.GetLink;
                        value.DisplayName = Path.GetFileNameWithoutExtension(item.Name);
                        value.Filename = item.Path;
                        shellLinkObject.GetIconLocation(out string pbs);
                        Icon icon = null;
                        if (!string.IsNullOrWhiteSpace(pbs))
                        {
                            try
                            {
                                icon = Icon.ExtractAssociatedIcon(pbs);
                            }
                            catch
                            {
                            }
                        }
                        if (icon == null)
                        {
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(shellLinkObject.Path))
                                {
                                    icon = (((File.GetAttributes(shellLinkObject.Path) & FileAttributes.Directory) == FileAttributes.Directory) ? Icon.ExtractAssociatedIcon("c:\\windows\\explorer.exe") : Icon.ExtractAssociatedIcon(shellLinkObject.Path));
                                }
                            }
                            catch
                            {
                            }
                        }
                        if (icon == null)
                        {
                            try
                            {
                                if (!string.IsNullOrWhiteSpace(item.Path))
                                {
                                    icon = Icon.ExtractAssociatedIcon(item.Path);
                                }
                            }
                            catch
                            {
                            }
                        }
                        if (icon == null)
                        {
                            try
                            {
                                icon = Icon.ExtractAssociatedIcon("C:\\windows\\HelpPane.exe");
                            }
                            catch
                            {
                            }
                        }
                        value.Icon = icon;
                        _shortcutInfos.Add(item.Path, value);
                    }
                }
                catch (Exception)
                {
                }
            }
            HashSet<string> hashSet = new HashSet<string>(Directory.GetFiles(Program.GetShortcutsPath()));
            KeyValuePair<string, ShortcutInfo>[] array = _shortcutInfos.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                KeyValuePair<string, ShortcutInfo> keyValuePair = array[i];
                if (!hashSet.Contains(keyValuePair.Value.Filename))
                {
                    _shortcutInfos.Remove(keyValuePair.Key);
                }
            }
        }

        private bool ContainsStringIgnoreCase(string s, string part)
        {
            return Thread.CurrentThread.CurrentUICulture.CompareInfo.IndexOf(s, part, CompareOptions.IgnoreCase) >= 0;
        }

        private bool StartsWithStringIgnoreCase(string s, string part)
        {
            return Thread.CurrentThread.CurrentUICulture.CompareInfo.IndexOf(s, part, CompareOptions.IgnoreCase) == 0;
        }

        private void BuildMenu()
        {
            try
            {
                string text = textBox1.Text.Trim();
                List<ShortcutInfo> list = new List<ShortcutInfo>();
                List<ShortcutInfo> list2 = new List<ShortcutInfo>();
                foreach (ShortcutInfo item in _shortcutInfos.Values.OrderBy((ShortcutInfo item) => item.DisplayName))
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            list2.Add(item);
                        }
                        else
                        {
                            if (StartsWithStringIgnoreCase(item.DisplayName, text))
                            {
                                list.Add(item);
                            }
                            if (ContainsStringIgnoreCase(item.DisplayName, text))
                            {
                                list2.Add(item);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                List<ShortcutInfo> list3 = (list.Count != 0 && text.Length <= 2) ? list : list2;
                listBox1.BeginUpdate();
                try
                {
                    listBox1.Items.Clear();
                    foreach (ShortcutInfo item2 in list3)
                    {
                        listBox1.Items.Add(item2);
                    }
                    int num = (Screen.PrimaryScreen.WorkingArea.Height - textBox1.Height - panel1.Height) / 26 * 26;
                    int num2 = listBox1.Items.Count * 26;
                    if (num2 > num)
                    {
                        num2 = num;
                    }
                    listBox1.Height = num2;
                    base.ClientSize = new Size(base.Width, listBox1.Height + textBox1.Height + panel1.Height);
                    SetDesktopLocation(Screen.PrimaryScreen.WorkingArea.Width - base.ClientRectangle.Width, Screen.PrimaryScreen.WorkingArea.Height - base.ClientRectangle.Height);
                    Point point = new Point(0, 0);
                    Point point2 = new Point(base.ClientSize.Width, base.ClientSize.Height);
                    _backgroundBrush = new LinearGradientBrush(point, point2, Color.Gray, Color.DarkGray);
                    _backgroundBrush2 = new LinearGradientBrush(point, point2, Color.DarkSlateBlue, Color.Gray);
                }
                finally
                {
                    listBox1.EndUpdate();
                }
            }
            catch (Exception)
            {
            }
        }

        private void FadeOut()
        {
            _fadeOut = true;
            timer1.Enabled = true;
        }

        private void FormMain_Deactivate(object sender, EventArgs e)
        {
            FadeOut();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            base.TopMost = true;
            Activate();
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                FadeOut();
            }
            if (e.KeyCode == Keys.Return)
            {
                ExecuteSelected();
            }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            bool flag = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            bool flag2 = e.Index == HotItem;
            if (e.Index > -1)
            {
                ShortcutInfo shortcutInfo = (ShortcutInfo)listBox1.Items[e.Index];
                Brush brush = WhiteBrush;
                Brush brush2;
                if (flag)
                {
                    brush2 = HighlightBrush;
                }
                else if (!flag2)
                {
                    brush2 = ((e.Index % 2 != 0) ? _backgroundBrush : _backgroundBrush2);
                }
                else
                {
                    brush2 = GradientInactiveCaptionBrush;
                    brush = BlackBrush;
                }
                e.Graphics.FillRectangle(brush2, e.Bounds);
                if (shortcutInfo.Icon != null)
                {
                    e.Graphics.DrawIcon(shortcutInfo.Icon, new Rectangle(e.Bounds.Left + 2, e.Bounds.Top + 1, 24, 24));
                }
                Rectangle r = new Rectangle(e.Bounds.Left + 28, e.Bounds.Top + 1, e.Bounds.Width - 26, e.Bounds.Height - 1);
                e.Graphics.DrawString(shortcutInfo.DisplayName, _font, brush, r, StringFormat.GenericDefault);
            }
            if (flag)
            {
                e.DrawFocusRectangle();
            }
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            HotItem = listBox1.IndexFromPoint(listBox1.PointToClient(Cursor.Position));
        }

        private void listBox1_MouseLeave(object sender, EventArgs e)
        {
            HotItem = -1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_fadeOut)
            {
                base.Opacity -= 0.2;
                if (base.Opacity <= 0.0)
                {
                    base.Opacity = 0.0;
                    timer1.Enabled = false;
                    Close();
                }
            }
            else
            {
                base.Opacity += 0.3;
                if (base.Opacity > 0.9)
                {
                    base.Opacity = 0.9;
                    timer1.Enabled = false;
                }
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _downIndex = listBox1.SelectedIndex;
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex != -1 && listBox1.SelectedIndex == _downIndex)
                {
                    ExecuteSelected();
                }
            }
            finally
            {
                _downIndex = -1;
            }
        }

        private void ExecuteSelected()
        {
            ShortcutInfo shortcutInfo = (ShortcutInfo)listBox1.Items[listBox1.SelectedIndex];
            try
            {
                Process.Start(shortcutInfo.Filename);
                FadeOut();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BuildMenu();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (listBox1.Items.Count == 0)
                {
                    return;
                }
                listBox1.SelectedIndex = 0;
                ExecuteSelected();
                bool handled = e.SuppressKeyPress = true;
                e.Handled = handled;
            }
            if (e.KeyCode == Keys.Up && listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.Focus();
                bool handled = e.SuppressKeyPress = true;
                e.Handled = handled;
            }
            if (e.KeyCode == Keys.Down && listBox1.Items.Count > 0)
            {
                if (listBox1.Items.Count == 0)
                {
                    listBox1.SelectedIndex = -1;
                }
                else if (listBox1.Items.Count == 1)
                {
                    listBox1.SelectedIndex = 0;
                }
                else
                {
                    listBox1.SelectedIndex = 1;
                }
                listBox1.Focus();
                bool handled = e.SuppressKeyPress = true;
                e.Handled = handled;
            }
            if (e.KeyCode == Keys.Escape)
            {
                FadeOut();
                bool handled = e.SuppressKeyPress = true;
                e.Handled = handled;
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                bool handled = e.SuppressKeyPress = true;
                e.Handled = handled;
            }
            if (e.KeyCode == Keys.Escape)
            {
                bool handled = e.SuppressKeyPress = true;
                e.Handled = handled;
            }
            if (e.KeyCode == Keys.Up)
            {
                bool handled = e.SuppressKeyPress = true;
                e.Handled = handled;
            }
            if (e.KeyCode == Keys.Down)
            {
                bool handled = e.SuppressKeyPress = true;
                e.Handled = handled;
            }
        }

        private void listBox1_Leave(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = -1;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.Prior && e.KeyCode <= Keys.Down) || e.KeyCode == Keys.Return)
            {
                return;
            }
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            textBox1.Focus();
            if (e.KeyCode == Keys.Back)
            {
                if (textBox1.Text.Length > 0)
                {
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                }
            }
            else
            {
                char c = Convert.ToChar(MapVirtualKey((uint)e.KeyData, 2u));
                textBox1.Text = c.ToString();
            }
            textBox1.Select(textBox1.Text.Length, 0);
            e.Handled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == 256)
            {
                Convert.ToChar(MapVirtualKey((uint)keyData, 2u));
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
