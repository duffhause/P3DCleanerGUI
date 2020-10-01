namespace SHARModOrganiserGUI
{
	partial class MainWindow
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
		public void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.Settings = new System.Windows.Forms.GroupBox();
			this.customHistory = new System.Windows.Forms.CheckBox();
			this.removeHistory = new System.Windows.Forms.CheckBox();
			this.orderChunksLexo = new System.Windows.Forms.CheckBox();
			this.Submit = new System.Windows.Forms.Button();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.modPath = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.Settings.SuspendLayout();
			this.SuspendLayout();
			// 
			// Settings
			// 
			this.Settings.BackColor = System.Drawing.SystemColors.Control;
			this.Settings.Controls.Add(this.customHistory);
			this.Settings.Controls.Add(this.removeHistory);
			this.Settings.Controls.Add(this.orderChunksLexo);
			this.Settings.Location = new System.Drawing.Point(12, 12);
			this.Settings.Name = "Settings";
			this.Settings.Size = new System.Drawing.Size(200, 113);
			this.Settings.TabIndex = 0;
			this.Settings.TabStop = false;
			this.Settings.Text = "Settings";
			// 
			// customHistory
			// 
			this.customHistory.Location = new System.Drawing.Point(6, 79);
			this.customHistory.Name = "customHistory";
			this.customHistory.Size = new System.Drawing.Size(188, 24);
			this.customHistory.TabIndex = 0;
			this.customHistory.Text = "Insert custom history chunks";
			this.customHistory.CheckedChanged += new System.EventHandler(this.customHistory_CheckedChanged);
			// 
			// removeHistory
			// 
			this.removeHistory.Location = new System.Drawing.Point(6, 49);
			this.removeHistory.Name = "removeHistory";
			this.removeHistory.Size = new System.Drawing.Size(188, 24);
			this.removeHistory.TabIndex = 1;
			this.removeHistory.Text = "Remove history and export info";
			// 
			// orderChunksLexo
			// 
			this.orderChunksLexo.Location = new System.Drawing.Point(6, 19);
			this.orderChunksLexo.Name = "orderChunksLexo";
			this.orderChunksLexo.Size = new System.Drawing.Size(188, 24);
			this.orderChunksLexo.TabIndex = 2;
			this.orderChunksLexo.Text = "Alphabetically order chunks";
			// 
			// Submit
			// 
			this.Submit.Location = new System.Drawing.Point(717, 102);
			this.Submit.Name = "Submit";
			this.Submit.Size = new System.Drawing.Size(75, 23);
			this.Submit.TabIndex = 1;
			this.Submit.Text = "Submit";
			this.Submit.UseVisualStyleBackColor = true;
			this.Submit.Click += new System.EventHandler(this.Submit_Click);
			// 
			// modPath
			// 
			this.modPath.Location = new System.Drawing.Point(218, 23);
			this.modPath.Name = "modPath";
			this.modPath.ReadOnly = true;
			this.modPath.Size = new System.Drawing.Size(574, 20);
			this.modPath.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(218, 49);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(87, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Mod folder";
			this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(218, 78);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(87, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "Individual P3D";
			this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(804, 135);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.modPath);
			this.Controls.Add(this.Submit);
			this.Controls.Add(this.Settings);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainWindow";
			this.Text = "P3D Cleaner";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.Settings.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox Settings;
		private System.Windows.Forms.CheckBox customHistory;
		private System.Windows.Forms.CheckBox removeHistory;
		private System.Windows.Forms.CheckBox orderChunksLexo;
		private System.Windows.Forms.Button Submit;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.TextBox modPath;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}

