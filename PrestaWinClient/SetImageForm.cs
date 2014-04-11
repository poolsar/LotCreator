using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using DevExpress.Data.PLinq.Helpers;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraPrinting.Native;
using PrestaSharp.Lib;
using ShopDataLib;
using Utils;
using Image = ShopDataLib.Image;

namespace PrestaWinClient
{
    public partial class SetImageForm : Form
    {
        public SetImageForm()
        {
            InitializeComponent();
        }

        public static void EditImages(IHaveImages imageHost)
        {

            SetImageForm form = new SetImageForm();
            form.ImagesHost = imageHost;
            form.ShowDialog();
        }

        public IHaveImages ImagesHost { get; set; }

        private GalleryItemGroup galleryItemGroup;
        private void SetImageForm_Load(object sender, EventArgs e)
        {
            Text = ImagesHost.ToString();


            var catSubImagesQ = ImagesHost.SameCategoryImages().Distinct();
            var catSubImages = catSubImagesQ.ToList();

            var allImages = catSubImages; // Context.Inst.ImageSet.ToList();
            var paths = allImages.Select(i => i.LocalPath).ToList();
            imageDict = ImageLiberator.ImagesFromFile(paths, this);


            //var otherImages = allImages.Except(catSubImages).ToList();
            //var otherImages = allImages;

            var catSubGroup = AddItemGroup("Картинки этой категории", catSubImages);
            //var otherGroup = AddItemGroup("Картинки каталога", otherImages);

            var entGroup = AddItemGroup("Выбранные картинки", ImagesHost.Images.ToList());

            PrepareGallery(galleryControl1);
            PrepareGallery(galleryEntityImages);

            galleryControl1.Gallery.CustomDrawItemImage += Gallery_CustomDrawItemImage;

            //galleryControl1.Gallery.Groups.AddRange(new[] { catSubGroup, otherGroup });
            galleryControl1.Gallery.Groups.AddRange(new[] { catSubGroup });

            galleryEntityImages.Gallery.Groups.Add(entGroup);

            InitDefaultImage(ImagesHost.DefaultImage);

            galleryControl1.Focus();
        }

        void Gallery_CustomDrawItemImage(object sender, GalleryItemCustomDrawEventArgs e)
        {
            //if (e.Item.Image != null) return;

            //var imagePath = e.Item.Value as string;

            //e.Item.Image = GetImage(imagePath);

        }

        private System.Drawing.Image GetImage(string imagePath)
        {
            System.Drawing.Image image = null;

            if (!imageDict.TryGetValue(imagePath, out image))
            {
                image = ImageLiberator.ImageFromFile(imagePath, this);
                imageDict.Add(imagePath, image);
            }
            return image;
        }



        private GalleryItemGroup AddItemGroup(string caption, List<Image> domImages)
        {
            var galleryGroup = new GalleryItemGroup() { };
            galleryGroup.Caption = caption;

            var paths = domImages.Select(i => i.LocalPath).ToList();

            foreach (var path in paths)
            {
                var galleryItem = new GalleryItem();
                galleryItem.Image = GetImage(path);

                galleryGroup.Items.Add(galleryItem);
            }

            return galleryGroup;
        }




        private void PrepareGallery(GalleryControl gc)
        {
            gc.Gallery.ItemImageLayout = ImageLayoutMode.ZoomInside;
            gc.Gallery.ImageSize = new Size(120, 90);
            gc.KeyUp += galleryControl_KeyUp;
            gc.Gallery.ItemClick += Gallery_ItemClick;
        }

        void Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            UpdateImage(e.Item.Image);
        }

        private List<T> ToList<T>(IList list)
        {
            var res = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                T castedItem = (T)item;
                res.Add(castedItem);
            }

