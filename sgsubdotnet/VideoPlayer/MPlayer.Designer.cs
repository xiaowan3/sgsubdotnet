using System.Windows.Forms;

namespace SGS.VideoPlayer
{
    partial class MPlayer
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.screen = new System.Windows.Forms.Panel();
            this.msglabel = new System.Windows.Forms.Label();
            this.playTimer = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.screen);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.msglabel);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel2_MouseMove);
            this.splitContainer1.Panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel2_MouseDown);
            this.splitContainer1.Panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel2_MouseUp);
            this.splitContainer1.Size = new System.Drawing.Size(315, 272);
            this.splitContainer1.SplitterDistance = 239;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // screen
            // 
            this.screen.Location = new System.Drawing.Point(59, 0);
            this.screen.Margin = new System.Windows.Forms.Padding(2);
            this.screen.Name = "screen";
           //this.screen.Size = new System.Drawing.Size(177, 225);
            this.screen.TabIndex = 0;
            this.screen.Dock = DockStyle.Fill;
            this.screen.SizeChanged += new System.EventHandler(screen_SizeChanged);
            // 
            // msglabel
            // 
            this.msglabel.AutoSize = true;
            this.msglabel.Font = new System.Drawing.Font("SimSun", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.msglabel.ForeColor = System.Drawing.Color.White;
            this.msglabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.msglabel.Location = new System.Drawing.Point(3, 16);
            this.msglabel.Name = "msglabel";
            this.msglabel.Size = new System.Drawing.Size(126, 13);
            this.msglabel.TabIndex = 0;
            this.msglabel.Text = "00:00:00/00:00:00";
            // 
            // playTimer
            // 
            this.playTimer.Enabled = true;
            this.playTimer.Interval = 200;
            this.playTimer.Tick += new System.EventHandler(this.playTimer_Tick);
            // 
            // DXVideoPlayer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.CausesValidation = false;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("SimSun", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DXVideoPlayer";
            this.Size = new System.Drawing.Size(315, 272);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel screen;
        private System.Windows.Forms.Timer playTimer;
        private System.Windows.Forms.Label msglabel;
    }
}
