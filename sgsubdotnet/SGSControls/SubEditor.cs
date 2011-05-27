using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using SGSDatatype;

namespace SGSControls
{
    public partial class SubEditor : UserControl
    {
        public SubEditor()
        {
            InitializeComponent();

            dataGridSubtitles.AutoGenerateColumns = false;
            dataGridSubtitles.AutoSize = false;

            var column1 = new DataGridViewTextBoxColumn { HeaderText = @"Start Time", DataPropertyName = "Start" };
            dataGridSubtitles.Columns.Add(column1);

            var column2 = new DataGridViewTextBoxColumn {HeaderText = @"End Time", DataPropertyName = "End"};
            dataGridSubtitles.Columns.Add(column2);

            var column3 = new DataGridViewTextBoxColumn
                         {
                             HeaderText = @"Text",
                             DataPropertyName = "Text",
                             AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                         };
            dataGridSubtitles.Columns.Add(column3);
            dataGridSubtitles.AllowUserToAddRows = false;

            _selectCells.Rows = dataGridSubtitles.Rows;
        }
        #region Private Members

        /// <summary>
        /// 字幕内容
        /// </summary>
        private SubStationAlpha _currentSub = new SubStationAlpha();
        private SSAIndex _subIndex = new SSAIndex();
        private bool _subLoaded;
        private double _videoLength;
        private readonly UndoRecord _undoRec = new UndoRecord();
        private readonly SelectCells _selectCells = new SelectCells();
        #endregion

        public SubStationAlpha CurrentSub
        {
            set
            {
                _currentSub = value;
                if (value != null && value.EventsSection.EventList.Count > 0)
                {
                    dataGridSubtitles.DataSource = value.EventsSection.EventList;
                    dataGridSubtitles.AllowUserToAddRows = true;
                    _subLoaded = true;
                    _subIndex.Subtitle = value;
                    _subIndex.CreateIndex(_videoLength);
                    Edited = false;
                }
                else
                {
                    dataGridSubtitles.AllowUserToAddRows = false;
                    _subLoaded = false;
                    Edited = false;
                }
                _undoRec.Reset();
                _selectCells.Reset();
            }
        }

        public SGSConfig Config { private get; set; }

        public SGSAutoSave Autosave { private get; set; }


        public double VideoLength
        {
            set
            {
                _videoLength = value;
                if (_subLoaded) _subIndex.CreateIndex(_videoLength);
            }

        }

        public int CurrentRowIndex
        {
            get
            {
                return _subLoaded && dataGridSubtitles.CurrentRow != null ? dataGridSubtitles.CurrentRow.Index : -1;
            }
            set
            {
                if (_subLoaded && value >= 0 && value < dataGridSubtitles.RowCount - 1)
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
        public event EventHandler AutosaveEvent = null;
        #endregion

        #region Methods
        public void EditStartTime(int rowIndex, double value)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (rowIndex >= 0 && rowIndex <= lastrowindex)
            {
                var item = (V4Event)(dataGridSubtitles.Rows[rowIndex].DataBoundItem);
                _undoRec.Edit(rowIndex, 0, item.Start.ToString());//记录Undo
                double os = item.Start.Value;
                item.Start.Value = value > 0 ? value : 0;
                _subIndex.ItemEdited(item, os, item.End.Value);
                dataGridSubtitles.UpdateCellValue(0, rowIndex);
                subtitleEdited();
            }

        }
        public void EditEndTime(int rowIndex, double value)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (rowIndex >= 0 && rowIndex <= lastrowindex)
            {
                var item = (V4Event)(dataGridSubtitles.Rows[rowIndex].DataBoundItem);
                _undoRec.Edit(rowIndex, 1, item.End.ToString());//记录Undo
                double oe = item.End.Value;
                item.End.Value = value > 0 ? value : 0;
                _subIndex.ItemEdited(item, item.Start.Value, oe);
                dataGridSubtitles.UpdateCellValue(1, rowIndex);
                subtitleEdited();
            }
        }

