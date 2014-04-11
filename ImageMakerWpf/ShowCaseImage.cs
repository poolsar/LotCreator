using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

using Point = System.Windows.Point;
using Size = System.Drawing.Size;


namespace ImageMakerWpf
{
    public interface IShowCaseStyler
    {
        Window Host { get; }
        Grid ScreenShotGrid { get; }
        bool Inited { get; }

        void Init(string product, decimal price, decimal priceOld, string imagePath, bool priceTopRight);
    }
    public enum ShowCaseStyle
    {
        Standart = 0,
        Pink = 1,
        _23Feb = 2
    }

    public class ShowCaseImage
    {


        // private static int i = 0;
        //private DirectoryInfo _sourceImagesFolder;
        //private DirectoryInfo _saveImagesFolder;
        private ShowCaseStyle _defaultStyle;
        Dictionary<ShowCaseStyle, IShowCaseStyler> _stylerDict = new Dictionary<ShowCaseStyle, IShowCaseStyler>();

        public ShowCaseImage(ShowCaseStyle defaultStyle = ShowCaseStyle.Pink)
        {
            //_sourceImagesFolder = GetFolder(sourceImageDirectory);
            //_saveImagesFolder = GetFolder(saveImageDirectory);

            _defaultStyle = defaultStyle;
        }


        private IShowCaseStyler GetSyler(ShowCaseStyle? style = null)
        {
            var styleV = style ?? _defaultStyle;

            IShowCaseStyler styler;

            if (!_stylerDict.TryGetValue(styleV, out styler))
            {
                switch (styleV)
                {
                    case ShowCaseStyle.Standart:
                        styler = new StandartCard();
                        break;
                    case ShowCaseStyle.Pink:
                        styler = new PinkLotCard();
                        break;
                    case ShowCaseStyle._23Feb:
                        styler = new _23FebCard();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("style");
                }

                _stylerDict.Add(styleV, styler);
            }

            return styler;
        }

        private DirectoryInfo GetFolder(string path)
        {
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }

            return dir;
        }

        static IShowCaseStyler styler;


        private class ImageTask
        {
            public string Product { get; set; }
            public decimal Price { get; set; }
            public decimal PriceOld { get; set; }
            public string SourcePath { get; set; }
            public string SavePath { get; set; }

            public bool PriceTopRight { get; set; }

            public ImageTask(string product, decimal price, decimal priceOld, string sourcePath, string savePath, bool priceTopRight)
            {
                Product = product;
                Price = price;
                PriceOld = priceOld;
                SourcePath = sourcePath;
                SavePath = savePath;
                PriceTopRight = priceTopRight;

            }
        }

        List<ImageTask> tasks = new List<ImageTask>();

        public void AddTask(string product, decimal price, decimal priceOld, string sourcePath, string savePath, bool priceTopRight)
        {
            var task = new ImageTask(product, price, priceOld, sourcePath, savePath, priceTopRight);
            tasks.Add(task);
        }

        public void Execute()
        {
            styler = GetSyler();
            styler.Host.Visibility = Visibility.Visible;

            Action act = () =>
            {
                Thread.Sleep(1000);
                foreach (var t in tasks)
                {
                    Action updateVisual = () =>
                    {
                        //styler.Host.Visibility = Visibility.Hidden;
                        styler.Init(t.Product, t.Price, t.PriceOld, t.SourcePath,t.PriceTopRight);
                        //styler.Host.Visibility = Visibility.Visible;
                    };

                    styler.Host.Dispatcher.Invoke(updateVisual);

                    bool goNext = false;
                    while (!goNext)
                    {
                        Thread.Sleep(200);
                        styler.Host.Dispatcher.Invoke(() => goNext = styler.Inited);
                    }


                    Action makeScreenShot = () =>
                    {
                        MakeScreenShot(styler.ScreenShotGrid, t.SavePath);
                    };

                    styler.Host.Dispatcher.Invoke(makeScreenShot);

                }

                tasks.Clear();

                Action closeWin = () => styler.Host.Visibility = Visibility.Hidden;
                styler.Host.Dispatcher.BeginInvoke(closeWin);
            };

            var thread = new Thread(() => act());
            thread.Start();
        }


