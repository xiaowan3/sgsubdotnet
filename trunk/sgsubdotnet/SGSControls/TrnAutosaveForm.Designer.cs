namespace SGS.Controls
{
    partial class TrnAutosaveForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRevert = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ColSaveTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColSaveTime,
            this.ColFilename});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(498, 330);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnRevert
            // 
            this.btnRevert.Location = new System.Drawing.Point(12, 348);
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Size = new System.Drawing.Size(75, 32);
            this.btnRevert.TabIndex = 1;
            this.btnRevert.Text = "恢复";
            this.btnRevert.UseVisualStyleBackColor = true;
            this.btnRevert.Click += new System.EventHandler(this.btnRevert_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(153, 348);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 32);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(294, 348);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 32);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(435, 348);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ColSaveTime
            // 
            this.ColSaveTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColSaveTime.DataPropertyName = "SaveTime";
            this.ColSaveTime.HeaderText = "保存时间";
            this.ColSaveTime.Name = "ColSaveTime";
            this.ColSaveTime.ReadOnly = true;
            this.ColSaveTime.Width = 78;
            // 
            // ColFilename
            // 
            this.ColFilename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ColFilename.DataPropertyName = "Filename";
            this.ColFilename.HeaderText = "文件名";
            this.ColFilename.Name = "ColFilename";
            this.ColFilename.ReadOnly = true;
            this.ColFilename.Width = 66;
            // 
            // TrnAutosaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 392);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRevert);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TrnAutosaveForm";
            this.Text = "自动保存记录";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRevert;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSaveTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFilename;
    }
}