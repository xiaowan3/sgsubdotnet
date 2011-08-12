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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureLayout = new System.Windows.Forms.PictureBox();
            this.listLayout = new System.Windows.Forms.ListBox();
            this.parameterConfig = new System.Windows.Forms.TabPage();
            this.comboPlayer = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textUncertainRight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textUncertainLeft = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numLineLength = new System.Windows.Forms.NumericUpDown();
            this.textCommentChar = new System.Windows.Forms.TextBox();
            this.textWindowChar = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numAutosaveLifeTime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numAutosavePeriod = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.layoutConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLayout)).BeginInit();
            this.parameterConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLineLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAutosaveLifeTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAutosavePeriod)).BeginInit();
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
            this.parameterConfig.Controls.Add(this.label12);
            this.parameterConfig.Controls.Add(this.comboPlayer);
            this.parameterConfig.Controls.Add(this.label11);
            this.parameterConfig.Controls.Add(this.textUncertainRight);
            this.parameterConfig.Controls.Add(this.label10);
            this.parameterConfig.Controls.Add(this.label9);
            this.parameterConfig.Controls.Add(this.textUncertainLeft);
            this.parameterConfig.Controls.Add(this.label8);
            this.parameterConfig.Controls.Add(this.numLineLength);
            this.parameterConfig.Controls.Add(this.textCommentChar);
            this.parameterConfig.Controls.Add(this.textWindowChar);
            this.parameterConfig.Controls.Add(this.label7);
            this.parameterConfig.Controls.Add(this.label6);
            this.parameterConfig.Controls.Add(this.label5);
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
            // comboPlayer
            // 
            this.comboPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPlayer.FormattingEnabled = true;
            this.comboPlayer.Items.AddRange(new object[] {
            "DShow 播放器",
            "MDX 播放器",
            "WMP 播放器"});
            this.comboPlayer.Location = new System.Drawing.Point(381, 27);
            this.comboPlayer.Name = "comboPlayer";
            this.comboPlayer.Size = new System.Drawing.Size(115, 20);
            this.comboPlayer.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(334, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 17;
            this.label11.Text = "播放器";
            // 
            // textUncertainRight
            // 
            this.textUncertainRight.Location = new System.Drawing.Point(243, 251);
            this.textUncertainRight.Name = "textUncertainRight";
            this.textUncertainRight.Size = new System.Drawing.Size(46, 21);
            this.textUncertainRight.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 255);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = "右";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(134, 255);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "左";
            // 
            // textUncertainLeft
            // 
            this.textUncertainLeft.Location = new System.Drawing.Point(157, 251);
            this.textUncertainLeft.Name = "textUncertainLeft";
            this.textUncertainLeft.Size = new System.Drawing.Size(46, 21);
            this.textUncertainLeft.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(62, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "存疑标记";
            // 
            // numLineLength
            // 
            this.numLineLength.Location = new System.Drawing.Point(136, 213);
            this.numLineLength.Name = "numLineLength";
            this.numLineLength.Size = new System.Drawing.Size(100, 21);
            this.numLineLength.TabIndex = 11;
            // 
            // textCommentChar
            // 
            this.textCommentChar.Location = new System.Drawing.Point(136, 175);
            this.textCommentChar.MaxLength = 1;
            this.textCommentChar.Name = "textCommentChar";
            this.textCommentChar.Size = new System.Drawing.Size(100, 21);
            this.textCommentChar.TabIndex = 10;
            // 
            // textWindowChar
            // 
            this.textWindowChar.Location = new System.Drawing.Point(136, 137);
            this.textWindowChar.MaxLength = 1;
            this.textWindowChar.Name = "textWindowChar";
            this.textWindowChar.Size = new System.Drawing.Size(100, 21);
            this.textWindowChar.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "每行字数报警";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(86, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "注释";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(98, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "窗";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "小时之前的记录";
            // 
            // numAutosaveLifeTime
            // 
            this.numAutosaveLifeTime.Location = new System.Drawing.Point(121, 67);
            this.numAutosaveLifeTime.Name = "numAutosaveLifeTime";
            this.numAutosaveLifeTime.Size = new System.Drawing.Size(82, 21);
            this.numAutosaveLifeTime.TabIndex = 3;
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
            // numAutosavePeriod
            // 
            this.numAutosavePeriod.Location = new System.Drawing.Point(121, 27);
            this.numAutosavePeriod.Name = "numAutosavePeriod";
            this.numAutosavePeriod.Size = new System.Drawing.Size(82, 21);
            this.numAutosavePeriod.TabIndex = 1;
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
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(506, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 12);
            this.label12.TabIndex = 19;
            this.label12.Text = "更改后请重启此程序";
            // 
            // ConfigForm
            // 
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
            ((System.ComponentModel.ISupportInitialize)(this.numLineLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAutosaveLifeTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAutosavePeriod)).EndInit();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textCommentChar;
        private System.Windows.Forms.TextBox textWindowChar;
        private System.Windows.Forms.NumericUpDown numLineLength;
        private System.Windows.Forms.TextBox textUncertainRight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textUncertainLeft;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboPlayer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}