using System;
using System.Linq;
using ShopDataLib;

namespace PrestaWinClient
{
    public class SupCatNode : ITreeNode
    {
        private TreeNodes _childs;
        public SupplierCategory Base { get; set; }
        public ITreeNode Parent { get; set; }

        public TreeNodes Childs
        {
            get
            {
                _childs = new TreeNodes();
                Base.Childs.ToList().Select(c => new SupCatNode(c)).ToList().ForEach(c => _childs.Add(c));
                Base.Products.ToList().Select(s => new SupProdNode(s)).ToList().ForEach(s => _childs.Add(s));

                return _childs;
            }
            set { _childs = value; }
        }

        public int ImageIndex
        {
            get { return 1; }
        }

        public SupCatNode(SupplierCategory @base)
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
            if (targetData ==null )
            {
                
            }
        }
    }
}