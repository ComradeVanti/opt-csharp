using System;
using FsCheck;

namespace ComradeVanti.CSharpTools
{

    public class OptGen
    {

        private static Gen<Opt<T>> GenNone<T>() =>
            Gen.Constant(Opt.None<T>());

        private static Gen<Opt<T>> GenSome<T>() =>
            Arb.Generate<T>().Select(Opt.Some);

        private static Gen<Opt<T>> GenOpt<T>() =>
            Gen.Frequency(Tuple.Create(1, GenNone<T>()),
                          Tuple.Create(2, GenSome<T>()));

        public static Arbitrary<Opt<int>> OptInt() =>
            Arb.From(GenOpt<int>());

        public static Arbitrary<Opt<TestRefType>> OptClass() =>
            Arb.From(GenOpt<TestRefType>());

    }

}