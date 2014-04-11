using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Contrib;
using ToyShopDataLib.Utils;

namespace ToyShopDataLib
{
    public class ProductCode
    {
        static char[] charsToEncode =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v','w', 'x', 'y', 'z','0', '1', '2', '3', '4', '5', '6', '7', '8', '9','à', 'á', 'â', 'ã', 'ä', 'å', '¸', 'æ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï', 'ð', 'ñ', 'ò', 'ó', 'ô', 'õ', 'ö', '÷', 'ø', 'ù', 'ú', 'û', 'ü', 'ý', 'þ', 'ÿ'
        };


        public static string Encode(byte[] bytes)
        {
            byte[] bytes3 = bytes;//.Take(3).ToArray();

            var bitArray = new BitArray(bytes3);
            byte[] byteArray = ToByteArray(bitArray, 5);

            List<byte> byteList = new List<byte>();
            byteList.AddRange(byteArray);
            //if (byteArray.Length % 2 != 0)
            //{
            //    byteList.Add(new byte());
            //}
            byteArray = byteList.ToArray();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < byteList.Count; i += 1)
            {
                int charIndex = Convert.ToUInt16(byteArray[i]);
                char charV = charsToEncode[charIndex];
                sb.Append(charV);
            }

            string result = sb.ToString();

            return result;
        }


        public static byte[] ToByteArray(BitArray bits, int bitsCount)
        {
            int numBytes = bits.Count / bitsCount;
            if (bits.Count % bitsCount != 0) numBytes++;
            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;
            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (bitsCount - 1 - bitIndex));
                bitIndex++;
                if (bitIndex == bitsCount)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }
            return bytes;
        }

        public static string Encode(int id)
        {
            return id.ToString();

            var digit = Convert.ToUInt16(id);
            var bytes = BitConverter.GetBytes(digit);
            //string str = Convert.ToBase64String(bytes);

            string str = Encode(bytes);
            return str;
        }

        private static string prefix = "3gel";

        public static string GetCode(Product product)
        {
            var style = product.GetSpecialStyle();
            var sale = product.GetActiveSale();

            string prodCode = prefix;
            
           
            prodCode += string.Format("-p{0}", Encode(product.Id));

            
            if (style != null)
            {
                if (!prodCode.IsEmpty()) prodCode += "-";
                prodCode += style.GetCode();
            }

            if (sale != null)
            {
                if (!prodCode.IsEmpty()) prodCode += "-";
                prodCode += sale.GetCode();
            }

            return prodCode;
        }

        public static string GetCode(Style style, bool tagMode=false)
        {
            if (style == null) return string.Empty;

            string code = string.Format("s{0}", Encode(style.Id));

            if (tagMode)
            {
                code = string.Format("{0} {1}", prefix, code);
                code = HttpUtility.UrlEncode(code);
            }

            return code;
        }

        public static string GetCode(Sale sale, bool tagMode = false)
        {
            if (sale == null) return string.Empty;

            string code = string.Format("a{0}", Encode(sale.Id));

            if (tagMode)
            {
                code = string.Format("{0} {1}", prefix, code);
                code = HttpUtility.UrlEncode(code);
            }

            return code;
        }
    }
}