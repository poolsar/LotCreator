using System.Collections.Generic;
using System.Linq;
using ImageMakerWpf;

namespace ToyShopDataLib
{
    public partial class ProductImage
    {
        public static void SyncImages(List<Product> products = null)
        {
            ProccessMesenger.Write("Синхронизация картинок");

            SyncImageData(products);
            SyncImageFiles();

            ProccessMesenger.Write("Синхронизация картинок успешно завершена.");
        }

        private static void SyncImageFiles()
        {

            var imgVersions = (from v in Context.Inst.ProductImageVersionSet
                               where v.Path == string.Empty
                               select v).ToList();

            var imgStyles = imgVersions.Select(v => v.ShowCaseStyle).Distinct().ToList();

            var showCaseDict = new Dictionary<ShowCaseStyle, ShowCaseImage>();

            foreach (var imgStyle in imgStyles)
            {
                var showCase = new ShowCaseImage(imgStyle);
                showCaseDict.Add(imgStyle, showCase);
            }

            int count = imgVersions.Count;
            for (int i = 0; i < imgVersions.Count; i++)
            {

                ProccessMesenger.Write("Синхронизация картинок. Подготовка создания файлов картинок {0} из {1}", i + 1, count);

                var version = imgVersions[i];
                var sourcePath = version.ProductImage.SourcePath;
                var path = version.GenerateImagePath();
                var pricePosition = ToyShopDataLib.PricePosition.TopRight ==
                                    (PricePosition)version.ProductImage.PricePosition;

                showCaseDict[version.ShowCaseStyle].AddTask(
                    version.Title, version.PriceNew, version.PriceOld,
                    sourcePath, path, pricePosition
                    );

                version.Path = path;
            }

            foreach (var showCase in showCaseDict.Values)
            {
                showCase.Execute();
            }

            Context.Save();
        }

        private static void SyncImageData(List<Product> products)
        {
            if (products == null)
            {
                products = Context.Inst.ProductSet.ToList();
            }

            int count = products.Count;

            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                var bproduct = product.BegemotProduct;

                ProccessMesenger.Write("Синхронизация информации о картинках {0} из {1}", i + 1, count);

                var image = product.Images.FirstOrDefault();

                if (image == null)
                {
                    image = new ProductImage();
                    Context.Inst.ProductImageSet.Add(image);
                    image.Product = product;

                    image.IsNoImage = bproduct.IsNoImage();

                    image.PricePosition = (int)product.PricePosition;

                    if (!image.IsNoImage)
                    {
                        image.SourcePath = bproduct.ImagePath;
                    }

                    Context.Save();
                }

                if (image.IsNoImage)
                {
                    continue;
                }

                var showCaseStyle = (int)product.GetShowCaseStyle();

                var version = image.Versions.FirstOrDefault
                    (
                        v =>
                            v.PriceNew == product.Price &&
                            v.PriceOld == product.PriceOld &&
                            v.Style == showCaseStyle
                    );

                if (version == null)
                {
                    version = new ProductImageVersion();
                    Context.Inst.ProductImageVersionSet.Add(version);
                    image.Versions.Add(version);

                    version.Path = string.Empty; //  product.GetImagePath();

                    product.ReApplyStyle(); // что-бы обновилось название стало без стиля площадки
                    version.Title = product.Title;

                    version.PriceNew = product.Price;
                    version.PriceOld = product.PriceOld;

                    version.Style = showCaseStyle;

                    // добавить стиль картинки
                    // и позицию цены

                    Context.Save();
                }

                if (image.CurrentVersion == null || image.CurrentVersion.Id != version.Id)
                {
                    image.CurrentVersion = version;
                    Context.Save();
                }
            }

            ProccessMesenger.Write("Синхронизация информации о картинках. Сохранение.");
            // Context.Save();
        }
    }
}