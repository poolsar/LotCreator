using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using RestSharp;
using RestSharp.Contrib;
using ToyShopDataLib.Utils;

namespace ToyShopDataLib.AdvExport
{
    public class AdvExporter24Au : AdvExporter
    {
        public override Uri BaseUri
        {
            get { return new Uri(URL); }
        }

        public const string URL = "http://24au.ru";

        public override void Sync(List<Adv24au> advs = null)
        {
            Message("Подготовка к синхронищации объявлений");

            if (advs == null)
            {
                advs = GetAdvs();
                advs = OrderAdvs(advs);
            }
            else
            {
                var marketplace = GetMarketplace();
                advs = advs.Where(a => a.Marketplace.Id == marketplace.Id).ToList();
            }

            var update = new List<AdvExportInfo>();
            var repost = new List<AdvExportInfo>();
            var close = new List<AdvExportInfo>();

            InitMessanger(advs);

            bool lastRepost = false;
            foreach (var adv in advs)
            {
                MessagePrepare(adv);

                var exportInfo = adv.PrepareExport();

                if (exportInfo.IsNoAction())
                {
                    continue;
                }

                // для соблюдения очередности сначала создаем и репостим
                // а потом где нужно изменяем

                if (exportInfo.Repost)
                {
                    lastRepost = true;
                    repost.Add(exportInfo);
                }

                if (exportInfo.Create)
                {
                    if (lastRepost)
                    {
                        MessagePost(repost.Last().Adv);

                        try
                        {
                            RepostAdvs(repost);
                        }
                        catch
                        {

                        }

                        repost.Clear();
                        lastRepost = false;
                    }

                    MessagePost(exportInfo.Adv);

                    try
                    {
                        PostAdv(exportInfo);
                    }
                    catch
                    {

                    }

                    continue;
                }

                if (exportInfo.Close)
                {
                    close.Add(exportInfo);
                    continue;
                }

                if (exportInfo.UpdateData || exportInfo.UpdateImages)
                {
                    update.Add(exportInfo);
                    continue;
                }
            }

            if (repost.Count > 0)
            {
                MessagePost(repost.Last().Adv);

                try
                {
                    RepostAdvs(repost);
                }
                catch
                {

                }
            }

            foreach (var exportInfo in close)
            {
                MessagePost(exportInfo.Adv);

                try
                {
                    CloseAdv(exportInfo.Adv);
                }
                catch
                {

                }
            }

            foreach (var exportInfo in update)
            {
                MessagePost(exportInfo.Adv);

                try
                {
                    PostAdv(exportInfo);
                }
                catch
                {

                }
            }

            Message("Объявления успешно синхронизованы");
        }

        int count;
        private Dictionary<Adv24au, int> counterDict;

        private void InitMessanger(List<Adv24au> advs)
        {
            count = advs.Count;
            counterDict = new Dictionary<Adv24au, int>();
            advs.ForEach(a => counterDict.Add(a, advs.IndexOf(a) + 1));

        }

        private void Message(string message, params object[] args)
        {
            ProccessMesenger.Write(message, args);
        }

        private void MessagePrepare(Adv24au adv)
        {
            var counter = counterDict[adv];
            Message("Подготовка синхронизации объявления {0} из {1}", counter, count);
        }

        private void MessagePost(Adv24au adv)
        {
            var counter = counterDict[adv];
            Message("Синхронизация объявления {0} из {1}", counter, count);
        }

        public override AdvPreparer GetPreparer()
        {
            var preparer = new AdvPreparer24Au();
            return preparer;
        }

        //public override void RepostAdv(params Adv24au[] advs)
        //{
        //    advs = advs.Where(a => a.Number != 0).ToArray();
        //    var lotsNumbers = advs.Select(a => a.Number).ToArray();
        //    RepostLots(lotsNumbers);
        //    foreach (var adv in advs)
        //    {
        //        adv.UpdateDateExpire();
        //    }

        //    Context.Save();
        //}

        private static List<Adv24au> OrderAdvs(List<Adv24au> advs)
        {
            // лоты в определенной последовательности
            advs = advs.OrderBy(a =>
            {
                var style = a.Product.GetSpecialStyle();
                var sTitle = style == null ? string.Empty : style.Title;
                return sTitle;
            }).ThenByDescending(a => a.Price).ToList();
            return advs;
        }

