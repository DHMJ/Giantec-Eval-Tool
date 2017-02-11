using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeneralRegConfigPlatform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //InitDVG();
        }

        private void InitDVG()
        {
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // Reg Address
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[0].ReadOnly = true;
            // Bitfield
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].ReadOnly = true;
            // Name
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].ReadOnly = true;
            // BFValue
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[3].ReadOnly = false;
            // RegValue
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[4].ReadOnly = true;
        }

        ImportRegInfoFromExcel importRegMap = null;
        private void btn_ImportRegMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select regmap excel file...";
            ofd.Filter = "xlsx(*.xlsx)|*.xlsx|xls(*.xls)|*.xls|All Files(*.*)|*.*";
            ofd.ReadOnlyChecked = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                importRegMap = new ImportRegInfoFromExcel(ofd.FileName);
            }
            else
                return;
        }

        public void BindingDVG(DataTable dt)
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.DataSource = dt;
            dt.RowChanged += new DataRowChangeEventHandler(dt_RowChanged);
            InitDVG();
        }

        void dt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            //Console.WriteLine(e.Row.ItemArray[0]);
            if (e.Row.ItemArray[0].ToString() != "")
            {
                // update reg value with ItemArray[0] regAddr
            }
            else
            {
                // update reg value with BF name and BF value
            }
        }

        public byte GetRegAddrWithBFColumn(int currentRowIx)
        {
            while (currentRowIx > 0 && dataGridView1[0, currentRowIx].Value.ToString() == "")
            {
                currentRowIx--;
            }
            return byte.Parse(dataGridView1[0, currentRowIx].Value.ToString().Replace("0x", ""));
        }

        List<byte> selectedRegAddr = new List<byte>();
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection dvgSelRC = dataGridView1.SelectedRows;
            // (sender as DataGridView).SelectedRows;
            selectedRegAddr.Clear();

            if (dvgSelRC.Count == 0)
                return;

            // Find selected rows which contain regAddr in the first column.
            foreach (DataGridViewRow dvgRow in dvgSelRC)
            {
                if (dvgRow.Cells[0].Value.ToString() != "")
                {
                    selectedRegAddr.Add(byte.Parse(dvgRow.Cells[0].Value.ToString().Replace("0x", ""),
                        System.Globalization.NumberStyles.HexNumber));
                }
            }

            // Find regAddr of the selected rows belong to if the first row doesn't have regAddr info
            if (dvgSelRC[0].Cells[0].Value.ToString() == "")
            {
                int firstRowIx = dataGridView1.Rows.IndexOf(dvgSelRC[0]);
                while (firstRowIx > 0 && dataGridView1.Rows[firstRowIx].Cells[0].Value.ToString() == "")
                {
                    firstRowIx--;
                }
                selectedRegAddr.Add(byte.Parse(dataGridView1.Rows[firstRowIx].Cells[0].Value.
                    ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber));
            }

            //foreach (byte val in selectedRegAddr)
            //{
            //    Console.WriteLine("0x" + val.ToString("X2"));
            //}

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Just make the diplay value have a 0x prefix, real cell value is the value you enter in
            if ((e.ColumnIndex < 1 || e.ColumnIndex > 2) && (e.Value.ToString() != ""))
            {
                e.Value = "0x" + e.Value.ToString().Replace("0x", "");
            }
            //Console.WriteLine(dataGridView1.Rows[0].Cells[4].Value.ToString());

            // set the black cell of bfValue column as readonly
            if (e.ColumnIndex == 3 && dataGridView1[0,e.RowIndex].Value.ToString() != "")
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
            }
            // set the regValue cell as editable
            else if (e.ColumnIndex == 4 && dataGridView1[0, e.RowIndex].Value.ToString() != "")
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].ReadOnly = false;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 3:         //Bit field value changed
                    // todo: write reg[GetRegAddrWithBFColumn(e.RowIndex)]
                    //Console.WriteLine(dataGridView1[0, e.RowIndex].Value.ToString());
                    Console.WriteLine(GetRegAddrWithBFColumn(e.RowIndex).ToString("X2"));
                    break;
                case 4:         //Reg Value changed
                    //Console.WriteLine(dataGridView1[0, e.RowIndex].Value.ToString());
                    // todo: write this reg to hw
                    break;
                default:
                    break;
            }

        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //TextBox txt_Regx = sender as TextBox;
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
            string str = "\r\b0123456789abcdefABCDEF";//This will allow the user to enter numeric HEX values only.

            e.Handled = !(str.Contains(e.KeyChar.ToString()));

            if (e.Handled)
                return;
            else
            {
                //if (e.KeyChar.ToString() == "\r")
                //{
                //    RegTextChangedDisplay(txt_Regx);
                //    txt_Regx.SelectionStart = txt_Regx.Text.Length;
                //    //try
                //    //{
                //    //    //string temp = txt_Regx.Text.TrimStart("0x".ToCharArray()).TrimEnd("H".ToCharArray());
                //    //    //uint _reg_value = UInt32.Parse((temp == "" ? "0" : temp), System.Globalization.NumberStyles.HexNumber);
                //    //    RegTextChangedDisplay(txt_Regx);
                //    //}
                //    //catch
                //    //{
                //    //    txt_Regx.Text = this.
                //    //}
                //}
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (this.dataGridView1.CurrentCell.ColumnIndex >= 3)
            {
                e.Control.KeyPress += new KeyPressEventHandler(EditingControl_KeyPress);
            }


        }
        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (dataGridView1.CurrentCell.Value.ToString().Replace("0x", "").Length >= 2)
            //{
            //    if (e.KeyChar == (char)Keys.Back && e.KeyChar == (char)Keys.Enter)
            //        e.Handled= true;
            //    else
            //        e.Handled= false;
            //}
            //else
            //{
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
                string str = "\r\b0123456789abcdefABCDEF";//This will allow the user to enter numeric HEX values only.

                e.Handled = !(str.Contains(e.KeyChar.ToString()));
            //}                        
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
