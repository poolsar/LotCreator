namespace PrestaWinClient
{
    public interface ITreeNode
    {
        ITreeNode Parent { get; set; }
        TreeNodes Childs { get; }
        int ImageIndex { get; }
        object GetCellData(string colName);
        void SetCellData(string colName, object newCellData);
        void Delete();
    }
}