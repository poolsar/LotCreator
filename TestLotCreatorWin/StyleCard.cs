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
    public partial class StyleCard : Form
    {
        public StyleCard()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            txtDefaultMrkCategory.DataSource = MarketCategory.MarketCategorySet;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Save();
            DialogResult = DialogResult.OK;

        }



        public static Style CreateStyle(StyleType styleType)
        {
            var styleCard = new StyleCard();
            styleCard._styleType = styleType;
            styleCard.ShowDialog();

            return styleCard._style;
        }


        public static void UpdateStyle(Style style)
        {
            var styleCard = new StyleCard();
            styleCard.GetData(style);
            styleCard.ShowDialog();
        }

        public static void CopyStyle(Style style)
        {
            var styleCard = new StyleCard();
            styleCard.GetData(style);
            styleCard._style = null;
            styleCard.ShowDialog();

        }

        private void GetData(Style style)
        {
            _style = style;

            txtName.Text = _style.Name;
            txtTitle.Text = _style.Title;
            txtHeader.Text = _style.Header;
            txtContent.Text = _style.Content;
            txtFooter.Text = _style.Footer;

            txtDefaultMrkCategory.SelectedItem = _style.DefMarketCategoryObj;
        }

        private void SetData(Style style)
        {
            style.Name = txtName.Text.Trim();
            style.Title = txtTitle.Text.Trim();
            style.Header = txtHeader.Text.Trim();
            style.Content = txtContent.Text.Trim();
            style.Footer = txtFooter.Text.Trim();

            _style.DefMarketCategoryObj = (MarketCategory)txtDefaultMrkCategory.SelectedItem;

            style.DateUpdate = DateTime.Now;
        }


        private Style _style;
        private StyleType _styleType;

        private void Save()
        {
            if (_style == null)
            {
                _style = new Style();
                Context.Inst.StyleSet.Add(_style);

                _style.Active = true;
                _style.StyleType = _styleType;
            }

            SetData(_style);

            Context.Save();
        }

        private void StyleCard_Load(object sender, EventArgs e)
        {

        }



    }
}
