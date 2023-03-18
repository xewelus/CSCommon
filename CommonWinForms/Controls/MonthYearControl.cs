using System;
using System.Windows.Forms;
using Common.Classes;

namespace CommonWinForms.Controls
{
	public partial class MonthYearControl : UserControl
	{
		public MonthYearControl()
		{
			this.InitializeComponent();
		}

		public MonthYear Value
		{
			get
			{
				return new MonthYear(this.cbMonth.SelectedIndex + 1, (int)this.nbYear.Value);
			}
			set
			{
				this.cbMonth.SelectedIndex = value.Month - 1;
				this.nbYear.Value = value.Year;
			}
		}

		public event EventHandler ValueChanged;

		private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged.Invoke(this, EventArgs.Empty);
			}
		}

		private void nbYear_ValueChanged(object sender, EventArgs e)
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
