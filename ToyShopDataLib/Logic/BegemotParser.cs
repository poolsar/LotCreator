using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Xml;
using ToyShopDataLib.Logic;

namespace ToyShopDataLib
{
    public class BegemotParser
    {

        public List<BegemotPriceRow> ParsePrice()
        {
            DataTable priceTable = GetPriceTable();

            var begemotPriceRows = new List<BegemotPriceRow>();
            foreach (DataRow dataRow in priceTable.Rows)
            {
                var begemotRow = new BegemotPriceRow(dataRow);
                begemotPriceRows.Add(begemotRow);
            }

            return begemotPriceRows;
        }

        public List<BegemotSaleRow> ParseSale(string path)
        {
            throw new NotImplementedException();
            //DataTable priceTable = GetExcelTable(path);

            //var saleRows = new List<BegemotSaleRow>();
            //for (int i = 0; i < priceTable.Rows.Count; i++)
            //{
            //    var dataRow = priceTable.Rows[i];
            //    var begemotRow = new BegemotSaleRow(dataRow);
            //    saleRows.Add(begemotRow);
            //}

            //return saleRows;
        }

        //private DataTable GetExcelTable(string filePath)
        //{
        //    FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

        //    ////1. Reading from a binary Excel file ('97-2003 format; *.xls)
        //    //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
        //    //...
        //    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        //    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //    //...
        //    //3. DataSet - The result of each spreadsheet will be created in the result.Tables
        //    DataSet dataSet = excelReader.AsDataSet();
        //    //...
        //    ////4. DataSet - Create column names from first row
        //    //excelReader.IsFirstRowAsColumnNames = true;
        //    //DataSet result = excelReader.AsDataSet();

        //    ////5. Data Reader methods
        //    //while (excelReader.Read())
        //    //{
        //    //    //excelReader.GetInt32(0);
        //    //}

        //    //6. Free resources (IExcelDataReader is IDisposable)
        //    excelReader.Close();
        //    DataTable result = dataSet.Tables[0];
        //    return result;
        //}

        public DataTable GetPriceTable()
        {
            var copyTable = new DataTable();

            copyTable.Columns.Add("Id", typeof(int));
            copyTable.Columns.Add("Group", typeof(string));
            copyTable.Columns.Add("Group1", typeof(string));
            copyTable.Columns.Add("Group2", typeof(string));
            copyTable.Columns.Add("Article", typeof(string));
            copyTable.Columns.Add("Title", typeof(string));
            copyTable.Columns.Add("Brand", typeof(string));
            copyTable.Columns.Add("Code", typeof(string));
            copyTable.Columns.Add("NDS", typeof(int));
            copyTable.Columns.Add("CountPerBlock", typeof(int));
            copyTable.Columns.Add("CountPerBox", typeof(int));
            copyTable.Columns.Add("RetailPrice", typeof(decimal));
            copyTable.Columns.Add("WholeSalePrice", typeof(decimal));
            copyTable.Columns.Add("Count", typeof(int));
            copyTable.Columns.Add("Descrption", typeof(string));
            copyTable.Columns.Add("ImagePath", typeof(string));
            copyTable.Columns.Add("DateUpdate", typeof(DateTime));
            copyTable.Columns.Add("Active", typeof(bool));
            copyTable.Columns.Add("CopyInfo", typeof(string));

            InitPriceTable(copyTable);

            return copyTable;
        }
        public void InitPriceTable(DataTable table)
        {
            string xmlString = string.Empty;

            //xmlString = File.ReadAllText("c:/mytemp/price.xml");

            xmlString = MakeRequests();

            xmlString = xmlString.Replace("ss:", ""); var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(xmlString);
            var rows = doc.DocumentNode.SelectNodes("//workbook/worksheet/table/row").Skip(4).ToList();

            var now = DateTime.Now;


            foreach (var row in rows)
            {
                var cells = row.SelectNodes("cell/data");
                if (cells == null || cells.Count() != 14) continue;

                var values = cells.Select(d => d.InnerText.Replace(Environment.NewLine, string.Empty).Trim()).ToArray();

                var crow = table.Rows.Add();


                for (int i = 0; i < 13; i++)
                {
                    SetCellValue(table, i, crow, values);
                }

                crow[16] = now;
                crow[17] = true;
                crow[18] = "import";
            }
        }

