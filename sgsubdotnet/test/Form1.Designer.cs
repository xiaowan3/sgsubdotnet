namespace test
{
    partial class Form1
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
            this.waveFormViewer1 = new SGSControls.WaveFormViewer();
            this.subEditor1 = new SGSControls.SubEditor();
            this.SuspendLayout();
            // 
            // waveFormViewer1
            // 
            this.waveFormViewer1.Location = new System.Drawing.Point(12, 23);
            this.waveFormViewer1.Name = "waveFormViewer1";
            this.waveFormViewer1.Size = new System.Drawing.Size(569, 245);
            this.waveFormViewer1.TabIndex = 0;
            // 
            // subEditor1
            // 
            this.subEditor1.Location = new System.Drawing.Point(12, 282);
            this.subEditor1.Name = "subEditor1";
            this.subEditor1.Size = new System.Drawing.Size(569, 214);
            this.subEditor1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 508);
            this.Controls.Add(this.subEditor1);
            this.Controls.Add(this.waveFormViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private SGSControls.WaveFormViewer waveFormViewer1;
        private SGSControls.SubEditor subEditor1;
    }
}

