namespace CommonWinForms.Forms
{
	partial class ShowTextDlg
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.textBox = new System.Windows.Forms.TextBox();
			this.wideLine1 = new CommonWinForms.Controls.WideLine();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.cbWordWrap = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox
			// 
			this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox.BackColor = System.Drawing.Color.White;
			this.textBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox.Location = new System.Drawing.Point(7, 30);
			this.textBox.MaxLength = 1000000;
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox.Size = new System.Drawing.Size(1170, 681);
			this.textBox.TabIndex = 35;
			// 
			// wideLine1
			// 
			this.wideLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.wideLine1.Location = new System.Drawing.Point(7, 714);
			this.wideLine1.Margin = new System.Windows.Forms.Padding(0);
			this.wideLine1.Name = "wideLine1";
			this.wideLine1.Size = new System.Drawing.Size(1170, 4);
			this.wideLine1.TabIndex = 32;
			// 
			// cbWordWrap
			// 
			this.cbWordWrap.Appearance = System.Windows.Forms.Appearance.Button;
			this.cbWordWrap.AutoSize = true;
			this.cbWordWrap.Checked = true;
			this.cbWordWrap.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbWordWrap.Image = global::CommonWinForms.Properties.Resources.style_go;
			this.cbWordWrap.Location = new System.Drawing.Point(6, 4);
			this.cbWordWrap.Name = "cbWordWrap";
			this.cbWordWrap.Size = new System.Drawing.Size(22, 22);
			this.cbWordWrap.TabIndex = 37;
			this.toolTip.SetToolTip(this.cbWordWrap, "Перенос строк");
			this.cbWordWrap.UseVisualStyleBackColor = true;
			this.cbWordWrap.CheckedChanged += new System.EventHandler(this.cbWordWrap_CheckedChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::CommonWinForms.Properties.Resources.btnRemove_Image;
			this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCancel.Location = new System.Drawing.Point(1044, 724);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(132, 25);
			this.btnCancel.TabIndex = 34;
			this.btnCancel.Text = "Закрыть";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCopy.Image = global::CommonWinForms.Properties.Resources.clipboard_text;
			this.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnCopy.Location = new System.Drawing.Point(7, 724);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(132, 25);
			this.btnCopy.TabIndex = 36;
			this.btnCopy.Text = "  Скопировать";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// ShowTextDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(1184, 761);
			this.Controls.Add(this.cbWordWrap);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.wideLine1);
			this.MinimizeBox = false;
			this.Name = "ShowTextDlg";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ShowTextDlg";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private CommonWinForms.Controls.WideLine wideLine1;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.CheckBox cbWordWrap;
		private System.Windows.Forms.ToolTip toolTip;
	}
}