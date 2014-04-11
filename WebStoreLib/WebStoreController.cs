using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using PrestaSharp.Entities;
using PrestaSharp.Entities.FilterEntities;
using PrestaSharp.Factories;
using ShopDataLib;
using Utils;
using category = PrestaSharp.Entities.category;
using language = PrestaSharp.Entities.language;
using product = PrestaSharp.Entities.product;
using shop = PrestaSharp.Entities.FilterEntities.shop;


namespace WebStoreLib
{


    public class WebStoreController
    {

        private CategoryFactory CategoryFactory = FactoryHost.CategoryFactory;

        private ProductFactory ProductFactory = FactoryHost.ProductFactory;

        private StockAvailableFactory StockAvailableFactory = FactoryHost.StockAvailableFactory;

        private ImageFactory ImageFactory = FactoryHost.ImageFactory;

        private language RusLang = FactoryHost.RusLang;

        private List<category> StoreCategories;
        private List<product> StoreProducts;
        private List<image> _allCategoryImages;

        public void TurnMarket(bool on)
        {
            FactoryHost.WebStoreSwitch.TurnMarket(on);
        }


        public void Sync()
        {
            try
            {
                TurnMarket(false);

                StoreCategories = CategoryFactory.GetAll();
                StoreProducts = ProductFactory.GetAll();

                var rootCats = Context.Inst.ShopCategorySet.Where(c => c.Parent == null).ToList();

                foreach (var cat in rootCats) Sync(cat);

                TurnMarket(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                TurnMarket(true);
            }
        }


        //public bool SetLinkRewrite(product prod, ShopProduct shopProduct)
        //{
        //    dynamic cat = prod;
        //    dynamic shopCategory = shopProduct;


        //    string linkRewrite = shopCategory.GetLinkRewrite();

        //    bool needSave = true;

        //    if (cat.link_rewrite.Count > 0)
        //    {
        //        needSave = cat.link_rewrite[0].Value != linkRewrite;
        //    }

        //    if (needSave)
        //    {
        //        cat.link_rewrite.Clear();
        //        var language = RusLang.CreateAux(linkRewrite);
        //        cat.AddLinkRewrite(language);
        //        shopCategory.LinkRewrite = cat.link_rewrite[0].Value;
        //    }

        //    return needSave;

        //}



        interface IShopDuck
        {
            string GetLinkRewrite();
            string LinkRewrite { get; set; }
        }

        interface IStoreDuck
        {
            List<PrestaSharp.Entities.AuxEntities.language> link_rewrite { get; set; }
            void AddLinkRewrite(PrestaSharp.Entities.AuxEntities.language Language);
        }

        private bool SetLinkRewrite(category store, ShopCategory shop)
        {
            return SetLinkRewrite((object)store, (object)shop);
        }

        private bool SetLinkRewrite(product store, ShopProduct shop)
        {
            return SetLinkRewrite((object)store, (object)shop);
        }

        private bool SetLinkRewrite(object store, object shop)
        {

            dynamic storeDuck = store;
            dynamic shopDuck = shop;


            string linkRewrite = shopDuck.GetLinkRewrite();

            bool needSave = true;

            if (storeDuck.link_rewrite.Count > 0)
            {
                needSave = storeDuck.link_rewrite[0].Value != linkRewrite;
            }

            if (needSave)
            {
                storeDuck.link_rewrite.Clear();
                var language = RusLang.CreateAux(linkRewrite);
                storeDuck.AddLinkRewrite(language);
                shopDuck.LinkRewrite = storeDuck.link_rewrite[0].Value;
            }

            return needSave;
        }


        private bool SetParentCategory(category store, ShopCategory shop)
        {
            bool needSave = true;

            int rootParentId = 2;

            int? newParentId = shop.Parent == null ? rootParentId : shop.Parent.IdOnWebStore;

            if (store.id_parent != null)
            {
                needSave = store.id_parent != newParentId;
            }

            if (needSave)
            {
                store.id_parent = newParentId;
            }

            if (store.id_parent == rootParentId)
            {
                store.is_root_category = 1;
            }

            return needSave;
        }

        private bool SetCategory(product prod, ShopProduct shopProduct)
        {
            bool needSave = true;

            if (prod.associations.categories.Count > 0)
            {
                needSave = prod.associations.categories[0].id != shopProduct.Category.IdOnWebStore.Value;
            }

            if (needSave)
            {
                prod.associations.categories.Clear();
                prod.associations.categories.Add(new PrestaSharp.Entities.AuxEntities.category());
                prod.associations.categories[0].id = shopProduct.Category.IdOnWebStore.Value;
                prod.id_category_default = shopProduct.Category.IdOnWebStore;
            }

            return needSave;
        }



        bool Set<T, P>(T storeEnt, Expression<Func<T, P>> storeEntExp, P shopValue)
        {
            bool needSave = true;

            var expr = (MemberExpression)storeEntExp.Body;
            var prop = (PropertyInfo)expr.Member;

            object storeValue = prop.GetValue(storeEnt);

            // если на сервер отправил null, потом может вернуться и не null
            object shopValueBox = shopValue;
            if (shopValueBox == null)
            {
                var shopValueType = typeof(P);

                if (shopValueType == typeof(string))
                {
                    shopValueBox = string.Empty;
                }
                else if (shopValueType == typeof(int?))
                {
                    shopValueBox = 0;
                }
                else if (shopValueType == typeof(decimal?))
                {
                    shopValueBox = 0m;
                }
            }


            if (storeValue != null)
            {
                needSave = !storeValue.Equals(shopValueBox);
            }
            else
            {
                needSave = !shopValueBox.Equals(null);
            }

            if (needSave)
            {
                prop.SetValue(storeEnt, shopValueBox);
            }

            return needSave;
        }


        public bool Set(List<PrestaSharp.Entities.AuxEntities.language> prop, string value)
        {
            bool needSave = true;

            if (prop.Count > 0)
            {
                needSave = prop[0].Value != (value ?? string.Empty);

                if (needSave)
                {


                    //findDifs(prop[0].Value, value);
                }
            }

            if (needSave)
            {
                RusLang.Write(prop, value);
            }

            return needSave;
        }

        private void findDifs(string value1, string value2)
        {
            int count = value1.Count();
            for (int i = 0; i < count; i++)
            {
                char v1 = value1[i];
                char v2 = value2[i];

                if (v1 != v2)
                {

                }
            }
        }


        private void Sync(ShopCategory shopCategory)
        {
            var cat = StoreCategories.FirstOrDefault(sc => sc.id == shopCategory.IdOnWebStore);
            bool isNew = cat == null;

            bool needSave = false;


            if (!shopCategory.InShop && !isNew)
            {
                cat.active = 0;
            }
            else
            {
                if (isNew)
                {
                    cat = new category();

                    cat.id_shop_default = 1;
                    cat.position = CalcPosition(cat);
                }

                cat.active = 1;

                needSave |= Set(cat.name, shopCategory.Title);

                if (shopCategory.Description != null)
                    shopCategory.Description = shopCategory.Description.Replace("\r", "").Replace("\n", "");

                needSave |= Set(cat.description, shopCategory.Description);
                needSave |= SetLinkRewrite(cat, shopCategory);
                needSave |= SetParentCategory(cat, shopCategory);
            }

            string operationName = null;

            if (isNew)
            {
                cat = CategoryFactory.Add(cat);
                shopCategory.IdOnWebStore = (int)cat.id;

                operationName = "добавлена";
                Messenger.Write("В online {0} категория: {1}", operationName, shopCategory.Title);
                Context.Save();
            }
            else
            {
                if (needSave)
                {
                    CategoryFactory.Update(cat);

                    operationName = "сохранена";
                    Messenger.Write("В online {0} категория: {1}", operationName, shopCategory.Title);
                    Context.Save();
                }
                else
                {
                    Messenger.Write("Категория - {0}, не изменена", shopCategory.Title);
                }
            }

            if (shopCategory.InShop)
            {
                SyncCatImages(shopCategory, cat);
            }

            foreach (var shopProduct in shopCategory.Products)
            {
                Sync(shopProduct);
            }

            foreach (var child in shopCategory.Childs)
            {
                Sync(child);
            }
        }

        private long CalcPosition(category cat)
        {
            long position = 0;
            var sameParentCats = StoreCategories.Where(c => c.id_parent == cat.id_parent).ToList();

            if (sameParentCats.Count == 0)
            {
                position = 1;
            }
            else
            {
                position = sameParentCats.Max(c => c.position) + 1;
            }

            return position;
        }


        //private bool CheckNeedSave(ShopCategory shopCategory, category cat)
        //{
        //    if (cat.id == null || cat.id == 0) return true;

        //    bool notNeed = cat.name[0].Value == shopCategory.Title;
        //    notNeed &= cat.description[0].Value == (shopCategory.Description ?? string.Empty);
        //    notNeed &= cat.link_rewrite[0].Value == shopCategory.GetLinkRewrite();

        //    notNeed &= (shopCategory.Parent == null && cat.id_parent == 2)
        //        || (cat.id_parent == shopCategory.Parent.IdOnWebStore);


        //    notNeed &= cat.active == (shopCategory.InShop ? 1 : 0);

        //    return !notNeed;
        //}

        private void Sync(ShopProduct shopProduct)
        {
            var prod = StoreProducts.FirstOrDefault(sc => sc.id == shopProduct.IdOnWebStore);
            bool isNew = prod == null;

            bool needSave = false;

            if (!shopProduct.InShop && !isNew)
            {
                prod.active = 0;
            }
            else
            {
                if (isNew)
                {
                    prod = new product();

                    prod.associations.categories = new List<PrestaSharp.Entities.AuxEntities.category>();

                    prod.width = 0;
                    prod.height = 0;
                    prod.depth = 0;

                    prod.weight = 0;

                    prod.unit_price_ratio = 0; //цена / цена за шт
                    prod.show_price = 1;
                    prod.id_tax_rules_group = 0; //( 0 - без налога)
                    prod.minimal_quantity = 1;
                    prod.active = 1;
                    prod.available_for_order = 1;
                }

                needSave |= Set(prod.name, shopProduct.Title);
                needSave |= Set(prod.description_short, string.Empty);

                if (shopProduct.Description != null)
                    shopProduct.Description = shopProduct.Description.Replace("\r", "").Replace("\n", "");

                needSave |= Set(prod.description, shopProduct.Description);

                needSave |= SetLinkRewrite(prod, shopProduct);
                needSave |= SetCategory(prod, shopProduct);

                needSave |= Set(prod, p => p.price, shopProduct.DiscountPrice);
                needSave |= Set(prod, p => p.on_sale, shopProduct.IsSale ? 1 : 0);
                needSave |= Set(prod, p => p.unity, shopProduct.Unity);

                // узнать что это за поля и как делать скидку (специальную цену)
                //prod.quantity_discount = (int)Math.Round(shopProduct.DiscountPercent);
                //prod.wholesale_price ???

                string operationName = null;

                if (isNew)
                {
                    prod = ProductFactory.Add(prod);
                    shopProduct.IdOnWebStore = (int)prod.id;

                    operationName = "добавлен";
                    Messenger.Write("В online {0} товар: {1}", operationName, shopProduct.Title);
                    Context.Save();
                }
                else
                {
                    if (needSave)
                    {
                        ProductFactory.Update(prod);

                        operationName = "сохранен";
                        Messenger.Write("В online {0} товар: {1}", operationName, shopProduct.Title);
                        Context.Save();
                    }
                    else
                    {
                        Messenger.Write("Товар - {0}, не изменен", shopProduct.Title);
                    }
                }


                // Количество на складе
                // ??? 
                // TODO продумать как синхронизировать запасы в обе стороны
                // TODO придумать интерфейс обработки заказов
                // колво доуступных товаров мне пока не известно, 
                // кол-во на сколаде у поставщика я буду узнавать в реальном времени
                // поэтому пускай пока их будет по 20
                // потом прикинутуть какие товары расходятся быстрее и сделать их больше

                //  пускай в отладке устанавливается пока только при создании
                if (isNew)
                {
                    var stockAvailable = StockAvailableFactory.Get(prod.associations.stock_availables[0].id);
                    stockAvailable.quantity = 20; // пока просто, чтобы можно было заказывать // shopProduct.Quantity ?? 0;                    StockAvailableFactory.Update(stockAvailable);
                }



                SyncProdImages(shopProduct, prod);
            }
        }



        public List<image> AllCategoryImages
        {
            get
            {
                if (_allCategoryImages == null)
                {
                    _allCategoryImages = ImageFactory.GetAllCategoryImages();
                }
                return _allCategoryImages;
            }
            set { _allCategoryImages = value; }
        }


        private void SyncCatImages(ShopCategory shopCat, category storeCat)
        {
            var image = shopCat.GetDefaultImage();

            // у категории поддердивается только одна картинка, и если уже сохранена, то больше не надо
            if (image == null || image.IdOnWebStore != null) return;


            bool needDeleteOld = AllCategoryImages.Any(i => i.id == storeCat.id);

            if (needDeleteOld)
            {
                ImageFactory.DeleteCategoryImage(storeCat.id.Value);
            }


            ImageFactory.AddCategoryImage(storeCat.id, image.LocalPath);

            image.IdOnWebStore = (int?)storeCat.id;
            shopCat.DeleteOldImages();
            Context.Save();


            Messenger.Write("В online добавлена картинка категории: {0} ({1})",
                shopCat.Title, image.LocalPath);
        }

        private void SyncProdImages(ShopProduct shopProd, product storeProd)
        {
            var storeImages = storeProd.associations.images;

            //var storeImages = ImageFactory.GetAllProductImages(storeProd.id.Value);

            var shopImages = shopProd.Images.ToList();

            var idsStore = storeImages.Select(ti => (int)ti.id).ToList();

            var idsShop = shopImages.Select(hi => hi.IdOnWebStore ?? -1).ToList();

            var idsToDelete = idsStore.Except(idsShop).ToList();
            var idsToAdd = idsShop.Except(idsStore).ToList();

            try
            {
                foreach (int idToDelete in idsToDelete)
                {
                    ImageFactory.DeleteProductImage((long)storeProd.id, (long)idToDelete);
                }

            }
            catch (Exception ex)
            {
                string dontWorryMessage = @"<![CDATA[This image does not exist on disk]]>";

                if (!ex.Message.Contains(dontWorryMessage)) throw ex;
            }

            var imagesToAdd =
                shopImages.Where(hi => hi.IdOnWebStore == null || idsToAdd.Contains(hi.IdOnWebStore.Value)).ToList();

            foreach (var imageToAdd in imagesToAdd)
            {
                ImageFactory.AddProductImage(storeProd.id.Value, imageToAdd.LocalPath);
                Messenger.Write("В online добавлена картинка товара: {0} ({1})",
                    shopProd.Title, imageToAdd.LocalPath);
            }


            // узнаем какие id у добавленных картинок
            //storeImages = ImageFactory.GetAllProductImages(storeProd.id.Value);
            storeImages = ProductFactory.Get(storeProd.id.Value).associations.images;


            if (idsToAdd.Count > 0)
            {
                var addedStoreImages = storeImages.Skip(storeImages.Count - imagesToAdd.Count).ToList();

                for (int i = 0; i < imagesToAdd.Count; i++)
                {
                    imagesToAdd[i].IdOnWebStore = (int)addedStoreImages[i].id;
                }
            }

            // устананавливаем картинку по умолчанию

            var defaultImage = shopProd.GetDefaultImage();
            if (defaultImage != null)
            {
                bool raznie = storeProd.id_default_image == null ||
                              (int)defaultImage.IdOnWebStore != (int)storeProd.id_default_image;

                if (raznie)
                {
                    storeProd.id_default_image = defaultImage.IdOnWebStore;
                    ProductFactory.Update(storeProd);
                }
            }

            Context.Save();
        }


        /*
        private void SyncImages(IHaveImages shopEnt, PrestashopEntity storeEnt)
        {
            bool isProduct = shopEnt is ShopProduct && storeEnt is product;
            bool isCategory = shopEnt is ShopCategory && storeEnt is category;

            if (!isProduct && !isCategory) return;

            ShopProduct shopProd = null;
            product storeProd = null;
            if (isProduct)
            {
                shopProd = shopEnt as ShopProduct;
                storeProd = storeEnt as product;
            }

            ShopCategory shopCat = null;
            category storeCat = null;
            if (isCategory)
            {
                shopCat = shopEnt as ShopCategory;
                storeCat = storeEnt as category;

                SyncCatImages(shopCat, storeCat);
                return;

            }

            var storeImages = isProduct
                    ? ImageFactory.GetAllProductImages()
                    : ImageFactory.GetAllCategoryImages();


            var shopImages = shopEnt.Images.ToList();

            var idsStore = storeImages.Select(ti => (int)ti.id).ToList();
            var idsShop = shopImages.Select(hi => hi.IdOnWebStore ?? -1).ToList();

            var idsToDelete = idsStore.Except(idsShop).ToList();
            var idsToAdd = idsShop.Except(idsStore).ToList();

            try
            {
                foreach (int idToDelete in idsToDelete)
                {
                    if (isProduct)
                        ImageFactory.DeleteProductImage((long)storeProd.id, (long)idToDelete);
                    else
                        ImageFactory.DeleteCategoryImage(storeCat.id.Value);
                }

            }
            catch (Exception ex)
            {
                string dontWorryMessage = @"<![CDATA[This image does not exist on disk]]>";

                if (!ex.Message.Contains(dontWorryMessage)) throw ex;
            }

            var imagesToAdd =
                shopImages.Where(hi => hi.IdOnWebStore == null || idsToAdd.Contains(hi.IdOnWebStore.Value)).ToList();

            foreach (var imageToAdd in imagesToAdd)
            {
                if (isProduct)
                {
                    ImageFactory.AddProductImage(storeProd.id.Value, imageToAdd.LocalPath);

                    Messenger.Write("В online добавлена картинка товара: {0} ({1})",
                        shopProd.Title, imageToAdd.LocalPath);
                }
                else
                {
                    ImageFactory.AddCategoryImage(storeCat.id, imageToAdd.LocalPath);

                    Messenger.Write("В online добавлена картинка категории: {0} ({1})",
                        shopCat.Title, imageToAdd.LocalPath);
                }
            }



            // узнаем какие id у добавленных картинок
            storeImages = isProduct
                    ? ImageFactory.GetAllProductImages()
                    : ImageFactory.GetAllCategoryImages();

            if (idsToAdd.Count > 0)
            {
                var addedStoreImages = storeImages.Skip(storeImages.Count - idsToAdd.Count).ToList();

                for (int i = 0; i < imagesToAdd.Count; i++)
                {
                    if (isProduct)
                    {
                        imagesToAdd[i].IdOnWebStore = (int)addedStoreImages[i].id;
                    }
                    else
                    {
                        imagesToAdd[i].IdOnWebStore = (int)storeCat.id; // так работает    
                    }

                }

                Context.Save();
            }

            // устананавливаем картинку по умолчанию
            if (isProduct)
            {
                storeProd.id_default_image = shopProd.IdOnWebStore;
                Context.Save();
            }
        }

        */
    }


