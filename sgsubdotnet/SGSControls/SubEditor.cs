using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace SGSControls
{
    public partial class SubEditor : UserControl
    {
        public SubEditor()
        {
            InitializeComponent();

            dataGridSubtitles.AutoGenerateColumns = false;
            dataGridSubtitles.AutoSize = false;

            var column1 = new DataGridViewTextBoxColumn {HeaderText = @"Begin Time", DataPropertyName = "StartTime"};
            dataGridSubtitles.Columns.Add(column1);

            var column2 = new DataGridViewTextBoxColumn {HeaderText = @"End Time", DataPropertyName = "EndTime"};
            dataGridSubtitles.Columns.Add(column2);

            var column3 = new DataGridViewTextBoxColumn
                         {
                             HeaderText = @"Text",
                             DataPropertyName = "Text",
                             AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                         };
            dataGridSubtitles.Columns.Add(column3);
            dataGridSubtitles.AllowUserToAddRows = false;

            _mSelectCells.Rows = dataGridSubtitles.Rows;
        }
        #region Private Members

        /// <summary>
        /// 字幕内容
        /// </summary>
        private Subtitle.AssSub _mCurrentSub = new Subtitle.AssSub();
        private Config.SGSConfig _mConfig;
        private bool _mSubLoaded;
        private double _mVideoLength;
        private readonly UndoRecord _mUndoRec = new UndoRecord();
        private readonly SelectCells _mSelectCells = new SelectCells();
        #endregion

        public Subtitle.AssSub CurrentSub
        {
            set
            {
                _mCurrentSub = value;
                if (value != null && value.SubItems.Count > 0)
                {
                    dataGridSubtitles.DataSource = value.SubItems;
                    dataGridSubtitles.AllowUserToAddRows = true;
                    _mSubLoaded = true;
                    _mCurrentSub.CreateIndex(_mVideoLength);
                    Edited = false;
                }
                else
                {
                    dataGridSubtitles.AllowUserToAddRows = false;

                    _mSubLoaded = false;
                    Edited = false;
                }
                _mUndoRec.Reset();
                _mSelectCells.Reset();
            }
        }

        public Config.SGSConfig Config
        {
            set
            {
                _mConfig = value;
            }
        }

        public double VideoLength
        {
            set
            {
                _mVideoLength = value;
                if (_mSubLoaded) _mCurrentSub.CreateIndex(_mVideoLength);
            }

        }

        public int CurrentRowIndex
        {
            get
            {
                return _mSubLoaded && dataGridSubtitles.CurrentRow != null ? dataGridSubtitles.CurrentRow.Index : -1;
            }
            set
            {
                if (_mSubLoaded && value >= 0 && value < dataGridSubtitles.RowCount - 1)
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
        public void EditBeginTime(int rowIndex, double value)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (rowIndex >= 0 && rowIndex <= lastrowindex)
            {
                var item = (Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex].DataBoundItem);
                _mUndoRec.Edit(rowIndex, 0, item.StartTime);//记录Undo
                double os = item.Start.TimeValue;
                item.Start.TimeValue = value > 0 ? value : 0;
                _mCurrentSub.ItemEdited(item, os, item.End.TimeValue);
                dataGridSubtitles.UpdateCellValue(0, rowIndex);
                Edited = true;
            }

        }
        public void EditEndTime(int rowIndex, double value)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (rowIndex >= 0 && rowIndex <= lastrowindex)
            {
                var item = (Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex].DataBoundItem);
                _mUndoRec.Edit(rowIndex, 1, item.EndTime);//记录Undo
                double oe = item.End.TimeValue;
                item.End.TimeValue = value > 0 ? value : 0;
                _mCurrentSub.ItemEdited(item, item.Start.TimeValue, oe);
                dataGridSubtitles.UpdateCellValue(1, rowIndex);
                Edited = true;
            }
        }

        public void EditCellTime(int rowIndex, int colIndex, double value)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (rowIndex >= 0 && rowIndex <= lastrowindex)
            {
                var item = (Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex].DataBoundItem);
                if (colIndex == 0)
                {
                    _mUndoRec.Edit(rowIndex, 0, item.StartTime);//记录Undo
                    double os = item.Start.TimeValue;
                    item.Start.TimeValue = value > 0 ? value : 0;
                    _mCurrentSub.ItemEdited(item, os, item.End.TimeValue);
                    dataGridSubtitles.UpdateCellValue(0, rowIndex);
                    Edited = true;
                }
                else if (colIndex == 1)
                {
                    _mUndoRec.Edit(rowIndex, 1, item.EndTime);//记录Undo
                    double oe = item.End.TimeValue;
                    item.End.TimeValue = value > 0 ? value : 0;
                    _mCurrentSub.ItemEdited(item, item.Start.TimeValue, oe);
                    dataGridSubtitles.UpdateCellValue(1, rowIndex);
                    Edited = true;
                }
            }
        }

        public void DisplayTime(double time)
        {
            if (_mSubLoaded)
                labelSub.Text = _mCurrentSub.GetSubtitle(time);
        }
        #endregion

        private void DeleteRow(DataGridViewRow row)
        {
            _mUndoRec.DeleteRow(row.Index, row);//为Undo记录删除操作
            var i = ((Subtitle.AssItem)(row.DataBoundItem));
            _mCurrentSub.SubItems.Remove(i);
            _mSelectCells.Reset();//清空标记的单元格
            dataGridSubtitles.Refresh();
            _mCurrentSub.RefreshIndex();
            Edited = true;
        }

        private void InsertNewRow(int index, DataGridViewRow currentRow)
        {
            Subtitle.AssItem i = ((Subtitle.AssItem)(currentRow.DataBoundItem)).Clone();
            i.Text = "";
            i.Start.TimeValue = 0;
            i.End.TimeValue = 0;
            _mCurrentSub.SubItems.Insert(index, i);
            _mCurrentSub.RefreshIndex();
            _mUndoRec.InsertRow(index);//为Undo记录插入操作
            _mSelectCells.Reset(); //清空标记的单元格
            dataGridSubtitles.Refresh();
            Edited = true;
        }

        #region Event handlers
        private void tsbtnJumpto_Click(object sender, EventArgs e)
        {
            Subtitle.AssItem item;
            if (dataGridSubtitles.CurrentRow != null && _mSubLoaded
                && (item = (Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                )
            {
                double time = item.Start.TimeValue;
                var seekevent = new SeekEventArgs(SeekDir.Begin, time);
                if (Seek != null) Seek(this, seekevent);
            }
        }

        private void tsbtnDuplicate_Click(object sender, EventArgs e)
        {
            Subtitle.AssItem item;
            if (dataGridSubtitles.CurrentRow != null && _mSubLoaded
                && (item = (Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                )
            {
                Subtitle.AssItem i = item.Clone();
                _mCurrentSub.SubItems.Insert(dataGridSubtitles.CurrentRow.Index + 1, i);
                _mUndoRec.InsertRow(dataGridSubtitles.CurrentRow.Index + 1);//为Undo记录插入操作
                _mSelectCells.Reset();//清空标记的单元格
                dataGridSubtitles.Refresh();
                _mCurrentSub.RefreshIndex();
                Edited = true;
            }
        }

        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridSubtitles.CurrentRow != null && _mSubLoaded
                && dataGridSubtitles.CurrentRow.DataBoundItem != null
                )
            {
                DeleteRow(dataGridSubtitles.CurrentRow);
            }
        }

        private void tsbtnInsBefore_Click(object sender, EventArgs e)
        {
            if (dataGridSubtitles.CurrentRow != null && _mSubLoaded
                && dataGridSubtitles.CurrentRow.DataBoundItem != null
                )
            {
                InsertNewRow(dataGridSubtitles.CurrentRow.Index, dataGridSubtitles.CurrentRow);
            }
        }

        private void tsbtnInsAfter_Click(object sender, EventArgs e)
        {
            if (dataGridSubtitles.CurrentRow != null && _mSubLoaded
                && dataGridSubtitles.CurrentRow.DataBoundItem != null
                )
            {
                InsertNewRow(dataGridSubtitles.CurrentRow.Index + 1, dataGridSubtitles.CurrentRow);
            }
        }

        private double _oldS;
        private double _oldE;
        private string _oldString;
        private bool _cancelEdit;
        private void dataGridSubtitles_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _mSelectCells.Reset();
            _oldString = dataGridSubtitles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (e.ColumnIndex != 2)
            {
                var item = (Subtitle.AssItem)dataGridSubtitles.Rows[e.RowIndex].DataBoundItem;
                if (item == null)
                {
                    _cancelEdit = true;
                    _oldString = "";
                }
                else
                {
                    _oldS = item.Start.TimeValue;
                    _oldE = item.End.TimeValue;
                }
            }
        }

        private void dataGridSubtitles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!_cancelEdit)
            {
                string newString = dataGridSubtitles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (!_oldString.Equals(newString))
                {
                    _mUndoRec.Edit(e.RowIndex, e.ColumnIndex, _oldString);//比较单元格内容，如改变，记录undo
                    Edited = true;
                }
                if (e.ColumnIndex != 2)
                {
                    var item = (Subtitle.AssItem)dataGridSubtitles.Rows[e.RowIndex].DataBoundItem;
                    _mCurrentSub.ItemEdited(item, _oldS, _oldE);
                }
            }

        }

        private void dataGridSubtitles_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            _mUndoRec.InsertRow(e.Row.Index - 1);
            _cancelEdit = false;
            _oldS = 0;
            _oldE = 0;
            _oldString = "";
            _mCurrentSub.RefreshIndex();
            Edited = true;
        }

        private void tsbtnUndo_Click(object sender, EventArgs e)
        {
            _mSelectCells.Reset();
            _mUndoRec.Undo(_mCurrentSub);
            dataGridSubtitles.Refresh();
            _mCurrentSub.RefreshIndex();
            Edited = true;
        }


        private enum TimeCheckStatus { OK = 0, OVERLAP, ERROR };

        private void tsbtnTimeLineScan_Click(object sender, EventArgs e)
        {
            if (_mSubLoaded)
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
                    timeerror ? "发现错误时间点" :
                    overlap ? "发现时间轴重叠" : "未发现时间轴重叠和错误时间点";

                MessageBox.Show(msg, @"时间轴检查");

            }
        }

        private void tsbtnMarkCells_Click(object sender, EventArgs e)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            foreach (var cell in
                dataGridSubtitles.SelectedCells.Cast<DataGridViewCell>().Where(cell => cell.RowIndex <= lastrowindex))
            {
                _mSelectCells.SelectCell(cell.ColumnIndex, cell.RowIndex);
            }
        }

        private void tsbtnUnmarkAll_Click(object sender, EventArgs e)
        {
            _mSelectCells.DeselectAll();
        }

        private void tsbtnTimeOffset_Click(object sender, EventArgs e)
        {
            if (!_mSubLoaded) return;
            var toDlg = new TimeOffsetDialog();
            if (toDlg.ShowDialog() == DialogResult.OK)
            {
                _mSelectCells.TimeOffset(toDlg.TimeOffset, _mUndoRec);
                dataGridSubtitles.Refresh();
            }
        }

        private void tsbtnPause_Click(object sender, EventArgs e)
        {
            var arg = new PlayerControlEventArgs(PlayerCommand.Pause);
            if (PlayerControl != null) PlayerControl(this, arg);
        }

        private void tsbtnPlay_Click(object sender, EventArgs e)
        {
            var arg = new PlayerControlEventArgs(PlayerCommand.Play);
            if (PlayerControl != null) PlayerControl(this, arg);
        }

        #endregion

        /// <summary>
        /// Record Key Status
        /// </summary>
        private readonly bool[] _mKeyhold = new bool[256];


        private void dataGridSubtitles_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.Cell == null) return;
            if (CurrentRowChanged == null) return;

            int rowindex = e.Cell.RowIndex;
            var args = new CurrentRowChangeEventArgs(rowindex);
            CurrentRowChanged(this, args);
        }

        private void dataGridSubtitles_KeyDown(object sender, KeyEventArgs e)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (_mConfig == null) return;
            if ((int) e.KeyCode < 0 || (int) e.KeyCode >= 256) return;

            int rowIndex = (dataGridSubtitles.CurrentRow == null) ? -1 : dataGridSubtitles.CurrentRow.Index;

            #region Timeing Keys
            if (e.KeyCode == _mConfig.AddTimePoint && !_mKeyhold[(int)e.KeyCode])
            {
                //单键插入时间

                if (rowIndex >= 0 && rowIndex <= lastrowindex)
                {
                    var timeEditArgs = new TimeEditEventArgs(TimeType.BeginTime, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + _mConfig.StartOffset;
                    if (!timeEditArgs.CancelEvent)
                    {
                        if (rowIndex > 0 && _mConfig.AutoOverlapCorrection)
                        {
                            Subtitle.AssItem lastitem = ((Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                            if (lastitem.End.TimeValue - time > 0 &&
                                lastitem.End.TimeValue - time < Math.Max(Math.Abs(_mConfig.StartOffset), Math.Abs(_mConfig.EndOffset)))
                            {
                                EditEndTime(rowIndex - 1, time - 0.01);
                            }
                        }
                        EditBeginTime(rowIndex, timeEditArgs.TimeValue);
                        dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex].Cells[1];
                    }
                }
            }
            else if (e.KeyCode == _mConfig.AddStartTime && !_mKeyhold[(int)e.KeyCode])
            {
                //插入开始时间
                if (rowIndex >= 0 && rowIndex <= lastrowindex)
                {
                    TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.BeginTime, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + _mConfig.StartOffset;
                    if (!timeEditArgs.CancelEvent)
                    {
                        if (rowIndex > 0 && _mConfig.AutoOverlapCorrection)
                        {
                            Subtitle.AssItem lastitem = ((Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                            if (lastitem.End.TimeValue - time > 0 &&
                                lastitem.End.TimeValue - time < Math.Max(Math.Abs(_mConfig.StartOffset), Math.Abs(_mConfig.EndOffset)))
                            {
                                EditEndTime(rowIndex - 1, time - 0.01);
                            }
                        }
                        EditBeginTime(rowIndex, time);
                        dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex].Cells[1];
                    }
                }
            }
            else if (e.KeyCode == _mConfig.AddEndTime && !_mKeyhold[(int)e.KeyCode])
            {
                //插入结束时间
                if (rowIndex >= 0 && rowIndex <= lastrowindex)
                {
                    TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.EndTime, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + _mConfig.EndOffset;
                    if (!timeEditArgs.CancelEvent)
                    {
                        EditEndTime(rowIndex, time);
                        if (rowIndex < lastrowindex)
                            dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex + 1].Cells[0];
                    }
                }
            }
            else if (e.KeyCode == _mConfig.AddContTimePoint && !_mKeyhold[(int)e.KeyCode])
            {
                //连续插入时间
                if (rowIndex >= 0 && rowIndex <= lastrowindex)
                {
                    TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.Unknown, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + _mConfig.StartOffset;
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
            else if (e.KeyCode == _mConfig.AddCellTime && !_mKeyhold[(int)e.KeyCode])
            {
                //插入单元格时间
                if (rowIndex >= 0 && rowIndex <= lastrowindex)
                {

                    TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.Unknown, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    if (!timeEditArgs.CancelEvent)
                    {
                        double time = timeEditArgs.TimeValue + _mConfig.StartOffset;
                        int colIndex = dataGridSubtitles.CurrentCell.ColumnIndex;
                        if (colIndex == 0) //插入开始时间
                        {
                            if (rowIndex > 0 && _mConfig.AutoOverlapCorrection)
                            {
                                Subtitle.AssItem lastitem = ((Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                                if (lastitem.End.TimeValue - time > 0 &&
                                    lastitem.End.TimeValue - time < Math.Max(Math.Abs(_mConfig.StartOffset), Math.Abs(_mConfig.EndOffset)))
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
            else if (e.KeyCode == _mConfig.Pause && !_mKeyhold[(int)e.KeyCode])
            {
                var arg = new PlayerControlEventArgs(PlayerCommand.Toggle);
                if (PlayerControl != null) PlayerControl(this, arg);
            }
            else if (e.KeyCode == _mConfig.SeekBackword && !_mKeyhold[(int)e.KeyCode])
            {
                var seekevent = new SeekEventArgs(SeekDir.CurrentPos, -_mConfig.SeekStep);
                if (Seek != null) Seek(this, seekevent);
            }
            else if (e.KeyCode == _mConfig.SeekForward && !_mKeyhold[(int)e.KeyCode])
            {
                var seekevent = new SeekEventArgs(SeekDir.CurrentPos, _mConfig.SeekStep);
                if (Seek != null) Seek(this, seekevent);
            }
            else if (e.KeyCode == _mConfig.GotoCurrent && !_mKeyhold[(int)e.KeyCode])
            {
                Subtitle.AssItem item;
                if (dataGridSubtitles.CurrentRow != null && _mSubLoaded
                    && (item = (Subtitle.AssItem)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                    )
                {
                    double time = item.Start.TimeValue;
                    var seekevent = new SeekEventArgs(SeekDir.Begin, time);
                    if (Seek != null) Seek(this, seekevent);
                }
            }
            else if (e.KeyCode == _mConfig.GotoPrevious && !_mKeyhold[(int)e.KeyCode])
            {
                if (rowIndex > 0 && rowIndex <= lastrowindex)
                {
                    var item = ((Subtitle.AssItem)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                    var seekevent = new SeekEventArgs(SeekDir.Begin, item.Start.TimeValue);
                    if (Seek != null) Seek(this, seekevent);
                }
            }

                #endregion
            #region Edit Keys
                //复制，支持多个单元格的复制和粘贴
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control && !_mKeyhold[(int)e.KeyCode])
            {
                if (dataGridSubtitles.CurrentCell != null)
                {
                    string cb = "";
                    //行，列的取值范围
                    int cmin = dataGridSubtitles.ColumnCount;
                    int cmax = 0;
                    int rmin = lastrowindex;
                    int rmax = 0;
                    foreach (DataGridViewCell cell in dataGridSubtitles.SelectedCells)
                    {
                        if (cell.RowIndex > lastrowindex) continue;
                        if (cell.ColumnIndex < cmin) cmin = cell.ColumnIndex;
                        if (cell.ColumnIndex > cmax) cmax = cell.ColumnIndex;
                        if (cell.RowIndex < rmin) rmin = cell.RowIndex;
                        if (cell.RowIndex > rmax) rmax = cell.RowIndex;
                    }
                    //行，列的个数
                    int nr = rmax - rmin + 1;
                    int nc = cmax - cmin + 1;
                    //内容
                    var content = new string[nr, nc];
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
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control && !_mKeyhold[(int)e.KeyCode])
            {
                if (dataGridSubtitles.CurrentRow != null && dataGridSubtitles.CurrentRow.Index <= lastrowindex
                    && Clipboard.ContainsText())
                {
                    int cC = dataGridSubtitles.CurrentCell.ColumnIndex;
                    int cR = dataGridSubtitles.CurrentCell.RowIndex;
                    string[] cells;
                    char[] spliter = { '\t' };
                    var strReader = new StringReader(Clipboard.GetText());
                    string line = strReader.ReadLine();
                    _mUndoRec.BeginMultiCells(); //开始Undo记录
                    while (line != null)
                    {
                        cells = line.Split(spliter, 3 - cC);
                        for (int i = 0; i < cells.Length; i++)
                        {
                            if (cells[i].Length != 0)
                            {
                                _mUndoRec.EditMultiCells(cR, cC + i, dataGridSubtitles.Rows[cR].Cells[cC + i].Value.ToString());
                                dataGridSubtitles.Rows[cR].Cells[cC + i].Value = cells[i];
                            }
                        }
                        cR++;
                        if (cR > lastrowindex) break;
                        line = strReader.ReadLine();
                    }
                    _mUndoRec.EndEditMultiCells();//结束Undo记录
                    Edited = true;
                    _mCurrentSub.RefreshIndex();
                }
            }

            else if (e.KeyCode == _mConfig.EnterEditMode)
            {
                if (dataGridSubtitles.CurrentCell != null)
                {
                    dataGridSubtitles.BeginEdit(true);
                }
            }
            else if (e.KeyCode == Keys.Delete && e.Modifiers != Keys.Control && !_mKeyhold[(int)e.KeyCode])
            {
                //清空选中单元格内容
                if (dataGridSubtitles.SelectedCells.Count != 0)
                {
                    _mUndoRec.BeginMultiCells(); //开始Undo记录
                    foreach (DataGridViewCell cell in dataGridSubtitles.SelectedCells)
                    {
                        if (cell.RowIndex > lastrowindex) continue;
                        _mUndoRec.EditMultiCells(cell.RowIndex, cell.ColumnIndex, cell.Value.ToString());
                        dataGridSubtitles.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = "";
                    }
                    _mUndoRec.EndEditMultiCells();
                    Edited = true;
                }
            }
            else if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control && !_mKeyhold[(int)e.KeyCode])
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
            else if (e.KeyCode == _mConfig.SaveAss && !_mKeyhold[(int)e.KeyCode])
            {
                if (_mSubLoaded)
                {
                    if (KeySaveAss != null) KeySaveAss(this, new EventArgs());
                }
            }
            #endregion
            _mKeyhold[(int)e.KeyCode] = true;
        }

        private void dataGridSubtitles_KeyUp(object sender, KeyEventArgs e)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            int rowIndex = (dataGridSubtitles.CurrentRow == null) ? -1 : dataGridSubtitles.CurrentRow.Index;
            if (_mConfig == null) return;
            if (e.KeyCode == _mConfig.AddTimePoint)
            {
                if (rowIndex > -1 && rowIndex <= lastrowindex)
                {
                    var timeEditArgs = new TimeEditEventArgs(TimeType.EndTime, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + _mConfig.EndOffset;
                    if (!timeEditArgs.CancelEvent)
                    {
                        EditEndTime(rowIndex, time);
                        if (rowIndex < lastrowindex)
                            dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex + 1].Cells[0];
                    }
                }
            }
            if ((int) e.KeyCode >= 0 && (int) e.KeyCode < 256)
            {
                _mKeyhold[(int) e.KeyCode] = false;
            }
        }

        new public bool Focus()
        {
            return dataGridSubtitles.Focus();
        }

    }

    public class SeekEventArgs : EventArgs
    {
        public readonly double SeekOffset;
        public readonly SeekDir SeekDirection;
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
        public readonly PlayerCommand ControlCMD;
    }
    public enum PlayerCommand{Play,Pause,Toggle};

    public class CurrentRowChangeEventArgs : EventArgs
    {
        public readonly int CurrentRowIndex;
        public CurrentRowChangeEventArgs(int rowIndex)
        {
            CurrentRowIndex = rowIndex;
        }
    }
}
