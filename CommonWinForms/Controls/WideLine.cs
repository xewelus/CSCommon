﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CommonWinForms.Controls
{
	public partial class WideLine : UserControl
	{
		public WideLine()
		{
			this.InitializeComponent();
		}

		[DefaultValue(typeof(Size), "0, 4")]
		public override Size MinimumSize
		{
			get
			{
				return new Size(0, 4);
			}
			set
			{
			}
		}

		[DefaultValue(typeof(Size), "10000, 4")]
		public override Size MaximumSize
		{
			get
			{
				return new Size(10000, 4);
			}
			set
			{
			}
		}
	}
}
