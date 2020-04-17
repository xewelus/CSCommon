using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CommonWinForms.Controls
{
	public partial class Line : UserControl
	{
		public Line()
		{
			this.InitializeComponent();
		}

		[DefaultValue(typeof(Size), "0, 2")]
		public override Size MinimumSize
		{
			get
			{
				return new Size(0, 2);
			}
			set
			{
			}
		}

		[DefaultValue(typeof(Size), "10000, 2")]
		public override Size MaximumSize
		{
			get
			{
				return new Size(10000, 2);
			}
			set
			{
			}
		}
	}
}
