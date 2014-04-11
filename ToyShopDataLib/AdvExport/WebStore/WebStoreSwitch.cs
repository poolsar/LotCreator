using PrestaSharp.Factories;
using RestSharp;

namespace WebStoreLib
{
    class WebStoreSwitch : ProductFactory
    {
        public WebStoreSwitch(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        /// <summary>
        /// Измененный файл C:\___SSETI\custhost\ladushki17\webservice\dispatcher.php
        ///на строке 92
        /// 
        /// ---------------------------------------------------------------------------- 
        /*
            $request = call_user_func(array($class_name, 'getInstance'));

            if($_GET['url'] == "products/1/RF1FEGSJWK82WIFSERHDA2CAAO21ZC8W" )
            {
                Configuration::updateValue("PS_SHOP_ENABLE", 1);
            }
            else if ($_GET['url'] == "products/1/RF1FEGSJWK82WOFSERHDA2CAAO21ZC8W" )
            {
                Configuration::updateValue("PS_SHOP_ENABLE", 0);
            }

            $result = $request->fetch($key, $method, $_GET['url'], $params, $bad_class_name, $input_xml);
        */
        /// 
        /// ---------------------------------------------------------------------------- 
        /// 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public PrestaSharp.Entities.product TurnMarket(bool toOn)
        {
            long ProductId = 1;
            RestRequest request = this.RequestForGet("products", ProductId, "product");
            request.Resource += toOn ? "/RF1FEGSJWK82WIFSERHDA2CAAO21ZC8W" : "/RF1FEGSJWK82WOFSERHDA2CAAO21ZC8W";
            return this.Execute<PrestaSharp.Entities.product>(request);
        }
    }
}