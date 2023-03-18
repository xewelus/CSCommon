using System;
using System.ComponentModel;
using System.Drawing;

namespace CommonWinForms.Controls
{
	/// <summary>
	/// Контрол для редактирования цифровых значений
	/// </summary>
	public class NumBox : System.Windows.Forms.TextBox
	{
		#region components

		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Timer timer;

		#endregion

		#region constructor

		public NumBox()
		{
			this.InitializeComponent();
		}

		#endregion

		#region IDisposable

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.timer = new System.Windows.Forms.Timer(this.components);
			// 
			// timer
			// 
			this.timer.Interval = 50;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// NumBox
			// 
			this.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

		}
		#endregion

		#region fields

		private bool _InternalUpdates = false;
		private string _ValidValue = string.Empty;
		private int _SelectionStart = 0;
		private int _SelectionLength = 0;
		private Color _BackColor;
		private bool _Modified = false;
		private int _MaxDecimal = 2;
		private bool _CheckRange = false;
		private decimal _MinValue = 0;
		private decimal _MaxValue = 100; 
		private object innerValue
		{
			get
			{
				string text = this._ValidValue;
				if (text == null || text.Length == 0)
				{
					return null;
				}
				else
				{
					text = this.AdjustNumericSeparator(text);
					return decimal.Parse(text, System.Globalization.NumberFormatInfo.CurrentInfo);
				}
			}
			set
			{
				this._InternalUpdates = true;
				string str = string.Empty;
				if (value != null)
				{
					decimal dec = Convert.ToDecimal(value);
					if (this.CheckRange)
					{
						if (dec > this._MaxValue)
						{
							dec = this._MaxValue;
						}
						if (dec < this._MinValue)
						{
							dec = this._MinValue;
						}
					}
					dec = decimal.Round(dec, this._MaxDecimal);
					str = Convert.ToString(dec);
				}
				this.Text = str;
				this._ValidValue = str;
				this._InternalUpdates = false;
				this.SaveState();
			}
		}

		#endregion

		#region public interface

		public bool IsNull()
		{
			return this.innerValue == null;
		}
		public decimal GetValue()
		{
			return (decimal)this.innerValue;
		}
		public void SetValue(object value)
		{
			this.innerValue = value;
		}

		[Browsable(true)]
		[DefaultValue(2)]
		public int MaxDecimal
		{
			get
			{
				return this._MaxDecimal;
			}
			set
			{
				this._MaxDecimal = value;
			}
		}
		[Browsable(true)]
		[DefaultValue(false)]
		public bool CheckRange
		{
			get
			{
				return this._CheckRange;
			}
			set
			{
				this._CheckRange = value;
			}
		}
		[Browsable(true)]
		[DefaultValue(0)]
		public decimal MinValue
		{
			get
			{
				return this._MinValue;
			}
			set
			{
				this._MinValue = value;
			}
		}
		[Browsable(true)]
		[DefaultValue(100)]
		public decimal MaxValue
		{
			get
			{
				return this._MaxValue;
			}
			set
			{
				this._MaxValue = value;
			}
		}

		#endregion

		#region internal members

		private void ShowAlert()
		{
			if (!this.timer.Enabled)
			{
				this._BackColor = this.BackColor;
				this.BackColor = Color.Red;
				this.timer.Start();
			}
		}

		private void SaveState()
		{
			this._ValidValue = this.Text;
			this._SelectionStart = this.SelectionStart;
			this._SelectionLength = this.SelectionLength;
			this._Modified = this.Modified;
		}

		private void RestoreState()
		{
			this._InternalUpdates = true;
			this.Text = this._ValidValue;
			if (this._SelectionStart >= 0
				&&
				this._SelectionLength >= 0)
			{
				this.SelectionStart = this._SelectionStart;
				this.SelectionLength = this._SelectionLength;
			}
			this._InternalUpdates = false;
			this.Modified = this._Modified;
		}

		private string AdjustNumericSeparator(string s)
		{
			string sep = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
			s = s.Replace(",", sep);
			s = s.Replace(".", sep);
			return s;
		}

		#endregion

		#region overrides
		protected override void OnTextChanged(EventArgs e)
		{

			if (this._InternalUpdates)
			{
				return;
			}
			bool validValue = false;
			try
			{
				string text = this.Text;
				if (text == null || text.Length == 0)
				{
					validValue = true;
				}
				else if (text.EndsWith("-"))
				{
					validValue = false;
				}
				else
				{
					if (text.EndsWith(".") || text.EndsWith(","))
					{
						text += "0";
					}
					text = this.AdjustNumericSeparator(text);
					decimal val = decimal.Parse(text, System.Globalization.NumberFormatInfo.CurrentInfo);
					if (this._CheckRange && (val < this._MinValue || val > this._MaxValue))
					{
						validValue = false;
					}
					else if (val == decimal.Round(val, this._MaxDecimal))
					{
						validValue = true;
					}
				}
			}
			catch (FormatException)
			{
			}
			if (validValue)
			{
				this.Modified = true;
				this.SaveState();
			}
			else
			{
				this.RestoreState();
				this.ShowAlert();
			}
			base.OnTextChanged(e);
		}

		public override void Refresh()
		{
			this.TabStop = !this.ReadOnly;
			base.Refresh ();
		}


		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged (e);
			if (this.Enabled)
			{
				this.BackColor = this._BackColor;
			}
			else
			{
				this.BackColor = DefaultBackColor;
			}
		}

		private void timer_Tick(object sender, System.EventArgs e)
		{
			this.timer.Stop();
			if (this.Enabled)
			{
				this.BackColor = this._BackColor;
			}
			else
			{
				this.BackColor = DefaultBackColor;
			}
		}

		#endregion
	}
}
