using System.Collections.Generic;

namespace Utils
{
	public enum TransliterationType
	{
		Gost,
		ISO
	}
	public static class Transliteration
	{
		private static Dictionary<string, string> gost = new Dictionary<string, string>(); //ÃÎÑÒ 16876-71
		private static Dictionary<string, string> iso = new Dictionary<string, string>(); //ISO 9-95

		public static string Front(string text)
		{
			return Front(text, TransliterationType.ISO);
		}
		public static string Front(string text, TransliterationType type)
		{
			string output = text;
			Dictionary<string, string> tdict = GetDictionaryByType(type);

			foreach (KeyValuePair<string, string> key in tdict)
			{
				output = output.Replace(key.Key, key.Value);
			}
			return output;
		}
		public static string Back(string text)
		{
			return Back(text, TransliterationType.ISO);
		}
		public static string Back(string text, TransliterationType type)
		{
			string output = text;
			Dictionary<string, string> tdict = GetDictionaryByType(type);

			foreach (KeyValuePair<string, string> key in tdict)
			{
				output = output.Replace(key.Value, key.Key);
			}
			return output;
		}

		private static Dictionary<string, string> GetDictionaryByType(TransliterationType type)
		{
			Dictionary<string, string> tdict = iso;
			if (type == TransliterationType.Gost) tdict = gost;
			return tdict;
		}

		static Transliteration()
		{
			gost.Add("ª", "EH");
			gost.Add("²", "I");
			gost.Add("³", "i");
			gost.Add("¹", "#");
			gost.Add("º", "eh");
			gost.Add("À", "A");
			gost.Add("Á", "B");
			gost.Add("Â", "V");
			gost.Add("Ã", "G");
			gost.Add("Ä", "D");
			gost.Add("Å", "E");
			gost.Add("¨", "JO");
			gost.Add("Æ", "ZH");
			gost.Add("Ç", "Z");
			gost.Add("È", "I");
			gost.Add("É", "JJ");
			gost.Add("Ê", "K");
			gost.Add("Ë", "L");
			gost.Add("Ì", "M");
			gost.Add("Í", "N");
			gost.Add("Î", "O");
			gost.Add("Ï", "P");
			gost.Add("Ğ", "R");
			gost.Add("Ñ", "S");
			gost.Add("Ò", "T");
			gost.Add("Ó", "U");
			gost.Add("Ô", "F");
			gost.Add("Õ", "KH");
			gost.Add("Ö", "C");
			gost.Add("×", "CH");
			gost.Add("Ø", "SH");
			gost.Add("Ù", "SHH");
			gost.Add("Ú", "'");
			gost.Add("Û", "Y");
			gost.Add("Ü", "");
			gost.Add("İ", "EH");
			gost.Add("Ş", "YU");
			gost.Add("ß", "YA");
			gost.Add("à", "a");
			gost.Add("á", "b");
			gost.Add("â", "v");
			gost.Add("ã", "g");
			gost.Add("ä", "d");
			gost.Add("å", "e");
			gost.Add("¸", "jo");
			gost.Add("æ", "zh");
			gost.Add("ç", "z");
			gost.Add("è", "i");
			gost.Add("é", "jj");
			gost.Add("ê", "k");
			gost.Add("ë", "l");
			gost.Add("ì", "m");
			gost.Add("í", "n");
			gost.Add("î", "o");
			gost.Add("ï", "p");
			gost.Add("ğ", "r");
			gost.Add("ñ", "s");
			gost.Add("ò", "t");
			gost.Add("ó", "u");

			gost.Add("ô", "f");
			gost.Add("õ", "kh");
			gost.Add("ö", "c");
			gost.Add("÷", "ch");
			gost.Add("ø", "sh");
			gost.Add("ù", "shh");
			gost.Add("ú", "");
			gost.Add("û", "y");
			gost.Add("ü", "");
			gost.Add("ı", "eh");
			gost.Add("ş", "yu");
			gost.Add("ÿ", "ya");
			gost.Add("«", "");
			gost.Add("»", "");
			gost.Add("—", "-");

			iso.Add("ª", "YE");
			iso.Add("²", "I");
			iso.Add("", "G");
			iso.Add("³", "i");
			iso.Add("¹", "#");
			iso.Add("º", "ye");
			iso.Add("ƒ", "g");
			iso.Add("À", "A");
			iso.Add("Á", "B");
			iso.Add("Â", "V");
			iso.Add("Ã", "G");
			iso.Add("Ä", "D");
			iso.Add("Å", "E");
			iso.Add("¨", "YO");
			iso.Add("Æ", "ZH");
			iso.Add("Ç", "Z");
			iso.Add("È", "I");
			iso.Add("É", "J");
			iso.Add("Ê", "K");
			iso.Add("Ë", "L");
			iso.Add("Ì", "M");
			iso.Add("Í", "N");
			iso.Add("Î", "O");
			iso.Add("Ï", "P");
			iso.Add("Ğ", "R");
			iso.Add("Ñ", "S");
			iso.Add("Ò", "T");
			iso.Add("Ó", "U");
			iso.Add("Ô", "F");
			iso.Add("Õ", "X");
			iso.Add("Ö", "C");
			iso.Add("×", "CH");
			iso.Add("Ø", "SH");
			iso.Add("Ù", "SHH");
			iso.Add("Ú", "'");
			iso.Add("Û", "Y");
			iso.Add("Ü", "");
			iso.Add("İ", "E");
			iso.Add("Ş", "YU");
			iso.Add("ß", "YA");
			iso.Add("à", "a");
			iso.Add("á", "b");
			iso.Add("â", "v");
			iso.Add("ã", "g");
			iso.Add("ä", "d");
			iso.Add("å", "e");
			iso.Add("¸", "yo");
			iso.Add("æ", "zh");
			iso.Add("ç", "z");
			iso.Add("è", "i");
			iso.Add("é", "j");
			iso.Add("ê", "k");
			iso.Add("ë", "l");
			iso.Add("ì", "m");
			iso.Add("í", "n");
			iso.Add("î", "o");
			iso.Add("ï", "p");
			iso.Add("ğ", "r");
			iso.Add("ñ", "s");
			iso.Add("ò", "t");
			iso.Add("ó", "u");
			iso.Add("ô", "f");
			iso.Add("õ", "x");
			iso.Add("ö", "c");
			iso.Add("÷", "ch");
			iso.Add("ø", "sh");
			iso.Add("ù", "shh");
			iso.Add("ú", "");
			iso.Add("û", "y");
			iso.Add("ü", "");
			iso.Add("ı", "e");
			iso.Add("ş", "yu");
			iso.Add("ÿ", "ya");
			iso.Add("«", "");
			iso.Add("»", "");
			iso.Add("—", "-");
		}
	}

	
}