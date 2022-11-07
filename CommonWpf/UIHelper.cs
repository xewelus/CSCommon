﻿using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using Common.Extensions;
using CommonWpf.Forms;

namespace CommonWpf
{
	public static class UIHelper
	{
		public static bool InRuntime;
		public static void ShowError(Control control, string text, string caption = "Ошибка")
		{
			ShowMessageBox(GetCurrentForm(control), text, caption, MessageBoxButton.OK, MessageBoxImage.Error);
		}

		public static void ShowWarning(Control control, string text, string caption = "Внимание")
		{
			ShowMessageBox(GetCurrentForm(control), text, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
		}

		public static void ShowMessage(Control control, string text, string caption = "Внимание")
		{
			ShowMessageBox(GetCurrentForm(control), text, caption);
		}

		public static bool AskYesNo(Control control, string text, string caption = "Внимание", MessageBoxImage icon = MessageBoxImage.Question)
		{
			return ShowMessageBox(GetCurrentForm(control), text, caption, MessageBoxButton.YesNo, icon) == MessageBoxResult.Yes;
		}

		public static void ShowError(string text, string caption = "Ошибка")
		{
			ShowError((Control)null, text, caption);
		}

		public static void ShowError(Exception exception, string caption = "Ошибка")
		{
			try
			{
				string text = exception.ToNiceString();
				ShowTextDlg.ShowText(caption, text);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), caption, MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		public static void ShowWarning(string text, string caption = "Внимание")
		{
			ShowWarning(null, text, caption);
		}

		public static void ShowMessage(string text, string caption = "Внимание")
		{
			ShowMessage(null, text, caption);
		}

		public static bool AskYesNo(string text, string caption = "Внимание", MessageBoxImage icon = MessageBoxImage.Question)
		{
			return AskYesNo(null, text, caption, icon);
		}

		public static MessageBoxResult ShowMessageBox(
			string text,
			string caption = "Внимание",
			MessageBoxButton buttons = MessageBoxButton.OK,
			MessageBoxImage icon = MessageBoxImage.Information)
		{
			return ShowMessageBox(null, text, caption, buttons, icon);
		}

		public static Window GetCurrentForm(this Control control)
		{
			if (control == null) return null;
			return Window.GetWindow(control ?? TopForm);
		}

		public static MessageBoxResult ShowMessageBox(
			this Control control,
			string text,
			string caption = "Внимание",
			MessageBoxButton buttons = MessageBoxButton.OK,
			MessageBoxImage icon = MessageBoxImage.Information)
		{
			Window window = GetCurrentForm(control);
			if (window == null)
			{
				return MessageBox.Show(text, caption, buttons, icon);
			}
			return MessageBox.Show(window, text, caption, buttons, icon);
		}

		public static Window TopForm
		{
			get
			{
				IntPtr active = GetActiveWindow();
				Window window = Application.Current.Windows
				                           .OfType<Window>()
				                           .SingleOrDefault(w => w.IsLoaded && new WindowInteropHelper(w).Handle == active);

				if (window != null)
				{
					return window;
				}

				window = Application.Current.MainWindow;
				if (window?.IsLoaded == true)
				{
					return window;
				}
				return null;
			}
		}

		[DllImport("user32.dll")]
		private static extern IntPtr GetActiveWindow();
	}
}
