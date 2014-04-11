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
    public partial class SaleCard : Form
    {
        public SaleCard()
        {
            InitializeComponent();
        }

        private void SaleCard_Load(object sender, EventArgs e)
        {

        }

        public static void Create()
        {
            var saleCard = new SaleCard();
            saleCard.ShowDialog();
        }

        public static void Update(Sale sale)
        {
            var saleCard = new SaleCard();
            saleCard.GetData(sale);
            var changed = saleCard.ShowDialog() == DialogResult.OK;
            if (changed)
            {
                sale.ReApply();
                Context.Save();
            }
        }


        public static void Copy(Sale sale)
        {
            var saleCard = new SaleCard();
            saleCard.GetData(sale, true);
            saleCard.ShowDialog();
        }

        private void GetData(Sale sale, bool isCopy = false)
        {
            if (!isCopy)
            {
                _sale = sale;
                txtTitle.Text = sale.Title;
            }

            txtActive.Checked = sale.Active;
            txtDescription.Text = sale.Description;
            txtDateStart.DateTime = sale.DateStart;
            txtDateExpire.DateTime = sale.DateExpire;

            SetStyle(sale.Style);
            txtMarginMax.Text = sale.MarginMax;
            txtMarginMin.Text = sale.MarginMin;
            txtMarginToWholesale.Text = sale.MarginToWholesale;
            txtDescountToRetail.Text = sale.DescountToRetail;
        }




        private Sale _sale;
        private void button1_Click(object sender, EventArgs e)
        {
            if (_sale == null)
            {
                _sale = new Sale();
                Context.Inst.SaleSet.Add(_sale);
                _sale.Algoritm = string.Empty;
            }

            var success = SetData(_sale);
            if (!success) return;

            _sale.DateUpdate = DateTime.Now;
            Context.Save();

            DialogResult = DialogResult.OK;
        }

        private bool SetData(Sale sale)
        {
            if (txtTitle.Text.IsEmpty())
            {
                MessageBox.Show("Не указано название");
                return false;
            }

            var title = txtTitle.Text;
            var description = txtDescription.Text;

            var dateStart = txtDateStart.DateTime;
            var dateExpire = txtDateExpire.DateTime;

            var now = DateTime.Now;

            if (dateStart >= dateExpire || dateExpire < now)
            {
                MessageBox.Show("Неправильно указан период");
                return false;
            }

            var marginMax = txtMarginMax.Text;
            var marginMin = txtMarginMin.Text;
            var marginToWholesale = txtMarginToWholesale.Text;
            var descountToRetail = txtDescountToRetail.Text;

            Check(marginMax, marginMin, marginToWholesale, descountToRetail);

            if (string.IsNullOrWhiteSpace(marginToWholesale)
                && string.IsNullOrWhiteSpace(descountToRetail))
            {
                MessageBox.Show(string.Format("Укажите наценку или скидку"));
                return false;
            }

            var useMax = txtUseMax.Checked;
            var active = txtActive.Checked;



            sale.Active = active;
            sale.Title = title;
            sale.Description = description;
            sale.DateStart = dateStart;
            sale.DateExpire = dateExpire;
            sale.Style = _style;
            sale.MarginMax = marginMax;
            sale.MarginMin = marginMin;
            sale.MarginToWholesale = marginToWholesale;
            sale.DescountToRetail = descountToRetail;
            sale.UseMaxResult = useMax;
            return true;
        }

        private static bool Check(params string[] values)
        {
            bool noErrors = true;
            foreach (var value in values)
            {
                var cheked = Check(value);
                if (!cheked)
                {
                    noErrors = false;
                    MessageBox.Show(string.Format("Значение: {0}  не соответствует формату", value));
                    break;
                }
            }
            return noErrors;
        }

        private static bool Check(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return true;

            var containsPercent = value.Contains("%");
            if (containsPercent)
            {
                value = value.Replace("%", "");
            }

            decimal dec;
            var cheked = decimal.TryParse(value, out dec);
            return cheked;
        }

        private Style _style;
        private void bthChooseStyle_Click(object sender, EventArgs e)
        {
            var style = StylesDictForm.ChooseStyle();
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
