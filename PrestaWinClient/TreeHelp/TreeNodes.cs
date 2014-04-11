using System.ComponentModel;
using DevExpress.XtraTreeList;

namespace PrestaWinClient
{
    public class TreeNodes : BindingList<ITreeNode>, TreeList.IVirtualTreeListData
    {
        void TreeList.IVirtualTreeListData.VirtualTreeGetChildNodes(VirtualTreeGetChildNodesInfo info)
        {
            ITreeNode obj = info.Node as ITreeNode;
            info.Children = obj.Childs;
        }
        
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            base.OnAddingNew(e);
        }

        protected override void InsertItem(int index, ITreeNode item)
        {
            base.InsertItem(index, item);
        }

        //protected override void InsertItem(int index, SupNode item)
        //{
        //    item.Parent = this;
        //    base.InsertItem(index, item);
        //}

        void TreeList.IVirtualTreeListData.VirtualTreeGetCellValue(VirtualTreeGetCellValueInfo info)
        {
            ITreeNode obj = info.Node as ITreeNode;

            info.CellData = obj.GetCellData(info.Column.Caption);
        }
        void TreeList.IVirtualTreeListData.VirtualTreeSetCellValue(VirtualTreeSetCellValueInfo info)
        {
            ITreeNode obj = info.Node as ITreeNode;

            obj.SetCellData(info.Column.Caption, info.NewCellData);

        }
    }
}