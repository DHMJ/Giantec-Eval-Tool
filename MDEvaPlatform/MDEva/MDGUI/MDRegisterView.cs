using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MD.MDCommon;
using DMDongle;

namespace GeneralRegConfigPlatform.MDGUI
{
    public partial class MDRegisterView : UserControl
    {
        DataTable dt_reg;
        RegisterMap regMap;
        DMDongle.comm serialPort;
        List<byte> regAddrList = new List<byte> { };
        List<byte> selectedRegAddr = new List<byte>();
        byte[] regData;
        public MDRegisterView(DataTable _dt, RegisterMap _regmap, DMDongle.comm _uart)
        {
            InitializeComponent();
            //mdDVG1.ClearSelection();
            dt_reg = _dt;
            regMap = _regmap;
            serialPort = _uart;
            CollectCurrentRegList(_dt);
            BindingDVG(dt_reg);

            if (_dt.TableName == "Customer")
            {
                this.rightClickMenu.Items.Add("&Read");
                this.rightClickMenu.Items.Add("&Write");
                this.rightClickMenu.Items.Add("&Delete from this tab");
            }
            else
            {
                this.rightClickMenu.Items.Add("&Read");
                this.rightClickMenu.Items.Add("&Write");
                this.rightClickMenu.Items.Add("&Add to customer tab");
            }
            this.rightClickMenu.ItemClicked += new ToolStripItemClickedEventHandler(rightClickMenu_ItemClicked);
        }

