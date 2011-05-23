namespace sgsubtr
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.layoutConfig = new System.Windows.Forms.TabPage();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureLayout = new System.Windows.Forms.PictureBox();
            this.listLayout = new System.Windows.Forms.ListBox();
            this.parameterConfig = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.numAutosavePeriod = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numAutosaveLifeTime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1.SuspendLayout();
            this.layoutConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLayout)).BeginInit();
            this.parameterConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutosavePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAutosaveLifeTime)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.layoutConfig);
            this.tabControl1.Controls.Add(this.parameterConfig);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(633, 378);
            this.tabControl1.TabIndex = 0;
            // 
            // layoutConfig
            // 
            this.layoutConfig.BackColor = System.Drawing.SystemColors.Control;
            this.layoutConfig.Controls.Add(this.groupBox1);
            this.layoutConfig.Controls.Add(this.listLayout);
            this.layoutConfig.Location = new System.Drawing.Point(4, 21);
            this.layoutConfig.Name = "layoutConfig";
            this.layoutConfig.Padding = new System.Windows.Forms.Padding(3);
            this.layoutConfig.Size = new System.Drawing.Size(625, 353);
            this.layoutConfig.TabIndex = 0;
            this.layoutConfig.Text = "布局管理";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(440, 13);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureLayout);
            this.groupBox1.Location = new System.Drawing.Point(169, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 312);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            // 
            // pictureLayout
            // 
            this.pictureLayout.Location = new System.Drawing.Point(32, 20);
            this.pictureLayout.Name = "pictureLayout";
            this.pictureLayout.Size = new System.Drawing.Size(355, 275);
            this.pictureLayout.TabIndex = 1;
            this.pictureLayout.TabStop = false;
            // 
            // listLayout
            // 
            this.listLayout.FormattingEnabled = true;
            this.listLayout.ItemHeight = 12;
            this.listLayout.Location = new System.Drawing.Point(25, 19);
            this.listLayout.Name = "listLayout";
            this.listLayout.Size = new System.Drawing.Size(120, 304);
            this.listLayout.TabIndex = 0;
            this.listLayout.SelectedIndexChanged += new System.EventHandler(this.listLayout_SelectedIndexChanged);
            // 
            // parameterConfig
            // 
            this.parameterConfig.BackColor = System.Drawing.SystemColors.Control;
            this.parameterConfig.Controls.Add(this.label4);
            this.parameterConfig.Controls.Add(this.label3);
            this.parameterConfig.Controls.Add(this.numAutosaveLifeTime);
            this.parameterConfig.Controls.Add(this.label2);
            this.parameterConfig.Controls.Add(this.numAutosavePeriod);
            this.parameterConfig.Controls.Add(this.label1);
            this.parameterConfig.Location = new System.Drawing.Point(4, 21);
            this.parameterConfig.Name = "parameterConfig";
            this.parameterConfig.Padding = new System.Windows.Forms.Padding(3);
            this.parameterConfig.Size = new System.Drawing.Size(625, 353);
            this.parameterConfig.TabIndex = 1;
            this.parameterConfig.Text = "参数设定";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "自动保存间隔";
            // 
            // numAutosavePeriod
            // 
            this.numAutosavePeriod.Location = new System.Drawing.Point(121, 27);
            this.numAutosavePeriod.Name = "numAutosavePeriod";
            this.numAutosavePeriod.Size = new System.Drawing.Size(82, 21);
            this.numAutosavePeriod.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "删除";
            // 
            // numAutosaveLifeTime
            // 
            this.numAutosaveLifeTime.Location = new System.Drawing.Point(121, 67);
            this.numAutosaveLifeTime.Name = "numAutosaveLifeTime";
            this.numAutosaveLifeTime.Size = new System.Drawing.Size(82, 21);
            this.numAutosaveLifeTime.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "小时之前的记录";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(220, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "分钟";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(536, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.btnConfirm);
            this.splitContainer1.Size = new System.Drawing.Size(633, 430);
            this.splitContainer1.SplitterDistance = 378;
            this.splitContainer1.TabIndex = 1;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(633, 430);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.ShowInTaskbar = false;
            this.Text = "ConfigForm";
            this.tabControl1.ResumeLayout(false);
            this.layoutConfig.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureLayout)).EndInit();
            this.parameterConfig.ResumeLayout(false);
            this.parameterConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAutosavePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAutosaveLifeTime)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage layoutConfig;
        private System.Windows.Forms.TabPage parameterConfig;
        private System.Windows.Forms.ListBox listLayout;
        private System.Windows.Forms.PictureBox pictureLayout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numAutosavePeriod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numAutosaveLifeTime;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}