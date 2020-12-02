using NUnit.Framework;
using UnityCodeGen.Builder;

namespace UnityCodeGen.Test.Builder
{
    [TestFixture]
    public class AstBuilderTest
    {
        [Test]
        public void ItBuildsWithAUsing()
        {
            var builder = new AbstractSyntaxBuilder();
            builder.WithUsing();

            var result = builder.Build();

            Assert.AreEqual(1, result.Usings.Length);
        }

        [Test]
        public void ItBuildsWithAClass()
        {
            var builder = new AbstractSyntaxBuilder();
            builder.WithClass();

            var result = builder.Build();

            Assert.AreEqual(1, result.Types.Length);
        }

        [Test]
        public void ItBuildsWithANamespace()
        {
            var builder = new AbstractSyntaxBuilder();
            builder.WithNamespace();

            var result = builder.Build();

            Assert.AreEqual(1, result.Namespaces.Length);
        }
    }
}
