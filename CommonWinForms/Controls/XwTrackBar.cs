using System;
using System.Windows.Forms;

namespace CommonWinForms.Controls
{
	public class XwTrackBar : TrackBar
	{
		private bool isMouseDown;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.isMouseDown = true;
			this.UpdateValue(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.UpdateValue(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.UpdateValue(e);
			this.isMouseDown = false;
		}

		private void UpdateValue(MouseEventArgs e)
		{
			if (this.isMouseDown)
			{
				int value = Convert.ToInt32((double)e.X / this.Width * (this.Maximum - this.Minimum));
				if (value < this.Minimum)
				{
					value = this.Minimum;
				}
				else if (value > this.Maximum)
				{
					value = this.Maximum;
				}
				this.Value = value;
			}
		}
	}
}
