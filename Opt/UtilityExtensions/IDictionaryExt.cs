using System.Collections.Generic;

namespace ComradeVanti.CSharpTools.UtilityExtensions
{

    // ReSharper disable once InconsistentNaming
    public static class IDictionaryExt
    {

        public static IOpt<TValue> TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) =>
            dict.TryGetValue(key, out var value)
                ? Opt.Some(value)
                : Opt.None<TValue>();

    }

}