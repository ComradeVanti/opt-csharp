﻿using System;

namespace ComradeVanti.CSharpTools
{

    /// <summary>
    ///     A value that may be present or not
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    public abstract class Opt<TValue> { }

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