using System;
using System.Text;

namespace ToyShopDataLib.AdvExport
{
    public class CSVConstants
    {
        /// <summary>
        /// Пропустить этот столбик
        /// </summary>
        public const string no = "no";
        /// <summary>
        /// №
        /// </summary>
        public const string id = "id";
        /// <summary>
        /// Активен (0/1)
        /// </summary>
        public const string active = "active";
        /// <summary>
        /// Название*
        /// </summary>
        public const string name = "name";
        /// <summary>
        /// Категории (x,y,z...)
        /// </summary>
        public const string category = "category";
        /// <summary>
        /// Цена без налогов
        /// </summary>
        public const string price_tex = "price_tex";
        /// <summary>
        /// Цена c налогами
        /// </summary>
        public const string price_tin = "price_tin";
        /// <summary>
        /// ID налога
        /// </summary>
        public const string id_tax_rules_group = "id_tax_rules_group";
        /// <summary>
        /// Общая цена
        /// </summary>
        public const string wholesale_price = "wholesale_price";
        /// <summary>
        /// Распродажа (0/1)
        /// </summary>
        public const string on_sale = "on_sale";
        /// <summary>
        /// Сумма скидки
        /// </summary>
        public const string reduction_price = "reduction_price";
        /// <summary>
        /// Процент скидки
        /// </summary>
        public const string reduction_percent = "reduction_percent";
        /// <summary>
        /// Скидка действует от (гггг-мм-дд)
        /// </summary>
        public const string reduction_from = "reduction_from";
        /// <summary>
        /// Скидка действует до (гггг-мм-дд)
        /// </summary>
        public const string reduction_to = "reduction_to";
        /// <summary>
        /// Артикул №
        /// </summary>
        public const string reference = "reference";
        /// <summary>
        /// Артикул поставщика №
        /// </summary>
        public const string supplier_reference = "supplier_reference";
        /// <summary>
        /// Поставщик
        /// </summary>
        public const string supplier = "supplier";
        /// <summary>
        /// Производитель
        /// </summary>
        public const string manufacturer = "manufacturer";
        /// <summary>
        /// Штрихкод
        /// </summary>
        public const string ean13 = "ean13";
        /// <summary>
        /// Универсальный Код Товара
        /// </summary>
        public const string upc = "upc";
        /// <summary>
        /// Эконалог
        /// </summary>
        public const string ecotax = "ecotax";
        /// <summary>
        /// Ширина
        /// </summary>
        public const string width = "width";
        /// <summary>
        /// Высота
        /// </summary>
        public const string height = "height";
        /// <summary>
        /// Глубина
        /// </summary>
        public const string depth = "depth";
        /// <summary>
        /// Вес
        /// </summary>
        public const string weight = "weight";
        /// <summary>
        /// Количество
        /// </summary>
        public const string quantity = "quantity";
        /// <summary>
        /// Минимальное количество
        /// </summary>
        public const string minimal_quantity = "minimal_quantity";
        /// <summary>
        /// Видимость
        /// </summary>
        public const string visibility = "visibility";
        /// <summary>
        /// Дополнительные расходы по доставке
        /// </summary>
        public const string additional_shipping_cost = "additional_shipping_cost";
        /// <summary>
        /// Единица изммерения
        /// </summary>
        public const string unity = "unity";
        /// <summary>
        /// Цена за единицу
        /// </summary>
        public const string unit_price_ratio = "unit_price_ratio";
        /// <summary>
        /// Краткое описание
        /// </summary>
        public const string description_short = "description_short";
        /// <summary>
        /// Описание
        /// </summary>
        public const string description = "description";
        /// <summary>
        /// Метки (x,y,z...)
        /// </summary>
        public const string tags = "tags";
        /// <summary>
        /// Мета-заголовок
        /// </summary>
        public const string meta_title = "meta_title";
        /// <summary>
        /// Мета ключевые слова
        /// </summary>
        public const string meta_keywords = "meta_keywords";
        /// <summary>
        /// Мета описание
        /// </summary>
        public const string meta_description = "meta_description";
        /// <summary>
        /// ЧПУ
        /// </summary>
        public const string link_rewrite = "link_rewrite";
        /// <summary>
        /// Текст когда на складе
        /// </summary>
        public const string available_now = "available_now";
        /// <summary>
        /// Текст, если предварительный заказ разрешен
        /// </summary>
        public const string available_later = "available_later";
        /// <summary>
        /// Доступен для заказа (0= нет, 1 = да)
        /// </summary>
        public const string available_for_order = "available_for_order";
        /// <summary>
        /// Дата появления товара в наличии
        /// </summary>
        public const string available_date = "available_date";
        /// <summary>
        /// Дата создания товара
        /// </summary>
        public const string date_add = "date_add";
        /// <summary>
        /// Отображать цену (0 = нет, 1 = да)
        /// </summary>
        public const string show_price = "show_price";
        /// <summary>
        /// URL изображений (x,y,z...)
        /// </summary>
        public const string image = "image";
        /// <summary>
        /// Удалить существующие изображения (0 = нет, 1 = да)
        /// </summary>
        public const string delete_existing_images = "delete_existing_images";
        /// <summary>
        /// Свойство(Наименование:Стоимость:Позиция:Настроено)
        /// </summary>
        public const string features = "features";
        /// <summary>
        /// Доступен только в режиме онлайн (0 = нет, 1 = да)
        /// </summary>
        public const string online_only = "online_only";
        /// <summary>
        /// Состояние
        /// </summary>
        public const string condition = "condition";
        /// <summary>
        /// Настраиваемый (0 = Нет, 1 = Да)
        /// </summary>
        public const string customizable = "customizable";
        /// <summary>
        /// Загружаемые файлы (0 = Нет, 1 = Да)
        /// </summary>
        public const string uploadable_files = "uploadable_files";
        /// <summary>
        /// Текстовые поля (0 = Нет, 1 = Да)
        /// </summary>
        public const string text_fields = "text_fields";
        /// <summary>
        /// Действия когда нет в наличии
        /// </summary>
        public const string out_of_stock = "out_of_stock";
        /// <summary>
        /// ID / Название магазина
        /// </summary>
        public const string shop = "shop";
        /// <summary>
        /// Расширенное управление запасами
        /// </summary>
        public const string advanced_stock_management = "advanced_stock_management";
        /// <summary>
        /// Depends on stock
        /// </summary>
        public const string depends_on_stock = "depends_on_stock";
        /// <summary>
        /// Склад
        /// </summary>
        public const string warehouse = "warehouse";


