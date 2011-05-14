using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSDatatype;
namespace test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            _config = SGSConfig.FromFile(@"E:\test\newsgscfg.xml");
        }

        private SGSConfig _config;
        private AssSub _sub;
        

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var sub = new AssSub();
                sub.LoadAss(openFileDialog.FileName);
                _sub = sub;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var sub = new AssSub();
                sub.LoadText(openFileDialog.FileName, _config);
                _sub = sub;
            }
        }
        SGSAutoSave _autosave = new SGSAutoSave();
        private void button3_Click(object sender, EventArgs e)
        {
            _autosave.SaveHistory(_sub);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            _autosave.Save(@"E:\test\autosave.xml");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _autosave = SGSAutoSave.Load(@"E:\test\autosave.gz");
        }
    }
}
