using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sgsubdotnet
{
    public partial class SGSMainForm : Form
    {
        public SGSMainForm()
        {
            InitializeComponent();
            DataGridViewColumn column;

            subtitleGrid.AutoGenerateColumns = false;
            subtitleGrid.AutoSize = false;

            subtitleGrid.DataSource = m_CurrentSub.SubItems;

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Begin Time";
            column.DataPropertyName = "StartTime";
            subtitleGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "End Time";
            column.DataPropertyName = "EndTime";
            subtitleGrid.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Text";
            column.DataPropertyName = "Text";
            subtitleGrid.Columns.Add(column);
        }

        private Subtitle.AssSub m_CurrentSub = new Subtitle.AssSub();

        private bool m_TrackLoaded = false;
        private bool m_VideoOpened = false;
        private bool m_VideoPlaying = false;
        private bool m_SubLoaded = false;

        private void button1_Click(object sender, EventArgs e)
        {
            string formatline = "Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text";
            string linetoparse = "Dialogue: 0,0:01:11.05,0:01:15.23,*Default,NTP,0000,0000,0000,,好吃就行了管他的呢 再来一碗";
            Subtitle.AssLineParser parser = new Subtitle.AssLineParser(formatline);
      
            parser.ParseLine(linetoparse);

            m_CurrentSub.LoadAss("E:\\test\\ass.ass");


           // OpenFileDialog dlg = new OpenFileDialog();
           // dlg.ShowDialog();
           // axWMP.URL = dlg.FileName;
           // axWMP.Ctlcontrols.play();
            m_SubLoaded = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
           // sub.WriteAss("E:\\test\\test2.ass");
            m_CurrentSub.CreateTrack(axWMP.currentMedia.duration);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (m_VideoOpened)
            {
                if (axWMP.Ctlcontrols.currentPosition > 0.5)
                    m_VideoPlaying = true;
                if (m_SubLoaded && m_VideoPlaying && (!m_TrackLoaded))
                {
                    m_CurrentSub.CreateTrack(axWMP.currentMedia.duration);
                    m_TrackLoaded = true;
                }
                if (m_TrackLoaded)
                    subLabel.Text = m_CurrentSub.GetSubtitle(axWMP.Ctlcontrols.currentPosition);
            }
        }

        private void OpenVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                axWMP.URL = dlg.FileName;
                axWMP.Ctlcontrols.play();
                m_VideoOpened = true;
                m_VideoPlaying = false;
                timer.Start();
            }
        }

        private void OpenSubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_CurrentSub.LoadAss(dlg.FileName);
                m_SubLoaded = true;
            }
        }
    }
}
