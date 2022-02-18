using FsCheck.Xunit;

namespace ComradeVanti.CSharpTools
{

    public class GeneralTests
    {

        [Property]
        public bool OptsThatAreSomeAreNeverNone(Opt<int> opt) => 
            opt.IsSome() != opt.IsNone();

    }

}