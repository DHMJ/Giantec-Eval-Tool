using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Collections;

namespace GeneralRegConfigPlatform
{
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
