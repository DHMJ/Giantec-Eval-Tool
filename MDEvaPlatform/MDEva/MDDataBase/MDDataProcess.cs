using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using MD.MDCommon;

namespace GeneralRegConfigPlatform.MDDataBase
{
    class MDDataSet
    {
        DataSet ds_excel;
        DataSet ds_display = new DataSet();
        RegisterMap regMap;
        string regmapName = "regmap$";
        string devAddrName = "DEV_ADDR";
        uint devAddr;

        public MDDataSet(DataSet _ds)
        {
            ds_excel = _ds;
            regMap = CreateRegMap(_ds);
            CreateBF_DT(regMap);
            CreateCustomerDT();
            Console.WriteLine(regMap.Count());
        }
        
        /// <summary>
        /// Create regmap from local excel data without bitfield
        /// </summary>
        /// <param name="_dsExcel"></param>
        private RegisterMap CreateRegMap(DataSet _dsExcel)
        {
            RegisterMap _regmap = new RegisterMap();
            ds_display.Clear();
            DataTable dt = _dsExcel.Tables[regmapName];

            // Get Device Address
            if (dt.Rows[0][0].ToString() == devAddrName)
            {
                this.devAddr = Convert.ToUInt32(dt.Rows[0][1].ToString().TrimStart("7'b".ToCharArray()), 2);
                _regmap.DevAddr = devAddr;
            }

            DataRowCollection drc = dt.Rows;
            int rowsCount = dt.Rows.Count;
            //DataTable tempDT = new DataTable() ;
            string regGroup = "";
            RWProperty rw = RWProperty.RW;
            for (int ix = 2; ix < rowsCount; ix++)
            {
                // New reg group generated here
                if (drc[ix].ItemArray[0].ToString() != "")
                {
                    regGroup = drc[ix].ItemArray[0].ToString().Replace("\n","");
                    _regmap.AddGroup(regGroup);
                }

                // Get all valid bit field name and store in a long array
                object[] items = drc[ix].ItemArray;
                int valid_count = 0;
                object[] temp_bf = new object[8];
                for (int bit_ix = 4; bit_ix <= 11; bit_ix++)
                {
                    if( items[bit_ix].ToString() != "")
                    {
                        temp_bf[valid_count++] = items[bit_ix];
                    }
                }
                // copy the true valid bit field name to new bitfield name array
                object[] bfs = new object[valid_count];
                for(int bf_ix = 0; bf_ix < valid_count; bf_ix++)
                {
                    bfs[bf_ix] = temp_bf[bf_ix];
                }

                if (items[3].ToString() == "R")
                    rw = RWProperty.R;
                else if (items[3].ToString() == "W")
                    rw = RWProperty.W;
                else
                    rw = RWProperty.RW;
                
                _regmap.Add(new Register(regGroup, items[1].ToString(), items[2].ToString(), rw, items[12].ToString(), bfs));
            }

            return _regmap;
        }

        private void CreateBF_DT(RegisterMap _regMap)
        {
            DataTableCollection allDT = ds_excel.Tables;
            DataTable tempDT, desc_DT = null;
            for (int ix = 0; ix < _regMap.GroupCount; ix++)
            {
                // Get the sheet with register group name    
                foreach (DataTable dt in allDT)
                {
                    Console.WriteLine(dt.TableName);
                    if (dt.TableName.ToUpper().Contains(_regMap.GetGroupName(ix).ToUpper()))
                    {
                        desc_DT = dt;
                        break;
                    }
                }

                ds_display.Tables.Add(new DataTable(_regMap.GetGroupName(ix)));//Create new data table for GUI display
                tempDT = ds_display.Tables[ds_display.Tables.Count - 1];
                tempDT.Columns.Add("ADDR(Hex)");
                tempDT.Columns.Add("BIT");
                tempDT.Columns.Add("Name");
                tempDT.Columns.Add("BFValue(Hex)");
                tempDT.Columns.Add("RegValue(Hex)");

                DataRowCollection drc = desc_DT.Rows;
                int rowsCount = desc_DT.Rows.Count;
                Register tempReg = null;
                BitField tempBF;
                object[] tempRowItems;
                for (int ix_row = 1; ix_row < rowsCount; ix_row++)
                {
                    tempRowItems = drc[ix_row].ItemArray;
                    // New reg start from here
                    if (tempRowItems[0].ToString() != "" && tempRowItems[0].ToString().Contains("Reg"))
                    {
                        tempReg = _regMap[tempRowItems[0].ToString()];
                        // Add row for register: RegAddress, "", RegName,"", RegValue
                        tempDT.Rows.Add(new object[] { tempReg.RegAddress.ToString("X2"), "", tempReg.RegName, "", tempReg.RegValue.ToString("X2") });
                    }

                    if (tempReg == null)
                        continue;

                    if (tempRowItems[2].ToString() == "")
                        continue;

                    tempBF = tempReg[tempRowItems[2].ToString()];
                    // Initialize bit field
                    tempBF.InitiBF(tempRowItems[1].ToString(),tempRowItems[2].ToString(), 
                        tempRowItems[3].ToString().Replace("\n","\r\n"), tempReg.RegValue.ToString("X2"));

                    // Add row for bitfield: "", BIT, Name, BFValue, ""
                    tempDT.Rows.Add(new object[] { "",tempRowItems[1].ToString(), tempRowItems[2].ToString(), tempBF.BFValue, ""});
                }                
            }

        }

