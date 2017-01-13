//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Drawing;

//namespace GeneralRegConfigPlatform.MDCommon
//{
//    class DGVMerge
//    {

//        #region"合并单元格(多行多列)"

//        //需要(行、列)合并的所有列标题名
//        List<String> colsHeaderText_V = new List<String>();
//        List<String> colsHeaderText_H = new List<String>();

//        private void InitFormatColumns()
//        {
//            colsHeaderText_V.Add("PHONE1");
//            colsHeaderText_V.Add("PHONE2");

//            colsHeaderText_H.Add("IMAGEINDEX");
//            colsHeaderText_H.Add("PARENTID");
//            colsHeaderText_H.Add("DEPARTMENT");
//            colsHeaderText_H.Add("LOCATION");
//        }


//        //绘制单元格
//        private void dataGridView1_CellPainting(object sender, System.Windows.Forms.DataGridViewCellPaintingEventArgs e)
//        {
//            foreach (string fieldHeaderText in colsHeaderText_H)
//            {
//                //纵向合并
//                if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].HeaderText == fieldHeaderText && e.RowIndex >= 0)
//                {
//                    using (
//                        Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor),
//                        backColorBrush = new SolidBrush(e.CellStyle.BackColor))
//                    {
//                        using (Pen gridLinePen = new Pen(gridBrush))
//                        {
//                            // 擦除原单元格背景
//                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

//                            /****** 绘制单元格相互间隔的区分线条，datagridview自己会处理左侧和上边缘的线条，因此只需绘制下边框和和右边框
//                             DataGridView控件绘制单元格时，不绘制左边框和上边框，共用左单元格的右边框，上一单元格的下边框*****/

//                            //不是最后一行且单元格的值不为null
//                            if (e.RowIndex < this.dataGridView1.RowCount - 1 && this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value != null)
//                            {
//                                //若与下一单元格值不同
//                                if (e.Value.ToString() != this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString())
//                                {
//                                    //下边缘的线
//                                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
//                                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
//                                    //绘制值
//                                    if (e.Value != null)
//                                    {
//                                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
//                                            Brushes.Crimson, e.CellBounds.X + 2,
//                                            e.CellBounds.Y + 2, StringFormat.GenericDefault);
//                                    }
//                                }
//                                //若与下一单元格值相同 
//                                else
//                                {
//                                    //背景颜色
//                                    //e.CellStyle.BackColor = Color.LightPink;   //仅在CellFormatting方法中可用
//                                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
//                                    this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Style.BackColor = Color.LightBlue;
//                                    //只读（以免双击单元格时显示值）
//                                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
//                                    this.dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].ReadOnly = true;
//                                }
//                            }
//                            //最后一行或单元格的值为null
//                            else
//                            {
//                                //下边缘的线
//                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
//                                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);

//                                //绘制值
//                                if (e.Value != null)
//                                {
//                                    e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
//                                        Brushes.Crimson, e.CellBounds.X + 2,
//                                        e.CellBounds.Y + 2, StringFormat.GenericDefault);
//                                }
//                            }

//                            ////左侧的线（）
//                            //e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
//                            //    e.CellBounds.Top, e.CellBounds.Left,
//                            //    e.CellBounds.Bottom - 1);

//                            //右侧的线
//                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
//                                e.CellBounds.Top, e.CellBounds.Right - 1,
//                                e.CellBounds.Bottom - 1);

//                            //设置处理事件完成（关键点），只有设置为ture,才能显示出想要的结果。
//                            e.Handled = true;
//                        }
//                    }
//                }
//            }

//            foreach (string fieldHeaderText in colsHeaderText_V)
//            {
//                //横向合并
//                if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].HeaderText == fieldHeaderText && e.RowIndex >= 0)
//                {
//                    using (
//                        Brush gridBrush = new SolidBrush(this.dataGridView1.GridColor),
//                        backColorBrush = new SolidBrush(e.CellStyle.BackColor))
//                    {
//                        using (Pen gridLinePen = new Pen(gridBrush))
//                        {
//                            // 擦除原单元格背景
//                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

//                            /****** 绘制单元格相互间隔的区分线条，datagridview自己会处理左侧和上边缘的线条，因此只需绘制下边框和和右边框
//                             DataGridView控件绘制单元格时，不绘制左边框和上边框，共用左单元格的右边框，上一单元格的下边框*****/

//                            //不是最后一列且单元格的值不为null
//                            if (e.ColumnIndex < this.dataGridView1.ColumnCount - 1 && this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value != null)
//                            {
//                                if (e.Value.ToString() != this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString())
//                                {
//                                    //右侧的线
//                                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top,
//                                        e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
//                                    //绘制值
//                                    if (e.Value != null)
//                                    {
//                                        e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
//                                            Brushes.Crimson, e.CellBounds.X + 2,
//                                            e.CellBounds.Y + 2, StringFormat.GenericDefault);
//                                    }
//                                }
//                                //若与下一单元格值相同 
//                                else
//                                {
//                                    //背景颜色
//                                    //e.CellStyle.BackColor = Color.LightPink;   //仅在CellFormatting方法中可用
//                                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightPink;
//                                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Style.BackColor = Color.LightPink;
//                                    //只读（以免双击单元格时显示值）
//                                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
//                                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].ReadOnly = true;
//                                }
//                            }
//                            else
//                            {
//                                //右侧的线
//                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top,
//                                    e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);

//                                //绘制值
//                                if (e.Value != null)
//                                {
//                                    e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font,
//                                        Brushes.Crimson, e.CellBounds.X + 2,
//                                        e.CellBounds.Y + 2, StringFormat.GenericDefault);
//                                }
//                            }
//                            //下边缘的线
//                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1,
//                                                        e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
//                            e.Handled = true;
//                        }
//                    }

//                }
//            }

//        }
//        #endregion
//    }
//}
