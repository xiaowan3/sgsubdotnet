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
        XmlText xmlText;
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
            LayoutLoader(this, root);

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
        #region Controls
        SubEditor subEditor = new SubEditor();
        WaveFormViewer waveViewer = new WaveFormViewer();
        #endregion

        public SgsubMainform()
        {
            InitializeComponent();
        }
    }
}
