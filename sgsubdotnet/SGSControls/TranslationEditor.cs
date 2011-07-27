﻿using System;
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
        }
        #endregion

        #region Events

        public event EventHandler<SeekEventArgs> SeekPlayer = null;
        public event EventHandler<TimeEditEventArgs> TimeEdit = null;

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
                switch (MessageBox.Show("", "", MessageBoxButtons.YesNoCancel))
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

        #endregion

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
            if (_config == null) return;

            int pos = syntaxHighlightingTextBox1.SelectionStart;
            int lineend = syntaxHighlightingTextBox1.Text.IndexOf('\n', pos);
            lineend = lineend == -1 ? syntaxHighlightingTextBox1.Text.Length : lineend;
            int linestart = syntaxHighlightingTextBox1.Text.LastIndexOf('\n', pos);
            linestart += 1;
            string line = syntaxHighlightingTextBox1.Text.Substring(linestart, lineend - linestart);

            int commentmark = line.LastIndexOf(_config.CommentMark);
            if (commentmark == -1) return;
            int tagstart = line.LastIndexOf('[');
            if (tagstart == -1) return;
            int tagend = line.LastIndexOf(']');
            if (tagend == -1) return;
            if (tagend < tagstart) return;
            var tag = TimeTag.TryParse(line.Substring(tagstart + 1, tagend - tagstart - 1));
            if (tag != null && SeekPlayer != null)
            {
                SeekPlayer(this,new SeekEventArgs(SeekDir.Begin,tag.StartTime));
            }
        }

        private void btnInsertTimeTag_Click(object sender, EventArgs e)
        {
            if (_config == null) return;
            int linebegin, lineend;
            string line = FindCurrentLine(out linebegin, out lineend);
            int pos = syntaxHighlightingTextBox1.SelectionStart;
            var timeEditEventArgs = new TimeEditEventArgs(TimeType.BeginTime,0,true);
            if (TimeEdit != null) TimeEdit(this, timeEditEventArgs);
            if (timeEditEventArgs.CancelEvent) return;
            string tag = TimeTag.CreateTag(timeEditEventArgs.TimeValue,1);
            if(FindTimeTag(line)!= null)
            {
                string text = syntaxHighlightingTextBox1.Text;
                int l = text.LastIndexOf('[', lineend);
                int len = text.IndexOf(']', l) - l + 1;
                syntaxHighlightingTextBox1.Text = text.Remove(l, len).Insert(l, tag);
            }
            else
            {
                if (line.LastIndexOf(_config.CommentMark) == -1)
                {
                    tag = "%" + tag;
                }
                syntaxHighlightingTextBox1.Text = syntaxHighlightingTextBox1.Text.Insert(lineend, tag);
            }
            syntaxHighlightingTextBox1.SelectionStart = pos;
        }

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

        private string FindCurrentLine(out int first, out int last)
        {
            int pos = syntaxHighlightingTextBox1.SelectionStart;
            int totalLen = syntaxHighlightingTextBox1.Text.Length;
            if (totalLen == 0)
            {
                first = 0;
                last = 0;
                return "";
            }
            if (pos >= totalLen) pos = totalLen - 1;

            int lineend = syntaxHighlightingTextBox1.Text.IndexOf('\n', pos);
            last = lineend == -1 ? totalLen -1 : lineend - 1;
            int linestart = syntaxHighlightingTextBox1.Text.LastIndexOf('\n', last);
            first = linestart + 1;
            if (last < 0) last = 0;
            if (first < 0) first = 0;
            return syntaxHighlightingTextBox1.Text.Substring(first, last - first + 1);
        }

    }
}
