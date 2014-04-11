using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon;
using ShopDataLib;
using Utils;
using Image = ShopDataLib.Image;

namespace PrestaWinClient
{
    public partial class EditShopProductForm : Form
    {
        public EditShopProductForm()
        {
            InitializeComponent();
        }

        public static void EditProduct(ShopProduct product)
        {
            var form = new EditShopProductForm();

            form.Init(product);
            form.ShowDialog();
        }

        private void Init(ShopProduct product)
        {
            Cursor = Cursors.WaitCursor;

            Product = product;
            LoadProduct();

            Cursor = Cursors.Default;

        }

        public ShopProduct Product { get; set; }

        private void Save()
        {
            Product.Title = txtTitle.Text;
            Product.Price = txtPrice.Value;
            Product.DiscountPrice = txtDiscountPrice.Value;
            Product.IsSale = txtIsSale.Checked;
            Product.ShortDescription = txtShortDescription.HtmlText;
            Product.Description = txtDescription.HtmlText;
            Product.InShop = txtInShop.Checked;
            Product.Unity = txtUnit.Text;
            txtLinkRewrite.Text = Product.LinkRewrite;
            Product.Quantity = (int)txtQuantity.Value;
        }


        private void LoadProduct()
        {
            Text = Product.Title;

            //Название
            //Цена
            //Цена со скидкой
            //Распродажа
            //Описание
            //Краткое описание
            //В продаже
            //Единица измерения (шт/кг/л)
            //ЧПУ
            //Колво на складе


            txtTitle.Text = Product.Title;
            txtPrice.Value = Product.Price;

            txtDiscountPrice.Value = Product.DiscountPrice;
            txtIsSale.Checked = Product.IsSale;
            txtShortDescription.HtmlText = Product.ShortDescription;
            txtDescription.HtmlText = Product.Description;
            txtInShop.Checked = Product.InShop;
            txtUnit.Text = Product.Unity;
            txtLinkRewrite.Text = Product.LinkRewrite;
            txtQuantity.Value = Product.Quantity ?? 0;



            PrepareGallery(galleryControl1);

            RefreshGallery();
            
        }

        private void RefreshGallery()
        {
            var itemGroup = AddItemGroup("Картинки", Product.Images.ToList());
            galleryControl1.Gallery.Groups.Clear();
            galleryControl1.Gallery.Groups.Add(itemGroup);
        }

        private GalleryItemGroup AddItemGroup(string caption, List<Image> domImages)
        {
            var galleryGroup = new GalleryItemGroup() { };
            galleryGroup.Caption = caption;

            var paths = domImages.Select(i => i.LocalPath).ToList();

            foreach (var path in paths)
            {
                var galleryItem = new GalleryItem();
                galleryItem.Image = ImageLiberator.ImageFromFile(path, this);

                galleryGroup.Items.Add(galleryItem);
            }

            return galleryGroup;
        }




        private void PrepareGallery(GalleryControl gc)
        {
            gc.Gallery.ItemImageLayout = ImageLayoutMode.ZoomInside;
            gc.Gallery.ImageSize = new Size(120, 90);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void Exit()
        {

            DialogResult = DialogResult.OK;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnImages_Click(object sender, EventArgs e)
        {
            SetImageForm.EditImages(Product);
            RefreshGallery();
        }


    }
}
