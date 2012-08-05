namespace RegtestChecker
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btnGo = new System.Windows.Forms.Button();
			this.btnBrowseOldExe = new System.Windows.Forms.Button();
			this.txtOldExe = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowseOldReg = new System.Windows.Forms.Button();
			this.txtOldReg = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowseNewReg = new System.Windows.Forms.Button();
			this.txtNewReg = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnBrowseNewExe = new System.Windows.Forms.Button();
			this.txtNewExe = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtStatus1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
			this.label6 = new System.Windows.Forms.Label();
			this.txtFileSpec = new System.Windows.Forms.TextBox();
			this.chkUseOld = new System.Windows.Forms.CheckBox();
			this.chkUseExistNew = new System.Windows.Forms.CheckBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.UpDownDiffs = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.UpDownThreads = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.chkUseSame = new System.Windows.Forms.CheckBox();
			this.btnInclude = new System.Windows.Forms.Button();
			this.txtInclude = new System.Windows.Forms.TextBox();
			this.lblInclude = new System.Windows.Forms.Label();
			this.chkSubdirs = new System.Windows.Forms.CheckBox();
			this.UpDownResolution = new System.Windows.Forms.NumericUpDown();
			this.label9 = new System.Windows.Forms.Label();
			this.pnlOldColour = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.label11 = new System.Windows.Forms.Label();
			this.pnlNewColour = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.UpDownDiffs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UpDownThreads)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UpDownResolution)).BeginInit();
			this.SuspendLayout();
			// 
			// btnGo
			// 
			this.btnGo.Enabled = false;
			this.btnGo.Location = new System.Drawing.Point(159, 238);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 0;
			this.btnGo.Text = "Go";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnBrowseOldExe
			// 
			this.btnBrowseOldExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseOldExe.Location = new System.Drawing.Point(476, 3);
			this.btnBrowseOldExe.Name = "btnBrowseOldExe";
			this.btnBrowseOldExe.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseOldExe.TabIndex = 22;
			this.btnBrowseOldExe.Text = "Browse...";
			this.btnBrowseOldExe.UseVisualStyleBackColor = true;
			this.btnBrowseOldExe.Click += new System.EventHandler(this.btnBrowseOldExe_Click);
			// 
			// txtOldExe
			// 
			this.txtOldExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOldExe.Location = new System.Drawing.Point(159, 8);
			this.txtOldExe.Name = "txtOldExe";
			this.txtOldExe.Size = new System.Drawing.Size(311, 20);
			this.txtOldExe.TabIndex = 21;
			this.txtOldExe.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(61, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 13);
			this.label2.TabIndex = 20;
			this.label2.Text = "Old LilyPond .exe:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnBrowseOldReg
			// 
			this.btnBrowseOldReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseOldReg.Location = new System.Drawing.Point(476, 30);
			this.btnBrowseOldReg.Name = "btnBrowseOldReg";
			this.btnBrowseOldReg.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseOldReg.TabIndex = 25;
			this.btnBrowseOldReg.Text = "Browse...";
			this.btnBrowseOldReg.UseVisualStyleBackColor = true;
			this.btnBrowseOldReg.Click += new System.EventHandler(this.btnBrowseOldReg_Click);
			// 
			// txtOldReg
			// 
			this.txtOldReg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOldReg.Location = new System.Drawing.Point(159, 34);
			this.txtOldReg.Name = "txtOldReg";
			this.txtOldReg.Size = new System.Drawing.Size(311, 20);
			this.txtOldReg.TabIndex = 24;
			this.txtOldReg.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(44, 37);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(109, 13);
			this.label1.TabIndex = 23;
			this.label1.Text = "Old LilyPond regtests:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnBrowseNewReg
			// 
			this.btnBrowseNewReg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseNewReg.Location = new System.Drawing.Point(476, 131);
			this.btnBrowseNewReg.Name = "btnBrowseNewReg";
			this.btnBrowseNewReg.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseNewReg.TabIndex = 31;
			this.btnBrowseNewReg.Text = "Browse...";
			this.btnBrowseNewReg.UseVisualStyleBackColor = true;
			this.btnBrowseNewReg.Click += new System.EventHandler(this.btnBrowseNewReg_Click);
			// 
			// txtNewReg
			// 
			this.txtNewReg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtNewReg.BackColor = System.Drawing.SystemColors.Window;
			this.txtNewReg.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtNewReg.Location = new System.Drawing.Point(159, 133);
			this.txtNewReg.Name = "txtNewReg";
			this.txtNewReg.Size = new System.Drawing.Size(311, 20);
			this.txtNewReg.TabIndex = 30;
			this.txtNewReg.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(38, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(115, 13);
			this.label3.TabIndex = 29;
			this.label3.Text = "New LilyPond regtests:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnBrowseNewExe
			// 
			this.btnBrowseNewExe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseNewExe.Location = new System.Drawing.Point(476, 103);
			this.btnBrowseNewExe.Name = "btnBrowseNewExe";
			this.btnBrowseNewExe.Size = new System.Drawing.Size(75, 23);
			this.btnBrowseNewExe.TabIndex = 28;
			this.btnBrowseNewExe.Text = "Browse...";
			this.btnBrowseNewExe.UseVisualStyleBackColor = true;
			this.btnBrowseNewExe.Click += new System.EventHandler(this.btnBrowseNewExe_Click);
			// 
			// txtNewExe
			// 
			this.txtNewExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtNewExe.Location = new System.Drawing.Point(159, 107);
			this.txtNewExe.Name = "txtNewExe";
			this.txtNewExe.Size = new System.Drawing.Size(311, 20);
			this.txtNewExe.TabIndex = 27;
			this.txtNewExe.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(55, 110);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(98, 13);
			this.label4.TabIndex = 26;
			this.label4.Text = "New LilyPond .exe:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtStatus1
			// 
			this.txtStatus1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtStatus1.Location = new System.Drawing.Point(159, 267);
			this.txtStatus1.Name = "txtStatus1";
			this.txtStatus1.ReadOnly = true;
			this.txtStatus1.Size = new System.Drawing.Size(356, 20);
			this.txtStatus1.TabIndex = 33;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(76, 270);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 13);
			this.label5.TabIndex = 32;
			this.label5.Text = "File in process:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// UpdateTimer
			// 
			this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(33, 162);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 13);
			this.label6.TabIndex = 34;
			this.label6.Text = "File spec. (default is *.ly)";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtFileSpec
			// 
			this.txtFileSpec.Location = new System.Drawing.Point(159, 159);
			this.txtFileSpec.Name = "txtFileSpec";
			this.txtFileSpec.Size = new System.Drawing.Size(166, 20);
			this.txtFileSpec.TabIndex = 35;
			this.txtFileSpec.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
			// 
			// chkUseOld
			// 
			this.chkUseOld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkUseOld.AutoSize = true;
			this.chkUseOld.Location = new System.Drawing.Point(557, 36);
			this.chkUseOld.Name = "chkUseOld";
			this.chkUseOld.Size = new System.Drawing.Size(119, 17);
			this.chkUseOld.TabIndex = 36;
			this.chkUseOld.Text = "Use existing images";
			this.chkUseOld.UseVisualStyleBackColor = true;
			this.chkUseOld.CheckedChanged += new System.EventHandler(this.CheckChanged);
			// 
			// chkUseExistNew
			// 
			this.chkUseExistNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkUseExistNew.AutoSize = true;
			this.chkUseExistNew.Location = new System.Drawing.Point(557, 137);
			this.chkUseExistNew.Name = "chkUseExistNew";
			this.chkUseExistNew.Size = new System.Drawing.Size(119, 17);
			this.chkUseExistNew.TabIndex = 37;
			this.chkUseExistNew.Text = "Use existing images";
			this.chkUseExistNew.UseVisualStyleBackColor = true;
			this.chkUseExistNew.CheckedChanged += new System.EventHandler(this.CheckChanged);
			// 
			// btnStop
			// 
			this.btnStop.BackColor = System.Drawing.Color.LightCoral;
			this.btnStop.Enabled = false;
			this.btnStop.Location = new System.Drawing.Point(562, 221);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(132, 76);
			this.btnStop.TabIndex = 38;
			this.btnStop.Text = "Abort!";
			this.btnStop.UseVisualStyleBackColor = false;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// UpDownDiffs
			// 
			this.UpDownDiffs.Location = new System.Drawing.Point(473, 241);
			this.UpDownDiffs.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.UpDownDiffs.Name = "UpDownDiffs";
			this.UpDownDiffs.Size = new System.Drawing.Size(40, 20);
			this.UpDownDiffs.TabIndex = 39;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(329, 243);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(138, 13);
			this.label7.TabIndex = 40;
			this.label7.Text = "Differences to be significant";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// UpDownThreads
			// 
			this.UpDownThreads.Location = new System.Drawing.Point(159, 211);
			this.UpDownThreads.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.UpDownThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UpDownThreads.Name = "UpDownThreads";
			this.UpDownThreads.Size = new System.Drawing.Size(34, 20);
			this.UpDownThreads.TabIndex = 41;
			this.UpDownThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UpDownThreads.ValueChanged += new System.EventHandler(this.UpDownThreads_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(59, 213);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(94, 13);
			this.label8.TabIndex = 42;
			this.label8.Text = "Number of threads";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// chkUseSame
			// 
			this.chkUseSame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkUseSame.AutoSize = true;
			this.chkUseSame.Location = new System.Drawing.Point(557, 59);
			this.chkUseSame.Name = "chkUseSame";
			this.chkUseSame.Size = new System.Drawing.Size(147, 17);
			this.chkUseSame.TabIndex = 43;
			this.chkUseSame.Text = "Use the same regtest files";
			this.chkUseSame.UseVisualStyleBackColor = true;
			this.chkUseSame.CheckedChanged += new System.EventHandler(this.chkUseSame_CheckedChanged);
			// 
			// btnInclude
			// 
			this.btnInclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInclude.Location = new System.Drawing.Point(476, 183);
			this.btnInclude.Name = "btnInclude";
			this.btnInclude.Size = new System.Drawing.Size(75, 23);
			this.btnInclude.TabIndex = 46;
			this.btnInclude.Text = "Browse...";
			this.btnInclude.UseVisualStyleBackColor = true;
			this.btnInclude.Click += new System.EventHandler(this.btnInclude_Click);
			// 
			// txtInclude
			// 
			this.txtInclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInclude.BackColor = System.Drawing.SystemColors.Window;
			this.txtInclude.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtInclude.Location = new System.Drawing.Point(159, 185);
			this.txtInclude.Name = "txtInclude";
			this.txtInclude.Size = new System.Drawing.Size(311, 20);
			this.txtInclude.TabIndex = 45;
			this.txtInclude.TextChanged += new System.EventHandler(this.txtBox_TextChanged);
			// 
			// lblInclude
			// 
			this.lblInclude.AutoSize = true;
			this.lblInclude.Location = new System.Drawing.Point(76, 188);
			this.lblInclude.Name = "lblInclude";
			this.lblInclude.Size = new System.Drawing.Size(77, 13);
			this.lblInclude.TabIndex = 44;
			this.lblInclude.Text = "Include this file";
			this.lblInclude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// chkSubdirs
			// 
			this.chkSubdirs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkSubdirs.AutoSize = true;
			this.chkSubdirs.Location = new System.Drawing.Point(557, 82);
			this.chkSubdirs.Name = "chkSubdirs";
			this.chkSubdirs.Size = new System.Drawing.Size(129, 17);
			this.chkSubdirs.TabIndex = 48;
			this.chkSubdirs.Text = "Include subdirectories";
			this.chkSubdirs.UseVisualStyleBackColor = true;
			this.chkSubdirs.CheckedChanged += new System.EventHandler(this.CheckChanged);
			// 
			// UpDownResolution
			// 
			this.UpDownResolution.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.UpDownResolution.Location = new System.Drawing.Point(473, 215);
			this.UpDownResolution.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.UpDownResolution.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.UpDownResolution.Name = "UpDownResolution";
			this.UpDownResolution.Size = new System.Drawing.Size(42, 20);
			this.UpDownResolution.TabIndex = 49;
			this.UpDownResolution.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.UpDownResolution.ValueChanged += new System.EventHandler(this.UpDownResolution_ValueChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(387, 217);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 13);
			this.label9.TabIndex = 50;
			this.label9.Text = "Resolution (dpi)";
			// 
			// pnlOldColour
			// 
			this.pnlOldColour.BackColor = System.Drawing.Color.Lime;
			this.pnlOldColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlOldColour.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pnlOldColour.Location = new System.Drawing.Point(599, 6);
			this.pnlOldColour.Name = "pnlOldColour";
			this.pnlOldColour.Size = new System.Drawing.Size(40, 20);
			this.pnlOldColour.TabIndex = 51;
			this.pnlOldColour.Click += new System.EventHandler(this.pnlOldColour_Click);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(556, 8);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(37, 13);
			this.label10.TabIndex = 52;
			this.label10.Text = "Colour";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(556, 108);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(37, 13);
			this.label11.TabIndex = 54;
			this.label11.Text = "Colour";
			// 
			// pnlNewColour
			// 
			this.pnlNewColour.BackColor = System.Drawing.Color.Red;
			this.pnlNewColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlNewColour.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pnlNewColour.Location = new System.Drawing.Point(599, 106);
			this.pnlNewColour.Name = "pnlNewColour";
			this.pnlNewColour.Size = new System.Drawing.Size(40, 20);
			this.pnlNewColour.TabIndex = 53;
			this.pnlNewColour.Click += new System.EventHandler(this.pnlNewColour_Click);
			// 
			// MainForm
			// 
			this.AcceptButton = this.btnGo;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(706, 311);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.pnlNewColour);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.pnlOldColour);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.UpDownResolution);
			this.Controls.Add(this.chkSubdirs);
			this.Controls.Add(this.btnInclude);
			this.Controls.Add(this.txtInclude);
			this.Controls.Add(this.lblInclude);
			this.Controls.Add(this.chkUseSame);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.UpDownThreads);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.UpDownDiffs);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.chkUseExistNew);
			this.Controls.Add(this.chkUseOld);
			this.Controls.Add(this.txtFileSpec);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtStatus1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnBrowseNewReg);
			this.Controls.Add(this.txtNewReg);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnBrowseNewExe);
			this.Controls.Add(this.txtNewExe);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnBrowseOldReg);
			this.Controls.Add(this.txtOldReg);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnBrowseOldExe);
			this.Controls.Add(this.txtOldExe);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnGo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Regtest checker";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Closing);
			((System.ComponentModel.ISupportInitialize)(this.UpDownDiffs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UpDownThreads)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UpDownResolution)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.Button btnBrowseOldExe;
		private System.Windows.Forms.TextBox txtOldExe;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowseOldReg;
		private System.Windows.Forms.TextBox txtOldReg;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowseNewReg;
		private System.Windows.Forms.TextBox txtNewReg;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnBrowseNewExe;
		private System.Windows.Forms.TextBox txtNewExe;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtStatus1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Timer UpdateTimer;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtFileSpec;
		private System.Windows.Forms.CheckBox chkUseOld;
		private System.Windows.Forms.CheckBox chkUseExistNew;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.NumericUpDown UpDownDiffs;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown UpDownThreads;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.CheckBox chkUseSame;
		private System.Windows.Forms.Button btnInclude;
		private System.Windows.Forms.TextBox txtInclude;
		private System.Windows.Forms.Label lblInclude;
		private System.Windows.Forms.CheckBox chkSubdirs;
		private System.Windows.Forms.NumericUpDown UpDownResolution;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Panel pnlOldColour;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Panel pnlNewColour;
	}
}

