using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
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

            m_selectCells.Rows = dataGridSubtitles.Rows;
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
        SelectCells m_selectCells = new SelectCells();
        #endregion

        public Subtitle.AssSub CurrentSub
        {
            get { return m_CurrentSub; }
            set
            {
                m_CurrentSub = value;
                if (value != null && value.SubItems.Count > 0)
                {
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
                m_UndoRec.Reset();
                m_selectCells.Reset();
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

        public int CurrentRowIndex
        {
            get
            {
                if (m_SubLoaded && dataGridSubtitles.CurrentRow != null) return dataGridSubtitles.CurrentRow.Index;
                else return -1;
            }
            set
            {
                if (m_SubLoaded && value >= 0 && value < dataGridSubtitles.RowCount - 1)
                {
                    dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[value].Cells[0];
                }
            }
        }

        public bool Edited { get; set; }
        #region Events
        public event EventHandler<SeekEventArgs> Seek = null;
        public event EventHandler<TimeEditEventArgs> TimeEdit = null;
        public event EventHandler<PlayerControlEventArgs> PlayerControl = null;
        public event EventHandler<CurrentRowChangeEventArgs> CurrentRowChanged = null;
        public event EventHandler KeySaveAss = null;
        #endregion

        #region Methods
        public void EditBeginTime(int RowIndex, double Value)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (RowIndex >= 0 && RowIndex <= lastrowindex)
            {
                Subtitle.AssItem item = (Subtitle.AssItem)(dataGridSubtitles.Rows[RowIndex].DataBoundItem);
                m_UndoRec.Edit(RowIndex, 0, item.StartTime);//记录Undo
                double os = item.Start.TimeValue;
                item.Start.TimeValue = Value > 0 ? Value : 0;
                m_CurrentSub.ItemEdited(item, os, item.End.TimeValue);
                dataGridSubtitles.UpdateCellValue(0, RowIndex);
                Edited = true;
            }

        }
        public void EditEndTime(int RowIndex, double Value)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (RowIndex >= 0 && RowIndex <= lastrowindex)
            {
                Subtitle.AssItem item = (Subtitle.AssItem)(dataGridSubtitles.Rows[RowIndex].DataBoundItem);
                m_UndoRec.Edit(RowIndex, 1, item.EndTime);//记录Undo
                double oe = item.End.TimeValue;
                item.End.TimeValue = Value > 0 ? Value : 0; ;
                m_CurrentSub.ItemEdited(item, item.Start.TimeValue, oe);
                dataGridSubtitles.UpdateCellValue(1, RowIndex);
                Edited = true;
            }
        }

        public void EditCellTime(int RowIndex, int ColIndex, double Value)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (RowIndex >= 0 && RowIndex <= lastrowindex)
            {
                Subtitle.AssItem item = (Subtitle.AssItem)(dataGridSubtitles.Rows[RowIndex].DataBoundItem);
                if (ColIndex == 0)
                {
                    m_UndoRec.Edit(RowIndex, 0, item.StartTime);//记录Undo
                    double os = item.Start.TimeValue;
                    item.Start.TimeValue = Value > 0 ? Value : 0;
                    m_CurrentSub.ItemEdited(item, os, item.End.TimeValue);
                    dataGridSubtitles.UpdateCellValue(0, RowIndex);
                    Edited = true;
                }
                else if (ColIndex == 1)
                {
                    m_UndoRec.Edit(RowIndex, 1, item.EndTime);//记录Undo
                    double oe = item.End.TimeValue;
                    item.End.TimeValue = Value > 0 ? Value : 0; ;
                    m_CurrentSub.ItemEdited(item, item.Start.TimeValue, oe);
                    dataGridSubtitles.UpdateCellValue(1, RowIndex);
                    Edited = true;
                }
            }
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
            m_selectCells.Reset();//清空标记的单元格
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
            m_selectCells.Reset(); //清空标记的单元格
            dataGridSubtitles.Refresh();
            Edited = true;
        }

        #region Event handlers
        private void tsbtnJumpto_Click(object sender, EventArgs e)
        {
            Subtitle.AssItem item;
            if (dataGridSubtitles.CurrentRow != null && m_SubLoaded
                && (item = (Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                )
            {
                double time = ((Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)).Start.TimeValue;
                SeekEventArgs seekevent = new SeekEventArgs(SeekDir.Begin, time);
                if (Seek != null) Seek(this, seekevent);
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
                m_selectCells.Reset();//清空标记的单元格
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
            m_selectCells.Reset();
            oldString = dataGridSubtitles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
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
                    Edited = true;
                }
                if (e.ColumnIndex != 2)
                {
                    Subtitle.AssItem item = (Subtitle.AssItem)dataGridSubtitles.Rows[e.RowIndex].DataBoundItem;
                    m_CurrentSub.ItemEdited(item, oldS, oldE);
                }
            }

        }

        private void dataGridSubtitles_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            m_UndoRec.InsertRow(e.Row.Index - 1);
            cancelEdit = false;
            oldS = 0;
            oldE = 0;
            oldString = "";
            m_CurrentSub.RefreshIndex();
            Edited = true;
        }

        private void tsbtnUndo_Click(object sender, EventArgs e)
        {
            m_selectCells.Reset();
            m_UndoRec.Undo(m_CurrentSub);
            dataGridSubtitles.Refresh();
            m_CurrentSub.RefreshIndex();
            Edited = true;
        }


        private enum TimeCheckStatus { OK = 0, OVERLAP, ERROR };

        private void tsbtnTimeLineScan_Click(object sender, EventArgs e)
        {
            if (m_SubLoaded)
            {
                bool overlap = false;
                bool timeerror = false;
                int rowCount = dataGridSubtitles.Rows.Count - 1;
                TimeCheckStatus[] itemStatus = new TimeCheckStatus[rowCount];
                for (int i = 0; i < rowCount - 1; i++)
                {
                    Subtitle.AssItem itema = (Subtitle.AssItem)(dataGridSubtitles.Rows[i].DataBoundItem);
                    if (itema.Start.TimeValue > itema.End.TimeValue)
                    {
                        itemStatus[i] = TimeCheckStatus.ERROR;
                        continue;
                    }
                    for (int j = i + 1; j < rowCount; j++)
                    {
                        Subtitle.AssItem itemb = (Subtitle.AssItem)(dataGridSubtitles.Rows[j].DataBoundItem);
                        if (itemb.Start.TimeValue > itemb.End.TimeValue)
                        {
                            itemStatus[j] = TimeCheckStatus.ERROR;
                            continue;
                        }
                        if ((
                            itema.End.TimeValue >= itemb.Start.TimeValue && itema.Start.TimeValue <= itemb.Start.TimeValue ||
                            itemb.End.TimeValue >= itema.Start.TimeValue && itemb.Start.TimeValue <= itema.Start.TimeValue) &&
                            itema.End.TimeValue - itema.Start.TimeValue > 0 && itemb.End.TimeValue - itemb.Start.TimeValue > 0
                        )
                        {
                            itemStatus[i] = TimeCheckStatus.OVERLAP;
                            itemStatus[j] = TimeCheckStatus.OVERLAP;
                        }
                    }

                }
                for (int i = 0; i < rowCount; i++)
                {
                    switch (itemStatus[i])
                    {
                        case TimeCheckStatus.OVERLAP:
                            dataGridSubtitles.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            dataGridSubtitles.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                            overlap = true;
                            break;
                        case TimeCheckStatus.OK:
                            dataGridSubtitles.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                            dataGridSubtitles.Rows[i].Cells[1].Style.ForeColor = Color.Black;
                            break;
                        case TimeCheckStatus.ERROR:
                            dataGridSubtitles.Rows[i].Cells[0].Style.ForeColor = Color.Blue;
                            dataGridSubtitles.Rows[i].Cells[1].Style.ForeColor = Color.Blue;
                            timeerror = true;
                            break;
                    }
                }
                string msg =
                    timeerror && overlap ? "发现时间轴重叠和错误时间点" :
                    timeerror && !overlap ? "发现错误时间点" :
                    !timeerror && overlap ? "发现时间轴重叠" : "未发现时间轴重叠和错误时间点";

                MessageBox.Show(msg, "时间轴检查");

            }
        }

        private void tsbtnMarkCells_Click(object sender, EventArgs e)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (dataGridSubtitles.SelectedCells != null)
            {
                foreach (DataGridViewCell cell in dataGridSubtitles.SelectedCells)
                {
                    if (cell.RowIndex <= lastrowindex)
                        m_selectCells.SelectCell(cell.ColumnIndex, cell.RowIndex);
                }
            }
        }

        private void tsbtnUnmarkAll_Click(object sender, EventArgs e)
        {
            m_selectCells.DeselectAll();
        }

        private void tsbtnTimeOffset_Click(object sender, EventArgs e)
        {
            if (m_SubLoaded)
            {
                TimeOffsetDialog toDlg = new TimeOffsetDialog();
                if (toDlg.ShowDialog() == DialogResult.OK)
                {
                    m_selectCells.TimeOffset(toDlg.TimeOffset, m_UndoRec);
                    dataGridSubtitles.Refresh();
                }
            }
        }

        private void tsbtnPause_Click(object sender, EventArgs e)
        {
            PlayerControlEventArgs arg = new PlayerControlEventArgs(PlayerCommand.Pause);
            if (PlayerControl != null) PlayerControl(this, arg);
        }

        private void tsbtnPlay_Click(object sender, EventArgs e)
        {
            PlayerControlEventArgs arg = new PlayerControlEventArgs(PlayerCommand.Play);
            if (PlayerControl != null) PlayerControl(this, arg);
        }

        #endregion

        /// <summary>
        /// Record Key Status
        /// </summary>
        private bool[] m_keyhold = new bool[256];


        private void dataGridSubtitles_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.Cell != null)
            {
                if (CurrentRowChanged != null)
                {
                    int rowindex = e.Cell.RowIndex;
                    CurrentRowChangeEventArgs args = new CurrentRowChangeEventArgs(rowindex);
                    CurrentRowChanged(this, args);
                }
            }
        }

        private void dataGridSubtitles_KeyDown(object sender, KeyEventArgs e)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (m_Config == null) return;
            if ((int)e.KeyCode >= 0 && (int)e.KeyCode < 256)
            {
                int rowIndex = (dataGridSubtitles.CurrentRow == null) ? -1 : dataGridSubtitles.CurrentRow.Index;
                #region Timeing Keys
                if (e.KeyCode == m_Config.AddTimePoint && !m_keyhold[(int)e.KeyCode])
                {
                    //单键插入时间

                    if (rowIndex >= 0 && rowIndex <= lastrowindex)
                    {
                        TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.BeginTime, 0, true);
                        if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                        double time = timeEditArgs.TimeValue + m_Config.StartOffset;
                        if (!timeEditArgs.CancelEvent)
                        {
                            if (rowIndex > 0 && m_Config.AutoOverlapCorrection)
                            {
                                Subtitle.AssItem lastitem = ((Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                                if (lastitem.End.TimeValue - time > 0 &&
                                    lastitem.End.TimeValue - time < Math.Max(Math.Abs(m_Config.StartOffset), Math.Abs(m_Config.EndOffset)))
                                {
                                    EditEndTime(rowIndex - 1, time - 0.01);
                                }
                            }
                            EditBeginTime(rowIndex, timeEditArgs.TimeValue);
                            dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex].Cells[1];
                        }
                    }
                }
                else if (e.KeyCode == m_Config.AddStartTime && !m_keyhold[(int)e.KeyCode])
                {
                    //插入开始时间
                    if (rowIndex >= 0 && rowIndex <= lastrowindex)
                    {
                        TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.BeginTime, 0, true);
                        if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                        double time = timeEditArgs.TimeValue + m_Config.StartOffset;
                        if (!timeEditArgs.CancelEvent)
                        {
                            if (rowIndex > 0 && m_Config.AutoOverlapCorrection)
                            {
                                Subtitle.AssItem lastitem = ((Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                                if (lastitem.End.TimeValue - time > 0 &&
                                    lastitem.End.TimeValue - time < Math.Max(Math.Abs(m_Config.StartOffset), Math.Abs(m_Config.EndOffset)))
                                {
                                    EditEndTime(rowIndex - 1, time - 0.01);
                                }
                            }
                            EditBeginTime(rowIndex, time);
                            dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex].Cells[1];
                        }
                    }
                }
                else if (e.KeyCode == m_Config.AddEndTime && !m_keyhold[(int)e.KeyCode])
                {
                    //插入结束时间
                    if (rowIndex >= 0 && rowIndex <= lastrowindex)
                    {
                        TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.EndTime, 0, true);
                        if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                        double time = timeEditArgs.TimeValue + m_Config.EndOffset;
                        if (!timeEditArgs.CancelEvent)
                        {
                            EditEndTime(rowIndex, time);
                            if (rowIndex < lastrowindex)
                                dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex + 1].Cells[0];
                        }
                    }
                }
                else if (e.KeyCode == m_Config.AddContTimePoint && !m_keyhold[(int)e.KeyCode])
                {
                    //连续插入时间
                    if (rowIndex >= 0 && rowIndex <= lastrowindex)
                    {
                        TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.Unknown, 0, true);
                        if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                        double time = timeEditArgs.TimeValue + m_Config.StartOffset;
                        if (!timeEditArgs.CancelEvent)
                        {
                            EditEndTime(rowIndex, time - 0.01);
                            if (rowIndex < lastrowindex)
                            {
                                EditBeginTime(rowIndex + 1, time);
                                dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex + 1].Cells[1];
                            }
                        }
                    }
                }
                else if (e.KeyCode == m_Config.AddCellTime && !m_keyhold[(int)e.KeyCode])
                {
                    //插入单元格时间
                    if (rowIndex >= 0 && rowIndex <= lastrowindex)
                    {

                        TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.Unknown, 0, true);
                        if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                        if (!timeEditArgs.CancelEvent)
                        {
                            double time = timeEditArgs.TimeValue + m_Config.StartOffset;
                            int colIndex = dataGridSubtitles.CurrentCell.ColumnIndex;
                            if (colIndex == 0) //插入开始时间
                            {
                                if (rowIndex > 0 && m_Config.AutoOverlapCorrection)
                                {
                                    Subtitle.AssItem lastitem = ((Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                                    if (lastitem.End.TimeValue - time > 0 &&
                                        lastitem.End.TimeValue - time < Math.Max(Math.Abs(m_Config.StartOffset), Math.Abs(m_Config.EndOffset)))
                                    {
                                        EditEndTime(rowIndex - 1, time - 0.01);
                                    }
                                }
                                EditBeginTime(rowIndex, time);
                                dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex].Cells[1];
                            }
                            else if (colIndex == 1)//插入结束时间
                            {
                                EditEndTime(rowIndex, time);
                                if (rowIndex < lastrowindex)
                                    dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex + 1].Cells[0];
                            }
                        }
                    }
                }
                #endregion
                #region Seek Keys
                else if (e.KeyCode == m_Config.Pause && !m_keyhold[(int)e.KeyCode])
                {
                    PlayerControlEventArgs arg = new PlayerControlEventArgs(PlayerCommand.Toggle);
                    if (PlayerControl != null) PlayerControl(this, arg);
                }
                else if (e.KeyCode == m_Config.SeekBackword && !m_keyhold[(int)e.KeyCode])
                {
                    SeekEventArgs seekevent = new SeekEventArgs(SeekDir.CurrentPos, -m_Config.SeekStep);
                    if (Seek != null) Seek(this, seekevent);
                }
                else if (e.KeyCode == m_Config.SeekForward && !m_keyhold[(int)e.KeyCode])
                {
                    SeekEventArgs seekevent = new SeekEventArgs(SeekDir.CurrentPos, m_Config.SeekStep);
                    if (Seek != null) Seek(this, seekevent);
                }
                else if (e.KeyCode == m_Config.GotoCurrent && !m_keyhold[(int)e.KeyCode])
                {
                    Subtitle.AssItem item;
                    if (dataGridSubtitles.CurrentRow != null && m_SubLoaded
                        && (item = (Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                        )
                    {
                        double time = ((Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)).Start.TimeValue;
                        SeekEventArgs seekevent = new SeekEventArgs(SeekDir.Begin, time);
                        if (Seek != null) Seek(this, seekevent);
                    }
                }
                else if (e.KeyCode == m_Config.GotoPrevious && !m_keyhold[(int)e.KeyCode])
                {
                    if (rowIndex > 0 && rowIndex <= lastrowindex)
                    {
                        Subtitle.AssItem item = ((Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                        SeekEventArgs seekevent = new SeekEventArgs(SeekDir.Begin, item.Start.TimeValue);
                        if (Seek != null) Seek(this, seekevent);
                    }
                }

                #endregion
                #region Edit Keys
                //复制，支持多个单元格的复制和粘贴
                else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control && !m_keyhold[(int)e.KeyCode])
                {
                    if (dataGridSubtitles.CurrentCell != null)
                    {
                        //行，列的取值范围
                        int cmin, cmax, rmin, rmax;
                        //行，列的个数
                        int nr, nc;
                        //内容
                        string[,] content;
                        string cb = "";
                        cmin = dataGridSubtitles.ColumnCount;
                        cmax = 0;
                        rmin = lastrowindex;
                        rmax = 0;
                        foreach (DataGridViewCell cell in dataGridSubtitles.SelectedCells)
                        {
                            if (cell.RowIndex > lastrowindex) continue;
                            if (cell.ColumnIndex < cmin) cmin = cell.ColumnIndex;
                            if (cell.ColumnIndex > cmax) cmax = cell.ColumnIndex;
                            if (cell.RowIndex < rmin) rmin = cell.RowIndex;
                            if (cell.RowIndex > rmax) rmax = cell.RowIndex;
                        }
                        nr = rmax - rmin + 1;
                        nc = cmax - cmin + 1;
                        content = new string[nr, nc];
                        foreach (DataGridViewCell cell in dataGridSubtitles.SelectedCells)
                        {
                            if (cell.RowIndex > lastrowindex) continue;
                            content[cell.RowIndex - rmin, cell.ColumnIndex - cmin] = cell.Value.ToString();
                        }
                        for (int r = 0; r < nr; r++)
                        {
                            for (int c = 0; c < nc; c++)
                            {
                                cb += content[r, c];
                                if (c != nc - 1) cb += "\t";
                            }
                            cb += Environment.NewLine;
                        }
                        Clipboard.SetText(cb);
                    }

                }
                //粘贴，支持多个单元格的复制和粘贴
                else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control && !m_keyhold[(int)e.KeyCode])
                {
                    if (dataGridSubtitles.CurrentCell != null && dataGridSubtitles.CurrentRow.Index <= lastrowindex
                        && Clipboard.ContainsText())
                    {
                        int cC, cR;
                        cC = dataGridSubtitles.CurrentCell.ColumnIndex;
                        cR = dataGridSubtitles.CurrentCell.RowIndex;
                        string[] cells;
                        char[] spliter = { '\t' };
                        StringReader strReader = new StringReader(Clipboard.GetText());
                        string line = strReader.ReadLine();
                        m_UndoRec.BeginMultiCells(); //开始Undo记录
                        while (line != null)
                        {
                            cells = line.Split(spliter, 3 - cC);
                            for (int i = 0; i < cells.Length; i++)
                            {
                                if (cells[i].Length != 0)
                                {
                                    m_UndoRec.EditMultiCells(cR, cC + i, dataGridSubtitles.Rows[cR].Cells[cC + i].Value.ToString());
                                    dataGridSubtitles.Rows[cR].Cells[cC + i].Value = cells[i];
                                }
                            }
                            cR++;
                            if (cR > lastrowindex) break;
                            line = strReader.ReadLine();
                        }
                        m_UndoRec.EndEditMultiCells();//结束Undo记录
                        Edited = true;
                        m_CurrentSub.RefreshIndex();
                    }
                }

                else if (e.KeyCode == m_Config.EnterEditMode)
                {
                    if (dataGridSubtitles.CurrentCell != null)
                    {
                        dataGridSubtitles.BeginEdit(true);
                    }
                }
                else if (e.KeyCode == Keys.Delete && e.Modifiers != Keys.Control && !m_keyhold[(int)e.KeyCode])
                {
                    //清空选中单元格内容
                    if (dataGridSubtitles.SelectedCells.Count != 0)
                    {
                        m_UndoRec.BeginMultiCells(); //开始Undo记录
                        foreach (DataGridViewCell cell in dataGridSubtitles.SelectedCells)
                        {
                            if (cell.RowIndex > lastrowindex) continue;
                            m_UndoRec.EditMultiCells(cell.RowIndex, cell.ColumnIndex, cell.Value.ToString());
                            dataGridSubtitles.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = "";
                        }
                        m_UndoRec.EndEditMultiCells();
                        Edited = true;
                    }
                }
                else if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control && !m_keyhold[(int)e.KeyCode])
                {
                    //删除选中的行
                    if (dataGridSubtitles.CurrentRow != null)
                    {
                        List<DataGridViewRow> deleteRow = new List<DataGridViewRow>();
                        foreach (DataGridViewCell cell in dataGridSubtitles.SelectedCells)
                        {
                            if (cell.RowIndex > lastrowindex) continue;
                            if (!deleteRow.Contains(dataGridSubtitles.Rows[cell.RowIndex]))
                                deleteRow.Add(dataGridSubtitles.Rows[cell.RowIndex]);
                        }
                        foreach (DataGridViewRow row in deleteRow)
                        {
                            DeleteRow(row);
                        }
                    }
                }
                #endregion
                #region File Keys
                else if (e.KeyCode == m_Config.SaveAss && !m_keyhold[(int)e.KeyCode])
                {
                    if (m_SubLoaded)
                    {
                        if (KeySaveAss != null) KeySaveAss(this, new EventArgs());
                    }
                }
                #endregion
                m_keyhold[(int)e.KeyCode] = true;
            }
        }

        private void dataGridSubtitles_KeyUp(object sender, KeyEventArgs e)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            int rowIndex = (dataGridSubtitles.CurrentRow == null) ? -1 : dataGridSubtitles.CurrentRow.Index;
            if (m_Config == null) return;
            if (e.KeyCode == m_Config.AddTimePoint)
            {
                if (rowIndex > -1 && rowIndex <= lastrowindex)
                {
                    TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.EndTime, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + m_Config.EndOffset;
                    if (!timeEditArgs.CancelEvent)
                    {
                        EditEndTime(rowIndex, time);
                        if (rowIndex < lastrowindex)
                            dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex + 1].Cells[0];
                    }
                }
            }
            if ((int)e.KeyCode >= 0 && (int)e.KeyCode < 256)
            {
                m_keyhold[(int)e.KeyCode] = false;
            }
        }

        new public bool Focus()
        {
            return dataGridSubtitles.Focus();
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
        public bool CancelEvent;
        public TimeEditEventArgs(TimeType editTime, double timevalue, bool cancelEvent)
        {
            EditTime = editTime;
            TimeValue = timevalue;
            CancelEvent = cancelEvent;
        }
    }
    public enum TimeType { BeginTime, EndTime, Unknown };

    public class PlayerControlEventArgs : EventArgs
    {
        public PlayerControlEventArgs(PlayerCommand cmd)
        {
            ControlCMD = cmd;
        }
        public PlayerCommand ControlCMD;
    }
    public enum PlayerCommand{Play,Pause,Toggle};

    public class CurrentRowChangeEventArgs : EventArgs
    {
        public int CurrentRowIndex;
        public CurrentRowChangeEventArgs(int rowIndex)
        {
            CurrentRowIndex = rowIndex;
        }
    }
}
