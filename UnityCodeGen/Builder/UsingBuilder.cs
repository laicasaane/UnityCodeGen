using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public class UsingBuilder
    {
        private string @namespace;

        public UsingBuilder WithNamespace(string @namespace)
        {
            this.@namespace = @namespace;
            return this;
        }

        public UsingNode Build()
        {
            return new UsingNode {
                Namespace = @namespace,
            };
        }
    }
}