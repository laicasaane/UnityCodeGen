using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Builder;
using NUnit.Framework;

namespace UnityCodeGen.Test.Builder
{
    [TestFixture]
    public class ClassBuilderTest
    {
        [Test]
        public void ItBuildsWithCorrectIsPartial()
        {
            var builder = new ClassBuilder();
            builder.IsPartial(true);

            var result = builder.Build<ClassNode>();

            Assert.AreEqual(true, result.IsPartial);
        }

        [Test]
        public void ItBuildsWithCorrectName()
        {
            var builder = new ClassBuilder();
            builder.WithName("FooBar");

            var result = builder.Build();

            Assert.AreEqual("FooBar", result.Name);
        }

        [Test]
        public void ItBuildsWithAProperty()
        {
            var builder = new ClassBuilder();
            builder.WithProperty();

            var result = builder.Build<ClassNode>();

            Assert.AreEqual(1, result.Members.Length);
        }

        [Test]
        public void ItBuildsWithAMethod()
        {
            var builder = new ClassBuilder();
            builder.WithMethod();

            var result = builder.Build<ClassNode>();

            Assert.AreEqual(1, result.Members.Length);
        }

        [Test]
        public void ItBuildsWithCorrectVisibility()
        {
            var builder = new ClassBuilder();
            builder.WithVisibility(AccessType.Public);

            var result = builder.Build();

            Assert.AreEqual(AccessType.Public, result.Visibility);
        }
    }
}
