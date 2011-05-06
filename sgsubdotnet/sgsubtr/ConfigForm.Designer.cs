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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.layoutConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLayout)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.layoutConfig);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(633, 419);
            this.tabControl1.TabIndex = 0;
            // 
            // layoutConfig
            // 
            this.layoutConfig.BackColor = System.Drawing.SystemColors.Control;
            this.layoutConfig.Controls.Add(this.btnConfirm);
            this.layoutConfig.Controls.Add(this.groupBox1);
            this.layoutConfig.Controls.Add(this.listLayout);
            this.layoutConfig.Location = new System.Drawing.Point(4, 21);
            this.layoutConfig.Name = "layoutConfig";
            this.layoutConfig.Padding = new System.Windows.Forms.Padding(3);
            this.layoutConfig.Size = new System.Drawing.Size(625, 394);
            this.layoutConfig.TabIndex = 0;
            this.layoutConfig.Text = "布局管理";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(400, 337);
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
            this.groupBox1.Size = new System.Drawing.Size(423, 280);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            // 
            // pictureLayout
            // 
            this.pictureLayout.Location = new System.Drawing.Point(32, 20);
            this.pictureLayout.Name = "pictureLayout";
            this.pictureLayout.Size = new System.Drawing.Size(355, 244);
            this.pictureLayout.TabIndex = 1;
            this.pictureLayout.TabStop = false;
            // 
            // listLayout
            // 
            this.listLayout.FormattingEnabled = true;
            this.listLayout.ItemHeight = 12;
            this.listLayout.Location = new System.Drawing.Point(25, 19);
            this.listLayout.Name = "listLayout";
            this.listLayout.Size = new System.Drawing.Size(120, 280);
            this.listLayout.TabIndex = 0;
            this.listLayout.SelectedIndexChanged += new System.EventHandler(this.listLayout_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(625, 394);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 419);
            this.Controls.Add(this.tabControl1);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage layoutConfig;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listLayout;
        private System.Windows.Forms.PictureBox pictureLayout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConfirm;
    }
}