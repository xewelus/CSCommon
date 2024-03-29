﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonWinForms.Extensions
{
	public class ListViewCellsCopier
	{
		private bool applied;
		private readonly ListView listView;
		private readonly ContextMenuStrip menu;
		private readonly ToolStripItem copyCellItem = new ToolStripMenuItem("Копировать значение ячейки");
		private readonly ToolStripItem copyRowsItem = new ToolStripMenuItem("Копировать выделенные строки");
		private readonly ToolStripItem copyAllItem = new ToolStripMenuItem("Копировать все строки");
		private string cursorText;

		public ListViewCellsCopier(ListView listView, ContextMenuStrip menu = null)
		{
			this.listView = listView;
			this.menu = menu ?? listView.ContextMenuStrip ?? new ContextMenuStrip();
		}

		public void Apply(ToolStripItem after = null)
		{
			if (this.applied) return;

			this.menu.Opening += this.MenuOnOpening;

			int index = -1;
			if (after != null)
			{
				index = this.menu.Items.IndexOf(after);
			}
			if (index == -1)
			{
				index = this.menu.Items.Count;
			}

			this.copyCellItem.Click += this.CopyCellItemOnClick;
			this.menu.Items.Insert(index++, this.copyCellItem);

			this.copyRowsItem.Click += this.CopyRowsItemOnClick;
			this.menu.Items.Insert(index++, this.copyRowsItem);

			this.copyAllItem.Click += this.CopyAllItemOnClick;
			this.menu.Items.Insert(index, this.copyAllItem);

			if (this.listView.ContextMenuStrip != this.menu)
			{
				this.listView.ContextMenuStrip = this.menu;
			}

			this.applied = true;
		}

		private void MenuOnOpening(object sender, CancelEventArgs cancelEventArgs)
		{
			try
			{
				this.copyRowsItem.Enabled = this.listView.SelectedItems.Count > 0;

				Point point = this.listView.PointToClient(Cursor.Position);
				ListViewItem item = this.listView.GetItemAt(point.X, point.Y);
				if (item != null)
				{
					ListViewItem.ListViewSubItem subItem = item.GetSubItemAt(point.X, point.Y);
					if (subItem != null)
					{
						this.cursorText = subItem.Text;
					}
					else
					{
						this.cursorText = item.Text;
					}
				}
				this.copyCellItem.Enabled = item != null;

				this.copyAllItem.Enabled = this.listView.Items.Count > 0;
			}
			catch (Exception ex)
			{
				ExcHandler.Catch(ex);
			}
		}

		private void CopyAllItemOnClick(object sender, EventArgs eventArgs)
		{
			try
			{
				List<ListViewItem> list = new List<ListViewItem>();
				foreach (ListViewItem item in this.listView.Items)
				{
					list.Add(item);
				}
				this.CopyItems(list);
			}
			catch (Exception ex)
			{
				ExcHandler.Catch(ex);
			}
		}

		private void CopyRowsItemOnClick(object sender, EventArgs eventArgs)
		{
			try
			{
				List<ListViewItem> list = new List<ListViewItem>();
				foreach (ListViewItem item in this.listView.SelectedItems)
				{
					list.Add(item);
				}
				this.CopyItems(list);
			}
			catch (Exception ex)
			{
				ExcHandler.Catch(ex);
			}
		}

		private void CopyCellItemOnClick(object sender, EventArgs eventArgs)
		{
			try
			{
				if (string.IsNullOrEmpty(this.cursorText))
				{
					Clipboard.Clear();
				}
				else
				{
					Clipboard.SetText(this.cursorText);
				}
			}
			catch (Exception ex)
			{
				ExcHandler.Catch(ex);
			}
		}

		private void CopyItems(IEnumerable<ListViewItem> items)
		{
			StringBuilder sb = new StringBuilder();
			foreach (ListViewItem item in items)
			{
				if (sb.Length > 0)
				{
					sb.AppendLine();
				}
				sb.Append(item.Text);

				foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
				{
					sb.Append('\t');
					sb.Append(subItem.Text);
				}
			}
			Clipboard.SetText(sb.ToString());
		}
	}
}
