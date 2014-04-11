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
using ToyShopDataLib.Utils;

namespace TestLotCreatorWin
{
    public partial class MarketplaceCard : Form
    {
        public MarketplaceCard()
        {
            InitializeComponent();
        }

        private void MarketplaceCard_Load(object sender, EventArgs e)
        {

        }

        private Marketplace _marketplace;

        private void btnSave_Click(object sender, EventArgs e)
        {
            var success = SetData(_marketplace);
            if (!success) return;

            Context.Save();

            DialogResult = DialogResult.OK;
        }

        public static void Update(Marketplace marketplace)
        {
            var marketplaceCard = new MarketplaceCard();
            marketplaceCard.GetData(marketplace);

            var changed = marketplaceCard.ShowDialog() == DialogResult.OK;
            if (changed)
            {
                //marketplace.ReApply();
                Context.Save();
            }
        }


        private void GetData(Marketplace market)
        {
            _marketplace = market;
            
            txtUrl.Text = market.Url;
            
            txtTitle.Text = market.Title;
            txtDescription.Text = market.Description;
        
            SetStyle(market.GetActiveStyle());
            txtActive.Checked = market.Active;
        }

        private bool SetData(Marketplace market)
        {
            if (txtTitle.Text.IsEmpty())
            {
                MessageBox.Show("Не указано название");
                return false;
            }

            //market.Url =txtUrl.Text;
            
            market.Title = txtTitle.Text;
            market.Description = txtDescription.Text;
            
            market.SetActiveStyle(_style);
            market.Active = txtActive.Checked;
            
            return true;
        }


        private Style _style;
        private void bthChooseStyle_Click(object sender, EventArgs e)
        {
            var style = StylesDictForm.ChooseStyle(StyleType.MarketPlace);
            SetStyle(style);
        }

        private void SetStyle(Style style)
        {
            _style = style;
            if (_style != null)
            {
                txtStyle.Text = _style.Name;
            }
        }

    }
}
