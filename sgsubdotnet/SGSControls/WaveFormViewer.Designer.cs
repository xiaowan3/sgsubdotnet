﻿namespace SGS.Controls
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
            this.waveScope = new WaveReader.WaveScope();
            this.labelLastLine = new System.Windows.Forms.Label();
            this.labelThisLine = new System.Windows.Forms.Label();
            this.labelNextLine = new System.Windows.Forms.Label();
            this.labelArrow = new System.Windows.Forms.Label();
            this.labelLastDuration = new System.Windows.Forms.Label();
            this.labelThisDuration = new System.Windows.Forms.Label();
            this.labelNextDuration = new System.Windows.Forms.Label();
            this.tsFile = new System.Windows.Forms.ToolStrip();
            this.tsbtnOpenAss = new System.Windows.Forms.ToolStripButton();
            this.tsbtnOpenTxt = new System.Windows.Forms.ToolStripButton();
            this.tsbtnOpenMedia = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnSaveASS = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnFFT = new System.Windows.Forms.ToolStripButton();
            this.toolStripWaveFormViewer.ContentPanel.SuspendLayout();
            this.toolStripWaveFormViewer.TopToolStripPanel.SuspendLayout();
            this.toolStripWaveFormViewer.SuspendLayout();
            this.waveViewerLayout.SuspendLayout();
            this.tsFile.SuspendLayout();
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
            // toolStripWaveFormViewer.TopToolStripPanel
            // 
            this.toolStripWaveFormViewer.TopToolStripPanel.Controls.Add(this.tsFile);
            // 
            // waveViewerLayout
            // 
            this.waveViewerLayout.ColumnCount = 3;
            this.waveViewerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.waveViewerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.waveViewerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.waveViewerLayout.Controls.Add(this.waveScope, 0, 0);
            this.waveViewerLayout.Controls.Add(this.labelLastLine, 1, 1);
            this.waveViewerLayout.Controls.Add(this.labelThisLine, 1, 2);
            this.waveViewerLayout.Controls.Add(this.labelNextLine, 1, 3);
            this.waveViewerLayout.Controls.Add(this.labelArrow, 0, 2);
            this.waveViewerLayout.Controls.Add(this.labelLastDuration, 2, 1);
            this.waveViewerLayout.Controls.Add(this.labelThisDuration, 2, 2);
            this.waveViewerLayout.Controls.Add(this.labelNextDuration, 2, 3);
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
            // waveScope
            // 
            this.waveViewerLayout.SetColumnSpan(this.waveScope, 3);
            this.waveScope.CurrentPosition = 0;
            this.waveScope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waveScope.Location = new System.Drawing.Point(2, 2);
            this.waveScope.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.waveScope.MaximumSize = new System.Drawing.Size(1024, 120);
            this.waveScope.MinimumSize = new System.Drawing.Size(0, 120);
            this.waveScope.Name = "waveScope";
            this.waveScope.Size = new System.Drawing.Size(510, 120);
            this.waveScope.TabIndex = 0;
            // 
            // labelLastLine
            // 
            this.labelLastLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelLastLine.AutoSize = true;
            this.labelLastLine.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLastLine.ForeColor = System.Drawing.Color.DimGray;
            this.labelLastLine.Location = new System.Drawing.Point(201, 125);
            this.labelLastLine.Name = "labelLastLine";
            this.labelLastLine.Size = new System.Drawing.Size(56, 16);
            this.labelLastLine.TabIndex = 1;
            this.labelLastLine.Text = "label1";
            // 
            // labelThisLine
            // 
            this.labelThisLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelThisLine.AutoSize = true;
            this.labelThisLine.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelThisLine.Location = new System.Drawing.Point(201, 147);
            this.labelThisLine.Name = "labelThisLine";
            this.labelThisLine.Size = new System.Drawing.Size(56, 16);
            this.labelThisLine.TabIndex = 2;
            this.labelThisLine.Text = "label2";
            // 
            // labelNextLine
            // 
            this.labelNextLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelNextLine.AutoSize = true;
            this.labelNextLine.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelNextLine.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelNextLine.Location = new System.Drawing.Point(201, 169);
            this.labelNextLine.Name = "labelNextLine";
            this.labelNextLine.Size = new System.Drawing.Size(56, 16);
            this.labelNextLine.TabIndex = 3;
            this.labelNextLine.Text = "label3";
            // 
            // labelArrow
            // 
            this.labelArrow.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelArrow.AutoSize = true;
            this.labelArrow.Location = new System.Drawing.Point(6, 151);
            this.labelArrow.Name = "labelArrow";
            this.labelArrow.Size = new System.Drawing.Size(21, 13);
            this.labelArrow.TabIndex = 4;
            this.labelArrow.Text = "->";
            // 
            // labelLastDuration
            // 
            this.labelLastDuration.AutoSize = true;
            this.labelLastDuration.Location = new System.Drawing.Point(432, 125);
            this.labelLastDuration.Name = "labelLastDuration";
            this.labelLastDuration.Size = new System.Drawing.Size(77, 13);
            this.labelLastDuration.TabIndex = 5;
            this.labelLastDuration.Text = "-:--:--.--";
            // 
            // labelThisDuration
            // 
            this.labelThisDuration.AutoSize = true;
            this.labelThisDuration.Location = new System.Drawing.Point(432, 147);
            this.labelThisDuration.Name = "labelThisDuration";
            this.labelThisDuration.Size = new System.Drawing.Size(77, 13);
            this.labelThisDuration.TabIndex = 6;
            this.labelThisDuration.Text = "-:--:--.--";
            // 
            // labelNextDuration
            // 
            this.labelNextDuration.AutoSize = true;
            this.labelNextDuration.Location = new System.Drawing.Point(432, 169);
            this.labelNextDuration.Name = "labelNextDuration";
            this.labelNextDuration.Size = new System.Drawing.Size(77, 13);
            this.labelNextDuration.TabIndex = 7;
            this.labelNextDuration.Text = "-:--:--.--";
            // 
            // tsFile
            // 
            this.tsFile.Dock = System.Windows.Forms.DockStyle.None;
            this.tsFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnOpenAss,
            this.tsbtnOpenTxt,
            this.tsbtnOpenMedia,
            this.toolStripSeparator1,
            this.tsbtnSaveASS,
            this.toolStripSeparator2,
            this.tsbtnFFT});
            this.tsFile.Location = new System.Drawing.Point(3, 0);
            this.tsFile.Name = "tsFile";
            this.tsFile.Size = new System.Drawing.Size(139, 25);
            this.tsFile.TabIndex = 0;
            // 
            // tsbtnOpenAss
            // 
            this.tsbtnOpenAss.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOpenAss.Image = global::SGS.Controls.Properties.Resources.openass;
            this.tsbtnOpenAss.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOpenAss.Name = "tsbtnOpenAss";
            this.tsbtnOpenAss.Size = new System.Drawing.Size(23, 22);
            this.tsbtnOpenAss.Text = "打开时间轴";
            // 
            // tsbtnOpenTxt
            // 
            this.tsbtnOpenTxt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOpenTxt.Image = global::SGS.Controls.Properties.Resources.opentxt;
            this.tsbtnOpenTxt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOpenTxt.Name = "tsbtnOpenTxt";
            this.tsbtnOpenTxt.Size = new System.Drawing.Size(23, 22);
            this.tsbtnOpenTxt.Text = "打开翻译文本";
            // 
            // tsbtnOpenMedia
            // 
            this.tsbtnOpenMedia.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnOpenMedia.Image = global::SGS.Controls.Properties.Resources.openvideo;
            this.tsbtnOpenMedia.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOpenMedia.Name = "tsbtnOpenMedia";
            this.tsbtnOpenMedia.Size = new System.Drawing.Size(23, 22);
            this.tsbtnOpenMedia.Text = "打开视频";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnSaveASS
            // 
            this.tsbtnSaveASS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSaveASS.Image = global::SGS.Controls.Properties.Resources.save;
            this.tsbtnSaveASS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSaveASS.Name = "tsbtnSaveASS";
            this.tsbtnSaveASS.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSaveASS.Text = "保存时间轴";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnFFT
            // 
            this.tsbtnFFT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFFT.Image = global::SGS.Controls.Properties.Resources.fft;
            this.tsbtnFFT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFFT.Name = "tsbtnFFT";
            this.tsbtnFFT.Size = new System.Drawing.Size(23, 22);
            this.tsbtnFFT.Text = "读取频谱";
            this.tsbtnFFT.Click += new System.EventHandler(this.tsbtnFFT_Click);
            // 
            // WaveFormViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.toolStripWaveFormViewer);
            this.Font = new System.Drawing.Font("SimSun", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.Name = "WaveFormViewer";
            this.Size = new System.Drawing.Size(514, 245);
            this.toolStripWaveFormViewer.ContentPanel.ResumeLayout(false);
            this.toolStripWaveFormViewer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripWaveFormViewer.TopToolStripPanel.PerformLayout();
            this.toolStripWaveFormViewer.ResumeLayout(false);
            this.toolStripWaveFormViewer.PerformLayout();
            this.waveViewerLayout.ResumeLayout(false);
            this.waveViewerLayout.PerformLayout();
            this.tsFile.ResumeLayout(false);
            this.tsFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripWaveFormViewer;
        private System.Windows.Forms.TableLayoutPanel waveViewerLayout;
        private WaveReader.WaveScope waveScope;
        private System.Windows.Forms.ToolStrip tsFile;
        private System.Windows.Forms.ToolStripButton tsbtnOpenAss;
        private System.Windows.Forms.ToolStripButton tsbtnOpenTxt;
        private System.Windows.Forms.ToolStripButton tsbtnOpenMedia;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtnSaveASS;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnFFT;
        private System.Windows.Forms.Label labelLastLine;
        private System.Windows.Forms.Label labelThisLine;
        private System.Windows.Forms.Label labelNextLine;
        private System.Windows.Forms.Label labelArrow;
        private System.Windows.Forms.Label labelLastDuration;
        private System.Windows.Forms.Label labelThisDuration;
        private System.Windows.Forms.Label labelNextDuration;
    }
}
