using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.PLinq.Helpers;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using PrestaWinClient.Logic;
using ShopDataLib;
using WebStoreLib;

namespace PrestaWinClient
{
    public partial class ManageProductsForm : Form
    {
        public ManageProductsForm()
        {
            InitializeComponent();
        }

        private ShopEntities context;
        private static void Save()
        {
            Context.Inst.SaveChanges();
        }

        private void ManageProductsForm_Load(object sender, EventArgs e)
        {
            context = Context.Inst;

            var dragDropController = new DragDropController(treeSuppliers, treeShop, btnDeleteNode);
            dragDropController.OnCursorChanged = OndragDropController_OnCursorChanged;

            BuildSupliersTree();
            BuildShopTree();

            this.KeyUp += ManageProductsForm_KeyUp;

            txtMarga.Value = Settings.Margin;

            treeShop.Focus();
        }

        void ManageProductsForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.Control) return;

            switch (e.KeyCode)
            {
                case Keys.Q:
                    treeSuppliers.Focus();
                    break;
                case Keys.W:
                    treeShop.Focus();
                    break;
            }
        }

        private void OndragDropController_OnCursorChanged(Cursor cursor)
        {
            Cursor = cursor;
        }


        private void btnRefreshSupliers_Click(object sender, EventArgs e)
        {

            RefreshSupliers();
            BuildSupliersTree();

        }

        private void RefreshSupliers()
        {
            Splash.Show();


            try
            {
                var taimyrScrapper = new TaimyrScrapper();

                taimyrScrapper.Output = Splash.WriteLine;

                taimyrScrapper.DownloadProducts();
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Ошибка");

                while (ex != null)
                {
                    sb.AppendLine(ex.Message);
                    ex = ex.InnerException;
                }


                Splash.Close();

                var msg = sb.ToString();


                MessageBox.Show(msg, "Ошибка загрузки поставок", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            Splash.Close();

        }

        internal void BuildSupliersTree()
        {
            var suppliers = context.SupplierSet.ToList();

            // чтобы захешировать и работало быстрее
            var supplierCategories = context.SupplierCategorySet.ToList();
            var supplierProducts = context.SupplierProductSet.ToList();

            var supNodes = new TreeNodes();

            foreach (var s in suppliers)
            {
                var supNode = new SupNode(s);
                supNodes.Add(supNode);
            }

            treeSuppliers.DataSource = supNodes;
            treeSuppliers.Refresh();
            if (treeSuppliers.Nodes.Count > 0)
            {
                treeSuppliers.Nodes[0].Expanded = true;
            }
        }

        private void BuildShopTree()
        {
            var shopCats = context.ShopCategorySet.Where(c => c.Parent == null).ToList();
            var shopNodes = new TreeNodes();

            foreach (var s in shopCats)
            {
                var supNode = new ShopCatNode(s, null);
                shopNodes.Add(supNode);
            }

            treeShop.DataSource = shopNodes;
            treeShop.Refresh();
            if (treeSuppliers.Nodes.Count > 0)
            {
                treeSuppliers.Nodes[0].Expanded = true;
            }
        }


        private void treeSuppliers_GetSelectImage(object sender, DevExpress.XtraTreeList.GetSelectImageEventArgs e)
        {
            GetSelectedImage(e, treeSuppliers);
        }

        private void treeShop_GetSelectImage(object sender, GetSelectImageEventArgs e)
        {
            GetSelectedImage(e, treeShop);
        }

        private static void GetSelectedImage(GetSelectImageEventArgs e, TreeList treeList)
        {
            var dataRecordByNode = treeList.GetDataRecordByNode(e.Node);

            ITreeNode obj = dataRecordByNode as ITreeNode;
            if (obj == null) return;

            e.NodeImageIndex = obj.ImageIndex;
        }


        private void SetDefaultCursor()
        {
            Cursor = Cursors.Default;
        }


        private void ManageProductsForm_MouseUp(object sender, MouseEventArgs e)
        {
            SetDefaultCursor();
        }

        private static ITreeNode GetNodeData(TreeListNode sourceNode)
        {
            if (sourceNode == null) return null;
            var sourceData = sourceNode.TreeList.GetDataRecordByNode(sourceNode) as ITreeNode;
            return sourceData;
        }


        private void ShowMergePopup(DragEventArgs e)
        {
            DevExpress.XtraBars.PopupMenu buttonContextMenu;
            DevExpress.XtraBars.BarButtonItem menuButtonExport = new DevExpress.XtraBars.BarButtonItem();
            DevExpress.XtraBars.BarButtonItem menuButtonSave = new DevExpress.XtraBars.BarButtonItem();


            var barManager1 = new BarManager();
            barManager1.Form = this;
            buttonContextMenu = new DevExpress.XtraBars.PopupMenu(barManager1);
            buttonContextMenu.Name = "subViewContextMenu";


            menuButtonExport.Caption = "Слить";
            menuButtonExport.Id = 1;
            menuButtonExport.Name = "menuButtonMerge";
            //menuButtonExport.ItemClick += new ItemClickEventHandler(menuButtonExport_ItemClick);

            menuButtonSave.Caption = "Добавить";
            menuButtonSave.Id = 2;
            menuButtonSave.Name = "menuButtonAdd";
            //menuButtonSave.ItemClick += new ItemClickEventHandler(menuButtonSave_ItemClick);
            //add items to barmanager
            barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[]
            {
                menuButtonExport,
                menuButtonSave
            });
            //create links between bar items and popup
            buttonContextMenu.ItemLinks.Add(barManager1.Items["menuButtonMerge"]);
            buttonContextMenu.ItemLinks.Add(barManager1.Items["menuButtonAdd"]);

            //finally set the context menu to the control or use the showpopup method on right click of control
            //barManager1.SetPopupContextMenu(btnInsert, buttonContextMenu);
            //buttonContextMenu.ShowPopup(treeShop.PointToClient(new Point(e.X, e.Y)));
            buttonContextMenu.ShowPopup(new Point(e.X, e.Y));
        }

        private void btnClearShop_Click(object sender, EventArgs e)
        {
            var cancel = MessageBox.Show("Вы уверены, что хотите сбросить ветрину?", "Сборс ветрины", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.Cancel;

            if (cancel) return;

            Splash.Show();
            ClearShop();
            BuildShopTree();
            Splash.Close();
        }

        private void ClearShop()
        {

            var shopProducts = context.ShopProductSet.ToList();
            var count = shopProducts.Count;
            for (int i = 0; i < shopProducts.Count; i++)
            {
                var shopProduct = shopProducts[i];
                shopProduct.Images = null;
                //context.ShopProductSet.Remove(shopProduct);
                shopProduct.Delete();

                Splash.WriteLine("Удален продукт {0} {1} {2}", shopProduct.Title, i + 1, count);
            }

            Splash.WriteLine("Сохранение удаления продуктов");
            context.SaveChanges();


            var shopCategories = context.ShopCategorySet.ToList();
            count = shopCategories.Count;
            for (int i = 0; i < shopCategories.Count; i++)
            {
                var shopCategory = shopCategories[i];
                shopCategory.Delete();
                //context.ShopCategorySet.Remove(shopCategory);
                Splash.WriteLine("Удалена категория {0} {1} {2}", shopCategory.Title, i + 1, count);
            }

            Splash.WriteLine("Сохранение удаления категорий");
            context.SaveChanges();
        }

        private void txtMarga_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtMarga_EditValueChanged(object sender, EventArgs e)
        {
            if (txtMarga.Value == Settings.Margin) return;

            Settings.Margin = txtMarga.Value;
            Save();
        }

        private void RecalcProductsPrice()
        {
            bool cancel = MessageBox.Show("Произвести перерасчет стоимости товаров", "Маржа изменена", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel;

            if (cancel) return;

            Wait("Пересчет стоимости товаров");

            ShopProduct.RecalcAllProductsPrice(Settings.Margin);
            Save();
            treeShop.RefreshDataSource();

            Complete();
        }

        private void Wait(string comments = "")
        {
            Cursor = Cursors.WaitCursor;

            Text = comments;
            Refresh();
        }

        private void Complete()
        {
            Cursor = Cursors.Default;
            Text = string.Empty;
        }

        private void btnRecalcPrices_Click(object sender, EventArgs e)
        {
            RecalcProductsPrice();
        }

        private void treeShop_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            e.Node.TreeList.RefreshNode(e.Node);

            //treeShop.RefreshDataSource();
        }

        private void RefreshRecursive(TreeListNode node)
        {
            node.TreeList.RefreshNode(node);

            foreach (TreeListNode subNode in node.Nodes)
            {
                if (!subNode.Visible) continue;
                RefreshRecursive(subNode);
            }

        }

        private void btnExpandTreeShop_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            treeShop.ExpandAll();

            Cursor = Cursors.Default;
        }

        private void btnSyncShop_Click(object sender, EventArgs e)
        {
            bool cancel = MessageBox.Show("Вы не нечайно нажали на кнопочку?", "Синхронизировать каталог", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel;

            if (cancel) return;

            SyncWebStore();
        }

        private void SyncWebStore()
        {
            Splash.Show();

            var webStoreController = new WebStoreController();

            webStoreController.Sync();

            Splash.Close();
        }

        private void btnOnlineCatalog_Click(object sender, EventArgs e)
        {
            var beforeSyncForm = new BeforeSyncForm();
            beforeSyncForm.ShowDialog();
        }


        //private void treeSuppliers_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        //{
        //    // e.NodeImageIndex = 0;
        //}


        //private void InternationalDropNodes(TreeListNode sourceNode, TreeListNode targetNode)
        //{
        //    try
        //    {
        //        var sourceData = GetNodeData(sourceNode);
        //        var targetData = GetNodeData(targetNode);

        //        ShopCategory parentShopCat = null;
        //        TreeNodes targetCollection = null;

        //        if (targetData == null)
        //        {
        //            var shopRoot = treeShop.DataSource as TreeNodes;
        //            targetCollection = shopRoot;
        //        }
        //        else if (targetData is ShopCatNode)
        //        {
        //            parentShopCat = (targetData as ShopCatNode).Base;
        //            targetCollection = targetData.Childs;
        //        }
        //        else if (targetData is ShopProdNode)
        //        {
        //            parentShopCat = (targetData as ShopProdNode).Base.Category;
        //            targetCollection = targetData.Childs;
        //        }

        //        if (sourceData is SupNode)
        //        {
        //            DropSupNode(sourceData, parentShopCat, targetCollection);
        //        }
        //        else if (sourceData is SupCatNode)
        //        {
        //            DropSupCatNode(sourceData, parentShopCat, targetCollection);
        //        }
        //        else if (sourceData is SupProdNode)
        //        {
        //            if (DropSupProdNode(parentShopCat, sourceData, targetCollection)) return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        //private bool DropSupProdNode(ShopCategory parentShopCat, ITreeNode sourceData, TreeNodes targetCollection)
        //{
        //    if (parentShopCat == null) return true;

        //    var supProdNode = sourceData as SupProdNode;
        //    var newShopProd = supProdNode.Base.CreateShopProduct(parentShopCat);
        //    context.SaveChanges();
        //    var newShopProdNode = new ShopProdNode(newShopProd);
        //    targetCollection.Add(newShopProdNode);
        //    return false;
        //}

        //private void DropSupCatNode(ITreeNode sourceData, ShopCategory parentShopCat, TreeNodes targetCollection)
        //{
        //    var supCatNode = sourceData as SupCatNode;
        //    var newShopCategory = supCatNode.Base.CreateShopCategory(parentShopCat, true);
        //    context.ShopCategorySet.Add(newShopCategory);
        //    context.SaveChanges();
        //    var newShopCatNode = new ShopCatNode(newShopCategory);
        //    targetCollection.Add(newShopCatNode);
        //}

        //private void DropSupNode(ITreeNode sourceData, ShopCategory parentShopCat, TreeNodes targetCollection)
        //{
        //    var supNode = sourceData as SupNode;

        //    List<ShopCategory> newShopCategories = supNode.Base.CreateShopCategories(parentShopCat, true);

        //    foreach (var newShopCategory in newShopCategories)
        //    {
        //        context.ShopCategorySet.Add(newShopCategory);
        //    }

        //    context.SaveChanges();

        //    foreach (var newShopCategory in newShopCategories)
        //    {
        //        var newShopCatNode = new ShopCatNode(newShopCategory);
        //        targetCollection.Add(newShopCatNode);
        //    }
        //}





        //private TreeListNode GetDragNode(IDataObject data)
        //{
        //    return data.GetData(typeof(TreeListNode)) as TreeListNode;
        //}


        //private void SetDragCursor(DragDropEffects e)
        //{
        //    return;
        //    if (e == DragDropEffects.Move)
        //        Cursor = new Cursor(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PrestaWinClient.ResourceImages.move.ico"));
        //    if (e == DragDropEffects.Copy)
        //        Cursor = new Cursor(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PrestaWinClient.ResourceImages.copy.ico"));
        //    if (e == DragDropEffects.None)
        //        Cursor = Cursors.No;
        //}



        //private void treeShop_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        //{
        //    //if (e.Menu is TreeListNodeMenu)
        //    //{
        //    //    treeShop.FocusedNode = ((TreeListNodeMenu)e.Menu).Node;
        //    //    e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("Добавить", bbAddChild_ItemClick));
        //    //    e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("Слить", bbMerge_ItemClick));
        //    //}
        //}


        //private void bbMerge_ItemClick(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Слитие");

        //}

        //private void bbAddChild_ItemClick(object sender, EventArgs e)
        //{
        //    MessageBox.Show("Добавление");
        //}
    }

    internal class DragDropController
    {
        private ShopEntities context;
        private Cursor _cursor;
        public TreeList treeSuppliers { get; set; }
        public TreeList treeShop { get; set; }

        public Cursor Cursor
        {
            get { return _cursor; }
            set
            {
                OnCursorChanged(value);
                _cursor = value;
            }
        }

        public Action<Cursor> OnCursorChanged { get; set; }

        public DragDropController(TreeList treeSuppliers, TreeList treeShop, Button btnDeleteNode)
        {
            this.treeSuppliers = treeSuppliers;
            this.treeShop = treeShop;

            context = Context.Inst;

            treeSuppliers.GiveFeedback += treeSuppliers_GiveFeedback;

            treeSuppliers.DragOver += treeSuppliers_DragOver;
            treeSuppliers.DragDrop += treeSuppliers_DragDrop;

            treeShop.GiveFeedback += treeShop_GiveFeedback;
            treeShop.DragOver += treeShop_DragOver;
            treeShop.DragDrop += treeShop_DragDrop;

            btnDeleteNode.DragOver += Form_DragOver;
            btnDeleteNode.DragDrop += Form_DragDrop;

            treeShop.KeyUp += treeShop_KeyUp;

            treeShop.DoubleClick += treeShop_DoubleClick;
        }


        void treeShop_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode node = treeShop.FocusedNode;
            EditNode(node);
        }

        void treeShop_KeyUp(object sender, KeyEventArgs e)
        {
            var focusedNode = treeShop.FocusedNode;

            if (focusedNode == null) return;

            if (!e.Control) return;

            switch (e.KeyCode)
            {
                case Keys.Right:
                    focusedNode.Expanded = true;
                    break;
                case Keys.Left:
                    focusedNode.Expanded = false;
                    break;
                case Keys.Delete:
                    DeleteNode(focusedNode);
                    break;
                case Keys.Enter:
                    EditNode(focusedNode);
                    break;
                case Keys.D:
                    EditImages(focusedNode);
                    break;


                case Keys.A:
                    if (e.Modifiers == Keys.Control) CreateShopCatNode(focusedNode); break;

                case Keys.S:
                    if (e.Modifiers == Keys.Control) CreateShopProdNode(focusedNode);
                    break;

            }



        }

        private void EditImages(TreeListNode node)
        {
            Cursor = Cursors.WaitCursor;

            ITreeNode treeNode = GetData(node);

            if (treeNode is ShopCatNode)
            {
                ShopCategory category = (treeNode as ShopCatNode).Base;
                SetImageForm.EditImages(category);
            }

            if (treeNode is ShopProdNode)
            {
                ShopProduct product = (treeNode as ShopProdNode).Base;
                SetImageForm.EditImages(product);
            }

            node.TreeList.RefreshNode(node);

            Cursor = Cursors.Default;
        }

        private void EditNode(TreeListNode focusedNode)
        {
            Cursor = Cursors.WaitCursor;

            ITreeNode treeNode = GetData(focusedNode);

            if (treeNode is ShopProdNode)
            {
                ShopProduct shopProduct = (treeNode as ShopProdNode).Base;
                ShopProductCard.EditProduct(shopProduct);
            }

            Cursor = Cursors.Default;
        }

        private void CreateShopProdNode(TreeListNode parent)
        {





        }

        private void CreateShopCatNode(TreeListNode parent)
        {
            ITreeNode data = GetData(parent);
            ShopCatNode parentShopCatNode = null;
            if (data is ShopProdNode)
            {
                parentShopCatNode = (data as ShopProdNode).Parent as ShopCatNode;
            }
            else
            {
                parentShopCatNode = data as ShopCatNode;
            }

            ShopCategory parentShopCat = parentShopCatNode.Base;

            var shopCategory = new ShopCategory(parentShopCat);
            shopCategory.Title = "Новая категория";
            context.SaveChanges();

            var shopCatNode = new ShopCatNode(shopCategory, parentShopCatNode);
            parentShopCatNode.Childs.Add(shopCatNode);

            parent.TreeList.RefreshDataSource();

            foreach (TreeListNode treeListNode in parent.Nodes)
            {
                ShopCatNode neighbourCat = GetData(treeListNode) as ShopCatNode;
                if (neighbourCat == null) continue;
                if (neighbourCat.Base.Id != shopCategory.Id) continue;

                var treeList = treeListNode.TreeList;
                treeList.MakeNodeVisible(treeListNode);
                treeListNode.TreeList.FocusedNode = treeListNode;
                //treeListNode.TreeList.SetFocusedNode(treeListNode);
                treeListNode.TreeList.ShowEditor();
                break;
            }
        }


        void Form_DragOver(object sender, DragEventArgs e)
        {
            TreeListNode dragNode = GetDragNode(e.Data);

            if (dragNode != null && dragNode.TreeList == treeShop)
            {
                e.Effect = DragDropEffects.Move;
                Cursor = Cursors.No;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        void Form_DragDrop(object sender, DragEventArgs e)
        {
            TreeListNode dragNode = GetDragNode(e.Data);

            DeleteNode(dragNode);
        }

        private void DeleteNode(TreeListNode dragNode)
        {

            ITreeNode dragData = GetData(dragNode);

            if (dragNode == null || dragNode.TreeList != treeShop) return;

            string msg = "Вы уверены, что хотите удалить ";
            msg += dragData is ShopCategory ? "категорию " : "продукт ";
            msg += dragData.GetCellData("Название");
            bool cancel = MessageBox.Show(msg, "Удаление", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.Cancel;

            if (cancel) return;

            Cursor = Cursors.WaitCursor;

            dragData.Delete();
            context.SaveChanges();
            treeShop.DeleteNode(dragNode);

            //treeShop.RefreshDataSource();

            Cursor = Cursors.Default;
        }

        void treeShop_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
        }

        void treeSuppliers_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
        }



        private void treeSuppliers_DragDrop(object sender, DragEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // для перенесенных ссылок из браузера
            string url = (string)e.Data.GetData(typeof(string));

            var newProdSuplier = Scraper.Scrape(url);

            if (newProdSuplier != null)
            {
                var treeNodes = treeSuppliers.DataSource as TreeNodes;

                var node = treeNodes.Cast<SupNode>().FirstOrDefault(n => n.Base.Id == newProdSuplier.Id);

                if (node == null)
                {
                    node = new SupNode(newProdSuplier);
                    treeNodes.Add(node);
                }

                treeSuppliers.RefreshDataSource();
            }

            Cursor = Cursors.Default;
        }

        private void treeSuppliers_DragOver(object sender, DragEventArgs e)
        {
            // для перенесенных ссылок из браузера
            string url = (string)e.Data.GetData(typeof(string));

            if (string.IsNullOrWhiteSpace(url) || !url.Contains("24au.ru"))
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
            }

            SetDragCursor(e.Effect);
        }


        void treeShop_DragOver(object sender, DragEventArgs e)
        {

            // разрешить или нет делать сюда drop
            // разрешено всегда?

            // если копирование лота от поставщика - создаем лот магазина. проверяем на повторы. категории предлагаем слить

            // если перемещение лота магазина - перемещаем. категории предлогаем слить


            // запрещаем только перенесение продуктов на пестое место

            TreeListHitInfo hi = treeShop.CalcHitInfo(treeShop.PointToClient(new Point(e.X, e.Y)));
            TreeListNode dragNode = GetDragNode(e.Data);
            ITreeNode dragData = GetData(dragNode);

            if (
                (hi.HitInfoType == HitInfoType.Empty || hi.Node == null)
                &&
                (dragData is SupProdNode || dragData is ShopProdNode)
                )
            {
                e.Effect = DragDropEffects.None;
            }
            else if (dragNode.TreeList == treeSuppliers)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (dragNode.TreeList == treeShop)
            {
                e.Effect = DragDropEffects.Move;
            }

            SetDragCursor(e.Effect);
        }

        void treeShop_DragDrop(object sender, DragEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            TreeListHitInfo hi = treeShop.CalcHitInfo(treeShop.PointToClient(new Point(e.X, e.Y)));
            TreeListNode targetNode = hi.Node;
            var sourceNode = GetDragNode(e.Data);
            DropeNode(sourceNode, targetNode);

            Cursor = Cursors.Default;
        }

        private void DropeNode(TreeListNode sourceNode, TreeListNode targetNode)
        {
            try
            {
                var sourceData = GetData(sourceNode);
                var targetData = GetData(targetNode);

                ShopCatNode targetShopCatNode = null;
                TreeNodes targetCollection = null;

                GetTargets(targetData, ref targetShopCatNode, ref targetCollection);

                if (sourceData is SupNode)
                {
                    DropSupNode(sourceData as SupNode, targetShopCatNode, targetCollection);
                }
                else if (sourceData is SupCatNode)
                {
                    DropSupCatNode(sourceData as SupCatNode, targetShopCatNode, targetCollection);
                }
                else if (sourceData is SupProdNode)
                {
                    DropSupProdNode(sourceData as SupProdNode, targetShopCatNode, targetCollection);
                }
                else if (sourceData is ShopCatNode)
                {
                    DropShopCatNode(sourceData as ShopCatNode, targetShopCatNode, targetCollection);
                }
                else if (sourceData is ShopProdNode)
                {
                    DropShopProdNode(sourceData as ShopProdNode, targetShopCatNode, targetCollection);
                }

                treeShop.RefreshDataSource();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void GetTargets(ITreeNode targetData, ref ShopCatNode targetShopCatNode, ref TreeNodes targetCollection)
        {
            if (targetData == null)
            {
                var shopRoot = treeShop.DataSource as TreeNodes;
                targetCollection = shopRoot;
            }
            else if (targetData is ShopCatNode)
            {
                targetShopCatNode = targetData as ShopCatNode;
                targetCollection = targetShopCatNode.Childs;
            }
            else if (targetData is ShopProdNode)
            {
                targetShopCatNode = (targetData as ShopProdNode).Parent as ShopCatNode;
                targetCollection = targetShopCatNode.Childs;
            }

        }

        private void DropShopCatNode(ShopCatNode sourceCatNode, ShopCatNode targetCatNode, TreeNodes targetCollection)
        {
            var sourceShopCat = sourceCatNode.Base;
            var targetShopCat = targetCatNode == null ? null : targetCatNode.Base;

            bool mergeCategories = false;
            if (targetCatNode != null)
            {
                mergeCategories = MessageBox.Show("Да - Слить, Нет - Добавить?", "Слияние", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }

            if (mergeCategories)
                sourceShopCat.Merge(targetShopCat);

            else
                sourceShopCat.Move(targetShopCat);

            context.SaveChanges();

            var rootTreeNodes = treeShop.DataSource as TreeNodes;
            TreeNodes sourceContainer = sourceCatNode.Parent == null ? rootTreeNodes : sourceCatNode.Parent.Childs;
            sourceContainer.Remove(sourceCatNode);

            if (mergeCategories) return;

            targetCollection.Add(sourceCatNode);
            sourceCatNode.Parent = targetCatNode;
        }

        private void DropShopProdNode(ShopProdNode shopProdNode, ShopCatNode targetShopCatNode, TreeNodes targetCollection)
        {
            if (targetShopCatNode == null) return;

            shopProdNode.Base.Move(targetShopCatNode.Base);
            context.SaveChanges();

            shopProdNode.Parent.Childs.Remove(shopProdNode);
            targetCollection.Add(shopProdNode);
            shopProdNode.Parent = targetShopCatNode;
        }

        //private void InternationalDropNodes(TreeListNode sourceNode, TreeListNode targetNode)
        //{
        //    try
        //    {
        //        var sourceData = GetData(sourceNode);
        //        var targetData = GetData(targetNode);

        //        ShopCatNode targetShopCatNode = null;
        //        TreeNodes targetCollection = null;

        //        GetTargets(targetData, ref targetShopCatNode, ref targetCollection);



        //        if (sourceData is SupNode)
        //        {
        //            DropSupNode(sourceData as SupNode, targetShopCatNode, targetCollection);
        //        }
        //        else if (sourceData is SupCatNode)
        //        {
        //            DropSupCatNode(sourceData as SupCatNode, targetShopCatNode, targetCollection);
        //        }
        //        else if (sourceData is SupProdNode)
        //        {
        //            DropSupProdNode(sourceData as SupProdNode, targetShopCatNode, targetCollection);
        //        }

        //        treeShop.RefreshDataSource();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        private void DropSupProdNode(SupProdNode supProdNode, ShopCatNode parentShopCatNode, TreeNodes targetCollection)
        {
            if (parentShopCatNode == null) return;

            var newShopProd = supProdNode.Base.CreateShopProduct(parentShopCatNode.Base);
            context.SaveChanges();
            var newShopProdNode = new ShopProdNode(newShopProd, parentShopCatNode);
            targetCollection.Add(newShopProdNode);
        }

        private void DropSupCatNode(ITreeNode sourceData, ShopCatNode parentShopCatNode, TreeNodes targetCollection)
        {
            var supCatNode = sourceData as SupCatNode;
            var parentShopCat = parentShopCatNode == null ? null : parentShopCatNode.Base;

            bool mergeCategories = false;
            if (parentShopCatNode != null)
            {
                mergeCategories = MessageBox.Show("Да - Слить, Нет - Добавить?", "Слияние", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }

            var newShopCategory = supCatNode.Base.CreateShopCategory(parentShopCat, mergeCategories);
            context.SaveChanges();

            var newShopCatNode = new ShopCatNode(newShopCategory, parentShopCatNode);
            targetCollection.Add(newShopCatNode);
        }

        private void DropSupNode(SupNode supNode, ShopCatNode parentShopCatNode, TreeNodes targetCollection)
        {
            List<ShopCategory> newShopCategories = supNode.Base.CreateShopCategories(parentShopCatNode.Base);

            context.SaveChanges();

            targetCollection.Clear();
            foreach (var newShopCategory in newShopCategories)
            {
                var newShopCatNode = new ShopCatNode(newShopCategory, parentShopCatNode);
                targetCollection.Add(newShopCatNode);
            }
        }



        private static ITreeNode GetData(TreeListNode node)
        {
            if (node == null) return null;
            ITreeNode treeNode = node.TreeList.GetDataRecordByNode(node) as ITreeNode;
            return treeNode;
        }

        private TreeListNode GetDragNode(IDataObject data)
        {
            return data.GetData(typeof(TreeListNode)) as TreeListNode;
        }



        private void SetDefaultCursor()
        {
            Cursor = Cursors.Default;
        }

        private void SetDragCursor(DragDropEffects e)
        {
            if (e == DragDropEffects.Move)
                Cursor = new Cursor(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PrestaWinClient.ResourceImages.move.ico"));
            else if (e == DragDropEffects.Copy)
                Cursor = new Cursor(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("PrestaWinClient.ResourceImages.copy.ico"));
            else if (e == DragDropEffects.None)
                Cursor = Cursors.No;
        }

    }

}