    class ProductFilds
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

    //private void LoadCategories()
    //{
    //    var categoryFactory = CreateFactory<CategoryFactory>();
    //    List<category> categories = categoryFactory.GetAll();

    //    var rootCats = categories.Where(c => c.is_root_category == 1).ToList();

    //    var shopCategories = Context.Inst.ShopCategorySet.ToList();
    //    foreach (var cat in rootCats)
    //    {
    //        var shopCategory = shopCategories.FirstOrDefault(c => c.IdOnWebStore == cat.id);
    //        if (shopCategory == null)
    //        {
    //            shopCategory = new ShopCategory();

    //            shopCategory.Title = cat.name[0].Value;
    //            shopCategory.Description = cat.description[0].Value;
    //            shopCategory.LinkRewrite = cat.link_rewrite[0].Value;
    //            shopCategory.InShop = cat.active == 1;
    //        }

    //    }
    //}

    //private void SyncImages(ShopCategory shopCategory, category storeCategory)
    //{
    //    var imageFactory = CreateFactory<ImageFactory>();
    //    var storeCategoryImages = imageFactory.GetAllCategoryImages();
    //    var shopCategoryImages = shopCategory.Images.ToList();

    //    var idsStore = storeCategoryImages.Select(ti => (int)ti.id).ToList();
    //    var idsShop = shopCategoryImages.Select(hi => hi.IdOnWebStore ?? -1).ToList();

