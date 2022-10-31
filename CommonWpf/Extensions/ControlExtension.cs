using System;
using System.Windows.Controls;
using CommonWpf;

namespace CommonWinForms.Extensions
{
	public static class ControlExtension
	{
		public static void Remove(this Control control)
		{
			if (control.Parent is Panel parent)
			{
				parent.Children.Remove(control);
			}
			else
			{
				throw new NotSupportedException(control.Parent?.GetType().FullName ?? "<null>");
			}
		}

		// ReSharper disable once UnusedParameter.Global
		public static bool InRuntime(this Control control)
		{
			return UIHelper.InRuntime;
		}
	}
}
