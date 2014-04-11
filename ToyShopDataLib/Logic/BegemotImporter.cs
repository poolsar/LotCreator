using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
//using BegemotImportLib;
//using BegemotPriceRow = ToyShopDataLib.Logic.BegemotPriceRow;

namespace ToyShopDataLib
{
    public class BegemotImporter
    {
       public void Import()
        {
            // скачивание прайса
            ProccessMesenger.Write("Импорт: скачивание прайса");
            var begemotPrice = new BegemotParser();
            DataTable table = begemotPrice.GetPriceTable();

            // импорт прайса в базу данных
            ProccessMesenger.Write("Импорт: загрузка прайса в базу данных");
            using (var copy = new SqlBulkCopy(Context.Inst.Database.Connection.ConnectionString))
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    copy.ColumnMappings.Add(i, i);
                }

                copy.DestinationTableName = "BegemotProductSet";
                copy.WriteToServer(table);
            }

            // обработка импортированных данных
            ProccessMesenger.Write("Импорт: обработка импортированных данных");
            var bulkUpdateSql = File.ReadAllText("SQL/bulk update/_all.sql", Encoding.GetEncoding("windows-1251"));
            var executed = Context.Inst.Database.ExecuteSqlCommand(bulkUpdateSql);

            ProccessMesenger.Write("Импорт: синхронизация товаров");
            Product.SyncProducts();

            ProccessMesenger.Write("Импорт: успешно завершен");
        }

         

        //private void UpdateDeactiveProducts(List<BegemotPriceRow> begemotPriceRows, DateTime now)
        //{
        //    var activeProdArticles = Context.Inst.BegemotProductSet.Where(p => p.Active).Select(p => p.Article).ToList();
        //    var priceRowsArticles = begemotPriceRows.Select(r => r.Article.ToString()).ToList();
        //    var deactiveProdArticles = activeProdArticles.Except(priceRowsArticles).ToList();

        //    if (deactiveProdArticles.Count == 0) return;

        //    var deactiveProducts =
        //        (
        //            from p in Context.Inst.BegemotProductSet.ToList()
        //            from a in deactiveProdArticles
        //            where a == p.Article
        //            select p
        //            ).ToList();

        //    //Context.Inst.BegemotProductSet.Where(p => deactiveProdArticles.Any(a => a == p.Article)).ToList();

        //    foreach (var product in deactiveProducts)
        //    {
        //        product.Active = false;
        //        product.DateUpdate = now;
        //        UpdateCount(now, product, 0);
        //    }

        //    Save();
        //}

        //private List<BegemotProduct> newProducts = new List<BegemotProduct>();
        //private List<BegemotPriceHistory> newPriceHistory = new List<BegemotPriceHistory>();
        //private List<BegemotCountHistory> newCountHistory = new List<BegemotCountHistory>();


        //private void UpdateActiveProducts(List<BegemotPriceRow> begemotPriceRows, DateTime now)
        //{
        //    // var begemotProducts = Context.Inst.BegemotProductSet.ToList();

        //    //var begemotProductsDict = Context.Inst.BegemotProductSet.Where(p => p.Active).ToDictionary(p => p.Article);

        //    var begemotProductsDict = Context.Inst.BegemotProductSet.ToDictionary(p => p.Article);


        //    int count = begemotPriceRows.Count;
        //    int counter = 0;
        //    int hundredCounter = 0;


        //    startTime = DateTime.Now;

        //    foreach (var priceRow in begemotPriceRows)
        //    {
        //        var article = priceRow.Article.ToString();

        //        BegemotProduct product = null;

        //        var isNewProduct = !begemotProductsDict.TryGetValue(article, out product);

        //        if (isNewProduct)
        //        {
        //            product = new BegemotProduct();
        //            Add(product);
        //            product.Article = priceRow.Article.ToString();

        //            product.Group = priceRow.Group;
        //            product.Group1 = priceRow.Group1;
        //            product.Group2 = priceRow.Group2;

        //            product.Title = priceRow.Title;
        //            product.Brand = priceRow.Brand;
        //            product.Code = priceRow.Code.ToString();
        //            product.NDS = priceRow.NDS;
        //            product.CountPerBlock = priceRow.CountInBlock;
        //            product.CountPerBox = priceRow.CountInBox;
        //        }

        //        if (!product.Active)
        //        {
        //            product.Active = true;
        //        }


        //        bool updatePrice =
        //            isNewProduct ||
        //            (product.RetailPrice != priceRow.RetailPrice) ||
        //            (product.WholeSalePrice != priceRow.WholeSalePrice);

        //        bool updateCount = isNewProduct || (product.Count != priceRow.CountInStock);

        //        if (updatePrice)
        //        {
        //            UpdatePrice(now, product, priceRow.RetailPrice, priceRow.WholeSalePrice);
        //        }

        //        if (updateCount)
        //        {
        //            UpdateCount(now, product, priceRow.CountInStock);
        //        }

        //        product.DateUpdate = now;

        //        counter++;
        //        hundredCounter++;

        //        if (hundredCounter == 20)
        //        {
        //            hundredCounter = 0;

        //            Save();

        //            var duration = DateTime.Now - startTime;
        //            var percent = counter / (decimal)count;
        //            var prognozInSeconds = duration.TotalSeconds * (double)(1 / percent);
        //            var prognoz = TimeSpan.FromSeconds(prognozInSeconds);

        //            var mes =
        //                string.Format(
        //                    "Выполнено {0} из {1}   {2:p0} время: {3:hh\\:mm\\:ss} прогноз: {4:hh\\:mm\\:ss}", counter,
        //                    count, percent, duration, prognoz);

        //            Message(mes);
        //        }

        //    }


        //}

       // private DateTime startTime;


        //private void UpdateCount(DateTime now, BegemotProduct product, int newCount)
        //{
        //    var countHistory = new BegemotCountHistory();
        //    Add(countHistory);

        //    countHistory.Article = product.Article;
        //    countHistory.DateCreate = now;
        //    product.Count = countHistory.Count = newCount;
        //}


        //private void UpdatePrice(DateTime now, BegemotProduct product, decimal newRetailPrice, decimal newWholeSalePrice)
        //{
        //    var priceHistory = new BegemotPriceHistory();
        //    Add(priceHistory);

        //    priceHistory.Article = product.Article;
        //    priceHistory.DateCreate = now;
        //    product.RetailPrice = priceHistory.RetailPrice = newRetailPrice;
        //    product.WholeSalePrice = priceHistory.WholeSalePrice = newWholeSalePrice;
        //}


        //private void Add(BegemotProduct product)
        //{
        //    newProducts.Add(product);
        //}

        //private void Add(BegemotPriceHistory priceHistory)
        //{
        //    newPriceHistory.Add(priceHistory);
        //}

        //private void Add(BegemotCountHistory countHistory)
        //{
        //    newCountHistory.Add(countHistory);
        //}

        //private void Save()
        //{
        //    foreach (var product in newProducts)
        //    {
        //        Context.Inst.BegemotProductSet.Add(product);
        //    }

        //    foreach (var price in newPriceHistory)
        //    {
        //        Context.Inst.BegemotPriceHistorySet.Add(price);
        //    }

        //    foreach (var count in newCountHistory)
        //    {
        //        Context.Inst.BegemotCountHistorySet.Add(count);
        //    }

        //    newProducts.Clear();
        //    newPriceHistory.Clear();
        //    newCountHistory.Clear();

        //    Context.Save();
        //}


    }
}