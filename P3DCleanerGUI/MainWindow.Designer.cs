namespace P3DCleaner
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
			this.deleteMisplaced = new System.Windows.Forms.CheckBox();
			this.customHistory = new System.Windows.Forms.CheckBox();
			this.removeHistory = new System.Windows.Forms.CheckBox();
			this.orderChunksLexo = new System.Windows.Forms.CheckBox();
			this.Submit = new System.Windows.Forms.Button();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.modPath = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.changeTheme = new System.Windows.Forms.Button();
			this.Settings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// Settings
			// 
			this.Settings.BackColor = System.Drawing.SystemColors.Control;
			this.Settings.Controls.Add(this.deleteMisplaced);
			this.Settings.Controls.Add(this.customHistory);
			this.Settings.Controls.Add(this.removeHistory);
			this.Settings.Controls.Add(this.orderChunksLexo);
			this.Settings.Location = new System.Drawing.Point(16, 15);
			this.Settings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Settings.Name = "Settings";
			this.Settings.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Settings.Size = new System.Drawing.Size(267, 176);
			this.Settings.TabIndex = 0;
			this.Settings.TabStop = false;
			this.Settings.Text = "Settings";
			// 
			// deleteMisplaced
			// 
			this.deleteMisplaced.AutoSize = true;
			this.deleteMisplaced.Checked = true;
			this.deleteMisplaced.CheckState = System.Windows.Forms.CheckState.Checked;
			this.deleteMisplaced.Location = new System.Drawing.Point(8, 134);
			this.deleteMisplaced.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.deleteMisplaced.Name = "deleteMisplaced";
			this.deleteMisplaced.Size = new System.Drawing.Size(187, 21);
			this.deleteMisplaced.TabIndex = 5;
			this.deleteMisplaced.Text = "Delete misplaced chunks";
			this.deleteMisplaced.UseVisualStyleBackColor = true;
			this.deleteMisplaced.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// customHistory
			// 
			this.customHistory.Location = new System.Drawing.Point(8, 97);
			this.customHistory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.customHistory.Name = "customHistory";
			this.customHistory.Size = new System.Drawing.Size(251, 30);
			this.customHistory.TabIndex = 0;
			this.customHistory.Text = "Insert custom history chunks";
			this.customHistory.CheckedChanged += new System.EventHandler(this.customHistory_CheckedChanged);
			// 
			// removeHistory
			// 
			this.removeHistory.Checked = true;
			this.removeHistory.CheckState = System.Windows.Forms.CheckState.Checked;
			this.removeHistory.Location = new System.Drawing.Point(8, 60);
			this.removeHistory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.removeHistory.Name = "removeHistory";
			this.removeHistory.Size = new System.Drawing.Size(251, 30);
			this.removeHistory.TabIndex = 1;
			this.removeHistory.Text = "Remove history and export info";
			// 
			// orderChunksLexo
			// 
			this.orderChunksLexo.Checked = true;
			this.orderChunksLexo.CheckState = System.Windows.Forms.CheckState.Checked;
			this.orderChunksLexo.Location = new System.Drawing.Point(8, 23);
			this.orderChunksLexo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.orderChunksLexo.Name = "orderChunksLexo";
			this.orderChunksLexo.Size = new System.Drawing.Size(251, 30);
			this.orderChunksLexo.TabIndex = 2;
			this.orderChunksLexo.Text = "Alphabetically order chunks";
			// 
			// Submit
			// 
			this.Submit.Location = new System.Drawing.Point(633, 162);
			this.Submit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Submit.Name = "Submit";
			this.Submit.Size = new System.Drawing.Size(100, 28);
			this.Submit.TabIndex = 1;
			this.Submit.Text = "Submit";
			this.Submit.UseVisualStyleBackColor = true;
			this.Submit.Click += new System.EventHandler(this.Submit_Click);
			// 
			// modPath
			// 
			this.modPath.Location = new System.Drawing.Point(291, 28);
			this.modPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.modPath.Name = "modPath";
			this.modPath.ReadOnly = true;
			this.modPath.Size = new System.Drawing.Size(445, 22);
			this.modPath.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(291, 60);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(116, 28);
			this.button1.TabIndex = 3;
			this.button1.Text = "Mod folder";
			this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(291, 96);
			this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(116, 28);
			this.button2.TabIndex = 4;
			this.button2.Text = "Individual P3D";
			this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::P3DCleaner.Properties.Resources.stoner_team;
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(633, 60);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 95);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// changeTheme
			// 
			this.changeTheme.Location = new System.Drawing.Point(291, 154);
			this.changeTheme.Name = "changeTheme";
			this.changeTheme.Size = new System.Drawing.Size(116, 36);
			this.changeTheme.TabIndex = 6;
			this.changeTheme.Text = "Change theme";
			this.changeTheme.UseVisualStyleBackColor = true;
			this.changeTheme.Click += new System.EventHandler(this.changeTheme_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(749, 207);
			this.Controls.Add(this.changeTheme);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.modPath);
			this.Controls.Add(this.Submit);
			this.Controls.Add(this.Settings);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "MainWindow";
			this.Text = "P3D Cleaner";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.Settings.ResumeLayout(false);
			this.Settings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        public System.Windows.Forms.CheckBox deleteMisplaced;
        private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button changeTheme;
	}
}

