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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SubStationAlpha _sub = null;
        private SSAIndex _subindex = null;
        private void button1_Click(object sender, EventArgs e)
        {
            _sub = SubStationAlpha.Load(@"E:\test\Haruhi_14.ass");
            _subindex = new SSAIndex {Subtitle = _sub};
            _subindex.CreateIndex(600);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (_subindex == null) return;
            label1.Text = _subindex.GetSubtitle(trackBar1.Value);

        }
    }
}
