using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToyShopDataLib;

namespace TestLotCreatorWin
{
    public partial class ImportSaleForm : Form
    {
        public ImportSaleForm()
        {
            InitializeComponent();
        }

        private BegemotSaleImporter saleImporter;

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            var dialog = openFileDialog1.ShowDialog();

            if (dialog != DialogResult.OK && openFileDialog1.CheckFileExists) return;

            saleImporter = new BegemotSaleImporter();
            saleImporter.LoadFile(openFileDialog1.FileNames[0]);

            var data =
                from s in saleImporter.Data
                let p = Context.Inst.BegemotProductSet.FirstOrDefault(p => s.Article == p.Article)


                let res =

                new
                {
                    s.Article,
                    Title = p != null ? p.Title : "",
                    Count = p != null ? p.Count : 0,
                    RetailPrice = s.RetailPriceOld,
                    WholeSalePrice = s.WholeSalePriceOld,
                    SaleRetPrice = s.RetailPrice,
                    SaleWholePrice = s.WholeSalePrice,
                    DifRet = s.RetailPriceOld - s.RetailPrice,
                    DifWhole = s.RetailPriceOld - s.WholeSalePrice
                }

                select res;

            gridControl1.DataSource = data.ToList();

            //gridControl1.DataSource = saleImporter.Data;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bool dateNotSetted =
                saleImporter == null ||
                saleImporter.Data == null ||
                txtSart.IsModified ||
                txtStop.IsModified ||
                string.IsNullOrWhiteSpace(txtDescription.Text);

            if (dateNotSetted)
            {
                MessageBox.Show("Указанны не все данные");
                return;
            }

            saleImporter.ImportSale(txtDescription.Text, txtSart.DateTime, txtStop.DateTime);

            MessageBox.Show("Импорт завершен успешно");
            Close();
        }


    }
}
