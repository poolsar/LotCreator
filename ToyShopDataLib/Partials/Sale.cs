using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using ToyShopDataLib.Utils;

namespace ToyShopDataLib
{
    public partial class Sale
    {

        public bool IsNowActive()
        {
            var now = DateTime.Now;
            var result = Active && DateStart <= now && now <= DateExpire;
            return result;
        }

        public void ReApply()
        {
            if (!IsNowActive()) return;

            var pbss = Product.GetPBS(Products);

            foreach (var pbs in pbss)
            {
                Apply(pbs.Product, pbs.BProduct, pbs.BSale);
            }
        }

        public void Apply(Product product, BegemotProduct bproduct, BegemotSalePrice bsale)
        {
            var needAdd = !product.Sales.Any(s => s.Id == Id);
            if (needAdd)
            {
                product.Sales.Add(this);
            }

            product.RecalcPrice(bproduct, bsale);

            if (Style != null)
            {
                product.ReApplyStyle();
            }
        }

        public decimal CalcSalePrice(decimal price, decimal selfPrice)
        {
            var result = SaleCalculator.GetPrice(price, selfPrice);
            return result;
        }

        private SaleCalculator _saleCalculator;
        private SaleCalculator SaleCalculator
        {
            get
            {
                return _saleCalculator ?? (_saleCalculator = new SaleCalculator(this));
            }
        }


        public string GetCode(bool tagMode = false)
        {
            var code = ProductCode.GetCode(this, tagMode);
            
            return code;
        }

        public static List<Sale> GetActiveSales()
        {
            var now = DateTime.Now;
            var result = Context.Inst.SaleSet.Where(s => s.Active && s.DateStart <= now && now < s.DateExpire).ToList();
            return result;
        }
    }

    public class SaleCalculator
    {
        private readonly Sale _sale;

        public SaleCalculator(Sale sale)
        {
            _sale = sale;
        }

        public decimal GetPrice(decimal price, decimal selfPrice)
        {
            decimal result = price;

            result = ApplyMarginsNDescounts(result, selfPrice);
            result = ApplyMarginBounds(result, selfPrice);
            result = Apply99Postfix(result);

            return result;
        }

        private decimal ApplyMarginsNDescounts(decimal price, decimal selfPrice)
        {
            // скидки или накидка

            var useDescToRet = !_sale.DescountToRetail.IsEmpty();
            var useMargToWhol = !_sale.MarginToWholesale.IsEmpty();

            decimal descToRetPrice = useDescToRet ? CalcMargin(price, _sale.DescountToRetail, true) : price;
            decimal margToWholPrice = useMargToWhol ? CalcMargin(selfPrice, _sale.MarginToWholesale, false) : price;

            decimal result = price;
            if (useDescToRet && useMargToWhol)
            {
                result = _sale.UseMaxResult
                    ? Math.Max(descToRetPrice, margToWholPrice)
                    : Math.Min(descToRetPrice, margToWholPrice);
            }
            else if (useDescToRet) result = descToRetPrice;
            else if (useMargToWhol) result = margToWholPrice;
            return result;
        }

        private decimal ApplyMarginBounds(decimal price, decimal selfPrice)
        {
            // ограничения по марже
            var useMarginMax = !_sale.MarginMax.IsEmpty();
            var useMarginMin = !_sale.MarginMin.IsEmpty();

            decimal margin = price - selfPrice;
            decimal maginMin = useMarginMin ? _sale.MarginMin.ToDecimal() : 0;
            decimal maginMax = useMarginMax ? _sale.MarginMax.ToDecimal() : 0;

            decimal result = price;

            if (useMarginMin && margin < maginMin)
            {
                result = selfPrice + maginMin;
            }
            else if (useMarginMax && margin > maginMax)
            {
                result = selfPrice + maginMax;
            }

            return result;
        }



        private static decimal CalcMargin(decimal price, string margin, bool isDescount = false)
        {
            if (margin.IsEmpty()) return price;

            var isPercent = margin.Contains("%");

            decimal result = price;

            if (isPercent)
            {
                result = CalcPercentMargin(price, margin, isDescount);
            }
            else
            {
                result = CalcNominalMargin(price, margin, isDescount);
            }

            return result;
        }

        private static decimal CalcNominalMargin(decimal price, string margin, bool isDescount)
        {
            var marginD = margin.ToDecimal();

            decimal result;
            if (isDescount)
            {
                result = price - marginD;
            }
            else
            {
                result = price + marginD;
            }

            return result;
        }

        private static decimal CalcPercentMargin(decimal price, string percent, bool isDescount)
        {
            var percentD = percent.Replace("%", "").ToDecimal();
            decimal koef = 1;
            if (isDescount)
            {
                koef = (100 - percentD) / 100;
            }
            else
            {
                koef = (100 + percentD) / 100;
            }

            var result = price * koef;

            return result;
        }

        /// <summary>
        /// TODO добавить в настройки скидки опцию устанавливать красивую цену
        /// </summary>
        /// <param name="price"></param>
        /// <param name="downIfTensZero"></param>
        /// <returns></returns>
        private static decimal Apply99Postfix(decimal price, bool downIfTensZero = true)
        {
            var pInt = ((int)price) / 10;

            if (downIfTensZero && (pInt % 10 == 0))
            {
                pInt--;
            }

            price = pInt * 10 + 9.99m;

            return price;
        }

    }
}