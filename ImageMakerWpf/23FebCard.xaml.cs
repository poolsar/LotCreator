using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BegemotImportLib;

namespace ImageMakerWpf
{
    /// <summary>
    /// Interaction logic for _23FebCard.xaml
    /// </summary>
    public partial class _23FebCard : Window, IShowCaseStyler
    {
        public _23FebCard()
        {
            InitializeComponent();
        }


        public static void ShowCard()
        {
            var febCard = new _23FebCard();
            febCard.ShowDialog();


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

        public bool Inited
        {
            get
            {
                return imgProductPhoto.IsLoaded;
            }
        }


        public void Init(string product, decimal price, decimal priceOld, string imagePath, bool priceTopRight)
        {

            linePriceOld.X2 = priceOld >= 1000 ? 160 : 145;

            txtTitle.Text = product;
            txtDescountPrice.Text = price.ToString("f2");
            txtPrice.Text = priceOld.ToString("f2");
            imgProductPhoto.Source = new BitmapImage(new Uri(imagePath));
        }
    }
}