        public void EditCellTime(int rowIndex, int colIndex, double value)
        {
            int lastrowindex = dataGridSubtitles.RowCount - 2;
            if (rowIndex >= 0 && rowIndex <= lastrowindex)
            {
                var item = (V4Event)(dataGridSubtitles.Rows[rowIndex].DataBoundItem);
                if (colIndex == 0)
                {
                    _undoRec.Edit(rowIndex, 0, item.Start.ToString());//记录Undo
                    double os = item.Start.Value;
                    item.Start.Value = value > 0 ? value : 0;
                    _subIndex.ItemEdited(item, os, item.End.Value);
                    dataGridSubtitles.UpdateCellValue(0, rowIndex);
                    subtitleEdited();
                }
                else if (colIndex == 1)
                {
                    _undoRec.Edit(rowIndex, 1, item.End.ToString());//记录Undo
                    double oe = item.End.Value;
                    item.End.Value = value > 0 ? value : 0;
                    _subIndex.ItemEdited(item, item.Start.Value, oe);
                    dataGridSubtitles.UpdateCellValue(1, rowIndex);
                    subtitleEdited();
                }
            }
        }

        public void DisplayTime(double time)
        {
            if (_subLoaded)
                labelSub.Text = _subIndex.GetSubtitle(time);
        }
        #endregion

        private void DeleteRow(DataGridViewRow row)
        {
            _undoRec.DeleteRow(row.Index, row);//为Undo记录删除操作
            var i = (V4Event)(row.DataBoundItem);
            _currentSub.EventsSection.EventList.Remove(i);
            _selectCells.Reset();//清空标记的单元格
            dataGridSubtitles.Refresh();
            _subIndex.RefreshIndex();
            subtitleEdited();
        }

        private void InsertNewRow(int index, DataGridViewRow currentRow)
        {
            V4Event i = _currentSub.CreateEmptyEvent("");//((V4Event)(currentRow.DataBoundItem)).Clone();
            _currentSub.EventsSection.EventList.Insert(index, i);
            _subIndex.RefreshIndex();
            _undoRec.InsertRow(index);//为Undo记录插入操作
            _selectCells.Reset(); //清空标记的单元格
            dataGridSubtitles.Refresh();
            subtitleEdited();
        }

        #region Event handlers
        private void tsbtnJumpto_Click(object sender, EventArgs e)
        {
            V4Event item;
            if (dataGridSubtitles.CurrentRow != null && _subLoaded
                && (item = (V4Event)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                )
            {
                double time = item.Start.Value;
                var seekevent = new SeekEventArgs(SeekDir.Begin, time);
                if (Seek != null) Seek(this, seekevent);
            }
        }

        private void tsbtnDuplicate_Click(object sender, EventArgs e)
        {
            dataGridSubtitles.EndEdit();
            V4Event item;
            if (dataGridSubtitles.CurrentRow != null && _subLoaded
                && (item = (V4Event)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                )
            {
                V4Event i = item.Clone();
                _currentSub.EventsSection.EventList.Insert(dataGridSubtitles.CurrentRow.Index + 1, i);
                _undoRec.InsertRow(dataGridSubtitles.CurrentRow.Index + 1);//为Undo记录插入操作
                _selectCells.Reset();//清空标记的单元格
                dataGridSubtitles.Refresh();
                _subIndex.RefreshIndex();
                subtitleEdited();
            }
        }

        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            dataGridSubtitles.EndEdit();
            if (dataGridSubtitles.CurrentRow != null && _subLoaded
                && dataGridSubtitles.CurrentRow.DataBoundItem != null
                )
            {
                DeleteRow(dataGridSubtitles.CurrentRow);
            }
        }

        private void tsbtnInsBefore_Click(object sender, EventArgs e)
        {
            dataGridSubtitles.EndEdit();
            if (dataGridSubtitles.CurrentRow != null && _subLoaded
                && dataGridSubtitles.CurrentRow.DataBoundItem != null
                )
            {
                InsertNewRow(dataGridSubtitles.CurrentRow.Index, dataGridSubtitles.CurrentRow);
            }
        }

        private void tsbtnInsAfter_Click(object sender, EventArgs e)
        {
            dataGridSubtitles.EndEdit();
            if (dataGridSubtitles.CurrentRow != null && _subLoaded
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
            _selectCells.Reset();
            _oldString = dataGridSubtitles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (e.ColumnIndex != 2)
            {
                var item = (V4Event)dataGridSubtitles.Rows[e.RowIndex].DataBoundItem;
                if (item == null)
                {
                    _cancelEdit = true;
                    _oldString = "";
                }
                else
                {
                    _oldS = item.Start.Value;
                    _oldE = item.End.Value;
                }
            }
        }

        private void dataGridSubtitles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!_cancelEdit)
            {
                var value = dataGridSubtitles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                var newString = (value == null ? "" : value.ToString());
                if (!_oldString.Equals(newString))
                {
                    _undoRec.Edit(e.RowIndex, e.ColumnIndex, _oldString);//比较单元格内容，如改变，记录undo
                    subtitleEdited();
                }
                if (e.ColumnIndex != 2)
                {
                    var item = (V4Event)dataGridSubtitles.Rows[e.RowIndex].DataBoundItem;
                    _subIndex.ItemEdited(item, _oldS, _oldE);
                }
            }

        }

