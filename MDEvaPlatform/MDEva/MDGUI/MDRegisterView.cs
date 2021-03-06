﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using MD.MDCommon;
using DMCommunication;
using GeneralRegConfigPlatform.GUI;
using GeneralRegConfigPlatform.MDDataBase;

namespace GeneralRegConfigPlatform.MDGUI
{
    public partial class MDRegisterView : UserControl
    {
        DataTable dt_reg;
        DataTable dt_Customer;
        RegisterMap regMap;
        DMDongle dongle;
        List<byte> regAddrList = new List<byte> { };
        List<byte> selectedRegAddr = new List<byte>();
        byte[] regData;

        // Define delegate to make row selected change can be mapped to left list select
        public delegate void RowSelectedChangeEventHandler(object sender, EventArgs e);
        public event RowSelectedChangeEventHandler RowSelectedChangeEvent;

        public MDRegisterView()
        {
            InitializeComponent();
        }

        public MDRegisterView(DataTable _dt, DataTable _dtCustomer, RegisterMap _regmap, DMDongle _dongle)
        {
            InitializeComponent();

            dt_reg = _dt;
            dt_Customer = _dtCustomer;
            regMap = _regmap;
            dongle = _dongle;
            CollectCurrentRegList(_dt);
            BindingDVG(dt_reg);

            if (_dt.TableName == "Customer")
            {
                this.rightClickMenu.Items.Add("&Read");
                this.rightClickMenu.Items.Add("&Write");
                this.rightClickMenu.Items.Add("&Delete from this tab");
                this.rightClickMenu.Items.Add("&Sort");
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
            switch (e.ClickedItem.Text)
            {
                case "&Read":
                    btn_SelectedRead_Click(null, null);
                    break;
                case "&Write":
                    btnWriteSel_Click(null, null);
                    //if (dg.IsOpen)
                    //{
                    //    byte[] regAddr = new byte[selectedRegAddr.Count];
                    //    byte[] regData = new byte[selectedRegAddr.Count];
                    //    for (int ix = 0; ix < selectedRegAddr.Count; ix++)
                    //    {
                    //        regAddr[ix] = selectedRegAddr[ix];
                    //    }
                    //    if (dg.readRegBlockSingle(regAddr, regData, regAddr.Length))
                    //    {
                    //        for (int ix = 0; ix < selectedRegAddr.Count; ix++)
                    //        {
                    //            regMap[selectedRegAddr[ix]].RegValue = regData[ix];
                    //        }
                    //    }
                    //}
                    break;
                case "&Add to customer tab":
                    for (int ix_reg = 0; ix_reg < selectedRegAddr.Count; ix_reg++)
                    {
                        Register tempReg = regMap[selectedRegAddr[ix_reg]];
                        // Judge if this register already in the customer tab
                        if (IfTableContainReg(dt_Customer, tempReg.RegAddress))
                            continue;

                        // Add row for register: RegAddress, "", RegName,"", RegValue
                        dt_Customer.Rows.Add(new object[] { tempReg.RegAddress.ToString("X2"), "", tempReg.RegName, "", tempReg.RegValue.ToString("X2") });
                        for (int ix_bf = 0; ix_bf < tempReg.BFCount; ix_bf++)
                        {
                            //Add row for bitfield: "", BIT, Name, BFValue, ""
                            dt_Customer.Rows.Add(new object[] { "", tempReg[ix_bf].BITs, tempReg[ix_bf].BFName, tempReg[ix_bf].BFValue, "" });
                        }
                    }
                    break;
                case "&Delete from this tab":
                    for (int ix_reg = 0; ix_reg < selectedRegAddr.Count; ix_reg++)
                    {
                        regAddrList.Remove(selectedRegAddr[ix_reg]);
                    }

                    ReCreatDataTable();
                    break;
                case "&Sort":
                    ReCreatDataTable();
                    break;
                default:
                    break;
            }
        }

        #region Funcs
        private bool IfTableContainReg(DataTable dt, byte regAddr)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[0].ToString() == "")
                    continue;

