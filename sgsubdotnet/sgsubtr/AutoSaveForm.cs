using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGS.Datatype;

namespace sgsubtr
{
    public partial class AutoSaveForm : Form
    {
        private readonly SGSAutoSave _autosave;
        public SubStationAlpha Sub;
        public AutoSaveForm(SGSAutoSave sgsAutoSave)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            _autosave = sgsAutoSave;
            _autosave.Load();
            dataGridView1.DataSource = _autosave.AutoSaveFileBindingSource;
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow != null)
            {
                var savefilename = ((SaveFileIndex) dataGridView1.CurrentRow.DataBoundItem).SaveFile;
                var sub = AutoSaveRecord.Fromfile(savefilename);
                Sub = sub.Subtitle;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Sub = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var filename = ((SaveFileIndex)dataGridView1.CurrentRow.DataBoundItem).SaveFile;
                System.IO.File.Delete(filename);
                _autosave.Load();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var dlgresult = MessageBox.Show(@"是否清空自动作存记录", @"清空", MessageBoxButtons.YesNo);
            if(dlgresult == DialogResult.Yes)
            {
                foreach (SaveFileIndex item in _autosave.AutoSaveFileBindingSource)
                {
                    System.IO.File.Delete(item.SaveFile);
                }
                _autosave.Load();
            }
        }
    }
}
