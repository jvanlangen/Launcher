// Launch.MainApp
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Launcher
{
    public class MainApp : IDisposable
    {
        private FormMain _formMain;

        private KeyboardHook _hook;

        private FileSystemWatcher _fileSystemWatcher;

        private bool _shortcutChanged;

        private readonly NotifyIcon _notifyIcon;

        private readonly Keys _keys;

        private readonly ModifierKeys _modifierKeys;

        private ContextMenu CreateContextMenu()
        {
            return new ContextMenu
            {
                MenuItems =
            {
                {
                    "&Show",
                    (EventHandler)Show_Click
                },
                "-",
                {
                    "&Open shortcut folder",
                    (EventHandler)OpenShortcutFolder_Click
                },
                "-",
                {
                    "&About",
                    (EventHandler)About_Click
                },
                "-",
                {
                    "E&xit",
                    (EventHandler)Exit_Click
                }
            }
            };
        }

        private void ShowMainForm()
        {
            if (_formMain != null)
            {
                _formMain.Dispose();
            }
            if (Directory.Exists(Program.GetShortcutsPath()))
            {
                if (_shortcutChanged)
                {
                    FormMain.RefreshShortcuts();
                    _shortcutChanged = false;
                }
                _formMain = new FormMain();
                _formMain.FormClosed += delegate
                {
                    _formMain = null;
                };
                _formMain.Show();
            }
        }

        private void OpenShortcutFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Program.GetShortcutsPath());
        }

        private void Show_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void About_Click(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Icon_DoubleClick(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Key == _keys && e.Modifier == _modifierKeys)
            {
                ShowMainForm();
            }
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            _shortcutChanged = true;
        }

        public MainApp()
        {

            _keys = Keys.Oemtilde;


            _modifierKeys = ModifierKeys.Win;

            _hook = new KeyboardHook();
            try
            {
                _hook.RegisterHotKey(_modifierKeys, _keys);
                _hook.KeyPressed += Hook_KeyPressed;
            }
            catch
            {
                MessageBox.Show("Unable to register hotkey, change the hotkey in the configuration");
                throw;
            }
            _fileSystemWatcher = new FileSystemWatcher(Program.GetShortcutsPath());
            _fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            _fileSystemWatcher.Created += FileSystemWatcher_Changed;
            _fileSystemWatcher.Renamed += FileSystemWatcher_Changed;
            _fileSystemWatcher.Deleted += FileSystemWatcher_Changed;
            _fileSystemWatcher.EnableRaisingEvents = true;
            FormMain.RefreshShortcuts();
            _notifyIcon = new NotifyIcon
            {
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                ContextMenu = CreateContextMenu(),
                Text = Program.GetTitle(),
                Visible = true
            };
            _notifyIcon.DoubleClick += Icon_DoubleClick;
        }

        public void Dispose()
        {
            _hook.Dispose();
            _notifyIcon.Dispose();
            _fileSystemWatcher.Dispose();
        }
    }
}