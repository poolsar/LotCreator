using System;
using System.Collections.Generic;
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

namespace ImageMakerWpf
{
    /// <summary>
    /// Interaction logic for StandartCard.xaml
    /// </summary>
    public partial class StandartCard : Window, IShowCaseStyler
    {
        public StandartCard()
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
            //, bool priceTopRight = true
            txtTitle.Text = product;
            imgProductPhoto.Source = new BitmapImage(new Uri(imagePath));

            DescountCanvas.Visibility = Visibility.Visible;
            DescountEllipse.Visibility = priceTopRight ? Visibility.Visible : Visibility.Hidden;


            bool noDescount = (price + 50) > priceOld;

            if (noDescount)
            {
                txtNoDescountPrice.Text = string.Format("{0:f0} р.", price);



                linePriceOld.Visibility = Visibility.Hidden;
                txtPrice.Visibility = Visibility.Hidden;
                txtDescountPrice.Visibility = Visibility.Hidden;
                txtAtAllDescount.Visibility = Visibility.Hidden;

                txtNoDescountPrice.Visibility = Visibility.Visible;
                txtAtAllNoDescount.Visibility = Visibility.Visible;
                //txtAtAllNoDescount.Visibility = Visibility.Hidden;



            }
            else
            {
                linePriceOld.X2 = priceOld >= 1000 ? 160 : 145;
                txtPrice.Text = priceOld.ToString("f2");
                txtDescountPrice.Text = price.ToString("f2");

                //Foreground="#ff0000"

                linePriceOld.Visibility = Visibility.Visible;
                txtPrice.Visibility = Visibility.Visible;
                txtDescountPrice.Visibility = Visibility.Visible;
                txtAtAllDescount.Visibility = Visibility.Visible;

                txtNoDescountPrice.Visibility = Visibility.Hidden;
                txtAtAllNoDescount.Visibility = Visibility.Hidden;


            }
        }
    }
}
