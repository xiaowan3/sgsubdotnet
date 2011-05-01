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
using Config;

namespace sgsubtr
{
    class SgsubMainform : Form
    {
        public SgsubMainform()
        {
            InitializeComponent();
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
                case "SubItemEditor":
                    container.Controls.Add(subItemEditor);
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        switch (attribute.Name)
                        {
                            case "Dock":
                                subItemEditor.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), attribute.Value);
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }


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
        SubItemEditor subItemEditor = new SubItemEditor();
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
        ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("复制:Ctrl-C  粘贴:Ctrl-V  清空单元格:Delete  删除选中行:Ctrl-Delete");


        #endregion

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Text = "SGSUB.Net Tita Russell";
            this.Icon = Resource.tita;
            this.AllowDrop = true;


            XmlDocument xmldoc;
            XmlTextReader layoutReader; xmldoc = new XmlDocument();
            StartUpPath = Application.StartupPath;

            #region Load Config
            AppFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Application.CompanyName;
            if (!System.IO.Directory.Exists(AppFolderPath))
            {
                System.IO.Directory.CreateDirectory(AppFolderPath);
            }
            if (!System.IO.Directory.Exists(AppFolderPath + @"\config"))
            {
                System.IO.Directory.CreateDirectory(AppFolderPath + @"\config");
            }
            if (!System.IO.File.Exists(AppFolderPath + @"\config\sgscfg.xml"))
            {
                m_Config = SGSConfig.FromFile(Application.StartupPath + @"\config\sgscfg.xml");
                m_Config.Save(AppFolderPath + @"\config\sgscfg.xml");
            }
            else
            {
                m_Config = SGSConfig.FromFile(AppFolderPath + @"\config\sgscfg.xml");
            }

            if (!System.IO.File.Exists(AppFolderPath + @"\config\layout.xml"))
            {
                layoutReader = new XmlTextReader(StartUpPath + @"\config\layout.xml");
                XmlWriter layoutWriter = new XmlTextWriter(AppFolderPath + @"\config\layout.xml", Encoding.Unicode);
                xmldoc.Load(layoutReader);
                xmldoc.WriteContentTo(layoutWriter);
            }
            else
            {
                layoutReader = new XmlTextReader(StartUpPath + @"\config\layout.xml");
                xmldoc.Load(layoutReader);
            }

            #endregion

            #region Build Layout

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

            HelpMenuItem.DropDownItems.Add(AboutSGSUBTR);

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
            statusStrip.Items.Add(statusLabel);
            LayoutLoader(panel, root);
            #endregion


            subEditor.Config = m_Config;

            waveViewer.FFMpegPath = StartUpPath + @"\ffmpeg.exe";
            SubItemEditor.FFMpegPath = StartUpPath + @"\ffmpeg.exe";

            #region Event Handlers
            this.DragDrop += new DragEventHandler(SgsubMainform_DragDrop);
            this.DragEnter += new DragEventHandler(SgsubMainform_DragEnter);
            this.FormClosing += new FormClosingEventHandler(SgsubMainform_FormClosing);

            openSub.Click += new EventHandler(openSub_Click);
            openTXT.Click += new EventHandler(openTXT_Click);
            openMedia.Click += new EventHandler(openMedia_Click);
            saveSub.Click += new EventHandler(saveSub_Click);
            saveSubAs.Click += new EventHandler(saveSubAs_Click);
            exit.Click += new EventHandler(exit_Click);
            KeyConfig.Click += new EventHandler(KeyConfig_Click);

            waveViewer.BTNOpenAss += new EventHandler(openSub_Click);
            waveViewer.BTNOpenMedia += new EventHandler(openMedia_Click);
            waveViewer.BTNOpenTxt += new EventHandler(openTXT_Click);
            waveViewer.BTNSaveAss += new EventHandler(saveSub_Click);
            waveViewer.WaveFormMouseDown += new EventHandler<WaveReader.WFMouseEventArgs>(waveViewer_WaveFormMouseDown);
            waveViewer.PlayerControl += new EventHandler<PlayerControlEventArgs>(PlayerControlEventHandler);

