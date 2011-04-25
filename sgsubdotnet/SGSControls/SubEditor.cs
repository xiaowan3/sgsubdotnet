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
    public partial class SubEditor : UserControl
    {
        public SubEditor()
        {
            InitializeComponent();

            DataGridViewColumn column;

            dataGridSubtitles.AutoGenerateColumns = false;
            dataGridSubtitles.AutoSize = false;

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Begin Time";
            column.DataPropertyName = "StartTime";
            dataGridSubtitles.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "End Time";
            column.DataPropertyName = "EndTime";
            dataGridSubtitles.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Text";
            column.DataPropertyName = "Text";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridSubtitles.Columns.Add(column);
            dataGridSubtitles.AllowUserToAddRows = false;
        }
        #region Private Members

        /// <summary>
        /// 字幕内容
        /// </summary>
        private Subtitle.AssSub m_CurrentSub = new Subtitle.AssSub();
        private Config.SGSConfig m_Config = null;
        private bool m_SubLoaded = false;
        private double m_VideoLength = 0;
        private UndoRecord m_UndoRec = new UndoRecord();
        #endregion

        public Subtitle.AssSub CurrentSub
        {
            get { return m_CurrentSub; }
            set
            {
                m_CurrentSub = value;
                if (value != null && value.SubItems.Count > 0)
                {
                    dataGridSubtitles.Enabled = true;
                    dataGridSubtitles.DataSource = value.SubItems;
                    dataGridSubtitles.AllowUserToAddRows = true;
                    m_SubLoaded = true;
                    m_CurrentSub.CreateIndex(m_VideoLength);
                    Edited = false;

                }
                else
                {
                    dataGridSubtitles.AllowUserToAddRows = false;
                    m_SubLoaded = false;
                    Edited = false;
                }
            }
        }

        public Config.SGSConfig Config
        {
            get { return m_Config; }
            set
            {
                m_Config = value;
            }
        }

        public double VideoLength
        {
            get { return m_VideoLength; }
            set
            {
                m_VideoLength = value;
                if (m_SubLoaded) m_CurrentSub.CreateIndex(m_VideoLength);
            }

        }

        public bool Edited { get; set; }
        #region Events
        public event EventHandler<SeekEventArgs> Seek = null;
        public event EventHandler<TimeEditEventArgs> TimeEdit = null;
        #endregion

        #region Methods
        public void EditBeginTime(int LineNumber, double Value)
        {
        }
        public void EditEndTime(int LineNumber, double Value)
        {
        }
        public void DisplayTime(double Time)
        {
            if (m_SubLoaded)
                labelSub.Text = m_CurrentSub.GetSubtitle(Time);
        }
        #endregion

        private void DeleteRow(DataGridViewRow row)
        {
            m_UndoRec.DeleteRow(row.Index, row);//为Undo记录删除操作
            Subtitle.AssItem i = ((Subtitle.AssItem)(row.DataBoundItem));
            m_CurrentSub.SubItems.Remove(i);
            //m_selectCells.Reset(); //清空选中的单元格
            dataGridSubtitles.Refresh();
            m_CurrentSub.RefreshIndex();
            Edited = true;
        }

        private void InsertNewRow(int index, DataGridViewRow currentRow)
        {
            Subtitle.AssItem i = ((Subtitle.AssItem)(currentRow.DataBoundItem)).Clone();
            i.Text = "";
            i.Start.TimeValue = 0;
            i.End.TimeValue = 0;
            m_CurrentSub.SubItems.Insert(index, i);
            m_CurrentSub.RefreshIndex();
            m_UndoRec.InsertRow(index);//为Undo记录插入操作
            //m_selectCells.Reset(); //清空选中的单元格
            dataGridSubtitles.Refresh();
            Edited = true;
        }

        private void tsbtnJumpto_Click(object sender, EventArgs e)
        {
            Subtitle.AssItem item;
            if (dataGridSubtitles.CurrentRow != null && m_SubLoaded
                && (item = (Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                )
            {
                double time = ((Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)).Start.TimeValue;
                SeekEventArgs seekevent = new SeekEventArgs(SeekDir.Begin, time);
                Seek(this, seekevent);
            }
        }

        private void tsbtnDuplicate_Click(object sender, EventArgs e)
        {
            Subtitle.AssItem item;
            if (dataGridSubtitles.CurrentRow != null && m_SubLoaded
                && (item = (Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                )
            {
                Subtitle.AssItem i = item.Clone();
                m_CurrentSub.SubItems.Insert(dataGridSubtitles.CurrentRow.Index + 1, i);
                m_UndoRec.InsertRow(dataGridSubtitles.CurrentRow.Index + 1);//为Undo记录插入操作
                //m_selectCells.Reset(); //清空选中的单元格
                dataGridSubtitles.Refresh();
                m_CurrentSub.RefreshIndex();
                Edited = true;
            }
        }

        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            Subtitle.AssItem item;
            if (dataGridSubtitles.CurrentRow != null && m_SubLoaded
                && (item = (Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                )
            {
                DeleteRow(dataGridSubtitles.CurrentRow);
            }
        }

        private void tsbtnInsBefore_Click(object sender, EventArgs e)
        {
            Subtitle.AssItem item;
            if (dataGridSubtitles.CurrentRow != null && m_SubLoaded
                && (item = (Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                )
            {
                InsertNewRow(dataGridSubtitles.CurrentRow.Index, dataGridSubtitles.CurrentRow);
            }
        }

        private void tsbtnInsAfter_Click(object sender, EventArgs e)
        {
            Subtitle.AssItem item;
            if (dataGridSubtitles.CurrentRow != null && m_SubLoaded
                && (item = (Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                )
            {
                InsertNewRow(dataGridSubtitles.CurrentRow.Index + 1, dataGridSubtitles.CurrentRow);
            }
        }

        private double oldS = 0, oldE = 0;
        private string oldString;
        private bool cancelEdit;
        private void dataGridSubtitles_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldString = dataGridSubtitles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
       //     if (m_VideoPlaying)
         //   {
         //       m_Paused = true;
         //       dxVideoPlayer.Pause();
         //   }
            if (e.ColumnIndex != 2)
            {
               Subtitle.AssItem item = (Subtitle.AssItem)dataGridSubtitles.Rows[e.RowIndex].DataBoundItem;
               if (item == null)
                {
                    cancelEdit = true;
                    oldString = "";
                }
                else
                {
                    oldS = item.Start.TimeValue;
                    oldE = item.End.TimeValue;
                }
            }
        }

        private void dataGridSubtitles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!cancelEdit)
            {
                string newString = dataGridSubtitles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (!oldString.Equals(newString))
                {
                    m_UndoRec.Edit(e.RowIndex, e.ColumnIndex, oldString);//比较单元格内容，如改变，记录undo
                }

                if (e.ColumnIndex != 2)
                {
                    Subtitle.AssItem item = (Subtitle.AssItem)dataGridSubtitles.Rows[e.RowIndex].DataBoundItem;
                    m_CurrentSub.ItemEdited(item, oldS, oldE);
                }
                Edited = true;
            }
            
        }

        private void dataGridSubtitles_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            m_UndoRec.InsertRow(e.Row.Index);
            cancelEdit = false;
            oldS = 0;
            oldE = 0;
            oldString = "";
            m_CurrentSub.RefreshIndex();
        }

        private void tsbtnUndo_Click(object sender, EventArgs e)
        {
            m_UndoRec.Undo(m_CurrentSub);
            dataGridSubtitles.Refresh();
            m_CurrentSub.RefreshIndex();
            Edited = true;
        }

    }

    public class SeekEventArgs : EventArgs
    {
        public double SeekOffset;
        public SeekDir SeekDirection;
        public SeekEventArgs(SeekDir seekdir, double seekoff)
        {
            SeekOffset = seekoff;
            SeekDirection = seekdir;
        }
    }
    public enum SeekDir { Begin, CurrentPos };

    public class TimeEditEventArgs : EventArgs
    {
        public TimeType EditTime;
        public double TimeValue;
        public TimeEditEventArgs(TimeType editTime, double timevalue)
        {
            EditTime = editTime;
            TimeValue = timevalue;
        }
    }
    public enum TimeType { BeginTime, EndTime };
}
