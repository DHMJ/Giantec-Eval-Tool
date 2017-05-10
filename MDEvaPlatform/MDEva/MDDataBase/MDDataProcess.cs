using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Windows.Forms;
using MD.MDCommon;

namespace GeneralRegConfigPlatform.MDDataBase
{
    [Serializable]
    class MDDataSet
    {
        DataSet ds_excel;
        DataSet ds_display = new DataSet();
        RegisterMap regMap;
        bool crazyMode = false;             // for unnormal register map(e.g. one regAddr follow several bytes data, especailly more than 4)
        string regmapName = "regmap$";
        string devAddrName = "DEV_ADDR";
        string devRegMode  = "REG_MODE";
        uint devAddr;

        public MDDataSet(DataSet _ds)
        {
            ds_excel = _ds;
            regMap = CreateRegMap(_ds);

            if (crazyMode)
                CreateBF_DT_Crazy(regMap);
            else
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
            if (dt.Rows[0][0].ToString().ToUpper() == devAddrName)
            {
                this.devAddr = Convert.ToUInt32(dt.Rows[0][1].ToString().TrimStart("7'b".ToCharArray()), 2);
                _regmap.DevAddr = devAddr;
            }

            // judge if this is a crazy mode reg map, if crazy mode, then there is one more column for num. of bytes of 1 register
            if (dt.Rows[0][2].ToString().ToUpper() == devRegMode)
            {
                if (dt.Rows[0][3].ToString().ToUpper() == "CRAZY")
                    crazyMode = true; 
            }
                
            DataRowCollection drc = dt.Rows;
            int rowsCount = dt.Rows.Count;
            string regGroup = "";
            string regName = "";
            string regAddr = "";
            string numOfBytes = "";
            string defaultValue = "";
            object[] items;
            List<object> temp_bf = new List<object> { };
            int valid_count = 0;

            RWProperty rw = RWProperty.RW;
            for (int ix = 2; ix < rowsCount;)
            {
                #region normal register map
                if (!crazyMode)
                {
                    // New reg group generated here
                    if (drc[ix].ItemArray[(int)itemIx.regGroup].ToString() != "")
                    {
                        regGroup = drc[ix].ItemArray[(int)itemIx.regGroup].ToString().Replace("\n", "");
                        _regmap.AddGroup(regGroup);
                    }

                    // Get all valid bit field name and store in a long array
                    items = drc[ix++].ItemArray;
                    valid_count = 0;
                    temp_bf.Clear();
                    for (int bit_ix = (int)itemIx.bit7; bit_ix <= (int)itemIx.bit0; bit_ix++)
                    {
                        if (items[bit_ix].ToString() != "")
                        {
                            valid_count++;
                            temp_bf.Add(items[bit_ix]);
                        }
                    }

                    if (valid_count == 0)
                        continue;

                    // copy the true valid bit field name to new bitfield name array
                    object[] bfs = new object[valid_count];
                    for (int bf_ix = 0; bf_ix < valid_count; bf_ix++)
                    {
                        bfs[bf_ix] = temp_bf[bf_ix];
                    }

                    if (items[(int)itemIx.rw].ToString() == "R")
                        rw = RWProperty.R;
                    else if (items[3].ToString() == "W")
                        rw = RWProperty.W;
                    else
                        rw = RWProperty.RW;

                    _regmap.Add(new Register(regGroup, items[(int)itemIx.regName].ToString(), items[(int)itemIx.regAddr].ToString(), rw, items[(int)itemIx.defaultRegValue].ToString(), bfs));
                }
                #endregion 

                #region  crazy register map mode
                else
                {
                    // New reg group generated here
                    if (drc[ix].ItemArray[(int)itemIx_Crazy.regGroup].ToString() != "")
                    {
                        regGroup = drc[ix].ItemArray[(int)itemIx_Crazy.regGroup].ToString().Replace("\n", "");                        
                        _regmap.AddGroup(regGroup);
                    }

                    // New Reg start from here, get regName, regAddr and number of bytes 
                    if (drc[ix].ItemArray[(int)itemIx_Crazy.regName].ToString() != "")
                    {
                        regName = drc[ix].ItemArray[(int)itemIx_Crazy.regName].ToString();
                        regAddr = drc[ix].ItemArray[(int)itemIx_Crazy.regAddr].ToString();
                        numOfBytes = drc[ix].ItemArray[(int)itemIx_Crazy.byteCount].ToString();
                        defaultValue = drc[ix].ItemArray[(int)itemIx_Crazy.defaultRegValue].ToString();
                    }

                    if (regName.ToUpper() == "RESERVED")
                    {
                        ix++;
                        continue;
                    }

                    // Get all valid bit field name and store in a long array
                    temp_bf.Clear();
                    valid_count = 0;
                    do
                    {
                        items = drc[ix].ItemArray;
                        for (int bit_ix = (int)itemIx_Crazy.bit7; bit_ix <= (int)itemIx_Crazy.bit0; bit_ix++)
                        {
                            if (items[bit_ix].ToString() != "" && items[bit_ix].ToString().ToUpper() != "RESERVED")
                            {
                                valid_count++;
                                temp_bf.Add(items[bit_ix]);
                            }
                        }
                    } while ((++ix < rowsCount) && (drc[ix].ItemArray[(int)itemIx_Crazy.regName].ToString() == ""));

                    // copy the true valid bit field name to new bitfield name array
                    object[] bfs = new object[valid_count];
                    for (int bf_ix = 0; bf_ix < valid_count; bf_ix++)
                    {
                        bfs[bf_ix] = temp_bf[bf_ix];
                    }

                    if (items[(int)itemIx_Crazy.rw].ToString() == "R")
                        rw = RWProperty.R;
                    else if (items[3].ToString() == "W")
                        rw = RWProperty.W;
                    else
                        rw = RWProperty.RW;

                    _regmap.Add(new Register(regGroup, regName, regAddr, rw, numOfBytes, defaultValue, bfs));
                }
                #endregion
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

                ds_display.Tables.Add(new DataTable(_regMap.GetGroupName(ix))); //Create new data table for GUI display
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
                    if (tempRowItems[(int)itemIx_BF.regName].ToString() != "")
                    {
                        tempReg = _regMap[tempRowItems[(int)itemIx_BF.regName].ToString()];
                        // Add row for register: RegAddress, "", RegName,"", RegValue
                        tempDT.Rows.Add(new object[] { tempReg.RegAddress.ToString("X2"), "", tempReg.RegName, "", tempReg.RegValue.ToString("X2") });
                    }

                    if (tempReg == null)
                        continue;

                    if (tempRowItems[(int)itemIx_BF.bfName].ToString() == "")
                        continue;

                    tempBF = tempReg[tempRowItems[(int)itemIx_BF.bfName].ToString()];
                    // Initialize bit field
                    tempBF.InitiBF(tempRowItems[(int)itemIx_BF.bit].ToString(), tempReg.ByteCount, tempRowItems[(int)itemIx_BF.bfName].ToString(),
                        tempRowItems[(int)itemIx_BF.Description].ToString().Replace("\n", "\r\n"), tempReg.RegValue.ToString("X2"));

                    // Add row for bitfield: "", BIT, Name, BFValue, ""
                    tempDT.Rows.Add(new object[] { "", tempRowItems[(int)itemIx_BF.bit].ToString(), tempRowItems[(int)itemIx_BF.bfName].ToString(), tempBF.BFValue.ToString("X2"), "" });
                }                
            }

        }

