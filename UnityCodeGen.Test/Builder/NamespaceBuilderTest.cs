using UnityCodeGen.Builder;
using NUnit.Framework;

namespace UnityCodeGen.Test.Builder
{
    [TestFixture]
    public class NamespaceBuilderTest
    {
        [Test]
        public void ItBuildsWithCorrectName()
        {
            var builder = new NamespaceBuilder();
            builder.WithName("FooBar");

            var result = builder.Build();

            Assert.AreEqual("FooBar", result.Name);
        }

        [Test]
        public void ItBuildsWithAClass()
        {
            var builder = new NamespaceBuilder();
            builder.WithClass();

            var result = builder.Build();

            Assert.AreEqual(1, result.Types.Length);
        }
    }
}
