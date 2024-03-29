﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using SGS.Controls;
using SGS.Datatype;
using SGS.VideoPlayer;

namespace SgsubTR
{
    class SgsubMainform : Form
    {
        public SgsubMainform()
        {
            _startUpPath = Application.StartupPath;
            _appFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + Application.CompanyName;
            //Setup path
            SGSConfig.FFMpegPath = _startUpPath + @"\ffmpeg.exe";
            SGSConfig.DefaultCfgPath = _startUpPath + @"\config\";
            SGSConfig.AutosavePath = _appFolderPath + @"\autosave\";
            SGSConfig.ConfigPath = _appFolderPath + @"\config\";
            SGSConfig.MPlayerPath = _startUpPath + @"\mplayer.exe";

            SGSConfig.WithMPlayer = System.IO.File.Exists(SGSConfig.MPlayerPath);

            if (!System.IO.Directory.Exists(_appFolderPath))
            {
                System.IO.Directory.CreateDirectory(_appFolderPath);
            }
            if (!System.IO.Directory.Exists(_appFolderPath + @"\config"))
            {
                System.IO.Directory.CreateDirectory(_appFolderPath + @"\config");
            }
            var defaultcfg = SGSConfig.FromFile(Application.StartupPath + @"\config\config.xml");

            if (!System.IO.File.Exists(_appFolderPath + @"\config\config.xml"))
            {
                _config = defaultcfg;
                _config.Save(_appFolderPath + @"\config\config.xml");
            }
            else
            {
                _config = SGSConfig.FromFile(_appFolderPath + @"\config\config.xml");
                if (!defaultcfg.Compatible(_config))
                {
                    _config = defaultcfg;
                    _config.Save(_appFolderPath + @"\config\config.xml");
                }
            }
            switch (_config.Player)
            {
                case PlayerType.DShowPlayer:
                    _playerControl = new DShowPlayer();
                    break;
                case PlayerType.MDXPlayer:
                    _playerControl = new DXVideoPlayer();
                    break;
                case PlayerType.WMPlayer:
                    _playerControl = new WMPlayer();

                    break;
                case PlayerType.MPlayer:
                    if (SGSConfig.WithMPlayer)
                    {
                        var mplayer = new MPlayer();
                        mplayer.MPlayerPath = SGSConfig.MPlayerPath;
                        _playerControl = mplayer;
                    }
                    else
                        _playerControl = new WMPlayer();
                    break;
            }
            _player = (ISGSPlayer)_playerControl;

            InitializeComponent();
        }

