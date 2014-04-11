//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.IO;
//using System.Net;
//using System.Text.RegularExpressions;
//using System.Web;
//using System.Windows.Forms;
//using System.Xml;
//using RestSharp;

//namespace PrestaWinClient
//{

//    public class AdminController
//    {




//        /// <summary>
//        /// Tries to request the URL: http://192.168.0.8/ladushki17/admin8056/index.php?controller=AdminMaintenance&amp;token=de54e4c2384e380be21283c482a3d69c
//        /// </summary>
//        /// <param name="response">After the function has finished, will possibly contain the response to the request.</param>

//        /// <returns>True if the request was successful; false otherwise.</returns>



//        public void TurnOffMarket()
//        {
//            MakeRequests(false);
//        }
//        public void TurnOnMarket()
//        {
//            MakeRequests(true);
//        }

//        //Calls request functions sequentially.
//        private void MakeRequests(bool isOpen)
//        {
//            HttpWebResponse response;

//            if (Request_192_168_0_8(isOpen, out response))
//            {
//                //Success, possibly use response.
//                response.Close();
//            }
//            else
//            {
//                //Failure, cannot use response.
//            }
//        }


//        private bool Request_192_168_0_8(bool isOpen, out HttpWebResponse response)
//        {
//            response = null;

//            try
//            {
//                //Create request to URL.
//                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.0.8/ladushki17/admin8056/index.php?controller=AdminMaintenance&token=de54e4c2384e380be21283c482a3d69c");

//                //Set request headers.
//                request.KeepAlive = true;
//                request.Headers.Set(HttpRequestHeader.Authorization, "Basic UkYxRkVHU0pXSzgyV09GU0VSSERBMkNBQU8yMVpDOFc6");
//                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
//                request.Headers.Add("Origin", @"http://192.168.0.8");
//                request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.101 Safari/537.36";
//                request.ContentType = "multipart/form-data; boundary=----WebKitFormBoundarypMHYmQrlwYWD3Egj";
//                request.Referer = "http://192.168.0.8/ladushki17/admin8056/index.php?controller=AdminMaintenance&token=de54e4c2384e380be21283c482a3d69c";
//                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch");
//                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
//                request.Headers.Set(HttpRequestHeader.Cookie, @"6bc890cb7b5704cff62e4f70e6f2fe91=9ho5pVX9XRL%2BHjrKVrXrC2VcSM7BMM%2BPES64ocogiMY5WRTW0AUavAvdAyKnW3CuPafQQEDggR3u7V%2BX3%2B2D93Dkz9csHqUlgxG9X2wx4UfeGKcovVfO9ETJVlQVWMNFbQKuTeE8dbIY8t9uZDCNdR7bA%2BNr2Jakmydr7gOxOlQW2D6E0%2BtkYIapvmY0KSMqQ%2BeEmPG0PM9QYWB067EX8GIH3ZeS75s%2F17MrJT0ack0%3D000161; b9fbede7e69b9aba478def168a78e3a9=9ho5pVX9XRL%2BHjrKVrXrC0idcvRXnlarhHudswLshKaEc%2FXqPsVFm%2F%2BbkVqyQAlYbibjb2jNPn1GF7aCO6gLn4fn02Q2ovoCKvBnPL5%2Fixce0gY%2F18MpBN%2Bd2PhjTmLYMPaYjYykXFhpzEF4xnKYW%2Bqxz4MeBO7MXjGcwf5psmUpbnZL5j0yBP7mB7f%2BZ336JERvaqWNc%2BNbb49qoXlMmKNB%2BwtPrwH53HMiS1fj2eDzRFXPChExoW%2F8BD4SzNXvhHLMzlGfb10tlrLcuqr4VU4OYrkTeg5hKJIs%2FELvVa4raUaGSFXTaAzqLoLGegxf000232; install_f4e4c0ae3ffcc5f281329a6db3665e03=2mcdmaeim3b666g9lh2u144h43");

//                //Set request method
//                request.Method = "POST";

//                // Disable 'Expect: 100-continue' behavior. More info: http://haacked.com/archive/2004/05/15/http-web-request-expect-100-continue.aspx
//                request.ServicePoint.Expect100Continue = false;

