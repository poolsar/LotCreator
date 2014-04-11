using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace PrestaWinClient.Logic
{
    public class PageLoader
    {
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


        public HtmlResponse RequestsHtml(string uri, bool simple = false, bool decompress = false)
        {
            HtmlResponse responseContent = new HtmlResponse();

            HttpWebResponse response;

            var requestResult = Request(uri, simple, decompress);
            response = requestResult.Response;

            if (requestResult.IsSuccess)
            {
                //Stream responseStream = response.GetResponseStream();
                //var streamReader = new StreamReader(responseStream);
                //responseContent.Html = streamReader.ReadToEnd();
                responseContent.Html = ReadResponse(response);

                //try
                //{
                //    streamReader.Close();
                //    streamReader.Dispose();
                //    responseStream.Close();
                //    responseStream.Dispose();
                //}
                //catch (Exception e)
                //{


                //}

                response.Close();
            }
            else if (requestResult.IsWebException)
            {
                responseContent.IsRequestError = true;
                responseContent.RequestError = response.StatusCode.ToString();
            }
            else
            {
                responseContent.IsRequestError = true;
                responseContent.RequestError = "UndefinedRequestError";
            }

            return responseContent;
        }


        protected RequestResult Request(string uri, bool simple = false, bool decompress = false)
        {
            HttpWebResponse response = null;
            RequestResult result = new RequestResult();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

                if (!simple)
                {
                    
                    request.KeepAlive = true;
                    request.Headers.Set(HttpRequestHeader.CacheControl, "max-age=0");
                    //request.Accept = "*/*";
                    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";

                }

                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31";

                if (!simple)
                {

                    request.Referer = "https://www.google.ru/";
                    request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                    request.Headers.Set(HttpRequestHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");

                    //request.Headers.Add("X-Requested-With", @"XMLHttpRequest");
                    //request.Headers.Set(HttpRequestHeader.AcceptCharset, "windows-1251,utf-8;q=0.7,*;q=0.3");
                    
                    //request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }

                if (decompress)
                {
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }

                response = (HttpWebResponse)request.GetResponse();
                result.IsSuccess = true;
            }
            catch (WebException e)
            {
                throw e;
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)e.Response;
                    result.IsWebException = true;
                }

                result.IsSuccess = false;

            }
            catch (Exception e)
            {
                throw e;
                if (response != null) response.Close();
                result.IsSuccess = false;
            }

            result.Response = response;

            return result;
        }



        public bool RequestImage(string uri, string savePath)
        {
            HttpWebResponse response = null;

            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Set(HttpRequestHeader.CacheControl, "max-age=0");
                webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31");
                webClient.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
                webClient.Headers.Set(HttpRequestHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");


                webClient.DownloadFile(uri, savePath);
            }
            catch (WebException e)
            {
                throw e;

                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
                else return false;


            }
            catch (Exception e)
            {
                throw e;

                if (response != null) response.Close();
                return false;
            }

            return true;
        }
    }

    public class HtmlResponse
    {
        public string Html { get; set; }

        public bool IsRequestError { get; set; }

        public string RequestError { get; set; }
    }

    public class RequestResult
    {
        public HttpWebResponse Response { get; set; }
        public bool IsWebException { get; set; }
        public bool IsSuccess { get; set; }
    }

}