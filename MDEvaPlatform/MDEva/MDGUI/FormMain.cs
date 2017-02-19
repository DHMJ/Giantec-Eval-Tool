﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using System.Runtime.InteropServices;
using GeneralRegConfigPlatform.MDDataBase;
using GeneralRegConfigPlatform.MDGUI;
using MD.MDCommon;
using DMCommunication;
using System.IO.Ports;

namespace GeneralRegConfigPlatform.GUI
{
    public partial class FormMain : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public FormMain()
        {            
            InitializeComponent();
        }

        DataSet DS_Excel;
        MDDataSet DataSet;
        RegisterMap regMap;
        FormDongle myDongleForm;
        List<MDRegisterViewTab> AllTables = new List<MDRegisterViewTab> { };
        DMDongle myUART = new DMDongle();
        private void MenuItemFile_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select regmap excel file...";
            ofd.Filter = "xlsx(*.xlsx)|*.xlsx|xls(*.xls)|*.xls|All Files(*.*)|*.*";
            //ofd.RestoreDirectory = true;
            ofd.ReadOnlyChecked = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DS_Excel = ImportExcel(ofd.FileName);
                if (DS_Excel == null)
                    return;

                DataSet = new MDDataSet(DS_Excel);
                regMap = DataSet.RegMap;
                // Init tabs with created data tables
                CreateTabs(DataSet.DS_Display);

                #region Test
                //Form1 testform = new Form1();
                //testform.BindingDVG(DataSet.DS_Display.Tables[0]);
                //testform.Show();
                #endregion Test

                //dataGridView1.AutoGenerateColumns = true;
                //dataGridView1.Dock = DockStyle.Fill;
                //this.dataGridView1.DataSource = DataSet.DS_Display.Tables[0];

                #region Comment out Reference code
                //dataGridView1.AutoGenerateColumns = true;
                //dataGridView1.Dock = DockStyle.Fill;
                //this.dataGridView1.DataSource = DS_Excel.Tables[1];

