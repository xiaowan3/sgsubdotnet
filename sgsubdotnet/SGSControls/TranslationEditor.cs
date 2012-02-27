using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGS.Datatype;

namespace SGS.Controls
{
    public partial class TranslationEditor : UserControl
    {
        public TranslationEditor()
        {
            InitializeComponent();
        }
        #region members

        private string _filename = "";
        private Datatype.SGSConfig _config = null;
        private Datatype.SGSTrnAutosave _autosave;

        #endregion

        #region public methods
        public void SetConfig(Datatype.SGSConfig config)
        {
            syntaxHighlightingTextBox1.SetConfig(config);
            _config = config;
            labelRW.Text = string.Format("后退：Ctrl+{0},{1}", _config.PlayerRW, _config.PlayerRW2);
            labelFF.Text = string.Format("前进：Ctrl+{0},{1}", _config.PlayerFF, _config.PlayerFF2);
            labelToggle.Text = string.Format("暂停：Ctrl+{0},{1}", _config.PlayerTogglePause, _config.PlayerTogglePause2);
        }

        public void SetAutosavePath(string path)
        {
            _autosave = new SGSTrnAutosave();
            _autosave.DeleteOld(_config.AutoSaveLifeTime);
        }

        public void New()
        {
            if (AskSave())
            {
                syntaxHighlightingTextBox1.Text = "";
                syntaxHighlightingTextBox1.SetSaved();
                _filename = "";
            }
        }

        public void Open()
        {
            if (AskSave())
            {
                OpenTranslation();
            }
        }

        public void Cut()
        {
            syntaxHighlightingTextBox1.Cut();
        }
        public void Paste()
        {
            syntaxHighlightingTextBox1.Paste();
        }

        /// <summary>
        /// Save the file. If filename is unknown, show SaveFileDialog.
        /// </summary>
        /// <returns>Return true when saved, false when cancelled.</returns>
        public bool Save()
        {
            if (_filename.Length == 0)
            {
                var saveFileDialog = new SaveFileDialog
                {
                    AddExtension = true,
                    DefaultExt = "trn",
                    Filter = @"翻译文本 (*.trn)|*.trn||"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _filename = saveFileDialog.FileName;
                }
                else
                {
                    return false;
                }
            }
            syntaxHighlightingTextBox1.Save(_filename);
            return true;
        }

        public bool SaveAs()
        {
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "trn",
                Filter = @"翻译文本 (*.trn)|*.trn||"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                syntaxHighlightingTextBox1.Save(saveFileDialog.FileName);
                _filename = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void ExportPlainScript()
        {
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "txt",
                Filter = @"文本文件 (*.txt)|*.txt||"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                syntaxHighlightingTextBox1.Export(saveFileDialog.FileName);
            }
        }

        public void Undo()
        {
            syntaxHighlightingTextBox1.Undo();
        }

        public void Copy()
        {
            syntaxHighlightingTextBox1.Copy();
        }

        public void ShowAutosaveDlg()
        {
            var saveDlg = new TrnAutosaveForm(_autosave);
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                syntaxHighlightingTextBox1.Text = saveDlg.TranslationText;
                _filename = null;
            }
        }

        public bool Edited { get { return !syntaxHighlightingTextBox1.Saved; } }
        #endregion

        #region Events

        private event EventHandler<SeekEventArgs> _seekPlayer = null;
        private event EventHandler<PlayerControlEventArgs> _playerControl = null;

        public event EventHandler<SeekEventArgs> SeekPlayer
        {
            add
            {
                _seekPlayer += value;
                syntaxHighlightingTextBox1.SeekPlayer += value;
            }
            remove
            {
                _seekPlayer -= value;
                syntaxHighlightingTextBox1.SeekPlayer -= value;
            }
        }
        public event EventHandler<TimeEditEventArgs> TimeEdit
        {
            add { syntaxHighlightingTextBox1.TimeEdit += value; }
            remove { syntaxHighlightingTextBox1.TimeEdit -= value; }
        }
        public event EventHandler<PlayerControlEventArgs> PlayerControl
        {
            add
            {
                syntaxHighlightingTextBox1.PlayerControl += value;
                _playerControl += value;
            }

            remove
            {
                syntaxHighlightingTextBox1.PlayerControl -= value;
                _playerControl -= value;
            }
        }

        #endregion

        #region event handlers



        private void syntaxHighlightingTextBox1_RefreshSummary(object sender, SummaryEventArgs e)
        {
            labelLines.Text = String.Format("共有{0}行", e.Lines);
            labelWindows.Text = String.Format("其中有{0}行有窗", e.Windows);
            labelUncertain.Text = String.Format("有{0}行翻译内容存疑", e.Uncertern);
            labelToolong.Text = String.Format("有{0}行字数超标", e.Toolong);

        }


        private void btnSeek_Click(object sender, EventArgs e)
        {
            syntaxHighlightingTextBox1.SeekToCurrentLine();
            syntaxHighlightingTextBox1.Focus();
        }

        private void btnInsertTimeTag_Click(object sender, EventArgs e)
        {
            syntaxHighlightingTextBox1.InsertTimeTag();
            syntaxHighlightingTextBox1.Focus();
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            if (_playerControl != null)
            {
                _playerControl(this, new PlayerControlEventArgs(PlayerCommand.Toggle));
            }
            syntaxHighlightingTextBox1.Focus();
        }

        private void btnRW_Click(object sender, EventArgs e)
        {
            if (_seekPlayer != null)
                _seekPlayer(this, new SeekEventArgs(SeekDir.CurrentPos, -_config.SeekStep));
            syntaxHighlightingTextBox1.Focus();
        }

        private void btnFF_Click(object sender, EventArgs e)
        {
            if (_seekPlayer != null)
                _seekPlayer(this, new SeekEventArgs(SeekDir.CurrentPos, _config.SeekStep));
            syntaxHighlightingTextBox1.Focus();
        }


        private void syntaxHighlightingTextBox1_CheckAutosave(object sender, EventArgs e)
        {
            if (_autosave == null) return;
            var offset = DateTime.Now.Subtract(_autosave.PreviousSaveTime);
            if (offset.TotalSeconds > _config.AutoSavePeriod)
            {
                _autosave.SaveHistory(syntaxHighlightingTextBox1.Text, _filename);
            }
        }

        #endregion

        #region file operations
        /// <summary>
        /// Ask user whether to save the file.
        /// </summary>
        /// <returns>Return true when file is saved if discarded. Return false if user chose cancel.</returns>
        private bool AskSave()
        {
            if(!syntaxHighlightingTextBox1.Saved)
            {
                switch (MessageBox.Show("内容已更改，是否保存？", "是否保存", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        if (Save()) return true;
                        return false;
                    case DialogResult.No:
                        return true;
                    default:
                        return false;
                }
            }
            return true;
        }



        private bool OpenTranslation()
        {
            var openFileDialog = new OpenFileDialog
                                     {
                                         AddExtension = true,
                                         CheckFileExists = true,
                                         DefaultExt = "trn",
                                         Filter = @"翻译文本 (*.trn)|*.trn|纯文本 (*.txt)|*.txt|所有文件 (*.*)|*.*||"
                                     };
            if(openFileDialog.ShowDialog()== DialogResult.OK)
            {
                syntaxHighlightingTextBox1.Open(openFileDialog.FileName);
                _filename = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        #endregion


    }
}
