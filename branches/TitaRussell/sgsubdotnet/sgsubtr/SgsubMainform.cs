﻿using System;
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




        #region Controls
        SubEditor subEditor = new SubEditor();
        WaveFormViewer waveViewer = new WaveFormViewer();
        VideoPlayer.DXVideoPlayer dxVideoPlayer = new VideoPlayer.DXVideoPlayer();

        Timer timer = new Timer();

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

        ToolStripMenuItem HelpMenuItem = new ToolStripMenuItem("帮助");
        ToolStripMenuItem AboutSGSUBTR = new ToolStripMenuItem("关于 SGSUB.Net");

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

            StartUpPath = Application.StartupPath;
            #region Build Layout
            XmlDocument xmldoc;
            XmlTextReader layoutReader; xmldoc = new XmlDocument();
            layoutReader = new XmlTextReader(StartUpPath + @"\config\layout.xml");
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
            mainMenu.Items.Add(HelpMenuItem);

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

            HelpMenuItem.DropDownItems.Add(HelpMenuItem);

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
            #endregion


            m_Config = Config.SGSConfig.FromFile(StartUpPath + @"\config\sgscfg.xml");
            subEditor.Config = m_Config;

            waveViewer.FFMpegPath = StartUpPath + @"\ffmpeg.exe";

            #region Event Handlers
            openSub.Click += new EventHandler(openSub_Click);
            openTXT.Click += new EventHandler(openTXT_Click);
            openMedia.Click += new EventHandler(openMedia_Click);
            saveSub.Click += new EventHandler(saveSub_Click);
            saveSubAs.Click += new EventHandler(saveSubAs_Click);
            exit.Click += new EventHandler(exit_Click);

            waveViewer.BTNOpenAss += new EventHandler(openSub_Click);
            waveViewer.BTNOpenMedia += new EventHandler(openMedia_Click);
            waveViewer.BTNOpenTxt += new EventHandler(openTXT_Click);
            waveViewer.BTNSaveAss += new EventHandler(saveSub_Click);
            waveViewer.WaveFormMouseDown += new EventHandler<WaveReader.WFMouseEventArgs>(waveViewer_WaveFormMouseDown);
            waveViewer.PlayerControl += new EventHandler<PlayerControlEventArgs>(subEditor_PlayerControl);

            subEditor.TimeEdit += new EventHandler<TimeEditEventArgs>(subEditor_TimeEdit);
            subEditor.CurrentRowChanged += new EventHandler<CurrentRowChangeEventArgs>(subEditor_CurrentRowChanged);
            subEditor.PlayerControl += new EventHandler<PlayerControlEventArgs>(subEditor_PlayerControl);
            subEditor.Seek += new EventHandler<SeekEventArgs>(subEditor_Seek);

            timer.Tick += new EventHandler(timer_Tick);
            #endregion

        }

        void subEditor_Seek(object sender, SeekEventArgs e)
        {
            switch (e.SeekDirection)
            {
                case SeekDir.Begin:
                    dxVideoPlayer.CurrentPosition = e.SeekOffset;
                    break;
                case SeekDir.CurrentPos:
                    dxVideoPlayer.CurrentPosition += e.SeekOffset;
                    break;
            }
        }

        void subEditor_PlayerControl(object sender, PlayerControlEventArgs e)
        {
            switch (e.ControlCMD)
            {
                case PlayerCommand.Pause:
                    dxVideoPlayer.Pause();
                    break;
                case PlayerCommand.Play:
                    dxVideoPlayer.Play();
                    break;
                case PlayerCommand.Toggle:
                    if (dxVideoPlayer.Paused)
                        dxVideoPlayer.Play();
                    else
                        dxVideoPlayer.Pause();
                    break;
            }
        }

        void subEditor_CurrentRowChanged(object sender, CurrentRowChangeEventArgs e)
        {
            waveViewer.CurrentLineIndex = e.CurrentRowIndex;
        }

        void waveViewer_WaveFormMouseDown(object sender, WaveReader.WFMouseEventArgs e)
        {
            if (dxVideoPlayer.MediaOpened)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        subEditor.EditBeginTime(subEditor.CurrentRowIndex, e.Time);
                        break;
                    case MouseButtons.Right:
                        subEditor.EditEndTime(subEditor.CurrentRowIndex, e.Time);
                        subEditor.CurrentRowIndex++;
                        break;
                }
                waveViewer.RefreshDisplay();
            }
            subEditor.Focus();
        }


        void subEditor_TimeEdit(object sender, TimeEditEventArgs e)
        {
            if (dxVideoPlayer.MediaOpened && !dxVideoPlayer.Paused)
            {
                e.TimeValue = dxVideoPlayer.CurrentPosition;
                e.CancelEvent = false;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (dxVideoPlayer.MediaOpened)
            {
                subEditor.DisplayTime(dxVideoPlayer.CurrentPosition);
                waveViewer.CurrentPosition = dxVideoPlayer.CurrentPosition;
            }
        }

        #region Private Members
        private Subtitle.AssSub m_CurrentSub = null;
        private string m_SubFilename = null;
        private Config.SGSConfig m_Config;
        private string StartUpPath;
        #endregion

        void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void saveSubAs_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void saveSub_Click(object sender, EventArgs e)
        {
            SaveAssSub();
        }

        void openMedia_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Video File (*.mp4;*.mkv;*.avi;*.mpg)|*.mp4;*.mkv;*.avi;*.mpg|All files (*.*)|*.*||";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                OpenVideo(dlg.FileName);
            }
        }

        /// <summary>
        /// 打开视频
        /// </summary>
        /// <param name="file"></param>
        private void OpenVideo(string filename)
        {
            try
            {
                dxVideoPlayer.OpenVideo(filename);
                subEditor.VideoLength = dxVideoPlayer.Duration;
                waveViewer.MediaFilename = filename;
                dxVideoPlayer.Play();
                timer.Interval = 100;
                timer.Start();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        void openTXT_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text File (*.txt)|*.txt||";

            if (AskSave() && dlg.ShowDialog() == DialogResult.OK)
            {
                OpenTxt(dlg.FileName);
            }
        }

        private void OpenTxt(string filename)
        {
            m_CurrentSub = new Subtitle.AssSub();
            SetDefaultValues();
            m_CurrentSub.LoadText(filename);
            m_SubFilename = null;
            subEditor.Edited = false;
            subEditor.CurrentSub = m_CurrentSub;
            waveViewer.CurrentSub = m_CurrentSub;
        }

        void openSub_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "ASS Subtitle (*.ass)|*.ass||";
            if (AskSave() && dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_CurrentSub = new Subtitle.AssSub();
                    SetDefaultValues();
                    m_CurrentSub.LoadAss(dlg.FileName);
                    m_SubFilename = dlg.FileName;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK);
                    m_CurrentSub = null;
                    m_SubFilename = null;
                }
                subEditor.CurrentSub = m_CurrentSub;
                waveViewer.CurrentSub = m_CurrentSub;
            }

        }

        private void SetDefaultValues()
        {
            m_CurrentSub.DefaultAssHead = m_Config.DefaultAssHead;
            m_CurrentSub.DefaultFormatLine = m_Config.DefaultFormatLine;
            m_CurrentSub.DefaultFormat = m_Config.DefaultFormat;
            m_CurrentSub.DefaultLayer = m_Config.DefaultLayer;
            m_CurrentSub.DefaultMarked = m_Config.DefaultMarked;
            m_CurrentSub.DefaultStart = m_Config.DefaultStart;
            m_CurrentSub.DefaultEnd = m_Config.DefaultEnd;
            m_CurrentSub.DefaultStyle = m_Config.DefaultStyle;
            m_CurrentSub.DefaultName = m_Config.DefaultName;
            m_CurrentSub.DefaultActor = m_Config.DefaultActor;
            m_CurrentSub.DefaultMarginL = m_Config.DefaultMarginL;
            m_CurrentSub.DefaultMarginR = m_Config.DefaultMarginR;
            m_CurrentSub.DefaultMarginV = m_Config.DefaultMarginV;
            m_CurrentSub.DefaultEffect = m_Config.DefaultEffect;
        }

        /// <summary>
        /// 如果字幕己修改，询问是否保存
        /// </summary>
        /// <returns>true:继续, false:取消操作</returns>
        private bool AskSave()
        {
            if (subEditor.Edited)
            {
                DialogResult result = MessageBox.Show("当前字幕己修改" + Environment.NewLine + "想保存文件吗",
                    "SGSUB.Net", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.Yes:
                        return SaveAssSub();
                    case DialogResult.No:
                        return true;
                    case DialogResult.Cancel:
                        return false;
                    default:
                        return false;
                }
            }
            return true;
        }

        private bool SaveAssSub()
        {
            if (m_SubFilename == null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.AddExtension = true;
                dlg.DefaultExt = "ass";
                dlg.Filter = "ASS Subtitle (*.ass)|*.ass||";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    m_SubFilename = dlg.FileName;
                }
                else
                {
                    return false;
                }
            }
            m_CurrentSub.WriteAss(m_SubFilename, Encoding.Unicode);
            subEditor.Edited = false;
            return true;
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
                                sc.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), attribute.Value);
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