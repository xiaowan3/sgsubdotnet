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

        #endregion

        public void SetConfig(SGSDatatype.SGSConfig config)
        {
            syntaxHighlightingTextBox1.SetConfig(config);
        }
        private void menuItemNew_Click(object sender, EventArgs e)
        {
            if (AskSave())
            {
                syntaxHighlightingTextBox1.Text = "";
                syntaxHighlightingTextBox1.SetSaved();
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
                        {

                        }
                        break;
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
            var saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "trn",
                Filter = @"翻译文本 (*.trn)|*.trn||"
            };
            if(saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                return true;
            }
            return false;
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
                return true;
            }
            return false;
        }


        private bool Open()
        {
            return false;
        }




    }
}
