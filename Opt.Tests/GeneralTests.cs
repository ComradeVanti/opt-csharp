using System.Diagnostics.CodeAnalysis;
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

        [Fact] [SuppressMessage("ReSharper", "EqualExpressionComparison")]
        public void OptsAreRelatedByEquality()
        {
            Assert.True(Opt.Some(0) == Opt.Some(0), "Equal opts should be same");
            Assert.False(Opt.Some(0) == Opt.Some(1), "Unequal opts should not be same");
            Assert.False(Opt.Some(0) != Opt.Some(0), "Equal opts should not be non-same");
            Assert.True(Opt.Some(0) != Opt.Some(1), "Unequal opts should be non-same");
        }

        [Property]
        public bool GettingValueFromSomeGetsTheValue(int i) =>
            Opt.Some(i).Get() == i;

        [Fact]
        public void GettingValueFromNoneThrowsException() => 
            Assert.Throws<OptionalMissingException>(() => Opt.None<int>().Get());

    }

}