            return res;
        }

        private void Save()
        {
            var oldImagePaths = ImagesHost.Images.Select(i => i.LocalPath).ToList();

            var galleryItemCollection = galleryEntityImages.Gallery.Groups[0].Items;

            var nowBinImages = ToList<GalleryItem>(galleryItemCollection).Select(i => i.Image).ToList();

            //var nowBinImages = PLinqHelpres.ToList(galleryItemCollection, typeof(GalleryItem)).Cast<GalleryItem>().Select(i => i.Image);

            var nowImagePathsQ = from i in nowBinImages
                                 let path = imageDict.FirstOrDefault(kvp => kvp.Value == i).Key
                                 select path;

            var nowImagePaths = nowImagePathsQ.ToList();

            var imagePathsToRemove = oldImagePaths.Except(nowImagePaths).ToList();
            var imagePathsToAdd = nowImagePaths.Except(oldImagePaths).ToList();
            string defImagePath = imageDict.KeyByValue(DefaultImage);


            foreach (string path in imagePathsToRemove)
            {
                FreeImageFile(path);

                ImagesHost.RemoveImage(path);
            }



            Image defImage = null;
            foreach (string path in imagePathsToAdd)
            {
                var image = ImagesHost.AddImage(path);
                newPathsDict.Add(path, image.LocalPath); // надо для функции изменения картинки в photoshop

                if (path == defImagePath) defImage = image;
            }

            if (defImage == null)
            {
                defImage = ImagesHost.Images.FirstOrDefault(i => i.LocalPath == defImagePath);
            }

            ImagesHost.DefaultImage = defImage;

            Context.Save();
        }

        /// <summary>
        /// пусть картинки перед добавлением - путь после добавления
        /// </summary>
        Dictionary<string, string> newPathsDict = new Dictionary<string, string>();


        private void FreeImageFile(string path)
        {
            var image = imageDict[path];

            RemoveImage(galleryControl1, image);
            imageDict.Remove(path);

            ImageLiberator.FreeImageFile(path);
        }

        private void FreeImagesFiles()
        {
            List<string> imagesPaths = imageDict.Keys.ToList();

            foreach (var path in imagesPaths)
            {
                FreeImageFile(path);
            }
        }



        private void SetImageForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Q:
                    galleryControl1.Focus();
                    break;
                case Keys.W:
                    galleryEntityImages.Focus();
                    break;
                case Keys.A:
                    LoadFormDiskDialog();
                    break;
                case Keys.S:
                    Save(); Exit();
                    break;
                case Keys.Escape:
                    Exit();
                    break;

            }
        }

        private void Exit()
        {
            DialogResult = DialogResult.OK;
        }




        void galleryControl_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Right:
                case Keys.Down:
                    UpdateImage(sender);
                    break;
                case Keys.Enter:
                    if (sender == galleryControl1) AddFocusedImage();
                    else if (sender == galleryEntityImages) SetFocusedPictureDefault();
                    break;
                case Keys.Delete:
                    if (sender == galleryEntityImages) DeleteFocusedPicture();
                    break;
                case Keys.F2:
                    if (sender == galleryEntityImages) EditImage();
                    break;
            }
        }

        private void EditImage()
        {
            Save();

            var focusedImage = GetFocusedImage(galleryEntityImages);
            if (focusedImage == null) return;

            //string defImagePath = imageDict.KeyByValue(DefaultImage);
            string imagePath = imageDict.KeyByValue(focusedImage);

            // если картинка была добавлена к сущности то она скопировалась в каталог картинок сущности, и
            // у неё изменился путь
            if (newPathsDict.ContainsKey(imagePath)) imagePath = newPathsDict[imagePath];

            ImageLiberator.FreeImageFile(focusedImage);
            var fileInfo = new FileInfo(imagePath);

            // этот путь надо добавить в окно настроек
            string photoshopPath = @"C:\Program Files\Adobe\Adobe Photoshop CS6 (x64)\Photoshop.exe";
            System.Diagnostics.Process.Start(photoshopPath, fileInfo.FullName);

            ImagesHost.ImageUpdated(imagePath);
            Context.Save();
            //Exit();
        }


        //private void InitChoseGallery(List<Image> domImages)
        //{
        //    var gc = galleryControl1;
        //    gc.Gallery.ItemImageLayout = ImageLayoutMode.ZoomInside;
        //    gc.Gallery.ImageSize = new Size(120, 90);
        //    //gc.Gallery.ShowItemText = true;

        //    galleryItemGroup = new GalleryItemGroup() { };
        //    galleryItemGroup.Caption = "Уже используемые";


        //    gc.Gallery.Groups.Add(galleryItemGroup);

        //    gc.KeyUp += galleryControl1_KeyUp;

        //    foreach (var i in domImages)
        //    {
        //        var bin = ImageLiberator.ImageFromFile(i.LocalPath);
        //        var galleryItem = new GalleryItem();
        //        galleryItem.Image = bin;


        //        galleryItemGroup.Items.Add(galleryItem);
        //    }
        //}





        Dictionary<string, System.Drawing.Image> imageDict = new Dictionary<string, System.Drawing.Image>();

        private void DeleteFocusedPicture()
        {
            var image = GetFocusedImage(galleryEntityImages);
            if (image == null) return;

            RemoveImage(galleryEntityImages, image);

        }

        private void RemoveImage(GalleryControl gc, System.Drawing.Image image)
        {
            var itemGroup = gc.Gallery.Groups[0];

            GalleryItem itemToDelete = GetItemByImage(itemGroup, image);

            if (itemToDelete != null)
            {
                itemGroup.Items.Remove(itemToDelete);
            }


            galleryEntityImages.Gallery.RefreshGallery();
        }

        private GalleryItem GetItemByImage(GalleryItemGroup itemGroup, System.Drawing.Image image)
        {
            GalleryItemCollection galleryItemCollection = itemGroup.Items;

            //var path = imageDict.KeyByValue(image);

            GalleryItem res = galleryItemCollection.Tolist<GalleryItem>()
                .FirstOrDefault(i => i.Image == image);

            return res;
        }

        public System.Drawing.Image DefaultImage { get; set; }

        private void InitDefaultImage(Image image)
        {
            if (image == null) return;
            var imageBin = GetImage(image.LocalPath);
            SetDefaultImage(imageBin);
            UpdateImage(imageBin);
        }

        private void SetFocusedPictureDefault()
        {
            var image = GetFocusedImage(galleryEntityImages);
            if (image == null) return;

            SetDefaultImage(image);
        }

        private void SetDefaultImage(System.Drawing.Image image)
        {
            if (DefaultImage == image) return;

            var itemGroup = galleryEntityImages.Gallery.Groups[0];

            if (DefaultImage != null)
            {
                GetItemByImage(itemGroup, DefaultImage).Caption = string.Empty;
            }

            DefaultImage = image;

            GalleryItem defItem = GetItemByImage(itemGroup, image);
            defItem.Caption = "Главная";

            galleryEntityImages.Gallery.RefreshGallery();
        }

        private void AddFocusedImage()
        {
            var image = GetFocusedImage(galleryControl1);
            if (image == null) return;

            var itemGroup = galleryEntityImages.Gallery.Groups[0];
            bool isRepeat = GetItemByImage(itemGroup, image) != null;

            if (isRepeat) return;

            if (ImagesHost is ShopCategory)
            {
                galleryEntityImages.Gallery.Groups[0].Items.Clear();
                galleryEntityImages.Gallery.Groups[0].Items.Add(new GalleryItem() { Image = image });
                SetDefaultImage(image);
            }
            else
            {
                galleryEntityImages.Gallery.Groups[0].Items.Add(new GalleryItem() { Image = image });
            }


        }

        private void UpdateImage(System.Drawing.Image image)
        {
            if (image == null) return;
            pictureEdit1.Image = image;
        }

        private void UpdateImage(object sender)
        {
            var image = GetFocusedImage(sender);
            if (image == null) return;
            pictureEdit1.Image = image;
        }

        private System.Drawing.Image GetFocusedImage(object sender)
        {
            var gc = (sender as GalleryControl);
            var selectedItem = gc.Gallery.GetViewInfo().KeyboardSelectedItem;
            if (selectedItem == null) return null;
            System.Drawing.Image image = selectedItem.Item.Image;
            return image;
        }


        private void galleryControlGallery1_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            //pictureEdit1.Image = e.Item.Image;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            LoadFormDiskDialog();
        }

        private void LoadFormDiskDialog()
        {
            bool cancel = openFileDialog1.ShowDialog() == DialogResult.Cancel;
            if (cancel) return;

            LoadFromDisk(openFileDialog1.FileNames);
        }

        private GalleryItemGroup groupFromDisk;

        private void LoadFromDisk(string[] fileNames)
        {
            if (groupFromDisk == null)
            {
                groupFromDisk = new GalleryItemGroup() { };
                groupFromDisk.Caption = "С диска";
                galleryControl1.Gallery.Groups.Add(groupFromDisk);
            }


            //Image.LoadFromDisk(fileNames);
            foreach (var fileName in fileNames)
            {
                var bin = ImageLiberator.ImageFromFile(fileName, this);

                groupFromDisk.Items.Add(new GalleryItem() { Image = bin });
            }

            galleryControl1.Gallery.RefreshGallery();

        }

        private void SetImageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //FreeImagesFiles();
        }


    }

    internal class ImageRow
    {
        public Image Dom { get; set; }
        public System.Drawing.Image Bin { get; set; }
    }

}
