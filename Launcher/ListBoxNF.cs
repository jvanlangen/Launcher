// Launch.ListBoxNF
using System.Windows.Forms;

namespace Launcher
{
	public class ListBoxNF : ListBox
	{
		public ListBoxNF()
		{
			DoubleBuffered = true;
		}

		protected override void DefWndProc(ref Message m)
		{
			if (m.Msg != 20)
			{
				base.DefWndProc(ref m);
			}
		}
	}
}