        private void CreateCustomerDT()
        {
            if (!ds_display.Tables.Contains("Customer"))
            {
                DataTable tempDT = new DataTable("Customer");
                tempDT.Columns.Add("ADDR(Hex)");
                tempDT.Columns.Add("BIT");
                tempDT.Columns.Add("Name");
                tempDT.Columns.Add("BFValue(Hex)");
                tempDT.Columns.Add("RegValue(Hex)");

                ds_display.Tables.Add(tempDT);
            }
        }
        
        public RegisterMap RegMap
        { 
            get { return regMap; } 
        }

        public DataSet DS_Display
        {
            get { return ds_display; }
        }
    }

    class ImportRegInfoFromExcel
    {
        public ImportRegInfoFromExcel(string _excelPath)
        {
            //this.strExcelPath = _excelPath;
            GetExcelTableByOleDB(_excelPath, "regmap");
        }

        #region Params
        string strExcelPath = System.IO.Directory.GetCurrentDirectory();
        #endregion Params

        #region Funcs
        public DataTable GetExcelTableByOleDB(string strExcelPath, string tableName)
        {
            try
            {
                DataTable dtExcel = new DataTable();
                //数据表
                DataSet ds = new DataSet();
                //获取文件扩展名
                string strExtension = System.IO.Path.GetExtension(strExcelPath);
                string strFileName = System.IO.Path.GetFileName(strExcelPath);
                //Excel的连接
                OleDbConnection objConn = null;
                switch (strExtension)
                {
                    case ".xls":
                        objConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            strExcelPath + ";" + "Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;\"");
                        break;
                    case ".xlsx":
                        objConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            strExcelPath + ";" + "Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1;\"");
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
                //获取Excel中所有Sheet表的信息
                //System.Data.DataTable schemaTable = objConn.GetOleDbSchemaTabl(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                //获取Excel的第一个Sheet表名
                //string tableName = schemaTable.Rows[0][2].ToString().Trim();
                //string strSql = "select * from [sheet1$]";
                //string strSql = "select * from [" + tableName + "$]";

                ArrayList result = new ArrayList();
                DataTable sheetNames = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "TABLE" });
                foreach (DataRow dr in sheetNames.Rows)
                {
                    result.Add(dr[2]);
                }


                string strSql = "select * from [" + tableName + "$]";
                //获取Excel指定Sheet表中的信息
                OleDbCommand objCmd = new OleDbCommand(strSql, objConn);
                OleDbDataAdapter myData = new OleDbDataAdapter(strSql, objConn);
                myData.Fill(ds, tableName);//填充数据
                objConn.Close();

                //dtExcel即为excel文件中指定表中存储的信息
                dtExcel = ds.Tables[tableName];

                //string test = dtExcel.Rows[0][0].ToString();

                foreach (DataColumn dc in dtExcel.Columns)
                {
                    foreach (DataRow dr in dc.Table.Rows)
                    {
                        if (dr.Table.Rows[0][0].ToString() == "Status Reg")
                        {
                            Console.WriteLine(dr.Table.Rows[0][0].ToString());
                            for (int ix_cl = 0; ix_cl < dr.Table.Rows.Count; ix_cl++)
                            {
                                Console.WriteLine(dr.Table.Rows[ix_cl][0]);
                            }
                        }
                    }
                }

                return dtExcel;
            }
            catch
            {
                return null;
            }
        }


        #endregion Funcs

        #region Events
        #endregion Events
    }
}
