using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using WebStoreLib;

namespace PrestaWinClient
{
    public partial class BeforeSyncForm : Form
    {
        public BeforeSyncForm()
        {
            InitializeComponent();
        }

        private BeforeSyncController beforeSyncController;
        private void BeforeSyncForm_Load(object sender, EventArgs e)
        {
            beforeSyncController = new BeforeSyncController();
            var beforeSyncRootCategories = beforeSyncController.Load();

            var treeNodes = new TreeNodes();
            foreach (var beforeSyncRootCategory in beforeSyncRootCategories)
            {
                var node = new BeforeSyncCatNode(beforeSyncRootCategory);
                treeNodes.Add(node);
            }

            treeBeforeSync.DataSource = treeNodes;
            treeBeforeSync.ExpandAll();


        }


        public class BeforeSyncCatNode : ITreeNode
        {
            private TreeNodes _childs;
            public BeforeSyncCategory Base { get; set; }
            public ITreeNode Parent { get; set; }

            public TreeNodes Childs
            {
                get
                {
                    _childs = new TreeNodes();

                    if (Base.Childs != null)
                        Base.Childs.ToList().Select(c => new BeforeSyncCatNode(c)).ToList().ForEach(c => _childs.Add(c));

                    if (Base.Products != null)
                        Base.Products.ToList().Select(s => new BeforeSyncProdNode(s)).ToList().ForEach(s => _childs.Add(s));

                    return _childs;
                }
                set { _childs = value; }
            }

            public int ImageIndex
            {
                get { return 1; }
            }

            public BeforeSyncCatNode(BeforeSyncCategory @base)
            {
                Base = @base;
            }

            public object GetCellData(string colName)
            {
                switch (colName)
                {
                    case ShopTreeColNames.Title:
                        return Base.Title;
                    case ShopTreeColNames.BeforeSyncCheck:
                        return Base.Checked;

                    default:
                        return null;
                }
            }

            public void SetCellData(string colName, object newCellData)
            {
                //bool save = true;
                switch (colName)
                {
                    case ShopTreeColNames.Title:
                        Base.Title = (string)newCellData;
                        break;

                    case ShopTreeColNames.BeforeSyncCheck:
                        Base.SetCheckedRecursive((bool)newCellData);
                        break;

                    default:
                        break;
                }

                //if (save) Context.Save();

            }

            public void Delete()
            {


            }
        }


        public class BeforeSyncProdNode : ITreeNode
        {
            public BeforeSyncProduct Base { get; set; }
            public ITreeNode Parent { get; set; }
            public TreeNodes Childs { get; set; }
            public int ImageIndex
            {
                get { return 2; }
            }

            public BeforeSyncProdNode(BeforeSyncProduct @base)
            {
                Base = @base;
            }

            public object GetCellData(string colName)
            {
                switch (colName)
                {
                    case ShopTreeColNames.Title:
                        return Base.Title;
                    case ShopTreeColNames.BeforeSyncCheck:
                        return Base.Checked;
                        break;

                    default:
                        return null;
                }
            }

            public void SetCellData(string colName, object newCellData)
            {

                switch (colName)
                {
                    case ShopTreeColNames.Title:
                        Base.Title = (string)newCellData;
                        break;
                    case ShopTreeColNames.BeforeSyncCheck:
                        Base.Checked = (bool)newCellData;
                        break;

                    default:
                        break;
                }

            }



            public void Delete()
            {
            }

        }

        private void treeBeforeSync_GetSelectImage(object sender, GetSelectImageEventArgs e)
        {
            GetSelectedImage(e, treeBeforeSync);
        }

        private static void GetSelectedImage(GetSelectImageEventArgs e, TreeList treeList)
        {
            var dataRecordByNode = treeList.GetDataRecordByNode(e.Node);

            ITreeNode obj = dataRecordByNode as ITreeNode;
            if (obj == null) return;

            e.NodeImageIndex = obj.ImageIndex;
        }

        private void treeBeforeSync_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            treeBeforeSync.RefreshDataSource();
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

        private void Save()
        {
            Cursor = Cursors.WaitCursor;
            beforeSyncController.Save();
            Cursor = Cursors.Default;

        }
    }
}
