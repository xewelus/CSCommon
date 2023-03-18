using Common;
using System;
using System.IO;
using System.Windows;
using Common.InputHooks;
using CommonWpf.Classes.UI;
using Common.Classes;

namespace CommonWpf.Classes
{
	public static class AppInitializer
	{
		/// <summary>
		/// Main initilizatioin method.
		/// </summary>
		public static bool Initialize(bool needFileLog = true, bool singleInstance = false, bool needKeyboardHook = false)
		{
			UIHelper.InRuntime = true;
			ExceptionHandler.Init();

			if (needFileLog)
			{
				ExceptionHandler.OnError = OnError;
			}

			if (singleInstance)
			{
				SingleGlobalInstance instance = new SingleGlobalInstance();
				if (!instance.HasHandle || instance.Abandoned)
				{
					Environment.Exit(0);
					return false;
				}

				Application.Current.Exit += (sender, args) => instance.Dispose();
			}

			if (needKeyboardHook)
			{
				GlobalHookExitFunc exitFunc = new GlobalHookExitFunc();
				KeyboardHook.Current = new KeyboardHook(exitFunc);
				KeyboardHook.Current.Start();
			}

			return true;
		}

		private static void OnError(Exception exception)
		{
			string path = FS.GetAppPath("errors.log");
			File.AppendAllText(path, $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff}] {exception}\r\n\r\n");
		}
	}
}
