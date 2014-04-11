using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Utils
{
    public class ImageLiberator
    {
        static Dictionary<string, Image> imageDict = new Dictionary<string, Image>();
        static Dictionary<Form, List<Image>> formImageDict = new Dictionary<Form, List<Image>>();

        public static Image ImageFromFile(string path, Form form = null)
        {
            Image image = null;
            if (!imageDict.TryGetValue(path, out image))
            {
                image = Image.FromFile(path);
                imageDict.Add(path, image);
            }

            if (form != null)
            {
                form.Disposed -= form_Disposed;
                form.Disposed += form_Disposed;

                formImageDict.GetOrAdd(form).Add(image);
            }

            return image;
        }

        public class ImagePath
        {
            public Image Image { get; set; }
            public string Path { get; set; }
        }

        public static Dictionary<string, Image> ImagesFromFile(List<string> paths, Form form = null)
        {
             var pathsToLoad = paths.Except(imageDict.Keys).ToList();
            
            var imagesPaths = pathsToLoad.AsParallel()
                .Select(p => new ImagePath{Path = p, Image = Image.FromFile(p)})
                .ToList();

            foreach (var image in imagesPaths)
            {
                imageDict.Add(image.Path, image.Image);
            }

            if (form != null)
            {
                form.Disposed -= form_Disposed;
                form.Disposed += form_Disposed;
            }

            var res = new Dictionary<string, Image>();

            foreach (var path in paths)
            {
                var image = imageDict[path];
                res.Add(path, image);

                if (form != null)
                    formImageDict.GetOrAdd(form).Add(image);
            }

            return res;
        }



        static void form_Disposed(object sender, System.EventArgs e)
        {
            var form = sender as Form;
            if (form == null) return;

            List<Image> images = formImageDict.GetOrAdd(form);
            foreach (var image in images)
            {
                FreeImageFile(image);
            }

            formImageDict.Remove(form);

        }


        public static void FreeImageFile(Image image)
        {
            var path = imageDict.KeyByValue(image);
            if (path == null) return;

            image.Dispose();
            imageDict.Remove(path);
        }

        public static void FreeImageFile(string path)
        {
            Image image = null;
            if (!imageDict.TryGetValue(path, out image)) return;

            image.Dispose();
            imageDict.Remove(path);
        }

        public static void DelteImageFile(string path)
        {
            FreeImageFile(path);
            File.Delete(path);
        }

    }
}
