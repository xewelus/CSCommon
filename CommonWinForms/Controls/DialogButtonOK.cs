using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CommonWinForms.Controls
{
	public class DialogButtonOK : Button
	{
		public DialogButtonOK()
		{
			base.Image = Properties.Resources.ok;
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image Image
		{
			get
			{
				return base.Image;
			}
			set
			{
				base.Image = value;
			}
		}
	}
}
