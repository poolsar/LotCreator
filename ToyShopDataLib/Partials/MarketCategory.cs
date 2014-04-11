using System.Collections.Generic;
using System.Linq;

namespace ToyShopDataLib
{
    public class MarketCategory
    {
        public static readonly MarketCategory MillitaryTheme = new MarketCategory("������� ����", 942);
        public static readonly MarketCategory ForSandbox = new MarketCategory("��� ���� � ��������� � � �����", 1971);
        public static readonly MarketCategory ForBabies = new MarketCategory("��� ���������", 935);
        public static readonly MarketCategory ForBathroom = new MarketCategory("������� ��� �������", 1595);
        public static readonly MarketCategory Frameworks = new MarketCategory("������������ � ������", 939);
        public static readonly MarketCategory Dolls = new MarketCategory("����� � ����������", 936);
        public static readonly MarketCategory Cars = new MarketCategory("������ � �������", 937);
        public static readonly MarketCategory Musical = new MarketCategory("�����������", 941);
        public static readonly MarketCategory Soft = new MarketCategory("������", 934);
        public static readonly MarketCategory TableGames = new MarketCategory("���������� ����", 943);
        public static readonly MarketCategory Puzzle = new MarketCategory("�����", 938);
        public static readonly MarketCategory Inteligent = new MarketCategory("�����������", 1532);
        public static readonly MarketCategory Electro = new MarketCategory("�����������", 940);
        public static readonly MarketCategory Other = new MarketCategory("������", 944);

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