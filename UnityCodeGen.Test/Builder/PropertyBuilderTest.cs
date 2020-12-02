using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Builder;
using NUnit.Framework;

namespace UnityCodeGen.Test.Builder
{
    [TestFixture]
    public class PropertyBuilderTest
    {
        [Test]
        public void ItBuildsWithCorrectName()
        {
            var builder = new PropertyBuilder();
            builder.WithName("FooBar");

            var result = builder.Build();

            Assert.AreEqual("FooBar", result.Name);
        }

        [Test]
        public void ItBuildsWithCorrectGetAccess()
        {
            var builder = new PropertyBuilder();
            builder.WithVisibility(AccessType.Public);

            var result = builder.Build();

            Assert.AreEqual(AccessType.Public, result.Visibility);
        }

        [Test]
        public void ItBuildsWithCorrectSetAccess()
        {
            var builder = new PropertyBuilder();
            builder.WithSetVisibility(AccessType.Public);

            var result = builder.Build<PropertyNode>();

            Assert.AreEqual(AccessType.Public, result.SetVisibility);
        }

        [Test]
        public void ItBuildsWithCorrectType()
        {
            var builder = new PropertyBuilder();
            builder.WithReturnType("FooBar");

            var result = builder.Build();

            Assert.AreEqual("FooBar", result.ReturnType.Name);
        }
    }
}
