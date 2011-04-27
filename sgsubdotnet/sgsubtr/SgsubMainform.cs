using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using SGSControls;

namespace sgsubtr
{
    class SgsubMainform : Form
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

        XmlTextReader layoutReader;
        XmlDocument xmldoc;

        #region Controls
        SubEditor subEditor = new SubEditor();
        WaveFormViewer waveViewer = new WaveFormViewer();
        VideoPlayer.DXVideoPlayer dxVideoPlayer = new VideoPlayer.DXVideoPlayer();

        MenuStrip mainMenu = new MenuStrip();
        ToolStripMenuItem fileMenuItems = new ToolStripMenuItem("文件");
        ToolStripMenuItem openSub = new ToolStripMenuItem("打开时间轴");
        ToolStripMenuItem openTXT = new ToolStripMenuItem("打开翻译文本");
        ToolStripMenuItem openMedia = new ToolStripMenuItem("打开视频");
        ToolStripMenuItem saveSub = new ToolStripMenuItem("保存时间轴");
        ToolStripMenuItem saveSubAs = new ToolStripMenuItem("另存为时间轴");
        ToolStripMenuItem exit = new ToolStripMenuItem("退出");
        
        ToolStripMenuItem ConfigMenuItems = new ToolStripMenuItem("设置");
        ToolStripMenuItem KeyConfig = new ToolStripMenuItem("按键设置");

        StatusStrip statusStrip = new StatusStrip();

        #endregion

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Text = "Form1";
            xmldoc = new XmlDocument();
            layoutReader = new XmlTextReader(@"e:\test\layout.xml");
            xmldoc.Load(layoutReader);
            if (xmldoc.ChildNodes.Count <= 0) throw new Exception("Wrong XML File");
            XmlNode root = xmldoc.ChildNodes[0];
            Panel panel = new Panel();

            this.mainMenu.SuspendLayout();
            panel.SuspendLayout();
            this.SuspendLayout();

            //Build Mainmenu
            mainMenu.Name = "Main Menu";
            mainMenu.Items.Add(fileMenuItems);
            mainMenu.Items.Add(ConfigMenuItems);
            openSub.Image = Resource.openass;
            openSub.ImageTransparentColor = Color.Magenta;

            openTXT.Image = Resource.opentxt;
            openTXT.ImageTransparentColor = Color.Magenta;

            openMedia.Image = Resource.openvideo;
            openMedia.ImageTransparentColor = Color.Magenta;

            saveSub.Image = Resource.save;
            saveSub.ImageTransparentColor = Color.Magenta;

            fileMenuItems.DropDownItems.Add(openSub);
            fileMenuItems.DropDownItems.Add(openTXT);
            fileMenuItems.DropDownItems.Add(openMedia);
            fileMenuItems.DropDownItems.Add(new ToolStripSeparator());
            fileMenuItems.DropDownItems.Add(saveSub);
            fileMenuItems.DropDownItems.Add(saveSubAs);
            fileMenuItems.DropDownItems.Add(new ToolStripSeparator());
            fileMenuItems.DropDownItems.Add(exit);

            ConfigMenuItems.DropDownItems.Add(KeyConfig);

            mainMenu.Size = new Size(200, 28);
            


            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 28);

            this.Controls.Add(panel);
            this.Controls.Add(statusStrip);
            this.Controls.Add(mainMenu);
            this.MainMenuStrip = mainMenu;

            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            panel.ResumeLayout(false);
            panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            LayoutLoader(panel, root);
        }
        private void LayoutLoader(Control container, XmlNode node)
        {
            switch (node.Name)
            {
                case "SplitContainer":
                    SplitContainer sc = new SplitContainer();
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        container.Controls.Add(sc);
                        switch (attribute.Name)
                        {
                            case "Orientation":
                                sc.Orientation = (Orientation)Enum.Parse(typeof(Orientation), attribute.Value);
                                break;
                            case "Dock":
                                sc.Dock = (DockStyle)Enum.Parse(typeof(DockStyle),attribute.Value);
                                break;
                            case "SpliterDistance":
                                sc.SplitterDistance = int.Parse(attribute.Value);
                                break;
                            case "BorderStyle":
                                sc.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), attribute.Value);
                                break;
                            case "FixedPanel":
                                sc.FixedPanel = (FixedPanel)Enum.Parse(typeof(FixedPanel), attribute.Value);
                                break;
                            case "IsSplitterFixed":
                                sc.IsSplitterFixed = Boolean.Parse(attribute.Value);
                                break;

                        }
                    }

                    if (node.ChildNodes.Count >= 1)
                    {
                        LayoutLoader(sc.Panel1, node.ChildNodes[0]);
                    }
                    if (node.ChildNodes.Count >= 2)
                    {
                        LayoutLoader(sc.Panel2, node.ChildNodes[1]);
                    }
                    break;
                case "SubEditor":
                    container.Controls.Add(subEditor);
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        switch (attribute.Name)
                        {
                            case "Dock":
                                subEditor.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), attribute.Value);
                                break;
                        }
                    }
                    break;
                case "WaveFormViewer":
                    container.Controls.Add(waveViewer);
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        switch (attribute.Name)
                        {
                            case "Dock":
                                waveViewer.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), attribute.Value);
                                break;
                        }
                    }
                    break;
                case "VideoPlayer":
                    container.Controls.Add(dxVideoPlayer);
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        switch (attribute.Name)
                        {
                            case "Dock":
                                dxVideoPlayer.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), attribute.Value);
                                break;
                        }
                    }
                    break;
                case "SGSUBLayout":
                    {
                        Size size = new Size();
                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            switch (attribute.Name)
                            {
                                case "Height":
                                    size.Height = int.Parse(attribute.Value);
                                    break;
                                case "Width":
                                    size.Width = int.Parse(attribute.Value);
                                    break;
                            }
                        }
                        this.Size = size;
                        foreach (XmlNode subnode in node.ChildNodes)
                        {
                            LayoutLoader(container, subnode);
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        public SgsubMainform()
        {
            InitializeComponent();
        }
    }
}
