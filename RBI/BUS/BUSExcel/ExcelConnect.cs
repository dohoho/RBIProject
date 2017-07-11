using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace RBI.BUS.BUSExcel
{
    class ExcelConnect
    {
        public OleDbConnection connectionExcel(String filePath)
        {
            String connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+filePath+";" +
                          @"Extended Properties='Excel 8.0;HDR=Yes;'";
            OleDbConnection conn = new OleDbConnection(connStr);
            return conn;
        }
    }
}
