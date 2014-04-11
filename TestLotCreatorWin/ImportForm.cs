using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using DevExpress.Utils.FormShadow;
using DevExpress.XtraGrid.Columns;
using ImageMakerWpf;
using ToyShopDataLib;

namespace TestLotCreatorWin
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            backgroundWorker1.RunWorkerAsync();


        }

        private BegemotImporter begemotImporter;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BegemotImporter begemotImporter = new BegemotImporter();
            begemotImporter.Import();

        }


        private static string progmessage;
        void UpdateProgress(string message)
        {
            progmessage = message;
            backgroundWorker1.ReportProgress(1);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Text = progmessage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshData();
            
            button1.Enabled = true;
            Text = "Импорт завершен";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ImportSaleForm().ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData(List<string> articles = null)
        {
            var data =
                from p in Context.Inst.BegemotProductSet
                let s = Context.Inst.BegemotSalePriceSet.FirstOrDefault(
                            s => s.Article == p.Article && s.BegemotSale.Active)
                let inCatalog = Context.Inst.ProductSet.Any(sh => sh.Article == p.Article && sh.Active)
                let RetailPrice = s != null ? s.RetailPriceOld : p.RetailPrice
                let WholeSalePrice = s != null ? s.WholeSalePriceOld : p.WholeSalePrice
                let SaleRetPrice = s != null ? s.RetailPrice : new decimal()
                let SaleWholePrice = s != null ? s.WholeSalePrice : new decimal()
                let IsSale = s != null
                select new DataSourceRow
                {
                    Group = p.Group,
                    Group1 = p.Group1,
                    Group2 = p.Group2,
                    Brand = p.Brand,
                    Article = p.Article,
                    Title = p.Title,
                    Count = p.Count,
                    RetailPrice = RetailPrice,
                    WholeSalePrice = WholeSalePrice,
                    SaleRetPrice = SaleRetPrice,
                    SaleWholePrice = SaleWholePrice,
                    IsSale = IsSale,
                    InCatalog = inCatalog,
                    OptPrice = WholeSalePrice * 1.15m
                };
            
            var rows = data.ToList();

            foreach (var row in rows)
            {
                var art = string.Concat(row.Article.Reverse());
                var artInt = Convert.ToInt32(art) + 789;
                row.MyArticle = artInt.ToString();
            }

            if (articles!=null)
            {
                rows = 
                    (   from r in rows
                        from a in articles
                        where r.Article == a
                        select r
                    ).ToList();
            }

            DataSourceRows = rows;


            for (int i = 0; i < 5; i++)
            {
                var column = gridView1.Columns[i];
                column.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;

                if (column.Caption == "Group1")
                {
                    column.Caption = "Категория";
                }
                else if (column.Caption == "Group2")
                {
                    column.Caption = "Подкатегория";
                }
            }
        }

        List<DataSourceRow> DataSourceRows
        {
            get
            {
                return gridControl1.DataSource as List<DataSourceRow>;
            }
            set
            {
                gridControl1.DataSource = value;
            }
        }


        private class DataSourceRow
        {
            public string Group { get; set; }
            public string Group1 { get; set; }
            public string Group2 { get; set; }
            public string Article { get; set; }
            public string Title { get; set; }
            public int Count { get; set; }
            public decimal RetailPrice { get; set; }
            public decimal WholeSalePrice { get; set; }
            public decimal SaleRetPrice { get; set; }
            public decimal SaleWholePrice { get; set; }
            public bool IsSale { get; set; }
            public bool InCatalog { get; set; }
            public string Brand { get; set; }
            public decimal OptPrice { get; set; }
            public string MyArticle { get; set; }
        }

        private void gridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;

            DataSourceRow row = (DataSourceRow)gridView1.GetFocusedRow();

            if (row == null) return;

            var form = new ImportProductPreviewForm();
            form.ShowProduct(row.Article);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ProccessMesenger.AttachForm(this);
            RefreshData();
        }

        private CatalogForm catalogForm;
        private List<DataSourceRow> _dataSourceRows;

        private void btnCatalog_Click(object sender, EventArgs e)
        {
            ////if (catalogForm == null)
            //{
            //    catalogForm = new CatalogForm();
            //}

            //catalogForm.Show();
        }

        private List<string> selectArticles;
        private void btnSaveToCatalog_Click(object sender, EventArgs e)
        {
            selectArticles = DataSourceRows.Where(r => r.InCatalog).Select(p => p.Article).ToList();
            bgwSaveToCatalog.RunWorkerAsync();

        }



        private void bgwSaveToCatalog_DoWork(object sender, DoWorkEventArgs e)
        {
            Product.UpdateProductActivation(selectArticles, ProgressReport);

        }

        private string reportMessage;
        private void ProgressReport(string message)
        {
            reportMessage = message;
            bgwSaveToCatalog.ReportProgress(0);
        }

        private void bgwSaveToCatalog_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Text = reportMessage;
        }

        private void bgwSaveToCatalog_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (catalogForm != null)
            {
                catalogForm.RefreshData();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnChooseSelected_Click(object sender, EventArgs e)
        {
            var selectedRows = gridView1.GetSelectedRows().Select(h => gridView1.GetRow(h) as DataSourceRow).ToList();

            selectedRows.ForEach(r => r.InCatalog = true);
            gridView1.RefreshData();
        }

        private void btnFilterByArticles_Click(object sender, EventArgs e)
        {
            FilterByArticles();
        }

        private void FilterByArticles()
        {
            var articles = txtArticles.Text.Split(new string[] {",", " "}, StringSplitOptions.RemoveEmptyEntries).ToList();
            RefreshData(articles);

            var noFindedArticles = articles.Except(DataSourceRows.Select(r => r.Article)).ToList();

            txtNoFindedArticles.Text = string.Join(" ", noFindedArticles);
        }

        private void txtArticles_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.F5) return;

            FilterByArticles();
        
        }
    }
}
