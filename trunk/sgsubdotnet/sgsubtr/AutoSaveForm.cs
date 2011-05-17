using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSDatatype;

namespace sgsubtr
{
    public partial class AutoSaveForm : Form
    {
        private readonly SGSAutoSave _autosave;
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

        }
    }
}
