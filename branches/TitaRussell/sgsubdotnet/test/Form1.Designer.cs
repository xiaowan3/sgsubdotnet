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
            Subtitle.AssSub assSub2 = new Subtitle.AssSub();
            this.waveFormViewer1 = new SGSControls.WaveFormViewer();
            this.subEditor1 = new SGSControls.SubEditor();
            this.button1 = new System.Windows.Forms.Button();
            this.debugMessage = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
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
            this.subEditor1.Config = null;
            this.subEditor1.CurrentSub = assSub2;
            this.subEditor1.Location = new System.Drawing.Point(12, 282);
            this.subEditor1.Name = "subEditor1";
            this.subEditor1.Size = new System.Drawing.Size(569, 214);
            this.subEditor1.TabIndex = 1;
            this.subEditor1.Seek += new System.EventHandler<SGSControls.SeekEventArgs>(this.subEditor1_Seek);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(600, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // debugMessage
            // 
            this.debugMessage.Location = new System.Drawing.Point(616, 98);
            this.debugMessage.Multiline = true;
            this.debugMessage.Name = "debugMessage";
            this.debugMessage.Size = new System.Drawing.Size(339, 292);
            this.debugMessage.TabIndex = 3;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(616, 408);
            this.trackBar1.Maximum = 600;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(339, 56);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickFrequency = 60;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 508);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.debugMessage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.subEditor1);
            this.Controls.Add(this.waveFormViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SGSControls.WaveFormViewer waveFormViewer1;
        private SGSControls.SubEditor subEditor1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox debugMessage;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

