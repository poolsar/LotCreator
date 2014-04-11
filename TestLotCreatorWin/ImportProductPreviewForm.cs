using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ToyShopDataLib;

namespace TestLotCreatorWin
{
    public partial class ImportProductPreviewForm : Form
    {
        public ImportProductPreviewForm()
        {
            InitializeComponent();
        }

        public void ShowProduct(string article)
        {
            var product = Context.Inst.BegemotProductSet.FirstOrDefault(p => p.Article == article);
            if (product == null) return;

            InitDescription(product);

            InitImage(product);
            
            ShowDialog();
        }

        private void InitDescription(BegemotProduct product)
        {
            var description = product.GetDescription();
            txtDescription.Text = description;
        }

        private void InitImage(BegemotProduct product)
        {
            if (product.HasImage())
            {
                pictureEdit1.Image = new Bitmap(product.ImagePath);
            }
            else
            {
                pictureEdit1.Image = null;
            }
        }
    }
}
