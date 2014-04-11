using System.Collections.Generic;
using System.Linq;

namespace ToyShopDataLib
{
    public class MarketCategory
    {
        public static readonly MarketCategory MillitaryTheme = new MarketCategory("Военная тема", 942);
        public static readonly MarketCategory ForSandbox = new MarketCategory("Для игры в песочнице и в снегу", 1971);
        public static readonly MarketCategory ForBabies = new MarketCategory("Для младенцев", 935);
        public static readonly MarketCategory ForBathroom = new MarketCategory("Игрушки для купания", 1595);
        public static readonly MarketCategory Frameworks = new MarketCategory("Конструкторы и наборы", 939);
        public static readonly MarketCategory Dolls = new MarketCategory("Куклы и аксессуары", 936);
        public static readonly MarketCategory Cars = new MarketCategory("Машины и техника", 937);
        public static readonly MarketCategory Musical = new MarketCategory("Музыкальные", 941);
        public static readonly MarketCategory Soft = new MarketCategory("Мягкие", 934);
        public static readonly MarketCategory TableGames = new MarketCategory("Настольные игры", 943);
        public static readonly MarketCategory Puzzle = new MarketCategory("Пазлы", 938);
        public static readonly MarketCategory Inteligent = new MarketCategory("Развивающие", 1532);
        public static readonly MarketCategory Electro = new MarketCategory("Электронные", 940);
        public static readonly MarketCategory Other = new MarketCategory("Другое", 944);

        public static readonly MarketCategory Default = Other;


        public static readonly MarketCategory[] MarketCategorySet = { MillitaryTheme, ForSandbox, ForBabies, ForBathroom, Frameworks, Dolls, Cars, Musical, Soft, TableGames, Puzzle, Inteligent, Electro, Other };


        public string Title { get; set; }

        public int Value { get; set; }


        private MarketCategory(string title, int value)
        {
            Title = title;
            Value = value;
        }

        public override string ToString()
        {
            return Title;
        }


        private static Dictionary<int, MarketCategory> _marketCategoryDict;
        public static MarketCategory Get(int defMrkCategory)
        {
            var marketCategoryDict = GetMarketCategoryDict();

            MarketCategory result;
            var contains = marketCategoryDict.TryGetValue(defMrkCategory, out result);
            if (!contains)
            {
                result = Default;
            }

            return result;
        }

        private static Dictionary<int, MarketCategory> GetMarketCategoryDict()
        {
            if (_marketCategoryDict == null)
            {
                _marketCategoryDict = MarketCategorySet.ToDictionary(m => m.Value);
            }

            return _marketCategoryDict;
        }
    }
}