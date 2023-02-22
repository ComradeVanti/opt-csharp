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
}