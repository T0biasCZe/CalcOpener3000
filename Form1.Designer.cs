namespace CalcOpener3000 {
	partial class Form1 {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			checkBox_opencalc = new CheckBox();
			timer = new System.Windows.Forms.Timer(components);
			textBox1 = new TextBox();
			toolTip1 = new ToolTip(components);
			checkBox_nomultiplecalcs = new CheckBox();
			label_aero = new Label();
			label_numlock = new Label();
			notifyIcon1 = new NotifyIcon(components);
			contextMenuStrip1 = new ContextMenuStrip(components);
			exitToolStripMenuItem = new ToolStripMenuItem();
			linkLabel_autostart = new LinkLabel();
			contextMenuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// checkBox_opencalc
			// 
			checkBox_opencalc.AutoSize = true;
			checkBox_opencalc.BackColor = Color.Transparent;
			checkBox_opencalc.ForeColor = SystemColors.WindowText;
			checkBox_opencalc.Location = new Point(12, 30);
			checkBox_opencalc.Name = "checkBox_opencalc";
			checkBox_opencalc.Size = new Size(112, 19);
			checkBox_opencalc.TabIndex = 0;
			checkBox_opencalc.Text = "Open Calculator";
			checkBox_opencalc.UseVisualStyleBackColor = false;
			checkBox_opencalc.CheckedChanged += checkBox_opencalc_CheckedChanged;
			// 
			// timer
			// 
			timer.Enabled = true;
			timer.Interval = 500;
			// 
			// textBox1
			// 
			textBox1.BackColor = Color.White;
			textBox1.Location = new Point(8, 72);
			textBox1.Name = "textBox1";
			textBox1.PlaceholderText = "Calculator path";
			textBox1.Size = new Size(224, 23);
			textBox1.TabIndex = 2;
			textBox1.Text = "C:\\Windows\\System32\\calc.exe";
			toolTip1.SetToolTip(textBox1, "Enter path to calculator executable that should be opened");
			// 
			// checkBox_nomultiplecalcs
			// 
			checkBox_nomultiplecalcs.AutoSize = true;
			checkBox_nomultiplecalcs.BackColor = Color.Transparent;
			checkBox_nomultiplecalcs.ForeColor = SystemColors.WindowText;
			checkBox_nomultiplecalcs.Location = new Point(12, 50);
			checkBox_nomultiplecalcs.Name = "checkBox_nomultiplecalcs";
			checkBox_nomultiplecalcs.Size = new Size(158, 19);
			checkBox_nomultiplecalcs.TabIndex = 3;
			checkBox_nomultiplecalcs.Text = "Dont open multiple calcs";
			toolTip1.SetToolTip(checkBox_nomultiplecalcs, "If app is already opened, it will focus the already running calculator instead of opening new one");
			checkBox_nomultiplecalcs.UseVisualStyleBackColor = false;
			// 
			// label_aero
			// 
			label_aero.AutoSize = true;
			label_aero.BackColor = Color.Transparent;
			label_aero.Font = new Font("Segoe UI Semilight", 9F, FontStyle.Italic, GraphicsUnit.Point, 238);
			label_aero.ForeColor = Color.Transparent;
			label_aero.Location = new Point(186, 50);
			label_aero.Name = "label_aero";
			label_aero.Size = new Size(45, 15);
			label_aero.TabIndex = 4;
			label_aero.Text = "aero on";
			label_aero.Visible = false;
			// 
			// label_numlock
			// 
			label_numlock.AutoSize = true;
			label_numlock.BackColor = Color.Transparent;
			label_numlock.ForeColor = SystemColors.WindowText;
			label_numlock.Location = new Point(10, 10);
			label_numlock.Name = "label_numlock";
			label_numlock.Size = new Size(95, 15);
			label_numlock.TabIndex = 5;
			label_numlock.Text = "NumLock is N/A";
			// 
			// notifyIcon1
			// 
			notifyIcon1.ContextMenuStrip = contextMenuStrip1;
			notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
			notifyIcon1.Text = "notifyIcon1";
			notifyIcon1.Visible = true;
			notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
			// 
			// contextMenuStrip1
			// 
			contextMenuStrip1.Items.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new Size(94, 26);
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new Size(93, 22);
			exitToolStripMenuItem.Text = "Exit";
			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
			// 
			// linkLabel_autostart
			// 
			linkLabel_autostart.AutoSize = true;
			linkLabel_autostart.Location = new Point(8, 96);
			linkLabel_autostart.Name = "linkLabel_autostart";
			linkLabel_autostart.Size = new Size(75, 15);
			linkLabel_autostart.TabIndex = 6;
			linkLabel_autostart.TabStop = true;
			linkLabel_autostart.Text = "autostart: off";
			linkLabel_autostart.LinkClicked += linkLabel_autostart_LinkClicked;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(238, 110);
			Controls.Add(label_numlock);
			Controls.Add(label_aero);
			Controls.Add(checkBox_nomultiplecalcs);
			Controls.Add(textBox1);
			Controls.Add(checkBox_opencalc);
			Controls.Add(linkLabel_autostart);
			ForeColor = Color.Black;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "Form1";
			Text = "Calc3000";
			Deactivate += Form1_Leave;
			FormClosing += Form1_FormClosing;
			Leave += Form1_Leave;
			contextMenuStrip1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private CheckBox checkBox_opencalc;
		private System.Windows.Forms.Timer timer;
		private TextBox textBox1;
		private ToolTip toolTip1;
		private CheckBox checkBox_nomultiplecalcs;
		private Label label_aero;
		private Label label_numlock;
		private NotifyIcon notifyIcon1;
		private ContextMenuStrip contextMenuStrip1;
		private ToolStripMenuItem exitToolStripMenuItem;
		private LinkLabel linkLabel_autostart;
	}
}
