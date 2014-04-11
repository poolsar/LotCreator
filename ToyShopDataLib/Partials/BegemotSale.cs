using System;

namespace ToyShopDataLib
{
    public partial class BegemotSale
    {
        public void UpdateActive()
        {
            var now = DateTime.Now;
            Active = DateStart <= now && now < DateStop;
        }
    }
}