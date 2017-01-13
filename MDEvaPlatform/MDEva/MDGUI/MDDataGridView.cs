using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GeneralRegConfigPlatform.GUI
{    
    /// <summary>
    /// mergeable DataGridView
    /// </summary>
    public class MDDataGridView : DataGridView
    {
        int[] m_mergeColumns;
        List<int[]> m_merges = new List<int[]>();

        /// <summary>
        /// columns enbale merge
        /// </summary>
        /// <param name="columns"></param>
        public void setAutoMerge(int[] columns)
        {
            m_mergeColumns = columns;
        }

        /// <summary>
        /// merge cell to prev cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void mergeCell(int row, int column)
        {
            m_merges.Add(new int[] { row, column });
        }

        /// <summary>
        /// if cell value is the same
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public bool cellNeedMerge(int rowIndex, int colIndex)
        {
            if (m_merges.FirstOrDefault(r => r[0] == rowIndex && r[1] == colIndex) != null)
            {
                return true;
            }

            // begin auto merge
            if (m_mergeColumns == null || !m_mergeColumns.Contains(colIndex))
            {
                return false;
            }
            DataGridViewCell currCell = Rows[rowIndex].Cells[colIndex];
            DataGridViewCell prevCell = Rows[rowIndex - 1].Cells[colIndex];
            if (currCell.Value == prevCell.Value || (currCell.Value != null && prevCell.Value != null && currCell.Value.ToString().Equals(prevCell.Value.ToString())))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);

            if (e.RowIndex == 0)
            {
                return;
            }
            if (cellNeedMerge(e.RowIndex, e.ColumnIndex))
            {
                e.Value = string.Empty;
                e.FormattingApplied = true;
            }
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            if (e.RowIndex < 1|| e.ColumnIndex < 0)
                return;

            if (cellNeedMerge(e.RowIndex, e.ColumnIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = AdvancedCellBorderStyle.Top;
            }
        }
    }
}
