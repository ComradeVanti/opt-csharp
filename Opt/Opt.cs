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

}