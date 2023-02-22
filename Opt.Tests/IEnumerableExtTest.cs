using System;
using FsCheck.Xunit;
using Xunit;

namespace ComradeVanti.CSharpTools;

// ReSharper disable once InconsistentNaming
public class IEnumerableExtTest
{
    [Property]
    public bool TrySingleIsSomeForSingletonCollections(int item)
    {
        var collection = new[] {item};
        var opt = collection.TrySingle();
        return opt.IsSome();
    }

    [Fact]
    public void TrySingleIsNoneForEmptyCollections()
    {
        var collection = Array.Empty<int>();
        var opt = collection.TrySingle();
        Assert.True(opt.IsNone());
    }

    [Fact]
    public void TrySingleIsNoneForCollectionsWithMoreThanOneItem()
    {
        var collection = new[] {1, 2, 3};
        var opt = collection.TrySingle();
        Assert.True(opt.IsNone());
    }

    [Fact]
    public void FilterSomeReturnsOnlyPresentValues()
    {
        var collections = new[] {Opt.Some(0), Opt.None<int>(), Opt.Some(1), Opt.None<int>(), Opt.None<int>(), Opt.Some(10)};
        var filtered = collections.FilterSome();
        Assert.Equal(filtered, new[] {0, 1, 10});
    }
}