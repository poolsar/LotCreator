using System;
using System.Linq;

namespace ShopDataLib
{
    public partial class Settings
    {

        private const string MARGIN = "MARGIN";
        private const string WEB_STORE_URI = "WEB_STORE_URI";
        private const string SERVICE_KEY = "SERVICE_KEY";
        

        public static decimal Margin
        {
            get
            {
                return GetSettings<decimal>(MARGIN);
            }
            set
            {
                SetSettings(MARGIN, value);
            }
        }

        public static string WebStoreUri
        {
            get
            {
                var uri = GetSettings<string>(WEB_STORE_URI);

                if (string.IsNullOrWhiteSpace(uri))
                {
                    uri = "http://192.168.56.101/ladushki17";
                    WebStoreUri = uri;
                    Context.Save();
                }

                return uri;
            }
            set
            {
                SetSettings(WEB_STORE_URI, value);
            }
        }

        public static string ServiceKey
        {
            get
            {
                var key = GetSettings<string>(SERVICE_KEY);

                if (string.IsNullOrWhiteSpace(key))
                {
                    key = "RF1FEGSJWK82WOFSERHDA2CAAO21ZC8W";
                    ServiceKey = key;
                    Context.Save();
                }

                return key;
            }
            set
            {
                SetSettings(SERVICE_KEY, value);
            }
        }


        private static T GetSettings<T>(string name)
        {
            Settings settings = GetSettings(name);
            object val = Convert.ChangeType(settings.Value, typeof(T));
            return (T)val;
        }

        private static void SetSettings(string name, object value)
        {
            Settings settings = GetSettings(name);
            settings.Value = Convert.ToString(value);
        }

        private static Settings GetSettings(string name)
        {
            Settings setting = Context.Inst.SettingsSet.FirstOrDefault(s => s.Name == name);

            if (setting == null)
            {
                setting = new Settings();
                setting.Name = name;
                setting.Value = GetDefaultValue(name);
            }

            return setting;
        }

        private static string GetDefaultValue(string name)
        {
            object value = null;

            switch (name)
            {
                case MARGIN:
                    value = 15m;
                    break;
            }

            return Convert.ToString(value);
        }



    }
}