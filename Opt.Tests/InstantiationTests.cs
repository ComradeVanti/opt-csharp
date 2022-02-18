using FsCheck.Xunit;
using Xunit;

namespace ComradeVanti.CSharpTools
{

    public class InstantiationTests
    {

        [Property]
        public bool OptsCreatedWithSomeAreAlwaysSome(int i) =>
            Opt.Some(i).IsSome();
        [Fact]
        public void OptsCreatedWithNoneAreAlwaysNone() =>
            Assert.True(Opt.None<int>().IsNone());
        [Property]
        public bool OptsCreatedFromNonNullValueTypesAreAlwaysSome(int i) =>
            Opt.FromNullable((int?)i).IsSome();

        [Property]
        public bool OptsCreatedFromNonNullReferenceTypesAreAlwaysSome(TestRefType obj) =>
            Opt.FromNullable(obj).IsSome();

        [Fact]
        public void OptsCreatedFromNullValueTypeAreNone() =>
            Assert.True(Opt.FromNullable((int?)null).IsNone());

        [Fact]
        public void OptsCreatedFromNullReferenceTypeAreNone()
        {
#pragma warning disable CS8600
            Assert.True(Opt.FromNullable((TestRefType)null).IsNone());
#pragma warning restore CS8600
        }


        public class TestRefType { }

    }

}