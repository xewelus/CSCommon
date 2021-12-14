using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CommonWpf.Classes.UI
{
	public static class ExceptionHandler
	{
		public static void Init()
		{
			Application.Current.DispatcherUnhandledException += CurrentOnDispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
		}

		private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			UIHelper.ShowError(e.Exception);
			e.SetObserved();
		}

		private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			UIHelper.ShowError(e.ExceptionObject as Exception);
		}

		private static void CurrentOnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			UIHelper.ShowError(e.Exception);
			e.Handled = true;
		}
	}
}
