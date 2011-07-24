namespace SGSControls
{
    partial class TranslationEditor
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
            this.translationEditorTSContainer = new System.Windows.Forms.ToolStripContainer();
            this.syntaxHighlightingTextBox1 = new SGSControls.SyntaxHighlightingTextBox();
            this.translationEditorTSContainer.ContentPanel.SuspendLayout();
            this.translationEditorTSContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // translationEditorTSContainer
            // 
            // 
            // translationEditorTSContainer.ContentPanel
            // 
            this.translationEditorTSContainer.ContentPanel.Controls.Add(this.syntaxHighlightingTextBox1);
            this.translationEditorTSContainer.ContentPanel.Size = new System.Drawing.Size(328, 229);
            this.translationEditorTSContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.translationEditorTSContainer.Location = new System.Drawing.Point(0, 0);
            this.translationEditorTSContainer.Name = "translationEditorTSContainer";
            this.translationEditorTSContainer.Size = new System.Drawing.Size(328, 254);
            this.translationEditorTSContainer.TabIndex = 0;
            this.translationEditorTSContainer.Text = "toolStripContainer1";
            // 
            // syntaxHighlightingTextBox1
            // 
            this.syntaxHighlightingTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxHighlightingTextBox1.Location = new System.Drawing.Point(0, 0);
            this.syntaxHighlightingTextBox1.MaxUndoRedoSteps = 50;
            this.syntaxHighlightingTextBox1.Name = "syntaxHighlightingTextBox1";
            this.syntaxHighlightingTextBox1.Size = new System.Drawing.Size(328, 229);
            this.syntaxHighlightingTextBox1.TabIndex = 0;
            this.syntaxHighlightingTextBox1.Text = "";
            // 
            // TranslationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.translationEditorTSContainer);
            this.Name = "TranslationEditor";
            this.Size = new System.Drawing.Size(328, 254);
            this.translationEditorTSContainer.ContentPanel.ResumeLayout(false);
            this.translationEditorTSContainer.ResumeLayout(false);
            this.translationEditorTSContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer translationEditorTSContainer;
        private SyntaxHighlightingTextBox syntaxHighlightingTextBox1;

    }
}
