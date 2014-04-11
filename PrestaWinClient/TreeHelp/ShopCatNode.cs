using System;
using System.Linq;
using ShopDataLib;

namespace PrestaWinClient
{
    public class ShopCatNode : ITreeNode
    {
        private TreeNodes _childs;
        public ShopCategory Base { get; set; }
        public ITreeNode Parent { get; set; }

        public TreeNodes Childs
        {
            get
            {
                if (_childs == null)
                {
                    _childs = new TreeNodes();

                    if (Base.Childs != null)
                        Base.Childs.ToList().Select(c => new ShopCatNode(c, this)).ToList().ForEach(c => _childs.Add(c));

                    if (Base.Products != null)
                        Base.Products.ToList()
                            .Select(s => new ShopProdNode(s, this))
                            .ToList()
                            .ForEach(s => _childs.Add(s));
                }

                return _childs;
            }
            set { _childs = value; }
        }

        public int ImageIndex
        {
            get { return 1; }
        }

        public ShopCatNode(ShopCategory @base, ShopCatNode parentShopCatNode)
        {
            Base = @base;
            Parent = parentShopCatNode;
        }

        public object GetCellData(string colName)
        {
            switch (colName)
            {
                case ShopTreeColNames.Title:
                    return Base.Title;
                case ShopTreeColNames.InShop:
                    return Base.InShop;
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
                    Base.SetInShopRecursive((bool)newCellData);
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