using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Config.SGSConfig cfg = Config.SGSConfig.FromFile(@"E:\test\sgscfg.xml");
            subEditor1.Config = cfg;
            
        }
        private Subtitle.AssSub m_CurrentSub = new Subtitle.AssSub();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "ASS Subtitle (*.ass)|*.ass||";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                OpenAss(dlg.FileName);
                subEditor1.CurrentSub = m_CurrentSub;
                subEditor1.VideoLength = 600;

            }
        }

        private void OpenAss(string filename)
        {
            m_CurrentSub.LoadAss(filename);
        }

        private void subEditor1_Seek(object sender, SGSControls.SeekEventArgs e)
        {
            debugMessage.Text += "Seek Event, " + e.SeekDirection.ToString() + " " + e.SeekOffset.ToString() + Environment.NewLine;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            subEditor1.DisplayTime(trackBar1.Value);
        }

        private void subEditor1_TimeEdit(object sender, SGSControls.TimeEditEventArgs e)
        {
            double value = trackBar1.Value;
            e.CancelEvent = checkBox1.Checked;
            e.TimeValue = value;
        }
    }
}
