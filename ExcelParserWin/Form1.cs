using System.Collections;
using System.Diagnostics;
using DevExpress.XtraPrinting.Native;
using Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelParserWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int COL_ABC_TITLE = 1;
        int COL_ABC_COUNT = 2;

        int COL_PRICE_TITLE = 4;
        int COL_PRICE_PRICE = 10;

        private Dictionary<string, DataRow> priceRowsDict;

        private void simpleButton1_Click(object sender, EventArgs e)
        {


            DataTable priceTable = GetExcelTable("c:/mytemp/price.xlsx");
            DataTable abcTable = GetExcelTable("c:/mytemp/abc.xlsx");

            DataRow[] priceRows = new DataRow[priceTable.Rows.Count];
            priceTable.Rows.CopyTo(priceRows, 0);

            DataRow[] abcRows = new DataRow[abcTable.Rows.Count];
            abcTable.Rows.CopyTo(abcRows, 0);

            //string tovNumber = priceRows[10][6].ToString();
            //string url = string.Format("http://www.begemott.ru/tov_{0}.html", tovNumber);
            //Process.Start(url);
            //return;


            var priceDict = new Dictionary<string, decimal>();
            var abcDict = new Dictionary<string, decimal>();


            priceRowsDict = new Dictionary<string, DataRow>();

            priceRows.ToList().ForEach(p =>
            {
                var title = p[COL_PRICE_TITLE] as string;
                if (title != null && !priceDict.ContainsKey(title))
                {
                    priceDict.Add(p[COL_PRICE_TITLE] as string,
                        Convert.ToDecimal(p[COL_PRICE_PRICE]));
                    priceRowsDict.Add(title, p);
                }
            });

            abcRows.ToList().ForEach(p =>
            {
                var title = p[COL_ABC_TITLE] as string;
                if (title != null && !abcDict.ContainsKey(title))
                {
                    abcDict.Add(p[COL_ABC_TITLE] as string,
                        Convert.ToDecimal(p[COL_ABC_COUNT]));
                }
            });


            var rr = from p in priceDict
                     let title = p.Key
                     let price = p.Value
                     let shortTitle = abcDict.Keys.FirstOrDefault(a => title.Contains(a))
                     let count = shortTitle == null ? 0 : abcDict[shortTitle]
                     where count != 0
                     let pribil = count * price * (decimal)0.85
                     select new { title, price, count, pribil };

            var abcResult = rr.ToList();

            var maxPribil = abcResult.Max(a => a.pribil);
            var fullPribil = abcResult.Sum(a => a.pribil);


            var abcResult2 = abcResult.Select(a => new AbcRow
            {
                Title = a.title,
                Count = a.count,
                Price = a.price,
                pribil = a.pribil,
                PercToFull = (a.pribil / fullPribil).ToString("p"),
                PercToMax = (a.pribil / maxPribil).ToString("p")
            }).ToList();





            gridControl1.DataSource = abcResult2;
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();



        }

        private class AbcRow
        {
            public string Title { get; set; }

            public decimal Count { get; set; }

            public decimal Price { get; set; }

            public decimal pribil { get; set; }

            public string PercToFull { get; set; }

            public string PercToMax { get; set; }
        }

        private static DataTable GetExcelTable(string filePath)
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

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;

            AbcRow abcInst = (AbcRow)gridView1.GetFocusedRow();

            var abcRow = priceRowsDict[abcInst.Title];
            var tovNumber = abcRow[6];
            string url = string.Format("http://www.begemott.ru/tov_{0}.html", tovNumber);

            //http://www.begemott.ru/tov_400117358.html

            Process.Start(url);
        }
    }
}