                if (byte.Parse(dr[0].ToString(), System.Globalization.NumberStyles.HexNumber) == regAddr)
                    return true;
            }

            return false;
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
            mdDVG1.Columns[1].Width = 120;
            //mdDVG1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //mdDVG1.Columns[1].Width = 60;
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
            //mdDVG1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            mdDVG1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            mdDVG1.Columns[1].Width = 110;
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

        public void CollectCurrentRegList(DataTable _dt)
        {
            byte tempAddr = 0;
            foreach (DataRow dr in _dt.Rows)
            {
                if (dr[0].ToString() != "")
                {
                    tempAddr = byte.Parse(dr[0].ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                    if (!regAddrList.Contains(tempAddr))
                        regAddrList.Add(tempAddr);
                }
            }
            regAddrList.Sort();
            regData = new byte[regAddrList.Count];
        }

        private void BindingDVG(DataTable dt)
        {
            mdDVG1.AutoGenerateColumns = true;
            mdDVG1.Dock = DockStyle.Fill;
            this.mdDVG1.DataSource = dt;
            dt.RowChanged += new DataRowChangeEventHandler(dt_RowChanged);
            InitDVG();
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

        public void UpdateGUIAll()
        {
            for (int ix = 0; ix < regAddrList.Count; ix++)
            {
                UpdateRegValueCell(regMap[regAddrList[ix]]);
            }
        }

        public int GetRegRowIx(int currentRowIx)
        {
            if (mdDVG1 == null || mdDVG1.Rows.Count == 0)
                return 0;

            while (currentRowIx > 0 && mdDVG1[0, currentRowIx].Value.ToString() == "")
            {
                currentRowIx--;
            }
            return currentRowIx;
        }

        public void UpdateSelectedRegList(DataGridViewSelectedRowCollection dgvSelectedRC)
        {
            byte tempAddr;
            string tempBFName;
            foreach (DataGridViewRow dgvRow in dgvSelectedRC)
            {
                if (dgvRow.Cells[(int)cellIx.regAddr].Value.ToString() != "")
                {
                    tempAddr = byte.Parse(dgvRow.Cells[0].Value.ToString(), System.Globalization.NumberStyles.HexNumber);
                    if (!selectedRegAddr.Contains(tempAddr))
                    {
                        selectedRegAddr.Add(tempAddr);
                        continue;
                    }
                }
                else
                {
                    tempBFName = dgvRow.Cells[(int)cellIx.name].Value.ToString();
                    foreach (Register tempReg in regMap.RegList)
                    {
                        if (tempReg.Contain(tempBFName))
                        {
                            if (!selectedRegAddr.Contains(tempReg.RegAddress))
                            {
                                selectedRegAddr.Add(tempReg.RegAddress);
                                break;
                            }
                        }
                    }
                }
            }
            selectedRegAddr.Sort();
        }

        private void ReCreatDataTable()
        {
            dt_Customer.Rows.Clear();
            //dt_RowChanged(null, new DataRowChangeEventArgs(null,DataRowAction.Delete));
            for (int ix_reg = 0; ix_reg < regAddrList.Count; ix_reg++)
            {
                Register tempReg = regMap[regAddrList[ix_reg]];
                // Add row for register: RegAddress, "", RegName,"", RegValue
                dt_Customer.Rows.Add(new object[] { tempReg.RegAddress.ToString("X2"), "", tempReg.RegName, "", tempReg.RegValue.ToString("X2") });
                for (int ix_bf = 0; ix_bf < tempReg.BFCount; ix_bf++)
                {
                    //Add row for bitfield: "", BIT, Name, BFValue, ""
                    dt_Customer.Rows.Add(new object[] { "", tempReg[ix_bf].BITs, tempReg[ix_bf].BFName, tempReg[ix_bf].BFValue, "" });
                }
            }
        }

        private void UpdateBFValueCells(int ix_reg, byte regAddr)
        {
            Register tempReg = regMap[regAddr];
            DataGridViewRow tempDGVRow;
            for (int ix_bf = 0; ix_bf < tempReg.BFCount; ix_bf++)
            {
                tempDGVRow = mdDVG1.Rows[ix_reg + 1 + ix_bf];
                tempDGVRow.Cells[(int)cellIx.bfValue].Value = tempReg.GetBFValue(tempDGVRow.Cells[(int)cellIx.name].Value.ToString()).ToString("X2");
            }
        }

        private void UpdateRegValueCell(Register updateReg)
        {
            foreach (DataGridViewRow dgvRow in mdDVG1.Rows)
            {
                if (dgvRow.Cells[0].Value.ToString() != "")
                {
                    if (byte.Parse(dgvRow.Cells[0].Value.ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber) == updateReg.RegAddress)
                    {
                        //dgvRow.Cells[4].Value = regAddr.ToString("X2");
                        if (updateReg.ByteCount <= 4)       // if byte count <= 4, can update reg value first and then update bf cell in value changed event.
                            dgvRow.Cells[(int)cellIx.regValue].Value = updateReg.RegValue.ToString("X2");
                        else                                // if byte count >4, will disable regVale cell and update bf cell directly
                            UpdateBFValueCells(dgvRow.Index, updateReg.RegAddress);                        
                        
                        return;
                    }
                }
            }
        }
        #endregion Funcs

        #region Events
        private void btnReadAll_Click(object sender, EventArgs e)
        {
            if (dongle.IsOpen)
            {
                Register tempReg;
                for (int ix = 0; ix < regAddrList.Count; ix++)
                {
                    tempReg = regMap[regAddrList[ix]];
                    dongle.readRegBurst(tempReg.RegAddress, tempReg.ByteValue, tempReg.ByteCount);
                    
                    // Update new value to GUI display
                    UpdateRegValueCell(tempReg);
                }
            }
        }

        private void btnWriteAll_Click(object sender, EventArgs e)
        {
            if (dongle.IsOpen)
            {
                Register tempReg;
                for (int ix = 0; ix < regAddrList.Count; ix++)
                {
                    tempReg = regMap[regAddrList[ix]];
                    dongle.writeRegBurst(tempReg.RegAddress, tempReg.ByteValue, tempReg.ByteCount);
                }
            }
        }

        private void btn_SelectedRead_Click(object sender, EventArgs e)
        {
            if (dongle.IsOpen)
            {
                Register tempReg;
                for (int ix = 0; ix < selectedRegAddr.Count; ix++)
                {
                    tempReg = regMap[selectedRegAddr[ix]];
                    dongle.readRegBurst(tempReg.RegAddress, tempReg.ByteValue, tempReg.ByteCount);

                    // Update new value to GUI display
                    UpdateRegValueCell(tempReg);
                }
            }
        }

        private void btnWriteSel_Click(object sender, EventArgs e)
        {
            if (dongle.IsOpen)
            {
                Register tempReg;
                for (int ix = 0; ix < selectedRegAddr.Count; ix++)
                {
                    tempReg = regMap[selectedRegAddr[ix]];
                    if(!dongle.writeRegBurst(tempReg.RegAddress, tempReg.ByteValue, tempReg.ByteCount))
                        MessageBox.Show("Write Register Failed!", "Warning");
                }
            }
        }

        void dt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (dt_reg.TableName == "Customer")
            {
                if (e.Action == DataRowAction.Add)
                {
                    CollectCurrentRegList(dt_reg);
                }
            }

            if (e.Action == DataRowAction.Delete)
            {
                return;
            }

            if (e.Action == DataRowAction.Change)
            {
                if (e.Row.ItemArray[0].ToString() != "")
                {
                    // update reg value with ItemArray[0] regAddr
                }
                else
                {
                    // update reg value with BF name and BF value
                }
            }
        }

        private void mdDVG1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection dgvSelRC = mdDVG1.SelectedRows;
            selectedRegAddr.Clear();

            if (dgvSelRC.Count == 0)
                return;

            UpdateSelectedRegList(dgvSelRC);

            // Fill in Description text with Bitfield or Register descriptions.
            string selectedBFName = "";
            if (dgvSelRC[0].Cells[0].Value.ToString() != "")
                this.txtDescriptions.Text = regMap[selectedRegAddr[0]].RegName;
            else
            {
                // try to do: if selected from bottom will crash !!!!!
                if (regMap[selectedRegAddr[0]].Contain(dgvSelRC[0].Cells[2].Value.ToString()))
                {
                    selectedBFName = regMap[selectedRegAddr[0]][dgvSelRC[0].Cells[2].Value.ToString()].BFName;
                    this.txtDescriptions.Text = regMap[selectedRegAddr[0]][dgvSelRC[0].Cells[2].Value.ToString()].BFDesc;
                }
                else
                {
                    selectedBFName = regMap[selectedRegAddr[selectedRegAddr.Count - 1]][dgvSelRC[0].Cells[2].Value.ToString()].BFName;
                    this.txtDescriptions.Text =
                        regMap[selectedRegAddr[selectedRegAddr.Count - 1]][dgvSelRC[0].Cells[2].Value.ToString()].BFDesc;
                }
            }

            // Make the left search item auto changed to the same BF selected in this dvg
            if (RowSelectedChangeEvent != null)
                RowSelectedChangeEvent(selectedBFName as object, e);
        }

