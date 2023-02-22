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

    [Fact]
    public void TryMaxIsNoneForEmptyCollections()
    {
        var collection = Array.Empty<int>();
        var opt = collection.TryMax();
        Assert.True(opt.IsNone());
    }

    [Fact]
    public void TryMaxFindsTheMaximumElement()
    {
        var collection = new[] {1, 2, 3};
        var opt = collection.TryMax();
        Assert.True(opt.Contains(3));
    }

    [Fact]
    public void TryMinIsNoneForEmptyCollections()
    {
        var collection = Array.Empty<int>();
        var opt = collection.TryMin();
        Assert.True(opt.IsNone());
    }

    [Fact]
    public void TryMinFindsTheMinimumElement()
    {
        var collection = new[] {1, 2, 3};
        var opt = collection.TryMin();
        Assert.True(opt.Contains(1));
    }

    [Fact]
    public void TryMaxByIsNoneForEmptyCollections()
    {
        var collection = Array.Empty<string>();
        var opt = collection.TryMaxBy(it => it.Length);
        Assert.True(opt.IsNone());
    }

    [Fact]
    public void TryMaxByFindsTheMaximumElement()
    {
        var collection = new[] {"a", "bb", "ccc"};
        var opt = collection.TryMaxBy(it => it.Length);
        Assert.True(opt.Contains("ccc"));
    }
    
    [Fact]
    public void TryMinByIsNoneForEmptyCollections()
    {
        var collection = Array.Empty<string>();
        var opt = collection.TryMinBy(it => it.Length);
        Assert.True(opt.IsNone());
    }

    [Fact]
    public void TryMinByFindsTheMinimumElement()
    {
        var collection = new[] {"a", "bb", "ccc"};
        var opt = collection.TryMinBy(it => it.Length);
        Assert.True(opt.Contains("a"));
    }
}