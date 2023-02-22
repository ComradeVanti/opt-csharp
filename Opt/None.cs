using System;
using System.Collections;
using System.Collections.Generic;

namespace ComradeVanti.CSharpTools
{
    /// <summary>
    ///     An optional value that is not present
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    internal sealed class None<TValue> : INone<TValue>
    {
        public TResult Match<TResult>(Func<TValue, TResult> _, Func<TResult> onNone) =>
            onNone();


        public IEnumerator<TValue> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override bool Equals(object obj) =>
            obj is IOpt<TValue> other
            && other.Match(_ => false,
                () => true);

        public override int GetHashCode() =>
            0;


        public override string ToString() =>
            "Opt { }";
    }
}