        private void RepostAdvs(List<AdvExportInfo> advExportInfos)
        {
            var advs = advExportInfos.Select(i => i.Adv).ToList();

            var sb = new StringBuilder();

            int repostedCount = 0;
            int packageСounter = 0;
            const int packageSize = 20;

            while (repostedCount < advs.Count)
            {
                List<Adv24au> package = advs.Skip(packageSize * packageСounter).Take(packageSize).ToList();

                if (package.Count < packageSize)
                {
                    //TODO  удостовериться, что кога после скипа остается элементов меньше чем 20, все нормально работает
                }

                sb.Clear();
                sb.Append("http://24au.ru/action.ashx?action=repeat_selected_lots&listlot=");

                foreach (var adv in package)
                {
                    sb.AppendFormat("{0};", adv.Number);
                }

                SendLotsRepost(sb);

                package.ForEach(a => a.UpdateDateExpire());

                repostedCount += package.Count;
                packageСounter++;
            }
        }

        public void CloseAdv(Adv24au adv)
        {
            if (adv.IsExpired()) return;
            LotCloser24au.Close(adv.Number.ToString(), "Лот закрыт");
            adv.OnClosed();
            Context.Save();
        }

        private void PostAdv(AdvExportInfo exportInfo)
        {
            if (exportInfo.IsNoAction() || exportInfo.IsRepostOnly()) return;

            bool postImages = exportInfo.Create || exportInfo.UpdateImages;
            if (postImages) PostImages(exportInfo);

            var adv = exportInfo.Adv;


            string formBody = CreateRequestBody(adv, exportInfo.Create);

            if (exportInfo.Create)
            {
                var lotNumber = PostCreateLot(formBody);
                adv.Number = Convert.ToInt32(lotNumber);
                adv.Published = true;
                adv.UpdateDateExpire();
            }
            else
            {

                PostEditLot(formBody);
                adv.UpdateDateExpire();
            }

            Context.Save();
        }

        private void PostImages(AdvExportInfo exportInfo)
        {
            var images = exportInfo.Adv.AdvImages.Where(i => i.NeedUpload()).ToList();

            foreach (var image in images)
            {
                var imagePath = image.GetImagePath();
                var uploadData = UploadImage(imagePath);
                image.SetUploadData(uploadData);

                Context.Save();
            }
        }


        //public void ExportLotAuto(Adv24au adv, bool needRepost = false)
        //{
        //    bool isNeedUpdate = false;

        //    var advImages = adv.AdvImages.ToList();

        //    foreach (var advImage in advImages)
        //    {
        //        if (advImage.NeedUpload())
        //        {
        //            var imagePath = advImage.GetImagePath();
        //            var uploadData = UploadImage(imagePath);
        //            advImage.SetUploadData(uploadData);
        //            Context.Save();

        //            isNeedUpdate = true;
        //        }
        //    }

        //    bool create = !adv.Published;
        //    isNeedUpdate |= adv.IsNeedUpdateData();
        //    bool isNeedRepost = needRepost || adv.IsExpired();

        //    //isNeedUpdate = true;

        //    if (isNeedUpdate) adv.CommitExport();


        //    var price = ((int)adv.Price).ToString();

        //    if (adv.Days == 0)
        //    {
        //        adv.Days = Adv24au.DeafaultDays;
        //    }

        //    string formBody = string.Empty;

        //    bool createBody = isNeedUpdate || create;
        //    if (createBody)
        //    {
        //        formBody = CreateRequestBody(adv.Title, adv.Description, price, adv.Image, adv.Number.ToString(), adv.Days, adv.Category.Number.Value, create);
        //    }
        //    if (create)
        //    {
        //        var lotNumber = PostCreateLot(formBody);
        //        adv.Number = Convert.ToInt32(lotNumber);
        //        adv.Published = true;
        //        adv.UpdateDateExpire();
        //    }
        //    else
        //    {
        //        if (isNeedRepost)
        //        {
        //            if (createBody)
        //            {
        //                RepostLots(adv.Number, formBody);
        //            }
        //            else
        //            {
        //                RepostLots(adv.Number);
        //            }


        //            adv.UpdateDateExpire();
        //        }
        //        else if (isNeedUpdate)
        //        {
        //            PostEditLot(formBody);
        //        }
        //    }

        //    adv.Published = true;

        //    Context.Save();
        //}









        private string UploadImage(string imagePath)
        {
            var uploadImage = new UploadImage24Au();

            string makeRequests = uploadImage.MakeRequests(imagePath);

            //makeRequests = "{\"Status\":\"Success\",\"Files\":[{\"Id\":\"52ff265dd17dce177c1ce7b4\",\"Name\":\"123\",\"Status\":\"Success\",\"ErrorText\":null,\"Url\":\"http://media2.24aul.ru/temp/120x90/52ff265dd17dce177c1ce7b4\"}]}";

            var o = RestSharp.SimpleJson.DeserializeObject(makeRequests) as JsonObject;
            var filesArr = o["Files"] as JsonArray;
            var files = filesArr[0] as JsonObject;
            string imageId = files["Id"].ToString();

            return imageId;

        }