            subEditor.TimeEdit += new EventHandler<TimeEditEventArgs>(subEditor_TimeEdit);
            subEditor.CurrentRowChanged += new EventHandler<CurrentRowChangeEventArgs>(subEditor_CurrentRowChanged);
            subEditor.PlayerControl += new EventHandler<PlayerControlEventArgs>(PlayerControlEventHandler);
            subEditor.Seek += new EventHandler<SeekEventArgs>(subEditor_Seek);
            subEditor.KeySaveAss += new EventHandler(saveSub_Click);

            subItemEditor.ButtonClicked += new EventHandler<EventArgs>(subItemEditor_ButtonClicked);
            subItemEditor.PlayerControl += new EventHandler<PlayerControlEventArgs>(PlayerControlEventHandler);

            timer.Tick += new EventHandler(timer_Tick);
            #endregion

        }

        private string[] AllowedExts = { "avi", "mkv", "mp4", "rmvb", "wmv", "ass", "txt" };


        void SgsubMainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AskSave())
            {
                e.Cancel = true;
            }
        }

        void SgsubMainform_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])(e.Data.GetData(DataFormats.FileDrop));
                if (files.Length == 1)
                {
                    string ext = (files[0].Substring(files[0].LastIndexOf('.') + 1)).ToLower();
                    if (AllowedExts.Contains(ext))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                }
            }
        }

        void SgsubMainform_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])(e.Data.GetData(DataFormats.FileDrop));
                if (files.Length == 1)
                {
                    string ext = (files[0].Substring(files[0].LastIndexOf('.') + 1)).ToLower();
                    switch (ext)
                    {
                        case "txt":
                            if (AskSave())
                            {
                                OpenTxt(files[0]);
                            }
                            break;
                        case "ass":
                            if (AskSave())
                            {
                                OpenAss(files[0]);
                            }
                            break;
                        default:
                            OpenVideo(files[0]);
                            break;
                    }
                    if (AllowedExts.Contains(ext))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                }
            }
        }

        void subItemEditor_ButtonClicked(object sender, EventArgs e)
        {
            subEditor.Focus();
        }

        void KeyConfig_Click(object sender, EventArgs e)
        {
            KeyConfigForm keycfg = new KeyConfigForm(m_Config);
            if (keycfg.ShowDialog() == DialogResult.OK)
            {
                m_Config.Save();
            }
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


        void PlayerControlEventHandler(object sender, PlayerControlEventArgs e)
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
            subItemEditor.CurrentIndex = e.CurrentRowIndex;
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
        private string AppFolderPath;
        #endregion

        void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void saveSubAs_Click(object sender, EventArgs e)
        {
            if (m_CurrentSub == null) return;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.DefaultExt = "ass";
            dlg.Filter = "ASS Subtitle (*.ass)|*.ass||";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_SubFilename = dlg.FileName;
                m_CurrentSub.WriteAss(m_SubFilename, Encoding.Unicode);
                subEditor.Edited = false;
            }

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


        void openTXT_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text File (*.txt)|*.txt||";

            if (AskSave() && dlg.ShowDialog() == DialogResult.OK)
            {
                OpenTxt(dlg.FileName);
            }
        }
        void openSub_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "ASS Subtitle (*.ass)|*.ass||";
            if (AskSave() && dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    OpenAss(dlg.FileName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK);
                    m_CurrentSub = null;
                    m_SubFilename = null;
                    SetCurrentSub();
                }
            }

        }



        #region File Operation
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
                subItemEditor.MediaFile = filename;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
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
            if (m_CurrentSub == null) return false;
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


        private void OpenTxt(string filename)
        {
            m_CurrentSub = new Subtitle.AssSub();
            SetDefaultValues();
            m_CurrentSub.LoadText(filename);
            m_SubFilename = null;
            subEditor.Edited = false;
            SetCurrentSub();
        }

        private void OpenAss(string filename)
        {
            m_CurrentSub = new Subtitle.AssSub();
            m_CurrentSub.LoadAss(filename);
            m_SubFilename = filename;
            SetCurrentSub();
        }

        private void SetCurrentSub()
        {
            subEditor.CurrentSub = m_CurrentSub;
            waveViewer.CurrentSub = m_CurrentSub;
            subItemEditor.CurrentSub = m_CurrentSub;
        }


        #endregion

    }
}