    //    var idsToDelete = idsStore.Except(idsShop).ToList();
    //    var idsToAdd = idsShop.Except(idsStore).ToList();

    //    foreach (int idToDelete in idsToDelete)
    //    {
    //        imageFactory.DeleteCategoryImage(idToDelete);
    //    }
    //    var imagesToAdd =
    //        shopCategoryImages.Where(hi => hi.IdOnWebStore == null || idsToAdd.Contains(hi.IdOnWebStore.Value)).ToList();

    //    foreach (var imageToAdd in imagesToAdd)
    //    {
    //        imageFactory.AddCategoryImage(storeCategory.id, imageToAdd.LocalPath);
    //    }
    //}









    //private void CopyProduct(product originalProduct, product newProduct)
    //{
    //    //originalProduct.active
    //    //originalProduct.additional_shipping_cost
    //    //originalProduct.advanced_stock_management
    //    //originalProduct.associations
    //    //originalProduct.available_date
    //    //originalProduct.available_for_order
    //    //originalProduct.available_later
    //    //originalProduct.available_now
    //    //originalProduct.cache_default_attribute
    //    //originalProduct.cache_has_attachments
    //    //originalProduct.cache_has_attachments

    //    //newProduct.active = originalProduct.active;
    //    //newProduct.additional_shipping_cost = originalProduct.additional_shipping_cost;
    //    //newProduct.active = originalProduct.active;
    //    //newProduct.active = originalProduct.active;
    //    //newProduct.active = originalProduct.active;
    //    //newProduct.active = originalProduct.active;


