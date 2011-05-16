using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using SGSDatatype;

namespace SGSControls
{
    class SelectCells
    {
        private List<CellPos> m_SelectedCells = new List<CellPos>();

        public DataGridViewRowCollection Rows
        {
            set;
            private get;
        }

        public SelectCells()
        {
            Rows = null;
        }

        /// <summary>
        /// 标记指定的单元格
        /// </summary>
        /// <param name="Col">列</param>
        /// <param name="Row">行</param>
        public void SelectCell(int Col, int Row)
        {
            CellPos cell = new CellPos(Col, Row);
            if (!m_SelectedCells.Contains(cell)) m_SelectedCells.Add(cell);
            if (Rows != null)
                if (Row < Rows.Count && Col < 3) Rows[Row].Cells[Col].Style.BackColor = Color.SkyBlue;

        }

        /// <summary>
        /// 取消标记指定的单元格
        /// </summary>
        /// <param name="Col">列</param>
        /// <param name="Row">行</param>
        public void Deselect(int Col, int Row)
        {
            if (Rows != null)
                if (Row < Rows.Count && Col < 3)
                {
                    Rows[Row].Cells[Col].Style.BackColor = Color.White;
                    CellPos cell = new CellPos(Col, Row);
                    if (m_SelectedCells.Contains(cell)) m_SelectedCells.Remove(cell);
                }
        }

        /// <summary>
        /// 取消所有标记
        /// </summary>
        public void DeselectAll()
        {
            foreach (CellPos cell in m_SelectedCells)
            {
                if (Rows != null)
                {
                    if (cell.Row < Rows.Count && cell.Col < 3)
                        Rows[cell.Row].Cells[cell.Col].Style.BackColor = Color.White;
                }
            }
            m_SelectedCells = new List<CellPos>();

        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            m_SelectedCells = new List<CellPos>();
            if (Rows.Count > 0)
            {
                for (int i = 0; i < Rows.Count; i++)
                {
                    Rows[i].Cells[0].Style.BackColor = Color.White;
                    Rows[i].Cells[1].Style.BackColor = Color.White;
                    Rows[i].Cells[2].Style.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// 时间平移
        /// </summary>
        /// <param name="timeOffset">增量</param>
        /// <param name="undo">记录Undo</param>
        public void TimeOffset(double timeOffset,UndoRecord undo)
        {
            bool edited = false;
            if (Rows != null)
            {
                if (undo != null) undo.BeginMultiCells();
                foreach (CellPos cell in m_SelectedCells)
                {
                    if (cell.Row < Rows.Count)
                    {
                        AssItem item = (AssItem)(Rows[cell.Row].DataBoundItem);
                        if (cell.Col == 0)
                        {
                            if (undo != null) undo.EditMultiCells(cell.Row, cell.Col, item.StartTime);
                            item.Start.TimeValue += timeOffset;
                            edited = true;
                        }
                        if (cell.Col == 1)
                        {
                            if (undo != null) undo.EditMultiCells(cell.Row, cell.Col, item.EndTime);
                            item.End.TimeValue += timeOffset;
                            edited = true;
                        }
                    }
                }
                if (edited && undo != null) undo.EndEditMultiCells(); 
            }
        }

    }


    class CellPos : IComparable<CellPos>,IEquatable<CellPos>
    {
        public CellPos()
        {
            Col = 0;
            Row = 0;
        }

        public CellPos(int col, int row)
        {
            Col = col;
            Row = row;
        }

       
        public int Col;
        public int Row;

        #region IComparable<CellPos> Members

        int IComparable<CellPos>.CompareTo(CellPos other)
        {
            int val = (Row - other.Row) * 3 + (Col - other.Col);
            return val;
        }

        #endregion

        #region IEquatable<CellPos> Members

        public bool Equals(CellPos other)
        {
            return (Row == other.Row && Col == other.Col);
        }

        #endregion
    }
    
}
