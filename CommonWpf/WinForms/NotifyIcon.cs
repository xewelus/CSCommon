using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Forms;
using CommonWpf.Classes.UI;
using Image = System.Drawing.Image;

namespace CommonWpf.WinForms
{
	public class NotifyIcon : IDisposable
	{
		public NotifyIcon()
		{
			this.components = new Container();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.cmSysTray = new ContextMenuStrip(this.components);
			this.notifyIcon.ContextMenuStrip = this.cmSysTray;
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseClick += this.notifyIcon_MouseClick;
			this.notifyIcon.MouseDoubleClick += this.notifyIcon_MouseDoubleClick;
			this.cmSysTray.Opening += this.cmSysTray_Opening;
		}

		public NotifyIcon(string title, Icon icon) : this()
		{
			this.notifyIcon.Icon = icon;
			this.notifyIcon.Text = title;
		}

		public event MouseButtonEventHandler Click;
		public event MouseButtonEventHandler DoubleClick;
		public event CancelEventHandler Opening;

		private readonly IContainer components;
		private readonly System.Windows.Forms.NotifyIcon notifyIcon;
		private readonly ContextMenuStrip cmSysTray;
		

		private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				if (this.Click == null) return;

				MouseButton? mouseButton = GetMouseButton(e);
				if (mouseButton == null) return;

				MouseButtonEventArgs args = new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, 0, mouseButton.Value);
				this.Click.Invoke(this, args);
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}

		private void notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			try
			{
				if (this.DoubleClick == null) return;

				MouseButton? mouseButton = GetMouseButton(e);
				if (mouseButton == null) return;

				MouseButtonEventArgs args = new MouseButtonEventArgs(InputManager.Current.PrimaryMouseDevice, 0, mouseButton.Value);
				this.DoubleClick.Invoke(this, args);
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}

		private static MouseButton? GetMouseButton(System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				return MouseButton.Left;
			}
			if (e.Button == MouseButtons.Right)
			{
				return MouseButton.Right;
			}
			if (e.Button == MouseButtons.Middle)
			{
				return MouseButton.Middle;
			}
			return null;
		}

		private void cmSysTray_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				this.Opening?.Invoke(this, e);
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}

		public Icon Icon
		{
			get
			{
				return this.notifyIcon.Icon;
			}
			set
			{
				this.notifyIcon.Icon = value;
			}
		}

		public string Text
		{
			get
			{
				return this.notifyIcon.Text;
			}
			set
			{
				this.notifyIcon.Text = value;
			}
		}

		public void Dispose()
		{
			this.components?.Dispose();
		}

		public Item AddItem(string text, Image image = null, EventHandler handler = null)
		{
			Item item = new Item(text, image);
			item.realItem.Click += handler;
			this.cmSysTray.Items.Add(item.realItem);
			return item;
		}

		public Item AddSeparator()
		{
			Item item = new Item();
			this.cmSysTray.Items.Add(item.realItem);
			return item;
		}

		public class Item
		{
			protected internal readonly ToolStripItem realItem;

			protected internal Item()
			{
				this.realItem = new ToolStripSeparator();
			}

			protected internal Item(string text, Image image)
			{
				this.realItem = new ToolStripMenuItem();
				this.realItem.Text = text;
				if (image != null)
				{
					this.realItem.Image = image;
				}
			}

			public bool Enabled
			{
				get
				{
					return this.realItem.Enabled;
				}
				set
				{
					this.realItem.Enabled = value;
				}
			}

			public bool Checked
			{
				get
				{
					ToolStripMenuItem item = this.realItem as ToolStripMenuItem;
					return item?.Checked == true;
				}
				set
				{
					ToolStripMenuItem item = (ToolStripMenuItem)this.realItem;
					item.Checked = value;
				}
			}
		}
	}
}
