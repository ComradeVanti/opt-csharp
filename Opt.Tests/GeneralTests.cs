using FsCheck.Xunit;
using Xunit;

namespace ComradeVanti.CSharpTools
{

    public class GeneralTests
    {

        [Property]
        public bool OptsThatAreSomeAreNeverNone(Opt<int> opt) =>
            opt.IsSome() != opt.IsNone();

        [Fact]
        public void ComparingSomeComparesTheirContainedValue()
        {
            Assert.Equal(Opt.Some(0), Opt.Some(0));
            Assert.NotEqual(Opt.Some(0), Opt.Some(1));
        }

        [Fact]
        public void SomeAndNoneAreNotEqual() =>
            Assert.NotEqual(Opt.Some(0), Opt.None<int>());

        [Fact]
        public void NoneAndNoneOfTheSameTypeAreEqual() =>
            Assert.Equal(Opt.None<int>(), Opt.None<int>());

    }

}