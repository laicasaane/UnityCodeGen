using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public abstract class MemberBuilder
    {
        public abstract MemberNode Build();

        public abstract T Build<T>() where T : MemberNode;
    }

    public abstract class MemberBuilder<TBuilder, TNode> : MemberBuilder
        where TBuilder : MemberBuilder<TBuilder, TNode>
        where TNode : MemberNode, new()
    {
        protected readonly TBuilder Self;

        public string Name => this.name;

        private string name;
        private SimpleType returnType;
        private AccessType visibility;

        public MemberBuilder()
        {
            this.Self = this as TBuilder;
        }

        public TBuilder WithName(string name)
        {
            this.name = name;
            return this.Self;
        }

        public TBuilder WithReturnType(string type)
        {
            this.returnType = SimpleType.Create(type);
            return this.Self;
        }

        public TBuilder WithReturnType(string @namespace, string type, bool isExplicit = false)
        {
            this.returnType = SimpleType.Create(@namespace, type, isExplicit);
            return this.Self;
        }

        public TBuilder WithReturnType(SimpleType type)
        {
            this.returnType = type;
            return this.Self;
        }

        public TBuilder WithReturnType<T>()
        {
            this.returnType = typeof(T).ToSimple();
            return this.Self;
        }

        public TBuilder WithVisibility(AccessType visibility)
        {
            this.visibility = visibility;
            return this.Self;
        }

        public sealed override MemberNode Build()
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
            node.ReturnType = this.returnType;
        }
    }
}