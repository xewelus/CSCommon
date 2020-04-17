using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common
{
	public static class ControlExtension
	{
		public static Form GetCurrentForm(this Control control)
		{
			return UIHelper.GetCurrentForm(control);
		}

		public static void Remove(this Control control, bool dispose = false)
		{
			if (control.Parent != null)
			{
				control.Parent.Controls.Remove(control);
			}

			if (dispose && !control.IsDisposed)
			{
				control.Dispose();
			}
		}

		public static int GetPreferredHeight(this Control control)
		{
			return control.GetPreferredSize(new Size(Int32.MaxValue, Int32.MaxValue)).Height;
		}

		public static void SetPreferredHeight(this Control control)
		{
			Size size = control.GetPreferredSize(new Size(Int32.MaxValue, Int32.MaxValue));
			control.Height = size.Height;
		}

		public static void AdjustHeight(this Control control, Control to = null, int offset = 3)
		{
			if (to == null)
			{
				to = FindBottomControl(control);
			}

			control.Height = (to == null ? 0 : to.Bottom) + offset;
		}

		public static void AdjustTop(this Control control, Control to = null, int offset = 3)
		{
			if (to == null)
			{
				to = FindBottomControl(control.Parent, control);
			}

			control.Top = (to == null ? 0 : to.Bottom) + offset;
		}

		private static Control FindBottomControl(Control parent, params Control[] except)
		{
			Control result = null;
			foreach (Control c in parent.Controls)
			{
				if (c.In(except)) continue;

				if (result == null || c.Bottom > result.Bottom)
				{
					result = c;
				}
			}
			return result;
		}

		/// <summary>
		/// Использовать с конструкцией "using". Вызывает BeginUpdate() а при диспозе EndUpdate().
		/// </summary>
		/// <param name="listView"></param>
		/// <returns></returns>
		public static SuspendingObject Suspending(this Control control)
		{
			return new SuspendingObject(control);
		}

		public class SuspendingObject : IDisposable
		{
			private readonly Control control;

			public SuspendingObject(Control control)
			{
				this.control = control;
				control.SuspendLayout();
			}

			public void Dispose()
			{
				this.control.ResumeLayout(false);
			}
		}
	}

	public static class ControlCollectionExtension
	{
		public static T AddControl<T>(this Control parent, T control, DockStyle dockStyle = DockStyle.Fill) where T : Control
		{
			control.Dock = dockStyle;
			parent.Controls.Add(control);
			return control;
		}
	}

	public static class TabControlExtension
	{
		public static void SetTabState(this TabControl tabControl, TabPage tabPage, bool visible)
		{
			if (visible)
			{
				if (!tabControl.TabPages.Contains(tabPage))
				{
					tabControl.TabPages.Add(tabPage);
				}
			}
			else
			{
				if (tabControl.TabPages.Contains(tabPage))
				{
					tabControl.TabPages.Remove(tabPage);
				}
			}
		}
	}
}
