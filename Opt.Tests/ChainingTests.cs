using System.Linq;
using FsCheck.Xunit;
using Xunit;

namespace ComradeVanti.CSharpTools;

public class ChainingTests
{
    [Property]
    public static bool MatchSomeActionIsOnlyCalledForSome(IOpt<int> opt)
    {
        var executed = false;

        opt.Match(_ => executed = true, () => { });

        return executed == opt.IsSome();
    }

    [Fact]
    public static void MatchNoneActionIsOnlyCalledForNone()
    {
        var executed = false;

        Opt.None<int>().Match(_ => { }, () => executed = true);

        Assert.True(executed);
    }

    [Property]
    public static bool MatchSomeFunctionIsOnlyCalledForSome(IOpt<int> opt)
    {
        var executed = false;

        _ = opt.Match(_ =>
        {
            executed = true;
            return 0;
        }, () => 0);

        return executed == opt.IsSome();
    }

    [Fact]
    public static void MatchNoneFunctionIsOnlyCalledForNone()
    {
        var result = Opt.None<int>().Match(_ => 0, () => 1);

        Assert.Equal(1, result);
    }

    [Property]
    public static bool ValueIsCorrectlyMatched(IOpt<int> opt)
    {
        var result = opt.Match(it => (int?) it, () => null);
        return result != null == opt.IsSome();
    }

    [Property]
    public static bool IterActionIsOnlyCalledForSome(IOpt<int> opt)
    {
        var executed = false;

        opt.Iter(_ => executed = true);

        return executed == opt.IsSome();
    }

    [Property]
    public static bool MappingFunctionIsOnlyCalledForSome(IOpt<int> opt)
    {
        var executed = false;

        _ = opt.Map(_ =>
        {
            executed = true;
            return 0;
        });

        return executed == opt.IsSome();
    }

    [Fact]
    public static void MappingNoneProducesNone() =>
        Assert.True(Opt.None<int>().Map(it => it).IsNone());

    [Fact]
    public static void MappingSomeProducesSome() =>
        Assert.True(Opt.Some(1).Map(it => it).IsSome());

    [Property]
    public static bool MappingChangesTheValue(int i) =>
        Opt.Some(i).Map(it => it + 1).Get() == i + 1;

    [Property]
    public static bool BindingFunctionIsOnlyCalledForSome(IOpt<int> opt)
    {
        var executed = false;

        _ = opt.Bind(_ =>
        {
            executed = true;
            return Opt.Some(0);
        });

        return executed == opt.IsSome();
    }

    [Fact]
    public static void BindingNoneProducesNone() =>
        Assert.True(Opt.None<int>().Bind(Opt.Some).IsNone());

    [Fact]
    public static void BindingSomeWithSomeFunctionProducesSome() =>
        Assert.True(Opt.Some(1).Bind(Opt.Some).IsSome());

    [Fact]
    public static void BindingSomeWithNoneFunctionProducesNone() =>
        Assert.True(Opt.Some(1).Bind(_ => Opt.None<int>()).IsNone());

    [Property]
    public static bool BindingChangesTheValue(int i) =>
        Opt.Some(i).Bind(it => Opt.Some(it + 1)).Get() == i + 1;

    [Property]
    public bool GettingValueFromSomeReturnsValue(int i) =>
        Opt.Some(i).Map(it => (int?) it).DefaultValue(null) != null;

    [Fact]
    public void GettingValueFromNoneReturnsReplacement() =>
        Assert.Equal(1, Opt.None<int>().DefaultValue(1));

    [Fact]
    public void CountIsZeroForNone() =>
        Assert.Empty(Opt.None<int>());

    [Property]
    public bool CountIsOneForSome(int i) =>
        Opt.Some(i).Count() == 1;

    [Property]
    public bool DefaultWithReturnsTheValueOfTheOptionalIfPresent(int i) =>
        Opt.Some(i.ToString()).DefaultWith(() => "") == i.ToString();

    [Fact]
    public void DefaultWithReturnsTheResultOfTheReplacementFunctionIfTheOptionalIsMissing() =>
        Assert.Equal(1, Opt.None<int>().DefaultWith(() => 1));

    [Fact]
    public void FilteringNoneIsNone() =>
        Assert.True(Opt.None<int>().Filter(_ => true).IsNone());

    [Property]
    public bool FilteringSomeWithIncorrectPredicateIsNone(int i) =>
        Opt.Some(i).Filter(it => it != i).IsNone();

    [Property]
    public bool FilteringSomeWithCorrectPredicateIsSome(int i) =>
        Opt.Some(i).Filter(it => it == i).IsSome();

    [Fact]
    public void FlatteningNestedNoneIsNone() =>
        Assert.True(Opt.Some(Opt.None<int>()).Flatten().IsNone());

    [Fact]
    public void FlatteningNoneIsNone() =>
        Assert.True(Opt.None<IOpt<int>>().Flatten().IsNone());

    [Property]
    public bool FlatteningNestedSomeIsSome(int i) =>
        Opt.Some(Opt.Some(i)).Flatten().Equals(Opt.Some(i));

    [Property]
    public bool FoldingNoneReturnsTheInitialState(int initial) =>
        Opt.None<int>().Fold((s, it) => s + it, initial) == initial;

    [Property]
    public bool FoldingSomeReturnsTheResultOfTheFoldingFunction(int initial, int i) =>
        Opt.Some(i).Fold((s, it) => s + it, initial) == i + initial;

    [Property]
    public bool FoldingBackNoneReturnsTheInitialState(int initial) =>
        Opt.None<int>().FoldBack((it, s) => s + it, initial) == initial;

    [Property]
    public bool FoldingBackSomeReturnsTheResultOfTheFoldingFunction(int initial, int i) =>
        Opt.Some(i).FoldBack((it, s) => s + it, initial) == i + initial;

    [Fact]
    public void ForAllNoneIsTrue() =>
        Assert.True(Opt.None<int>().ForAll(it => it > 0));

    [Property]
    public bool ForAllSomeWithCorrectPredicateIsTrue(int i) =>
        Opt.Some(i).ForAll(it => it == i);

    [Property]
    public bool ForAllSomeWithIncorrectPredicateIsFalse(int i) =>
        !Opt.Some(i).ForAll(it => it != i);
}