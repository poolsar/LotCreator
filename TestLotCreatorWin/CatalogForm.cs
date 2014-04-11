using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting.Native;
using RestSharp.Contrib;
using ToyShopDataLib;
using ToyShopDataLib.AdvExport;

namespace TestLotCreatorWin
{
    public partial class CatalogForm : Form
    {
        public CatalogForm()
        {
            InitializeComponent();

            pictureEdit1.Image = null;

            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
        }

        private void ShowCaseForm_Load(object sender, EventArgs e)
        {
            ProccessMesenger.AttachForm(this);
            RefreshData();
            
        }

        private void gridView1_FocusedRowChanged(object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = (CatalogFormRow)gridView1.GetFocusedRow();
            if (row == null) return;

            UpdateFocused(row);
        }

        private void btnSpecialDescription_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("Не выброна ни одна строка");
                return;
            }

            Style style = StylesDictForm.ChooseStyle();

            if (style == null) return;

            var rows = GetSelectedRows();

            foreach (var row in rows)
            {
                var product = row.Product;
                var bproduct = product.BegemotProduct;

                style.Apply(product, bproduct);
            }

            //ReApplyStyles(rows);

            Context.Save();

            RefreshData();

        }

        private void btnRescrapeInfoFromSite_Click(object sender, EventArgs e)
        {
            var rows = GetSelectedRows();
            if (rows == null) return;

            var count = rows.Count;
            int counter = 0;

            foreach (var row in rows)
            {
                Text = string.Format("Обновление {0} {1} из {2}", row.Title, ++counter, count);
                Refresh();

                var product = row.Product;
                var bproduct = product.BegemotProduct;

                bproduct.ScrapeInfoFromSite();
                product.ReApplyStyle();

                gridView1.RefreshData();
            }

            Context.Save();


            Text = string.Format("Обновление завершено");
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var importForm = new ImportForm();
            importForm.ShowDialog();

            RefreshData();

        }

        private void btnAssignSale_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("Не выброна ни одна строка");
                return;
            }

            Sale sale;
            bool success = SaleDict.ChooseSale(out sale);
            if (!success) return;

            var rows = GetSelectedRows();

            foreach (var row in rows)
            {
                var product = row.Product;

                if (sale == null)
                {
                    product.ResetActiveSales();
                }
                else
                {
                    var bproduct = product.BegemotProduct;

                    var bsale = bproduct.GetActiveSale();

                    sale.Apply(product, bproduct, bsale);
                }
            }
            Context.Save();

            RefreshData();
        }

        private void btnMarketplaceDict_Click(object sender, EventArgs e)
        {
            new MarketplaceDict().ShowDialog();
        }

        private void btnSalesDict_Click(object sender, EventArgs e)
        {
            new SaleDict().ShowDialog();
        }

        private void btnStylesDict_Click(object sender, EventArgs e)
        {
            new StylesDictForm().ShowDialog();
        }

        private void btnRefreshAll_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("Не выброна ни одна строка");
                return;
            }

            var rows = GetSelectedRows();


            Text = "Обновление цен";
            Refresh();

            RecalcPrices(rows);
            Context.Save();


            Text = "Обновление стилей";
            Refresh();

            ReApplyStyles(rows);
            Context.Save();

            Text = "Обновление картинок";
            Refresh();
            RefreshData();
            Refresh();

            UpdateImages(rows);
            Context.Save();

            RefreshData();

            Text = "Обновление успешно завершено";
        }

        private void gridControl1_DragDrop(object sender, DragEventArgs e)
        {
            string url = (string)e.Data.GetData(typeof(string));
            var uri = new Uri(url);
            var begemotProductCode = uri.LocalPath.Replace("/tov_", "").Replace(".html", "");
            var begemotProduct = Context.Inst.BegemotProductSet.FirstOrDefault(b => b.Code == begemotProductCode);

            if (begemotProduct == null)
            {
                MessageBox.Show("Товар не зарегестирован. Попробуйте обновить Базу Данных.");
                return;

            }

            var product = Product.FromBegemotProduct(begemotProduct);
            Context.Inst.ProductSet.Add(product);
            Context.Save();
            RefreshData();
        }

        private void gridControl1_DragOver(object sender, DragEventArgs e)
        {
            // для перенесенных ссылок из браузера
            string url = (string)e.Data.GetData(typeof(string));

            if (string.IsNullOrWhiteSpace(url) || !url.Contains("begemott.ru"))
            {
                e.Effect = DragDropEffects.None;
                //Cursor = Cursors.No;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
                //Cursor = Cursors.;
            }
        }

        private void pictureEdit1_DragOver(object sender, DragEventArgs e)
        {
            // для перенесенных ссылок из браузера
            string url = (string)e.Data.GetData(typeof(string));

            if (url.Contains(".jpg") || url.Contains(".jpeg"))
            {
                e.Effect = DragDropEffects.Copy;
                //Cursor = Cursors.No;
            }
            else
            {
                e.Effect = DragDropEffects.None;

                //Cursor = Cursors.;
            }
        }

        private void pictureEdit1_DragDrop(object sender, DragEventArgs e)
        {
            string url = (string)e.Data.GetData(typeof(string));
            var uri = new Uri(url);

            var row = (CatalogFormRow)gridView1.GetFocusedRow();
            if (row == null) return;

            FreeFocusedImage();

            var product = row.Product;
            var bproduct = product.BegemotProduct;
            //var hasImage = bproduct.HasImage();
            //if (hasImage) return;

            var image = bproduct.DownloadImage(uri);
            Context.Save();

            pictureEdit1.Image = new Bitmap(image);

            Text = "Картинка загружена";
        }

        private void btnFindImage_Click(object sender, EventArgs e)
        {
            var row = (CatalogFormRow)gridView1.GetFocusedRow();
            if (row == null) return;

            var product = row.Product;
            var bproduct = product.BegemotProduct;

            string query = HttpUtility.UrlEncode(bproduct.GetClearTitle());

            var searchUrl = string.Format("https://www.google.ru/search?q={0}&tbm=isch", query);

            Process.Start(searchUrl);
        }

        private void btnChangePricePosition_Click(object sender, EventArgs e)
        {
            var row = (CatalogFormRow)gridView1.GetFocusedRow();
            if (row == null) return;

            var product = row.Product;
            product.PricePosition = PricePosition.DownRight;

            Context.Save();
            UpdateImages(new List<CatalogFormRow> { row });





        }

        private void btnClearUnreadMsg_Click(object sender, EventArgs e)
        {
            Text = "Читаем непрочитанные сообщения";
            Refresh();
            var exporter24Au = new AdvExporter24Au();
            exporter24Au.ClearUnreadMsg();
            Text = "Непрочитанные сообщения успешно прочитанны";
        }

        private void btnPrepareImagesVKontakte_Click(object sender, EventArgs e)
        {
            //var rows = GetSelectedRows();
            //if (rows == null) return;

            //var products = rows.Select(r => r.GetProduct()).ToList();

            //var exporterVKontakte = new AdvExporterVKontakte();
            //exporterVKontakte.Sync(products);

            //Text = "Картинки для контакта успешно подготовлены";
        }

        private void btnUnpost_Click(object sender, EventArgs e)
        {
            var rows = GetSelectedRows();
            if (rows == null) return;

            foreach (var row in rows)
            {
                var product = row.Product;
                product.Advs.ForEach(a => a.Active = false);
            }

            Context.Save();

            Text = "Снятие объявлений завершено";

            RefreshData(rows);

        }



        private void btnTest_Click(object sender, EventArgs e)
        {
            Text = string.Format("Тест пройден {0}", DateTime.Now);
        }

        private void btnCreateImages_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("Не выброна ни одна строка");
                return;
            }

            var rows = GetSelectedRows();
            UpdateImages(rows);
        }

        private void btnSyncAdvs_Click(object sender, EventArgs e)
        {
            var rows = GetSelectedRows();
            var products = rows.Select(r => r.Product).ToList();

            var form = new SyncAdvDialog();
            var syncInfo = form.SyncDialog(products);

            if (syncInfo.Cancel) return;

            RunInBackGroung(() =>
            {
                foreach (var marketplace in syncInfo.Marketplaces)
                {
                    marketplace.Sync(syncInfo.Mode, products);
                }
            });
        }


        public List<CatalogFormRow> DataSource
        {
            get
            {
                return gridControl1.DataSource as List<CatalogFormRow>;
            }
            set
            {
                gridControl1.DataSource = value;
                gridControl1.RefreshDataSource();
            }
        }



        private void UpdateFocused(CatalogFormRow row)
        {
            var product = row.Product;

            SetImage(row);
            txtDescription.Text = product.Description;
        }

        private void SetImage(CatalogFormRow row)
        {
            var product = row.Product;
            var bproduct = product.BegemotProduct;

            pictureEdit1.Image = product.GetImage(bproduct);
        }

        private static void ReApplyStyles(List<CatalogFormRow> rows)
        {
            foreach (var row in rows)
            {
                var product = row.Product;
                product.ReApplyStyle();
            }
        }

        private static void RecalcPrices(List<CatalogFormRow> rows)
        {
            foreach (var row in rows)
            {
                var product = row.Product;
                var bproduct = product.BegemotProduct;

                var bsale = bproduct.GetActiveSale();

                product.RecalcPrice(bproduct, bsale);
            }
        }

        private void FreeFocusedImage()
        {
            var row = (CatalogFormRow)gridView1.GetFocusedRow();
            if (row == null) return;

            pictureEdit1.Image = null;
            row.Product.FreeImage();
        }

        private List<CatalogFormRow> GetSelectedRows()
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("Не выброна ни одна строка");
                return null;
            }

            var selectedRows = gridView1.GetSelectedRows().Select(h => gridView1.GetRow(h) as CatalogFormRow).ToList();
            return selectedRows;
        }

        private void UpdateImages(List<CatalogFormRow> rows)
        {
            var products = rows.Select(r => r.Product).ToList();
            ProductImage.SyncImages(products);


            // старая версия

            //pictureEdit1.Image = null;
            //pictureEdit1.Refresh();

            //rows.ForEach(r => r.GetProduct().FreeImage());

            //var showCaseImage = new ShowCaseImage(ShowCaseStyle.Standart);

            //foreach (var row in rows)
            //{
            //    var product = row.Product;
            //    var bproduct = row.GetBegemotProduct();

            //    if (!bproduct.IsNoImage())
            //    {
            //        showCaseImage.AddTask(product.Title, product.Price, product.PriceOld,
            //            bproduct.GetImagePath(),
            //            product.GetImagePath(),
            //            product.PricePosition == PricePosition.TopRight

            //            );
            //    }
            //}

            //showCaseImage.Execute();
        }


        private void RunInBackGroung(Action action, string comment = null)
        {
            if (backgroundWorker1.IsBusy)
            {
                Text = "backgroundWorker занят";
                return;
            }

            backgroudAction = action;

            MyEnable = false;

            backgroudComment = comment;
            backgroundWorker1.RunWorkerAsync();
        }

        private bool MyEnable
        {
            set
            {
                foreach (var control in Controls)
                {
                    if (control is Button) (control as Button).Enabled = value;
                }

                menuStrip1.Enabled = value;
                gridControl1.Enabled = value;

                pictureEdit1.Properties.ReadOnly = !value;
                txtDescription.ReadOnly = !value;
            }
        }

        private Action backgroudAction;
        private string backgroudComment;

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!backgroudComment.IsEmpty())
            {
                ProccessMesenger.Write(backgroudComment);
            }

            backgroudAction();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroudAction = null;

            MyEnable = true;

            if (!backgroudComment.IsEmpty())
            {
                ProccessMesenger.Write(backgroudComment + " Выполнено");
            }
        }

        //public void RefreshData2()
        //{
        //    DataSource = (from p in Context.Inst.ProductSet
        //                  from b in Context.Inst.BegemotProductSet
        //                  let s =
        //                      Context.Inst.BegemotSalePriceSet.FirstOrDefault(s => s.Article == p.Article && s.BegemotSale.Active)
        //                  where p.Active && p.Article == b.Article
        //                  select new { p, b, s }).ToList().Select(p => new CatalogFormRow(p.p, p.b, p.s)).ToList();

        //    gridView1.Columns[0].OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
        //}

        public void RefreshData(params CatalogFormRow[] rows)
        {
            RefreshData(rows.ToList());
        }

        public void RefreshData(List<CatalogFormRow> rows)
        {
            RunInBackGroung(() => RefreshDataGrid(rows), "Обновление грида");
        }

        public void RefreshDataGrid(List<CatalogFormRow> rows)
        {
            var now = DateTime.Now;
            var zeroTime = new TimeSpan();

            IQueryable<int> needIds = (new int[] { }).AsQueryable();
            bool filterProducts = rows != null && rows.Any();
            if (filterProducts)
            {
                needIds = rows.Select(r => r.Product.Id).ToArray().AsQueryable();
            }

            var q = (
                from p in Context.Inst.ProductSet
                //from id in needIds
                //where !filterProducts || p.Id == id

                let b = p.BegemotProduct
                let st = p.SpecialStyle.FirstOrDefault(st => st.Active)
                let s = p.Sales.FirstOrDefault(s => s.Active && s.DateStart <= now && now <= s.DateExpire)
                let a = p.Advs.FirstOrDefault(a => a.Marketplace.Url == AdvExporter24Au.URL)
                let hasImage = p.Images.Any(i => !i.IsNoImage)
                let sa = b.BegemotSalePrice.FirstOrDefault(sa =>
                    sa.BegemotSale.Active && sa.BegemotSale.DateStart <= now && now < sa.BegemotSale.DateStop)

                let selfPrice = sa != null ? sa.WholeSalePrice : b.WholeSalePrice * Context.BegemotDescountCoef
                let margin = p.Price - selfPrice
                let marginPrec = margin / selfPrice * 100m

                let descount = (int)(p.PriceOld == 0 ? p.Price : (1 - p.Price / p.PriceOld) * 100)

                let style = st != null ? st.Name : string.Empty
                let sale = s != null ? s.Title : string.Empty

                let posted = a != null
                let advNumber = posted ? a.Number : 0

                let expired = posted && a.DateExpire <= now
                //let beforeExpired = !posted || expired ? (now - now) : (a.DateExpire - now)

                select new CatalogFormRow
                {
                    Title = p.Title,
                    Brand = b.Brand,
                    Style = style,
                    Price = p.Price,
                    PriceOld = p.PriceOld,
                    Article = p.Article,
                    Margin = margin,
                    Descount = descount,
                    LotNumber = advNumber,
                    IsPosted = posted,
                    IsExpired = expired,
                    //BreforeExpired = beforeExpired,
                    Group1 = p.BegemotProduct.Group1,
                    Group2 = p.BegemotProduct.Group1,
                    Count = p.BegemotProduct.Count,
                    Sale = sale,
                    HasImage = hasImage,
                    Product = p,
                    Adv = a
                });

            var catalogFormRows = q.ToList();

            Action refreshComplete = () =>
            {

                if (!filterProducts)
                {
                    bool firstInit = DataSource == null;

                    DataSource = catalogFormRows;

                    if (firstInit) PrepareGrid();

                    gridView1.RefreshData();
                }
                else
                {
                    bool useCommonRefresh = rows.Count > 50;
                    foreach (var row in rows)
                    {
                        int index = DataSource.IndexOf(row);
                        DataSource[index] = catalogFormRows[0];

                        if (useCommonRefresh) continue;

                        int handle = gridView1.GetRowHandle(index);
                        gridView1.RefreshRow(handle);
                    }

                    if (useCommonRefresh) gridView1.RefreshData();
                }



            };

            BeginInvoke(refreshComplete);
        }

        private void PrepareGrid()
        {
            gridView1.Columns[0].OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;

            string[] hideColumns = CatalogFormRow.HideColumns;

            foreach (var hideColumn in hideColumns)
            {
                var prodCol = gridView1.Columns.ColumnByFieldName(hideColumn);
                if (prodCol != null)
                {
                    prodCol.Visible = false;
                }
            }

            GridViewSaver.Load(this, gridView1);
        
        }

        private void CatalogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GridViewSaver.Save(this, gridView1);
        }
    }

    class GridViewSaver
    {
        public const string SaveFolder = "GridViewsSaves";

        public static void Load(Form form, GridView gridView)
        {
            var path = GetPath(form, gridView);

            var fileExists = File.Exists(path);
            if (!fileExists) return;

            var saveText = File.ReadAllText(path);

            var visibleColumns = saveText.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (GridColumn column in gridView.Columns)
            {
                column.Visible = visibleColumns.Contains(column.FieldName);
            }

            //gridView.RestoreLayoutFromXml(path);

            //using (var fileStream = File.OpenRead(path))
            //{
            //    gridView.RestoreLayoutFromStream(fileStream);
            //}

        }

        public static void Save(Form form, GridView gridView)
        {
            var path = GetPath(form, gridView);
            //gridView.SaveLayoutToXml(path);

            var sb = new StringBuilder();
            foreach (GridColumn column in gridView.Columns)
            {
                if (column.Visible) sb.AppendFormat("{0};", column.FieldName);
            }
            var saveText = sb.ToString();
            File.WriteAllText(path, saveText);

            //using (var fileStream = File.OpenWrite(path))
            //{
            //    gridView.SaveLayoutToStream(fileStream);    
            //}
        }


        private static string GetPath(Form form, GridView gridView)
        {
            var dirExists = Directory.Exists(SaveFolder);
            if (!dirExists) Directory.CreateDirectory(SaveFolder);

            var path = string.Format("{0}/{1}.{2}.txt", SaveFolder, form.Name, gridView.Name);
            return path;
        }
    }

    public class CatalogFormRow
    {
        public static string[] HideColumns = { "Product", "Adv" };

        public string Title { get; set; }

        public string Brand { get; set; }

        public string Style { get; set; }

        public decimal Price { get; set; }

        public decimal PriceOld { get; set; }

        public string Article { get; set; }

        public decimal Margin { get; set; }

        public int Descount { get; set; }

        public int LotNumber { get; set; }

        public bool IsPosted { get; set; }

        public bool IsExpired { get; set; }

        //public string BreforeExpired { get; set; }

        private string _breforeExpired;

        public string BreforeExpired
        {
            get
            {
                var now = DateTime.Now;

                _breforeExpired = !IsPosted || IsExpired ? string.Empty :

                    (Adv.DateExpire - now).ToString(@"hh\:mm");//\:ss");

                return _breforeExpired;
            }
        }

        public string Group1 { get; set; }

        public string Group2 { get; set; }

        public int Count { get; set; }

        public string Sale { get; set; }

        public bool HasImage { get; set; }

        public Product Product { get; set; }

        public Adv24au Adv { get; set; }
    }



    //public class CatalogFormRowOld
    //{
    //    private readonly Product _product;
    //    private readonly BegemotProduct _begemotProduct;
    //    private readonly BegemotSalePrice _begemotSalePrice;

    //    public string Title
    //    {
    //        get
    //        {
    //            return _product.Title;
    //        }
    //    }

    //    public string Brand
    //    {
    //        get
    //        {
    //            return _begemotProduct.Brand;
    //        }
    //    }

    //    private string _style;

    //    public string Style
    //    {
    //        get
    //        {
    //            UpdateAdvInfo();
    //            return _style;
    //        }
    //    }

    //    public decimal Price
    //    {
    //        get
    //        {
    //            return _product.Price;
    //        }
    //    }

    //    public decimal PriceOld
    //    {
    //        get
    //        {
    //            return _product.PriceOld;
    //        }
    //    }

    //    public string Article
    //    {
    //        get
    //        {
    //            return _product.Article;
    //        }
    //    }

    //    public decimal Margin { get; private set; }

    //    public int Descount
    //    {
    //        get
    //        {
    //            decimal result = 0;
    //            if (_product.PriceOld != 0)
    //            {
    //                result = (1 - _product.Price / _product.PriceOld);
    //            }

    //            return (int)(result * 100);
    //        }
    //    }

    //    private string _lotNumber;

    //    public string LotNumber
    //    {
    //        get
    //        {
    //            if (string.IsNullOrWhiteSpace(_lotNumber))
    //            {
    //                if (!IsPosted) return string.Empty;

    //                var adv24Au = _product.Advs.FirstOrDefault();
    //                if (adv24Au == null) return string.Empty;

    //                _lotNumber = adv24Au.Number.ToString();
    //            }
    //            return _lotNumber;
    //        }
    //    }

    //    private bool _posted = false;

    //    private bool _isExpired;

    //    private string _beforeExpired;

    //    private DateTime _lastPostUpdate;

    //    private Adv24au _adv24Au;

    //    private bool _upsateInfo = true;

    //    public void Refresh()
    //    {
    //        _upsateInfo = true;
    //        UpdateAdvInfo();
    //    }

    //    private void UpdateAdvInfo()
    //    {
    //        if (!_upsateInfo) return;

    //        _upsateInfo = false;

    //        if (_adv24Au == null)
    //            _adv24Au = _product.Advs.FirstOrDefault(a => a.Marketplace.Title == "24au.ru");

    //        if (_adv24Au != null)
    //        {
    //            _posted = _adv24Au.Published && _adv24Au.Active;
    //            _isExpired = _adv24Au.IsExpired();

    //            var beforeExpired = _adv24Au.TimeBegoreExpired();
    //            if (beforeExpired == default(TimeSpan))
    //            {
    //                _beforeExpired = "expired";
    //            }
    //            else
    //            {
    //                _beforeExpired = string.Format(@"{0:dd\.hh\:mm\:ss}", beforeExpired);
    //            }
    //        }



    //        if (string.IsNullOrWhiteSpace(_lotNumber) && _adv24Au != null)
    //        {
    //            _lotNumber = _adv24Au.Number.ToString();
    //        }


    //        var style = _product.GetSpecialStyle();
    //        if (style == null) _style = String.Empty;
    //        else _style = style.Name;





    //    }

    //    public bool IsPosted
    //    {
    //        get
    //        {
    //            UpdateAdvInfo();
    //            return _posted;
    //        }
    //    }

    //    public bool IsExpired
    //    {
    //        get
    //        {
    //            UpdateAdvInfo();
    //            return _isExpired;
    //        }
    //    }

    //    public string BreforeExpired
    //    {
    //        get
    //        {
    //            UpdateAdvInfo();
    //            return _beforeExpired;
    //        }
    //    }

    //    public CatalogFormRowOld(Product product, BegemotProduct begemotProduct, BegemotSalePrice begemotSalePrice)
    //    {
    //        _product = product;
    //        _begemotProduct = begemotProduct;
    //        _begemotSalePrice = begemotSalePrice;

    //        Margin = product.CalcMargin(begemotProduct, begemotSalePrice);
    //    }

    //    public Product GetProduct()
    //    {
    //        return _product;
    //    }

    //    public BegemotProduct GetBegemotProduct()
    //    {
    //        return _begemotProduct;
    //    }

    //    public BegemotSalePrice GetBegemotSale()
    //    {
    //        return _begemotSalePrice;
    //    }

    //    public string Group1
    //    {
    //        get
    //        {
    //            return _begemotProduct.Group1;
    //        }
    //    }

    //    public string Group2
    //    {
    //        get
    //        {
    //            return _begemotProduct.Group2;
    //        }
    //    }

    //    public int Count
    //    {
    //        get
    //        {
    //            return _begemotProduct.Count;
    //        }
    //    }

    //    public string Sale
    //    {
    //        get
    //        {
    //            var activeSale = _product.GetActiveSale();
    //            return activeSale == null ? string.Empty : activeSale.Title;
    //        }
    //    }

    //    public bool HasImage
    //    {
    //        get
    //        {
    //            var hasImage = _begemotProduct.HasImage();
    //            return hasImage;
    //        }
    //    }

    //    public string Code
    //    {
    //        get
    //        {
    //            var code = _product.GetCode();
    //            return code;
    //        }
    //    }
    //}
}
