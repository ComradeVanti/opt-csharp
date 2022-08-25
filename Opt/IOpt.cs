using System;

namespace ComradeVanti.CSharpTools
{

    /// <summary>
    ///     A value that may or may not be present
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    public interface IOpt<out TValue>
    {

        TResult Match<TResult>(Func<TValue, TResult> onSome, Func<TResult> onNone);

    }

}