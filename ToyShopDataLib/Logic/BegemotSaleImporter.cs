using System;
using System.Collections.Generic;
//using BegemotImportLib;

namespace ToyShopDataLib
{
    public class BegemotSaleImporter
    {
        private List<BegemotSalePrice> _data;
        public List<BegemotSalePrice> Data
        {
            get { return _data; }
        
        }


        public void LoadFile(string filePath)
        {
            var parser = new BegemotParser();
            var saleRows = parser.ParseSale(filePath);

            _data = new List<BegemotSalePrice>();

            foreach (var saleRow in saleRows)
            {
                var salePrice = new BegemotSalePrice();

                salePrice.Article = saleRow.Article;
                salePrice.RetailPrice = saleRow.RetailPrice;
                salePrice.WholeSalePrice = saleRow.WholeSalePrice;
                salePrice.RetailPriceOld = saleRow.RetailPriceOld;
                salePrice.WholeSalePriceOld = saleRow.WholeSalePriceOld;

                _data.Add(salePrice);
            }
        }

        

        public void ImportSale(string description, DateTime dateStart, DateTime dateStop)
        {
            if (_data == null || _data.Count == 0)
            {
                throw new ApplicationException("Нет данных для импорта. Возможно предварительно не вызвана функция LoadFile");
                return;
            }

            var begemotSale = new BegemotSale();
            begemotSale.Description = description;
            begemotSale.DateStart = dateStart;
            begemotSale.DateStop = dateStop;
            Context.Inst.BegemotSaleSet.Add(begemotSale);

            foreach (var salePrice in _data)
            {
                salePrice.BegemotSale = begemotSale;
                Context.Inst.BegemotSalePriceSet.Add(salePrice);
            }

            begemotSale.UpdateActive();

            Context.Save();
        }
    }
}