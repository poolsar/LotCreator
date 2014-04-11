using System;
using System.Linq;
using ShopDataLib;

namespace PrestaWinClient
{
    public class SupNode : ITreeNode
    {
        private TreeNodes _childs;
        public Supplier Base { get; set; }
        public ITreeNode Parent { get; set; }

        public TreeNodes Childs
        {
            get
            {
                _childs = new TreeNodes();
                Base.Categories.Where(c => c.Parent == null).ToList().Select(c => new SupCatNode(c)).ToList().ForEach(c => _childs.Add(c));

                return _childs;
            }
            private set { _childs = value; }
        }

        public int ImageIndex
        {
            get { return 0; }
        }

        public SupNode(Supplier @base)
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