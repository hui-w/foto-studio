namespace QLike.Foto.BatchResizer
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlSrcDest = new System.Windows.Forms.Panel();
            this.linkTarget = new System.Windows.Forms.LinkLabel();
            this.linkSource = new System.Windows.Forms.LinkLabel();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.lblTarget = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.pnlProperties = new System.Windows.Forms.Panel();
            this.groupProperties = new System.Windows.Forms.GroupBox();
            this.pnlType = new System.Windows.Forms.Panel();
            this.radioType1 = new System.Windows.Forms.RadioButton();
            this.radioType2 = new System.Windows.Forms.RadioButton();
            this.radioType3 = new System.Windows.Forms.RadioButton();
            this.lblSizeSymbol = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.numThumbHeight = new System.Windows.Forms.NumericUpDown();
            this.numThumbWidth = new System.Windows.Forms.NumericUpDown();
            this.lblQuality = new System.Windows.Forms.Label();
            this.numQuality = new System.Windows.Forms.NumericUpDown();
            this.pictureType = new System.Windows.Forms.PictureBox();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnOpenTarget = new System.Windows.Forms.ToolStripButton();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlPreview = new System.Windows.Forms.Panel();
            this.lstPreview = new System.Windows.Forms.ListBox();
            this.pnlSrcDest.SuspendLayout();
            this.pnlProperties.SuspendLayout();
            this.groupProperties.SuspendLayout();
            this.pnlType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThumbHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThumbWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureType)).BeginInit();
            this.toolStripMain.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSrcDest
            // 
            this.pnlSrcDest.Controls.Add(this.linkTarget);
            this.pnlSrcDest.Controls.Add(this.linkSource);
            this.pnlSrcDest.Controls.Add(this.txtTarget);
            this.pnlSrcDest.Controls.Add(this.lblTarget);
            this.pnlSrcDest.Controls.Add(this.lblSource);
            this.pnlSrcDest.Controls.Add(this.txtSource);
            this.pnlSrcDest.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSrcDest.Location = new System.Drawing.Point(0, 0);
            this.pnlSrcDest.Name = "pnlSrcDest";
            this.pnlSrcDest.Size = new System.Drawing.Size(684, 80);
            this.pnlSrcDest.TabIndex = 0;
            // 
            // linkTarget
            // 
            this.linkTarget.AutoSize = true;
            this.linkTarget.Location = new System.Drawing.Point(621, 49);
            this.linkTarget.Name = "linkTarget";
            this.linkTarget.Size = new System.Drawing.Size(51, 13);
            this.linkTarget.TabIndex = 5;
            this.linkTarget.TabStop = true;
            this.linkTarget.Text = "&Browse...";
            this.linkTarget.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTarget_LinkClicked);
            // 
            // linkSource
            // 
            this.linkSource.AutoSize = true;
            this.linkSource.Location = new System.Drawing.Point(621, 13);
            this.linkSource.Name = "linkSource";
            this.linkSource.Size = new System.Drawing.Size(51, 13);
            this.linkSource.TabIndex = 4;
            this.linkSource.TabStop = true;
            this.linkSource.Text = "&Browse...";
            this.linkSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSource_LinkClicked);
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(95, 46);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(520, 20);
            this.txtTarget.TabIndex = 3;
            this.txtTarget.TextChanged += new System.EventHandler(this.txtTarget_TextChanged);
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Location = new System.Drawing.Point(13, 49);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(73, 13);
            this.lblTarget.TabIndex = 2;
            this.lblTarget.Text = "Target Folder:";
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(13, 13);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(76, 13);
            this.lblSource.TabIndex = 1;
            this.lblSource.Text = "Source Folder:";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(95, 10);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(520, 20);
            this.txtSource.TabIndex = 0;
            this.txtSource.Leave += new System.EventHandler(this.txtSource_Leave);
            // 
            // pnlProperties
            // 
            this.pnlProperties.Controls.Add(this.groupProperties);
            this.pnlProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlProperties.Location = new System.Drawing.Point(0, 80);
            this.pnlProperties.Name = "pnlProperties";
            this.pnlProperties.Padding = new System.Windows.Forms.Padding(10, 0, 10, 5);
            this.pnlProperties.Size = new System.Drawing.Size(684, 140);
            this.pnlProperties.TabIndex = 1;
            // 
            // groupProperties
            // 
            this.groupProperties.Controls.Add(this.pnlType);
            this.groupProperties.Controls.Add(this.lblSizeSymbol);
            this.groupProperties.Controls.Add(this.lblSize);
            this.groupProperties.Controls.Add(this.numThumbHeight);
            this.groupProperties.Controls.Add(this.numThumbWidth);
            this.groupProperties.Controls.Add(this.lblQuality);
            this.groupProperties.Controls.Add(this.numQuality);
            this.groupProperties.Controls.Add(this.pictureType);
            this.groupProperties.Controls.Add(this.chkOverwrite);
            this.groupProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupProperties.Location = new System.Drawing.Point(10, 0);
            this.groupProperties.Name = "groupProperties";
            this.groupProperties.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            this.groupProperties.Size = new System.Drawing.Size(664, 135);
            this.groupProperties.TabIndex = 0;
            this.groupProperties.TabStop = false;
            // 
            // pnlType
            // 
            this.pnlType.Controls.Add(this.radioType1);
            this.pnlType.Controls.Add(this.radioType2);
            this.pnlType.Controls.Add(this.radioType3);
            this.pnlType.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlType.Location = new System.Drawing.Point(230, 17);
            this.pnlType.Name = "pnlType";
            this.pnlType.Size = new System.Drawing.Size(106, 110);
            this.pnlType.TabIndex = 11;
            // 
            // radioType1
            // 
            this.radioType1.AutoSize = true;
            this.radioType1.Location = new System.Drawing.Point(3, 14);
            this.radioType1.Name = "radioType1";
            this.radioType1.Size = new System.Drawing.Size(58, 17);
            this.radioType1.TabIndex = 4;
            this.radioType1.TabStop = true;
            this.radioType1.Text = "Type 1";
            this.radioType1.UseVisualStyleBackColor = true;
            this.radioType1.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // radioType2
            // 
            this.radioType2.AutoSize = true;
            this.radioType2.Location = new System.Drawing.Point(3, 38);
            this.radioType2.Name = "radioType2";
            this.radioType2.Size = new System.Drawing.Size(58, 17);
            this.radioType2.TabIndex = 5;
            this.radioType2.TabStop = true;
            this.radioType2.Text = "Type 2";
            this.radioType2.UseVisualStyleBackColor = true;
            this.radioType2.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // radioType3
            // 
            this.radioType3.AutoSize = true;
            this.radioType3.Location = new System.Drawing.Point(3, 63);
            this.radioType3.Name = "radioType3";
            this.radioType3.Size = new System.Drawing.Size(58, 17);
            this.radioType3.TabIndex = 6;
            this.radioType3.TabStop = true;
            this.radioType3.Text = "Type 3";
            this.radioType3.UseVisualStyleBackColor = true;
            this.radioType3.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // lblSizeSymbol
            // 
            this.lblSizeSymbol.AutoSize = true;
            this.lblSizeSymbol.Location = new System.Drawing.Point(140, 72);
            this.lblSizeSymbol.Name = "lblSizeSymbol";
            this.lblSizeSymbol.Size = new System.Drawing.Size(14, 13);
            this.lblSizeSymbol.TabIndex = 10;
            this.lblSizeSymbol.Text = "X";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(8, 72);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(55, 13);
            this.lblSize.TabIndex = 9;
            this.lblSize.Text = "New Size:";
            // 
            // numThumbHeight
            // 
            this.numThumbHeight.Location = new System.Drawing.Point(160, 69);
            this.numThumbHeight.Maximum = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            this.numThumbHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numThumbHeight.Name = "numThumbHeight";
            this.numThumbHeight.Size = new System.Drawing.Size(48, 20);
            this.numThumbHeight.TabIndex = 8;
            this.numThumbHeight.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // numThumbWidth
            // 
            this.numThumbWidth.Location = new System.Drawing.Point(86, 69);
            this.numThumbWidth.Maximum = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            this.numThumbWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numThumbWidth.Name = "numThumbWidth";
            this.numThumbWidth.Size = new System.Drawing.Size(48, 20);
            this.numThumbWidth.TabIndex = 7;
            this.numThumbWidth.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(8, 46);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(72, 13);
            this.lblQuality.TabIndex = 3;
            this.lblQuality.Text = "JPEG Quality:";
            // 
            // numQuality
            // 
            this.numQuality.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numQuality.Location = new System.Drawing.Point(86, 43);
            this.numQuality.Name = "numQuality";
            this.numQuality.Size = new System.Drawing.Size(48, 20);
            this.numQuality.TabIndex = 2;
            this.numQuality.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // pictureType
            // 
            this.pictureType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureType.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureType.Location = new System.Drawing.Point(336, 17);
            this.pictureType.Name = "pictureType";
            this.pictureType.Size = new System.Drawing.Size(320, 110);
            this.pictureType.TabIndex = 1;
            this.pictureType.TabStop = false;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Checked = true;
            this.chkOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOverwrite.Location = new System.Drawing.Point(11, 21);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(134, 17);
            this.chkOverwrite.TabIndex = 0;
            this.chkOverwrite.Text = "Overwrite Existing Files";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnOpenTarget});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(684, 25);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // btnStart
            // 
            this.btnStart.Image = global::QLike.Foto.BatchResizer.Properties.Resources.icon_start;
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(86, 22);
            this.btnStart.Text = "&Start Resize";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnOpenTarget
            // 
            this.btnOpenTarget.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenTarget.Image")));
            this.btnOpenTarget.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenTarget.Name = "btnOpenTarget";
            this.btnOpenTarget.Size = new System.Drawing.Size(129, 22);
            this.btnOpenTarget.Text = "Open Target Folder";
            this.btnOpenTarget.Visible = false;
            this.btnOpenTarget.Click += new System.EventHandler(this.btnOpenTarget_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(684, 24);
            this.menuStripMain.TabIndex = 3;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(92, 22);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "&Help";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(116, 22);
            this.menuAbout.Text = "&About...";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.lblStatus});
            this.statusStripMain.Location = new System.Drawing.Point(0, 440);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(684, 22);
            this.statusStripMain.TabIndex = 4;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 16);
            this.progressBar.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.pnlPreview);
            this.pnlMain.Controls.Add(this.pnlProperties);
            this.pnlMain.Controls.Add(this.pnlSrcDest);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 49);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(684, 391);
            this.pnlMain.TabIndex = 5;
            // 
            // pnlPreview
            // 
            this.pnlPreview.Controls.Add(this.lstPreview);
            this.pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreview.Location = new System.Drawing.Point(0, 220);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Padding = new System.Windows.Forms.Padding(8);
            this.pnlPreview.Size = new System.Drawing.Size(684, 171);
            this.pnlPreview.TabIndex = 2;
            // 
            // lstPreview
            // 
            this.lstPreview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lstPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPreview.FormattingEnabled = true;
            this.lstPreview.HorizontalScrollbar = true;
            this.lstPreview.Location = new System.Drawing.Point(8, 8);
            this.lstPreview.Name = "lstPreview";
            this.lstPreview.Size = new System.Drawing.Size(668, 147);
            this.lstPreview.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "frmMain";
            this.Text = "Batch Resizer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.pnlSrcDest.ResumeLayout(false);
            this.pnlSrcDest.PerformLayout();
            this.pnlProperties.ResumeLayout(false);
            this.groupProperties.ResumeLayout(false);
            this.groupProperties.PerformLayout();
            this.pnlType.ResumeLayout(false);
            this.pnlType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThumbHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThumbWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureType)).EndInit();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlPreview.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSrcDest;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.LinkLabel linkTarget;
        private System.Windows.Forms.LinkLabel linkSource;
        private System.Windows.Forms.Panel pnlProperties;
        private System.Windows.Forms.GroupBox groupProperties;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.PictureBox pictureType;
        private System.Windows.Forms.NumericUpDown numQuality;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.RadioButton radioType3;
        private System.Windows.Forms.RadioButton radioType2;
        private System.Windows.Forms.RadioButton radioType1;
        private System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.ListBox lstPreview;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.NumericUpDown numThumbWidth;
        private System.Windows.Forms.NumericUpDown numThumbHeight;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblSizeSymbol;
        private System.Windows.Forms.Panel pnlType;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripButton btnOpenTarget;

    }
}

