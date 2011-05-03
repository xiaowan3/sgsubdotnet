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
                    var sc = new SplitContainer();
                    if (node.Attributes == null) break;
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        container.Controls.Add(sc);
                        switch (attribute.Name)
                        {
                            case "Orientation":
                                sc.Orientation = (Orientation) Enum.Parse(typeof (Orientation), attribute.Value);
                                break;
                            case "Dock":
                                sc.Dock = (DockStyle) Enum.Parse(typeof (DockStyle), attribute.Value);
                                break;
                            case "SpliterDistance":
                                sc.SplitterDistance = int.Parse(attribute.Value);
                                break;
                            case "BorderStyle":
                                sc.BorderStyle = (BorderStyle) Enum.Parse(typeof (BorderStyle), attribute.Value);
                                break;
                            case "FixedPanel":
                                sc.FixedPanel = (FixedPanel) Enum.Parse(typeof (FixedPanel), attribute.Value);
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
                    if (node.Attributes == null) break;
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
                    if (node.Attributes == null) break;
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
                    if (node.Attributes == null) break;
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
                        var size = new Size();
                        if (node.Attributes == null) break;
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
                        Size = size;
                        foreach (XmlNode subnode in node.ChildNodes)
                        {
                            LayoutLoader(container, subnode);
                        }
                    }
                    break;
                case "SubItemEditor":
                    container.Controls.Add(subItemEditor);
                    if (node.Attributes == null) break;
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
        private IContainer components;

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
            components = new Container();
            AutoScaleMode = AutoScaleMode.None;
            Text = @"SGSUB.Net Tita Russell";
            Icon = Resource.tita;
            AllowDrop = true;


            var xmldoc= new XmlDocument();
            XmlTextReader layoutReader;  
            _startUpPath = Application.StartupPath;

            #region Load Config
            _appFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Application.CompanyName;
            if (!System.IO.Directory.Exists(_appFolderPath))
            {
                System.IO.Directory.CreateDirectory(_appFolderPath);
            }
            if (!System.IO.Directory.Exists(_appFolderPath + @"\config"))
            {
                System.IO.Directory.CreateDirectory(_appFolderPath + @"\config");
            }
            if (!System.IO.File.Exists(_appFolderPath + @"\config\sgscfg.xml"))
            {
                _config = SGSConfig.FromFile(Application.StartupPath + @"\config\sgscfg.xml");
                _config.Save(_appFolderPath + @"\config\sgscfg.xml");
            }
            else
            {
                _config = SGSConfig.FromFile(_appFolderPath + @"\config\sgscfg.xml");
            }

            if (!System.IO.File.Exists(_appFolderPath + @"\config\layout.xml"))
            {
                layoutReader = new XmlTextReader(_startUpPath + @"\config\layout.xml");
                XmlWriter layoutWriter = new XmlTextWriter(_appFolderPath + @"\config\layout.xml", Encoding.Unicode);
                xmldoc.Load(layoutReader);
                xmldoc.WriteContentTo(layoutWriter);
            }
            else
            {
                layoutReader = new XmlTextReader(_startUpPath + @"\config\layout.xml");
                xmldoc.Load(layoutReader);
            }

            #endregion

            #region Build Layout

            if (xmldoc.ChildNodes.Count <= 0) throw new Exception("Wrong XML File");
            XmlNode root = xmldoc.ChildNodes[0];
            var panel = new Panel();

            mainMenu.SuspendLayout();
            panel.SuspendLayout();
            SuspendLayout();

            //Build Mainmenu
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

            Controls.Add(panel);
            Controls.Add(statusStrip);
            Controls.Add(mainMenu);
            MainMenuStrip = mainMenu;

            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
            statusStrip.Items.Add(statusLabel);
            LayoutLoader(panel, root);
            #endregion


            subEditor.Config = _config;

            waveViewer.FFMpegPath = _startUpPath + @"\ffmpeg.exe";
            SubItemEditor.FFMpegPath = _startUpPath + @"\ffmpeg.exe";

            #region Event Handlers
            DragDrop += new DragEventHandler(SgsubMainform_DragDrop);
            DragEnter += new DragEventHandler(SgsubMainform_DragEnter);
            FormClosing += new FormClosingEventHandler(SgsubMainform_FormClosing);

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

        private readonly string[] _allowedExts = { "avi", "mkv", "mp4", "rmvb", "wmv", "ass", "txt" };


        void SgsubMainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AskSave()) e.Cancel = true;
        }

        void SgsubMainform_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])(e.Data.GetData(DataFormats.FileDrop));
                if (files.Length == 1)
                {
                    string ext = (files[0].Substring(files[0].LastIndexOf('.') + 1)).ToLower();
                    if (_allowedExts.Contains(ext))
                        e.Effect = DragDropEffects.Copy;
                }
            }
        }

        void SgsubMainform_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])(e.Data.GetData(DataFormats.FileDrop));
                if (files.Length == 1)
                {
                    string ext = (files[0].Substring(files[0].LastIndexOf('.') + 1)).ToLower();
                    switch (ext)
                    {
                        case "txt":
                            if (AskSave())
                                OpenTxt(files[0]);
                            break;
                        case "ass":
                            if (AskSave())
                                OpenAss(files[0]);
                            break;
                        default:
                            OpenVideo(files[0]);
                            break;
                    }
                    if (_allowedExts.Contains(ext))
                        e.Effect = DragDropEffects.Copy;
                }
            }
        }

        void subItemEditor_ButtonClicked(object sender, EventArgs e)
        {
            subEditor.Focus();
        }

        void KeyConfig_Click(object sender, EventArgs e)
        {
            var keycfg = new KeyConfigForm(_config);
            if (keycfg.ShowDialog() == DialogResult.OK)
                _config.Save();
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
            _currentSub.DefaultAssHead = _config.DefaultAssHead;
            _currentSub.DefaultFormatLine = _config.DefaultFormatLine;
            _currentSub.DefaultFormat = _config.DefaultFormat;
            _currentSub.DefaultLayer = _config.DefaultLayer;
            _currentSub.DefaultMarked = _config.DefaultMarked;
            _currentSub.DefaultStart = _config.DefaultStart;
            _currentSub.DefaultEnd = _config.DefaultEnd;
            _currentSub.DefaultStyle = _config.DefaultStyle;
            _currentSub.DefaultName = _config.DefaultName;
            _currentSub.DefaultActor = _config.DefaultActor;
            _currentSub.DefaultMarginL = _config.DefaultMarginL;
            _currentSub.DefaultMarginR = _config.DefaultMarginR;
            _currentSub.DefaultMarginV = _config.DefaultMarginV;
            _currentSub.DefaultEffect = _config.DefaultEffect;
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
        private Subtitle.AssSub _currentSub;
        private string _subFilename;
        private SGSConfig _config;
        private string _startUpPath;
        private string _appFolderPath;
        #endregion

        void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        void saveSubAs_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog
                          {
                              AddExtension = true,
                              DefaultExt = "ass",
                              Filter = @"ASS Subtitle (*.ass)|*.ass||"
                          };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _subFilename = dlg.FileName;
                _currentSub.WriteAss(_subFilename, Encoding.Unicode);
                subEditor.Edited = false;
            }

        }

        void saveSub_Click(object sender, EventArgs e)
        {
            SaveAssSub();
        }

        void openMedia_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
                          {
                              Filter =
                                  @"Video File (*.mp4;*.mkv;*.avi;*.mpg)|*.mp4;*.mkv;*.avi;*.mpg|All files (*.*)|*.*||"
                          };
            if (dlg.ShowDialog() == DialogResult.OK)
                OpenVideo(dlg.FileName);
        }


        void openTXT_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = @"Text File (*.txt)|*.txt||"};

            if (AskSave() && dlg.ShowDialog() == DialogResult.OK)
                OpenTxt(dlg.FileName);
        }
        void openSub_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog {Filter = @"ASS Subtitle (*.ass)|*.ass||"};
            if (AskSave() && dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    OpenAss(dlg.FileName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK);
                    _currentSub = null;
                    _subFilename = null;
                    SetCurrentSub();
                }
            }
        }

        #region File Operation
        /// <summary>
        /// 打开视频
        /// </summary>
        /// <param name="filename"></param>
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
                var result = MessageBox.Show(string.Format("当前字幕己修改{0}想保存文件吗", Environment.NewLine),
                    @"SGSUB.Net", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
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
            if (_subFilename == null)
            {
                var dlg = new SaveFileDialog
                              {
                                  AddExtension = true,
                                  DefaultExt = "ass",
                                  Filter = @"ASS Subtitle (*.ass)|*.ass||"
                              };
                if (dlg.ShowDialog() == DialogResult.OK)
                    _subFilename = dlg.FileName;
                else
                    return false;
            }
            _currentSub.WriteAss(_subFilename, Encoding.Unicode);
            subEditor.Edited = false;
            return true;
        }


        private void OpenTxt(string filename)
        {
            _currentSub = new Subtitle.AssSub();
            SetDefaultValues();
            _currentSub.LoadText(filename);
            _subFilename = null;
            subEditor.Edited = false;
            SetCurrentSub();
        }

        private void OpenAss(string filename)
        {
            _currentSub = new Subtitle.AssSub();
            _currentSub.LoadAss(filename);
            _subFilename = filename;
            SetCurrentSub();
        }

        private void SetCurrentSub()
        {
            subEditor.CurrentSub = _currentSub;
            waveViewer.CurrentSub = _currentSub;
            subItemEditor.CurrentSub = _currentSub;
        }


        #endregion

    }
}
