using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BegemotImportLib;
using PrestaWinClient.Logic;
using Point = System.Windows.Point;
using Size = System.Drawing.Size;

namespace ImageMakerWpf
{
    /// <summary>
    /// Interaction logic for PinkLotCard.xaml
    /// </summary>
    public partial class PinkLotCard : Window, IShowCaseStyler
    {
        public PinkLotCard()
        {
            InitializeComponent();
        }

        private bool first = true;
        private int counter = -1;

        private DirectoryInfo imagesFolder;
        private DirectoryInfo sourceImagesFolder;

        private List<string> imagesToSkipDownload;
        private List<string> imagesToSkipProccess;

        private FileInfo[] imagesFiles;

        private Timer timer;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var testForm = new TestForm();
            //testForm.ShowDialog();
            //return;
            Start();
        }

        private List<BegemotPriceRow> lots;
        int index = -1;
        private PageLoader pageLoader;

        private void Start()
        {
            btnTest.Visibility = Visibility.Hidden;


            var begemotPriceRows = (new BegemotParser()).ParsePrice("");

            lots = begemotPriceRows.Where(r => r.CountInStock > 2).ToList();

            pageLoader = new PageLoader();

            sourceImagesFolder = new DirectoryInfo("images/SourceImages");
            imagesFolder = new DirectoryInfo("images");

            imagesToSkipDownload = sourceImagesFolder.GetFiles().Select(f => f.Name.Replace(f.Extension, "")).ToList();
            imagesToSkipProccess = imagesFolder.GetFiles().Select(f => f.Name.Replace(f.Extension, "")).ToList();

            timer = new Timer();
            timer.Interval = 1;
            timer.Tick += timer_Tick;

            timer.Start();
        }


        BegemotPriceRow lot;

        void timer_Tick(object sender, EventArgs e)
        {
            SaveScreenShot();

            while (true)
            {
                while (true)
                {
                    index++;

                    if (index >= lots.Count)
                    {
                        timer.Stop();
                        return;
                    }

                    Title = string.Format("{0} из {1}", index + 1, lots.Count);
                    lot = lots[index];
                    var articleStr = lot.Article.ToString();


                    var needToSkip = imagesToSkipProccess.Any(i => i == articleStr);
                    if (!needToSkip) break;
                }


                string uri = string.Format("http://www.begemott.ru/photos/{0}.jpg", lot.Article);
                string savePath = string.Format("{0}/{1}.jpg", sourceImagesFolder.FullName, lot.Article);


                bool noImage = false;
                try
                {
                    bool skipDownload = imagesToSkipDownload.Any(i => i == lot.Article.ToString());

                    if (!skipDownload)
                    {
                        pageLoader.RequestImage(uri, savePath);
                    }
                }
                catch (WebException we)
                {
                    noImage = true;

                }


                if (noImage)
                {
                    // обрабатываем следующий лот
                }
                else
                {

                    imgProductPhoto.Source = new BitmapImage(new Uri(savePath));
                    txtPrice.Text = lot.OldPrice.ToString("f2");
                    txtDescountPrice.Text = lot.DescountPrice.ToString("f2");
                    txtTitle.Text = lot.TitleNormal;

                    saveImageFlag = true;

                    break;
                }
            }
        }



        private bool saveImageFlag = false;
        private FileInfo imagesFile = null;



        private void SaveScreenShot()
        {
            if (!saveImageFlag) return;
            saveImageFlag = false;

            var Me = gridTest;

            var bitmap = new Bitmap((int)Me.ActualWidth, (int)Me.ActualHeight);
            var screenshot = Graphics.FromImage(bitmap);

            var pointToScreen = Me.PointToScreen(new Point(0, 0));

            screenshot.CopyFromScreen(
                (int)pointToScreen.X, (int)pointToScreen.Y, 0, 0,
                new Size((int)Me.ActualWidth, (int)Me.ActualHeight));

            var newFileName = string.Format("{0}/{1}.bmp", imagesFolder.FullName, lot.Article);
            bitmap.Save(newFileName, ImageFormat.Bmp);
        }

        


        public Window Host
        {
            get
            {
                return this;
            }
        }

        public Grid ScreenShotGrid
        {
            get
            {
                return gridTest;
            }
        }

        public bool Inited { get; private set; }

        public void Init(string product, decimal price, decimal priceOld, string imagePath, bool priceTopRight)
        {
            txtTitle.Text = product;
            txtDescountPrice.Text = price.ToString();
            txtPrice.Text = priceOld.ToString();
            imgProductPhoto.Source = new BitmapImage(new Uri(imagePath));
        }
    }
}
