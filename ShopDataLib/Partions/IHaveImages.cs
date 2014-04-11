using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils;

namespace ShopDataLib
{
    public interface IHaveImages
    {
        ICollection<Image> Images { get; set; }
        Image DefaultImage { get; set; }
        IEnumerable<Image> SameCategoryImages();
        void RemoveImage(string path);
        Image AddImage(string path);
        DirectoryInfo GetImgFolder();


        void ImageUpdated(string imagePath);
    }

    public static class IHaveImagesUtils
    {
        public static System.Drawing.Image GetDefaulImageBin(this IHaveImages imagesHost)
        {
            System.Drawing.Image res = null;

            var defImage = imagesHost.DefaultImage;
            if (defImage == null)
            {
                defImage = imagesHost.Images.FirstOrDefault();
            }

            if (defImage != null)
            {
                res = ImageLiberator.ImageFromFile(defImage.LocalPath);
            }

            return res;
        }
    }

}