        public string CreateRequestBody(Adv24au adv, bool isCreate = false)
        {
            //(string name, string description, decimal price, string imageId, int lotNumber, int days, int category, bool isCreate = false)
            //adv.Title, adv.Description, adv.Price, adv.AdvImages, adv.Number, adv.Days, adv.Category.Number.Value, exportInfo.Create



            var forms = new Dictionary<string, string>();
            forms.Add("Name", adv.Title);
            forms.Add("Cat1", "928"); // Дети растут
            forms.Add("Cat2", "933"); // Игрушки 

            //forms.Add("Cat3", "934"); // Мягкие
            //forms.Add("Cat3", "937"); // Машины и Техника
            //forms.Add("Cat3", "936"); // Куклы и аксессуары
            forms.Add("Cat3", adv.Category.Number.Value.ToString());

            forms.Add("selectCategoryType", "fromManual");
            forms.Add("ListProperties[0].Id", "447"); // состояние
            forms.Add("ListProperties[0].Value", "18791"); // новое

            var advImages = adv.AdvImages.OrderBy(i => i.Order).ToList();
            for (int i = 0; i < advImages.Count; i++)
            {
                var advImage = advImages[i];
                var key = string.Format("ImageIds[{0}]", i);
                var value = advImage.GetUploadData();
                forms.Add(key, value);
            }

            forms.Add("imageloadstatus", "SUCCESS");

            forms.Add("Description", adv.Description);
            forms.Add("TypeAuction", "ONLY_BLITZ");
            forms.Add("StandartStartPrice", "");
            forms.Add("StandartBlitzPrice", "");
            forms.Add("RoubleBlitzPrice", "");
            forms.Add("OnlyBlitzPrice", adv.Price.ToString("F0"));
            forms.Add("ReverseStartPrice", "");
            forms.Add("MinPrice", "");

            // Для создания


            forms.Add("Days", adv.Days.ToString()); // Продолжительность торгов


            // Расположено Красноярск Тотмина 14
            forms.Add("Landmark.Id", "10");
            forms.Add("Landmark.IdRegion", "464");
            forms.Add("Latitude", "56,0318984985352");
            forms.Add("Longitude", "92,7762985229492");

            forms.Add("Place", "Самовывоз. Возможна доставка по договоренности."); // Условие передачи
            forms.Add("ExtraContactInfo", "");
            forms.Add("IdRegion", "464");
            forms.Add("UseAutoRepeat", "false"); // Авто-перевыставление лота



            if (!isCreate)
            {
                forms.Add("Id", adv.Number.ToString()); // номер лота
            }

            var sb = new StringBuilder();
            bool first = true;
            foreach (var param in forms.Keys)
            {
                string value = HttpUtility.UrlEncode(forms[param]);

                if (!first) sb.Append("&");
                first = false;

                sb.AppendFormat("{0}={1}", param, value);
            }


            string formBody = sb.ToString();

            return formBody;


        }

        private int PostCreateLot(string formBody)
        {
            string url = "http://krsk.24au.ru/new/";

            HttpWebResponse response;

            int lotNumber = 0;
            if (PostForm(url, url, formBody, out response))
            {
                var responseString = ReadResponse(response);
                lotNumber = Adv24au.ParseCreateLotResponse(responseString);

                response.Close();
            }

            return lotNumber;
        }

        private string ReadResponse(HttpWebResponse response)
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

                string readToEnd;
                //using (StreamReader streamReader = new StreamReader(streamToRead))
                //{
                //    readToEnd = streamReader.ReadToEnd();
                //}

                //using (StreamReader streamReader =
                //   new StreamReader(streamToRead, Encoding.GetEncoding(response.CharacterSet)))
                //{
                //    readToEnd = streamReader.ReadToEnd();
                //}


                bool is1251 = response.CharacterSet.Contains("1251");

                var encoding = is1251 ? Encoding.GetEncoding("windows-1251") : Encoding.UTF8;

                using (StreamReader streamReader =
                    new StreamReader(streamToRead, encoding))
                {
                    readToEnd = streamReader.ReadToEnd();
                }

                return readToEnd;

