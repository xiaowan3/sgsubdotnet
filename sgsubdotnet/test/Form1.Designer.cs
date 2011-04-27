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
            Subtitle.AssSub assSub1 = new Subtitle.AssSub();
            this.button1 = new System.Windows.Forms.Button();
            this.debugMessage = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.subEditor1 = new SGSControls.SubEditor();
            this.waveFormViewer1 = new SGSControls.WaveFormViewer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(600, 41);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // debugMessage
            // 
            this.debugMessage.Location = new System.Drawing.Point(616, 98);
            this.debugMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.debugMessage.Multiline = true;
            this.debugMessage.Name = "debugMessage";
            this.debugMessage.Size = new System.Drawing.Size(339, 292);
            this.debugMessage.TabIndex = 3;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(616, 408);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar1.Maximum = 600;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(339, 56);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickFrequency = 60;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(731, 45);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(101, 19);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(616, 464);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 29);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // subEditor1
            // 
            this.subEditor1.Config = null;
            this.subEditor1.CurrentSub = assSub1;
            this.subEditor1.Edited = false;
            this.subEditor1.Location = new System.Drawing.Point(15, 361);
            this.subEditor1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.subEditor1.Name = "subEditor1";
            this.subEditor1.Size = new System.Drawing.Size(569, 214);
            this.subEditor1.TabIndex = 1;
            this.subEditor1.VideoLength = 0;
            this.subEditor1.Seek += new System.EventHandler<SGSControls.SeekEventArgs>(this.subEditor1_Seek);
            this.subEditor1.CurrentRowChanged += new System.EventHandler<SGSControls.CurrentRowChangeEventArgs>(this.subEditor1_CurrentRowChanged);
            this.subEditor1.TimeEdit += new System.EventHandler<SGSControls.TimeEditEventArgs>(this.subEditor1_TimeEdit);
            // 
            // waveFormViewer1
            // 
            this.waveFormViewer1.CurrentLineIndex = -1;
            this.waveFormViewer1.CurrentPosition = 0;
            this.waveFormViewer1.CurrentSub = null;
            this.waveFormViewer1.Location = new System.Drawing.Point(12, 22);
            this.waveFormViewer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.waveFormViewer1.Name = "waveFormViewer1";
            this.waveFormViewer1.Size = new System.Drawing.Size(569, 292);
            this.waveFormViewer1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(744, 464);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(213, 100);
            this.splitContainer1.SplitterDistance = 75;
            this.splitContainer1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 589);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.debugMessage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.subEditor1);
            this.Controls.Add(this.waveFormViewer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SGSControls.WaveFormViewer waveFormViewer1;
        private SGSControls.SubEditor subEditor1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox debugMessage;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

