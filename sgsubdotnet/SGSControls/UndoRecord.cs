using System.Collections.Generic;
using System.Windows.Forms;
using SGSDatatype;
namespace SGSControls
{
    class UndoRecord
    {
        List<UndoStep> _undo = new List<UndoStep>();
        const int MaxSteps = 30;
        /// <summary>
        /// 记录一个单元格的修改
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <param name="text">内容</param>
        public void Edit(int row, int col, string text)
        {
            var step = new UndoStep();
            step.Cells.Add(new UndoCell(row, col, text));
            AddStep(step);
        }

        UndoStep _multicells;

        /// <summary>
        /// 重置多单元格的操作
        /// </summary>
        public void BeginMultiCells()
        {
            _multicells = null;
        }

        /// <summary>
        /// 操作多个单元格中的一个
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <param name="text">内容</param>
        public void EditMultiCells(int row, int col, string text)
        {
            if (_multicells == null) _multicells = new UndoStep();
            _multicells.Cells.Add(new UndoCell(row, col, text));
        }

        /// <summary>
        /// 完成多个单元格操作
        /// </summary>
        public void EndEditMultiCells()
        {
            if (_multicells != null)
            {
                AddStep(_multicells);
                _multicells = null;
            }
        }

        /// <summary>
        /// 记录删除行操作
        /// </summary>
        /// <param name="rowindex">行号</param>
        /// <param name="row">被删的行</param>
        public void DeleteRow(int rowindex,DataGridViewRow row)
        {
            var step = new UndoStep(EditType.Delete);
            step.Cells.Add(new UndoCell(rowindex,0,row.DataBoundItem));
            AddStep(step);
        }

        /// <summary>
        /// 记录插入操作
        /// </summary>
        /// <param name="rowindex">插入的行号</param>
        public void InsertRow(int rowindex)
        {
            var step = new UndoStep(EditType.Insert);
            step.Cells.Add(new UndoCell(rowindex,0,null));
            AddStep(step);
        }

        /// <summary>
        /// Undo
        /// </summary>
        /// <param name="sub"></param>
        public void Undo(AssSub sub)
        {
            if (_undo.Count > 0)
            {
                UndoStep undo = _undo[_undo.Count - 1];
                switch (undo.Type)
                {
                    case EditType.Edit://撤削编辑操作

                        foreach (UndoCell cell in undo.Cells)
                        {
                            var item = (AssItem)sub.SubItems[cell.Row];
                            switch (cell.Col)
                            {
                                case 0:
                                    item.StartTime = (string)cell.Content;
                                    break;
                                case 1:
                                    item.EndTime = (string)cell.Content;
                                    break;
                                case 2:
                                    item.Text = (string)cell.Content;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case EditType.Delete://撤削删除操作
                        {
                            var item = (AssItem)undo.Cells[0].Content;
                            sub.SubItems.Insert(undo.Cells[0].Row,item);
                        }
                        break;
                    case EditType.Insert://撤削插入操作
                        sub.SubItems.RemoveAt(undo.Cells[0].Row);
                        break;
                }
                _undo.RemoveAt(_undo.Count - 1);
            }
        }

        private void AddStep(UndoStep step)
        {
            _undo.Add(step);
            if (_undo.Count > MaxSteps) _undo.RemoveAt(0);
        }

        /// <summary>
        /// 清空undo记录
        /// </summary>
        public void Reset()
        {
            _undo = new List<UndoStep>();
        }


    }

    /// <summary>
    /// 操作类形
    /// </summary>
    enum EditType { Edit, Insert, Delete }

    /// <summary>
    /// 记录操作的每一步
    /// </summary>
    class UndoStep
    {
        public UndoStep()
        {
            Type = EditType.Edit;
            Cells = new List<UndoCell>();
        }
        public UndoStep(EditType type)
        {
            Type = type;
            Cells = new List<UndoCell>();
        }
        public readonly EditType Type;
        public readonly List<UndoCell> Cells;
    }

    /// <summary>
    /// 改变的单元格
    /// </summary>
    class UndoCell
    {
        public UndoCell(int row, int col, object content)
        {
            Col = col;
            Row = row;
            Content = content;
        }
        public readonly int Col;
        public readonly int Row;
        public readonly object Content;
    }
}
