using FsCheck.Xunit;
using Xunit;

namespace ComradeVanti.CSharpTools
{

    public class OptTests
    {

        [Property]
        public bool OptsCreatedWithSomeAreAlwaysSome(int i) =>
            Opt.Some(i).IsSome();

        [Property]
        public bool OptsCreatedWithSomeAreNeverNone(int i) =>
            !Opt.Some(i).IsNone();

        [Fact]
        public void OptsCreatedWithNoneAreAlwaysNone() =>
            Assert.True(Opt.None<int>().IsNone());

        [Fact]
        public void OptsCreatedWithNoneAreNeverSome() =>
            Assert.False(Opt.None<int>().IsSome());

    }

}