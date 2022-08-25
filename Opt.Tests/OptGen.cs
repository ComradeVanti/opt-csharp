using System;
using FsCheck;

namespace ComradeVanti.CSharpTools;

public class OptGen
{

    private static Gen<IOpt<T>> GenNone<T>() =>
        Gen.Constant(Opt.None<T>());

    private static Gen<IOpt<T>> GenSome<T>() =>
        Arb.Generate<T>().Select(Opt.Some);

    private static Gen<IOpt<T>> GenOpt<T>() =>
        Gen.Frequency(Tuple.Create(1, GenNone<T>()),
                      Tuple.Create(2, GenSome<T>()));

    public static Arbitrary<IOpt<int>> OptInt() =>
        Arb.From(GenOpt<int>());

    public static Arbitrary<IOpt<TestRefType>> OptClass() =>
        Arb.From(GenOpt<TestRefType>());

}