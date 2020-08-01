using System;
using Xunit;
using Codenation.Challenge.Models;

namespace Codenation.Challenge
{
    public sealed class CompanyModelTest : ModelBaseTest
    {
        public CompanyModelTest()
            : base(new CodenationContext())
        {
            Model = "Codenation.Challenge.Models.Company";
            Table = "company";
        }

        [Fact]
        public void Should_Has_Table()
        {
            AssertTable();
        }

        [Fact]
        public void Should_Has_Primary_Key()
        {
            AssertPrimaryKeys("id");
        }

        [Theory]
        [InlineData("id", false, typeof(int), null)]
        [InlineData("name", false, typeof(string), 100)]
        [InlineData("slug", false, typeof(string), 50)]
        [InlineData("created_at", false, typeof(DateTime), null)]
        public void Should_Has_Field(string fieldName, bool isNullable, Type fieldType, int? fieldSize)
        {
            AssertField(fieldName, isNullable, fieldType, fieldSize);
        }

        [Theory]
        [InlineData("candidate")]
        public void Should_Has_Children_Navigation(string childrenTable)
        {
            AssertChildrenNavigation(childrenTable);
        }

    }
}