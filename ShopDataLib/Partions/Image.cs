using System.IO;
using System.Linq;
using Utils;

namespace ShopDataLib
{
    public partial class Image : IHaveScrapeStatus
    {
        public override string ToString()
        {
            string toString = string.Format("{0}", LocalPath);
            return toString;
        }
        
        public static Image AddImage(IHaveImages imagesHost, string imagePath)
        {
            var file = new FileInfo(imagePath);

            bool exist = imagesHost.Images.Any(i => new FileInfo(i.LocalPath).FullName == file.FullName);
            if (exist) return null;

            var folder = imagesHost.GetImgFolder();
            var fileName = FileSystemUtils.GetFileName(file, folder);
            string path = string.Format("{0}/{1}", folder.FullName, fileName);
            file = file.CopyTo(path);

            string localPath = file.FullName.ExeptString(Directory.GetCurrentDirectory() + @"\");
            var image = new Image();
            image.LocalPath = localPath;
            imagesHost.Images.Add(image);

            return image;
        }

        public static void RemoveImage(IHaveImages imagesHost, string imagePath)
        {
            var file = new FileInfo(imagePath);

            var imageToRemove = imagesHost.Images.FirstOrDefault(i => new FileInfo(i.LocalPath).FullName == file.FullName);
            if (imageToRemove == null) return;

            imageToRemove.Delete();
        }
 
        public void Delete()
        {
            var file = new FileInfo(LocalPath);
            ImageLiberator.DelteImageFile(file.FullName);
            Context.Inst.ImageSet.Remove(this);
        }

        public static Image GetDefaultImage(IHaveImages imagesHost)
        {
            if (imagesHost.DefaultImage==null)
            {
                imagesHost.DefaultImage = imagesHost.Images.FirstOrDefault();
            }

            return imagesHost.DefaultImage;
        }

        public static void ImageUpdate(IHaveImages imagesHost, string imagePath)
        {
            string fullImagePath = new FileInfo(imagePath).FullName;
            var image = imagesHost.Images.FirstOrDefault(i => new FileInfo(i.LocalPath).FullName == fullImagePath);
            if (image == null) return;

            image.IdOnWebStore = null;
        }
        
    }
}