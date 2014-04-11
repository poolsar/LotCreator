namespace ToyShopDataLib
{
    public partial class AdvImage
    {
        public bool NeedUpload()
        {
            bool need = string.IsNullOrWhiteSpace(CurrentVersion.Data);
            return need;
        }

        public string GetImagePath()
        {
            var path = CurrentVersion.ProductImageVersion.Path;
            return path;
        }

        public void SetUploadData(string uploadData)
        {
            CurrentVersion.Data = uploadData;
        }

        public string GetUploadData()
        {
            return CurrentVersion.Data;
        }
    }
}