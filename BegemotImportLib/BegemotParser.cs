using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;

namespace BegemotImportLib
{
    public class BegemotParser
    {
        public  List<BegemotPriceRow> ParsePrice(string path)
        {
            DataTable priceTable = GetExcelTable(path);

            var begemotPriceRows = new List<BegemotPriceRow>();
            for (int i = 4; i < priceTable.Rows.Count; i++)
            {
                var dataRow = priceTable.Rows[i];
                var begemotRow = new BegemotPriceRow(dataRow);
                begemotPriceRows.Add(begemotRow);
            }

            return begemotPriceRows;
        }

        public List<BegemotSaleRow> ParseSale(string path)
        {
            DataTable priceTable = GetExcelTable(path);

            var saleRows = new List<BegemotSaleRow>();
            for (int i = 0; i < priceTable.Rows.Count; i++)
            {
                var dataRow = priceTable.Rows[i];
                var begemotRow = new BegemotSaleRow(dataRow);
                saleRows.Add(begemotRow);
            }

            return saleRows;
        }
        
        private DataTable GetExcelTable(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            ////1. Reading from a binary Excel file ('97-2003 format; *.xls)
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            //...
            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            //...
            //3. DataSet - The result of each spreadsheet will be created in the result.Tables
            DataSet dataSet = excelReader.AsDataSet();
            //...
            ////4. DataSet - Create column names from first row
            //excelReader.IsFirstRowAsColumnNames = true;
            //DataSet result = excelReader.AsDataSet();

            ////5. Data Reader methods
            //while (excelReader.Read())
            //{
            //    //excelReader.GetInt32(0);
            //}

            //6. Free resources (IExcelDataReader is IDisposable)
            excelReader.Close();
            DataTable result = dataSet.Tables[0];
            return result;
        }


    }
}
