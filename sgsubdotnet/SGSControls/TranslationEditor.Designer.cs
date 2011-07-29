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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TranslationEditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnToggle = new System.Windows.Forms.Button();
            this.labelToggle = new System.Windows.Forms.Label();
            this.labelFF = new System.Windows.Forms.Label();
            this.labelRW = new System.Windows.Forms.Label();
            this.btnSeek = new System.Windows.Forms.Button();
            this.btnInsertTimeTag = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelToolong = new System.Windows.Forms.Label();
            this.labelUncertain = new System.Windows.Forms.Label();
            this.labelWindows = new System.Windows.Forms.Label();
            this.labelLines = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.查找ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.替换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syntaxHighlightingTextBox1 = new SGSControls.SyntaxHighlightingTextBox();
            this.btnRW = new System.Windows.Forms.Button();
            this.btnFF = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnRW);
            this.splitContainer1.Panel1.Controls.Add(this.btnFF);
            this.splitContainer1.Panel1.Controls.Add(this.btnToggle);
            this.splitContainer1.Panel1.Controls.Add(this.labelToggle);
            this.splitContainer1.Panel1.Controls.Add(this.labelFF);
            this.splitContainer1.Panel1.Controls.Add(this.labelRW);
            this.splitContainer1.Panel1.Controls.Add(this.btnSeek);
            this.splitContainer1.Panel1.Controls.Add(this.btnInsertTimeTag);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.syntaxHighlightingTextBox1);
            this.splitContainer1.Size = new System.Drawing.Size(491, 370);
            this.splitContainer1.SplitterDistance = 207;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnToggle
            // 
            this.btnToggle.Image = global::SGSControls.Properties.Resources.toggle;
            this.btnToggle.Location = new System.Drawing.Point(39, 134);
            this.btnToggle.Name = "btnToggle";
            this.btnToggle.Size = new System.Drawing.Size(25, 23);
            this.btnToggle.TabIndex = 6;
            this.btnToggle.UseVisualStyleBackColor = true;
            this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
            // 
            // labelToggle
            // 
            this.labelToggle.AutoSize = true;
            this.labelToggle.Location = new System.Drawing.Point(20, 281);
            this.labelToggle.Name = "labelToggle";
            this.labelToggle.Size = new System.Drawing.Size(83, 12);
            this.labelToggle.TabIndex = 5;
            this.labelToggle.Text = "暂停：Ctrl+↓";
            // 
            // labelFF
            // 
            this.labelFF.AutoSize = true;
            this.labelFF.Location = new System.Drawing.Point(20, 256);
            this.labelFF.Name = "labelFF";
            this.labelFF.Size = new System.Drawing.Size(83, 12);
            this.labelFF.TabIndex = 4;
            this.labelFF.Text = "前进：Ctrl+→";
            // 
            // labelRW
            // 
            this.labelRW.AutoSize = true;
            this.labelRW.Location = new System.Drawing.Point(20, 231);
            this.labelRW.Name = "labelRW";
            this.labelRW.Size = new System.Drawing.Size(83, 12);
            this.labelRW.TabIndex = 3;
            this.labelRW.Text = "后退：Ctrl+←";
            // 
            // btnSeek
            // 
            this.btnSeek.Location = new System.Drawing.Point(103, 177);
            this.btnSeek.Name = "btnSeek";
            this.btnSeek.Size = new System.Drawing.Size(75, 28);
            this.btnSeek.TabIndex = 2;
            this.btnSeek.TabStop = false;
            this.btnSeek.Text = "跳转";
            this.btnSeek.UseVisualStyleBackColor = true;
            this.btnSeek.Click += new System.EventHandler(this.btnSeek_Click);
            // 
            // btnInsertTimeTag
            // 
            this.btnInsertTimeTag.Location = new System.Drawing.Point(22, 177);
            this.btnInsertTimeTag.Name = "btnInsertTimeTag";
            this.btnInsertTimeTag.Size = new System.Drawing.Size(75, 28);
            this.btnInsertTimeTag.TabIndex = 1;
            this.btnInsertTimeTag.TabStop = false;
            this.btnInsertTimeTag.Text = "插入标签";
            this.btnInsertTimeTag.UseVisualStyleBackColor = true;
            this.btnInsertTimeTag.Click += new System.EventHandler(this.btnInsertTimeTag_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelToolong);
            this.groupBox1.Controls.Add(this.labelUncertain);
            this.groupBox1.Controls.Add(this.labelWindows);
            this.groupBox1.Controls.Add(this.labelLines);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "统计";
            // 
            // labelToolong
            // 
            this.labelToolong.AutoSize = true;
            this.labelToolong.Location = new System.Drawing.Point(17, 86);
            this.labelToolong.Name = "labelToolong";
            this.labelToolong.Size = new System.Drawing.Size(29, 12);
            this.labelToolong.TabIndex = 3;
            this.labelToolong.Text = "太长";
            // 
            // labelUncertain
            // 
            this.labelUncertain.AutoSize = true;
            this.labelUncertain.Location = new System.Drawing.Point(17, 61);
            this.labelUncertain.Name = "labelUncertain";
            this.labelUncertain.Size = new System.Drawing.Size(29, 12);
            this.labelUncertain.TabIndex = 2;
            this.labelUncertain.Text = "存疑";
            // 
            // labelWindows
            // 
            this.labelWindows.AutoSize = true;
            this.labelWindows.Location = new System.Drawing.Point(17, 39);
            this.labelWindows.Name = "labelWindows";
            this.labelWindows.Size = new System.Drawing.Size(17, 12);
            this.labelWindows.TabIndex = 1;
            this.labelWindows.Text = "窗";
            // 
            // labelLines
            // 
            this.labelLines.AutoSize = true;
            this.labelLines.Location = new System.Drawing.Point(17, 17);
            this.labelLines.Name = "labelLines";
            this.labelLines.Size = new System.Drawing.Size(29, 12);
            this.labelLines.TabIndex = 0;
            this.labelLines.Text = "行数";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemEdit});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(491, 24);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "主菜单";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.menuItemSave,
            this.menuItemSaveas,
            this.menuItemExport});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(41, 20);
            this.menuItemFile.Text = "文件";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.Size = new System.Drawing.Size(118, 22);
            this.menuItemNew.Text = "新建";
            this.menuItemNew.Click += new System.EventHandler(this.menuItemNew_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Size = new System.Drawing.Size(118, 22);
            this.menuItemOpen.Text = "打开";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItemSave
            // 
            this.menuItemSave.Name = "menuItemSave";
            this.menuItemSave.Size = new System.Drawing.Size(118, 22);
            this.menuItemSave.Text = "保存";
            this.menuItemSave.Click += new System.EventHandler(this.menuItemSave_Click);
            // 
            // menuItemSaveas
            // 
            this.menuItemSaveas.Name = "menuItemSaveas";
            this.menuItemSaveas.Size = new System.Drawing.Size(118, 22);
            this.menuItemSaveas.Text = "另存为";
            this.menuItemSaveas.Click += new System.EventHandler(this.menuItemSaveas_Click);
            // 
            // menuItemExport
            // 
            this.menuItemExport.Name = "menuItemExport";
            this.menuItemExport.Size = new System.Drawing.Size(118, 22);
            this.menuItemExport.Text = "导出文本";
            this.menuItemExport.Click += new System.EventHandler(this.menuItemExport_Click);
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemCopy,
            this.MenuItemCut,
            this.MenuItemPaste,
            this.toolStripSeparator1,
            this.查找ToolStripMenuItem,
            this.替换ToolStripMenuItem});
            this.menuItemEdit.Name = "menuItemEdit";
            this.menuItemEdit.Size = new System.Drawing.Size(41, 20);
            this.menuItemEdit.Text = "编辑";
            // 
            // MenuItemCopy
            // 
            this.MenuItemCopy.Name = "MenuItemCopy";
            this.MenuItemCopy.Size = new System.Drawing.Size(94, 22);
            this.MenuItemCopy.Text = "复制";
            this.MenuItemCopy.Click += new System.EventHandler(this.MenuItemCopy_Click);
            // 
            // MenuItemCut
            // 
            this.MenuItemCut.Name = "MenuItemCut";
            this.MenuItemCut.Size = new System.Drawing.Size(94, 22);
            this.MenuItemCut.Text = "剪切";
            this.MenuItemCut.Click += new System.EventHandler(this.MenuItemCut_Click);
            // 
            // MenuItemPaste
            // 
            this.MenuItemPaste.Name = "MenuItemPaste";
            this.MenuItemPaste.Size = new System.Drawing.Size(94, 22);
            this.MenuItemPaste.Text = "粘贴";
            this.MenuItemPaste.Click += new System.EventHandler(this.MenuItemPaste_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(91, 6);
            // 
            // 查找ToolStripMenuItem
            // 
            this.查找ToolStripMenuItem.Name = "查找ToolStripMenuItem";
            this.查找ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.查找ToolStripMenuItem.Text = "查找";
            // 
            // 替换ToolStripMenuItem
            // 
            this.替换ToolStripMenuItem.Name = "替换ToolStripMenuItem";
            this.替换ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.替换ToolStripMenuItem.Text = "替换";
            // 
            // syntaxHighlightingTextBox1
            // 
            this.syntaxHighlightingTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syntaxHighlightingTextBox1.HideSelection = false;
            this.syntaxHighlightingTextBox1.Location = new System.Drawing.Point(0, 0);
            this.syntaxHighlightingTextBox1.MaxUndoRedoSteps = 50;
            this.syntaxHighlightingTextBox1.Name = "syntaxHighlightingTextBox1";
            this.syntaxHighlightingTextBox1.Size = new System.Drawing.Size(280, 370);
            this.syntaxHighlightingTextBox1.TabIndex = 0;
            this.syntaxHighlightingTextBox1.Text = "";
            this.syntaxHighlightingTextBox1.RefreshSummary += new System.EventHandler<SGSControls.SummaryEventArgs>(this.syntaxHighlightingTextBox1_RefreshSummary);
            // 
            // btnRW
            // 
            this.btnRW.Image = ((System.Drawing.Image)(resources.GetObject("btnRW.Image")));
            this.btnRW.Location = new System.Drawing.Point(92, 134);
            this.btnRW.Name = "btnRW";
            this.btnRW.Size = new System.Drawing.Size(25, 23);
            this.btnRW.TabIndex = 9;
            this.btnRW.UseVisualStyleBackColor = true;
            this.btnRW.Click += new System.EventHandler(this.btnRW_Click);
            // 
            // btnFF
            // 
            this.btnFF.Image = global::SGSControls.Properties.Resources.ff;
            this.btnFF.Location = new System.Drawing.Point(123, 134);
            this.btnFF.Name = "btnFF";
            this.btnFF.Size = new System.Drawing.Size(25, 23);
            this.btnFF.TabIndex = 8;
            this.btnFF.UseVisualStyleBackColor = true;
            this.btnFF.Click += new System.EventHandler(this.btnFF_Click);
            // 
            // TranslationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainMenu);
            this.Name = "TranslationEditor";
            this.Size = new System.Drawing.Size(491, 394);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SyntaxHighlightingTextBox syntaxHighlightingTextBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemSave;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveas;
        private System.Windows.Forms.ToolStripMenuItem menuItemExport;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCut;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPaste;
        private System.Windows.Forms.ToolStripMenuItem 查找ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 替换ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelUncertain;
        private System.Windows.Forms.Label labelWindows;
        private System.Windows.Forms.Label labelLines;
        private System.Windows.Forms.Label labelToolong;
        private System.Windows.Forms.Button btnSeek;
        private System.Windows.Forms.Button btnInsertTimeTag;
        private System.Windows.Forms.Label labelToggle;
        private System.Windows.Forms.Label labelFF;
        private System.Windows.Forms.Label labelRW;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnRW;
        private System.Windows.Forms.Button btnFF;
        private System.Windows.Forms.Button btnToggle;

    }
}
