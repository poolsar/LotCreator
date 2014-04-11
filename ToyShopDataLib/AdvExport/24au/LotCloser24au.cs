using System;
using System.IO;
using System.Net;
using RestSharp.Contrib;

namespace ToyShopDataLib.AdvExport
{
    internal class LotCloser24au
    {

        public static void Close(string number, string description)
        {
            HttpWebResponse response;

            if (Request_krsk_24au_ru(number, description, out response))
            {
                response.Close();
            }
        }

        private static bool Request_krsk_24au_ru(string number, string description, out HttpWebResponse response)
        {
            response = null;

            try
            {
                string url = string.Format("http://krsk.24au.ru/{0}/Close/", number);
                string urlReferer = string.Format("http://krsk.24au.ru/{0}/", number);

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
                request.Headers.Set(HttpRequestHeader.Cookie, @"_S_STAT=626915f2-8af0-43a1-8d73-cbf70191a504; idCityByIP=464; ASP.NET_SessionId=tvuaywcuob2q4ba35zuzu25a; lvidc=0; Hy701jhkafgPOOYWoehf=D38279808D9765F64517629F22EE250E2741B085E1A556ABE341A9FA6F40852E522857D0C6A9D4CB1A9D262D79FAFA4586FCB7D402358F0FFEB4864AD9D48835; 071ce61de6dafda9cc06b4d262bd0058=; is_adult=0; __atuvc=1%7C7; region=464; KIKkb2k323bJkaASDF923=mxWpVSfzKtAg2GFwAPsHV9ZYyIiX6w_l2jOO0lKZUjxR0j8nNb2k1ZYkEE_5otEOfJhR8uDE_pxZzBeLyH0f0aYOnjRAePnxH4tDsaKJ9iiuONRlAxdF9c81ix6ZeFa9Xs2wDHfVhMrfO3jlY8dL_7JblDVeSDeBAprji5gL3CLP5D8vvePj-zJaICKEvU4yKdSIDGt-HgMMI0a4Iyg_PTIPJgLFL0xy3HwdhHM8ZIxIwzrcL-Sp7hm3iQ-AfkM2PcaTyrXN-yvisA4dQSQUf3jxmRzc9GSxYmDV_0r4zSF6BiXYFLgdhrUkEqNHgSSkNt5XJaXiVTrsQVVuy_A3HEJwhICp-LMW9ditdTUvwZqQg_xka7jlM156e8VJsqVU9wxUT57LQn8Axct7jwlYNixjRVDG6pbdIJO0K4Fk0AC-TosSNfmD5cOwr2BBURc-t1UDG2l32a54hwPsK8ZFVcddamQ76_YuEbiDeqx5LALRIOe5OoxA08zVtS1Yf7VSI2OUF9IqGfFBxbhYGacUlqTxmWveVT3b5vqao1L3kqOb2WsgIQnzZ1M_QGWymK1XjQtLCq8cE-GeIYZc9Lb0DM1XjQySehGwwHw0TCEQnGnZ1dToIA0STmRh5V1LzDJhXJCKiziKLNT4pv5k5MEsp5ci2j281a0H_TNSzoIEFgrPxyOXaZl2tc_d-gdr4p3Q1dCO6BC6JX46eciihID3gF3nyDcNtdlGoGoEfOr_hQbv2QFzzXRzM5wqG7dmA8IRbfuFXv9bv9_DtoiDuvxp1ickJcH4mqoXY1GMmCQFLhdER3X8Wn3W8l8S4LXzsVTDApURA5nMziNdZQSVr5b2j5bOWT8uoemib_7r1QSyDklh0cnZCFsieWmNSzXSiGMP5IpJBfbX3BZ2B-V7VZ4m3f3ABogn5dsE59tVbp1qYqfgbO0xXzCWKOATz0TpOvwk0; _ga=GA1.2.943512218.1390976751");

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                //string body = @"deleteRemark=%D0%BE%D1%88%D0%B8%D0%B1%D0%BA%D0%B0";
                string body = "deleteRemark=" + HttpUtility.UrlEncode(description);
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
