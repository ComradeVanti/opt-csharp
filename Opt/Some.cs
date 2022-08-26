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

}