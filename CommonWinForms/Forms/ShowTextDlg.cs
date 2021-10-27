using System;
using System.Windows.Forms;

namespace CommonWinForms.Forms
{
	public partial class ShowTextDlg : Form
	{
		public ShowTextDlg()
		{
			this.InitializeComponent();
		}

		public static void ShowText(string caption, string text)
		{
			using (ShowTextDlg dlg = new ShowTextDlg())
			{
				dlg.Text = caption;
				dlg.textBox.Text = text;
				dlg.ShowDialog();
			}
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			try
			{
				Clipboard.SetText(this.textBox.Text);
			}
			catch (Exception ex)
			{
				UIHelper.ShowError(ex);
			}
		}

		private void cbWordWrap_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				this.textBox.WordWrap = this.cbWordWrap.Checked;
			}
			catch (Exception ex)
			{
				UIHelper.ShowError(ex);
			}
		}
	}
}
