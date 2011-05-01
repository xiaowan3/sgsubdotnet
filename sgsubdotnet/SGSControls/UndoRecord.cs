using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSControls
{
    class UndoRecord
    {
        List<UndoStep> m_undo = new List<UndoStep>();
        int m_maxSteps = 20;
        /// <summary>
        /// 记录一个单元格的修改
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <param name="text">内容</param>
        public void Edit(int row, int col, string text)
        {
            UndoStep step = new UndoStep();
            step.Cells.Add(new UndoCell(row, col, text));
            AddStep(step);
        }

        UndoStep m_multicells = null;

        /// <summary>
        /// 重置多单元格的操作
        /// </summary>
        public void BeginMultiCells()
        {
            m_multicells = null;
        }

        /// <summary>
        /// 操作多个单元格中的一个
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <param name="text">内容</param>
        public void EditMultiCells(int row, int col, string text)
        {
            if (m_multicells == null) m_multicells = new UndoStep();
            m_multicells.Cells.Add(new UndoCell(row, col, text));
        }

        /// <summary>
        /// 完成多个单元格操作
        /// </summary>
        public void EndEditMultiCells()
        {
            if (m_multicells != null)
            {
                AddStep(m_multicells);
                m_multicells = null;
            }
        }

        /// <summary>
        /// 记录删除行操作
        /// </summary>
        /// <param name="rowindex">行号</param>
        /// <param name="row">被删的行</param>
        public void DeleteRow(int rowindex,DataGridViewRow row)
        {
            UndoStep step = new UndoStep(EditType.Delete);
            step.Cells.Add(new UndoCell(rowindex,0,row.DataBoundItem));
            AddStep(step);
        }

        /// <summary>
        /// 记录插入操作
        /// </summary>
        /// <param name="rowindex">插入的行号</param>
        public void InsertRow(int rowindex)
        {
            UndoStep step = new UndoStep(EditType.Insert);
            step.Cells.Add(new UndoCell(rowindex,0,null));
            AddStep(step);
        }

        /// <summary>
        /// Undo
        /// </summary>
        /// <param name="sub"></param>
        public void Undo(Subtitle.AssSub sub)
        {
            if (m_undo.Count > 0)
            {
                UndoStep undo = m_undo[m_undo.Count - 1];
                switch (undo.Type)
                {
                    case EditType.Edit://撤削编辑操作

                        foreach (UndoCell cell in undo.Cells)
                        {
                            Subtitle.AssItem item = (Subtitle.AssItem)sub.SubItems[cell.Row];
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
                            Subtitle.AssItem item = (Subtitle.AssItem)undo.Cells[0].Content;
                            sub.SubItems.Insert(undo.Cells[0].Row,item);
                        }
                        break;
                    case EditType.Insert://撤削插入操作
                        sub.SubItems.RemoveAt(undo.Cells[0].Row);
                        break;
                }
                m_undo.RemoveAt(m_undo.Count - 1);
            }
        }

        private void AddStep(UndoStep step)
        {
            m_undo.Add(step);
            if (m_undo.Count > m_maxSteps) m_undo.RemoveAt(0);
        }

        /// <summary>
        /// 清空undo记录
        /// </summary>
        public void Reset()
        {
            m_undo = new List<UndoStep>();
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
        public EditType Type;
        public List<UndoCell> Cells;
    }

    /// <summary>
    /// 改变的单元格
    /// </summary>
    class UndoCell
    {
        public UndoCell()
        {
        }
        public UndoCell(int row, int col, object content)
        {
            Col = col;
            Row = row;
            Content = content;
        }
        public int Col = 0;
        public int Row = 0;
        public object Content = null;
    }
}
