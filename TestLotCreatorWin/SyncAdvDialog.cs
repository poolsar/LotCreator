using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToyShopDataLib;

namespace TestLotCreatorWin
{
    public partial class SyncAdvDialog : Form
    {
        public SyncAdvDialog()
        {
            InitializeComponent();
        }

        private void SyncAdvDialog_Load(object sender, EventArgs e)
        {
            DataSource = Context.Inst.MarketplaceSet.ToList().Select(m => new DataSourceRow(m)).ToList();
        }

        private List<DataSourceRow> DataSource
        {
            get
            {
                var dataSourceRows = gridControl1.DataSource as List<DataSourceRow>;
                return dataSourceRows;
            }
            set
            {
                gridControl1.DataSource = value;
            }
        }

        private class DataSourceRow
        {
            private readonly Marketplace _marketplace;

            public DataSourceRow(Marketplace marketplace)
            {
                _marketplace = marketplace;

                Title = marketplace.Title;
            }

            public bool IsChecked { get; set; }
            public string Title { get; set; }



            public Marketplace GetMarketplace()
            {
                return _marketplace;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            DataSource.ForEach(r => r.IsChecked = chkAll.Checked);
            gridView1.RefreshData();
        }

        public SyncDialogResult SyncDialog(List<Product> products = null)
        {
            bool allowSelectMode = products != null && products.Count > 0;
            if (allowSelectMode)
            {
                radioSyncMode.Properties.Items[0].Enabled = true;
                radioSyncMode.SelectedIndex = 0;
            }

            DialogResult dialogResult = ShowDialog();

            var result = new SyncDialogResult();
            result.Cancel = dialogResult == DialogResult.Cancel;
            result.Marketplaces = DataSource.Where(r => r.IsChecked).Select(r => r.GetMarketplace()).ToList();
            result.Mode = (MarketplaceSyncMode)radioSyncMode.SelectedIndex;

            return result;
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            bool mpSeleted = DataSource.Any(r => r.IsChecked);
            if (!mpSeleted)
            {
                MessageBox.Show("Не выбрана ни одина площадка");
                return;
            }

            DialogResult = DialogResult.OK;

        }
    }



    public class SyncDialogResult
    {
        public bool Cancel { get; set; }
        public List<Marketplace> Marketplaces { get; set; }
        public MarketplaceSyncMode Mode { get; set; }
    }
}
