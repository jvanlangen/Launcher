// Launch.ModifierKeys
using System;

namespace Launcher
{
	[Flags]
	public enum ModifierKeys : uint
	{
		Alt = 0x1,
		Control = 0x2,
		Shift = 0x4,
		Win = 0x8
	}
}