        static void MakeScreenShot(Grid Me, string savePath)
        {
            //var Me = gridTest;

            var bitmap = new Bitmap((int)Me.ActualWidth, (int)Me.ActualHeight);
            var screenshot = Graphics.FromImage(bitmap);

            var pointToScreen = Me.PointToScreen(new Point(0, 0));

            screenshot.CopyFromScreen(
                (int)pointToScreen.X, (int)pointToScreen.Y, 0, 0,
                new Size((int)Me.ActualWidth, (int)Me.ActualHeight));



            SaveBitmap(savePath, bitmap);
        }

        private static void SaveBitmap2(string savePath, Bitmap bitmap)
        {
            bitmap.Save(savePath, ImageFormat.Bmp);
        }

        private static void SaveBitmap(string savePath, Bitmap bitmap)
        {
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter;

            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bitmap.Save(savePath, jgpEncoder, myEncoderParameters);


            //myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            //myEncoderParameters.Param[0] = myEncoderParameter;
            //bitmap.Save(@"c:\TestPhotoQualityHundred.jpg", jgpEncoder, myEncoderParameters);
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }


        //private bool first = true;
        //private int counter = -1;

        //private DirectoryInfo imagesFolder;
        //private DirectoryInfo sourceImagesFolder;

        //private List<string> imagesToSkipDownload;
        //private List<string> imagesToSkipProccess;

        //private FileInfo[] imagesFiles;

        //private Timer timer;

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //var testForm = new TestForm();
        //    //testForm.ShowDialog();
        //    //return;
        //    Start();
        //}

        //private List<BegemotPriceRow> lots;
        //int index = -1;
        //private PageLoader pageLoader;




        //BegemotPriceRow lot;

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    SaveScreenShot();

        //    while (true)
        //    {
        //        while (true)
        //        {
        //            index++;

        //            if (index >= lots.Count)
        //            {
        //                timer.Stop();
        //                return;
        //            }

        //            Title = string.Format("{0} из {1}", index + 1, lots.Count);
        //            lot = lots[index];
        //            var articleStr = lot.Article.ToString();


        //            var needToSkip = imagesToSkipProccess.Any(i => i == articleStr);
        //            if (!needToSkip) break;
        //        }


        //        string uri = string.Format("http://www.begemott.ru/photos/{0}.jpg", lot.Article);
        //        string savePath = string.Format("{0}/{1}.jpg", sourceImagesFolder.FullName, lot.Article);


        //        bool noImage = false;
        //        try
        //        {
        //            bool skipDownload = imagesToSkipDownload.Any(i => i == lot.Article.ToString());

        //            if (!skipDownload)
        //            {
        //                pageLoader.RequestImage(uri, savePath);
        //            }
        //        }
        //        catch (WebException we)
        //        {
        //            noImage = true;

        //        }


        //        if (noImage)
        //        {
        //            // обрабатываем следующий лот
        //        }
        //        else
        //        {

        //            imgProductPhoto.Source = new BitmapImage(new Uri(savePath));
        //            txtPrice.Text = lot.OldPrice.ToString("f2");
        //            txtDescountPrice.Text = lot.DescountPrice.ToString("f2");
        //            txtTitle.Text = lot.TitleNormal;

        //            saveImageFlag = true;

        //            break;
        //        }
        //    }
        //}



        //private bool saveImageFlag = false;
        //private FileInfo imagesFile = null;



        //private void SaveScreenShot()
        //{
        //    if (!saveImageFlag) return;
        //    saveImageFlag = false;

        //    var Me = gridTest;

        //    var bitmap = new Bitmap((int)Me.ActualWidth, (int)Me.ActualHeight);
        //    var screenshot = Graphics.FromImage(bitmap);

        //    var pointToScreen = Me.PointToScreen(new Point(0, 0));

        //    screenshot.CopyFromScreen(
        //        (int)pointToScreen.X, (int)pointToScreen.Y, 0, 0,
        //        new Size((int)Me.ActualWidth, (int)Me.ActualHeight));

        //    var newFileName = string.Format("{0}/{1}.bmp", imagesFolder.FullName, lot.Article);
        //    bitmap.Save(newFileName, ImageFormat.Bmp);
        //}

    }
}