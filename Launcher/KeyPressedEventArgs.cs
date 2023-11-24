// Launch.KeyPressedEventArgs
using System;
using System.Windows.Forms;

namespace Launcher
{
	public class KeyPressedEventArgs : EventArgs
	{
		public ModifierKeys Modifier
		{
			get;
		}

		public Keys Key
		{
			get;
		}

		internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
		{
			Modifier = modifier;
			Key = key;
		}
	}
}