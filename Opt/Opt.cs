﻿using System;

namespace ComradeVanti.CSharpTools
{

    /// <summary>
    ///     A value that may be present or not
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    public abstract class Opt<TValue>
    {

        public override bool Equals(object obj)
        {
            switch (this)
            {
                case Some<TValue> some1 when obj is Some<TValue> some2:
                    return Equals(some1.Value, some2.Value);
                case None<TValue> _ when obj is None<TValue> _:
                    return true;
                default: return false;
            }
        }

        public override int GetHashCode()
        {
            switch (this)
            {
                case Some<TValue> some:
                    return some.GetHashCode();
                case None<TValue> _:
                    return 0;
                default:
                    throw new Exception("Invalid type"); // Here for the compiler. Should never happen
            }
        }

        public static bool operator ==(Opt<TValue> opt1, Opt<TValue> opt2) =>
            Equals(opt1, opt2);

        public static bool operator !=(Opt<TValue> opt1, Opt<TValue> opt2) =>
            !Equals(opt1, opt2);

    }

    /// <summary>
    ///     An optional value that is present
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    internal sealed class Some<TValue> : Opt<TValue>
    {

        public TValue Value { get; }


        public Some(TValue value) =>
            Value = value;

    }

    /// <summary>
    ///     An optional value that is not present
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    internal sealed class None<TValue> : Opt<TValue> { }


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

    }

    /// <summary>
    ///     Contains convenience-methods for creating optionals
    /// </summary>
    public static class Opt
    {

        /// <summary>
        ///     Creates a present optional
        /// </summary>
        /// <param name="value">The value to be contained in the optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The created optional</returns>
        public static Opt<TValue> Some<TValue>(TValue value) =>
            new Some<TValue>(value);

        /// <summary>
        ///     Creates a missing optional
        /// </summary>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The created optional</returns>
        public static Opt<TValue> None<TValue>() =>
            new None<TValue>();

        /// <summary>
        ///     Creates an optional from a nullable, which will be missing if the
        ///     given value is null
        /// </summary>
        /// <param name="value">The value to convert into an optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The created optional</returns>
        public static Opt<TValue> FromNullable<TValue>(TValue value) where TValue : new() =>
            value != null ? Some(value) : None<TValue>();

        /// <summary>
        ///     Creates an optional from a nullable, which will be missing if the
        ///     given value is null
        /// </summary>
        /// <param name="value">The value to convert into an optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The created optional</returns>
        public static Opt<TValue> FromNullable<TValue>(TValue? value) where TValue : struct =>
            value != null ? Some(value.Value) : None<TValue>();

        /// <summary>
        ///     Creates an optional from an operation that might fail (throw an exception)
        /// </summary>
        /// <param name="op">The operation</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The created optional</returns>
        public static Opt<TValue> FromOp<TValue>(Func<TValue> op)
        {
            try
            {
                return Some(op());
            }
            catch
            {
                return None<TValue>();
            }
        }

    }

}