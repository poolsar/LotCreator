using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace ToyShopDataLib.AdvExport
{
    internal class UploadImage24Au
    {
       
        public string MakeRequests(string imagePath)
        {
            string responseString = string.Empty;
            HttpWebResponse response;

            if (Request_24au_ru(imagePath, out response))
            {

                Stream responseStream = response.GetResponseStream();
                var streamReader = new StreamReader(responseStream);
                responseString = streamReader.ReadToEnd();

                try
                {
                    streamReader.Close();
                    streamReader.Dispose();
                    responseStream.Close();
                    responseStream.Dispose();
                }
                catch (Exception e)
                {


                }


                response.Close();
            }

            return responseString;
        }

        private static void WriteMultipartBodyToRequest(HttpWebRequest request, string body)
        {
            string[] multiparts = Regex.Split(body, @"<!>");
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                foreach (string part in multiparts)
                {
                    if (File.Exists(part))
                    {
                        bytes = File.ReadAllBytes(part);
                    }
                    else
                    {
                        bytes = System.Text.Encoding.UTF8.GetBytes(part.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n"));
                    }

                    ms.Write(bytes, 0, bytes.Length);
                }

                request.ContentLength = ms.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    ms.WriteTo(stream);
                }
            }
        }

        private bool Request_24au_ru(string imagePath, out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://24au.ru/imageupload/LoadMultiFile/");

                request.KeepAlive = true;
                request.Headers.Add("Origin", @"http://24au.ru");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1700.107 Safari/537.36";
                request.ContentType = "multipart/form-data; boundary=----WebKitFormBoundary6BwXZigBQetJHzph";
                request.Accept = "*/*";
                request.Referer = "http://24au.ru/new/ImageUploadFrame/?TextType=LOT_TEXT&ImageCollection=Temp&IsProUser=False&MaxPhotoCount=7&";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
                request.Headers.Set(HttpRequestHeader.Cookie, @"_S_STAT=626915f2-8af0-43a1-8d73-cbf70191a504; idCityByIP=464; ASP.NET_SessionId=tvuaywcuob2q4ba35zuzu25a; lvidc=0; Hy701jhkafgPOOYWoehf=D38279808D9765F64517629F22EE250E2741B085E1A556ABE341A9FA6F40852E522857D0C6A9D4CB1A9D262D79FAFA4586FCB7D402358F0FFEB4864AD9D48835; 071ce61de6dafda9cc06b4d262bd0058=; is_adult=0; region=464; _ga=GA1.2.943512218.1390976751");

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;


                var fileInfo = new FileInfo(imagePath);
                string body = @"------WebKitFormBoundary6BwXZigBQetJHzph
Content-Disposition: form-data; name=""file0""; filename=""" +  fileInfo.Name  + @"""
Content-Type: image/jpeg

<!>" + fileInfo.FullName + @"<!>
------WebKitFormBoundary6BwXZigBQetJHzph--
";
                WriteMultipartBodyToRequest(request, body);

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