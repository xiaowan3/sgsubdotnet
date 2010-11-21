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
            this.axWMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.fileToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.smallSpliter = new System.Windows.Forms.SplitContainer();
            this.subLabel = new System.Windows.Forms.Label();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.subtitleGrid = new System.Windows.Forms.DataGridView();
            this.subToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveAsSubToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeyCfgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.AboutSgsubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stripBtnOpenAss = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.stripBtnSaveSub = new System.Windows.Forms.ToolStripButton();
            this.toolStripPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripPlay = new System.Windows.Forms.ToolStripButton();
            this.toolStripDuplicate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripInsertAfter = new System.Windows.Forms.ToolStripButton();
            this.toolStripJumpto = new System.Windows.Forms.ToolStripButton();
            this.OpenSubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveSubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hSpliter.Panel1.SuspendLayout();
            this.hSpliter.Panel2.SuspendLayout();
            this.hSpliter.SuspendLayout();
            this.vSpliter.Panel1.SuspendLayout();
            this.vSpliter.Panel2.SuspendLayout();
            this.vSpliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWMP)).BeginInit();
            this.toolStripContainer2.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
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
            this.SuspendLayout();
            // 
            // hSpliter
            // 
            this.hSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hSpliter.Location = new System.Drawing.Point(0, 24);
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
            this.hSpliter.Size = new System.Drawing.Size(734, 504);
            this.hSpliter.SplitterDistance = 227;
            this.hSpliter.TabIndex = 0;
            // 
            // vSpliter
            // 
            this.vSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vSpliter.Location = new System.Drawing.Point(0, 0);
            this.vSpliter.Name = "vSpliter";
            // 
            // vSpliter.Panel1
            // 
            this.vSpliter.Panel1.Controls.Add(this.axWMP);
            // 
            // vSpliter.Panel2
            // 
            this.vSpliter.Panel2.Controls.Add(this.toolStripContainer2);
            this.vSpliter.Size = new System.Drawing.Size(734, 227);
            this.vSpliter.SplitterDistance = 380;
            this.vSpliter.TabIndex = 0;
            // 
            // axWMP
            // 
            this.axWMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWMP.Enabled = true;
            this.axWMP.Location = new System.Drawing.Point(0, 0);
            this.axWMP.Name = "axWMP";
            this.axWMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWMP.OcxState")));
            this.axWMP.Size = new System.Drawing.Size(380, 227);
            this.axWMP.TabIndex = 0;
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(350, 202);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(350, 227);
            this.toolStripContainer2.TabIndex = 0;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // toolStripContainer2.TopToolStripPanel
            // 
            this.toolStripContainer2.TopToolStripPanel.Controls.Add(this.fileToolStrip);
            // 
            // fileToolStrip
            // 
            this.fileToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.fileToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripBtnOpenAss,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator3,
            this.stripBtnSaveSub});
            this.fileToolStrip.Location = new System.Drawing.Point(5, 0);
            this.fileToolStrip.Name = "fileToolStrip";
            this.fileToolStrip.Size = new System.Drawing.Size(110, 25);
            this.fileToolStrip.TabIndex = 0;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            this.smallSpliter.Size = new System.Drawing.Size(734, 273);
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
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(710, 191);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.subToolStrip);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(734, 216);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // subtitleGrid
            // 
            this.subtitleGrid.AllowUserToAddRows = false;
            this.subtitleGrid.AllowUserToDeleteRows = false;
            this.subtitleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.subtitleGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subtitleGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.subtitleGrid.Location = new System.Drawing.Point(0, 0);
            this.subtitleGrid.MultiSelect = false;
            this.subtitleGrid.Name = "subtitleGrid";
            this.subtitleGrid.RowTemplate.Height = 23;
            this.subtitleGrid.Size = new System.Drawing.Size(710, 191);
            this.subtitleGrid.TabIndex = 0;
            this.subtitleGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.subtitleGrid_CellBeginEdit);
            this.subtitleGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.subtitleGrid_CellEndEdit);
            this.subtitleGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.subtitleGrid_KeyDown);
            this.subtitleGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.subtitleGrid_KeyUp);
            // 
            // subToolStrip
            // 
            this.subToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.subToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPause,
            this.toolStripPlay,
            this.toolStripSeparator1,
            this.toolStripDuplicate,
            this.toolStripButton1,
            this.toolStripInsertAfter,
            this.toolStripSeparator2,
            this.toolStripJumpto});
            this.subToolStrip.Location = new System.Drawing.Point(0, 3);
            this.subToolStrip.Name = "subToolStrip";
            this.subToolStrip.Size = new System.Drawing.Size(24, 161);
            this.subToolStrip.TabIndex = 0;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(22, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(22, 6);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(734, 24);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(139, 6);
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
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KeyCfgToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.设置ToolStripMenuItem.Text = "设置";
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
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 506);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(734, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "Status";
            // 
            // timer
            // 
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // AboutSgsubToolStripMenuItem
            // 
            this.AboutSgsubToolStripMenuItem.Name = "AboutSgsubToolStripMenuItem";
            this.AboutSgsubToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.AboutSgsubToolStripMenuItem.Text = "关于 SGSUB.Net";
            this.AboutSgsubToolStripMenuItem.Click += new System.EventHandler(this.AboutSgsubToolStripMenuItem_Click);
            // 
            // stripBtnOpenAss
            // 
            this.stripBtnOpenAss.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stripBtnOpenAss.Image = global::sgsubdotnet.Properties.Resources.openass;
            this.stripBtnOpenAss.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stripBtnOpenAss.Name = "stripBtnOpenAss";
            this.stripBtnOpenAss.Size = new System.Drawing.Size(23, 22);
            this.stripBtnOpenAss.Text = "Save Ass as";
            this.stripBtnOpenAss.Click += new System.EventHandler(this.OpenSub_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::sgsubdotnet.Properties.Resources.opentxt;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripOpenTxt";
            this.toolStripButton2.Click += new System.EventHandler(this.OpenTxt_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::sgsubdotnet.Properties.Resources.openvideo;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripOpenVideo";
            this.toolStripButton3.Click += new System.EventHandler(this.OpenVideo_Click);
            // 
            // stripBtnSaveSub
            // 
            this.stripBtnSaveSub.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stripBtnSaveSub.Image = global::sgsubdotnet.Properties.Resources.save;
            this.stripBtnSaveSub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stripBtnSaveSub.Name = "stripBtnSaveSub";
            this.stripBtnSaveSub.Size = new System.Drawing.Size(23, 22);
            this.stripBtnSaveSub.Text = "Save Ass";
            this.stripBtnSaveSub.Click += new System.EventHandler(this.SaveSub_Click);
            // 
            // toolStripPause
            // 
            this.toolStripPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPause.Image = global::sgsubdotnet.Properties.Resources.Pause;
            this.toolStripPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPause.Name = "toolStripPause";
            this.toolStripPause.Size = new System.Drawing.Size(22, 20);
            this.toolStripPause.Text = "Pause";
            this.toolStripPause.Click += new System.EventHandler(this.toolStripPause_Click);
            // 
            // toolStripPlay
            // 
            this.toolStripPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPlay.Image = global::sgsubdotnet.Properties.Resources.Run;
            this.toolStripPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPlay.Name = "toolStripPlay";
            this.toolStripPlay.Size = new System.Drawing.Size(22, 20);
            this.toolStripPlay.Text = "Play";
            this.toolStripPlay.Click += new System.EventHandler(this.toolStripPlay_Click);
            // 
            // toolStripDuplicate
            // 
            this.toolStripDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDuplicate.Image = global::sgsubdotnet.Properties.Resources.copy;
            this.toolStripDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDuplicate.Name = "toolStripDuplicate";
            this.toolStripDuplicate.Size = new System.Drawing.Size(22, 20);
            this.toolStripDuplicate.Text = "Duplicate";
            this.toolStripDuplicate.Click += new System.EventHandler(this.toolStripDuplicate_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::sgsubdotnet.Properties.Resources.delete;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(22, 20);
            this.toolStripButton1.Text = "Delete";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripInsertAfter
            // 
            this.toolStripInsertAfter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripInsertAfter.Image = global::sgsubdotnet.Properties.Resources.insertafter;
            this.toolStripInsertAfter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripInsertAfter.Name = "toolStripInsertAfter";
            this.toolStripInsertAfter.Size = new System.Drawing.Size(22, 20);
            this.toolStripInsertAfter.Text = "Insert after";
            this.toolStripInsertAfter.Click += new System.EventHandler(this.toolStripInsertAfter_Click);
            // 
            // toolStripJumpto
            // 
            this.toolStripJumpto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripJumpto.Image = global::sgsubdotnet.Properties.Resources.jumpto;
            this.toolStripJumpto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripJumpto.Name = "toolStripJumpto";
            this.toolStripJumpto.Size = new System.Drawing.Size(22, 20);
            this.toolStripJumpto.Text = "Jump to";
            this.toolStripJumpto.Click += new System.EventHandler(this.toolStripJumpto_Click);
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
            // SaveSubToolStripMenuItem
            // 
            this.SaveSubToolStripMenuItem.Image = global::sgsubdotnet.Properties.Resources.save;
            this.SaveSubToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveSubToolStripMenuItem.Name = "SaveSubToolStripMenuItem";
            this.SaveSubToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.SaveSubToolStripMenuItem.Text = "保存时间轴";
            this.SaveSubToolStripMenuItem.Click += new System.EventHandler(this.SaveSub_Click);
            // 
            // SGSMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 528);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.hSpliter);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SGSMainForm";
            this.Text = "SGSUB.Net Reiner Rubin Beta";
            this.hSpliter.Panel1.ResumeLayout(false);
            this.hSpliter.Panel2.ResumeLayout(false);
            this.hSpliter.ResumeLayout(false);
            this.vSpliter.Panel1.ResumeLayout(false);
            this.vSpliter.Panel2.ResumeLayout(false);
            this.vSpliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWMP)).EndInit();
            this.toolStripContainer2.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer2.TopToolStripPanel.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer hSpliter;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.SplitContainer vSpliter;
        private AxWMPLib.AxWindowsMediaPlayer axWMP;
        private System.Windows.Forms.DataGridView subtitleGrid;
        private System.Windows.Forms.SplitContainer smallSpliter;
        private System.Windows.Forms.Label subLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenSubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveSubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsSubToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip subToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripPause;
        private System.Windows.Forms.ToolStripButton toolStripPlay;
        private System.Windows.Forms.ToolStripButton toolStripDuplicate;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripJumpto;
        private System.Windows.Forms.ToolStripButton toolStripInsertAfter;
        private System.Windows.Forms.ToolStripMenuItem OpenTxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KeyCfgToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.ToolStrip fileToolStrip;
        private System.Windows.Forms.ToolStripButton stripBtnSaveSub;
        private System.Windows.Forms.ToolStripButton stripBtnOpenAss;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutSgsubToolStripMenuItem;
    }
}

