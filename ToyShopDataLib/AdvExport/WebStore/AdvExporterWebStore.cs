using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace ToyShopDataLib.AdvExport
{
    internal class AdvExporterWebStore : AdvExporter
    {
        public override Uri BaseUri
        {
            get { return new Uri("http://3gelania.ru"); }
        }

        //private void buildCSV()
        //{
        //    var dict = new Dictionary<string, string>
        //    {
        //        {"no", "Пропустить этот столбик"},
        //        {"id", "№"},
        //        {"active", "Активен (0/1)"},
        //        {"name", "Название*"},
        //        {"category", "Категории (x,y,z...)"},
        //        {"price_tex", "Цена без налогов"},
        //        {"price_tin", "Цена c налогами"},
        //        {"id_tax_rules_group", "ID налога"},
        //        {"wholesale_price", "Общая цена"},
        //        {"on_sale", "Распродажа (0/1)"},
        //        {"reduction_price", "Сумма скидки"},
        //        {"reduction_percent", "Процент скидки"},
        //        {"reduction_from", "Скидка действует от (гггг-мм-дд)"},
        //        {"reduction_to", "Скидка действует до (гггг-мм-дд)"},
        //        {"reference", "Артикул №"},
        //        {"supplier_reference", "Артикул поставщика №"},
        //        {"supplier", "Поставщик"},
        //        {"manufacturer", "Производитель"},
        //        {"ean13", "Штрихкод"},
        //        {"upc", "Универсальный Код Товара"},
        //        {"ecotax", "Эконалог"},
        //        {"width", "Ширина"},
        //        {"height", "Высота"},
        //        {"depth", "Глубина"},
        //        {"weight", "Вес"},
        //        {"quantity", "Количество"},
        //        {"minimal_quantity", "Минимальное количество"},
        //        {"visibility", "Видимость"},
        //        {"additional_shipping_cost", "Дополнительные расходы по доставке"},
        //        {"unity", "Единица изммерения"},
        //        {"unit_price_ratio", "Цена за единицу"},
        //        {"description_short", "Краткое описание"},
        //        {"description", "Описание"},
        //        {"tags", "Метки (x,y,z...)"},
        //        {"meta_title", "Мета-заголовок"},
        //        {"meta_keywords", "Мета ключевые слова"},
        //        {"meta_description", "Мета описание"},
        //        {"link_rewrite", "ЧПУ"},
        //        {"available_now", "Текст когда на складе"},
        //        {"available_later", "Текст, если предварительный заказ разрешен"},
        //        {"available_for_order", "Доступен для заказа (0= нет, 1 = да)"},
        //        {"available_date", "Дата появления товара в наличии"},
        //        {"date_add", "Дата создания товара"},
        //        {"show_price", "Отображать цену (0 = нет, 1 = да)"},
        //        {"image", "URL изображений (x,y,z...)"},
        //        {"delete_existing_images", "Удалить существующие изображения (0 = нет, 1 = да)"},
        //        {"features", "Свойство(Наименование:Стоимость:Позиция:Настроено)"},
        //        {"online_only", "Доступен только в режиме онлайн (0 = нет, 1 = да)"},
        //        {"condition", "Состояние"},
        //        {"customizable", "Настраиваемый (0 = Нет, 1 = Да)"},
        //        {"uploadable_files", "Загружаемые файлы (0 = Нет, 1 = Да)"},
        //        {"text_fields", "Текстовые поля (0 = Нет, 1 = Да)"},
        //        {"out_of_stock", "Действия когда нет в наличии"},
        //        {"shop", "ID / Название магазина"},
        //        {"advanced_stock_management", "Расширенное управление запасами"},
        //        {"depends_on_stock", "Depends on stock"},
        //        {"warehouse", "Склад"}
        //    };

        //    string[] csvFeilds = { "id", "name", "category" };
        //}

        public void ExportAdvCategory(params AdvCategory[] advCategories)
        {
            var webStoreController = new WebStoreLib.WebStoreController();
            foreach (var advCategory in advCategories)
            {
                webStoreController.Sync(advCategory);
            }
        }

        public void ExportAdvs(Adv24au[] advs)
        {
            var exportCats = advs.Where(a => a.Category.Number == 0 || a.Category.Number == null).Select(a => a.Category).ToArray();

            ExportAdvCategory(exportCats);

            var ftpClient = new MyFtpClient("ftp://localhost/", "poolsar", "rolton");


            var withNewImages = advs.Where(a => a.PrepareExport().UpdateImages).ToList();

            foreach (var adv in withNewImages)
            {
                // выкладываем картинку
                var srcImagePath = adv.Product.GetImagePath();
                var fileInfo = new FileInfo(srcImagePath);
                string filePath = string.Format("localhost/prestashop/imgimport/{0}", fileInfo.Name);
                ftpClient.WriteFileBinary(filePath, fileInfo.FullName);
                adv.Image = "http://" + filePath;
            }

            //  выкладываем csv

            var sb = new StringBuilder();
            string csvString;
            foreach (var adv in advs)
            {
                csvString = CSVConstants.GetCsvString(adv);
                sb.AppendLine(csvString);
            }
            csvString = sb.ToString();


            ftpClient.WriteFileText("localhost/prestashop/admin8706/import/20140325213015-imgimport2.csv", csvString);

            //  отправляем запрос на импорт
            string columnsBody = CSVConstants.GetCsvColumns();
            MakeRequests(columnsBody);

            var newAdvs = advs.Where(a => a.Number == 0).ToArray();
            if (newAdvs.Any())
            {
                WebStoreLib.WebStoreController.SetNumbersByIdsInReferences(newAdvs);
            }

            Context.Save();

        }

        public void ExportAdv(Adv24au adv)
        {
            var ftpClient = new MyFtpClient("ftp://localhost/", "poolsar", "rolton");

            // выкладываем картинку
            var srcImagePath = adv.Product.GetImagePath();
            var fileInfo = new FileInfo(srcImagePath);
            string filePath = string.Format("localhost/prestashop/imgimport/{0}", fileInfo.Name);
            ftpClient.WriteFileBinary(filePath, fileInfo.FullName);
            adv.Image = "http://" + filePath;


            //  выкладываем csv

            var sb = new StringBuilder();
            //  sb.AppendLine("\"ID налога\";\"Фото\";\"Имя\";\"Категория\";\"Цена\";\"Количество\";");

            var csvString = CSVConstants.GetCsvString(adv);
            sb.AppendLine(csvString);

            csvString = sb.ToString();

            ftpClient.WriteFileText("localhost/prestashop/admin8706/import/20140325213015-imgimport2.csv", csvString);

            //  отправляем запрос на импорт
            string columnsBody = CSVConstants.GetCsvColumns();
            MakeRequests(columnsBody);

            WebStoreLib.WebStoreController.SetNumbersByIdsInReferences(adv);

            Context.Save();

        }

        //var sb = new StringBuilder();
        //    sb.AppendLine("\"ID налога\";\"Фото\";\"Имя\";\"Категория\";\"Цена\";\"Количество\";");
        //    //sb.AppendLine(string.Format(
        //    //    "\"0\";\"http://localhost/prestashop/imgimport/123321.jpg\";\"{0}\";\"Куклы и аксессуары\";\"123.5\";\"8\";", adv.Title));
        //    sb.AppendLine(string.Format(
        //       "\"0\";\"http://localhost/prestashop/imgimport/123321.jpg\";\"{0}\";\"4\";\"123.5\";\"8\";", adv.Title));


        private void MakeRequests(string columnsBody)
        {
            HttpWebResponse response;
            string responseText;

            if (Request_localhost(columnsBody, out response))
            {
                responseText = ReadResponse(response);

                response.Close();
            }
        }

        private static string ReadResponse(HttpWebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                Stream streamToRead = responseStream;
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    streamToRead = new GZipStream(streamToRead, CompressionMode.Decompress);
                }
                else if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    streamToRead = new DeflateStream(streamToRead, CompressionMode.Decompress);
                }

                using (StreamReader streamReader = new StreamReader(streamToRead, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        private bool Request_localhost(string columnsBody, out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/prestashop/admin8706/index.php?controller=AdminImport&token=e713d23f9c6d31269e7afdec60f5c14b");

                request.KeepAlive = true;
                request.Headers.Set(HttpRequestHeader.CacheControl, "max-age=0");
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Origin", @"http://localhost");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = "http://localhost/prestashop/admin8706/index.php?controller=AdminImport&token=e713d23f9c6d31269e7afdec60f5c14b";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
                request.Headers.Set(HttpRequestHeader.Cookie, @"bc47c9955d44c3f8c727da724a4f4f64=bsGDeJz0uWQT9Ml45nODEGJCu8r07aQ2bHcp0uD80VYFpzeAspQnebwWsXG4pPXW0e0CgEiZy5AQDRhhhNDY9aTGS4XMjCJPYWanY8mU1NhyfRZaCL3iSfR9zq97Qjuhs%2FIfSZG1sFOAIFm8OF%2BR6HOHCielM81SoJoGpnhb0Q7BrP%2FbX0x7Uq8nlQZuIIh7000140; 784f9c2ccd57c7683791fd19a9ed3cf2=bsGDeJz0uWQT9Ml45nODEE%2FQN%2B8PEkzZWTVrVNhbYeUsRv37fK8WtvXju66o1BXKwF66ZxBDB%2BKgT45Co0wo6C0p1brn19xnl7i0H02JXpHSG%2B3JsWiN0CgbJ1rtkMS6kVtnMgWSvAZuTW4yJLw06lVBoBtEq3kKbR9shkq%2BRdJx5F9DKRjtKCEQi5T3xz4ChbKXwYatZ7oNgyqhwokCVVHPBvEsVjgmkSbB1zY1%2B1bvzBUPRnWcDwwhJ1zge9Ce5kUS6sH4ZuRwfSCTmrE21DBLoTscsrpxOJM5JONeow1tSLFe%2BrgC0tFn3ozSIeeDzRNAVsDPcX2gZ%2Frnlujs2WPuCrBnVeHqAlBL2mTpCW3Dmv9MeNBBUjFowgeaK7YE27ZyFuDZysjA8X3Ey1BDyMsbqlJpMy%2BV%2FnryBGFaLX73A3uHEMxw%2F80pqizKfutmHP6TV4pmc3IdoJq1taALU0GYUWuedMejL79vQj%2BHPYP3MY4m%2BCDABGLkOtX4Puy1PEfE15vsEGU%2BydCjVsOXDA%3D%3D000388; install_421aa90e079fa326b6494f812ad13e79=jpq4r0lrs5kjqefg4bfufri3b5; PHPSESSID=sijlav735pv0js9qn1o3rsrq91");

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;



                string body =
                    @"csv=20140325213015-imgimport2.csv&convert=&regenerate=&entity=1&iso_lang=ru&separator=%3B&multiple_value_separator=%2C&skip=0" + columnsBody + "&import=";
                ;//  + "&type_value%5B6%5D=no&import=";

                //string body2 =
                //    @"csv=20140325213015-imgimport2.csv&convert=&regenerate=&entity=1&iso_lang=ru&separator=%3B&multiple_value_separator=%2C&skip=1&type_value%5B0%5D=id_tax_rules_group&type_value%5B1%5D=image&type_value%5B2%5D=name&type_value%5B3%5D=category&type_value%5B4%5D=price_tex&type_value%5B5%5D=quantity&type_value%5B6%5D=no&import=";





                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();

                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
                else return false;
            }
            catch (Exception)
            {
                if (response != null) response.Close();
                return false;
            }

            return true;
        }
    }
}

