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
    public partial class StylesDictForm : Form
    {
        public StylesDictForm()
        {
            InitializeComponent();
        }

        private void btnTitlesDescriptions_Click(object sender, EventArgs e)
        {
            CreateStyle(StyleType.SpecialDescrition);
        }

        private void btnCreateSaleStyle_Click(object sender, EventArgs e)
        {
            CreateStyle(StyleType.Sale);
        }

        private void btnCreateMarketStyle_Click(object sender, EventArgs e)
        {

            CreateStyle(StyleType.MarketPlace);
        }

        private void CreateStyle(StyleType styleType)
        {
            var style = StyleCard.CreateStyle(styleType);
            RefreshData();
        }


        private void StylesDictForm_Load(object sender, EventArgs e)
        {
            if (_styleType != null)
            {
                btnCreateSpecDescriptions.Enabled = _styleType == StyleType.SpecialDescrition;
                btnCreateSaleStyle.Enabled = _styleType == StyleType.Sale;
                btnCreateMarketStyle.Enabled = _styleType == StyleType.MarketPlace;
            }

            RefreshData();
        }

        private void RefreshData()
        {
            var f = (from s in Context.Inst.StyleSet.ToList()
                    where _styleType == null || s.StyleType == _styleType
                    select new DataSourceRow(s)).Reverse().ToList();
            
            if (f.Count == 0) return;

            gridControl1.DataSource = f;

        }

        private class DataSourceRow
        {
            public string Name
            {
                get { return _style.Name; }
            }

            public string Type
            {
                get { return _style.StyleType.String(); }
            }

            public string Category
            {
                get { return _style.DefMarketCategoryObj.Title; }
            }

            private readonly Style _style;

            public DataSourceRow(Style style)
            {
                _style = style;
            }

            public Style GetStyle()
            {
                return _style;

            }
        }

        public static Style ChooseStyle(StyleType? styleType = null)
        {

            var stylesDictForm = new StylesDictForm();

            stylesDictForm._styleType = styleType;


            if (stylesDictForm.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return stylesDictForm._style;
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

            _style = row.GetStyle();

            DialogResult = DialogResult.OK;
        }

        private Style _style;
        private StyleType? _styleType;

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Choose();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            var row = (DataSourceRow)gridView1.GetFocusedRow();
            if (row == null) return;

            StyleCard.UpdateStyle(row.GetStyle());
            RefreshData();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            var row = (DataSourceRow)gridView1.GetFocusedRow();
            if (row == null) return;

            StyleCard.CopyStyle(row.GetStyle());
            RefreshData();
        }


    }
}