                //dataGridView1.Rows[2].Cells[0].AdjustCellBorderStyle
                //object test = this.dataGridView1.DataBindings;
                #endregion 
            }
            else
                return;           
        }

        private void MenuItemFile_Close_Click(object sender, EventArgs e)
        {
            this.tabCtrlRegView.TabPages.Clear();
            this.DataSet = null;
            this.DS_Excel = null;
        }

        private void MenuItemFile_Import_Click(object sender, EventArgs e)
        {
            if (AllTables.Count == 0)
            {
                MessageBox.Show("Please open project first!!!");
                return;
            }

            StringBuilder tempValue = new StringBuilder(255);
            OpenFileDialog importFile = new OpenFileDialog();
            importFile.Title = "Import registe setting and update to GUI...";
            importFile.Filter = "MDCFG(.mdcfg)|*.mdcfg|All File(*.*)|*.*";
            //importFile.RestoreDirectory = true;
            if(importFile.ShowDialog() == DialogResult.OK)
            {
                string filename = importFile.FileName;
                foreach(Register reg in regMap.RegList)
                {
                    GetPrivateProfileString(reg.RegName, "Value", "00", tempValue, 256, filename);
                    reg.RegValue = byte.Parse(tempValue.ToString().Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                }                
            }
            //todo update GUI 
            for (int ix = 0; ix < AllTables.Count; ix++)
            {
                AllTables[ix].UpdateAllGUI_Tab();
            }
        }

        private void MenuItemFile_Export_Click(object sender, EventArgs e)
        {
            if (AllTables.Count == 0)
            {
                MessageBox.Show("Please open project first!!!");
                return;
            }

            SaveFileDialog exportFile = new SaveFileDialog();
            exportFile.Title = "Export all the register setting to local file...";
            exportFile.Filter = "MDCFG(.mdcfg)|*.mdcfg|All File(*.*)|*.*";
            //exportFile.RestoreDirectory = true;
            if (exportFile.ShowDialog() == DialogResult.OK)
            {
                string filename = exportFile.FileName;
                foreach (Register reg in regMap.RegList)
                {
                    WritePrivateProfileString(reg.RegName, "Address", "0x" + reg.RegAddress.ToString("X2"), filename);
                    WritePrivateProfileString(reg.RegName, "Value", "0x" + reg.RegValue.ToString("X2"), filename);
                }
            }
        }

        private void MenuItemFile_Exit_Click(object sender, EventArgs e)
        {

        }

        private void MenuItemFile_ExitWithoutSave_Click(object sender, EventArgs e)
        {

        }

        private DataSet ImportExcel(string excelFileName)
        {
            try
            {
                DataTable dtExcel = new DataTable();
                //数据表
                DataSet ds = new DataSet();
                //获取文件扩展名
                string strExtension = System.IO.Path.GetExtension(excelFileName);
                string strFileName = System.IO.Path.GetFileName(excelFileName);
                //Excel的连接
                OleDbConnection objConn = null;
                switch (strExtension)
                {
                    case ".xls":
                        objConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            excelFileName + ";" + "Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"");
                        break;
                    case ".xlsx":
                        objConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            excelFileName + ";" + "Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1;\"");
                        break;
                    default:
                        objConn = null;
                        break;
                }

                if (objConn == null)
                {
                    return null;
                }
                objConn.Open();

                // Get all sheet name in the excel                
                ArrayList result = new ArrayList { };
                DataTable sheetNames = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "TABLE" });
                foreach (DataRow dr in sheetNames.Rows)
                {
                        result.Add(dr[2]);
                }

                // Get all datatable from each sheet and add to ds.
                ds.Clear();
                string strSql;
                foreach (string tName in result)
                {
                    strSql = "select * from [" + tName + "]";
                    OleDbCommand objCmd = new OleDbCommand(strSql, objConn);
                    OleDbDataAdapter myData = new OleDbDataAdapter(strSql, objConn);
                    myData.Fill(ds, tName);//填充数据
                }
                objConn.Close();

                return ds;
            }
            catch { return null; }
        }

        private void CreateTabs(DataSet _ds)
        {
            this.tabCtrlRegView.TabPages.Clear();
            AllTables.Clear();
            for (int ix_tab = 0; ix_tab < _ds.Tables.Count; ix_tab++)
            {
                this.tabCtrlRegView.TabPages.Add(_ds.Tables[ix_tab].TableName);
                MDRegisterViewTab newTab = new MDRegisterViewTab(_ds.Tables[ix_tab],_ds.Tables["Customer"], DataSet.RegMap, myUART);
                this.tabCtrlRegView.TabPages[ix_tab].Controls.Add(newTab);
                AllTables.Add(newTab);
                newTab.Dock = DockStyle.Fill;
                newTab.BorderStyle = BorderStyle.Fixed3D;                
            }

            //Create script tab
            this.tabCtrlRegView.TabPages.Add("Script");
            FormScript frm_script = new FormScript(myUART);
            this.tabCtrlRegView.TabPages[tabCtrlRegView.TabPages.Count - 1].Controls.Add(frm_script);
            //AllTables.Add(frm_script);
            frm_script.Dock = DockStyle.Fill;
            frm_script.BorderStyle = BorderStyle.Fixed3D;
        }

        private void selectDongleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //myDongleForm = new FormDongle(myUART);
            //myDongleForm.ShowDialog();

            //Add firmware read info here, and update to GUI
            
        }

        private void selectDongleToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            this.selectDongleToolStripMenuItem.DropDownItems.Clear();

            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                //本机没有串口！
                this.selectDongleToolStripMenuItem.DropDownItems.Add("NULL");
                //this.selectDongleToolStripMenuItem.DropDownItems[0].Select();
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    this.selectDongleToolStripMenuItem.DropDownItems.Add(str[i]);
                    this.selectDongleToolStripMenuItem.DropDownItems[i].Click += new EventHandler(DongleItem_Click);
                }

                //this.cbPortName.SelectedIndex = 0;
            }
        }




        public void DongleItem_Click(object sender, EventArgs e)
        {
            (sender as ToolStripDropDownItem).Select();
            if (myUART.dongleInit((sender as ToolStripDropDownItem).Text, DMDongle.VCPGROUP.SC, 0x65, 10))
            {
                statusBar_DeviceConnected.Text = "Dongle Connected";
                statusBar_DeviceConnected.BackColor = Color.Green;
                //MessageBox.Show("Connected");
            }
            else
            {
                statusBar_DeviceConnected.Text = "Dongle Disconnected";
                statusBar_DeviceConnected.BackColor = Color.Red;
                //MessageBox.Show("Connected Failed");
            }
        }

        private void scriptWriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //todo which is very smilar with the import functions.
        }

        private void rbt_Valid_on_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_Valid_on.Checked)
            {
                //GPIO on interface
                if (myUART.IsOpen)
                {
                    myUART.setUserIO(DMDongle.USERIOGROUP.GROUP_A, DMDongle.USERIOPIN.USR_IO_0);
                }
            }
            else
            {
                //GPIO on interface
                if (myUART.IsOpen)
                {
                    myUART.resetUserIO(DMDongle.USERIOGROUP.GROUP_A, DMDongle.USERIOPIN.USR_IO_0);
                }
            }
        }

        private void rbt_RSTB_On_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_RSTB_On.Checked)
            {
                //GPIO on interface
                if (myUART.IsOpen)
                {
                    myUART.setUserIO(DMDongle.USERIOGROUP.GROUP_A, DMDongle.USERIOPIN.USR_IO_1);
                }
            }
            else
            {
                //GPIO on interface
                if (myUART.IsOpen)
                {
                    myUART.resetUserIO(DMDongle.USERIOGROUP.GROUP_A, DMDongle.USERIOPIN.USR_IO_1);
                }
            }
        }

        private void btn_Reg00_Read_Click(object sender, EventArgs e)
        {
            byte value;
            byte address = 0x00;
            address = Convert.ToByte(textBox_Reg00_Addr.Text, 16);
            myUART.readRegSingle(address, out value);

            textBox_Reg00_Value.Text = value.ToString("X2");
        }

        private void btn_Reg00_Write_Click(object sender, EventArgs e)
        {
            byte value = 0x00;
            byte address = 0x00;
            address = Convert.ToByte(textBox_Reg00_Addr.Text, 16);
            value = Convert.ToByte(textBox_Reg00_Value.Text, 16);
            if(!myUART.writeRegSingle(address,value))
                MessageBox.Show("Write Register Failed!","Waning");
        }

        private void btn_Reg01_Read_Click(object sender, EventArgs e)
        {
            byte value;
            byte address = 0x00;
            address = Convert.ToByte(textBox_Reg01_Addr.Text, 16);
            myUART.readRegSingle(address, out value);

            textBox_Reg01_Value.Text = value.ToString("X2");
        }

        private void btn_Reg01_Write_Click(object sender, EventArgs e)
        {
            byte value = 0x00;
            byte address = 0x00;
            address = Convert.ToByte(textBox_Reg01_Addr.Text, 16);
            value = Convert.ToByte(textBox_Reg01_Value.Text, 16);
            if (!myUART.writeRegSingle(address, value))
                MessageBox.Show("Write Register Failed!", "Waning");
        }

        private void btn_Reg02_Read_Click(object sender, EventArgs e)
        {
            byte value;
            byte address = 0x00;
            address = Convert.ToByte(textBox_Reg02_Addr.Text, 16);
            myUART.readRegSingle(address, out value);

            textBox_Reg02_Value.Text = value.ToString("X2");
        }

        private void btn_Reg02_Write_Click(object sender, EventArgs e)
        {
            byte value = 0x00;
            byte address = 0x00;
            address = Convert.ToByte(textBox_Reg02_Addr.Text, 16);
            value = Convert.ToByte(textBox_Reg02_Value.Text, 16);
            if (!myUART.writeRegSingle(address, value))
                MessageBox.Show("Write Register Failed!", "Waning");
        }

        private void btn_Reg03_Read_Click(object sender, EventArgs e)
        {
            byte value;
            byte address = 0x00;
            address = Convert.ToByte(textBox_Reg03_Addr.Text, 16);
            myUART.readRegSingle(address, out value);

            textBox_Reg03_Value.Text = value.ToString("X2");
        }

        private void btn_Reg03_Write_Click(object sender, EventArgs e)
        {
            byte value = 0x00;
            byte address = 0x00;
            address = Convert.ToByte(textBox_Reg03_Addr.Text, 16);
            value = Convert.ToByte(textBox_Reg03_Value.Text, 16);
            if (!myUART.writeRegSingle(address, value))
                MessageBox.Show("Write Register Failed!", "Waning");
        }

        private void btn_Reg04_Read_Click(object sender, EventArgs e)
        {
            byte value;
            byte address = 0x00;
            address = Convert.ToByte(textBox_Reg04_Addr.Text, 16);
            myUART.readRegSingle(address, out value);

            textBox_Reg04_Value.Text = value.ToString("X2");
        }

        private void btn_Reg04_Write_Click(object sender, EventArgs e)
        {
            byte value = 0x00;
            byte address = 0x00;
            address = Convert.ToByte(textBox_Reg04_Addr.Text, 16);
            value = Convert.ToByte(textBox_Reg04_Value.Text, 16);
            if (!myUART.writeRegSingle(address, value))
                MessageBox.Show("Write Register Failed!", "Waning");
        }

        private void btn_UpdateGPIO_Click(object sender, EventArgs e)
        {
            //To Do: add GPIO status read back functions, and update to GUI
        }

        
    }
}
