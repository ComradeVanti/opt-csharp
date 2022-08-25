using System.Collections.Generic;
using System.Linq;

namespace ComradeVanti.CSharpTools.UtilityExtensions
{

    // ReSharper disable once InconsistentNaming
    public static class IEnumerableExt
    {

        public static IOpt<T> TryFirst<T>(this IEnumerable<T> items) =>
            Opt.FromOp(items.First);

        public static IOpt<T> TryLast<T>(this IEnumerable<T> items) =>
            Opt.FromOp(items.Last);

        public static IOpt<T> TryElementAt<T>(this IEnumerable<T> items, int index) =>
            Opt.FromOp(() => items.ElementAt(index));

        public static IOpt<IEnumerable<T>> Collect<T>(this IEnumerable<IOpt<T>> opts) => 
            Opt.FromOp(() => opts.Select(it => it.Get()));

    }

}