using System;
using System.Windows;
using CommonWpf.Classes.UI;

namespace CommonWpf.Forms
{
	/// <summary>
	/// Interaction logic for ShowTextDlg.xaml
	/// </summary>
	public partial class ShowTextDlg : Window
	{
		public ShowTextDlg()
		{
			this.InitializeComponent();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			IconHelper.RemoveIcon(this);
		}

		public static void ShowText(string caption, string text)
		{
			ShowTextDlg dlg = new ShowTextDlg();
			dlg.Owner = UIHelper.TopForm;
			dlg.Title = caption;
			dlg.run.Text = text;
			dlg.ShowDialog();
		}
	}
}
