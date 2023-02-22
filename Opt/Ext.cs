using System;

namespace ComradeVanti.CSharpTools
{
    public static class Ext
    {
        /// <summary>
        ///     Checks if the optional is present
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>Whether a value is present or not</returns>
        public static bool IsSome<TValue>(this IOpt<TValue> opt) =>
            opt.Match(_ => true,
                () => false);

        /// <summary>
        ///     Checks if the optional is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>Whether a value is missing or not</returns>
        public static bool IsNone<TValue>(this IOpt<TValue> opt) =>
            opt.Match(_ => false,
                () => true);

        /// <summary>
        ///     Attempts to get the value from the optional and throws an
        ///     exception if the value is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The contained value</returns>
        /// <exception cref="OptionalMissingException">If the optional is missing</exception>
        public static TValue Get<TValue>(this IOpt<TValue> opt) =>
            opt.Match(it => it,
                () => throw new OptionalMissingException());

        /// <summary>
        ///     Executes either a onSome or onNone action depending on if the optional is present
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="onSome">The action for when the optional is present</param>
        /// <param name="onNone">The action for when the optional is missing</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        public static void Match<TValue>(this IOpt<TValue> opt, Action<TValue> onSome, Action onNone) =>
            _ = opt.Match(value =>
                {
                    onSome(value);
                    return 0;
                },
                () =>
                {
                    onNone();
                    return 0;
                });

        /// <summary>
        ///     Executes the given action if the optional is present,
        ///     passing in the contained value.
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="action">The action to execute</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        public static void Iter<TValue>(this IOpt<TValue> opt, Action<TValue> action) =>
            opt.Match(action, () => { });

        /// <summary>
        ///     Maps an optional from one type to another using a mapping-function.
        ///     If the optional is present its value will be passed to the
        ///     mapping-function and the result wrapped in a new optional. If the optional
        ///     is missing a new missing optional will be returned.
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="mapper">The mapping function</param>
        /// <typeparam name="TMapped">The type of the mapped optional</typeparam>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The mapped optional</returns>
        public static IOpt<TMapped> Map<TMapped, TValue>(this IOpt<TValue> opt, Func<TValue, TMapped> mapper) =>
            opt.Match(it => Opt.Some(mapper(it)),
                Opt.None<TMapped>);

        /// <summary>
        ///     Maps an optional from one type to another using a mapping-function
        ///     which itself produces an optional.
        ///     If the optional is present its value will be passed to the
        ///     mapping-function and the result returned. If the optional
        ///     is missing a new missing optional will be returned.
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="mapper">The mapping function</param>
        /// <typeparam name="TMapped">The type of the mapped optional</typeparam>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The mapped optional</returns>
        public static IOpt<TMapped> Bind<TMapped, TValue>(this IOpt<TValue> opt, Func<TValue, IOpt<TMapped>> mapper) =>
            opt.Match(mapper,
                Opt.None<TMapped>);

        /// <summary>
        ///     Gets the value from this optional or a replacement if the value
        ///     is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="replacement">The replacement value</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The value or the replacement</returns>
        public static TValue DefaultValue<TValue>(this IOpt<TValue> opt, TValue replacement) =>
            opt.Match(it => it,
                () => replacement);

        /// <summary>
        ///     Gets the value or the result of the replacement-function if the optional is
        ///     missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="replacementF">The replacement-function</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>Thw value or replacement</returns>
        public static TValue DefaultWith<TValue>(this IOpt<TValue> opt, Func<TValue> replacementF) =>
            opt.Match(it => it,
                replacementF);

        /// <summary>
        ///     Filters the optional with a predicate. If the optional is present but the
        ///     predicate is not satisfied a missing optional is returned.
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="predicate">The predicate</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The filtered optional</returns>
        public static IOpt<TValue> Filter<TValue>(this IOpt<TValue> opt, Func<TValue, bool> predicate) =>
            opt.Bind(it => predicate(it) ? opt : Opt.None<TValue>());

        /// <summary>
        ///     Collapses a nested optional into a flat one
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The flattened optional</returns>
        public static IOpt<TValue> Flatten<TValue>(this IOpt<IOpt<TValue>> opt) =>
            opt.Match(it => it,
                Opt.None<TValue>);

        /// <summary>
        ///     Executes a folder-function with the value if present and returns the result
        ///     otherwise the initial state
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="folder">The folder-function</param>
        /// <param name="state">The initial state</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <typeparam name="TState">The type of the state</typeparam>
        /// <returns>The result of the folder-function or initial state</returns>
        public static TState Fold<TValue, TState>(this IOpt<TValue> opt, Func<TState, TValue, TState> folder, TState state) =>
            opt.Match(it => folder(state, it),
                () => state);

        /// <summary>
        ///     Executes a folder-function with the value if present and returns the result
        ///     otherwise the initial state
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="folder">The folder-function</param>
        /// <param name="state">The initial state</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <typeparam name="TState">The type of the state</typeparam>
        /// <returns>The result of the folder-function or initial state</returns>
        public static TState FoldBack<TValue, TState>(this IOpt<TValue> opt, Func<TValue, TState, TState> folder, TState state) =>
            opt.Match(it => folder(it, state),
                () => state);

        /// <summary>
        ///     Checks if the value in the optional satisfies a predicate. Returns true if
        ///     the optional is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="predicate">The predicate</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>Whether the value satisfies the predicate</returns>
        public static bool ForAll<TValue>(this IOpt<TValue> opt, Func<TValue, bool> predicate) =>
            opt.Match(predicate,
                () => true);
    }
}