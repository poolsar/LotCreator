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
    public partial class SaleDict : Form
    {
        private Sale _sale;
        private bool _choseMode;

        public SaleDict()
        {
            InitializeComponent();
        }

        private void btnCreateSale_Click(object sender, EventArgs e)
        {
            SaleCard.Create();
            RefreshData();
        }

        private void SaleDict_Load(object sender, EventArgs e)
        {
            btnChoose.Enabled = _choseMode;
            btnWothoutSale.Enabled = _choseMode;

            RefreshData();
        }

        private void RefreshData()
        {
            var f = from s in Context.Inst.SaleSet.ToList()
                    select new DataSourceRow(s);

            gridControl1.DataSource = f;
        }

        private class DataSourceRow
        {

            public string Title
            {
                get { return _sale.Title; }
            }

            public bool Active
            {
                get { return _sale.Active; }
            }

            public string Start
            {
                get { return _sale.DateStart.ToShortDateString(); }
            }

            public string Expire
            {
                get { return _sale.DateExpire.ToShortDateString(); }
            }

            public string Update
            {
                get { return _sale.DateUpdate.ToString(); }
            }


            private readonly Sale _sale;

            public DataSourceRow(Sale sale)
            {
                _sale = sale;
            }

            public Sale GetSale()
            {
                return _sale;

            }
        }

        public static bool ChooseSale(out Sale sale)
        {
            var saleDict = new SaleDict();

            saleDict._choseMode = true;
            var success = saleDict.ShowDialog() == DialogResult.OK;
            sale = saleDict._sale;

            return success;
        }

        private void btnChoose_Click(object sender, EventArgs e)
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

            _sale = row.GetSale();

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

            SaleCard.Update(row.GetSale());
            RefreshData();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            var row = (DataSourceRow)gridView1.GetFocusedRow();
            if (row == null) return;

            SaleCard.Copy(row.GetSale());
            RefreshData();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Choose();
        }

        private void btnWothoutSale_Click(object sender, EventArgs e)
        {
            _sale = null;
            DialogResult = DialogResult.OK;
        }

    }
}