        private void CreateBF_DT_Crazy(RegisterMap _regMap)
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

                ds_display.Tables.Add(new DataTable(_regMap.GetGroupName(ix))); //Create new data table for GUI display
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
                    if (tempRowItems[(int)itemIx_BF_Crazy.regName].ToString() != "")
                    {
                        tempReg = _regMap[tempRowItems[(int)itemIx_BF_Crazy.regName].ToString()];

                        // Just for regmap debugging
                        if (tempReg == null)
                        {
                            MessageBox.Show("Register " + tempRowItems[(int)itemIx_BF_Crazy.regAddr].ToString()
                                + " name mismatched in regmap and description sheet");
                            continue;
                        }

                        // Add row for register: RegAddress, "", RegName,"", RegValue
                        if (tempReg.DisplayRegValueCell)
                            tempDT.Rows.Add(new object[] { tempReg.RegAddress.ToString("X2"), "", tempReg.RegName, "", "" });   //if use byte array, no reg data displayed on GUI
                        else
                            tempDT.Rows.Add(new object[] { tempReg.RegAddress.ToString("X2"), "", tempReg.RegName, "", tempReg.RegValue.ToString("X2") });
                    }
                    if (tempReg == null)

                        continue;

                    if (tempRowItems[(int)itemIx_BF_Crazy.bfName].ToString() == "" || tempRowItems[(int)itemIx_BF_Crazy.bfName].ToString() == "RESERVED")
                        continue;

                    tempBF = tempReg[tempRowItems[(int)itemIx_BF_Crazy.bfName].ToString()];

                    // just for regmap Debugging
                    if (tempBF == null)
                    {
                        MessageBox.Show("Can't find " + tempRowItems[(int)itemIx_BF_Crazy.bfName].ToString()
                            + " in register 0x" + tempReg.RegAddress.ToString("X2"));
                        continue;
                    }
                    // Initialize bit field
                    tempBF.InitiBF(tempRowItems[(int)itemIx_BF_Crazy.bit].ToString(), tempReg.ByteCount, tempRowItems[(int)itemIx_BF_Crazy.bfName].ToString(),
                        tempRowItems[(int)itemIx_BF_Crazy.Description].ToString().Replace("\n", "\r\n"), tempReg.RegValue.ToString("X2"));

                    // Add row for bitfield: "", BIT, Name, BFValue, ""
                    tempDT.Rows.Add(new object[] { "", tempRowItems[(int)itemIx_BF_Crazy.bit].ToString(), tempRowItems[(int)itemIx_BF_Crazy.bfName].ToString(), tempBF.BFValue.ToString("X2"), "" });
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

    public enum itemIx
    {
        regGroup = 0,
        regName  = 1,
        regAddr  = 2,
        rw       = 3,
        bit7     = 4,
        bit6     = 5,
        bit5     = 6,
        bit4     = 7,
        bit3     = 8,
        bit2     = 9,
        bit1     = 10,
        bit0     = 11,
        defaultRegValue = 12
    }

    public enum itemIx_BF
    {
        regName = 0,
        bit     = 1,
        bfName    = 2,
        Description = 3
    }

    public enum itemIx_BF_Crazy
    {
        regAddr = 0,
        regName = 1,
        bit     = 2,
        bfName    = 3,
        Description = 4
    }

    public enum itemIx_Crazy
    {
        regGroup = 0,
        regName = 1,
        regAddr = 2,
        byteCount = 3,
        rw = 4,
        bit7 = 5,
        bit6 = 6,
        bit5 = 7,
        bit4 = 8,
        bit3 = 9,
        bit2 = 10,
        bit1 = 11,
        bit0 = 12,
        defaultRegValue = 13
    }

    public enum cellIx
    {
        regAddr     = 0,
        bitField    = 1,
        name        = 2,
        bfValue     = 3,
        regValue    = 4
    }

    [Obsolete]
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