        private void dataGridSubtitles_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            _undoRec.InsertRow(e.Row.Index - 1);
            _cancelEdit = false;
            _oldS = 0;
            _oldE = 0;
            _oldString = "";
            _subIndex.RefreshIndex();
            subtitleEdited();

        }

        private void tsbtnUndo_Click(object sender, EventArgs e)
        {
            dataGridSubtitles.EndEdit();
            _selectCells.Reset();
            _undoRec.Undo(_currentSub);
            dataGridSubtitles.Refresh();
            _subIndex.RefreshIndex();
            subtitleEdited();
        }


        private enum TimeCheckStatus { Ok = 0, Overlap, Error };

        private void tsbtnTimeLineScan_Click(object sender, EventArgs e)
        {
            if (_subLoaded)
            {
                bool overlap = false;
                bool timeerror = false;
                int rowCount = dataGridSubtitles.Rows.Count - 1;
                var itemStatus = new TimeCheckStatus[rowCount];
                for (int i = 0; i < rowCount - 1; i++)
                {
                    var itema = (V4Event)(dataGridSubtitles.Rows[i].DataBoundItem);
                    if (itema.Start.Value > itema.End.Value)
                    {
                        itemStatus[i] = TimeCheckStatus.Error;
                        continue;
                    }
                    for (int j = i + 1; j < rowCount; j++)
                    {
                        var itemb = (V4Event)(dataGridSubtitles.Rows[j].DataBoundItem);
                        if (itemb.Start.Value > itemb.End.Value)
                        {
                            itemStatus[j] = TimeCheckStatus.Error;
                            continue;
                        }
                        if ((
                            itema.End.Value >= itemb.Start.Value && itema.Start.Value <= itemb.Start.Value ||
                            itemb.End.Value >= itema.Start.Value && itemb.Start.Value <= itema.Start.Value) &&
                            itema.End.Value - itema.Start.Value > 0 && itemb.End.Value - itemb.Start.Value > 0
                        )
                        {
                            itemStatus[i] = TimeCheckStatus.Overlap;
                            itemStatus[j] = TimeCheckStatus.Overlap;
                        }
                    }

                }
                for (int i = 0; i < rowCount; i++)
                {
                    switch (itemStatus[i])
                    {
                        case TimeCheckStatus.Overlap:
                            dataGridSubtitles.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            dataGridSubtitles.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                            overlap = true;
                            break;
                        case TimeCheckStatus.Ok:
                            dataGridSubtitles.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                            dataGridSubtitles.Rows[i].Cells[1].Style.ForeColor = Color.Black;
                            break;
                        case TimeCheckStatus.Error:
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
                _selectCells.SelectCell(cell.ColumnIndex, cell.RowIndex);
            }
        }

        private void tsbtnUnmarkAll_Click(object sender, EventArgs e)
        {
            _selectCells.DeselectAll();
        }

        private void tsbtnTimeOffset_Click(object sender, EventArgs e)
        {
            if (!_subLoaded) return;
            var toDlg = new TimeOffsetDialog();
            if (toDlg.ShowDialog() == DialogResult.OK)
            {
                _selectCells.TimeOffset(toDlg.TimeOffset, _undoRec);
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
            if (Config == null) return;
            if ((int) e.KeyCode < 0 || (int) e.KeyCode >= 256) return;

            int rowIndex = (dataGridSubtitles.CurrentRow == null) ? -1 : dataGridSubtitles.CurrentRow.Index;

            #region Timeing Keys
            if (e.KeyCode == Config.AddTimePoint && !_mKeyhold[(int)e.KeyCode])
            {
                //单键插入时间

                if (rowIndex >= 0 && rowIndex <= lastrowindex)
                {
                    var timeEditArgs = new TimeEditEventArgs(TimeType.BeginTime, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + Config.StartOffset;
                    if (!timeEditArgs.CancelEvent)
                    {
                        if (rowIndex > 0 && Config.AutoOverlapCorrection)
                        {
                            V4Event lastitem = ((V4Event)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                            if (lastitem.End.Value - time > 0 &&
                                lastitem.End.Value - time < Math.Max(Math.Abs(Config.StartOffset), Math.Abs(Config.EndOffset)))
                            {
                                EditEndTime(rowIndex - 1, time - 0.01);
                            }
                        }
                        EditStartTime(rowIndex, time);
                        dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex].Cells[1];
                    }
                }
            }
            else if (e.KeyCode == Config.AddStartTime && !_mKeyhold[(int)e.KeyCode])
            {
                //插入开始时间
                if (rowIndex >= 0 && rowIndex <= lastrowindex)
                {
                    TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.BeginTime, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + Config.StartOffset;
                    if (!timeEditArgs.CancelEvent)
                    {
                        if (rowIndex > 0 && Config.AutoOverlapCorrection)
                        {
                            V4Event lastitem = ((V4Event)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                            if (lastitem.End.Value - time > 0 &&
                                lastitem.End.Value - time < Math.Max(Math.Abs(Config.StartOffset), Math.Abs(Config.EndOffset)))
                            {
                                EditEndTime(rowIndex - 1, time - 0.01);
                            }
                        }
                        EditStartTime(rowIndex, time);
                        dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex].Cells[1];
                    }
                }
            }
            else if (e.KeyCode == Config.AddEndTime && !_mKeyhold[(int)e.KeyCode])
            {
                //插入结束时间
                if (rowIndex >= 0 && rowIndex <= lastrowindex)
                {
                    TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.EndTime, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + Config.EndOffset;
                    if (!timeEditArgs.CancelEvent)
                    {
                        EditEndTime(rowIndex, time);
                        if (rowIndex < lastrowindex)
                            dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex + 1].Cells[0];
                        if (rowIndex - dataGridSubtitles.FirstDisplayedScrollingRowIndex > Config.SelectRowOffset)
                            dataGridSubtitles.FirstDisplayedScrollingRowIndex = rowIndex - Config.SelectRowOffset;
                    }
                }
            }
            else if (e.KeyCode == Config.AddContTimePoint && !_mKeyhold[(int)e.KeyCode])
            {
                //连续插入时间
                if (rowIndex >= 0 && rowIndex <= lastrowindex)
                {
                    TimeEditEventArgs timeEditArgs = new TimeEditEventArgs(TimeType.Unknown, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + Config.StartOffset;
                    if (!timeEditArgs.CancelEvent)
                    {
                        EditEndTime(rowIndex, time - 0.01);
                        if (rowIndex < lastrowindex)
                        {
                            EditStartTime(rowIndex + 1, time);
                            dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex + 1].Cells[1];
                        }
                        if (rowIndex - dataGridSubtitles.FirstDisplayedScrollingRowIndex > Config.SelectRowOffset)
                            dataGridSubtitles.FirstDisplayedScrollingRowIndex = rowIndex - Config.SelectRowOffset;
                    }
                }
            }
            else if (e.KeyCode == Config.AddCellTime && !_mKeyhold[(int)e.KeyCode])
            {
                //插入单元格时间
                if (rowIndex >= 0 && rowIndex <= lastrowindex)
                {

                    var timeEditArgs = new TimeEditEventArgs(TimeType.Unknown, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    if (!timeEditArgs.CancelEvent)
                    {
                        double time = timeEditArgs.TimeValue + Config.StartOffset;
                        int colIndex = dataGridSubtitles.CurrentCell.ColumnIndex;
                        if (colIndex == 0) //插入开始时间
                        {
                            if (rowIndex > 0 && Config.AutoOverlapCorrection)
                            {
                                var lastitem = ((V4Event)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                                if (lastitem.End.Value - time > 0 &&
                                    lastitem.End.Value - time < Math.Max(Math.Abs(Config.StartOffset), Math.Abs(Config.EndOffset)))
                                {
                                    EditEndTime(rowIndex - 1, time - 0.01);
                                }
                            }
                            EditStartTime(rowIndex, time);
                            dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex].Cells[1];
                        }
                        else if (colIndex == 1)//插入结束时间
                        {
                            EditEndTime(rowIndex, time);
                            if (rowIndex < lastrowindex)
                                dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex + 1].Cells[0];
                            if (rowIndex - dataGridSubtitles.FirstDisplayedScrollingRowIndex > Config.SelectRowOffset)
                                dataGridSubtitles.FirstDisplayedScrollingRowIndex = rowIndex - Config.SelectRowOffset;
                        }
                    }
                }
            }
            #endregion
            #region Seek Keys
            else if (e.KeyCode == Config.Pause && !_mKeyhold[(int)e.KeyCode])
            {
                var arg = new PlayerControlEventArgs(PlayerCommand.Toggle);
                if (PlayerControl != null) PlayerControl(this, arg);
            }
            else if (e.KeyCode == Config.SeekBackword && !_mKeyhold[(int)e.KeyCode])
            {
                var seekevent = new SeekEventArgs(SeekDir.CurrentPos, -Config.SeekStep);
                if (Seek != null) Seek(this, seekevent);
            }
            else if (e.KeyCode == Config.SeekForward && !_mKeyhold[(int)e.KeyCode])
            {
                var seekevent = new SeekEventArgs(SeekDir.CurrentPos, Config.SeekStep);
                if (Seek != null) Seek(this, seekevent);
            }
            else if (e.KeyCode == Config.GotoCurrent && !_mKeyhold[(int)e.KeyCode])
            {
                V4Event item;
                if (dataGridSubtitles.CurrentRow != null && _subLoaded
                    && (item = (V4Event)(dataGridSubtitles.CurrentRow.DataBoundItem)) != null
                    )
                {
                    double time = item.Start.Value;
                    var seekevent = new SeekEventArgs(SeekDir.Begin, time);
                    if (Seek != null) Seek(this, seekevent);
                }
            }
            else if (e.KeyCode == Config.GotoPrevious && !_mKeyhold[(int)e.KeyCode])
            {
                if (rowIndex > 0 && rowIndex <= lastrowindex)
                {
                    var item = ((V4Event)(dataGridSubtitles.Rows[rowIndex - 1].DataBoundItem));
                    var seekevent = new SeekEventArgs(SeekDir.Begin, item.Start.Value);
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
                    _undoRec.BeginMultiCells(); //开始Undo记录
                    while (line != null)
                    {
                        cells = line.Split(spliter, 3 - cC);
                        for (int i = 0; i < cells.Length; i++)
                        {
                            if (cells[i].Length != 0)
                            {
                                _undoRec.EditMultiCells(cR, cC + i, dataGridSubtitles.Rows[cR].Cells[cC + i].Value.ToString());
                                dataGridSubtitles.Rows[cR].Cells[cC + i].Value = cells[i];
                            }
                        }
                        cR++;
                        if (cR > lastrowindex) break;
                        line = strReader.ReadLine();
                    }
                    _undoRec.EndEditMultiCells();//结束Undo记录
                    subtitleEdited();
                    _subIndex.RefreshIndex();
                }
            }

            else if (e.KeyCode == Config.EnterEditMode)
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
                    _undoRec.BeginMultiCells(); //开始Undo记录
                    foreach (DataGridViewCell cell in dataGridSubtitles.SelectedCells)
                    {
                        if (cell.RowIndex > lastrowindex) continue;
                        _undoRec.EditMultiCells(cell.RowIndex, cell.ColumnIndex, cell.Value.ToString());
                        dataGridSubtitles.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = "";
                    }
                    _undoRec.EndEditMultiCells();
                    subtitleEdited();
                }
            }
            else if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control && !_mKeyhold[(int)e.KeyCode])
            {
                //删除选中的行
                dataGridSubtitles.EndEdit();
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
            else if (e.KeyCode == Config.SaveAss && !_mKeyhold[(int)e.KeyCode])
            {
                if (_subLoaded)
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
            if (Config == null) return;
            if (e.KeyCode == Config.AddTimePoint)
            {
                if (rowIndex > -1 && rowIndex <= lastrowindex)
                {
                    var timeEditArgs = new TimeEditEventArgs(TimeType.EndTime, 0, true);
                    if (TimeEdit != null) TimeEdit(this, timeEditArgs);
                    double time = timeEditArgs.TimeValue + Config.EndOffset;
                    if (!timeEditArgs.CancelEvent)
                    {
                        EditEndTime(rowIndex, time);
                        if (rowIndex < lastrowindex)
                            dataGridSubtitles.CurrentCell = dataGridSubtitles.Rows[rowIndex + 1].Cells[0];
                        if (rowIndex - dataGridSubtitles.FirstDisplayedScrollingRowIndex > Config.SelectRowOffset)
                            dataGridSubtitles.FirstDisplayedScrollingRowIndex = rowIndex - Config.SelectRowOffset;
                    }
                }
            }
            if ((int) e.KeyCode >= 0 && (int) e.KeyCode < 256)
            {
                _mKeyhold[(int) e.KeyCode] = false;
            }
        }

        private void subtitleEdited()
        {
            Edited = true;
            var offset = DateTime.Now.Subtract(Autosave.PreviousSaveTime);
            if (offset.TotalSeconds > Config.AutoSavePeriod)
                if (AutosaveEvent != null) AutosaveEvent(this, new EventArgs());
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
