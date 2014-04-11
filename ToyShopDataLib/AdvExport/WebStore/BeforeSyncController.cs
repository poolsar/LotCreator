using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using PrestaSharp.Entities;
using PrestaSharp.Factories;

namespace WebStoreLib
{
    public class BeforeSyncController
    {
        private CategoryFactory CategoryFactory = FactoryHost.CategoryFactory;
        private ProductFactory ProductFactory = FactoryHost.ProductFactory;


        /// <summary>
        /// Загружаем текущие категории
        /// </summary>
        public List<BeforeSyncCategory> Load()
        {
            StoreCategories = CategoryFactory.GetAll();
            StoreProducts = ProductFactory.GetAll();

            if (StoreCategories == null || StoreProducts == null) return null;

            BeforeSyncCategories = BuildTree();
            return BeforeSyncCategories;
        }

        public List<BeforeSyncCategory> BeforeSyncCategories { get; set; }

        public void Save()
        {
            foreach (var category in BeforeSyncCategories)
            {
                Save(category);
            }

        }

        private void Save(BeforeSyncCategory category)
        {
            if (!category.Checked)
            {
                CategoryFactory.Delete(category.Base);
            }

            foreach (var product in category.Products)
            {
                Save(product);
            }

            foreach (var child in category.Childs)
            {
                Save(child);
            }
        }

        private void Save(BeforeSyncProduct product)
        {
            if (!product.Checked)
            {
                ProductFactory.Delete(product.Base);
            }
        }

        private List<BeforeSyncCategory> BuildTree()
        {
            var rootCats = StoreCategories.Where(c => c.id_parent == 0).ToList();

            List<BeforeSyncCategory> beforeSyncCategories = new List<BeforeSyncCategory>();

            foreach (category category in rootCats)
            {
                var beforeSyncCategory = BuildTree(null, category);

                beforeSyncCategories.Add(beforeSyncCategory);
            }

            return beforeSyncCategories;
        }

        private BeforeSyncCategory BuildTree(BeforeSyncCategory parentSyncCat, category category)
        {
            var beforeSyncCategory = new BeforeSyncCategory(parentSyncCat, category);

            var products = StoreProducts.Where(p => p.id_category_default == category.id).ToList();
            foreach (product product in products)
            {
                var beforeSyncProduct = new BeforeSyncProduct(beforeSyncCategory, product);
            }


            var childCategories = StoreCategories.Where(c => c.id_parent == category.id).ToList();
            foreach (category child in childCategories)
            {
                BuildTree(beforeSyncCategory, child);
            }

            return beforeSyncCategory;
        }

        public List<product> StoreProducts { get; set; }

        public List<category> StoreCategories { get; set; }



    }

    public class BeforeSyncCategory
    {
        private bool _checked = true;

        public BeforeSyncCategory(BeforeSyncCategory parentSyncCat, category category)
        {
            Childs = new List<BeforeSyncCategory>();
            Products = new List<BeforeSyncProduct>();

            Id = (int)category.id;
            Title = category.name[0].Value;

            Base = category;

            Parent = parentSyncCat;
            if (parentSyncCat != null) parentSyncCat.Childs.Add(this);
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public BeforeSyncCategory Parent { get; set; }
        public List<BeforeSyncCategory> Childs { get; set; }

        public List<BeforeSyncProduct> Products { get; set; }

        public category Base { get; set; }

        public bool Checked
        {
            get
            {

                return _checked;
            }
            set
            {
                _checked = value;

            }
        }

        public void SetCheckedRecursive(bool value)
        {
            Checked = value;

            foreach (var product in Products)
            {
                product.Checked = value;
            }

            foreach (var child in Childs)
            {
                child.SetCheckedRecursive(value);
            }

        }
    }

    public class BeforeSyncProduct
    {
        private bool _checked = true;

        public BeforeSyncProduct(BeforeSyncCategory beforeSyncCategory, product product)
        {

            Id = (int)product.id;
            Title = product.name[0].Value;

            Base = product;

            Category = beforeSyncCategory;
            beforeSyncCategory.Products.Add(this);
        }

        public BeforeSyncCategory Category { get; set; }

        public int Id { get; set; }
        public string Title { get; set; }

        public product Base { get; set; }

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
    }

}