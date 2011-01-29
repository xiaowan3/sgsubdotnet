namespace sgsubdotnet
{
    partial class SGSMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SGSMainForm));
            this.hSpliter = new System.Windows.Forms.SplitContainer();
            this.vSpliter = new System.Windows.Forms.SplitContainer();
            this.dxVideoPlayer = new VideoPlayer.DXVideoPlayer();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.spliterSoundbar = new System.Windows.Forms.SplitContainer();
            this.waveScope = new WaveReader.WaveScope();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labellastline = new System.Windows.Forms.Label();
            this.labelcurrent = new System.Windows.Forms.Label();
            this.labelnextline = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fileToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtnOpenAss = new System.Windows.Forms.ToolStripButton();
            this.tsBtnOpenTxt = new System.Windows.Forms.ToolStripButton();
            this.tsBtnOpenVideo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSaveSub = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnFFT = new System.Windows.Forms.ToolStripButton();
            this.smallSpliter = new System.Windows.Forms.SplitContainer();
            this.subLabel = new System.Windows.Forms.Label();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.subtitleGrid = new System.Windows.Forms.DataGridView();
            this.subToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtnPause = new System.Windows.Forms.ToolStripButton();
            this.tsBtnPlay = new System.Windows.Forms.ToolStripButton();
            this.tsBtnJumpto = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnDuplicate = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDelItem = new System.Windows.Forms.ToolStripButton();
            this.tsBtnInsAfter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnOLScan = new System.Windows.Forms.ToolStripButton();
            this.tsBtnUndo = new System.Windows.Forms.ToolStripButton();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenSubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveSubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsSubToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeyCfgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutSgsubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.mainpanel = new System.Windows.Forms.Panel();
            this.hSpliter.Panel1.SuspendLayout();
            this.hSpliter.Panel2.SuspendLayout();
            this.hSpliter.SuspendLayout();
            this.vSpliter.Panel1.SuspendLayout();
            this.vSpliter.Panel2.SuspendLayout();
            this.vSpliter.SuspendLayout();
            this.toolStripContainer2.ContentPanel.SuspendLayout();
            this.toolStripContainer2.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            this.spliterSoundbar.Panel1.SuspendLayout();
            this.spliterSoundbar.Panel2.SuspendLayout();
            this.spliterSoundbar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.fileToolStrip.SuspendLayout();
            this.smallSpliter.Panel1.SuspendLayout();
            this.smallSpliter.Panel2.SuspendLayout();
            this.smallSpliter.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subtitleGrid)).BeginInit();
            this.subToolStrip.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.mainpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // hSpliter
            // 
            this.hSpliter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.hSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hSpliter.Location = new System.Drawing.Point(0, 0);
            this.hSpliter.Name = "hSpliter";
            this.hSpliter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // hSpliter.Panel1
            // 
            this.hSpliter.Panel1.Controls.Add(this.vSpliter);
            // 
            // hSpliter.Panel2
            // 
            this.hSpliter.Panel2.Controls.Add(this.smallSpliter);
            this.hSpliter.Size = new System.Drawing.Size(742, 551);
            this.hSpliter.SplitterDistance = 243;
            this.hSpliter.TabIndex = 0;
            // 
            // vSpliter
            // 
            this.vSpliter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.vSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vSpliter.Location = new System.Drawing.Point(0, 0);
            this.vSpliter.Name = "vSpliter";
            // 
            // vSpliter.Panel1
            // 
            this.vSpliter.Panel1.Controls.Add(this.dxVideoPlayer);
            // 
            // vSpliter.Panel2
            // 
            this.vSpliter.Panel2.Controls.Add(this.toolStripContainer2);
            this.vSpliter.Size = new System.Drawing.Size(742, 243);
            this.vSpliter.SplitterDistance = 379;
            this.vSpliter.TabIndex = 0;
            // 
            // dxVideoPlayer
            // 
            this.dxVideoPlayer.BackColor = System.Drawing.Color.Black;
            this.dxVideoPlayer.CausesValidation = false;
            this.dxVideoPlayer.CurrentPosition = 0;
            this.dxVideoPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dxVideoPlayer.Location = new System.Drawing.Point(0, 0);
            this.dxVideoPlayer.Margin = new System.Windows.Forms.Padding(2);
            this.dxVideoPlayer.Name = "dxVideoPlayer";
            this.dxVideoPlayer.Size = new System.Drawing.Size(375, 239);
            this.dxVideoPlayer.TabIndex = 0;
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Controls.Add(this.spliterSoundbar);
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(355, 214);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(355, 239);
            this.toolStripContainer2.TabIndex = 0;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // toolStripContainer2.TopToolStripPanel
            // 
            this.toolStripContainer2.TopToolStripPanel.Controls.Add(this.fileToolStrip);
            // 
            // spliterSoundbar
            // 
            this.spliterSoundbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spliterSoundbar.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spliterSoundbar.IsSplitterFixed = true;
            this.spliterSoundbar.Location = new System.Drawing.Point(0, 0);
            this.spliterSoundbar.Margin = new System.Windows.Forms.Padding(2);
            this.spliterSoundbar.Name = "spliterSoundbar";
            this.spliterSoundbar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spliterSoundbar.Panel1
            // 
            this.spliterSoundbar.Panel1.Controls.Add(this.waveScope);
            // 
            // spliterSoundbar.Panel2
            // 
            this.spliterSoundbar.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.spliterSoundbar.Size = new System.Drawing.Size(355, 214);
            this.spliterSoundbar.SplitterDistance = 121;
            this.spliterSoundbar.SplitterWidth = 3;
            this.spliterSoundbar.TabIndex = 0;
            // 
            // waveScope
            // 
            this.waveScope.CurrentPosition = 0;
            this.waveScope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waveScope.Location = new System.Drawing.Point(0, 0);
            this.waveScope.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.waveScope.MaximumSize = new System.Drawing.Size(0, 120);
            this.waveScope.MinimumSize = new System.Drawing.Size(0, 120);
            this.waveScope.Name = "waveScope";
            this.waveScope.Size = new System.Drawing.Size(355, 120);
            this.waveScope.TabIndex = 0;
            this.waveScope.WSMouseDown += new System.EventHandler<WaveReader.WFMouseEventArgs>(this.waveScope_WSMouseDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labellastline, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelcurrent, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelnextline, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(355, 90);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labellastline
            // 
            this.labellastline.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labellastline.AutoSize = true;
            this.labellastline.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labellastline.ForeColor = System.Drawing.Color.DimGray;
            this.labellastline.Location = new System.Drawing.Point(182, 0);
            this.labellastline.Name = "labellastline";
            this.labellastline.Size = new System.Drawing.Size(0, 16);
            this.labellastline.TabIndex = 0;
            // 
            // labelcurrent
            // 
            this.labelcurrent.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelcurrent.AutoSize = true;
            this.labelcurrent.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelcurrent.ForeColor = System.Drawing.Color.Black;
            this.labelcurrent.Location = new System.Drawing.Point(182, 22);
            this.labelcurrent.Name = "labelcurrent";
            this.labelcurrent.Size = new System.Drawing.Size(0, 16);
            this.labelcurrent.TabIndex = 1;
            // 
            // labelnextline
            // 
            this.labelnextline.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelnextline.AutoSize = true;
            this.labelnextline.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelnextline.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelnextline.Location = new System.Drawing.Point(182, 44);
            this.labelnextline.Name = "labelnextline";
            this.labelnextline.Size = new System.Drawing.Size(0, 16);
            this.labelnextline.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "->";
            // 
            // fileToolStrip
            // 
            this.fileToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.fileToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnOpenAss,
            this.tsBtnOpenTxt,
            this.tsBtnOpenVideo,
            this.toolStripSeparator3,
            this.tsBtnSaveSub,
            this.toolStripSeparator6,
            this.tsBtnFFT});
            this.fileToolStrip.Location = new System.Drawing.Point(5, 0);
            this.fileToolStrip.Name = "fileToolStrip";
            this.fileToolStrip.Size = new System.Drawing.Size(139, 25);
            this.fileToolStrip.TabIndex = 0;
            // 
            // tsBtnOpenAss
            // 
            this.tsBtnOpenAss.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnOpenAss.Image = global::sgsubdotnet.Properties.Resources.openass;
            this.tsBtnOpenAss.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnOpenAss.Name = "tsBtnOpenAss";
            this.tsBtnOpenAss.Size = new System.Drawing.Size(23, 22);
            this.tsBtnOpenAss.Text = "Open ASS File";
            this.tsBtnOpenAss.Click += new System.EventHandler(this.OpenSub_Click);
            // 
            // tsBtnOpenTxt
            // 
            this.tsBtnOpenTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnOpenTxt.Image = global::sgsubdotnet.Properties.Resources.opentxt;
            this.tsBtnOpenTxt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnOpenTxt.Name = "tsBtnOpenTxt";
            this.tsBtnOpenTxt.Size = new System.Drawing.Size(23, 22);
            this.tsBtnOpenTxt.Text = "Open TXT File";
            this.tsBtnOpenTxt.Click += new System.EventHandler(this.OpenTxt_Click);
            // 
            // tsBtnOpenVideo
            // 
            this.tsBtnOpenVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnOpenVideo.Image = global::sgsubdotnet.Properties.Resources.openvideo;
            this.tsBtnOpenVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnOpenVideo.Name = "tsBtnOpenVideo";
            this.tsBtnOpenVideo.Size = new System.Drawing.Size(23, 22);
            this.tsBtnOpenVideo.Text = "Open Video";
            this.tsBtnOpenVideo.Click += new System.EventHandler(this.OpenVideo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnSaveSub
            // 
            this.tsBtnSaveSub.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSaveSub.Image = global::sgsubdotnet.Properties.Resources.save;
            this.tsBtnSaveSub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSaveSub.Name = "tsBtnSaveSub";
            this.tsBtnSaveSub.Size = new System.Drawing.Size(23, 22);
            this.tsBtnSaveSub.Text = "Save ASS";
            this.tsBtnSaveSub.Click += new System.EventHandler(this.SaveSub_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnFFT
            // 
            this.tsBtnFFT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnFFT.Image = global::sgsubdotnet.Properties.Resources.fft;
            this.tsBtnFFT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnFFT.Name = "tsBtnFFT";
            this.tsBtnFFT.Size = new System.Drawing.Size(23, 22);
            this.tsBtnFFT.Text = "Initialize FFT";
            this.tsBtnFFT.Click += new System.EventHandler(this.tsBtnFFT_Click);
            // 
            // smallSpliter
            // 
            this.smallSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smallSpliter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.smallSpliter.IsSplitterFixed = true;
            this.smallSpliter.Location = new System.Drawing.Point(0, 0);
            this.smallSpliter.Name = "smallSpliter";
            this.smallSpliter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // smallSpliter.Panel1
            // 
            this.smallSpliter.Panel1.Controls.Add(this.subLabel);
            // 
            // smallSpliter.Panel2
            // 
            this.smallSpliter.Panel2.Controls.Add(this.toolStripContainer1);
            this.smallSpliter.Size = new System.Drawing.Size(738, 300);
            this.smallSpliter.SplitterDistance = 53;
            this.smallSpliter.TabIndex = 1;
            // 
            // subLabel
            // 
            this.subLabel.AutoSize = true;
            this.subLabel.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.subLabel.Location = new System.Drawing.Point(50, 9);
            this.subLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.subLabel.Name = "subLabel";
            this.subLabel.Size = new System.Drawing.Size(256, 16);
            this.subLabel.TabIndex = 0;
            this.subLabel.Text = "Subtitle will be diplayed here.";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.subtitleGrid);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(706, 218);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.subToolStrip);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(738, 243);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // subtitleGrid
            // 
            this.subtitleGrid.AllowUserToAddRows = false;
            this.subtitleGrid.AllowUserToDeleteRows = false;
            this.subtitleGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.subtitleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.subtitleGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subtitleGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.subtitleGrid.Location = new System.Drawing.Point(0, 0);
            this.subtitleGrid.Name = "subtitleGrid";
            this.subtitleGrid.RowTemplate.Height = 23;
            this.subtitleGrid.Size = new System.Drawing.Size(706, 218);
            this.subtitleGrid.TabIndex = 0;
            this.subtitleGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.subtitleGrid_CellBeginEdit);
            this.subtitleGrid.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.subtitleGrid_CellStateChanged);
            this.subtitleGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.subtitleGrid_CellEndEdit);
            this.subtitleGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.subtitleGrid_KeyDown);
            this.subtitleGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.subtitleGrid_KeyUp);
            // 
            // subToolStrip
            // 
            this.subToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.subToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnPause,
            this.tsBtnPlay,
            this.tsBtnJumpto,
            this.toolStripSeparator1,
            this.tsBtnDuplicate,
            this.tsBtnDelItem,
            this.tsBtnInsAfter,
            this.toolStripSeparator2,
            this.tsBtnOLScan,
            this.tsBtnUndo});
            this.subToolStrip.Location = new System.Drawing.Point(0, 3);
            this.subToolStrip.Name = "subToolStrip";
            this.subToolStrip.Size = new System.Drawing.Size(32, 215);
            this.subToolStrip.TabIndex = 0;
            // 
            // tsBtnPause
            // 
            this.tsBtnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnPause.Image = global::sgsubdotnet.Properties.Resources.Pause;
            this.tsBtnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPause.Name = "tsBtnPause";
            this.tsBtnPause.Size = new System.Drawing.Size(30, 20);
            this.tsBtnPause.Text = "Pause";
            this.tsBtnPause.Click += new System.EventHandler(this.toolStripPause_Click);
            // 
            // tsBtnPlay
            // 
            this.tsBtnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnPlay.Image = global::sgsubdotnet.Properties.Resources.Run;
            this.tsBtnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnPlay.Name = "tsBtnPlay";
            this.tsBtnPlay.Size = new System.Drawing.Size(30, 20);
            this.tsBtnPlay.Text = "Play";
            this.tsBtnPlay.Click += new System.EventHandler(this.toolStripPlay_Click);
            // 
            // tsBtnJumpto
            // 
            this.tsBtnJumpto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnJumpto.Image = global::sgsubdotnet.Properties.Resources.jumpto;
            this.tsBtnJumpto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnJumpto.Name = "tsBtnJumpto";
            this.tsBtnJumpto.Size = new System.Drawing.Size(30, 20);
            this.tsBtnJumpto.Text = "Jump to";
            this.tsBtnJumpto.Click += new System.EventHandler(this.toolStripJumpto_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(30, 6);
            // 
            // tsBtnDuplicate
            // 
            this.tsBtnDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnDuplicate.Image = global::sgsubdotnet.Properties.Resources.copy;
            this.tsBtnDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDuplicate.Name = "tsBtnDuplicate";
            this.tsBtnDuplicate.Size = new System.Drawing.Size(30, 20);
            this.tsBtnDuplicate.Text = "Duplicate";
            this.tsBtnDuplicate.Click += new System.EventHandler(this.toolStripDuplicate_Click);
            // 
            // tsBtnDelItem
            // 
            this.tsBtnDelItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnDelItem.Image = global::sgsubdotnet.Properties.Resources.delete;
            this.tsBtnDelItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDelItem.Name = "tsBtnDelItem";
            this.tsBtnDelItem.Size = new System.Drawing.Size(30, 20);
            this.tsBtnDelItem.Text = "Delete";
            this.tsBtnDelItem.Click += new System.EventHandler(this.toolStripDeleteItem_Click);
            // 
            // tsBtnInsAfter
            // 
            this.tsBtnInsAfter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnInsAfter.Image = global::sgsubdotnet.Properties.Resources.insertafter;
            this.tsBtnInsAfter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnInsAfter.Name = "tsBtnInsAfter";
            this.tsBtnInsAfter.Size = new System.Drawing.Size(30, 20);
            this.tsBtnInsAfter.Text = "Insert after";
            this.tsBtnInsAfter.Click += new System.EventHandler(this.toolStripInsertAfter_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(30, 6);
            // 
            // tsBtnOLScan
            // 
            this.tsBtnOLScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnOLScan.Image = global::sgsubdotnet.Properties.Resources.olscan;
            this.tsBtnOLScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnOLScan.Name = "tsBtnOLScan";
            this.tsBtnOLScan.Size = new System.Drawing.Size(30, 20);
            this.tsBtnOLScan.Text = "Overlap Scan";
            this.tsBtnOLScan.Click += new System.EventHandler(this.tsBtnOLScan_Click);
            // 
            // tsBtnUndo
            // 
            this.tsBtnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnUndo.Image")));
            this.tsBtnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnUndo.Name = "tsBtnUndo";
            this.tsBtnUndo.Size = new System.Drawing.Size(23, 20);
            this.tsBtnUndo.Text = "toolStripButton1";
            this.tsBtnUndo.Click += new System.EventHandler(this.tsBtnUndo_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ConfigToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(742, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenSubToolStripMenuItem,
            this.OpenTxtToolStripMenuItem,
            this.OpenVideoToolStripMenuItem,
            this.toolStripSeparator4,
            this.SaveSubToolStripMenuItem,
            this.SaveAsSubToolStripMenuItem1,
            this.toolStripSeparator5,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.FileToolStripMenuItem.Text = "文件";
            // 
            // OpenSubToolStripMenuItem
            // 
            this.OpenSubToolStripMenuItem.Image = global::sgsubdotnet.Properties.Resources.openass;
            this.OpenSubToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenSubToolStripMenuItem.Name = "OpenSubToolStripMenuItem";
            this.OpenSubToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.OpenSubToolStripMenuItem.Text = "打开时间轴";
            this.OpenSubToolStripMenuItem.Click += new System.EventHandler(this.OpenSub_Click);
            // 
            // OpenTxtToolStripMenuItem
            // 
            this.OpenTxtToolStripMenuItem.Image = global::sgsubdotnet.Properties.Resources.opentxt;
            this.OpenTxtToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenTxtToolStripMenuItem.Name = "OpenTxtToolStripMenuItem";
            this.OpenTxtToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.OpenTxtToolStripMenuItem.Text = "打开翻译文本";
            this.OpenTxtToolStripMenuItem.Click += new System.EventHandler(this.OpenTxt_Click);
            // 
            // OpenVideoToolStripMenuItem
            // 
            this.OpenVideoToolStripMenuItem.Image = global::sgsubdotnet.Properties.Resources.openvideo;
            this.OpenVideoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenVideoToolStripMenuItem.Name = "OpenVideoToolStripMenuItem";
            this.OpenVideoToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.OpenVideoToolStripMenuItem.Text = "打开动画";
            this.OpenVideoToolStripMenuItem.Click += new System.EventHandler(this.OpenVideo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(139, 6);
            // 
            // SaveSubToolStripMenuItem
            // 
            this.SaveSubToolStripMenuItem.Image = global::sgsubdotnet.Properties.Resources.save;
            this.SaveSubToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveSubToolStripMenuItem.Name = "SaveSubToolStripMenuItem";
            this.SaveSubToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.SaveSubToolStripMenuItem.Text = "保存时间轴";
            this.SaveSubToolStripMenuItem.Click += new System.EventHandler(this.SaveSub_Click);
            // 
            // SaveAsSubToolStripMenuItem1
            // 
            this.SaveAsSubToolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveAsSubToolStripMenuItem1.Name = "SaveAsSubToolStripMenuItem1";
            this.SaveAsSubToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.SaveAsSubToolStripMenuItem1.Text = "另存为时间轴";
            this.SaveAsSubToolStripMenuItem1.Click += new System.EventHandler(this.SaveAsSub_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(139, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // ConfigToolStripMenuItem
            // 
            this.ConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KeyCfgToolStripMenuItem});
            this.ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            this.ConfigToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.ConfigToolStripMenuItem.Text = "设置";
            // 
            // KeyCfgToolStripMenuItem
            // 
            this.KeyCfgToolStripMenuItem.Name = "KeyCfgToolStripMenuItem";
            this.KeyCfgToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.KeyCfgToolStripMenuItem.Text = "按键设置";
            this.KeyCfgToolStripMenuItem.Click += new System.EventHandler(this.KeyCfgToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutSgsubToolStripMenuItem});
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.AboutToolStripMenuItem.Text = "关于";
            // 
            // AboutSgsubToolStripMenuItem
            // 
            this.AboutSgsubToolStripMenuItem.Name = "AboutSgsubToolStripMenuItem";
            this.AboutSgsubToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.AboutSgsubToolStripMenuItem.Text = "关于 SGSUB.Net";
            this.AboutSgsubToolStripMenuItem.Click += new System.EventHandler(this.AboutSgsubToolStripMenuItem_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 575);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(742, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "Status";
            // 
            // timer
            // 
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // mainpanel
            // 
            this.mainpanel.Controls.Add(this.hSpliter);
            this.mainpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainpanel.Location = new System.Drawing.Point(0, 24);
            this.mainpanel.Name = "mainpanel";
            this.mainpanel.Size = new System.Drawing.Size(742, 551);
            this.mainpanel.TabIndex = 3;
            // 
            // SGSMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 597);
            this.Controls.Add(this.mainpanel);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SGSMainForm";
            this.Text = "SGSUB.Net Reiner Rubin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SGSMainForm_FormClosing);
            this.hSpliter.Panel1.ResumeLayout(false);
            this.hSpliter.Panel2.ResumeLayout(false);
            this.hSpliter.ResumeLayout(false);
            this.vSpliter.Panel1.ResumeLayout(false);
            this.vSpliter.Panel2.ResumeLayout(false);
            this.vSpliter.ResumeLayout(false);
            this.toolStripContainer2.ContentPanel.ResumeLayout(false);
            this.toolStripContainer2.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer2.TopToolStripPanel.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.spliterSoundbar.Panel1.ResumeLayout(false);
            this.spliterSoundbar.Panel2.ResumeLayout(false);
            this.spliterSoundbar.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.fileToolStrip.ResumeLayout(false);
            this.fileToolStrip.PerformLayout();
            this.smallSpliter.Panel1.ResumeLayout(false);
            this.smallSpliter.Panel1.PerformLayout();
            this.smallSpliter.Panel2.ResumeLayout(false);
            this.smallSpliter.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subtitleGrid)).EndInit();
            this.subToolStrip.ResumeLayout(false);
            this.subToolStrip.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainpanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer hSpliter;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.SplitContainer vSpliter;
        private System.Windows.Forms.DataGridView subtitleGrid;
        private System.Windows.Forms.SplitContainer smallSpliter;
        private System.Windows.Forms.Label subLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenSubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveSubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsSubToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip subToolStrip;
        private System.Windows.Forms.ToolStripButton tsBtnPause;
        private System.Windows.Forms.ToolStripButton tsBtnPlay;
        private System.Windows.Forms.ToolStripButton tsBtnDuplicate;
        private System.Windows.Forms.ToolStripButton tsBtnDelItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnJumpto;
        private System.Windows.Forms.ToolStripButton tsBtnInsAfter;
        private System.Windows.Forms.ToolStripMenuItem OpenTxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KeyCfgToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.ToolStrip fileToolStrip;
        private System.Windows.Forms.ToolStripButton tsBtnSaveSub;
        private System.Windows.Forms.ToolStripButton tsBtnOpenAss;
        private System.Windows.Forms.ToolStripButton tsBtnOpenTxt;
        private System.Windows.Forms.ToolStripButton tsBtnOpenVideo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutSgsubToolStripMenuItem;
        private System.Windows.Forms.Panel mainpanel;
        private System.Windows.Forms.SplitContainer spliterSoundbar;
        private WaveReader.WaveScope waveScope;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labellastline;
        private System.Windows.Forms.Label labelcurrent;
        private System.Windows.Forms.Label labelnextline;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsBtnFFT;
        private System.Windows.Forms.ToolStripButton tsBtnOLScan;
        private VideoPlayer.DXVideoPlayer dxVideoPlayer;
        private System.Windows.Forms.ToolStripButton tsBtnUndo;
    }
}

