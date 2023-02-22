using System;
using System.Collections;
using System.Collections.Generic;

namespace ComradeVanti.CSharpTools
{
    /// <summary>
    ///     An optional value that is present
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    internal sealed class Some<TValue> : ISome<TValue>
    {
        public Some(TValue value) =>
            Value = value;

        public TValue Value { get; }


        public TResult Match<TResult>(Func<TValue, TResult> onSome, Func<TResult> _) =>
            onSome(Value);


        public IEnumerator<TValue> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override bool Equals(object obj) =>
            obj is IOpt<TValue> other
            && other.Match(otherValue => Equals(Value, otherValue),
                () => false);

        public override int GetHashCode() =>
            Value.GetHashCode();

        public override string ToString() =>
            $"Opt {{ {Value} }}";
    }
}