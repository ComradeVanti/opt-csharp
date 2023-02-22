using System.Linq;
using FsCheck.Xunit;
using Xunit;

namespace ComradeVanti.CSharpTools;

public class GeneralTests
{
    [Property]
    public bool OptsThatAreSomeAreNeverNone(IOpt<int> opt) =>
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

    [Property]
    public bool GettingValueFromSomeGetsTheValue(int i) =>
        Opt.Some(i).Get() == i;

    [Fact]
    public void GettingValueFromNoneThrowsException() =>
        Assert.Throws<OptionalMissingException>(() => Opt.None<int>().Get());

    [Property]
    public bool NoneOptsNeverContainAValue(int i) =>
        !Opt.None<int>().Contains(i);

    [Property]
    public bool SomeOptsContainValueIfItIsEqual(int i) =>
        Opt.Some(i).Contains(i);

    [Property]
    public bool SomeOptsDontContainValueIfItIsNotEqual(int i) =>
        !Opt.Some(i).Contains(i + 1);

    [Fact]
    public void NoneIsEmptyCollection()
    {
        var opt = Opt.None<int>();
        Assert.Empty(opt);
    }

    [Property]
    public bool SomeIsSingletonCollection(int i)
    {
        var opt = Opt.Some(i);
        return opt.ToArray().SequenceEqual(new[] {i});
    }
}