        /// <summary>
        /// Set the cell read only properity and "0x" prefix for hex data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mdDVG1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            /* GUI display sequence: regAddr(0) | bitField(1) | name(2) | bfValue(3) | regValue(4)  */

            // Just make the diplay value have a 0x prefix, real cell value is the value you enter in
            if ((e.ColumnIndex < (int)cellIx.bitField || e.ColumnIndex > (int)cellIx.name) && (e.Value.ToString() != ""))
            {
                e.Value = "0x" + e.Value.ToString().Replace("0x", "");
            }

            //if(e.Value == "")
            //    mdDVG1[e.ColumnIndex, e.RowIndex].ReadOnly = true;

            // set the blank cell of bfValue column as readonly
            if (e.ColumnIndex == (int)cellIx.bfValue && mdDVG1[(int)cellIx.regAddr, e.RowIndex].Value.ToString() != "")
            {
                mdDVG1[e.ColumnIndex, e.RowIndex].ReadOnly = true;
            }
            // set the regValue cell as editable
            else if (e.ColumnIndex == (int)cellIx.regValue && mdDVG1[(int)cellIx.regAddr, e.RowIndex].Value.ToString() != "")
            {
                mdDVG1[e.ColumnIndex, e.RowIndex].ReadOnly = false;
            }
            //else if (e.ColumnIndex == (int)cellIx.regValue && mdDVG1[(int)cellIx.regAddr, e.RowIndex].Value.ToString() != "")
            //{
            //    mdDVG1[e.ColumnIndex, e.RowIndex].ReadOnly = false;
            //}
        }

        /// <summary>
        /// This event just be triggered when bf value or reg value changed. And will update opposite value(e.g. bf value changed -> update reg value)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mdDVG1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            /* GUI display sequence: regAddr(0) | bitField(1) | name(2) | bfValue(3) | regValue(4)  */

            byte tempAddr;
            DataGridViewRow tempRow = mdDVG1.Rows[e.RowIndex];
            Register tempReg;
            switch (e.ColumnIndex)
            {
                case (int)cellIx.bfValue:         //Bit field value changed
                    // Update displayed RegValue and regmap
                    int regRowIx = GetRegRowIx(e.RowIndex);
                    tempAddr = byte.Parse(mdDVG1[(int)cellIx.regAddr, regRowIx].Value.ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                    tempReg = regMap[tempAddr];
                    tempReg.SetBFValue(tempRow.Cells[(int)cellIx.name].Value.ToString(), tempRow.Cells[(int)cellIx.bfValue].Value.ToString().Replace("0x", ""));

                    if (!tempReg.DisplayRegValueCell)  //if byte count of reg <= 4, then update reg Value on GUI
                    {
                        this.mdDVG1.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(this.mdDVG1_CellValueChanged);
                        mdDVG1[4, regRowIx].Value = regMap[tempAddr].RegValue.ToString("X2");
                        this.mdDVG1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.mdDVG1_CellValueChanged);
                    }
                    break;

                case (int)cellIx.regValue:         //Reg Value changed
                    // Update regmap and BF value in GUI display
                    tempAddr = byte.Parse(tempRow.Cells[(int)cellIx.regAddr].Value.ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);

                    this.mdDVG1.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(this.mdDVG1_CellValueChanged);
                    regMap[tempAddr].RegValue = UInt32.Parse(tempRow.Cells[(int)cellIx.regValue].Value.ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                    UpdateBFValueCells(e.RowIndex, tempAddr);
                    this.mdDVG1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.mdDVG1_CellValueChanged);

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

        private void mdDVG1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (mdDVG1.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
            {
                for (int ix = 0; ix < mdDVG1.Rows[e.RowIndex].Cells.Count; ix++)
                {
                    mdDVG1.Rows[e.RowIndex].Cells[ix].Style.BackColor = Color.LightGray;
                    mdDVG1.Rows[e.RowIndex].Cells[ix].Style.Font = new System.Drawing.Font("Calibri", 12F,
                        System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        /// <summary>
        /// This function will decide if the input value reasonable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mdDVG1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            /* GUI display sequence: regAddr(0) | bitField(1) | name(2) | bfValue(3) | regValue(4)  */

            //Console.WriteLine("Enter Cell Validataing");
            uint tempData;
            byte tempAddr;
            DataGridViewRow tempRow = mdDVG1.Rows[e.RowIndex];
            if (e.ColumnIndex < (int)cellIx.bfValue)
                return;

            if (tempRow.Cells[e.ColumnIndex].EditedFormattedValue.ToString() == "")
            {
                //e.Cancel = true;
                return;
            }

            //if (tempRow.Cells[e.ColumnIndex].EditedFormattedValue.ToString().Length > 4)
            //{
            //    e.Cancel = true;
            //    return;
            //}

            switch (e.ColumnIndex)
            {
                case 3:         //Bit field value changing
                    int regRowIx = GetRegRowIx(e.RowIndex);
                    tempAddr = byte.Parse(mdDVG1[(int)cellIx.regAddr, regRowIx].Value.ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                    tempData = UInt32.Parse(mdDVG1[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                    if (tempData > regMap[tempAddr][tempRow.Cells[(int)cellIx.name].Value.ToString()].BFMaxValue)
                        e.Cancel = true;
                    break;
                case 4:         //Reg Value changed
                    tempData = UInt32.Parse(tempRow.Cells[(int)cellIx.regValue].EditedFormattedValue.ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                    tempAddr = byte.Parse(tempRow.Cells[(int)cellIx.regAddr].Value.ToString(), System.Globalization.NumberStyles.HexNumber);
                    if (tempData > regMap[tempAddr].RegMaxValue)
                        e.Cancel = true;
                    break;
                default:
                    break;
            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (dongle.dongleInit(this.cbPortName.Text, DMDongle.VCPGROUP.SC, 0x65, 10))
            {

            }
            else
            {

            }
        }

        private void MDRegisterView_Load(object sender, EventArgs e)
        {
            txtDescriptions.Width = this.Width - this.panel1.Width - 2;
        }

        #endregion Events



    }
        
}