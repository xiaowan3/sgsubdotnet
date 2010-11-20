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
            ((System.ComponentModel.ISupportInitialize)(this.numST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numET)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFF
            // 
            this.btnFF.Location = new System.Drawing.Point(128, 12);
            this.btnFF.Name = "btnFF";
            this.btnFF.Size = new System.Drawing.Size(102, 28);
            this.btnFF.TabIndex = 0;
            this.btnFF.Text = "前进";
            this.btnFF.UseVisualStyleBackColor = true;
            this.btnFF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnFF_KeyDown);
            // 
            // btnBW
            // 
            this.btnBW.Location = new System.Drawing.Point(17, 12);
            this.btnBW.Name = "btnBW";
            this.btnBW.Size = new System.Drawing.Size(102, 28);
            this.btnBW.TabIndex = 1;
            this.btnBW.Text = "后退";
            this.btnBW.UseVisualStyleBackColor = true;
            this.btnBW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBW_KeyDown);
            // 
            // btnT
            // 
            this.btnT.Location = new System.Drawing.Point(17, 56);
            this.btnT.Name = "btnT";
            this.btnT.Size = new System.Drawing.Size(102, 28);
            this.btnT.TabIndex = 2;
            this.btnT.Text = "插入时间点";
            this.btnT.UseVisualStyleBackColor = true;
            this.btnT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnT_KeyDown);
            // 
            // btnST
            // 
            this.btnST.Location = new System.Drawing.Point(128, 56);
            this.btnST.Name = "btnST";
            this.btnST.Size = new System.Drawing.Size(102, 28);
            this.btnST.TabIndex = 3;
            this.btnST.Text = "插入起始点";
            this.btnST.UseVisualStyleBackColor = true;
            this.btnST.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnST_KeyDown);
            // 
            // btnET
            // 
            this.btnET.Location = new System.Drawing.Point(239, 56);
            this.btnET.Name = "btnET";
            this.btnET.Size = new System.Drawing.Size(102, 28);
            this.btnET.TabIndex = 4;
            this.btnET.Text = "插入终止点";
            this.btnET.UseVisualStyleBackColor = true;
            this.btnET.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnET_KeyDown);
            // 
            // btnP
            // 
            this.btnP.Location = new System.Drawing.Point(239, 12);
            this.btnP.Name = "btnP";
            this.btnP.Size = new System.Drawing.Size(102, 28);
            this.btnP.TabIndex = 5;
            this.btnP.Text = "暂停";
            this.btnP.UseVisualStyleBackColor = true;
            this.btnP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnP_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "起始点反应时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 153);
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
            this.numST.Location = new System.Drawing.Point(119, 113);
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
            this.numET.Location = new System.Drawing.Point(119, 151);
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
            this.btnOK.Location = new System.Drawing.Point(185, 220);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 34);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(266, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 34);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // KeyConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 266);
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
            this.Name = "KeyConfigForm";
            this.Text = "按键设置";
            this.Load += new System.EventHandler(this.KeyConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numET)).EndInit();
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
    }
}