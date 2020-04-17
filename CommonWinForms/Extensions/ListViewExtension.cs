using System;
using System.Windows.Forms;
using Common;

namespace CommonWinForms.Extensions
{
	public static class ListViewExtension
	{
		/// <summary>
		/// Выравнивает все колонки по оптимальной ширине, учитывая и содержимое строк и длину хедера.
		/// </summary>
		public static void AdjustColumns(this ListView listView)
		{
			listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}

		public static void AdjustListHeight(this ListView listView)
		{
			if (listView.Items.Count > 0)
			{
				ListViewItem item = listView.Items[listView.Items.Count - 1];
				listView.Height = item.Bounds.Bottom + 10;
			}
		}

		public static void AddCopyFunctionality(this ListView listView)
		{
			ListViewCellsCopier copier = new ListViewCellsCopier(listView);
			copier.Apply();
		}

		/// <summary>
		/// Использовать с конструкцией "using". Вызывает BeginUpdate() а при диспозе EndUpdate().
		/// </summary>
		/// <param name="listView"></param>
		/// <returns></returns>
		public static UpdatingObject Updating(this ListView listView)
		{
			return new UpdatingObject(listView);
		}

		public class UpdatingObject : IDisposable
		{
			private readonly ListView listView;

			public UpdatingObject(ListView listView)
			{
				this.listView = listView;
				listView.BeginUpdate();
			}

			public void Dispose()
			{
				this.listView.EndUpdate();
			}
		}
	}

	public static class ListViewItemExtension
	{
		public static void Update(this ListViewItem item, params object[] subItems)
		{
			while (item.SubItems.Count < subItems.Length) item.SubItems.Add(string.Empty);

			for (int i = 0; i < subItems.Length; i++)
			{
				object o = subItems[i];
				item.SubItems[i].Text = o == null ? string.Empty : o.ToHRString();
			}
		}
	}
}
