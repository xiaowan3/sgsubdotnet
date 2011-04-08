namespace SGSControls
{
    partial class WaveFormViewer
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
            this.toolStripWaveFormViewer = new System.Windows.Forms.ToolStripContainer();
            this.waveViewerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.waveScope1 = new WaveReader.WaveScope();
            this.toolStripWaveFormViewer.ContentPanel.SuspendLayout();
            this.toolStripWaveFormViewer.SuspendLayout();
            this.waveViewerLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripWaveFormViewer
            // 
            // 
            // toolStripWaveFormViewer.ContentPanel
            // 
            this.toolStripWaveFormViewer.ContentPanel.Controls.Add(this.waveViewerLayout);
            this.toolStripWaveFormViewer.ContentPanel.Size = new System.Drawing.Size(514, 220);
            this.toolStripWaveFormViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripWaveFormViewer.Location = new System.Drawing.Point(0, 0);
            this.toolStripWaveFormViewer.Name = "toolStripWaveFormViewer";
            this.toolStripWaveFormViewer.Size = new System.Drawing.Size(514, 245);
            this.toolStripWaveFormViewer.TabIndex = 0;
            this.toolStripWaveFormViewer.Text = "toolStripContainer1";
            // 
            // waveViewerLayout
            // 
            this.waveViewerLayout.ColumnCount = 3;
            this.waveViewerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.waveViewerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.waveViewerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.waveViewerLayout.Controls.Add(this.waveScope1, 0, 0);
            this.waveViewerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waveViewerLayout.Location = new System.Drawing.Point(0, 0);
            this.waveViewerLayout.Name = "waveViewerLayout";
            this.waveViewerLayout.RowCount = 5;
            this.waveViewerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.waveViewerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.waveViewerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.waveViewerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.waveViewerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.waveViewerLayout.Size = new System.Drawing.Size(514, 220);
            this.waveViewerLayout.TabIndex = 0;
            // 
            // waveScope1
            // 
            this.waveViewerLayout.SetColumnSpan(this.waveScope1, 3);
            this.waveScope1.CurrentPosition = 0;
            this.waveScope1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waveScope1.Location = new System.Drawing.Point(2, 2);
            this.waveScope1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.waveScope1.MaximumSize = new System.Drawing.Size(750, 120);
            this.waveScope1.MinimumSize = new System.Drawing.Size(0, 120);
            this.waveScope1.Name = "waveScope1";
            this.waveScope1.Size = new System.Drawing.Size(510, 120);
            this.waveScope1.TabIndex = 0;
            // 
            // WaveFormViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.toolStripWaveFormViewer);
            this.Name = "WaveFormViewer";
            this.Size = new System.Drawing.Size(514, 245);
            this.toolStripWaveFormViewer.ContentPanel.ResumeLayout(false);
            this.toolStripWaveFormViewer.ResumeLayout(false);
            this.toolStripWaveFormViewer.PerformLayout();
            this.waveViewerLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripWaveFormViewer;
        private System.Windows.Forms.TableLayoutPanel waveViewerLayout;
        private WaveReader.WaveScope waveScope1;
    }
}