        // если создание товара, то все поля, если редактирование только измененные и id
        //public static readonly string[] CSVFeilds = { id, reference, name, description, category, price_tex, quantity, image };

        //    "\"ID налога\";\"Фото\";\"Имя\";\"Категория\";\"Цена\";\"Количество\";"
        
        //public static readonly string[] CSVFeilds = { id_tax_rules_group, image, name, category, price_tex, quantity };
        public static readonly string[] CSVFeilds = { id_tax_rules_group, id, reference, name, description, category, price_tex, quantity, image };
        
        private static object GetValue(Adv24au adv, string csvFeild)
        {

            switch (csvFeild)
            {

                case id:
                    return adv.Number;
                case reference:
                    return adv.Id;
                case name:
                    return adv.Title;
                case description:
                    return string.Empty;//  adv.Description;
                case category:
                    return adv.Category.Number;
                case price_tex:
                    return adv.Price;
                case quantity:
                    return adv.GetQuantity();
                case image:
                    return adv.Image;

                case id_tax_rules_group:
                    return 0;

                default: throw new ArgumentOutOfRangeException();

            }
        }

        private static string CSVEncode(string value)
        {
            return value;value = value.Replace("\"", "\"\"")
                 .Replace(";", "\";\"")
                 .Replace(",", "\",\"")
                 .Replace(Environment.NewLine, string.Format("\"{0}\"", Environment.NewLine));

            return value;
        }

        //private static AdvCategory GetCategoryValues(AdvCategory advCategory)
        //{
        //    string result = "";
        //    if (advCategory.Parent != null)
        //    {
        //        result = GetCategoryValues(advCategory.Parent) + ",";
        //    }

        //    result += string.Format("\"{0}\"", advCategory.Number);

        //    return adv.Category;
        //}

        public static string GetCsvString(Adv24au adv)
        {
            var sb = new StringBuilder();

            foreach (var csvFeild in CSVFeilds)
            {
                var value = GetValue(adv, csvFeild);

                if (value is string)
                {
                    value = CSVEncode(value as string);
                }

                sb.AppendFormat("\"{0}\";", value);
            }

            var result = sb.ToString();

            return result;
        }


        public static string GetCsvColumns()
        {
            //    &type_value%5B0%5D=id_tax_rules_group

            var sb = new StringBuilder();

            for (int i = 0; i < CSVFeilds.Length; i++)
            {
                var csvFeild = CSVFeilds[i];
                sb.AppendFormat("&type_value%5B{0}%5D={1}", i, csvFeild);
            }

            string result = sb.ToString();

            return result;
        }
    }
}