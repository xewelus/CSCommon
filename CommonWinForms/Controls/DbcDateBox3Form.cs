using System;
using System.Drawing;
using System.Windows.Forms;

namespace CommonWinForms.Controls
{
	/// <summary>
	/// Drop-down form for DbcDateBox3
	/// </summary>
	public class DbcDateBox3Form : System.Windows.Forms.Form
	{
		public System.Windows.Forms.MonthCalendar mcDate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DbcDateBox3Form()
		{
			//
			// Required for Windows Form Designer support
			//
			this.InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mcDate = new System.Windows.Forms.MonthCalendar();
			this.SuspendLayout();
			// 
			// mcDate
			// 
			this.mcDate.Location = new System.Drawing.Point(0, 0);
			this.mcDate.MaxSelectionCount = 1;
			this.mcDate.Name = "mcDate";
			this.mcDate.TabIndex = 0;
			// 
			// DbcDateBox3Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(190, 154);
			this.ControlBox = false;
			this.Controls.Add(this.mcDate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DbcDateBox3Form";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.ResumeLayout(false);

		}
		#endregion

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (this.DesignMode)
			{
				return;
			}
			this.ClientSize = this.mcDate.Size;
			Point pt = this.Location;
			pt.Offset(this.Width, this.Height);
			Rectangle workingArea = Screen.GetWorkingArea(pt);
			int dx = this.Right - workingArea.Right;
			int dy = this.Bottom - workingArea.Bottom;
			if (dx > 0 || dy > 0)
			{
				this.Location = new Point(this.Left - (dx > 0 ? dx : 0), this.Top - (dy > 0 ? dy : 0));
			}
		}


		protected override void OnDeactivate(EventArgs e)
		{
			if (this.DesignMode)
			{
				return;
			}
			base.OnDeactivate(e);
			this.Hide();
		}
	}
}