        private static Dictionary<int, Func<object, object>> convDict;
        private static void SetCellValue(DataTable table, int inIndex, DataRow crow, string[] values)
        {var converters = getConvDict(table);

            var outIndex = inIndex + 1;

            var converter = converters[outIndex];

            var inValue = values[inIndex];
            var outValue = converter(inValue);
            crow[outIndex] = outValue;
        }

        private static Dictionary<int, Func<object, object>> getConvDict(DataTable table)
        {
            if (convDict == null)
            {
                convDict = new Dictionary<int, Func<object, object>>();

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    DataColumn column = table.Columns[j];

                    Func<object, object> conFunc = null;

                    if (column.DataType == typeof(string))
                    {
                        conFunc = v => v;
                    }
                    else if (column.DataType == typeof(int))
                    {
                        conFunc = v =>
                        {
                            var emp = string.Empty;
                            var vStr = (v as string);
                            vStr = vStr.Replace("%", emp).Replace(Environment.NewLine, emp);
                            var vInt = Convert.ToInt32(vStr);
                            return vInt;
                        };
                    }
                    else if (column.DataType == typeof(decimal))
                    {
                        conFunc = v => Convert.ToDecimal(v);
                    }
                    else if (column.DataType == typeof(DateTime))
                    {
                        conFunc = v => Convert.ToDateTime(v);
                    }
                    else if (column.DataType == typeof(bool))
                    {
                        conFunc = v => Convert.ToDateTime(v);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    convDict.Add(j, conFunc);
                }
            }

            return convDict;
        }


        private string MakeRequests()
        {
            HttpWebResponse response;
            string responseText = null;

            if (Request_www_begemott_ru(out response))
            {
                responseText = ReadResponse(response);

                response.Close();
            }

            return responseText;
        }

        private static string ReadResponse(HttpWebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                Stream streamToRead = responseStream;
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    streamToRead = new GZipStream(streamToRead, CompressionMode.Decompress);
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    streamToRead = new DeflateStream(streamToRead, CompressionMode.Decompress);
                }

