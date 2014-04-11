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
    public partial class MarketplaceDict : Form
    {
        private Marketplace _marketplace;

        public MarketplaceDict()
        {
            InitializeComponent();
        }


        private void MarketplaceDict_Load_1(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            var f = from s in Context.Inst.MarketplaceSet.ToList()
                    select new DataSourceRow(s);

            gridControl1.DataSource = f;
        }

        private class DataSourceRow
        {

            public string Title
            {
                get { return _Marketplace.Title; }
            }

            public bool Active
            {
                get { return _Marketplace.Active; }
            }

            public string Url
            {
                get { return _Marketplace.Url; }
            }

            private readonly Marketplace _Marketplace;

            public DataSourceRow(Marketplace Marketplace)
            {
                _Marketplace = Marketplace;
            }

            public Marketplace GetMarketplace()
            {
                return _Marketplace;

            }
        }

        public static Marketplace ChooseMarketplace()
        {
            var marketplaceDict = new MarketplaceDict();
            if (marketplaceDict.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return marketplaceDict._marketplace;
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            Choose();
        }
        private void gridView1_DoubleClick_1(object sender, EventArgs e)
        {
            Choose();
        }

        private void Choose()
        {
            var row = (DataSourceRow)gridView1.GetFocusedRow();
            if (row == null)
            {
                return;
            }

            _marketplace = row.GetMarketplace();

            DialogResult = DialogResult.OK;
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            var row = (DataSourceRow)gridView1.GetFocusedRow();
            if (row == null) return;

            MarketplaceCard.Update(row.GetMarketplace());
            RefreshData();
        }
    }
}

