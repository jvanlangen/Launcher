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
		public const string Url = "http://launcher.vanlangen.biz";

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
			string text = LauncherSettings.Default.ShortcutsPath;
			if (!Path.IsPathRooted(text))
			{
				text = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), text);
			}
			return text;
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
					if (!Directory.Exists(GetShortcutsPath()))
					{
						MessageBox.Show("Shortcuts path is not valid, check the configuration.\r\nWhen no configuration exists, a 'shortcuts' subfolder will be chosen as default.\r\n\r\nFor downloading the original binaries, check: http://launcher.vanlangen.biz", "Error loading shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						using (new MainApp())
						{
							Application.Run();
						}
					}
				}
			}
		}
	}
}