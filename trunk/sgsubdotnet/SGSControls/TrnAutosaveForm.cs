using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSDatatype;

namespace SGSControls
{
    public partial class TrnAutosaveForm : Form
    {
        private readonly SGSTrnAutosave _autosave;
        public string TranslationText;

        public TrnAutosaveForm(SGSTrnAutosave trnAutosave)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            _autosave = trnAutosave;
            _autosave.Load();
            dataGridView1.DataSource = _autosave.AutoSaveFileBindingSource;
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var savefilename = ((TrnSaveFileIndex)dataGridView1.CurrentRow.DataBoundItem).SaveFile;
                var item = TrnAutosaveRec.Fromfile(savefilename);
                TranslationText = item.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var filename = ((TrnSaveFileIndex)dataGridView1.CurrentRow.DataBoundItem).SaveFile;
                System.IO.File.Delete(filename);
                _autosave.Load();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var dlgresult = MessageBox.Show(@"是否清空自动作存记录", @"清空", MessageBoxButtons.YesNo);
            if (dlgresult == DialogResult.Yes)
            {
                foreach (TrnSaveFileIndex item in _autosave.AutoSaveFileBindingSource)
                {
                    System.IO.File.Delete(item.SaveFile);
                }
                _autosave.Load();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            TranslationText = "";
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
