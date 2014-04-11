using System.IO;
using PrestaSharp.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrestaWinClient.Logic;
using RestSharp;
using WebStoreLib;
using category = PrestaSharp.Entities.category;
using product = PrestaSharp.Entities.product;

namespace PrestaWinClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            this.FormClosing += Form1_FormClosing;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }



        private bool isOn = true;
        private void button1_Click(object sender, EventArgs e)
        {
            var marketController = new MarketController(BaseUrl, Account, Password);

            isOn = !isOn;

            marketController.TurnMarket(isOn);

            button1.Text = isOn ? "На профилактику" : "Включит магазин";
        }



        string BaseUrl = "http://192.168.0.8/ladushki17/api";
        string Account = "RF1FEGSJWK82WOFSERHDA2CAAO21ZC8W";
        string Password = "";


        private ProductFactory CreateProductFactory()
        {
            ProductFactory productFactory = new ProductFactory(BaseUrl, Account, Password);
            return productFactory;
        }

        private CategoryFactory CreateCategoryFactory()
        {
            var factory = new CategoryFactory(BaseUrl, Account, Password);
            return factory;
        }

        private LanguageFactory CreatelLanguageFactory()
        {
            var factory = new LanguageFactory(BaseUrl, Account, Password);
            return factory;
        }



        class MarketController : ProductFactory
        {
            public MarketController(string BaseUrl, string Account, string SecretKey)
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

        private void GetImage()
        {
            //RestClient _Client = new RestClient(BASE_URI);
            //RestRequest request = new RestRequest("/api/img/{FileName}");
            //request.AddParameter("FileName", "dummy.jpg", ParameterType.UrlSegment);
            //_Client.ExecuteAsync(
            //request,
            //Response =>
            //{
            //    if (Response != null)
            //    {
            //        byte[] imageBytes = Response.RawBytes;
            //        var bitmapImage = new BitmapImage();
            //        bitmapImage.BeginInit();
            //        bitmapImage.StreamSource = new MemoryStream(imageBytes);
            //        bitmapImage.CreateOptions = BitmapCreateOptions.None;
            //        bitmapImage.CacheOption = BitmapCacheOption.Default;
            //        bitmapImage.EndInit();

            //        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            //        Guid photoID = System.Guid.NewGuid();
            //        String photolocation = String.Format(@"c:\temp\{0}.jpg", Guid.NewGuid().ToString());
            //        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            //        using (var filestream = new FileStream(photolocation, FileMode.Create))
            //            encoder.Save(filestream);

            //        this.Dispatcher.Invoke((Action)(() => { img.Source = bitmapImage; }));
            //        ;
            //    }
            //});
        }



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = "Работаем...";
                textBox1.Refresh();


                var languageFactory = CreatelLanguageFactory();
                var languages = languageFactory.GetAll();
                var rusLang = languages.First(l => l.language_code == "ru");


                var categoryFactory2 = CreateCategoryFactory();

                var catId = categoryFactory2.GetIds().Max() + 1;

                var cat = new category();
                rusLang.Write(cat.name, "Молочные продукты " + catId);
                rusLang.Write(cat.link_rewrite, "milkprod");
                rusLang.Write(cat.description, "Молочные продукты, из экологически чистого домашнего деревенского молока");
                cat.position = 2;// position;
                cat.active = 1;
                cat.id_parent = 2;
                cat.id_shop_default = 1;

                cat = categoryFactory2.Add(cat);

                var imageFactory = CreateImageFactory();
                imageFactory.AddCategoryImage(cat.id, @"c:\milk_cat.jpg");


                textBox1.Text = "Готово";

                return;




                var productFactory = CreateProductFactory();





                //var stockAvId1 = prod1.associations.stock_availables[0].id;
                //var stockAvailableFactory1 = new StockAvailableFactory(BaseUrl, Account, Password);
                //var stockAvailable1 = stockAvailableFactory1.Get(stockAvId1);
                //stockAvailable1.quantity = 15;
                //stockAvailableFactory1.Update(stockAvailable1);
                //return;




                //var prod1 = productFactory.Get(16);
                // Категория
                //var categoryFactory1 = CreateCategoryFactory();
                ////var categoryId = categoryFactory1.GetIds().Max();
                //prod1.associations.categories.Add(new PrestaSharp.Entities.AuxEntities.category());
                //prod1.associations.categories[0].id = 4;

                //prod1.id_category_default = 4;

                //productFactory.Update(prod1);



                //return;


                product prod = new product();

                prod.width = 0;
                prod.height = 0;
                prod.depth = 0;

                prod.weight = 0;

                //unit_price_ratio = цена / цена за шт
                //id_tax_rules_group = ( 0 - без налога)
                //minimal_quantity = 1
                //show_price = 1

                prod.unit_price_ratio = 0; //цена / цена за шт
                prod.show_price = 1;
                prod.id_tax_rules_group = 0; //( 0 - без налога)
                prod.minimal_quantity = 1;
                prod.active = 1;
                prod.available_for_order = 1;

                prod.price = 500;
                prod.unity = "шт";



                // Название
                var maxid = productFactory.GetIds().Max();
                var postFix = (maxid + 1).ToString();
                prod.name.Add(rusLang.CreateAux("Молоко " + postFix));

                // Человеко понятная ссылка
                prod.link_rewrite.Add(rusLang.CreateAux("milk" + postFix));

                // Краткое описание
                prod.description_short[0].Value = @"<p>Свежайшее домашнее деревенское молоко.</p>";


                // Описание
                string desc = @"<div>
<h2>Описание</h2>

<div><p>Коровье молоко содержит белки, углеводы, жиры, ферменты, минеральные соли, казеин в оптимальном для человека соотношении. Кроме того, в нем присутствует ценнейший молочный жир, легко усвояемый и очень полезный в виду наличия в нем лактозы и углеводов. Наиболее ценным коровье молоко считается благодаря наличию в нем рибофлавина – очень редкого витамина, известного также как В2.</p>
<h2>Чем полезно?</h2>
<p>На протяжении веков люди употребляют коровье молоко, заслуженно считая его одним из полезнейших продуктов. Так, оно стимулирует формирование костной ткани, обновление крови. Натуральное деревенское молоко обладает мощным терапевтическим эффектом и используется при лечении туберкулеза, малокровия, изжоги, гипертонии, оно выводит токсины и рекомендуется тем, кто трудится на вредных производствах.</p>
<p>Однако даже тем людям, которые не имеют выраженных проблем со здоровьем, рекомендуется включать молоко в ежедневный рацион. Уже через весьма короткий промежуток времени проявятся результаты, которые потрясут вас – бодрость, прилив сил и жизненной энергии, улучшение состояния кожи, волос и ногтей, и это только видимые проявления благотворного воздействия молока. Наиболее же важный эффект – профилактика множества заболеваний, укрепление скелета, очистка крови.<br><br></p></div>
</div>";
                prod.description[0].Value = desc;


                // Категория
                //var categoryFactory = CreateCategoryFactory();
                prod.associations.categories = new List<PrestaSharp.Entities.AuxEntities.category>();
                prod.associations.categories.Add(new PrestaSharp.Entities.AuxEntities.category());
                prod.associations.categories[0].id = 4; //categoryId;
                prod.id_category_default = 4;




                // ------------------------------
                // Сохранение
                //--------------------------------
                prod = productFactory.Add(prod);



                // Количество на складе
                var stockAvId = prod.associations.stock_availables[0].id;
                var stockAvailableFactory = new StockAvailableFactory(BaseUrl, Account, Password);
                var stockAvailable = stockAvailableFactory.Get(stockAvId);
                stockAvailable.quantity = 15;
                stockAvailableFactory.Update(stockAvailable);


                // Картинка
                AddImage(prod.id.Value);

                //prod.id_category_default = categoryId;
                //prod.id_default_image = prod.associations.images.First().id;



                textBox1.Text = "Всё ок!";

            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }



            //CopyProject(originalProduct, newProduct);


            int i = 0;
        }

        private void AddImage(long productId)
        {
            var imageFactory = CreateImageFactory();
            var imageFile = new FileInfo(@"c:\milk.jpg");
            imageFactory.AddProductImage(productId, imageFile.FullName);
        }

        private ImageFactory CreateImageFactory()
        {
            var imageFactory = new ImageFactory(BaseUrl, Account, Password);
            return imageFactory;
        }

        private void CopyProject(product originalProduct, product newProduct)
        {
            //originalProduct.active
            //originalProduct.additional_shipping_cost
            //originalProduct.advanced_stock_management
            //originalProduct.associations
            //originalProduct.available_date
            //originalProduct.available_for_order
            //originalProduct.available_later
            //originalProduct.available_now
            //originalProduct.cache_default_attribute
            //originalProduct.cache_has_attachments
            //originalProduct.cache_has_attachments

            //newProduct.active = originalProduct.active;
            //newProduct.additional_shipping_cost = originalProduct.additional_shipping_cost;
            //newProduct.active = originalProduct.active;
            //newProduct.active = originalProduct.active;
            //newProduct.active = originalProduct.active;
            //newProduct.active = originalProduct.active;


        }

        class MyClass
        {

            //on_sale
            //online_only 
            //ean13 
            //upc 
            //ecotax 
            //minimal_quantity 
            //price 
            //wholesale_price 
            //unity 
            //unit_price_ratio 
            //additional_shipping_cost 
            //reference 
            //supplier_reference 
            //location 
            //width 
            //height 
            //depth 
            //weight 
            //quantity_discount 
            //customizable 
            //uploadable_files 
            //text_fields 
            //active 
            //redirect_type 
            //id_product_redirected 
            //available_for_order 
            //available_date 
            //condition 
            //show_price 
            //indexed 
            //visibility 
            //cache_is_pack 
            //cache_has_attachments 
            //is_virtual 
            //cache_default_attribute 
            //date_add 
            //date_upd 
            //advanced_stock_management 
            //meta_description 
            //meta_keywords 
            //meta_title 
            //link_rewrite 
            //name 
            //description 
            //description_short 
            //available_now 
            //available_later 
            //associations 

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new ManageProductsForm();
            form.ShowDialog();

            // DownloadProductsAsync();
        }

        private void DownloadProductsAsync()
        {
            backgroundWorker1.RunWorkerAsync();
            //Thread t = new Thread(DownloadProducts);
            //ThreadController.AddThread(t);
            //t.Start();
        }
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DownloadProducts();
        }

        private void DownloadProducts()
        {
            try
            {
                var taimyrScrapper = new TaimyrScrapper();
                taimyrScrapper.Output = WriteOutputAsync;
                //List<TimyrPriceRow> timyrPriceRows = taimyrScrapper.DownloadPrice();
                //gridControl1.DataSource = timyrPriceRows;
                taimyrScrapper.DownloadProducts();
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Ошибка");

                while (ex != null)
                {
                    sb.AppendLine(ex.Message);
                    ex = ex.InnerException;
                }
                var msg = sb.ToString();
                WriteOutputAsync(msg);
            }
        }

        private void WriteOutputAsync(string msg)
        {
            Action act = () =>
            {
                textBox1.Text += Environment.NewLine + msg;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            };

            this.BeginInvoke(act);
        }

    }
}
