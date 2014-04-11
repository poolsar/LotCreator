using System;
using System.Linq;
using PrestaSharp.Entities;
using PrestaSharp.Factories;
using ShopDataLib;

namespace WebStoreLib
{
    public class FactoryHost
    {
        //static string BaseUrl = "http://192.168.0.8/ladushki17/api";
        //static string Account = "RF1FEGSJWK82WOFSERHDA2CAAO21ZC8W";
        static string Password = "";

        static string BaseUrl
        {
            get
            {
                return Settings.WebStoreUri + @"/api";
            }
        }

        
        static string Account{
            get
            {
                return Settings.ServiceKey;
            }
        }

         

        public static CategoryFactory CategoryFactory
        {
            get { return _categoryFactory ?? (_categoryFactory = F<CategoryFactory>()); }
        }

        public static ProductFactory ProductFactory
        {
            get { return _productFactory ?? (_productFactory = F<ProductFactory>()); }
        }

        public static LanguageFactory LanguageFactory
        {
            get { return _languageFactory ?? (_languageFactory = F<LanguageFactory>()); }
        }

        public static StockAvailableFactory StockAvailableFactory
        {
            get { return _stockAvailableFactory ?? (_stockAvailableFactory = F<StockAvailableFactory>()); }
        }

        public static ImageFactory ImageFactory
        {
            get { return _imageFactory ?? (_imageFactory = F<ImageFactory>()); }
        }

        public static language RusLang
        {
            get
            {
                if (_rusLang == null)
                {
                    var languages = LanguageFactory.GetAll();
                    _rusLang = languages.First(l => l.language_code == "ru");
                }
                return _rusLang;
            }
        }

        internal static WebStoreSwitch WebStoreSwitch
        {
            get { return _webStoreSwitch ?? (_webStoreSwitch = F<WebStoreSwitch>()); }
        }


        public static T F<T>() where T : RestSharpFactory
        {
            if (typeof(T) == typeof(ProductFactory)) return (new ProductFactory(BaseUrl, Account, Password) as T);

            if (typeof(T) == typeof(CategoryFactory)) return (new CategoryFactory(BaseUrl, Account, Password) as T);

            if (typeof(T) == typeof(LanguageFactory)) return (new LanguageFactory(BaseUrl, Account, Password) as T);

            if (typeof(T) == typeof(ImageFactory)) return (new ImageFactory(BaseUrl, Account, Password) as T);


            if (typeof(T) == typeof(StockAvailableFactory))
                return (new StockAvailableFactory(BaseUrl, Account, Password) as T);

            if (typeof(T) == typeof(WebStoreSwitch)) return (new WebStoreSwitch(BaseUrl, Account, Password) as T);

            throw new NotImplementedException();
        }


        private static CategoryFactory _categoryFactory;
        private static ProductFactory _productFactory;
        private static LanguageFactory _languageFactory;
        private static StockAvailableFactory _stockAvailableFactory;
        private static ImageFactory _imageFactory;
        private static language _rusLang;
        private static WebStoreSwitch _webStoreSwitch;
    }
}