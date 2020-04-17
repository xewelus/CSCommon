using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CommonWinForms.Controls
{
	/// <summary>
	/// Advanced date box with text edit and drop down calendar.
	/// </summary>
	public class DateBox : UserControl
	{
		#region wincontrols

		private System.Windows.Forms.Panel pnlBG;
		private System.Windows.Forms.Panel pnlText;
		private System.Windows.Forms.TextBox tbText;
		private SmartButton btnDropDown;
		private System.Windows.Forms.ErrorProvider epMain;
		private System.ComponentModel.Container components = null;

		#endregion

		#region contructors

		public DateBox()
		{
			this.InitializeComponent();

			this.btnDropDown.Text = "\x80";
			this.CalendarForm.mcDate.DateSelected += new DateRangeEventHandler(this.OnDateSelected);
		}

		#endregion

		#region IDisposable

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
				if (this.CalendarForm != null)
				{
					this.CalendarForm.mcDate.DateSelected -= new DateRangeEventHandler(this.OnDateSelected);
					this.CalendarForm.Dispose();
					this.CalendarForm = null;
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
			this.pnlBG = new System.Windows.Forms.Panel();
			this.btnDropDown = new SmartButton();
			this.pnlText = new System.Windows.Forms.Panel();
			this.tbText = new System.Windows.Forms.TextBox();
			this.epMain = new System.Windows.Forms.ErrorProvider();
			this.pnlBG.SuspendLayout();
			this.pnlText.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlBG
			// 
			this.pnlBG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlBG.Controls.Add(this.btnDropDown);
			this.pnlBG.Controls.Add(this.pnlText);
			this.pnlBG.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBG.Location = new System.Drawing.Point(0, 0);
			this.pnlBG.Name = "pnlBG";
			this.pnlBG.Size = new System.Drawing.Size(160, 20);
			this.pnlBG.TabIndex = 0;
			// 
			// btnDropDown
			// 
			this.btnDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.btnDropDown.Font = new System.Drawing.Font("Wingdings 3", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(2)));
			this.btnDropDown.Location = new System.Drawing.Point(140, 0);
			this.btnDropDown.Name = "btnDropDown";
			this.btnDropDown.Selectable = false;
			this.btnDropDown.Size = new System.Drawing.Size(16, 16);
			this.btnDropDown.TabIndex = 1;
			this.btnDropDown.Click += new System.EventHandler(this.btnDropDown_Click);
			// 
			// pnlText
			// 
			this.pnlText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlText.BackColor = System.Drawing.SystemColors.Window;
			this.pnlText.Controls.Add(this.tbText);
			this.pnlText.Location = new System.Drawing.Point(0, 0);
			this.pnlText.Name = "pnlText";
			this.pnlText.Size = new System.Drawing.Size(140, 20);
			this.pnlText.TabIndex = 0;
			// 
			// tbText
			// 
			this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbText.AutoSize = false;
			this.tbText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.epMain.SetIconPadding(this.tbText, -16);
			this.tbText.Location = new System.Drawing.Point(0, 0);
			this.tbText.MaxLength = 20;
			this.tbText.Name = "tbText";
			this.tbText.Size = new System.Drawing.Size(140, 20);
			this.tbText.TabIndex = 0;
			this.tbText.Text = "";
			this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
			this.tbText.Leave += new System.EventHandler(this.tbText_Leave);
			this.tbText.Enter += new System.EventHandler(this.tbText_Enter);
			// 
			// epMain
			// 
			this.epMain.ContainerControl = this;
			// 
			// DateBox
			// 
			this.Controls.Add(this.pnlBG);
			this.Name = "DateBox";
			this.Size = new System.Drawing.Size(160, 20);
			this.pnlBG.ResumeLayout(false);
			this.pnlText.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region fields

		/// <summary>
		/// Формат даты по умолчанию.
		/// </summary>
		public const string DefaultDateFormat = "dd.MM.yyyy";


		/// <summary>
		/// Всплывающая форма календаря.
		/// </summary>
		private DbcDateBox3Form CalendarForm = new DbcDateBox3Form();
		/// <summary>
		/// Внутренние изменения.
		/// </summary>
		private bool InternalUpdates = false;
		/// <summary>
		/// Введенный текст.
		/// </summary>
		private string EditingText = string.Empty;
		/// <summary>
		/// Введенный текст корректен.
		/// </summary>
		private bool LastValueIsValid = true;
		/// <summary>
		/// Автовставка разделителей.
		/// </summary>
		public bool AutoInfill = true;

		/// <summary>
		/// Событие изменения значения.
		/// </summary>
		public event EventHandler OnValueChanged;

		/// <summary>
		/// Формат даты.
		/// </summary>
		private string mDateFormat = DefaultDateFormat;
		/// <summary>
		/// Формат даты.
		/// </summary>
		[DefaultValue(DefaultDateFormat)]
		[Description("Формат даты.")]
		public string DateFormat
		{
			get
			{
				return this.mDateFormat;
			}
			set
			{
				this.mDateFormat = value;
			}
		}

		/// <summary>
		/// Нередактируемый.
		/// </summary>
		private bool mReadOnly = false;
		/// <summary>
		/// Нередактируемый.
		/// </summary>
		[DefaultValue(false)]
		[Description("Нередактируемый.")]
		public bool ReadOnly
		{
			get
			{
				return this.mReadOnly;
			}
			set
			{
				this.mReadOnly = value;
				this.Refresh();
			}
		}

		/// <summary>
		/// Значение не задано.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsNull
		{
			get
			{
				return this.mValue == null;
			}
		}

		/// <summary>
		/// Значение.
		/// </summary>
		private DateTime? mValue;
		/// <summary>
		/// Значение.
		/// </summary>
		[Description("Значение.")]
		public DateTime? Value
		{
			get
			{
				return this.mValue;
			}
			set
			{
				this.InternalSetValue(value);
				this.UpdateEditingText();
				this.UpdateText();
			}
		}

		public DateTime Date
		{
			get
			{
				if (this.Value == null) return DateTime.MinValue;
				return this.Value.Value.Date;
			}
		}

		#endregion

		#region overrides

		public override void Refresh()
		{
			this.btnDropDown.Enabled = !this.ReadOnly;
			this.tbText.ReadOnly = this.ReadOnly;
			this.pnlText.BackColor = this.tbText.BackColor;

			base.Refresh ();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (this.DesignMode)
			{
				return;
			}
			this.btnDropDown.Left += this.btnDropDown.Width - this.btnDropDown.Height;
			this.btnDropDown.Width = this.btnDropDown.Height;
			this.pnlText.Width = this.pnlBG.ClientSize.Width - this.btnDropDown.Width;
			this.tbText.Top = (this.pnlText.Height - this.tbText.Height) / 2;
		}

		#endregion

		#region event handlers

		private void btnDropDown_Click(object sender, System.EventArgs e)
		{
			if (this.DesignMode)
			{
				return;
			}

			this.CalendarForm.Location = this.Parent.PointToScreen(new Point(this.Left, this.Bottom));
			this.CalendarForm.Show();
		}

		private void InternalSetValue(DateTime? date)
		{
			this.mValue = date;

			if (this.OnValueChanged != null)
			{
				this.OnValueChanged(this, EventArgs.Empty);
			}
		}

		private void tbText_TextChanged(object sender, System.EventArgs e)
		{
			if (this.DesignMode)
			{
				return;
			}
			if (this.InternalUpdates)
			{
				return;
			}

			this.LastValueIsValid = true;
			if (this.tbText.TextLength == 0)
			{
				this.InternalSetValue(null);
			}
			else
			{
				try
				{
					if (this.AutoInfill && this.tbText.SelectionStart == this.tbText.Text.Length)
					{
						this.InternalUpdates = true;
						//xxx -> xx.x
						if (Regex.Match(this.tbText.Text, @"^[^.-/]{3}$").Success)
						{
							this.tbText.Text = this.tbText.Text.Insert(2, DateTimeFormatInfo.CurrentInfo.DateSeparator);
						}
						//xx.xxx -> xx.xx.x
						if (Regex.Match(this.tbText.Text, @"^[^.-/]{2}[.-/][^.-/]{3}$").Success)
						{
							this.tbText.Text = this.tbText.Text.Insert(5, DateTimeFormatInfo.CurrentInfo.DateSeparator);
						}
						this.tbText.SelectionStart = this.tbText.Text.Length;
						this.InternalUpdates = false;
					}
					this.InternalSetValue(DateTime.Parse(this.tbText.Text));
				}
				catch (Exception)
				{
					this.LastValueIsValid = false;
				}
			}
		}

		private void tbText_Leave(object sender, System.EventArgs e)
		{
			if (this.DesignMode)
			{
				return;
			}
			this.EditingText = this.tbText.Text;
			this.UpdateText();
			string err = this.LastValueIsValid ? string.Empty : "Введено некорректное значение.";
			this.epMain.SetError(this.tbText, err);
		}

		private void tbText_Enter(object sender, System.EventArgs e)
		{
			if (this.DesignMode)
			{
				return;
			}
			this.InternalUpdates = true;
			this.tbText.Text = this.EditingText;
			this.InternalUpdates = false;
		}

		private void OnDateSelected(object sender, DateRangeEventArgs e)
		{
			if (this.DesignMode)
			{
				return;
			}
			this.CalendarForm.Hide();
			this.Value = this.CalendarForm.mcDate.SelectionStart.Date;
			this.LastValueIsValid = true;
			this.epMain.SetError(this.tbText, string.Empty);
			if (this.tbText.Focused)
			{
				this.InternalUpdates = true;
				this.tbText.Text = this.EditingText;
				this.InternalUpdates = false;
			}
		}

		#endregion

		

		private string GetDateString()
		{
			if (this.Value == null)
			{
				return string.Empty;
			}
			else
			{
				return this.Value.Value.ToString(this.DateFormat);
			}
		}

		private void UpdateText()
		{
			this.InternalUpdates = true;
			this.tbText.Text = this.GetDateString();
			this.InternalUpdates = false;
		}

		private void UpdateEditingText()
		{
			this.EditingText = this.GetDateString();
		}
	}
}
