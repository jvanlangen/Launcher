using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Launcher
{
	public static class ExtractIcon
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct SHFILEINFO
		{
			public IntPtr hIcon;

			public int iIcon;

			public uint dwAttributes;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;

			public SHFILEINFO(bool b)
			{
				hIcon = IntPtr.Zero;
				iIcon = 0;
				dwAttributes = 0u;
				szDisplayName = "";
				szTypeName = "";
			}
		}

		[Flags]
		private enum SHGFI
		{
			Icon = 0x100,
			DisplayName = 0x200,
			TypeName = 0x400,
			Attributes = 0x800,
			IconLocation = 0x1000,
			ExeType = 0x2000,
			SysIconIndex = 0x4000,
			LinkOverlay = 0x8000,
			Selected = 0x10000,
			Attr_Specified = 0x20000,
			LargeIcon = 0x0,
			SmallIcon = 0x1,
			OpenIcon = 0x2,
			ShellIconSize = 0x4,
			PIDL = 0x8,
			UseFileAttributes = 0x10,
			AddOverlays = 0x20,
			OverlayIndex = 0x40
		}

		private const int MAX_PATH = 260;

		private const int MAX_TYPE = 80;

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		private static extern int SHGetFileInfo(string pszPath, int dwFileAttributes, out SHFILEINFO psfi, uint cbfileInfo, SHGFI uFlags);

		public static Icon GetIcon(string strPath, bool bSmall)
		{
			SHFILEINFO psfi = new SHFILEINFO(b: true);
			int cbfileInfo = Marshal.SizeOf((object)psfi);
			SHGFI uFlags = (!bSmall) ? (~SHGFI.LinkOverlay) : (~SHGFI.LinkOverlay);
			SHGetFileInfo(strPath, 256, out psfi, (uint)cbfileInfo, uFlags);
			return Icon.FromHandle(psfi.hIcon);
		}

		public static Icon GetIcon(string strPath)
		{
			SHFILEINFO psfi = new SHFILEINFO(b: true);
			int cbfileInfo = Marshal.SizeOf((object)psfi);
			SHGFI uFlags = SHGFI.Icon | SHGFI.DisplayName | SHGFI.LinkOverlay;
			SHGetFileInfo(strPath, 256, out psfi, (uint)cbfileInfo, uFlags);
			return Icon.FromHandle(psfi.hIcon);
		}
	}
}