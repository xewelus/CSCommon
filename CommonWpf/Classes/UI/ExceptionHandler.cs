using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CommonWpf.Classes.UI
{
	public static class ExceptionHandler
	{
		internal static void Init()
		{
			Application.Current.DispatcherUnhandledException += CurrentOnDispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
		}

		public static void Catch(Exception ex)
		{
			if (ex == null)
			{
				UIHelper.ShowError($"Empty error:\r\n{Environment.StackTrace}");
				return;
			}
			UIHelper.ShowError(ex);
		}

		private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			Catch(e.Exception);
			e.SetObserved();
		}

		private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			if (e.ExceptionObject == null)
			{
				Catch(null);
			}
			else if (e.ExceptionObject is Exception exception)
			{
				Catch(exception);
			}
			else
			{
				UIHelper.ShowError(e.ExceptionObject?.ToString());
			}
		}

		private static void CurrentOnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			Catch(e.Exception);
			e.Handled = true;
		}
	}
}
