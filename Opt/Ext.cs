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
        public static bool IsSome<TValue>(this Opt<TValue> opt) =>
            opt is Some<TValue>;

        /// <summary>
        ///     Checks if the optional is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>Whether a value is missing or not</returns>
        public static bool IsNone<TValue>(this Opt<TValue> opt) =>
            opt is None<TValue>;

        /// <summary>
        ///     Attempts to get the value from the optional and throws an
        ///     exception if the value is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The contained value</returns>
        /// <exception cref="OptionalMissingException">If the optional is missing</exception>
        public static TValue Get<TValue>(this Opt<TValue> opt) =>
            opt.Match(it => it,
                      () => throw new OptionalMissingException());

        /// <summary>
        ///     Executes the onSome action if the optional is present, passing in
        ///     the value or the onNone if it is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="onSome">The action for when the optional is present</param>
        /// <param name="onNone">The action for when the optional is missing</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        public static void Match<TValue>(this Opt<TValue> opt, Action<TValue> onSome, Action onNone)
        {
            switch (opt)
            {
                case Some<TValue> some:
                    onSome(some.Value);
                    return;
                case None<TValue> _:
                    onNone();
                    return;
            }
        }

        /// <summary>
        ///     Executes the onSome function if the optional is present, passing in
        ///     the value or the onNone if it is missing and returns the result
        ///     of the executed function.
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="onSome">The function to execute if the optional is present</param>
        /// <param name="onNone">The function to execute if the optional is missing</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <typeparam name="TMapped">The type of the result</typeparam>
        /// <returns>The result of either the onSome or onNone function</returns>
        public static TMapped Match<TValue, TMapped>(this Opt<TValue> opt, Func<TValue, TMapped> onSome, Func<TMapped> onNone)
        {
            switch (opt)
            {
                case Some<TValue> some: return onSome(some.Value);
                case None<TValue> _: return onNone();
                default: throw new Exception("Invalid type");
            }
        }

        /// <summary>
        ///     Executes the given action if the optional is present,
        ///     passing in the contained value.
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="action">The action to execute</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        public static void Iter<TValue>(this Opt<TValue> opt, Action<TValue> action) =>
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
        public static Opt<TMapped> Map<TMapped, TValue>(this Opt<TValue> opt, Func<TValue, TMapped> mapper) =>
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
        public static Opt<TMapped> Bind<TMapped, TValue>(this Opt<TValue> opt, Func<TValue, Opt<TMapped>> mapper) =>
            opt.Match(mapper, Opt.None<TMapped>);

        /// <summary>
        ///     Checks if this optional contains a specific value. If the optional
        ///     is missing false is returned.
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="value">The value to check</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>Whether the optional contains the value</returns>
        public static bool Contains<TValue>(this Opt<TValue> opt, TValue value) =>
            opt.Match(it => Equals(it, value), () => false);

        /// <summary>
        ///     Gets the value from this optional or a replacement if the value
        ///     is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="replacement">The replacement value</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The value or the replacement</returns>
        public static TValue DefaultValue<TValue>(this Opt<TValue> opt, TValue replacement) =>
            opt.Match(it => it, () => replacement);

        /// <summary>
        ///     Gets the "count" of this optional, meaning 1 if it is present and 0 if it
        ///     is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The count of the  optional</returns>
        public static int Count<TValue>(this Opt<TValue> opt) =>
            opt.Match(_ => 1, () => 0);

        /// <summary>
        ///     Gets the value or the result of the replacement-function if the optional is
        ///     missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="replacementF">The replacement-function</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>Thw value or replacement</returns>
        public static TValue DefaultWith<TValue>(this Opt<TValue> opt, Func<TValue> replacementF) =>
            opt.Match(it => it, replacementF);

        /// <summary>
        ///     Checks if the value in the optional satisfies a predicate. Returns false if
        ///     the optional is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="predicate">The predicate</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>Whether the value satisfies the predicate</returns>
        public static bool Exists<TValue>(this Opt<TValue> opt, Func<TValue, bool> predicate) =>
            opt.Match(predicate, () => false);

        /// <summary>
        ///     Filters the optional with a predicate. If the optional is present but the
        ///     predicate is not satisfied a missing optional is returned.
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="predicate">The predicate</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The filtered optional</returns>
        public static Opt<TValue> Filter<TValue>(this Opt<TValue> opt, Func<TValue, bool> predicate) =>
            opt.Bind(it => predicate(it) ? opt : Opt.None<TValue>());

        /// <summary>
        ///     Collapses a nested optional into a flat one
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The flattened optional</returns>
        public static Opt<TValue> Flatten<TValue>(this Opt<Opt<TValue>> opt) =>
            opt.Match(it => it, Opt.None<TValue>);

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
        public static TState Fold<TValue, TState>(this Opt<TValue> opt, Func<TState, TValue, TState> folder, TState state) =>
            opt.Match(it => folder(state, it), () => state);

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
        public static TState FoldBack<TValue, TState>(this Opt<TValue> opt, Func<TValue, TState, TState> folder, TState state) =>
            opt.Match(it => folder(it, state), () => state);


        /// <summary>
        ///     Checks if the value in the optional satisfies a predicate. Returns true if
        ///     the optional is missing
        /// </summary>
        /// <param name="opt">The optional</param>
        /// <param name="predicate">The predicate</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>Whether the value satisfies the predicate</returns>
        public static bool ForAll<TValue>(this Opt<TValue> opt, Func<TValue, bool> predicate) =>
            opt.Match(predicate, () => true);

    }

}