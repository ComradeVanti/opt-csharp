using FsCheck.Xunit;
using Xunit;

namespace ComradeVanti.CSharpTools
{

    public class InstantiationTests
    {

        [Property]
        public bool OptsCreatedWithSomeAreSome(int i) =>
            Opt.Some(i).IsSome();
        
        [Fact]
        public void OptsCreatedWithNoneAreNone() =>
            Assert.True(Opt.None<int>().IsNone());
        
        [Property]
        public bool OptsCreatedFromNonNullValueTypesAreSome(int i) =>
            Opt.FromNullable((int?)i).IsSome();

        [Property]
        public bool OptsCreatedFromNonNullReferenceTypesAreSome(TestRefType obj) =>
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