        void rightClickMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "Read":
                    btn_SelectedRead_Click(null, null);
                    break;
                case "Write":
                    break;
                case "Add to customer tab":
                    break;
                default:
                    break;
            }
        }

        public MDRegisterView()
        {
            InitializeComponent();
        }

        private void InitDVG()
        {
            mdDVG1.ShowCellToolTips = true;

            mdDVG1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            mdDVG1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // Reg Address
            mdDVG1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            mdDVG1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            mdDVG1.Columns[0].ReadOnly = true;
            // Bitfield
            mdDVG1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            mdDVG1.Columns[1].Width = 60;
            mdDVG1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            mdDVG1.Columns[1].ReadOnly = true;
            // Name
            mdDVG1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            mdDVG1.Columns[2].Width = 300;
            mdDVG1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            mdDVG1.Columns[2].ReadOnly = true;
            // BFValue
            mdDVG1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            mdDVG1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            mdDVG1.Columns[3].ReadOnly = false;
            // RegValue
            mdDVG1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            mdDVG1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            mdDVG1.Columns[4].ReadOnly = true;
        }

        private void UpdateRegMap(byte startRegAddr, byte[] regData, int count)
        {
            for (byte ix = 0; ix < count; ix++)
            {
                regMap[startRegAddr + ix].RegValue = regData[ix];
            }
        }

        private void CollectCurrentRegList(DataTable _dt)
        {
            regAddrList.Clear();
            foreach (DataRow dr in _dt.Rows)
            {
                if (dr[0].ToString() != "")
                {
                    regAddrList.Add(byte.Parse(dr[0].ToString().Replace("0x", ""),
                        System.Globalization.NumberStyles.HexNumber));
                }
            }
            regData = new byte[regAddrList.Count];
        }

        public void JumpToSelected(string selStr)
        {
            int rowCounts = mdDVG1.Height / mdDVG1.Rows[0].Height;
            foreach (DataGridViewRow dvgRow in mdDVG1.Rows)
            {
                //if (dvgRow.Cells[0].Value != null && dvgRow.Cells[0].Value.ToString() != "")
                if (dvgRow.Cells[2].Value.ToString() == selStr)
                {
                    mdDVG1.ClearSelection();
                    dvgRow.Selected = true;
                    mdDVG1.FirstDisplayedScrollingRowIndex = (mdDVG1.Rows.IndexOf(dvgRow) - rowCounts / 2) > 0 ? 
                        (mdDVG1.Rows.IndexOf(dvgRow) - rowCounts / 2) : 0;
                }
            }
        }

        private void btnReadAll_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.readRegBurst(regAddrList[0], regData, (byte)regAddrList.Count);
            }

            // single read way
            //writeRegBurst(byte startregaddr, byte[] data, byte count)
            //readRegBurst(
        }

        private void btnWriteAll_Click(object sender, EventArgs e)
        {
            // single write way
            for (int ix = 0; ix < regAddrList.Count; ix++)
            {
                regData[ix] = regMap[regAddrList[ix]].RegValue;
            }
        }

        private void btn_SelectedRead_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
 
            }
        }

        private void BindingDVG(DataTable dt)
        {
            mdDVG1.AutoGenerateColumns = true;
            mdDVG1.Dock = DockStyle.Fill;
            this.mdDVG1.DataSource = dt;
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
            if (mdDVG1 == null || mdDVG1.Rows.Count == 0)
                return 0;

            while (currentRowIx > 0 && mdDVG1[0, currentRowIx].Value.ToString() == "")
            {
                currentRowIx--;
            }
            return byte.Parse(mdDVG1[0, currentRowIx].Value.ToString().Replace("0x", ""));
        }

        private void mdDVG1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection dvgSelRC = mdDVG1.SelectedRows;
            // (sender as DataGridView).SelectedRows;
            selectedRegAddr.Clear();

            if (dvgSelRC.Count == 0)
                return;

            // Find selected rows which contain regAddr in the first column.
            foreach (DataGridViewRow dvgRow in dvgSelRC)
            {
                //if (dvgRow.Cells[0].Value != null && dvgRow.Cells[0].Value.ToString() != "")
                if (dvgRow.Cells[0].Value.ToString() != "")
                {
                    selectedRegAddr.Add(byte.Parse(dvgRow.Cells[0].Value.ToString().Replace("0x", ""),
                        System.Globalization.NumberStyles.HexNumber));
                }
            }

            // Find regAddr of the selected rows belong to if the first row doesn't have regAddr info
            //if (dvgSelRC[0].Cells[0].Value != null && dvgSelRC[0].Cells[0].Value.ToString() == "") 
            if (dvgSelRC[0].Cells[0].Value.ToString() == "")
            {
                int firstRowIx = mdDVG1.Rows.IndexOf(dvgSelRC[0]);
                while (firstRowIx > 0 && mdDVG1.Rows[firstRowIx].Cells[0].Value.ToString() == "")
                {
                    firstRowIx--;
                }
                selectedRegAddr.Add(byte.Parse(mdDVG1.Rows[firstRowIx].Cells[0].Value.
                    ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber));
            }

            //foreach (byte val in selectedRegAddr)
            //{
            //    Console.WriteLine("0x" + val.ToString("X2"));
            //}
            if (dvgSelRC[0].Cells[0].Value.ToString() != "")
                this.txtDescriptions.Text = regMap[selectedRegAddr[0]].RegName;
            else
                this.txtDescriptions.Text = regMap[selectedRegAddr[0]][dvgSelRC[0].Cells[2].Value.ToString()].BFDesc;
        }

        private void mdDVG1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Just make the diplay value have a 0x prefix, real cell value is the value you enter in
            if ((e.ColumnIndex < 1 || e.ColumnIndex > 2) && (e.Value.ToString() != ""))
            {
                e.Value = "0x" + e.Value.ToString().Replace("0x", "");
            }
            //Console.WriteLine(mdDVG1.Rows[0].Cells[4].Value.ToString());

            // set the black cell of bfValue column as readonly
            if (e.ColumnIndex == 3 && mdDVG1[0, e.RowIndex].Value.ToString() != "")
            {
                mdDVG1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
            }
            // set the regValue cell as editable
            else if (e.ColumnIndex == 4 && mdDVG1[0, e.RowIndex].Value.ToString() != "")
            {
                mdDVG1[e.ColumnIndex, e.RowIndex].ReadOnly = false;
            }
        }

        private void mdDVG1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 3:         //Bit field value changed
                    // todo: write reg[GetRegAddrWithBFColumn(e.RowIndex)]
                    //Console.WriteLine(mdDVG1[0, e.RowIndex].Value.ToString());
                    Console.WriteLine(GetRegAddrWithBFColumn(e.RowIndex).ToString("X2"));
                    break;
                case 4:         //Reg Value changed
                    //Console.WriteLine(mdDVG1[0, e.RowIndex].Value.ToString());
                    // todo: write this reg to hw
                    break;
                default:
                    break;
            }

        }

        private void mdDVG1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void mdDVG1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (this.mdDVG1.CurrentCell.ColumnIndex >= 3)
            {
                e.Control.KeyPress += new KeyPressEventHandler(EditingControl_KeyPress);
            }


        }

        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (mdDVG1.CurrentCell.Value.ToString().Replace("0x", "").Length >= 2)
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
    }
}