                using (StreamReader streamReader = new StreamReader(streamToRead, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        private bool Request_www_begemott_ru(out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.begemott.ru/getprice_custom.xls");

                request.KeepAlive = true;
                request.Headers.Set(HttpRequestHeader.CacheControl, "max-age=0");
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Origin", @"http://www.begemott.ru");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = "http://www.begemott.ru/price_custom/";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
                request.Headers.Set(HttpRequestHeader.Cookie, @"PHPSESSID=b8apbdnp8q0iajq8ma5r81e0l1; ID=335530; USER=21493; LOGIN=nataliemorningangel%40mail.ru; CITY=604; FORMAT=1; LOYALTY=1; CMP=all; __utma=1.1253204801.1390926306.1395753471.1395800565.128; __utmb=1.7.10.1395800565; __utmc=1; __utmz=1.1394032491.85.4.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); _ym_visorc_15548317=b");

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body = @"price_grp_all=1&price_grp_id_400126364=1&price_sgrp_id_400126364_400126370=1&price_sgrp_id_400126364_400137876=1&price_sgrp_id_400126364_400126371=1&price_sgrp_id_400126364_400126372=1&price_sgrp_id_400126364_400126423=1&price_sgrp_id_400126364_400126374=1&price_sgrp_id_400126364_400137877=1&price_sgrp_id_400126364_400126404=1&price_sgrp_id_400126364_400126378=1&price_sgrp_id_400126364_400126375=1&price_sgrp_id_400126364_400126406=1&price_sgrp_id_400126364_400126376=1&price_sgrp_id_400126364_400126367=1&price_sgrp_id_400126364_400126407=1&price_sgrp_id_400126364_400126377=1&price_sgrp_id_400126364_400126369=1&price_sgrp_id_400126364_400137875=1&price_sgrp_id_400126364_400134121=1&price_sgrp_id_400126364_400126373=1&price_sgrp_id_400126364_400126368=1&price_grp_id_400117549=1&price_sgrp_id_400117549_400117551=1&price_sgrp_id_400117549_400117556=1&price_sgrp_id_400117549_400137780=1&price_sgrp_id_400117549_400117550=1&price_sgrp_id_400117549_400118238=1&price_sgrp_id_400117549_400117553=1&price_sgrp_id_400117549_400134532=1&price_sgrp_id_400117549_400117554=1&price_sgrp_id_400117549_400118241=1&price_sgrp_id_400117549_400117555=1&price_sgrp_id_400117549_400122550=1&price_grp_id_400117075=1&price_grp_id_400117077=1&price_sgrp_id_400117077_400117078=1&price_sgrp_id_400117077_400117079=1&price_sgrp_id_400117077_400117081=1&price_sgrp_id_400117077_400117082=1&price_sgrp_id_400117077_400117083=1&price_sgrp_id_400117077_400117085=1&price_sgrp_id_400117077_400117086=1&price_sgrp_id_400117077_400117087=1&price_grp_id_400117090=1&price_sgrp_id_400117090_400117088=1&price_sgrp_id_400117090_400117091=1&price_grp_id_400117093=1&price_sgrp_id_400117093_400117095=1&price_sgrp_id_400117093_400117094=1&price_grp_id_400116436=1&price_grp_id_400117096=1&price_sgrp_id_400117096_400117097=1&price_sgrp_id_400117096_400117098=1&price_sgrp_id_400117096_400117099=1&price_sgrp_id_400117096_400119923=1&price_sgrp_id_400117096_400117100=1&price_sgrp_id_400117096_400117101=1&price_sgrp_id_400117096_400117102=1&price_grp_id_400116670=1&price_sgrp_id_400116670_400116981=1&price_sgrp_id_400116670_400116982=1&price_grp_id_400117103=1&price_sgrp_id_400117103_400117104=1&price_sgrp_id_400117103_400117106=1&price_sgrp_id_400117103_400117107=1&price_sgrp_id_400117103_400117108=1&price_grp_id_400117109=1&price_sgrp_id_400117109_400117110=1&price_sgrp_id_400117109_400117111=1&price_sgrp_id_400117109_400117150=1&price_sgrp_id_400117109_400117112=1&price_sgrp_id_400117109_400117113=1&price_sgrp_id_400117109_400117114=1&price_sgrp_id_400117109_400117149=1&price_grp_id_400117115=1&price_sgrp_id_400117115_400117116=1&price_sgrp_id_400117115_400117117=1&price_sgrp_id_400117115_400117118=1&price_sgrp_id_400117115_400117119=1&price_sgrp_id_400117115_400117120=1&price_grp_id_400116671=1&price_sgrp_id_400116671_400116983=1&price_sgrp_id_400116671_400116908=1&price_sgrp_id_400116671_400116985=1&price_sgrp_id_400116671_400116986=1&price_sgrp_id_400116671_400116987=1&price_sgrp_id_400116671_400116988=1&price_sgrp_id_400116671_400116989=1&price_sgrp_id_400116671_400116990=1&price_sgrp_id_400116671_400116991=1&price_sgrp_id_400116671_400116992=1&price_sgrp_id_400116671_400116993=1&price_sgrp_id_400116671_400116994=1&price_sgrp_id_400116671_400116995=1&price_sgrp_id_400116671_400116996=1&price_grp_id_400117121=1&price_sgrp_id_400117121_400117123=1&price_sgrp_id_400117121_400117133=1&price_sgrp_id_400117121_400117124=1&price_sgrp_id_400117121_400117126=1&price_sgrp_id_400117121_400117127=1&price_sgrp_id_400117121_400117130=1&price_sgrp_id_400117121_400117131=1&price_sgrp_id_400117121_400117132=1&price_grp_id_400117139=1&price_sgrp_id_400117139_400117134=1&price_sgrp_id_400117139_400117135=1&price_sgrp_id_400117139_400117136=1&price_sgrp_id_400117139_400117138=1&price_grp_id_400117140=1&price_sgrp_id_400117140_400117142=1&price_sgrp_id_400117140_400135314=1&price_sgrp_id_400117140_400117143=1&price_sgrp_id_400117140_400117144=1&price_grp_id_400116644=1&price_sgrp_id_400116644_400116858=1&price_sgrp_id_400116644_400116857=1&price_sgrp_id_400116644_400116859=1&price_sgrp_id_400116644_400116860=1&price_sgrp_id_400116644_400116861=1&price_grp_id_400116645=1&price_sgrp_id_400116645_400119361=1&price_sgrp_id_400116645_400116862=1&price_sgrp_id_400116645_400116863=1&price_sgrp_id_400116645_400116864=1&price_grp_id_400116646=1&price_sgrp_id_400116646_400116865=1&price_sgrp_id_400116646_400116866=1&price_sgrp_id_400116646_400116867=1&price_sgrp_id_400116646_400116868=1&price_sgrp_id_400116646_400120209=1&price_sgrp_id_400116646_400116869=1&price_grp_id_400116647=1&price_sgrp_id_400116647_400116870=1&price_sgrp_id_400116647_400116871=1&price_sgrp_id_400116647_400116872=1&price_sgrp_id_400116647_400116873=1&price_sgrp_id_400116647_400116874=1&price_sgrp_id_400116647_400116875=1&price_sgrp_id_400116647_400116876=1&price_grp_id_400116648=1&price_sgrp_id_400116648_400124405=1&price_sgrp_id_400116648_400116878=1&price_sgrp_id_400116648_400117547=1&price_sgrp_id_400116648_400117273=1&price_sgrp_id_400116648_400129034=1&price_sgrp_id_400116648_400117274=1&price_sgrp_id_400116648_400120475=1&price_sgrp_id_400116648_400117546=1&price_sgrp_id_400116648_400117271=1&price_grp_id_400116649=1&price_sgrp_id_400116649_400116883=1&price_sgrp_id_400116649_400116884=1&price_sgrp_id_400116649_400116885=1&price_sgrp_id_400116649_400116886=1&price_sgrp_id_400116649_400116887=1&price_sgrp_id_400116649_400116888=1&price_sgrp_id_400116649_400116889=1&price_sgrp_id_400116649_400116890=1&price_sgrp_id_400116649_400116891=1&price_sgrp_id_400116649_400116892=1&price_sgrp_id_400116649_400116893=1&price_grp_id_400116650=1&price_sgrp_id_400116650_400116894=1&price_sgrp_id_400116650_400116895=1&price_sgrp_id_400116650_400116896=1&price_sgrp_id_400116650_400116897=1&price_sgrp_id_400116650_400116898=1&price_grp_id_400116651=1&price_sgrp_id_400116651_400116899=1&price_sgrp_id_400116651_400116900=1&price_grp_id_400116652=1&price_sgrp_id_400116652_400119551=1&price_sgrp_id_400116652_400116901=1&price_sgrp_id_400116652_400116902=1&price_sgrp_id_400116652_400116903=1&price_sgrp_id_400116652_400116904=1&price_grp_id_400116653=1&price_sgrp_id_400116653_400116905=1&price_sgrp_id_400116653_400116906=1&price_sgrp_id_400116653_400116907=1&price_sgrp_id_400116653_400116909=1&price_sgrp_id_400116653_400116910=1&price_sgrp_id_400116653_400116911=1&price_sgrp_id_400116653_400116912=1&price_sgrp_id_400116653_400116913=1&price_sgrp_id_400116653_400116914=1&price_sgrp_id_400116653_400116915=1&price_grp_id_400116654=1&price_sgrp_id_400116654_400116916=1&price_sgrp_id_400116654_400116917=1&price_grp_id_400116655=1&price_sgrp_id_400116655_400116918=1&price_sgrp_id_400116655_400116919=1&price_sgrp_id_400116655_400116920=1&price_grp_id_400117257=1&price_grp_id_400121135=1&price_sgrp_id_400121135_400117261=1&price_sgrp_id_400121135_400121136=1&price_sgrp_id_400121135_400121137=1&price_sgrp_id_400121135_400121138=1&price_grp_id_400119400=1&price_sgrp_id_400119400_400117544=1&price_sgrp_id_400119400_400117262=1&price_grp_id_400119399=1&price_sgrp_id_400119399_400117263=1&price_sgrp_id_400119399_400117267=1&price_grp_id_400119402=1&price_sgrp_id_400119402_400117264=1&price_sgrp_id_400119402_400117545=1&price_sgrp_id_400119402_400134507=1&price_sgrp_id_400119402_400121144=1&price_grp_id_400117266=1&price_sgrp_id_400117266_400121786=1&price_grp_id_400121797=1&price_sgrp_id_400121797_400121800=1&price_sgrp_id_400121797_400121802=1&price_sgrp_id_400121797_400121803=1&price_sgrp_id_400121797_400121804=1&price_grp_id_400121149=1&price_sgrp_id_400121149_400117282=1&price_sgrp_id_400121149_400121150=1&price_sgrp_id_400121149_400121152=1&price_grp_id_400117275=1&price_sgrp_id_400117275_400121788=1&price_grp_id_400117276=1&price_sgrp_id_400117276_400121789=1&price_sgrp_id_400117276_400121790=1&price_sgrp_id_400117276_400121791=1&price_grp_id_400121807=1&price_sgrp_id_400121807_400121808=1&price_grp_id_400119403=1&price_sgrp_id_400119403_400117278=1&price_sgrp_id_400119403_400121780=1&price_sgrp_id_400119403_400121781=1&price_grp_id_400119398=1&price_sgrp_id_400119398_400117279=1&price_sgrp_id_400119398_400117315=1&price_sgrp_id_400119398_400121809=1&price_sgrp_id_400119398_400121134=1&price_sgrp_id_400119398_400117317=1&price_sgrp_id_400119398_400117268=1&price_sgrp_id_400119398_400121139=1&price_sgrp_id_400119398_400121141=1&price_sgrp_id_400119398_400117314=1&price_sgrp_id_400119398_400117316=1&price_sgrp_id_400119398_400121142=1&price_grp_id_400119401=1&price_sgrp_id_400119401_400121793=1&price_sgrp_id_400119401_400117281=1&price_sgrp_id_400119401_400121794=1&price_grp_id_400129728=1&price_grp_id_400116673=1&price_sgrp_id_400116673_400116999=1&price_sgrp_id_400116673_400117000=1&price_grp_id_400129730=1&price_grp_id_400129729=1&price_sgrp_id_400129729_400129733=1&price_sgrp_id_400129729_400129734=1&price_sgrp_id_400129729_400129732=1&price_grp_id_400116977=1&price_sgrp_id_400116977_400129736=1&price_sgrp_id_400116977_400129735=1&price_grp_id_400129731=1&price_sgrp_id_400129731_400116998=1&price_sgrp_id_400129731_400116980=1&price_grp_id_400116440=1&price_grp_id_400116674=1&price_sgrp_id_400116674_400117001=1&price_sgrp_id_400116674_400117002=1&price_sgrp_id_400116674_400117003=1&price_sgrp_id_400116674_400117004=1&price_grp_id_400116675=1&price_sgrp_id_400116675_400117005=1&price_sgrp_id_400116675_400117006=1&price_sgrp_id_400116675_400117007=1&price_sgrp_id_400116675_400117008=1&price_sgrp_id_400116675_400117009=1&price_sgrp_id_400116675_400117010=1&price_sgrp_id_400116675_400117011=1&price_sgrp_id_400116675_400117012=1&price_grp_id_400116676=1&price_sgrp_id_400116676_400117013=1&price_sgrp_id_400116676_400117014=1&price_sgrp_id_400116676_400117015=1&price_sgrp_id_400116676_400117016=1&price_sgrp_id_400116676_400117017=1&price_sgrp_id_400116676_400117018=1&price_sgrp_id_400116676_400117019=1&price_grp_id_400116677=1&price_sgrp_id_400116677_400117020=1&price_sgrp_id_400116677_400117021=1&price_grp_id_400116678=1&price_sgrp_id_400116678_400117022=1&price_grp_id_400116679=1&price_sgrp_id_400116679_400117023=1&price_sgrp_id_400116679_400117024=1&price_sgrp_id_400116679_400117025=1&price_sgrp_id_400116679_400117026=1&price_sgrp_id_400116679_400117027=1&price_sgrp_id_400116679_400117028=1&price_sgrp_id_400116679_400117029=1&price_sgrp_id_400116679_400117030=1&price_sgrp_id_400116679_400117031=1&price_sgrp_id_400116679_400117032=1&price_sgrp_id_400116679_400117033=1&price_sgrp_id_400116679_400117034=1&price_grp_id_400116680=1&price_sgrp_id_400116680_400117035=1&price_sgrp_id_400116680_400117036=1&price_sgrp_id_400116680_400117037=1&price_sgrp_id_400116680_400117038=1&price_sgrp_id_400116680_400117039=1&price_grp_id_400116681=1&price_sgrp_id_400116681_400117040=1&price_sgrp_id_400116681_400117041=1&price_sgrp_id_400116681_400117042=1&price_grp_id_400116682=1&price_sgrp_id_400116682_400117043=1&price_sgrp_id_400116682_400117044=1&price_sgrp_id_400116682_400117045=1&price_sgrp_id_400116682_400117046=1&price_grp_id_400116441=1&price_grp_id_400116683=1&price_sgrp_id_400116683_400117048=1&price_sgrp_id_400116683_400117049=1&price_sgrp_id_400116683_400117050=1&price_grp_id_400116684=1&price_sgrp_id_400116684_400117051=1&price_sgrp_id_400116684_400117052=1&price_grp_id_400116685=1&price_sgrp_id_400116685_400125841=1&price_sgrp_id_400116685_400117053=1&price_sgrp_id_400116685_400131596=1&price_sgrp_id_400116685_400117054=1&price_sgrp_id_400116685_400131365=1&price_sgrp_id_400116685_400129608=1&price_sgrp_id_400116685_400117055=1&price_sgrp_id_400116685_400117056=1&price_sgrp_id_400116685_400117057=1&price_grp_id_400116686=1&price_sgrp_id_400116686_400117058=1&price_sgrp_id_400116686_400117059=1&price_sgrp_id_400116686_400117060=1&price_sgrp_id_400116686_400132608=1&price_grp_id_400116687=1&price_sgrp_id_400116687_400117061=1&price_sgrp_id_400116687_400117062=1&price_sgrp_id_400116687_400117129=1&price_sgrp_id_400116687_400117063=1&price_sgrp_id_400116687_400117064=1&price_sgrp_id_400116687_400117065=1&price_grp_id_400116439=1&price_grp_id_400116669=1&price_sgrp_id_400116669_400116976=1&price_sgrp_id_400116669_400116997=1&price_sgrp_id_400116669_400116978=1&price_sgrp_id_400116669_400116979=1&part=1&price_with_rc=0&open_office_param=0";
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();

                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
                else return false;
            }
            catch (Exception)
            {
                if (response != null) response.Close();
                return false;
            }

            return true;
        }


    }
}