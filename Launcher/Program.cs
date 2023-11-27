// Launch.Program
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Launcher
{
    internal static class Program
    {
        public const string Url = "https://github.com/jvanlangen/Launcher";

        public static string GetTitle()
        {
            return GetTitlePrefix() + " " + GetVersion();
        }

        public static string GetTitlePrefix()
        {
            return "Launcher";
        }

        public static string GetVersion()
        {
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            return "v" + versionInfo.FileVersion;
        }

        public static string GetShortcutsPath()
        {

            var appData = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            appData = Path.Combine(appData, "Launcher", "shortcuts");

            return appData;
        }

        [STAThread]
        private static void Main()
        {
            bool createdNew;
            using (EventWaitHandle eventWaitHandle = new EventWaitHandle(initialState: true, EventResetMode.ManualReset, GetTitlePrefix(), out createdNew))
            {
                if (createdNew || !eventWaitHandle.WaitOne(100))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(defaultValue: false);
                    var shortcutsPath = GetShortcutsPath();

                    if (!Directory.Exists(shortcutsPath))
                    {
                        Directory.CreateDirectory(shortcutsPath);
                        // create example shortcut
                        var exampleShortcutFilename = Path.Combine(shortcutsPath, "notepad.exe.lnk");
                        using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Launcher.Notepad.lnk"))
                        using (var fileStream = File.OpenWrite(exampleShortcutFilename))
                            s.CopyTo(fileStream);
                    }

                    using (new MainApp())
                        Application.Run();
                }
            }
        }
    }
}