// Launch.Log
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Launcher
{
	public static class Log
	{
		private const string LogPath = "C:\\temp\\Launcher";

		[Conditional("DEBUG")]
		public static void Error(string message)
		{
			try
			{
				if (!Directory.Exists("C:\\temp\\Launcher"))
				{
					Directory.CreateDirectory("C:\\temp\\Launcher");
				}
				using (StreamWriter streamWriter = File.AppendText(Path.Combine("C:\\temp\\Launcher", $"{DateTime.Now:yyyyMMdd}_launcher.log")))
				{
					streamWriter.WriteLine($"{DateTime.Now:HH:mm:ss} |  {message}");
				}
			}
			catch
			{
				MessageBox.Show("Unable to write to log file, \r\nMessage: " + message);
			}
		}
	}
}