        private void LayoutLoader(Control container, XmlNode node)
        {
            switch (node.Name)
            {
                case "SplitContainer":
                    var sc = new SplitContainer();
                    if (node.Attributes != null)
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
                    _subEditerContainer = container.Controls;
                    container.Controls.Add(subEditor);
                    if (node.Attributes != null)
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
                    if (node.Attributes != null)
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
                    container.Controls.Add(_playerControl);
                    if (node.Attributes != null)
                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            switch (attribute.Name)
                            {
                                case "Dock":
                                    _playerControl.Dock = (DockStyle)Enum.Parse(typeof(DockStyle), attribute.Value);
                                    break;
                            }
                        }
                    break;
                case "SGSUBLayout":
                    {
                        var size = new Size();
                        if (node.Attributes != null)
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
        //  SubItemEditor subItemEditor = new SubItemEditor();
        // VideoPlayer.PlayerControl dxVideoPlayer = new VideoPlayer.DXVideoPlayer();
        private Control _playerControl;
        private ISGSPlayer _player;
        private TranslationEditor translationEditor = new TranslationEditor();

        Timer timer = new Timer();

        MenuStrip mainMenu = new MenuStrip();
        ToolStripMenuItem _menuFile = new ToolStripMenuItem("文件");
        ToolStripMenuItem _menuItemOpenSub = new ToolStripMenuItem("打开时间轴");
        ToolStripMenuItem _menuItemOpenTXT = new ToolStripMenuItem("打开翻译文本");
        ToolStripMenuItem _menuItemOpenMedia = new ToolStripMenuItem("打开视频");
        ToolStripMenuItem _menuItemSaveSub = new ToolStripMenuItem("保存时间轴");
        ToolStripMenuItem _menuItemSaveSubAs = new ToolStripMenuItem("另存为时间轴");
        ToolStripMenuItem _menuItemAutoSaveRecord = new ToolStripMenuItem("查看自动保存记录");
        ToolStripMenuItem _menuItemExit = new ToolStripMenuItem("退出");

        ToolStripMenuItem _menuEdit = new ToolStripMenuItem("编辑");
        ToolStripMenuItem _menuItemUndoSub = new ToolStripMenuItem("撤消");
        ToolStripMenuItem _menuItemCopySub = new ToolStripMenuItem("复制");
        ToolStripMenuItem _menuItemPasteSub = new ToolStripMenuItem("粘贴");

        ToolStripMenuItem _menuConfig = new ToolStripMenuItem("设置");
        ToolStripMenuItem _menuItemKeyConfig = new ToolStripMenuItem("按键设置");
        ToolStripMenuItem _menuItemCustomize = new ToolStripMenuItem("自定义");
        ToolStripMenuItem _menuItemTranslationMode = new ToolStripMenuItem("翻译模式");

        ToolStripMenuItem _menuHelp = new ToolStripMenuItem("帮助");
        ToolStripMenuItem _menuItemAboutSGSUBTR = new ToolStripMenuItem("关于 SGSUB.Net");

        StatusStrip statusStrip = new StatusStrip();
        ToolStripStatusLabel statusLabel = new ToolStripStatusLabel();
        //Translation Mode menu items.
        ToolStripMenuItem _menuItemOpenScript = new ToolStripMenuItem("打开翻译原稿");
        ToolStripMenuItem _menuItemExportTxt = new ToolStripMenuItem("导出翻译文本");
        ToolStripMenuItem _menuItemSaveTrn = new ToolStripMenuItem("保存翻译原稿");
        ToolStripMenuItem _menuItemSaveTrnAs = new ToolStripMenuItem("翻译原稿另存为");
        ToolStripMenuItem _menuItemTrnAutoSave = new ToolStripMenuItem("查看自动保存记录");

        ToolStripMenuItem _menuItemUndoTrn = new ToolStripMenuItem("撤消");
        ToolStripMenuItem _menuItemCutTrn = new ToolStripMenuItem("剪切");
        ToolStripMenuItem _menuItemCopyTrn = new ToolStripMenuItem("复制");
        ToolStripMenuItem _menuItemPasteTrn = new ToolStripMenuItem("粘贴");

        ToolStripMenuItem _menuItemTimingMode = new ToolStripMenuItem("时间轴模式");

        #endregion

        private void buildMenuItems()
        {
            //Build mainMenu
            mainMenu.Items.Add(_menuFile);
            mainMenu.Items.Add(_menuEdit);
            mainMenu.Items.Add(_menuConfig);
            mainMenu.Items.Add(_menuHelp);

            _menuItemOpenSub.Image = Properties.Resources.openass;
            _menuItemOpenSub.ImageTransparentColor = Color.Magenta;

            _menuItemOpenTXT.Image = Properties.Resources.opentxt;
            _menuItemOpenTXT.ImageTransparentColor = Color.Magenta;

            _menuItemOpenMedia.Image = Properties.Resources.openvideo;
            _menuItemOpenMedia.ImageTransparentColor = Color.Magenta;

            _menuItemSaveSub.Image = Properties.Resources.save;
            _menuItemSaveSub.ImageTransparentColor = Color.Magenta;

            _menuItemSaveTrn.Image = Properties.Resources.save;
            _menuItemSaveTrn.ImageTransparentColor = Color.Magenta;

            _menuFile.DropDownItems.Add(_menuItemOpenSub);
            _menuFile.DropDownItems.Add(_menuItemOpenTXT);
            _menuFile.DropDownItems.Add(_menuItemOpenMedia);
            _menuFile.DropDownItems.Add(new ToolStripSeparator());
            _menuFile.DropDownItems.Add(_menuItemSaveSub);
            _menuFile.DropDownItems.Add(_menuItemSaveSubAs);
            _menuFile.DropDownItems.Add(_menuItemAutoSaveRecord);
            _menuFile.DropDownItems.Add(new ToolStripSeparator());
            _menuFile.DropDownItems.Add(_menuItemExit);

            _menuEdit.DropDownItems.Add(_menuItemUndoSub);
            _menuEdit.DropDownItems.Add(new ToolStripSeparator());
            _menuEdit.DropDownItems.Add(_menuItemCopySub);
            _menuEdit.DropDownItems.Add(_menuItemPasteSub);

            _menuConfig.DropDownItems.Add(_menuItemKeyConfig);
            _menuConfig.DropDownItems.Add(_menuItemCustomize);
            _menuConfig.DropDownItems.Add(_menuItemTranslationMode);

            _menuHelp.DropDownItems.Add(_menuItemAboutSGSUBTR);

            mainMenu.Size = new Size(200, 28);


            _menuItemOpenSub.Click += new EventHandler(openSub_Click);
            _menuItemOpenTXT.Click += new EventHandler(openTXT_Click);
            _menuItemOpenMedia.Click += new EventHandler(openMedia_Click);
            _menuItemOpenScript.Click += new EventHandler(openTranslationScript_Click);
            _menuItemExportTxt.Click += new EventHandler(exportTranslation_Click);
            _menuItemSaveTrn.Click += new EventHandler(saveTranslation_Click);
            _menuItemSaveTrnAs.Click += new EventHandler(saveTranslationas_Click);
            _menuItemSaveSub.Click += new EventHandler(saveSub_Click);
            _menuItemSaveSubAs.Click += new EventHandler(saveSubAs_Click);
            _menuItemAutoSaveRecord.Click += new EventHandler(_menuItemAutoSaveRecord_Click);
            _menuItemTrnAutoSave.Click += new EventHandler(
                (sender, e) => translationEditor.ShowAutosaveDlg());
            _menuItemExit.Click += new EventHandler(exit_Click);

            _menuItemUndoSub.Click += new EventHandler((s, e) => subEditor.Undo());
            _menuItemCopySub.Click += new EventHandler((s, e) => subEditor.Copy());
            _menuItemPasteSub.Click += new EventHandler((s, e) => subEditor.Paste());
            _menuItemUndoTrn.Click += new EventHandler((s, e) => translationEditor.Undo());
            _menuItemCopyTrn.Click += new EventHandler((s, e) => translationEditor.Copy());
            _menuItemCutTrn.Click += new EventHandler((s, e) => translationEditor.Cut());
            _menuItemPasteTrn.Click += new EventHandler((s, e) => translationEditor.Paste());

            _menuItemKeyConfig.Click += new EventHandler(
                (s, e) =>
                    {
                        var keycfg = new KeyConfigForm(_config);
                        if (keycfg.ShowDialog() == DialogResult.OK)
                        {
                            _config.Save();
                            updateConfig();
                        }
                    });
            _menuItemCustomize.Click += new EventHandler(
                (s, e) =>
                    {
                        var cfgform = new ConfigForm(_config);
                        cfgform.ShowDialog();
                        updateConfig();
                    });
            _menuItemTranslationMode.Click += new EventHandler((s, e) => SetTranslationMode());
            _menuItemTimingMode.Click += new EventHandler((s, e) => SetTimingMode());
            _menuItemAboutSGSUBTR.Click += new EventHandler((s,e)=>(new AboutBox()).Show());

        }


        private void SetTranslationMode()
        {
            if (!AskSave()) return;
            _workMode = WorkMode.TranslationMode;
            _currentSub = null;
            SetCurrentSub();
            _subEditerContainer.Clear();
            _subEditerContainer.Add(translationEditor);
            translationEditor.Dock = DockStyle.Fill;
            _messageMode = 2;
            statusLabel.Text = StatusMessages[_messageMode];

            _menuFile.DropDownItems.Clear();
            _menuFile.DropDownItems.Add(_menuItemOpenScript);
            _menuFile.DropDownItems.Add(_menuItemOpenMedia);
            _menuFile.DropDownItems.Add(new ToolStripSeparator());
            _menuFile.DropDownItems.Add(_menuItemSaveTrn);
            _menuFile.DropDownItems.Add(_menuItemSaveTrnAs);
            _menuFile.DropDownItems.Add(_menuItemExportTxt);
            _menuFile.DropDownItems.Add(_menuItemTrnAutoSave);
            _menuFile.DropDownItems.Add(new ToolStripSeparator());
            _menuFile.DropDownItems.Add(_menuItemExit);

            _menuEdit.DropDownItems.Clear();
            _menuEdit.DropDownItems.Add(_menuItemUndoTrn);
            _menuEdit.DropDownItems.Add(new ToolStripSeparator());
            _menuEdit.DropDownItems.Add(_menuItemCutTrn);
            _menuEdit.DropDownItems.Add(_menuItemCopyTrn);
            _menuEdit.DropDownItems.Add(_menuItemPasteTrn);

            _menuConfig.DropDownItems.Clear();
            _menuConfig.DropDownItems.Add(_menuItemKeyConfig);
            _menuConfig.DropDownItems.Add(_menuItemCustomize);
            _menuConfig.DropDownItems.Add(_menuItemTimingMode);

        }
        private void SetTimingMode()
        {
            _workMode = WorkMode.TimingMode;
            _subEditerContainer.Clear();
            _subEditerContainer.Add(subEditor);
            translationEditor.Dock = DockStyle.Fill;
            _messageMode = 0;
            statusLabel.Text = StatusMessages[_messageMode];

            _menuFile.DropDownItems.Clear();
            _menuFile.DropDownItems.Add(_menuItemOpenSub);
            _menuFile.DropDownItems.Add(_menuItemOpenTXT);
            _menuFile.DropDownItems.Add(_menuItemOpenMedia);
            _menuFile.DropDownItems.Add(new ToolStripSeparator());
            _menuFile.DropDownItems.Add(_menuItemSaveSub);
            _menuFile.DropDownItems.Add(_menuItemSaveSubAs);
            _menuFile.DropDownItems.Add(_menuItemAutoSaveRecord);
            _menuFile.DropDownItems.Add(new ToolStripSeparator());
            _menuFile.DropDownItems.Add(_menuItemExit);

            _menuEdit.DropDownItems.Clear();
            _menuEdit.DropDownItems.Add(_menuItemUndoSub);
            _menuEdit.DropDownItems.Add(new ToolStripSeparator());
            _menuEdit.DropDownItems.Add(_menuItemCopySub);
            _menuEdit.DropDownItems.Add(_menuItemPasteSub);

            _menuConfig.DropDownItems.Clear();
            _menuConfig.DropDownItems.Add(_menuItemKeyConfig);
            _menuConfig.DropDownItems.Add(_menuItemCustomize);
            _menuConfig.DropDownItems.Add(_menuItemTranslationMode);
        }
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            AutoScaleMode = AutoScaleMode.None;
            Text = @"SGSUB.Net Tita Russell";
            Icon = Properties.Resources.tita;
            AllowDrop = true;
            statusLabel.Text = StatusMessages[_messageMode];

            XmlTextReader layoutReader;
            var xmldoc = new XmlDocument();


            #region Load Config

            //Load Layout
            var layoutfilename = string.Format(@"{0}\config\{1}.layout", _startUpPath, _config.LayoutName);
            if (!System.IO.File.Exists(layoutfilename))
            {
                layoutReader = new XmlTextReader(_startUpPath + @"\config\default.layout");
                xmldoc.Load(layoutReader);
                _config.LayoutName = "default";
                _config.Save();
            }
            else
            {
                layoutReader = new XmlTextReader(layoutfilename);
                xmldoc.Load(layoutReader);
            }

            updateConfig();


            //AutoSave
            _autosave = new SGSAutoSave();
            _autosave.DeleteOld(_config.AutoSaveLifeTime);
            translationEditor.SetAutosavePath(_appFolderPath + @"\autosave");

            #endregion

            #region Build Layout

            if (xmldoc.ChildNodes.Count <= 0) throw new Exception("Wrong XML File");
            XmlNode root = xmldoc.ChildNodes.Cast<XmlNode>().Where(node => node.Name == "SGSUBLayout").FirstOrDefault();

            if (root == null)
            {
                MessageBox.Show(@"无法读取布局文件，使用默认布局", @"错误", MessageBoxButtons.OK);
                var defaultReader = new XmlTextReader(_startUpPath + @"\config\layout.xml");
                xmldoc.Load(defaultReader);
                root = xmldoc.ChildNodes[0];
            }
            var panel = new Panel();

            mainMenu.SuspendLayout();
            panel.SuspendLayout();
            SuspendLayout();

            //Build Mainmenu
            buildMenuItems();




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


            subEditor.Autosave = _autosave;

            _player.Init();

            #region Event Handlers
            DragDrop += new DragEventHandler(SgsubMainform_DragDrop);
            DragEnter += new DragEventHandler(SgsubMainform_DragEnter);
            FormClosing += new FormClosingEventHandler(SgsubMainform_FormClosing);




            waveViewer.BTNOpenAss += new EventHandler(openSub_Click);
            waveViewer.BTNOpenMedia += new EventHandler(openMedia_Click);
            waveViewer.BTNOpenTxt += new EventHandler(openTXT_Click);
            waveViewer.BTNSaveAss += new EventHandler(saveSub_Click);
            waveViewer.WaveFormMouseDown += new EventHandler<SGS.WaveReader.WFMouseEventArgs>(
                (sender, e) =>
                {
                    if (_player.MediaOpened)
                    {
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                subEditor.EditStartTime(subEditor.CurrentRowIndex, e.Time);
                                break;
                            case MouseButtons.Right:
                                subEditor.EditEndTime(subEditor.CurrentRowIndex, e.Time);
                                subEditor.CurrentRowIndex++;
                                break;
                        }
                        waveViewer.RefreshDisplay();
                    }
                    subEditor.Focus();
                });
            waveViewer.PlayerControl += new EventHandler<PlayerControlEventArgs>(PlayerControlEventHandler);
            
            waveViewer.MouseLeave += new EventHandler(
                (sender, e) => statusLabel.Text = StatusMessages[_messageMode]);
            waveViewer.MouseEnter += new EventHandler(
                (sender, e) => statusLabel.Text = StatusMessages[1]);

            subEditor.PlayerControl += new EventHandler<PlayerControlEventArgs>(PlayerControlEventHandler);
            subEditor.Seek += new EventHandler<SeekEventArgs>(subEditor_Seek);
            subEditor.KeySaveAss += new EventHandler(saveSub_Click);
            subEditor.TimeEdit += new EventHandler<TimeEditEventArgs>(subEditor_TimeEdit);

            subEditor.CurrentRowChanged += new EventHandler<CurrentRowChangeEventArgs>(
                (sender, e) => waveViewer.CurrentLineIndex = e.CurrentRowIndex);

            subEditor.AutosaveEvent += new EventHandler(
                (sender, e) =>
                {
                    if (_currentSub != null)
                        _autosave.SaveHistory(_currentSub, _subFilename);
                });

            translationEditor.SeekPlayer += new EventHandler<SeekEventArgs>(subEditor_Seek);
            translationEditor.TimeEdit += new EventHandler<TimeEditEventArgs>(subEditor_TimeEdit);
            translationEditor.PlayerControl += new EventHandler<PlayerControlEventArgs>(PlayerControlEventHandler);

            timer.Tick += new EventHandler(timer_Tick);
            #endregion

        }

