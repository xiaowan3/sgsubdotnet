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
            this.btnRabbitear = new System.Windows.Forms.Button();
            this.btnCatear = new System.Windows.Forms.Button();
            this.btnHumanear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRabbitear
            // 
            this.btnRabbitear.Image = global::SGSControls.Properties.Resources.slower;
            this.btnRabbitear.Location = new System.Drawing.Point(200, 2);
            this.btnRabbitear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRabbitear.Name = "btnRabbitear";
            this.btnRabbitear.Size = new System.Drawing.Size(75, 41);
            this.btnRabbitear.TabIndex = 2;
            this.btnRabbitear.UseVisualStyleBackColor = true;
            this.btnRabbitear.Click += new System.EventHandler(this.btnRabbitear_Click);
            // 
            // btnCatear
            // 
            this.btnCatear.Image = global::SGSControls.Properties.Resources.slow;
            this.btnCatear.Location = new System.Drawing.Point(101, 2);
            this.btnCatear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCatear.Name = "btnCatear";
            this.btnCatear.Size = new System.Drawing.Size(75, 41);
            this.btnCatear.TabIndex = 1;
            this.btnCatear.UseVisualStyleBackColor = true;
            this.btnCatear.Click += new System.EventHandler(this.btnDogear_Click);
            // 
            // btnHumanear
            // 
            this.btnHumanear.Image = global::SGSControls.Properties.Resources.normal;
            this.btnHumanear.Location = new System.Drawing.Point(3, 2);
            this.btnHumanear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHumanear.Name = "btnHumanear";
            this.btnHumanear.Size = new System.Drawing.Size(75, 41);
            this.btnHumanear.TabIndex = 0;
            this.btnHumanear.UseVisualStyleBackColor = true;
            this.btnHumanear.Click += new System.EventHandler(this.btnHumanear_Click);
            // 
            // SubItemEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnRabbitear);
            this.Controls.Add(this.btnCatear);
            this.Controls.Add(this.btnHumanear);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SubItemEditor";
            this.Size = new System.Drawing.Size(317, 47);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHumanear;
        private System.Windows.Forms.Button btnCatear;
        private System.Windows.Forms.Button btnRabbitear;
    }
}