    //}

    //private void GetImage()
    //{
    //    //RestClient _Client = new RestClient(BASE_URI);
    //    //RestRequest request = new RestRequest("/api/img/{FileName}");
    //    //request.AddParameter("FileName", "dummy.jpg", ParameterType.UrlSegment);
    //    //_Client.ExecuteAsync(
    //    //request,
    //    //Response =>
    //    //{
    //    //    if (Response != null)
    //    //    {
    //    //        byte[] imageBytes = Response.RawBytes;
    //    //        var bitmapImage = new BitmapImage();
    //    //        bitmapImage.BeginInit();
    //    //        bitmapImage.StreamSource = new MemoryStream(imageBytes);
    //    //        bitmapImage.CreateOptions = BitmapCreateOptions.None;
    //    //        bitmapImage.CacheOption = BitmapCacheOption.Default;
    //    //        bitmapImage.EndInit();

    //    //        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
    //    //        Guid photoID = System.Guid.NewGuid();
    //    //        String photolocation = String.Format(@"c:\temp\{0}.jpg", Guid.NewGuid().ToString());
    //    //        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
    //    //        using (var filestream = new FileStream(photolocation, FileMode.Create))
    //    //            encoder.Save(filestream);

    //    //        this.Dispatcher.Invoke((Action)(() => { img.Source = bitmapImage; }));
    //    //        ;
    //    //    }
    //    //});
    //}
}
