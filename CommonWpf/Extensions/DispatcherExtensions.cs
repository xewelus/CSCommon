using System;
using System.Windows.Threading;

namespace CommonWpf.Extensions
{
	public static class DispatcherExtensions
	{
		public static void InvokeAction(this Dispatcher dispatcher, Action action)
		{
			if (dispatcher.CheckAccess())
			{
				action();
			}
			else
			{
				if (!dispatcher.HasShutdownStarted && !dispatcher.HasShutdownFinished)
				{
					dispatcher.Invoke(action);
				}
			}
		}
	}
}
