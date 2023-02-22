using System;
using System.Collections.Generic;

namespace ComradeVanti.CSharpTools
{

    /// <summary>
    ///     A value that may or may not be present
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    public interface IOpt<out TValue> : IEnumerable<TValue>
    {

        TResult Match<TResult>(Func<TValue, TResult> onSome, Func<TResult> onNone);
        
    }

    /// <summary>
    ///     An option that is present
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    public interface ISome<out TValue> : IOpt<TValue>
    {
        TValue Value { get; }
    }

    /// <summary>
    ///     An option that is missing
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value</typeparam>
    public interface INone<out TValue> : IOpt<TValue> { }

}