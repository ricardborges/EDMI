using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// For a Key/Value pair: 
        /// 
        ///     - If the key not exist the Value is inserted to the dictionary.
        ///     - If the key exist the Value associated to the Key is updated to the dictionary.
        /// </summary>
        public static void Set<K, V>(this Dictionary<K, V> dictionary, K key, V value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
            else
                dictionary[key] = value;
        }


        /// <summary>
        ///     - If the dictionary contains the Key, the function returns the value associated.
        ///     - Else, the function returns the 'defaultValue'.
        /// </summary>
        public static V Get<K, V>(this Dictionary<K, V> dictionary, K key, V defaultValue)
        {
            V result;

            if (dictionary.ContainsKey(key))
            {
                result = dictionary[key];
            }
            else
                result = defaultValue;

            return (result);
        }
    }
}

