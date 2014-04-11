//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using Excel;

//namespace ImageMakerWpf
//{
//    class BegemotPrice
//    {
//        internal class BegemotPriceRow
//        {
//            public string Group1 { get; set; }
//            public string Group2 { get; set; }
//            public string Group3 { get; set; }
//            public int Article { get; set; }
//            public string Title { get; set; }
//            public string Brand { get; set; }
//            public int Code { get; set; }
//            public int NDS { get; set; }
//            public int CountInBlock { get; set; }
//            public int CountInBox { get; set; }
//            public decimal RetailPrice { get; set; }
//            public decimal WholeSalePrice { get; set; }
//            public int CountInStock { get; set; }


//            public decimal OldPrice
//            {
//                get
//                {
//                    var result = RetailPrice * 1.15m;
//                    return result;
//                }
//            }

//            public decimal DescountPrice
//            {
//                get
//                {
//                    decimal result = (int)(RetailPrice / 10) * 10 + 9.99m;
//                    return result;
//                }
//            }

//            private string _titleNormal;
//            public string TitleNormal
//            {
//                get
//                {
//                    if (string.IsNullOrWhiteSpace(_titleNormal))
//                    {
//                        string[] words = Title.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries);

//                        var sb = new StringBuilder();
//                        for (int i = 0; i < words.Length; i++)
//                        {
//                            if (i == 1) continue;
//                            var word = words[i];
//                            sb.Append(word);

//                            if (i < words.Length - 1)
//                            {
//                                sb.Append(" ");
//                            }
//                        }

//                        _titleNormal = sb.ToString();
//                    }

//                    return _titleNormal;
//                }
//            }


//            public BegemotPriceRow(DataRow row)
//            {
//                Group1 = (string)row[0];
//                Group2 = (string)row[1];
//                Group3 = (string)row[2];
//                Article = Convert.ToInt32(row[3]);
//                Title = (string)row[4];
//                Brand = (string)row[5];
//                Code = Convert.ToInt32(row[6]);
//                NDS = Convert.ToInt32(row[7].ToString().Replace("%", ""));
//                CountInBlock = Convert.ToInt32(row[8]);
//                CountInBox = Convert.ToInt32(row[9]);
//                RetailPrice = Convert.ToDecimal(row[10]);
//                WholeSalePrice = Convert.ToDecimal(row[11]);
//                CountInStock = Convert.ToInt32(row[12]);
//            }
//        }

//        public static List<BegemotPriceRow> ParsePrice()
//        {


//            DataTable priceTable = GetExcelTable("c:/mytemp/PriceSoftToys.xlsx");

//            var begemotPriceRows = new List<BegemotPriceRow>();
//            for (int i = 4; i < priceTable.Rows.Count; i++)
//            {
//                var dataRow = priceTable.Rows[i];
//                var begemotRow = new BegemotPriceRow(dataRow);
//                begemotPriceRows.Add(begemotRow);
//            }

//            return begemotPriceRows;

//        }


//        private static DataTable GetExcelTable(string filePath)
//        {
//            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

//            ////1. Reading from a binary Excel file ('97-2003 format; *.xls)
//            //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
//            //...
//            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
//            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
//            //...
//            //3. DataSet - The result of each spreadsheet will be created in the result.Tables
//            DataSet dataSet = excelReader.AsDataSet();
//            //...
//            ////4. DataSet - Create column names from first row
//            //excelReader.IsFirstRowAsColumnNames = true;
//            //DataSet result = excelReader.AsDataSet();

//            ////5. Data Reader methods
//            //while (excelReader.Read())
//            //{
//            //    //excelReader.GetInt32(0);
//            //}

//            //6. Free resources (IExcelDataReader is IDisposable)
//            excelReader.Close();
//            DataTable result = dataSet.Tables[0];
//            return result;
//        }


//    }
//}
