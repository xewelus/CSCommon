namespace CommonWinForms.Controls
{
	partial class MonthYearControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cbMonth = new System.Windows.Forms.ComboBox();
			this.nbYear = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.nbYear)).BeginInit();
			this.SuspendLayout();
			// 
			// cbMonth
			// 
			this.cbMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMonth.FormattingEnabled = true;
			this.cbMonth.Items.AddRange(new object[] {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябр",
            "Декабрь"});
			this.cbMonth.Location = new System.Drawing.Point(0, 0);
			this.cbMonth.MaxDropDownItems = 13;
			this.cbMonth.Name = "cbMonth";
			this.cbMonth.Size = new System.Drawing.Size(76, 21);
			this.cbMonth.TabIndex = 56;
			this.cbMonth.SelectedIndexChanged += new System.EventHandler(this.cbMonth_SelectedIndexChanged);
			// 
			// nbYear
			// 
			this.nbYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nbYear.Location = new System.Drawing.Point(80, 0);
			this.nbYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.nbYear.Minimum = new decimal(new int[] {
            1980,
            0,
            0,
            0});
			this.nbYear.Name = "nbYear";
			this.nbYear.Size = new System.Drawing.Size(50, 20);
			this.nbYear.TabIndex = 57;
			this.nbYear.Value = new decimal(new int[] {
            1980,
            0,
            0,
            0});
			this.nbYear.ValueChanged += new System.EventHandler(this.nbYear_ValueChanged);
			// 
			// MonthYearControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.nbYear);
			this.Controls.Add(this.cbMonth);
			this.Name = "MonthYearControl";
			this.Size = new System.Drawing.Size(133, 23);
			((System.ComponentModel.ISupportInitialize)(this.nbYear)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cbMonth;
		private System.Windows.Forms.NumericUpDown nbYear;
	}
}
