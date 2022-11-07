using System.Windows;
using Common.InputHooks;

namespace CommonWpf.Classes
{
	internal class GlobalHookExitFunc : GlobalHook.ExitFunc
	{
		protected override void Init()
		{
			Application.Current.Exit += (sender, args) => this.Exit();
		}
	}
}
