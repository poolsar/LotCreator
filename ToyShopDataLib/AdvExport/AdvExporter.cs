using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyShopDataLib.AdvExport
{
    public abstract class AdvExporter
    {
        public abstract Uri BaseUri { get; }

        public virtual void Sync(List<Adv24au> advs = null)
        {
            ProccessMesenger.Write("Процедура реализации для площадки {0} не реализованна.", BaseUri);
        }

        //public abstract void CloseAdv(Adv24au adv);
        //public abstract void ExportAdv(Adv24au adv);

        //public static void Export(Adv24au adv)
        //{
        //    var exporter = GetExporter(adv);
        //    exporter.ExportAdv(adv);
        //}

        //public static void ExportMany(Adv24au[] advs)
        //{
        //    var exporter = GetExporter(advs.First());
        //    exporter.ExportAdvs(advs);
        //}

        //public static void Repost(params Adv24au[] advs)
        //{
        //    var exporter = GetExporter(advs.First());
        //    exporter.RepostAdv(advs);
        //}

        //public static void Close(Adv24au adv)
        //{
        //    var exporter = GetExporter(adv);
        //    exporter.CloseAdv(adv);
        //}

        public static void Sync(Marketplace marketplace, List<Adv24au> advs = null)
        {
            var exporter = GetExporter(marketplace);
            exporter.Sync(advs);
        }

        public static AdvExporter GetExporter(Marketplace marketplace)
        {
            var uri = marketplace.GetBaseUri();
            var exporter = exporters.FirstOrDefault(e => e.BaseUri.Host.ToLower() == uri.Host.ToLower());

            if (exporter == null)
            {
                throw new NotImplementedException(
                    string.Format("Экспорт на площадку {0} не реализован", marketplace.Url));
            }

            return exporter;
        }

        public virtual AdvPreparer GetPreparer()
        {
            var preparer = new AdvPreparer();
            return preparer;
        }

        //public static LotExporter GetExporter(Adv24au adv)
        //{
        //    var uri = adv.Marketplace.GetBaseUri();
        //    var exporter = exporters.FirstOrDefault(e => e.BaseUri.Host.ToLower() == uri.Host.ToLower());

        //    if (exporter == null)
        //    {
        //        throw new NotImplementedException(
        //            string.Format("Экспорт на площадку {0} не реализован", adv.Marketplace.Url));
        //    }

        //    return exporter;
        //}

        //private static LotExporter[] exporters = { new LotExporter24Au(), new LotExporter3gelania() };

        private static AdvExporter[] exporters = { new AdvExporter24Au(), new AdvExporterWebStore() };


        public static void ClearUnreadMsg()
        {
            var exporter24Au = exporters[0] as AdvExporter24Au;
            exporter24Au.ClearUnreadMsg();
        }

        public Marketplace GetMarketplace()
        {
            var marketplace = Context.Inst.MarketplaceSet.FirstOrDefault(m => m.Url == BaseUri.AbsoluteUri);
            return marketplace;
        }

        public List<Adv24au> GetAdvs()
        {
            var marketplace = GetMarketplace();
            var advs = marketplace.Advs.ToList();

            return advs;
        }

        
    }

    //internal class LotExporter3gelania : LotExporter
    //{
    //    public override Uri BaseUri
    //    {
    //        get { return new Uri("http://3gelania.ru"); }
    //    }

    //    public override void ExportAdv(Adv24au adv)
    //    {
    //        var webStoreController = new WebStoreLib.WebStoreController();
    //        webStoreController.Sync(adv);
    //    }
    //    public override void CloseAdv(Adv24au adv)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void RepostAdv(Adv24au[] advs)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void ExportAdv(Adv24au[] advs)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}

