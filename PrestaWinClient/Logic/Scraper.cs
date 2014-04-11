using ShopDataLib;

namespace PrestaWinClient.Logic
{
    public class Scraper
    {
        public static Supplier Scrape(string uri)
        {
            if (Scrapper24AuRu.CheckUri(uri))
            {
                var scrapper24AuRu = new Scrapper24AuRu();
                var product = scrapper24AuRu.ScraperProduct(uri);

                return product.Supplier;
            }

            return null;
        }
    }
}