using System.Collections.Generic;
using System.Linq;
using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Extensions;

namespace UnityCodeGen.Builder
{
    public abstract class TypeGenericBuilder<TBuilder, TNode> : TypeBuilder<TBuilder, TNode>
        where TBuilder : TypeGenericBuilder<TBuilder, TNode>
        where TNode : TypeGenericNode, new()
    {
        private readonly List<string> typeParameters = new List<string>();
        private readonly List<TypeConstraintBuilder> typeConstraints = new List<TypeConstraintBuilder>();

        public TBuilder ClearTypeParameters()
        {
            this.typeParameters.Clear();
            return this.Self;
        }

        public TBuilder ClearTypeConstraints()
        {
            this.typeConstraints.Clear();
            return this.Self;
        }

        public TBuilder WithTypeParameter(string name)
        {
            this.typeParameters.Add(name);
            return this.Self;
        }

        public TypeConstraintBuilder WithTypeConstraint()
        {
            var typeConstraintBuilder = new TypeConstraintBuilder();
            this.typeConstraints.Add(typeConstraintBuilder);
            return typeConstraintBuilder;
        }

        protected override void Build(TNode node)
        {
            base.Build(node);

            node.TypeParameters = this.typeParameters.ToArray();
            node.TypeConstraints = this.typeConstraints.Map(t => t.Build()).ToArray();
        }
    }
}