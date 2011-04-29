namespace SGSControls
{
    partial class SubItemEditor
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
            this.btnHumanear = new System.Windows.Forms.Button();
            this.btnCatear = new System.Windows.Forms.Button();
            this.btnRabbitear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHumanear
            // 
            this.btnHumanear.Location = new System.Drawing.Point(15, 15);
            this.btnHumanear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHumanear.Name = "btnHumanear";
            this.btnHumanear.Size = new System.Drawing.Size(56, 29);
            this.btnHumanear.TabIndex = 0;
            this.btnHumanear.Text = "人耳";
            this.btnHumanear.UseVisualStyleBackColor = true;
            this.btnHumanear.Click += new System.EventHandler(this.btnHumanear_Click);
            // 
            // btnCatear
            // 
            this.btnCatear.Location = new System.Drawing.Point(77, 15);
            this.btnCatear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCatear.Name = "btnCatear";
            this.btnCatear.Size = new System.Drawing.Size(56, 29);
            this.btnCatear.TabIndex = 1;
            this.btnCatear.Text = "猫耳";
            this.btnCatear.UseVisualStyleBackColor = true;
            this.btnCatear.Click += new System.EventHandler(this.btnDogear_Click);
            // 
            // btnRabbitear
            // 
            this.btnRabbitear.Location = new System.Drawing.Point(139, 15);
            this.btnRabbitear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRabbitear.Name = "btnRabbitear";
            this.btnRabbitear.Size = new System.Drawing.Size(56, 29);
            this.btnRabbitear.TabIndex = 2;
            this.btnRabbitear.Text = "兔耳";
            this.btnRabbitear.UseVisualStyleBackColor = true;
            this.btnRabbitear.Click += new System.EventHandler(this.btnRabbitear_Click);
            // 
            // SubItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRabbitear);
            this.Controls.Add(this.btnCatear);
            this.Controls.Add(this.btnHumanear);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SubItemEditor";
            this.Size = new System.Drawing.Size(238, 76);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHumanear;
        private System.Windows.Forms.Button btnCatear;
        private System.Windows.Forms.Button btnRabbitear;
    }
}
