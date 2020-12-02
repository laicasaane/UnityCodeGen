using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Builder;
using NUnit.Framework;

namespace UnityCodeGen.Test.Builder
{
    [TestFixture]
    public class MethodBuilderTest
    {
        [Test]
        public void TestIsAbstract()
        {
            var builder = new MethodBuilder();
            builder.IsAbstract(true);

            var result = builder.Build<MethodNode>();

            Assert.AreEqual(true, result.IsAbstract);
        }

        [Test]
        public void TestIsStatic()
        {
            var builder = new MethodBuilder();
            builder.IsStatic(true);

            var result = builder.Build<MethodNode>();

            Assert.AreEqual(true, result.IsStatic);
        }

        [Test]
        public void TestIsVirtual()
        {
            var builder = new MethodBuilder();
            builder.IsVirtual(true);

            var result = builder.Build<MethodNode>();

            Assert.AreEqual(true, result.IsVirtual);
        }

        [Test]
        public void TestWithName()
        {
            var builder = new MethodBuilder();
            builder.WithName("FooBar");

            var result = builder.Build();

            Assert.AreEqual("FooBar", result.Name);
        }

        [Test]
        public void TestWithReturnType()
        {
            var builder = new MethodBuilder();
            builder.WithReturnType("FooBar");

            var result = builder.Build();

            Assert.AreEqual("FooBar", result.ReturnType.Name);
        }

        [Test]
        public void TestWithVisibility()
        {
            var builder = new MethodBuilder();
            builder.WithVisibility(AccessType.Internal);

            var result = builder.Build();

            Assert.AreEqual(AccessType.Internal, result.Visibility);
        }

        [Test]
        public void TestWithParameter()
        {
            var builder = new MethodBuilder();
            builder.WithParameter();

            var result = builder.Build<MethodNode>();

            Assert.AreEqual(1, result.Parameters.Length);
        }
    }
}
