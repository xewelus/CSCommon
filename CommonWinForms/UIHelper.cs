using System;
using System.Threading;
using System.Windows.Forms;

namespace CommonWinForms
{
	public static class UIHelper
	{
		public static Form MainForm;

		public static void ShowError(Control control, string text, string caption = "Ошибка")
		{
			ShowMessageBox(GetCurrentForm(control), text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void ShowWarning(Control control, string text, string caption = "Внимание")
		{
			ShowMessageBox(GetCurrentForm(control), text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		public static void ShowMessage(Control control, string text, string caption = "Внимание")
		{
			ShowMessageBox(GetCurrentForm(control), text, caption);
		}

		public static bool AskYesNo(Control control, string text, string caption = "Внимание", MessageBoxIcon icon = MessageBoxIcon.Question)
		{
			return ShowMessageBox(GetCurrentForm(control), text, caption, MessageBoxButtons.YesNo, icon) == DialogResult.Yes;
		}

		public static void ShowError(string text, string caption = "Ошибка")
		{
			ShowError(null, text, caption);
		}

		public static void ShowError(Exception exception, string caption = "Ошибка")
		{
			ShowError(null, exception.ToString(), caption);
		}

		public static void ShowWarning(string text, string caption = "Внимание")
		{
			ShowWarning(null, text, caption);
		}

		public static void ShowMessage(string text, string caption = "Внимание")
		{
			ShowMessage(null, text, caption);
		}

		public static bool AskYesNo(string text, string caption = "Внимание", MessageBoxIcon icon = MessageBoxIcon.Question)
		{
			return AskYesNo(null, text, caption, icon);
		}

		public static DialogResult ShowMessageBox(
			string text,
			string caption = "Внимание",
			MessageBoxButtons buttons = MessageBoxButtons.OK,
			MessageBoxIcon icon = MessageBoxIcon.Information)
		{
			return ShowMessageBox(null, text, caption, buttons, icon);
		}

		public static Form GetCurrentForm(this Control control)
		{
			Control c = control;
			while (c != null && c.Parent != null)
			{
				c = c.Parent;
			}

			if (c is Form)
			{
				return (Form)c;
			}

			return UIHelper.TopForm;
		}

		public static DialogResult ShowMessageBox(
			this Control control,
			string text,
			string caption = "Внимание",
			MessageBoxButtons buttons = MessageBoxButtons.OK,
			MessageBoxIcon icon = MessageBoxIcon.Information)
		{
			return MessageBox.Show(GetCurrentForm(control), text, caption, buttons, icon);
		}

		public static Form TopForm
		{
			get
			{
				return Form.ActiveForm ?? MainForm;
			}
		}

		public static void SetUnhandledExceptionSafe()
		{
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.ThreadException += ApplicationOnThreadException;
		}

		private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs args)
		{
			ExcHandler.Catch(args.Exception);
		}
	}
}
