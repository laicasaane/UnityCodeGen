using UnityCodeGen.Builder;
using NUnit.Framework;

namespace UnityCodeGen.Test.Builder
{
    [TestFixture]
    public class UsingBuilderTest
    {
        [Test]
        public void ItBuildsWithCorrectNamespaceName()
        {
            var builder = new UsingBuilder();
            builder.WithNamespace("FooBar");

            var result = builder.Build();

            Assert.AreEqual("FooBar", result.Namespace);
        }
    }
}
