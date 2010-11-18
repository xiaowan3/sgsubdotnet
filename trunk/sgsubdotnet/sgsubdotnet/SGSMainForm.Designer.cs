﻿namespace sgsubdotnet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SGSMainForm));
            this.hSpliter = new System.Windows.Forms.SplitContainer();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.vSpliter = new System.Windows.Forms.SplitContainer();
            this.axWMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.ssubtitleGrid = new System.Windows.Forms.DataGridView();
            this.smallSpliter = new System.Windows.Forms.SplitContainer();
            this.hSpliter.Panel1.SuspendLayout();
            this.hSpliter.Panel2.SuspendLayout();
            this.hSpliter.SuspendLayout();
            this.vSpliter.Panel1.SuspendLayout();
            this.vSpliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ssubtitleGrid)).BeginInit();
            this.smallSpliter.Panel2.SuspendLayout();
            this.smallSpliter.SuspendLayout();
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
            this.hSpliter.Size = new System.Drawing.Size(734, 559);
            this.hSpliter.SplitterDistance = 328;
            this.hSpliter.TabIndex = 0;
            // 
            // mainMenu
            // 
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(734, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 561);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(734, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "Status";
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
            this.vSpliter.Size = new System.Drawing.Size(734, 328);
            this.vSpliter.SplitterDistance = 382;
            this.vSpliter.TabIndex = 0;
            // 
            // axWMP
            // 
            this.axWMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWMP.Enabled = true;
            this.axWMP.Location = new System.Drawing.Point(0, 0);
            this.axWMP.Name = "axWMP";
            this.axWMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWMP.OcxState")));
            this.axWMP.Size = new System.Drawing.Size(382, 328);
            this.axWMP.TabIndex = 0;
            // 
            // ssubtitleGrid
            // 
            this.ssubtitleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ssubtitleGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssubtitleGrid.Location = new System.Drawing.Point(0, 0);
            this.ssubtitleGrid.Name = "ssubtitleGrid";
            this.ssubtitleGrid.RowTemplate.Height = 23;
            this.ssubtitleGrid.Size = new System.Drawing.Size(734, 178);
            this.ssubtitleGrid.TabIndex = 0;
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
            // smallSpliter.Panel2
            // 
            this.smallSpliter.Panel2.Controls.Add(this.ssubtitleGrid);
            this.smallSpliter.Size = new System.Drawing.Size(734, 227);
            this.smallSpliter.SplitterDistance = 45;
            this.smallSpliter.TabIndex = 1;
            // 
            // SGSMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 583);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.hSpliter);
            this.Controls.Add(this.mainMenu);
            this.Name = "SGSMainForm";
            this.Text = "SGSUB.Net";
            this.hSpliter.Panel1.ResumeLayout(false);
            this.hSpliter.Panel2.ResumeLayout(false);
            this.hSpliter.ResumeLayout(false);
            this.vSpliter.Panel1.ResumeLayout(false);
            this.vSpliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ssubtitleGrid)).EndInit();
            this.smallSpliter.Panel2.ResumeLayout(false);
            this.smallSpliter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer hSpliter;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.SplitContainer vSpliter;
        private AxWMPLib.AxWindowsMediaPlayer axWMP;
        private System.Windows.Forms.DataGridView ssubtitleGrid;
        private System.Windows.Forms.SplitContainer smallSpliter;
    }
}
