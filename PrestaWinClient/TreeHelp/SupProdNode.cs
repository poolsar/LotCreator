using System;
using ShopDataLib;

namespace PrestaWinClient
{
    public class SupProdNode : ITreeNode
    {
        public SupplierProduct Base { get; set; }
        public ITreeNode Parent { get; set; }
        public TreeNodes Childs { get; set; }
        public int ImageIndex
        {
            get { return 2; }
        }

        public SupProdNode(SupplierProduct @base)
        {
            Base = @base;
        }

        public object GetCellData(string colName)
        {
            switch (colName)
            {
                case "Название":
                    return Base.Title;
                default:
                    return null;
            }
        }

        public void SetCellData(string colName, object newCellData)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Drop(ITreeNode targetData)
        {
            throw new NotImplementedException();
        }
    }
}