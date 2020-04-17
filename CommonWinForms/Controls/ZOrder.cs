using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace CommonWinForms.Controls
{
	/// <summary>
	/// Позволяет решить проблему с позициями контролов с установленным Dock при изменении Visibility.
	/// </summary>
	[ProvideProperty("ZOrder", typeof(Control))]
	public class ZOrder : Component, IExtenderProvider
	{
		private ZOrder()
		{
		}

		public ZOrder(IContainer container) : this()
		{
			if (container != null)
			{
				container.Add(this);
			}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (ControlItem item in this.controls.Values)
				{
					item.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		public bool CanExtend(object extendee)
		{
			return extendee is Panel
				|| extendee is GroupBox
				|| extendee is UserControl
				|| extendee is TabControl
				|| extendee is Form;
		}

		private readonly Dictionary<Control, ControlItem> controls = new Dictionary<Control, ControlItem>();

		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Позволяет решить проблему с позициями контролов с установленным Dock при изменении Visibility.")]
		public bool GetZOrder(Control control)
		{
			if (control == null) throw new ArgumentNullException("control");
			return this.controls.ContainsKey(control);
		}

		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Позволяет решить проблему с позициями контролов с установленным Dock при изменении Visibility.")]
		public void SetZOrder(Control control, bool value)
		{
			if (control == null) throw new ArgumentNullException("control");

			if (value)
			{
				if (!this.controls.ContainsKey(control))
				{
					this.controls[control] = new ControlItem(control);
				}
			}
			else
			{
				ControlItem item;
				if (this.controls.TryGetValue(control, out item))
				{
					this.controls.Remove(control);
					item.Dispose();
				}
			}
		}

		private class ControlItem : IDisposable
		{
			private Control control;
			private readonly List<Control> children = new List<Control>();

			public ControlItem(Control control)
			{
				this.control = control;

				foreach (Control child in control.Controls)
				{
					this.AddChild(child);
				}

				control.ControlAdded += this.ControlOnControlAdded;
				control.ControlRemoved += this.ControlOnControlRemoved;
			}

			private void ControlOnControlAdded(object sender, ControlEventArgs controlEventArgs)
			{
				if (this.control != null)
				{
					this.AddChild(controlEventArgs.Control);
				}
			}

			private void ControlOnControlRemoved(object sender, ControlEventArgs controlEventArgs)
			{
				if (this.control != null)
				{
					this.RemoveChild(controlEventArgs.Control);
				}
			}

			public void Dispose()
			{
				if (this.control != null)
				{
					this.control.ControlAdded -= this.ControlOnControlAdded;
					foreach (Control child in this.children)
					{
						child.VisibleChanged -= this.ChildOnVisibleChanged;
					}
					this.children.Clear();
					this.control = null;
				}
			}

			private void AddChild(Control child)
			{
				if (!this.children.Contains(child))
				{
					this.children.Add(child);
					child.VisibleChanged += this.ChildOnVisibleChanged;
				}
			}

			private void RemoveChild(Control child)
			{
				if (this.children.Contains(child))
				{
					this.children.Remove(child);
					child.VisibleChanged -= this.ChildOnVisibleChanged;
				}
			}

			private void ChildOnVisibleChanged(object sender, EventArgs eventArgs)
			{
				foreach (Control child in this.control.Controls)
				{
					int index = this.children.IndexOf(child);
					if (index != -1)
					{
						this.control.Controls.SetChildIndex(child, index);
					}
				}
			}
		}
	}
}
