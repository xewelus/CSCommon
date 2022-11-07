using System.Windows.Forms;
using Common.InputHooks;

namespace CommonWpf.Classes
{
	internal class GlobalHookExitFunc : GlobalHook.ExitFunc
	{
		protected override void Init()
		{
			Application.ApplicationExit += (sender, args) => this.Exit();
		}
	}
}
