using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSControls
{
    public partial class TranslationEditor : UserControl
    {
        public TranslationEditor()
        {
            InitializeComponent();
        }
        #region members

        private string _filename = "";
        private SGSDatatype.SGSConfig _config = null;

        #endregion

        #region public methods
        public void SetConfig(SGSDatatype.SGSConfig config)
        {
            syntaxHighlightingTextBox1.SetConfig(config);
            _config = config;
            labelRW.Text = "后退：Ctrl+" + _config.PlayerRW;
            labelFF.Text = "前进：Ctrl+" + _config.PlayerFF;
            labelToggle.Text = "暂停：Ctrl+" + _config.PlayerTogglePause;
        }
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
        public event EventHandler<TimeEditEventArgs> TimeEdit = null;
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
        private void menuItemNew_Click(object sender, EventArgs e)
        {
            if (AskSave())
            {
                syntaxHighlightingTextBox1.Text = "";
                syntaxHighlightingTextBox1.SetSaved();
                _filename = "";
            }
        }


        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if(AskSave())
            {
                Open();
            }
        }


        private void menuItemSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void menuItemSaveas_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void menuItemExport_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void syntaxHighlightingTextBox1_RefreshSummary(object sender, SummaryEventArgs e)
        {
            labelLines.Text = String.Format("共有{0}行", e.Lines);
            labelWindows.Text = String.Format("其中有{0}行有窗", e.Windows);
            labelUncertain.Text = String.Format("有{0}行翻译内容存疑", e.Uncertern);
            labelToolong.Text = String.Format("有{0}行字数超标", e.Toolong);

        }

        /// <summary>
        /// 这算法有这么麻烦吗，囧
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeek_Click(object sender, EventArgs e)
        {
            int linestart, linelen;

            string line = FindCurrentLine(out linestart, out linelen);
            var tag = FindTimeTag(line);
            if (tag != null && _seekPlayer != null)
            {
                _seekPlayer(this, new SeekEventArgs(SeekDir.Begin, tag.StartTime));
            }
            syntaxHighlightingTextBox1.Focus();
        }

        private void btnInsertTimeTag_Click(object sender, EventArgs e)
        {
            if (_config == null) return;
            int linebegin, linelen;
            string line = FindCurrentLine(out linebegin, out linelen);
            int pos = syntaxHighlightingTextBox1.SelectionStart;
            syntaxHighlightingTextBox1.LockScrollPos();
            var timeEditEventArgs = new TimeEditEventArgs(TimeType.BeginTime, 0, true);
            if (TimeEdit != null) TimeEdit(this, timeEditEventArgs);
            if (timeEditEventArgs.CancelEvent) return;
            string tag = TimeTag.CreateTag(timeEditEventArgs.TimeValue, 1);
            if (FindTimeTag(line) != null)
            {
                string text = syntaxHighlightingTextBox1.Text;
                int l = text.LastIndexOf('[', linebegin + linelen - 1);
                int len = text.IndexOf(']', l) - l + 1;
                syntaxHighlightingTextBox1.Text = text.Remove(l, len).Insert(l, tag);
            }
            else
            {
                if (line.LastIndexOf(_config.CommentMark) == -1)
                {
                    tag = "%" + tag;
                }
                syntaxHighlightingTextBox1.Text = syntaxHighlightingTextBox1.Text.Insert(linebegin + linelen, tag);
            }
            syntaxHighlightingTextBox1.SelectionStart = pos;
            syntaxHighlightingTextBox1.UnlockScrollPos();
            syntaxHighlightingTextBox1.Focus();

        }

        private void MenuItemCopy_Click(object sender, EventArgs e)
        {
            syntaxHighlightingTextBox1.Copy();
        }

        private void MenuItemCut_Click(object sender, EventArgs e)
        {
            syntaxHighlightingTextBox1.Cut();
        }

        private void MenuItemPaste_Click(object sender, EventArgs e)
        {
            syntaxHighlightingTextBox1.Paste();
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

        /// <summary>
        /// Save the file. If filename is unknown, show SaveFileDialog.
        /// </summary>
        /// <returns>Return true when saved, false when cancelled.</returns>
        private bool Save()
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

        private bool SaveAs()
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


        private bool Open()
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

        private void Export()
        {
            var saveFileDialog = new SaveFileDialog
                                     {
                                         AddExtension = true,
                                         DefaultExt = "txt",
                                         Filter = @"文本文件 (*.txt)|*.txt||"
                                     };
            if(saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                syntaxHighlightingTextBox1.Export(saveFileDialog.FileName);
            }
        }

        #endregion

 
        private TimeTag FindTimeTag(string line)
        {
            int commentmark = line.LastIndexOf(_config.CommentMark);
            if (commentmark == -1) return null;
            int tagstart = line.LastIndexOf('[');
            if (tagstart == -1) return null;
            int tagend = line.LastIndexOf(']');
            if (tagend == -1) return null;
            if (tagend < tagstart) return null;
            var tag = TimeTag.TryParse(line.Substring(tagstart + 1, tagend - tagstart - 1));
            return tag;
        }

        private string FindCurrentLine(out int first, out int len)
        {
            int pos = syntaxHighlightingTextBox1.SelectionStart;
            int totalLen = syntaxHighlightingTextBox1.Text.Length;
            if (totalLen == 0)
            {
                first = 0;
                len = 0;
                return "";
            }
            if (pos >= totalLen) pos = totalLen - 1;
            if (pos == 0) pos = 1; 
            int linebegin = syntaxHighlightingTextBox1.Text.LastIndexOf('\n', pos - 1);
            first = linebegin + 1; //行的第一个字符位置
            int last = syntaxHighlightingTextBox1.Text.IndexOf('\n', first);
            last = last == -1 ? totalLen - 1 : last - 1;
            len = last - first + 1;
            return syntaxHighlightingTextBox1.Text.Substring(first, len);
        }

 
    }
}
