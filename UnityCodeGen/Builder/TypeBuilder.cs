using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public abstract class TypeBuilder
    {
        public abstract TypeNode Build();

        public abstract T Build<T>() where T : TypeNode;
    }

    public abstract class TypeBuilder<TBuilder, TNode> : TypeBuilder
        where TBuilder : TypeBuilder<TBuilder, TNode>
        where TNode : TypeNode, new()
    {
        protected readonly TBuilder Self;

        protected string name;
        protected AccessType visibility;

        public TypeBuilder()
        {
            this.Self = this as TBuilder;
        }

        public TBuilder WithName(string name)
        {
            this.name = name;
            return this.Self;
        }

        public TBuilder WithVisibility(AccessType visibility)
        {
            this.visibility = visibility;
            return this.Self;
        }

        public sealed override TypeNode Build()
        {
            return BuildNode();
        }

        public sealed override T Build<T>()
        {
            return BuildNode() as T;
        }

        protected TNode BuildNode()
        {
            var node = new TNode();

            Build(node);

            return node;
        }

        protected virtual void Build(TNode node)
        {
            node.Name = this.name;
            node.Visibility = this.visibility;
        }
    }
}