//                //Set request body.
//                string body = string.Format(@"------WebKitFormBoundarypMHYmQrlwYWD3Egj
//Content-Disposition: form-data; name=""ps_shop_enable""
//
//{0}
//------WebKitFormBoundarypMHYmQrlwYWD3Egj
//Content-Disposition: form-data; name=""ps_maintenance_ip""
//
//
//------WebKitFormBoundarypMHYmQrlwYWD3Egj
//Content-Disposition: form-data; name=""submitoptionsconfiguration""
//
//1
//------WebKitFormBoundarypMHYmQrlwYWD3Egj--
//", isOpen ? "1" : "0");


//                var httpWebRequest = new HttpWebRequest();


//                WriteMultipartBodyToRequest(request, body);

//                //Get response to request.
//                response = (HttpWebResponse)request.GetResponse();





//            }
//            catch (WebException e)
//            {
//                //ProtocolError indicates a valid HTTP response, but with a non-200 status code (e.g. 304 Not Modified, 404 Not Found)
//                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
//                else return false;
//            }
//            catch (Exception)
//            {
//                if (response != null) response.Close();
//                return false;
//            }

//            return true;
//        }


//        private void turn()
//        {
//            var request = new RestRequest();
//            //request.Resource = Resource;
//            request.Method = Method.POST;
//            //request.RequestFormat = DataFormat.Xml;
//            request.AddHeader("Content-Type", "multipart/form-data; boundary=----WebKitFormBoundarylBQg5XLJzGBdvO9B");


            
            
//            //Connection: keep-alive
//            //Content-Length: 364
//            //Authorization: Basic UkYxRkVHU0pXSzgyV09GU0VSSERBMkNBQU8yMVpDOFc6
//            //Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
//            //Origin: http://192.168.0.8
//            //User-Agent: Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.101 Safari/537.36

//            //Content-Type: multipart/form-data; boundary=----WebKitFormBoundarylBQg5XLJzGBdvO9B

//            //Referer: http://192.168.0.8/ladushki17/admin8056/index.php?controller=AdminMaintenance&token=de54e4c2384e380be21283c482a3d69c

//            //Accept-Encoding: gzip,deflate,sdch
//            //Accept-Language: ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4





//            string body = 

//@"------WebKitFormBoundarylBQg5XLJzGBdvO9B
//Content-Disposition: form-data; name=!@#PS_SHOP_ENABLE!@#
//
//1
//------WebKitFormBoundarylBQg5XLJzGBdvO9B
//Content-Disposition: form-data; name=!@#PS_MAINTENANCE_IP!@#
//
//
//------WebKitFormBoundarylBQg5XLJzGBdvO9B
//Content-Disposition: form-data; name=!@#submitOptionsconfiguration!@#
//
//1
//------WebKitFormBoundarylBQg5XLJzGBdvO9B--".Replace("!@#", "\"");


//            request.AddBody(body);



//            //Hack implementation in PrestaSharpSerializer to serialize PrestaSharp.Entities.AuxEntities.language
//            request.XmlSerializer = new PrestaSharp.Serializers.PrestaSharpSerializer();
//            string serialized = ((PrestaSharp.Serializers.PrestaSharpSerializer)request.XmlSerializer).PrestaSharpSerialize(Entity);
//            serialized = "<prestashop>\n" + serialized + "\n</prestashop>";
//            request.AddParameter("xml", serialized);


//            return request;
//        }

//        //Parses and writes the multipart body to the web request.
//        private static void WriteMultipartBodyToRequest(HttpWebRequest request, string body)
//        {
//            string[] multiparts = Regex.Split(body, @"<!>");
//            byte[] bytes;
//            using (MemoryStream ms = new MemoryStream())
//            {
//                foreach (string part in multiparts)
//                {
//                    //Determine if part is plain text or "<!>" line.
//                    if (File.Exists(part))
//                    {
//                        bytes = File.ReadAllBytes(part);
//                    }
//                    else
//                    {
//                        bytes = System.Text.Encoding.UTF8.GetBytes(part.Replace("\n", "\r\n"));
//                    }

//                    ms.Write(bytes, 0, bytes.Length);
//                }

//                request.ContentLength = ms.Length;
//                using (Stream stream = request.GetRequestStream())
//                {
//                    ms.WriteTo(stream);
//                }
//            }
//        }

//    }
//}

