using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Views;
using ShopDataLib;
using Utils;

namespace PrestaWinClient
{
    public partial class ShopProductCard : Form
    {


        public static void EditProduct(ShopProduct product)
        {

            EditShopProductForm.EditProduct(product);

            //var form = new ShopProductCard();

            //form.Init(product);
            //form.ShowDialog();
        }

        public ShopProduct Product { get; set; }


        private void Init(ShopProduct product)
        {
            Cursor = Cursors.WaitCursor;

            Product = product;
            LoadProduct();
        }

        private void LoadProduct()
        {
            Text = Product.Title;
            txtTitle.Text = Product.Title;
            txtPrice.Value = Product.Price;
            txtDescription.HtmlText = Product.Description;

            swchIsOn.IsOn = Product.InShop;

            imageSlider1.Images.Clear();
            var images = Product.Images.ToList();
            foreach (var image in images)
            {
                var imagefromFile = ImageLiberator.ImageFromFile(image.LocalPath, this);
                imageSlider1.Images.Add(imagefromFile);
            }

        }


        
        private void SaveProduct()
        {
            Cursor = Cursors.WaitCursor;

            Text = txtTitle.Text;
            Product.Title = txtTitle.Text;
            Product.Price = txtPrice.Value;
            Product.Description = txtDescription.HtmlText;

            Product.InShop = swchIsOn.IsOn;
                
            //imageSlider1.Images.Clear();
            //var images = Product.Image.ToList();
            //foreach (var image in images)
            //{
            //    var imagefromFile = ImageLiberator.ImageFromFile(image.LocalPath);
            //    imageSlider1.Images.Add(imagefromFile);
            //}

            Context.Inst.SaveChanges();

            Cursor = Cursors.Default;
        }

        public ShopProductCard()
        {
            InitializeComponent();
        }

        private void ShopProductCard_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void ShopProductCard_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape: Exit();
                    break;
                case Keys.S: if (e.Control) SaveProduct();
                    break;
            }
        }

        private void Exit()
        {
            DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveProduct();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Exit();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode== Keys.S)
            {
                return;
            }

            base.OnKeyUp(e);
        }

    }
}
