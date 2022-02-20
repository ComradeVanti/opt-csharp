using System;

namespace ComradeVanti.CSharpTools
{

    /// <summary>
    ///     Exception thrown when a missing optional is accessed. Equivalent to a
    ///     conventional null-pointer exception.
    /// </summary>
    public class OptionalMissingException : InvalidOperationException
    {

        public OptionalMissingException()
            : base("Attempted to read missing value from an optional") { }

    }

}