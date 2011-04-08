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
            this.subeditorSpliter = new System.Windows.Forms.SplitContainer();
            this.labelSub = new System.Windows.Forms.Label();
            this.subEditorToolStrip = new System.Windows.Forms.ToolStripContainer();
            this.dataGridSubtitles = new System.Windows.Forms.DataGridView();
            this.subeditorSpliter.Panel1.SuspendLayout();
            this.subeditorSpliter.Panel2.SuspendLayout();
            this.subeditorSpliter.SuspendLayout();
            this.subEditorToolStrip.ContentPanel.SuspendLayout();
            this.subEditorToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSubtitles)).BeginInit();
            this.SuspendLayout();
            // 
            // subeditorSpliter
            // 
            this.subeditorSpliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subeditorSpliter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
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
            this.labelSub.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSub.AutoSize = true;
            this.labelSub.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSub.Location = new System.Drawing.Point(21, 12);
            this.labelSub.Name = "labelSub";
            this.labelSub.Size = new System.Drawing.Size(69, 20);
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
            this.subEditorToolStrip.ResumeLayout(false);
            this.subEditorToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSubtitles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer subeditorSpliter;
        private System.Windows.Forms.Label labelSub;
        private System.Windows.Forms.ToolStripContainer subEditorToolStrip;
        private System.Windows.Forms.DataGridView dataGridSubtitles;
    }
}