        #region Menu Event Handlers

         void _menuItemAutoSaveRecord_Click(object sender, EventArgs e)
        {
            var saveDlg = new AutoSaveForm(_autosave);
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                _currentSub = saveDlg.Sub;
                _subFilename = null;
                subEditor.Edited = false;
                SetCurrentSub();
            }
        }

        #endregion

        #region File Event Handlers

        void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        void saveSubAs_Click(object sender, EventArgs e)
        {
            if (_currentSub == null) return;
            var dlg = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "ass",
                Filter = @"ASS Subtitle (*.ass)|*.ass||"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _subFilename = dlg.FileName;
                _currentSub.Save(_subFilename, Encoding.Unicode);
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
            var dlg = new OpenFileDialog { Filter = @"Text File (*.txt)|*.txt||" };

            if (AskSave() && dlg.ShowDialog() == DialogResult.OK)
                OpenTxt(dlg.FileName);
        }
        void openSub_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = @"ASS Subtitle (*.ass)|*.ass||" };
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

        void saveTranslationas_Click(object sender, EventArgs e)
        {
            translationEditor.SaveAs();
        }



        void saveTranslation_Click(object sender, EventArgs e)
        {
            translationEditor.Save();
        }

        void exportTranslation_Click(object sender, EventArgs e)
        {
            translationEditor.ExportPlainScript();
        }

        void openTranslationScript_Click(object sender, EventArgs e)
        {
            translationEditor.Open();
        }

        #endregion

        #region SubEditor Event Handlers


        void subEditor_Seek(object sender, SeekEventArgs e)
        {
            switch (e.SeekDirection)
            {
                case SeekDir.Begin:
                    _player.CurrentPosition = e.SeekOffset;
                    break;
                case SeekDir.CurrentPos:
                    _player.CurrentPosition += e.SeekOffset;
                    break;
            }
        }


        void subEditor_TimeEdit(object sender, TimeEditEventArgs e)
        {
            if (_player.MediaOpened)
            {
                e.TimeValue = _player.CurrentPosition;
                e.CancelEvent = false;
                e.Paused = _player.Paused;
            }
        }

        #endregion

        #region Player Event Handler

        void PlayerControlEventHandler(object sender, PlayerControlEventArgs e)
        {
            switch (e.ControlCMD)
            {
                case PlayerCommand.Pause:
                    _player.Pause();
                    break;
                case PlayerCommand.Play:
                    _player.Play();
                    break;
                case PlayerCommand.Toggle:
                    _player.TogglePause();
                    break;
                case PlayerCommand.Step:
                    _player.Step();
                    break;
            }
        }


        #endregion

        #region Other
        void SgsubMainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AskSave()) e.Cancel = true;
            else
            {
                _player.Uninit();
            }
        }

        void SgsubMainform_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            var files = (string[])(e.Data.GetData(DataFormats.FileDrop));
            if (files.Length == 1)
            {
                string ext = (files[0].Substring(files[0].LastIndexOf('.') + 1)).ToLower();
                if (AllowedExts.Contains(ext))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        void SgsubMainform_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            var files = (string[])(e.Data.GetData(DataFormats.FileDrop));
            if (files.Length != 1) return;

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


        /// <summary>
        /// 每个定时器周期刷新字幕的显示
        /// </summary>
        void timer_Tick(object sender, EventArgs e)
        {
            if (_player.MediaOpened)
            {
                if (Math.Abs(_player.Duration - _lastDuration) > 0.01)
                {
                    subEditor.VideoLength = _player.Duration;
                    _lastDuration = _player.Duration;
                }
                subEditor.DisplayTime(_player.CurrentPosition);
                waveViewer.CurrentPosition = _player.CurrentPosition;
            }
        }


        #endregion


        #region Private Members

        private Control.ControlCollection _subEditerContainer;

        private SubStationAlpha _currentSub;
        private string _subFilename;
        private SGSConfig _config;
        private SGSAutoSave _autosave;
        private string _startUpPath;
        private string _appFolderPath;

        private string[] AllowedExts = { "avi", "mkv", "mp4", "rmvb", "wmv", "ass", "txt" };

        private string[] StatusMessages =
        {
            "时间轴模式  复制:Ctrl+C  粘贴:Ctrl+V  清空单元格:Delete  删除选中行:Ctrl-Delete",
            "鼠标左键:设置字幕开始时间   鼠标右键:设置字幕结束时间",
            "翻译模式  复制:Ctrl+C  粘贴:Ctrl+V  撤消:Ctrl+Z  重做:Ctrl+Y"            
        };

        private int _messageMode;

        private WorkMode _workMode = WorkMode.TimingMode;

        private double _lastDuration;
        #endregion



        #region File Operation

        /// <summary>
        /// 打开视频
        /// </summary>
        private void OpenVideo(string filename)
        {
            try
            {
                _player.OpenVideo(filename);
                subEditor.VideoLength = _player.Duration;
                subEditor.MediaFile = filename;
                subEditor.EnableStep = _player.CanStep;
                waveViewer.MediaFilename = filename;
                _player.Play();
                timer.Interval = 100;
                timer.Start();
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
            switch (_workMode)
            {
                case WorkMode.TimingMode:

                    if (subEditor.Edited)
                    {
                        DialogResult result = MessageBox.Show(string.Format("当前字幕己修改{0}想保存文件吗", Environment.NewLine),
                                                              @"SGSUB.Net", MessageBoxButtons.YesNoCancel,
                                                              MessageBoxIcon.Warning);
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
                case WorkMode.TranslationMode:
                    if (translationEditor.Edited)
                    {
                        DialogResult result = MessageBox.Show(string.Format("当前内容己修改{0}想保存文件吗", Environment.NewLine),
                                                              @"SGSUB.Net", MessageBoxButtons.YesNoCancel,
                                                              MessageBoxIcon.Warning);
                        switch (result)
                        {
                            case DialogResult.Yes:
                                return translationEditor.Save();
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
            return true;
        }



        private bool SaveAssSub()
        {
            if (_currentSub == null) return false;
            if (_subFilename == null)
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
                }
                else
                    return false;
            }
            _currentSub.Save(_subFilename, Encoding.Unicode);
            subEditor.Edited = false;
            return true;
        }


        private void OpenTxt(string filename)
        {
            var templatefile = string.Format(@"{0}\templates\{1}.template", _appFolderPath, _config.TemplateName);
            if (!System.IO.File.Exists(templatefile)) templatefile = _startUpPath + @"\config\default.template";
            _currentSub = SubStationAlpha.CreateFromTxt(filename, templatefile);
            _subFilename = null;
            subEditor.Edited = false;
            SetCurrentSub();
        }

        private void OpenAss(string filename)
        {
            _currentSub = SubStationAlpha.Load(filename);
            _subFilename = filename;
            SetCurrentSub();
        }

        private void SetCurrentSub()
        {
            subEditor.CurrentSub = _currentSub;
            waveViewer.CurrentSub = _currentSub;
        }

        private void updateConfig()
        {
            subEditor.Config = _config;
            translationEditor.SetConfig(_config);
        }

        #endregion


    }
}

enum WorkMode { TimingMode, TranslationMode }