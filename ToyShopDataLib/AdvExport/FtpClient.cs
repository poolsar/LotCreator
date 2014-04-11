using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

//using Transliteration;

namespace ToyShopDataLib.AdvExport
{
    public class MyFtpClient
    {
        private string ftpServerIP; // = "193.218.136.134/MODIS_DATA/update";
        private string ftpUserID;// = "VKurnosov";
        private string ftpPassword;// = "1";


        public MyFtpClient(string ftpServerIp, string ftpUserId, string ftpPassword)
        {
            ftpServerIP = ftpServerIp;
            ftpUserID = ftpUserId;
            this.ftpPassword = ftpPassword;
        }

        public string[] ListDirectory(string path = "")
        {
            string[] downloadFiles = null;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;

            bool complete = false;

            while (!complete)
            {
                try
                {
                    FtpWebRequest reqFTP;
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpServerIP + path));
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                    reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                    reqFTP.Proxy = null;
                    reqFTP.KeepAlive = false;
                    reqFTP.UsePassive = false;
                    response = reqFTP.GetResponse();
                    reader = new StreamReader(response.GetResponseStream());
                    string line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        return new string[0];
                    }

                    while (line != null)
                    {
                        result.Append(line);
                        result.Append("\n");
                        line = reader.ReadLine();
                    }
                    // to remove the trailing '\n'
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                    downloadFiles = result.ToString().Split('\n');

                    complete = true;


                }
                catch (Exception ex)
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                    if (response != null)
                    {
                        response.Close();
                    }
                    downloadFiles = null;

                }
            }

            return downloadFiles;
        }


        public string ReadFile(string fileName)
        {
            string uri = ftpServerIP + fileName;


            try
            {
                Uri serverUri = new Uri(uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    throw new ArgumentException("Неправильный URI ресурса");
                }
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(serverUri);
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();


                string resultUtf = string.Empty;
                using (var streamReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    resultUtf = streamReader.ReadToEnd();
                }

                string result = resultUtf;

                response.Close();

                return result;
            }
            //catch (WebException wEx)
            //{
            //    throw new WebException( "Download Error '{0}'", wEx);
            //}
            catch (Exception ex)
            {
                string err = string.Format("Download Error '{0}'", uri);
                throw new Exception(err, ex);
            }
        }




        public void WriteFileBinary(string fileName, string path)
        {
            WriteFile(fileName, path, true);
        }

        public void WriteFileText(string fileName, string text)
        {
            WriteFile(fileName, text, false);
        }

        private void WriteFile(string fileName, string text, bool isBinary = false)
        {
            int errorCounter = 0;
            int errorCounter2 = 0;
            bool isComplete = false;
            while (!isComplete)
            {
                try
                {
                    //FileInfo fi = new FileInfo(fileName);
                    string url = ftpServerIP + fileName;
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(url));
                    request.Method = WebRequestMethods.Ftp.UploadFile;

                    request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);



                    if (isBinary)
                    {
                        request.UseBinary = true;
                        request.KeepAlive = true;

                        var srcPath = text;

                        System.IO.FileInfo fi = new System.IO.FileInfo(srcPath);
                        request.ContentLength = fi.Length;
                        byte[] buffer = new byte[4097];
                        int bytes = 0;
                        int total_bytes = (int)fi.Length;
                        System.IO.FileStream fs = fi.OpenRead();
                        System.IO.Stream rs = request.GetRequestStream();
                        while (total_bytes > 0)
                        {
                            bytes = fs.Read(buffer, 0, buffer.Length);
                            rs.Write(buffer, 0, bytes);
                            total_bytes = total_bytes - bytes;
                        }
                        //fs.Flush();
                        fs.Close();
                        rs.Close();
                    }
                    else
                    {
                        Stream rs = request.GetRequestStream();
                        using (StreamWriter streamWriter = new StreamWriter(rs))
                        {
                            streamWriter.Write(text);
                        }
                        rs.Close();
                    }



                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    //string statusDescription = response.StatusDescription;
                    response.Close();

                    isComplete = true;
                }
                catch (Exception exc)
                {
                    errorCounter++;
                    if (errorCounter >= 10)
                    {
                        Thread.Sleep(10000);
                        //errorCounter = 0;
                        errorCounter2++;
                        if (errorCounter2 >= 10)
                        {
                            throw;
                        }

                    }
                }
            }


        }


        public void MakeDirectory(string path = "")
        {
            FtpWebResponse response = null;
            StreamReader reader = null;

            bool isSuccess = false;

            int errorCounter1 = 0;
            int errorCounter2 = 0;

            while (!isSuccess)
            {



                try
                {
                    FtpWebRequest reqFTP;
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpServerIP + path));
                    reqFTP.UseBinary = true;
                    reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.Proxy = null;
                    reqFTP.KeepAlive = false;
                    reqFTP.UsePassive = false;
                    response = (FtpWebResponse)reqFTP.GetResponse();
                    var ftpStatusCode = response.StatusCode;
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    isSuccess = false;

                    errorCounter1++;
                    if (errorCounter1 >= 10)
                    {
                        Thread.Sleep(1000);
                        errorCounter1 = 0;
                        errorCounter2++;

                        if (errorCounter2 >= 10)
                        {
                            throw ex;
                        }
                    }


                    if (reader != null)
                    {
                        reader.Close();
                    }
                    if (response != null)
                    {
                        response.Close();
                    }
                }

            }

        }








        private void FileToStream(string fileName, Stream requestStream)
        {
            const int bufferLength = 2048;
            byte[] buffer = new byte[bufferLength];
            int count = 0;
            int readBytes = 0;



            FileStream stream = File.OpenRead(fileName);

            do
            {
                readBytes = stream.Read(buffer, 0, bufferLength);
                requestStream.Write(buffer, 0, readBytes);
                count += readBytes;
            } while (readBytes != 0);
        }

        private static FileStream stream;
        private static StreamReader reader;
        private static StreamWriter writer;
        private readonly string indexPath = "c:/DigitTree.txt";

        public MyFtpClient()
        {
            throw new NotImplementedException();
        }

        public bool ScanWithSaveToIndex { get; set; }


        public bool CheckIndex()
        {
            var exists = File.Exists(indexPath);
            return exists;
        }

        public int LoadIndex()
        {
            if (reader == null)
            {
                stream = GetIndexFileStream();
                reader = new StreamReader(stream);
            }

            if (reader.EndOfStream)
            {
                return -1;
            }

            var line = reader.ReadLine();
            var number = int.Parse(line);


            WinConsole.WriteLazy("Загружен из индекса №{0}", number);

            return number;
        }

        public class WinConsole
        {
            public static void WriteLazy(string загруженИзИндекса, int number)
            {
                //throw new NotImplementedException();
            }
        }

        public void SaveIndex(int number)
        {
            if (writer == null)
            {
                stream = GetIndexFileStream();
                writer = new StreamWriter(stream);
            }

            writer.WriteLine(number);

            WinConsole.WriteLazy("Записан в индекс №{0}", number);
        }


        private FileStream GetIndexFileStream()
        {
            if (stream == null)
            {
                stream = new FileStream(indexPath, FileMode.OpenOrCreate);
            }

            return stream;
        }
    }
}