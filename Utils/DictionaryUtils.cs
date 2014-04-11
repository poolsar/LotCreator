using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class DictionaryUtils
    {
        public static K GetOrAdd<T, K>(this Dictionary<T, K> dict, T key) where K : new()
        {

            if (!dict.ContainsKey(key))
            {
                K value = new K();
                dict.Add(key, value);
            }

            return dict[key];
        }

        public static K GetOrAdd<T, K>(this Dictionary<T, K> dict, T key, K value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }

            return dict[key];
        }

        public static T KeyByValue<T, K>(this Dictionary<T, K> dict, K value)
        {
            var kvpImage = dict.FirstOrDefault(kvp => kvp.Value.Equals( value));

            if (kvpImage.Equals(null)) return default(T);

            return kvpImage.Key;

        }
    }
}