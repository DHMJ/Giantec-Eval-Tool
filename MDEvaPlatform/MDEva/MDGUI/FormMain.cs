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
using GeneralRegConfigPlatform.MDDataBase;
using GeneralRegConfigPlatform.MDGUI;

namespace GeneralRegConfigPlatform.GUI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        DataSet DS_Excel;
        MDDataSet DataSet;
        DMDongle.comm mySerialPort = new DMDongle.comm();
        private void MenuItemFile_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Please select regmap excel file...";
            ofd.Filter = "xlsx(*.xlsx)|*.xlsx|xls(*.xls)|*.xls|All Files(*.*)|*.*";
            ofd.ReadOnlyChecked = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                DS_Excel = ImportExcel(ofd.FileName);

                DataSet = new MDDataSet(DS_Excel);

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

        }

        private void MenuItemFile_Import_Click(object sender, EventArgs e)
        {

        }

        private void MenuItemFile_Export_Click(object sender, EventArgs e)
        {

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

                //string tableName = (string)result[0];
                //string strSql = "select * from [" + tableName + "$]";
                ////获取Excel指定Sheet表中的信息
                //OleDbCommand objCmd = new OleDbCommand(strSql, objConn);
                //OleDbDataAdapter myData = new OleDbDataAdapter(strSql, objConn);
                //myData.Fill(ds, tableName);//填充数据
                //objConn.Close();

                //dtExcel即为excel文件中指定表中存储的信息
                //dtExcel = ds.Tables[tableName];

                ////string test = dtExcel.Rows[0][0].ToString();

                //foreach (DataColumn dc in dtExcel.Columns)
                //{
                //    foreach (DataRow dr in dc.Table.Rows)
                //    {
                //        if (dr.Table.Rows[0][0].ToString() == "Status Reg")
                //        {
                //            Console.WriteLine(dr.Table.Rows[0][0].ToString());
                //            for (int ix_cl = 0; ix_cl < dr.Table.Rows.Count; ix_cl++)
                //            {
                //                Console.WriteLine(dr.Table.Rows[ix_cl][0]);
                //            }
                //        }
                //    }
                //}

                return ds;
            }
            catch { return null; }
        }

        private void CreateTabs(DataSet _ds)
        {
            this.tabCtrlRegView.TabPages.Clear();
            for (int ix_tab = 0; ix_tab < _ds.Tables.Count; ix_tab++)
            {
                this.tabCtrlRegView.TabPages.Add(_ds.Tables[ix_tab].TableName);
                MDRegisterViewTab newTab = new MDRegisterViewTab(_ds.Tables[ix_tab], DataSet.RegMap, mySerialPort);
                this.tabCtrlRegView.TabPages[ix_tab].Controls.Add(newTab);
                newTab.Dock = DockStyle.Fill;
                newTab.BorderStyle = BorderStyle.Fixed3D;                
            }
        }

        private void tabCtrlRegView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
}
