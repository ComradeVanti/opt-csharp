using System;
using System.Collections.Generic;
using System.Linq;

namespace ComradeVanti.CSharpTools.UtilityExtensions
{

    // ReSharper disable once InconsistentNaming
    public static class IEnumerableExt
    {

        /// <summary>
        ///     Attempts to get the first element from the sequence
        /// </summary>
        /// <param name="items">The sequence</param>
        /// <typeparam name="T">The type of item in the sequence</typeparam>
        /// <returns>The first item or None, if the sequence is empty</returns>
        public static IOpt<T> TryFirst<T>(this IEnumerable<T> items) =>
            Opt.FromOp(items.First);

        /// <summary>
        ///     Attempts to get the last element from the sequence
        /// </summary>
        /// <param name="items">The sequence</param>
        /// <typeparam name="T">The type of item in the sequence</typeparam>
        /// <returns>The last item or None, if the sequence is empty</returns>
        public static IOpt<T> TryLast<T>(this IEnumerable<T> items) =>
            Opt.FromOp(items.Last);

        /// <summary>
        ///     Attempts to get an element from the sequence by its index
        /// </summary>
        /// <param name="items">The sequence</param>
        /// <param name="index">The index of the item to get</param>
        /// <typeparam name="T">The type of item in the sequence</typeparam>
        /// <returns>The item or None, if no such index exists</returns>
        public static IOpt<T> TryElementAt<T>(this IEnumerable<T> items, int index) =>
            Opt.FromOp(() => items.ElementAt(index));

        /// <summary>
        ///     Selects and maps all items in the sequence where the given choose function
        ///     returned some
        /// </summary>
        /// <param name="items">The sequence</param>
        /// <param name="choose">The choose function</param>
        /// <typeparam name="T">The type of item in the sequence</typeparam>
        /// <typeparam name="TChosen">The type of the chosen sequence</typeparam>
        /// <returns>The sequence of chosen items</returns>
        public static IEnumerable<TChosen> Choose<T, TChosen>(this IEnumerable<T> items, Func<T, IOpt<TChosen>> choose) =>
            from item in items
            let chosen = choose(item)
            where chosen.IsSome()
            select chosen.Get();

        /// <summary>
        ///     Collects a sequence of optionals
        /// </summary>
        /// <param name="opts">The sequence</param>
        /// <typeparam name="T">The type of the contained value</typeparam>
        /// <returns>
        ///     The sequence if all optionals were present or none if one or more
        ///     where missing
        /// </returns>
        public static IOpt<IEnumerable<T>> Collect<T>(this IEnumerable<IOpt<T>> opts) =>
            Opt.FromOp(() => opts.Select(it => it.Get()));

    }

}