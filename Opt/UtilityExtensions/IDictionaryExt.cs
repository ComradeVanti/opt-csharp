using System.Collections.Generic;

namespace ComradeVanti.CSharpTools.UtilityExtensions
{

    // ReSharper disable once InconsistentNaming
    public static class IDictionaryExt
    {

        /// <summary>
        ///     Attempts to get a value from the dictionary
        /// </summary>
        /// <param name="dict">The dictionary</param>
        /// <param name="key">The key</param>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TValue">The type of the value</typeparam>
        /// <returns>The value or none if not found</returns>
        public static IOpt<TValue> TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) =>
            dict.TryGetValue(key, out var value)
                ? Opt.Some(value)
                : Opt.None<TValue>();

    }

}