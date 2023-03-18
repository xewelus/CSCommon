using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CommonWinForms.Controls
{
	public partial class FolderBox : UserControl
	{
		public FolderBox()
		{
			this.InitializeComponent();
		}

		[Browsable(true)]
		public new event EventHandler TextChanged;

		[Browsable(true)]
		[DefaultValue("")]
		public new string Text
		{
			get
			{
				return this.tbPath.Text;
			}
			set
			{
				this.tbPath.Text = value;
			}
		}

		[Browsable(false)]
		public int TextLength
		{
			get
			{
				return this.tbPath.TextLength;
			}
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			try
			{
				using (FolderBrowserDialog dlg = new FolderBrowserDialog())
				{
					dlg.SelectedPath = this.tbPath.Text;
					if (dlg.ShowDialog(this) == DialogResult.OK)
					{
						this.tbPath.Text = dlg.SelectedPath;
					}
				}
			}
			catch (Exception ex)
			{
				UIHelper.ShowError(ex);
			}
		}

		private void tbPath_TextChanged(object sender, EventArgs e)
		{
			try
			{
				this.TextChanged?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				UIHelper.ShowError(ex);
			}
		}
	}
}
