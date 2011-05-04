namespace sgsubtr
{
    partial class KeyConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyConfigForm));
            this.btnFF = new System.Windows.Forms.Button();
            this.btnBW = new System.Windows.Forms.Button();
            this.btnT = new System.Windows.Forms.Button();
            this.btnST = new System.Windows.Forms.Button();
            this.btnET = new System.Windows.Forms.Button();
            this.btnP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numST = new System.Windows.Forms.NumericUpDown();
            this.numET = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numSS = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.checkAOC = new System.Windows.Forms.CheckBox();
            this.btnGC = new System.Windows.Forms.Button();
            this.btnGP = new System.Windows.Forms.Button();
            this.btnCT = new System.Windows.Forms.Button();
            this.btnEEM = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnCellT = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnSaveAss = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numET)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSS)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFF
            // 
            this.btnFF.Location = new System.Drawing.Point(169, 46);
            this.btnFF.Name = "btnFF";
            this.btnFF.Size = new System.Drawing.Size(53, 28);
            this.btnFF.TabIndex = 0;
            this.btnFF.Text = "前进";
            this.btnFF.UseVisualStyleBackColor = true;
            this.btnFF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnFF_KeyDown);
            // 
            // btnBW
            // 
            this.btnBW.Location = new System.Drawing.Point(169, 12);
            this.btnBW.Name = "btnBW";
            this.btnBW.Size = new System.Drawing.Size(53, 28);
            this.btnBW.TabIndex = 1;
            this.btnBW.Text = "后退";
            this.btnBW.UseVisualStyleBackColor = true;
            this.btnBW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBW_KeyDown);
            // 
            // btnT
            // 
            this.btnT.Location = new System.Drawing.Point(169, 126);
            this.btnT.Name = "btnT";
            this.btnT.Size = new System.Drawing.Size(53, 28);
            this.btnT.TabIndex = 2;
            this.btnT.Text = "TimeL";
            this.btnT.UseVisualStyleBackColor = true;
            this.btnT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnT_KeyDown);
            // 
            // btnST
            // 
            this.btnST.Location = new System.Drawing.Point(372, 126);
            this.btnST.Name = "btnST";
            this.btnST.Size = new System.Drawing.Size(53, 28);
            this.btnST.TabIndex = 3;
            this.btnST.Text = "St";
            this.btnST.UseVisualStyleBackColor = true;
            this.btnST.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnST_KeyDown);
            // 
            // btnET
            // 
            this.btnET.Location = new System.Drawing.Point(372, 160);
            this.btnET.Name = "btnET";
            this.btnET.Size = new System.Drawing.Size(53, 28);
            this.btnET.TabIndex = 4;
            this.btnET.Text = "Ed";
            this.btnET.UseVisualStyleBackColor = true;
            this.btnET.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnET_KeyDown);
            // 
            // btnP
            // 
            this.btnP.Location = new System.Drawing.Point(169, 80);
            this.btnP.Name = "btnP";
            this.btnP.Size = new System.Drawing.Size(53, 28);
            this.btnP.TabIndex = 5;
            this.btnP.Text = "暂停";
            this.btnP.UseVisualStyleBackColor = true;
            this.btnP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnP_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "起始点反应时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 360);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "终止点反应时间";
            // 
            // numST
            // 
            this.numST.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numST.Location = new System.Drawing.Point(135, 320);
            this.numST.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numST.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numST.Name = "numST";
            this.numST.Size = new System.Drawing.Size(74, 21);
            this.numST.TabIndex = 8;
            this.numST.Value = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
            // 
            // numET
            // 
            this.numET.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numET.Location = new System.Drawing.Point(135, 358);
            this.numET.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numET.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numET.Name = "numET";
            this.numET.Size = new System.Drawing.Size(74, 21);
            this.numET.TabIndex = 9;
            this.numET.Value = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(265, 454);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 34);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(371, 454);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 34);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "ms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 360);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "ms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 397);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "前进/后退步长";
            // 
            // numSS
            // 
            this.numSS.Location = new System.Drawing.Point(135, 396);
            this.numSS.Name = "numSS";
            this.numSS.Size = new System.Drawing.Size(74, 21);
            this.numSS.TabIndex = 15;
            this.numSS.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 398);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "s";
            // 
            // checkAOC
            // 
            this.checkAOC.AutoSize = true;
            this.checkAOC.Location = new System.Drawing.Point(57, 434);
            this.checkAOC.Name = "checkAOC";
            this.checkAOC.Size = new System.Drawing.Size(96, 16);
            this.checkAOC.TabIndex = 17;
            this.checkAOC.Text = "自动重叠修正";
            this.checkAOC.UseVisualStyleBackColor = true;
            // 
            // btnGC
            // 
            this.btnGC.Location = new System.Drawing.Point(372, 12);
            this.btnGC.Name = "btnGC";
            this.btnGC.Size = new System.Drawing.Size(53, 28);
            this.btnGC.TabIndex = 18;
            this.btnGC.Text = "跳至当前行";
            this.btnGC.UseVisualStyleBackColor = true;
            this.btnGC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGotoCurrent_KeyDown);
            // 
            // btnGP
            // 
            this.btnGP.Location = new System.Drawing.Point(372, 46);
            this.btnGP.Name = "btnGP";
            this.btnGP.Size = new System.Drawing.Size(53, 28);
            this.btnGP.TabIndex = 19;
            this.btnGP.Text = "跳至上一行";
            this.btnGP.UseVisualStyleBackColor = true;
            this.btnGP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGotoPrevious_KeyDown);
            // 
            // btnCT
            // 
            this.btnCT.Location = new System.Drawing.Point(169, 194);
            this.btnCT.Name = "btnCT";
            this.btnCT.Size = new System.Drawing.Size(53, 28);
            this.btnCT.TabIndex = 20;
            this.btnCT.Text = "CContT";
            this.btnCT.UseVisualStyleBackColor = true;
            this.btnCT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCT_KeyDown);
            // 
            // btnEEM
            // 
            this.btnEEM.Location = new System.Drawing.Point(169, 240);
            this.btnEEM.Name = "btnEEM";
            this.btnEEM.Size = new System.Drawing.Size(53, 28);
            this.btnEEM.TabIndex = 21;
            this.btnEEM.Text = "编辑";
            this.btnEEM.UseVisualStyleBackColor = true;
            this.btnEEM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnEEM_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(112, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "后退：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(112, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "前进：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(112, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 24;
            this.label9.Text = "暂停：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(275, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "跳至当前行：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(275, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 26;
            this.label11.Text = "跳至上一行：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(40, 134);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 12);
            this.label12.TabIndex = 27;
            this.label12.Text = "插入时间（按行）：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(28, 168);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 12);
            this.label13.TabIndex = 28;
            this.label13.Text = "插入时间（单元格）：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(40, 202);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 12);
            this.label14.TabIndex = 29;
            this.label14.Text = "插入时间（连续）：";
            // 
            // btnCellT
            // 
            this.btnCellT.Location = new System.Drawing.Point(169, 160);
            this.btnCellT.Name = "btnCellT";
            this.btnCellT.Size = new System.Drawing.Size(53, 28);
            this.btnCellT.TabIndex = 30;
            this.btnCellT.Text = "Cell";
            this.btnCellT.UseVisualStyleBackColor = true;
            this.btnCellT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCellT_KeyDown);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(263, 134);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 12);
            this.label15.TabIndex = 31;
            this.label15.Text = "插入起始时间：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(263, 168);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 12);
            this.label16.TabIndex = 32;
            this.label16.Text = "插入结束时间：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(76, 248);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 12);
            this.label17.TabIndex = 33;
            this.label17.Text = "编辑单元格：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(253, 296);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 152);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "说明";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(24, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(148, 121);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "插入时间（按行）：按下时插入开始时间，释放时插入结束时间。\r\n\r\n插入时间（单元格）：按下时插入当前单元格的时间。\r\n\r\n插入时间（连续）：按下时插入当前行的结束" +
                "时间和下一行的开始时间。";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(287, 248);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 35;
            this.label18.Text = "保存字幕：";
            // 
            // btnSaveAss
            // 
            this.btnSaveAss.Location = new System.Drawing.Point(372, 240);
            this.btnSaveAss.Name = "btnSaveAss";
            this.btnSaveAss.Size = new System.Drawing.Size(53, 28);
            this.btnSaveAss.TabIndex = 36;
            this.btnSaveAss.Text = "编辑";
            this.btnSaveAss.UseVisualStyleBackColor = true;
            this.btnSaveAss.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSaveAss_KeyDown);
            // 
            // KeyConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 500);
            this.Controls.Add(this.btnSaveAss);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnCellT);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnEEM);
            this.Controls.Add(this.btnCT);
            this.Controls.Add(this.btnGP);
            this.Controls.Add(this.btnGC);
            this.Controls.Add(this.checkAOC);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numSS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.numET);
            this.Controls.Add(this.numST);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnP);
            this.Controls.Add(this.btnET);
            this.Controls.Add(this.btnST);
            this.Controls.Add(this.btnT);
            this.Controls.Add(this.btnBW);
            this.Controls.Add(this.btnFF);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KeyConfigForm";
            this.ShowInTaskbar = false;
            this.Text = "按键设置";
            this.Load += new System.EventHandler(this.KeyConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numET)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSS)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFF;
        private System.Windows.Forms.Button btnBW;
        private System.Windows.Forms.Button btnT;
        private System.Windows.Forms.Button btnST;
        private System.Windows.Forms.Button btnET;
        private System.Windows.Forms.Button btnP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numST;
        private System.Windows.Forms.NumericUpDown numET;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numSS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkAOC;
        private System.Windows.Forms.Button btnGC;
        private System.Windows.Forms.Button btnGP;
        private System.Windows.Forms.Button btnCT;
        private System.Windows.Forms.Button btnEEM;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnCellT;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnSaveAss;
    }
}