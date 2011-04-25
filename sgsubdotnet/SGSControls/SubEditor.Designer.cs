namespace SGSControls
{
    partial class SubEditor
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubEditor));
            this.subeditorSpliter = new System.Windows.Forms.SplitContainer();
            this.labelSub = new System.Windows.Forms.Label();
            this.subEditorToolStrip = new System.Windows.Forms.ToolStripContainer();
            this.dataGridSubtitles = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnPause = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPlay = new System.Windows.Forms.ToolStripButton();
            this.tsbtnJumpto = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnDuplicate = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbtnInsAfter = new System.Windows.Forms.ToolStripButton();
            this.tsbtnInsBefore = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnTimeLineScan = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.subeditorSpliter.Panel1.SuspendLayout();
            this.subeditorSpliter.Panel2.SuspendLayout();
            this.subeditorSpliter.SuspendLayout();
            this.subEditorToolStrip.ContentPanel.SuspendLayout();
            this.subEditorToolStrip.TopToolStripPanel.SuspendLayout();
            this.subEditorToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSubtitles)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // subeditorSpliter
            // 
            this.subeditorSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subeditorSpliter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.subeditorSpliter.IsSplitterFixed = true;
            this.subeditorSpliter.Location = new System.Drawing.Point(0, 0);
            this.subeditorSpliter.Name = "subeditorSpliter";
            this.subeditorSpliter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // subeditorSpliter.Panel1
            // 
            this.subeditorSpliter.Panel1.Controls.Add(this.labelSub);
            // 
            // subeditorSpliter.Panel2
            // 
            this.subeditorSpliter.Panel2.Controls.Add(this.subEditorToolStrip);
            this.subeditorSpliter.Size = new System.Drawing.Size(456, 414);
            this.subeditorSpliter.SplitterDistance = 44;
            this.subeditorSpliter.TabIndex = 0;
            // 
            // labelSub
            // 
            this.labelSub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSub.AutoSize = true;
            this.labelSub.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSub.Location = new System.Drawing.Point(13, 9);
            this.labelSub.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSub.Name = "labelSub";
            this.labelSub.Size = new System.Drawing.Size(90, 25);
            this.labelSub.TabIndex = 0;
            this.labelSub.Text = "label1";
            // 
            // subEditorToolStrip
            // 
            // 
            // subEditorToolStrip.ContentPanel
            // 
            this.subEditorToolStrip.ContentPanel.Controls.Add(this.dataGridSubtitles);
            this.subEditorToolStrip.ContentPanel.Size = new System.Drawing.Size(456, 341);
            this.subEditorToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subEditorToolStrip.Location = new System.Drawing.Point(0, 0);
            this.subEditorToolStrip.Name = "subEditorToolStrip";
            this.subEditorToolStrip.Size = new System.Drawing.Size(456, 366);
            this.subEditorToolStrip.TabIndex = 0;
            this.subEditorToolStrip.Text = "toolStripContainer1";
            // 
            // subEditorToolStrip.TopToolStripPanel
            // 
            this.subEditorToolStrip.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // dataGridSubtitles
            // 
            this.dataGridSubtitles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSubtitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridSubtitles.Location = new System.Drawing.Point(0, 0);
            this.dataGridSubtitles.Name = "dataGridSubtitles";
            this.dataGridSubtitles.RowTemplate.Height = 23;
            this.dataGridSubtitles.Size = new System.Drawing.Size(456, 341);
            this.dataGridSubtitles.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnPause,
            this.tsbtnPlay,
            this.tsbtnJumpto,
            this.toolStripSeparator1,
            this.tsbtnDuplicate,
            this.tsbtnDelete,
            this.tsbtnInsAfter,
            this.tsbtnInsBefore,
            this.toolStripSeparator2,
            this.tsbtnTimeLineScan,
            this.tsbtnUndo,
            this.toolStripButton10,
            this.toolStripButton11,
            this.toolStripButton12});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(331, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // tsbtnPause
            // 
            this.tsbtnPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPause.Image = global::SGSControls.Properties.Resources.pause;
            this.tsbtnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPause.Name = "tsbtnPause";
            this.tsbtnPause.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPause.Text = "暂停";
            // 
            // tsbtnPlay
            // 
            this.tsbtnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPlay.Image = global::SGSControls.Properties.Resources.run;
            this.tsbtnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPlay.Name = "tsbtnPlay";
            this.tsbtnPlay.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPlay.Text = "播放";
            // 
            // tsbtnJumpto
            // 
            this.tsbtnJumpto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnJumpto.Image = global::SGSControls.Properties.Resources.jumpto;
            this.tsbtnJumpto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnJumpto.Name = "tsbtnJumpto";
            this.tsbtnJumpto.Size = new System.Drawing.Size(23, 22);
            this.tsbtnJumpto.Text = "跳至当前行";
            this.tsbtnJumpto.Click += new System.EventHandler(this.tsbtnJumpto_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnDuplicate
            // 
            this.tsbtnDuplicate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDuplicate.Image = global::SGSControls.Properties.Resources.copy;
            this.tsbtnDuplicate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDuplicate.Name = "tsbtnDuplicate";
            this.tsbtnDuplicate.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDuplicate.Text = "toolStripButton4";
            this.tsbtnDuplicate.Click += new System.EventHandler(this.tsbtnDuplicate_Click);
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = global::SGSControls.Properties.Resources.delete;
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDelete.Text = "toolStripButton5";
            this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
            // 
            // tsbtnInsAfter
            // 
            this.tsbtnInsAfter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnInsAfter.Image = global::SGSControls.Properties.Resources.insertafter;
            this.tsbtnInsAfter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnInsAfter.Name = "tsbtnInsAfter";
            this.tsbtnInsAfter.Size = new System.Drawing.Size(23, 22);
            this.tsbtnInsAfter.Text = "toolStripButton6";
            this.tsbtnInsAfter.Click += new System.EventHandler(this.tsbtnInsAfter_Click);
            // 
            // tsbtnInsBefore
            // 
            this.tsbtnInsBefore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnInsBefore.Image = global::SGSControls.Properties.Resources.insertbefore;
            this.tsbtnInsBefore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnInsBefore.Name = "tsbtnInsBefore";
            this.tsbtnInsBefore.Size = new System.Drawing.Size(23, 22);
            this.tsbtnInsBefore.Text = "toolStripButton7";
            this.tsbtnInsBefore.Click += new System.EventHandler(this.tsbtnInsBefore_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnTimeLineScan
            // 
            this.tsbtnTimeLineScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnTimeLineScan.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnTimeLineScan.Image")));
            this.tsbtnTimeLineScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTimeLineScan.Name = "tsbtnTimeLineScan";
            this.tsbtnTimeLineScan.Size = new System.Drawing.Size(23, 22);
            this.tsbtnTimeLineScan.Text = "toolStripButton8";
            // 
            // tsbtnUndo
            // 
            this.tsbtnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUndo.Image")));
            this.tsbtnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUndo.Name = "tsbtnUndo";
            this.tsbtnUndo.Size = new System.Drawing.Size(23, 22);
            this.tsbtnUndo.Text = "toolStripButton9";
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton10.Text = "toolStripButton10";
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton11.Text = "toolStripButton11";
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton12.Image")));
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton12.Text = "toolStripButton12";
            // 
            // SubEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.subeditorSpliter);
            this.Name = "SubEditor";
            this.Size = new System.Drawing.Size(456, 414);
            this.subeditorSpliter.Panel1.ResumeLayout(false);
            this.subeditorSpliter.Panel1.PerformLayout();
            this.subeditorSpliter.Panel2.ResumeLayout(false);
            this.subeditorSpliter.ResumeLayout(false);
            this.subEditorToolStrip.ContentPanel.ResumeLayout(false);
            this.subEditorToolStrip.TopToolStripPanel.ResumeLayout(false);
            this.subEditorToolStrip.TopToolStripPanel.PerformLayout();
            this.subEditorToolStrip.ResumeLayout(false);
            this.subEditorToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSubtitles)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer subeditorSpliter;
        private System.Windows.Forms.Label labelSub;
        private System.Windows.Forms.ToolStripContainer subEditorToolStrip;
        private System.Windows.Forms.DataGridView dataGridSubtitles;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnPause;
        private System.Windows.Forms.ToolStripButton tsbtnPlay;
        private System.Windows.Forms.ToolStripButton tsbtnJumpto;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnDuplicate;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.ToolStripButton tsbtnInsAfter;
        private System.Windows.Forms.ToolStripButton tsbtnInsBefore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnTimeLineScan;
        private System.Windows.Forms.ToolStripButton tsbtnUndo;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
    }
}
