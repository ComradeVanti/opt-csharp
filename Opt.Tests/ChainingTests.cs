using FsCheck.Xunit;

namespace ComradeVanti.CSharpTools
{

    public class ChainingTests
    {

        [Property]
        public static bool IterActionIsOnlyCalledForSome(Opt<int> opt)
        {
            var executed = false;

            opt.Iter(_ => executed = true);

            return executed == opt.IsSome();
        }

    }

}