using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Config;

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
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            subtitleGrid.Columns.Add(column);
        }

        private Subtitle.AssSub m_CurrentSub = new Subtitle.AssSub();

        private bool m_TrackLoaded = false;
        private bool m_VideoOpened = false;
        private bool m_VideoPlaying = false;
        private bool m_SubLoaded = false;
        private bool m_Paused = false;

        private void button1_Click(object sender, EventArgs e)
        {


            //m_CurrentSub.LoadAss("E:\\test\\55.ass");
            //m_Config.DefaultAssHead = m_CurrentSub.m_AssHead;
            //m_Config.DefaultFormatLine = m_CurrentSub.m_AssParser.FmtLine;

            //m_Config.Save(@"E:\test\sgscfg.xml");

            m_SubLoaded = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
           // sub.WriteAss("E:\\test\\test2.ass");
            m_CurrentSub.CreateIndex(axWMP.currentMedia.duration);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (m_VideoOpened)
            {
                if (axWMP.Ctlcontrols.currentPosition > 0.5)
                {
                    m_VideoPlaying = true;
                }
                if (m_SubLoaded && m_VideoPlaying && (!m_TrackLoaded))
                {
                    m_CurrentSub.CreateIndex(axWMP.currentMedia.duration);
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
                m_Paused = false;
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


        private double oldS = 0, oldE = 0;
        private void subtitleGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (m_VideoPlaying)
            {
                m_Paused = true;
                axWMP.Ctlcontrols.pause();
                if (e.ColumnIndex != 2)
                {
                    Subtitle.AssItem item = (Subtitle.AssItem)subtitleGrid.Rows[e.RowIndex].DataBoundItem;
                    oldS = item.Start.TimeValue;
                    oldE = item.End.TimeValue;
                }
            }
        }

        private void subtitleGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 2)
            {
                Subtitle.AssItem item = (Subtitle.AssItem)subtitleGrid.Rows[e.RowIndex].DataBoundItem;
                m_CurrentSub.ItemEdited(item, oldS, oldE);
            }
        }

        private bool m_keydown = false;

        private void subtitleGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (!m_keydown)
            {
                m_keydown = true;
                if (e.KeyCode == m_Config.AddTimePoint)
                {
                    if (subtitleGrid.CurrentRow != null)
                    {
                        int rowindex = subtitleGrid.CurrentRow.Index;
                        if (m_VideoPlaying && m_TrackLoaded)
                        {
                            Subtitle.AssItem item = (Subtitle.AssItem)(subtitleGrid.CurrentRow.DataBoundItem);
                            double os = item.Start.TimeValue;
                            item.Start.TimeValue = axWMP.Ctlcontrols.currentPosition + m_Config.StartOffset;
                            m_CurrentSub.ItemEdited(item, os, item.End.TimeValue);
                            subtitleGrid.CurrentCell = subtitleGrid.CurrentRow.Cells[1];
                        }
                    }
                }
                else if (e.KeyCode == m_Config.Pause)
                {
                    if (m_VideoPlaying)
                    {
                        if (m_Paused)
                        {
                            axWMP.Ctlcontrols.play();
                            m_Paused = false;
                        }
                        else
                        {
                            axWMP.Ctlcontrols.pause();
                            m_Paused = true;
                        }

                    }
                }
                else if (e.KeyCode == m_Config.SeekBackword)
                {
                    if (m_VideoPlaying)
                    {
                        axWMP.Ctlcontrols.currentPosition -= m_Config.SeekStep;
                    }
                }
                else if (e.KeyCode == m_Config.SeekForward)
                {
                    if (m_VideoPlaying)
                    {
                        axWMP.Ctlcontrols.currentPosition += m_Config.SeekStep;
                    }
                }


            }

        }

        private void subtitleGrid_KeyUp(object sender, KeyEventArgs e)
        {
            m_keydown = false;
            if (subtitleGrid.CurrentRow != null && e.KeyCode==m_Config.AddTimePoint)
            {
                int rowindex = subtitleGrid.CurrentRow.Index;
                if (m_VideoPlaying && m_TrackLoaded)
                {
                    Subtitle.AssItem item = (Subtitle.AssItem)(subtitleGrid.Rows[rowindex].DataBoundItem);
                    double oe = item.End.TimeValue;
                    item.End.TimeValue = axWMP.Ctlcontrols.currentPosition + m_Config.EndOffset;
                    m_CurrentSub.ItemEdited(item, item.Start.TimeValue, oe);
                    if (rowindex < subtitleGrid.Rows.Count - 1)
                        subtitleGrid.CurrentCell = subtitleGrid.Rows[rowindex + 1].Cells[0];
                    if (rowindex - subtitleGrid.FirstDisplayedScrollingRowIndex > m_Config.SelectRowOffset)
                        subtitleGrid.FirstDisplayedScrollingRowIndex = rowindex - m_Config.SelectRowOffset;
                }
            }
        }

        private SGSConfig m_Config = new SGSConfig();

        private void toolStripPause_Click(object sender, EventArgs e)
        {
            if (m_VideoPlaying)
            {

                    axWMP.Ctlcontrols.pause();
                    m_Paused = true;
            }
        }

        private void toolStripPlay_Click(object sender, EventArgs e)
        {
            if (m_VideoPlaying)
            {
                axWMP.Ctlcontrols.play();
                m_Paused = false;
            }
        }

        private void toolStripJumpto_Click(object sender, EventArgs e)
        {
            if (m_VideoPlaying && subtitleGrid.CurrentRow != null)
            {
                double position = ((Subtitle.AssItem)(subtitleGrid.CurrentRow.DataBoundItem)).Start.TimeValue;
                if (position < axWMP.currentMedia.duration)
                {
                    axWMP.Ctlcontrols.currentPosition = position;
                }
                
            }
        }
    }
}