                //using (StreamReader streamReader = new StreamReader(streamToRead, Encoding.UTF8))
                //{
                //    return streamReader.ReadToEnd();
                //}
            }
        }

        private void PostEditLot(string formBody)
        {
            string url = "http://krsk.24au.ru/new/Edit/";

            HttpWebResponse response;

            if (PostForm(url, url, formBody, out response))
            {
                response.Close();
            }
        }

        //private void RepostLots(int lotNumber, string formBody)
        //{
        //    string url = "http://www.24au.ru/new/Anew/";
        //    string referer = string.Format("http://24au.ru/new/Anew/{0}/", lotNumber);

        //    HttpWebResponse response;

        //    if (PostForm(url, referer, formBody, out response))
        //    {
        //        response.Close();
        //    }
        //}



        private void SendLotsRepost(StringBuilder sb)
        {
            string url = sb.ToString();
            string referer = string.Format("http://24au.ru/user/me/lots/closed/?");

            HttpWebResponse response;
            if (GetRequest(url, referer, out response))
            {
                response.Close();
            }
        }

        //private void PostRepostLot(string formBody, string lotNumber)
        //{

        //    ////http://24au.ru/action.ashx?action=repeat_selected_lots&listlot=3799419;3799418;3799417;
        //    //string url = "http://www.24au.ru/new/Anew/";
        //    //string referer = string.Format("http://24au.ru/new/Anew/{0}/", lotNumber);

        //    //HttpWebResponse response;

        //    //if (PostForm(url, referer, formBody, out response))
        //    //{
        //    //    response.Close();
        //    //}



        //    RepostLots()
        //}

        private string SendGetRequest(string url, string urlReferer)
        {


            string respString = string.Empty;

            HttpWebResponse response;
            if (GetRequest(url, urlReferer, out response))
            {
                respString = ReadResponse(response);
                response.Close();
            }
            return respString;
        }

        private bool GetRequest(string url, string urlReferer, out HttpWebResponse response)
        {
            return MakeRequest(url, urlReferer, RequestMethod.Get, null, out response);
        }

        private bool PostForm(string url, string urlReferer, string formBody, out HttpWebResponse response)
        {
            return MakeRequest(url, urlReferer, RequestMethod.Post, formBody, out response);
        }

        private enum RequestMethod
        {
            Get, Post
        }

        private bool MakeRequest(string url, string urlReferer, RequestMethod method, string formBody, out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.KeepAlive = true;
                request.Headers.Set(HttpRequestHeader.CacheControl, "max-age=0");
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Headers.Add("Origin", @"http://krsk.24au.ru");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.107 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = urlReferer;
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
                request.Headers.Set(HttpRequestHeader.Cookie, @"_S_STAT=626915f2-8af0-43a1-8d73-cbf70191a504; idCityByIP=464; ASP.NET_SessionId=tvuaywcuob2q4ba35zuzu25a; lvidc=0; Hy701jhkafgPOOYWoehf=D38279808D9765F64517629F22EE250E2741B085E1A556ABE341A9FA6F40852E522857D0C6A9D4CB1A9D262D79FAFA4586FCB7D402358F0FFEB4864AD9D48835; 071ce61de6dafda9cc06b4d262bd0058=; is_adult=0; region=464; _ga=GA1.2.943512218.1390976751");

                switch (method)
                {
                    case RequestMethod.Get:
                        request.Method = "GET";
                        break;
                    case RequestMethod.Post:
                        request.Method = "POST";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("method");
                }

                request.ServicePoint.Expect100Continue = false;

                if (!formBody.IsEmpty())
                {
                    byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(formBody);
                    request.ContentLength = postBytes.Length;
                    Stream stream = request.GetRequestStream();
                    stream.Write(postBytes, 0, postBytes.Length);
                    stream.Close();
                }

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


        public void ClearUnreadMsg()
        {
            string msgPage = RespMessages();
            int unreadCount = Adv24au.ParseUnreadMsgCount(msgPage);

            int readedCount = 0;


            int pageCounter = 1;
            bool uncomplete = true;
            while (uncomplete)
            {
                var numbers = Adv24au.ParseUnreadMsgNumbers(msgPage);

                if (numbers.Count > 0)
                {
                    SetMessagesReaded(numbers);
                }

                readedCount += numbers.Count;
                uncomplete = readedCount < unreadCount;
                if (uncomplete)
                {
                    pageCounter++;
                    msgPage = RespMessages(pageCounter);
                }
            }

        }

        private string RespMessages(int pageNumber = 1)
        {
            string url = @"http://24au.ru/messages/auto/?page=" + pageNumber;

            string respString = SendGetRequest(url, url);

            return respString;
        }

        private void SetMessagesReaded(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0) return;

            string url = @"http://24au.ru/action.ashx?action=read_selected_messages&listmes=";

            foreach (var number in numbers)
            {
                url += string.Format("{0};", number);
            }

            string urlRef = @"http://24au.ru/messages/auto/";

            SendGetRequest(url, urlRef);

        }
    }
}