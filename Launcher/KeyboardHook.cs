// Launch.KeyboardHook
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Launcher
{
	public sealed class KeyboardHook : IDisposable
	{
		private class Window : NativeWindow, IDisposable
		{
			private static readonly int WM_HOTKEY = 786;

			public event EventHandler<KeyPressedEventArgs> KeyPressed;

			public Window()
			{
				CreateHandle(new CreateParams());
			}

			protected override void WndProc(ref Message m)
			{
				base.WndProc(ref m);
				if (m.Msg == WM_HOTKEY)
				{
					Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
					ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);
					this.KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
				}
			}

			public void Dispose()
			{
				DestroyHandle();
			}
		}

		private int _currentId;

		private Window _window = new Window();

		public event EventHandler<KeyPressedEventArgs> KeyPressed;

		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		public KeyboardHook()
		{
			_window.KeyPressed += delegate (object sender, KeyPressedEventArgs args)
			{
				if (this.KeyPressed != null)
				{
					this.KeyPressed(this, args);
				}
			};
		}

		public void RegisterHotKey(ModifierKeys modifier, Keys key)
		{
			_currentId++;
			if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
			{
				throw new InvalidOperationException("Couldn’t register the hot key.");
			}
		}

		public void Dispose()
		{
			for (int num = _currentId; num > 0; num--)
			{
				UnregisterHotKey(_window.Handle, num);
			}
			_window.Dispose();
		}
	}
}