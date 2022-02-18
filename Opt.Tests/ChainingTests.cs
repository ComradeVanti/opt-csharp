using FsCheck.Xunit;
using Xunit;

namespace ComradeVanti.CSharpTools
{

    public class ChainingTests
    {

        [Property]
        public static bool IterActionIsOnlyCalledForSome(Opt<int> opt)
        {
            var executed = false;

            opt.Iter(_ => executed = true);

            return executed == opt.IsSome();
        }

        [Property]
        public static bool MappingFunctionIsOnlyCalledForSome(Opt<int> opt)
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

    }

}