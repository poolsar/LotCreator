using System;
using System.Linq;

namespace Utils
{
    public static class StringUtils
    {
        public static string ExeptString(this string fileName, string ext)
        {
            return fileName.Split(new[] { ext }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        }
    }
}