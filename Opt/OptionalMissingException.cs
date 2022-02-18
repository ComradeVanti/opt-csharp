using System;

namespace ComradeVanti.CSharpTools
{

    public class OptionalMissingException : InvalidOperationException
    {

        public OptionalMissingException()
            : base("Attempted to read missing value from an optional") { }

    }

}