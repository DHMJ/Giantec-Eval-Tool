using System;
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
        }



    }
}
