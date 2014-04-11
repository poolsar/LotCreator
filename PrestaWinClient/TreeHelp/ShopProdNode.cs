using System;
using System.Linq;
using ShopDataLib;

namespace PrestaWinClient
{
    public class ShopProdNode : ITreeNode
    {
        public ShopProduct Base { get; set; }
        public ITreeNode Parent { get; set; }
        public TreeNodes Childs { get; set; }
        public int ImageIndex
        {
            get { return 2; }
        }

        public ShopProdNode(ShopProduct @base, ShopCatNode shopCatNode)
        {
            Base = @base;
            Parent = shopCatNode;
        }

        public object GetCellData(string colName)
        {
            switch (colName)
            {
                case ShopTreeColNames.Title:
                    return Base.Title;
                case ShopTreeColNames.InShop:
                    return Base.InShop;
                case ShopTreeColNames.DiscountPrice:
                    return Base.DiscountPrice;
                case ShopTreeColNames.PurchasingDiscountPrice:
                    return Base.PurchasingDiscountPrice();
                case ShopTreeColNames.Margin:
                    return Base.Margin;
                case ShopTreeColNames.MarginPercent:
                    return Base.MarginPercent;

                case ShopTreeColNames.Picture:
                    return Base.GetDefaulImageBin();

                default:
                    return null;
            }
        }

        public void SetCellData(string colName, object newCellData)
        {
            bool save = true;

            switch (colName)
            {
                case ShopTreeColNames.Title:
                    Base.Title = (string)newCellData;
                    break;
                case ShopTreeColNames.InShop:
                    Base.InShop = (bool)newCellData;
                    break;
                case ShopTreeColNames.DiscountPrice:
                    Base.DiscountPrice = (decimal)newCellData;
                    break;
                case ShopTreeColNames.Margin:
                    Base.Margin = (decimal)newCellData;
                    break;
                case ShopTreeColNames.MarginPercent:
                    Base.MarginPercent = (decimal)newCellData;
                    break;
                default:
                    save = false;
                    break;
            }

            if (save) Context.Save();
        }



        public void Delete()
        {
            Base.Delete();
        }

    }
}