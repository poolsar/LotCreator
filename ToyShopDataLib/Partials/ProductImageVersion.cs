using ImageMakerWpf;

namespace ToyShopDataLib
{
    public partial class ProductImageVersion
    {
        public string GenerateImagePath()
        {
            var ImagePath = string.Format("{0}\\{1}.{2}.{3}.jpg", Context.ImageSaveFolder, ProductImage.Product.Article, ProductImage.Id, Id);
            return ImagePath;
        }


        public ShowCaseStyle ShowCaseStyle
        {
            get
            {
                return (ShowCaseStyle)this.Style;
            }
            set
            {
                this.Style = (int)ShowCaseStyle;
            }
        }
    }
}