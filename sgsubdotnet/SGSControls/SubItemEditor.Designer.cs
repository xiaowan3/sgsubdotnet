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
            this.btnDogear = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHumanear
            // 
            this.btnHumanear.Location = new System.Drawing.Point(23, 45);
            this.btnHumanear.Name = "btnHumanear";
            this.btnHumanear.Size = new System.Drawing.Size(75, 23);
            this.btnHumanear.TabIndex = 0;
            this.btnHumanear.Text = "人耳";
            this.btnHumanear.UseVisualStyleBackColor = true;
            this.btnHumanear.Click += new System.EventHandler(this.btnHumanear_Click);
            // 
            // btnDogear
            // 
            this.btnDogear.Location = new System.Drawing.Point(104, 45);
            this.btnDogear.Name = "btnDogear";
            this.btnDogear.Size = new System.Drawing.Size(75, 23);
            this.btnDogear.TabIndex = 1;
            this.btnDogear.Text = "狗耳";
            this.btnDogear.UseVisualStyleBackColor = true;
            this.btnDogear.Click += new System.EventHandler(this.btnDogear_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(185, 45);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // SubItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnDogear);
            this.Controls.Add(this.btnHumanear);
            this.Name = "SubItemEditor";
            this.Size = new System.Drawing.Size(317, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHumanear;
        private System.Windows.Forms.Button btnDogear;
        private System.Windows.Forms.Button button3;
    }
}
