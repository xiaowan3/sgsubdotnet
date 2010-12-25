namespace sgsubdotnet
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
            ((System.ComponentModel.ISupportInitialize)(this.numST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numET)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSS)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFF
            // 
            this.btnFF.Location = new System.Drawing.Point(171, 15);
            this.btnFF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFF.Name = "btnFF";
            this.btnFF.Size = new System.Drawing.Size(136, 35);
            this.btnFF.TabIndex = 0;
            this.btnFF.Text = "前进";
            this.btnFF.UseVisualStyleBackColor = true;
            this.btnFF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnFF_KeyDown);
            // 
            // btnBW
            // 
            this.btnBW.Location = new System.Drawing.Point(23, 15);
            this.btnBW.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBW.Name = "btnBW";
            this.btnBW.Size = new System.Drawing.Size(136, 35);
            this.btnBW.TabIndex = 1;
            this.btnBW.Text = "后退";
            this.btnBW.UseVisualStyleBackColor = true;
            this.btnBW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBW_KeyDown);
            // 
            // btnT
            // 
            this.btnT.Location = new System.Drawing.Point(23, 125);
            this.btnT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnT.Name = "btnT";
            this.btnT.Size = new System.Drawing.Size(136, 35);
            this.btnT.TabIndex = 2;
            this.btnT.Text = "插入时间点";
            this.btnT.UseVisualStyleBackColor = true;
            this.btnT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnT_KeyDown);
            // 
            // btnST
            // 
            this.btnST.Location = new System.Drawing.Point(23, 179);
            this.btnST.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnST.Name = "btnST";
            this.btnST.Size = new System.Drawing.Size(136, 35);
            this.btnST.TabIndex = 3;
            this.btnST.Text = "插入起始点";
            this.btnST.UseVisualStyleBackColor = true;
            this.btnST.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnST_KeyDown);
            // 
            // btnET
            // 
            this.btnET.Location = new System.Drawing.Point(171, 179);
            this.btnET.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnET.Name = "btnET";
            this.btnET.Size = new System.Drawing.Size(136, 35);
            this.btnET.TabIndex = 4;
            this.btnET.Text = "插入终止点";
            this.btnET.UseVisualStyleBackColor = true;
            this.btnET.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnET_KeyDown);
            // 
            // btnP
            // 
            this.btnP.Location = new System.Drawing.Point(319, 15);
            this.btnP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnP.Name = "btnP";
            this.btnP.Size = new System.Drawing.Size(136, 35);
            this.btnP.TabIndex = 5;
            this.btnP.Text = "暂停";
            this.btnP.UseVisualStyleBackColor = true;
            this.btnP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnP_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 250);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "起始点反应时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 298);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
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
            this.numST.Location = new System.Drawing.Point(163, 248);
            this.numST.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.numST.Size = new System.Drawing.Size(99, 25);
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
            this.numET.Location = new System.Drawing.Point(163, 295);
            this.numET.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.numET.Size = new System.Drawing.Size(99, 25);
            this.numET.TabIndex = 9;
            this.numET.Value = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(253, 418);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 42);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(361, 418);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 42);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 250);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "ms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(269, 298);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "ms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 344);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "前进/后退步长";
            // 
            // numSS
            // 
            this.numSS.Location = new System.Drawing.Point(163, 342);
            this.numSS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numSS.Name = "numSS";
            this.numSS.Size = new System.Drawing.Size(99, 25);
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
            this.label6.Location = new System.Drawing.Point(269, 345);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "s";
            // 
            // checkAOC
            // 
            this.checkAOC.AutoSize = true;
            this.checkAOC.Location = new System.Drawing.Point(23, 395);
            this.checkAOC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkAOC.Name = "checkAOC";
            this.checkAOC.Size = new System.Drawing.Size(119, 19);
            this.checkAOC.TabIndex = 17;
            this.checkAOC.Text = "自动重叠修正";
            this.checkAOC.UseVisualStyleBackColor = true;
            // 
            // btnGC
            // 
            this.btnGC.Location = new System.Drawing.Point(23, 70);
            this.btnGC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGC.Name = "btnGC";
            this.btnGC.Size = new System.Drawing.Size(136, 35);
            this.btnGC.TabIndex = 18;
            this.btnGC.Text = "跳至当前行";
            this.btnGC.UseVisualStyleBackColor = true;
            this.btnGC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGotoCurrent_KeyDown);
            // 
            // btnGP
            // 
            this.btnGP.Location = new System.Drawing.Point(171, 70);
            this.btnGP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGP.Name = "btnGP";
            this.btnGP.Size = new System.Drawing.Size(136, 35);
            this.btnGP.TabIndex = 19;
            this.btnGP.Text = "跳至上一行";
            this.btnGP.UseVisualStyleBackColor = true;
            this.btnGP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGotoPrevious_KeyDown);
            // 
            // btnCT
            // 
            this.btnCT.Location = new System.Drawing.Point(171, 125);
            this.btnCT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCT.Name = "btnCT";
            this.btnCT.Size = new System.Drawing.Size(136, 35);
            this.btnCT.TabIndex = 20;
            this.btnCT.Text = "连续插入时间";
            this.btnCT.UseVisualStyleBackColor = true;
            this.btnCT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCT_KeyDown);
            // 
            // KeyConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 479);
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
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "KeyConfigForm";
            this.Text = "按键设置";
            this.Load += new System.EventHandler(this.KeyConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numET)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSS)).EndInit();
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
    }
}