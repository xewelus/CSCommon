using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace CommonWinForms.Components
{
	/// <summary>
	/// Уменьшение ширины элемента при уменьшении ширины родительского контейнера.
	/// </summary>
	[ProvideProperty("AdaptiveWidth", typeof(Control))]
	public class AdaptiveWidth : Component, IExtenderProvider
	{
		private AdaptiveWidth()
		{
		}

		public AdaptiveWidth(IContainer container) : this()
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
			return extendee is Control;
		}

		private readonly Dictionary<Control, ControlItem> controls = new Dictionary<Control, ControlItem>();

		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Уменьшение ширины элемента при уменьшении ширины родительского контейнера (работает совместно с MaximumSize или Size).")]
		public bool GetAdaptiveWidth(Control control)
		{
			if (control == null) throw new ArgumentNullException("control");
			return this.controls.ContainsKey(control);
		}

		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Уменьшение ширины элемента при уменьшении ширины родительского контейнера (работает совместно с MaximumSize или Size).")]
		public void SetAdaptiveWidth(Control control, bool value)
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
			private int width;
			private Control prevParent;
			private bool internalChanges;

			public ControlItem(Control control)
			{
				this.control = control;

				control.SizeChanged += this.ControlOnSizeChanged;
				this.ControlOnSizeChanged(null, null);

				control.ParentChanged += this.ControlOnParentChanged;
				this.ControlOnParentChanged(null, null);

				control.MarginChanged += this.CalcHandler;
				this.CalcHandler(null, null);
			}

			private void ControlOnSizeChanged(object sender, EventArgs e)
			{
				if (!this.internalChanges)
				{
					this.width = this.control.Width;
				}
			}

			private void ControlOnParentChanged(object sender, EventArgs e)
			{
				Control parent = this.control.Parent;

				if (parent == this.prevParent) return;

				this.UnsubscribeParent();

				if (parent != null)
				{
					parent.SizeChanged += this.CalcHandler;
					parent.PaddingChanged += this.CalcHandler;
					this.prevParent = parent;

					this.CalcHandler(null, null);
				}
			}

			private void UnsubscribeParent()
			{
				if (this.prevParent != null)
				{
					this.prevParent.SizeChanged -= this.CalcHandler;
					this.prevParent.PaddingChanged -= this.CalcHandler;
					this.prevParent = null;
				}
			}

			private void CalcHandler(object sender, EventArgs e)
			{
				if (this.prevParent == null) return;

				int maxWidth = this.control.MaximumSize.Width > 0 ? this.control.MaximumSize.Width : this.width;

				int w = this.prevParent.Width - this.control.Left - this.prevParent.Padding.Right - this.control.Margin.Right;
				if (w > maxWidth) w = maxWidth;
				if (w < 0) w = 0;

				if (this.control.Width != w)
				{
					try
					{
						this.internalChanges = true;
						this.control.Width = w;
					}
					finally
					{
						this.internalChanges = false;
					}
				}
			}

			public void Dispose()
			{
				this.UnsubscribeParent();

				if (this.control != null)
				{
					this.control.ParentChanged -= this.ControlOnParentChanged;
					this.control.MarginChanged -= this.CalcHandler;
					this.control.SizeChanged -= this.ControlOnSizeChanged;
					this.control = null;
				}
			}
		}
	}
}
