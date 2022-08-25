using System;

namespace ComradeVanti.CSharpTools
{

    /// <summary>
    ///     An optional value that is present
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    internal sealed class Some<TValue> : IOpt<TValue>
    {

        private readonly TValue value;


        public Some(TValue value) =>
            this.value = value;


        public TResult Match<TResult>(Func<TValue, TResult> onSome, Func<TResult> _) =>
            onSome(value);


        public override bool Equals(object obj) =>
            obj is IOpt<TValue> other
            && other.Match(otherValue => Equals(value, otherValue),
                           () => false);

        public override int GetHashCode() =>
            value.GetHashCode();

        public override string ToString() =>
            $"Opt {{ {value} }}";

    }

    /// <summary>
    ///     An optional value that is not present
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    internal sealed class None<TValue> : IOpt<TValue>
    {

        public TResult Match<TResult>(Func<TValue, TResult> _, Func<TResult> onNone) =>
            onNone();


        public override bool Equals(object obj) =>
            obj is IOpt<TValue> other
            && other.Match(_ => false,
                           () => true);

        public override int GetHashCode() =>
            0;


        public override string ToString() =>
            "Opt { }";

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
        public static IOpt<TValue> Some<TValue>(TValue value) =>
            new Some<TValue>(value);

        /// <summary>
        ///     Creates a missing optional
        /// </summary>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The created optional</returns>
        public static IOpt<TValue> None<TValue>() =>
            new None<TValue>();
        
        /// <summary>
        ///     Creates an optional from a nullable, which will be missing if the
        ///     given value is null
        /// </summary>
        /// <param name="value">The value to convert into an optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The created optional</returns>
        public static IOpt<TValue> FromNullable<TValue>(TValue value) where TValue : class =>
            value != null ? Some(value) : None<TValue>();

        /// <summary>
        ///     Creates an optional from a nullable, which will be missing if the
        ///     given value is null
        /// </summary>
        /// <param name="value">The value to convert into an optional</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The created optional</returns>
        public static IOpt<TValue> FromNullable<TValue>(TValue? value) where TValue : struct =>
            value != null ? Some(value.Value) : None<TValue>();

        /// <summary>
        ///     Creates an optional from an operation that might fail (throw an exception)
        /// </summary>
        /// <param name="op">The operation</param>
        /// <typeparam name="TValue">The type of the contained value</typeparam>
        /// <returns>The created optional</returns>
        public static IOpt<TValue> FromOp<TValue>(Func<TValue> op)
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