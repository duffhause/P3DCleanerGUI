namespace P3DCleaner
{
	partial class InputHistoryChunkForm
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
			if (disposing && (components != null))
			{
				components.Dispose();
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
			this.amountOfLines = new System.Windows.Forms.NumericUpDown();
			this.SubmitLines = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.amountOfLines)).BeginInit();
			this.SuspendLayout();
			// 
			// amountOfLines
			// 
			this.amountOfLines.Location = new System.Drawing.Point(15, 25);
			this.amountOfLines.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.amountOfLines.Name = "amountOfLines";
			this.amountOfLines.Size = new System.Drawing.Size(260, 20);
			this.amountOfLines.TabIndex = 1;
			this.amountOfLines.ThousandsSeparator = true;
			this.amountOfLines.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// SubmitLines
			// 
			this.SubmitLines.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.SubmitLines.Location = new System.Drawing.Point(281, 22);
			this.SubmitLines.Name = "SubmitLines";
			this.SubmitLines.Size = new System.Drawing.Size(75, 23);
			this.SubmitLines.TabIndex = 3;
			this.SubmitLines.Text = "Submit";
			this.SubmitLines.UseVisualStyleBackColor = true;
			this.SubmitLines.Click += new System.EventHandler(this.SubmitLines_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Amount of lines";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// InputHistoryChunkForm
			// 
			this.AcceptButton = this.SubmitLines;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.CancelButton = this.SubmitLines;
			this.ClientSize = new System.Drawing.Size(396, 261);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SubmitLines);
			this.Controls.Add(this.amountOfLines);
			this.Name = "InputHistoryChunkForm";
			this.Load += new System.EventHandler(this.CustomHistoryChunkScreen_Load);
			((System.ComponentModel.ISupportInitialize)(this.amountOfLines)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown amountOfLines;
		private System.Windows.Forms.Button SubmitLines;
		private System.Windows.Forms.Label label1;
	}
}