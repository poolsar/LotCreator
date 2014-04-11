using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyShopDataLib
{
    public class Context
    {
        public const decimal BegemotDescount = 15;
        public const decimal BegemotDescountCoef = (100 - BegemotDescount) / 100m;


        public const decimal OldPriceCoefficient = 1.15m;

        public const decimal MinMargin = 200;
        public const decimal MaxMargin = 500;


        const string _imageSourceFolder = @"C:\mytemp\SourceImages";
        const string _imageSaveFolder = @"C:\mytemp\SaveImages";

        private static ToyShopModelContainer _inst;

        public static ToyShopModelContainer Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new ToyShopModelContainer();
                }
                return _inst;
            }
        }



        public static void Save()
        {
            try
            {
                Inst.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ImportBegemot()
        {
            var begemotImporter = new BegemotImporter();
            begemotImporter.Import();
        }

        public static decimal WithBegemotDescount(decimal price)
        {
            var result = price * (1 - (BegemotDescount / 100));
            return result;
        }

        public static string ImageSourceFolder
        {
            get
            {
                if (!Directory.Exists(_imageSourceFolder))
                {
                    Directory.CreateDirectory(_imageSourceFolder);
                }

                return _imageSourceFolder;
            }
        }

        public static string ImageSaveFolder
        {
            get
            {
                if (!Directory.Exists(_imageSaveFolder))
                {
                    Directory.CreateDirectory(_imageSaveFolder);
                }

                return _imageSaveFolder;
            }
        }


    }


}
