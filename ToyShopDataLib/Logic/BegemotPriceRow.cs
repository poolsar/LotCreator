using System;
using System.Data;
using System.Text;

namespace ToyShopDataLib.Logic
{
    public class BegemotPriceRow
    {
        public string Group { get; set; }
        public string Group1 { get; set; }
        public string Group2 { get; set; }
        public int Article { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public int Code { get; set; }
        public int NDS { get; set; }
        public int CountInBlock { get; set; }
        public int CountInBox { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholeSalePrice { get; set; }
        public int CountInStock { get; set; }


        public decimal OldPrice
        {
            get
            {
                var result = RetailPrice * 1.15m;
                return result;
            }
        }

        public decimal DescountPrice
        {
            get
            {
                decimal result = (int)(RetailPrice / 10) * 10 + 9.99m;
                return result;
            }
        }

        private string _titleNormal;
        public string TitleNormal
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_titleNormal))
                {
                    string[] words = Title.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    var sb = new StringBuilder();
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (i == 1) continue;
                        var word = words[i];
                        sb.Append(word);

                        if (i < words.Length - 1)
                        {
                            sb.Append(" ");
                        }
                    }

                    _titleNormal = sb.ToString();
                }

                return _titleNormal;
            }
        }


        public BegemotPriceRow(DataRow row)
        {
            Group = (string)row[0];
            Group1 = (string)row[1];
            Group2 = (string)row[2];
            Article = Convert.ToInt32(row[3]);
            Title = (string)row[4];
            Brand = (string)row[5];
            Code = Convert.ToInt32(row[6]);
            NDS = Convert.ToInt32(row[7].ToString().Replace("%", ""));
            CountInBlock = Convert.ToInt32(row[8]);
            CountInBox = Convert.ToInt32(row[9]);
            RetailPrice = Convert.ToDecimal(row[10]);
            WholeSalePrice = Convert.ToDecimal(row[11]);
            CountInStock = Convert.ToInt32(row[12]);
        }
    }

    public class BegemotSaleRow
    {
        public BegemotSaleRow(DataRow row)
        {
            Article = Convert.ToInt32(row[0]).ToString();
            RetailPrice = Convert.ToDecimal(row[1]);
            WholeSalePrice = Convert.ToDecimal(row[2]);
            RetailPriceOld = Convert.ToDecimal(row[3]);
            WholeSalePriceOld = Convert.ToDecimal(row[4]);

        }

        public decimal WholeSalePrice { get; set; }

        public decimal RetailPrice { get; set; }

        public string Article { get; set; }

        public decimal RetailPriceOld { get; set; }
        public decimal WholeSalePriceOld { get; set; }
    
    }
}