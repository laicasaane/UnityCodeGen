using System.Collections.Generic;
using System.Linq;
using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Extensions;

namespace UnityCodeGen.Builder
{
    public class InterfaceMethodBuilder : InterfaceMemberBuilder<InterfaceMethodBuilder, MethodNode>
    {
        private readonly List<ParameterBuilder> parameters = new List<ParameterBuilder>();
        private readonly List<string> typeParameters = new List<string>();
        private readonly List<TypeConstraintBuilder> typeConstraints = new List<TypeConstraintBuilder>();

        public InterfaceMethodBuilder ClearParameters()
        {
            this.parameters.Clear();
            return this;
        }

        public InterfaceMethodBuilder ClearTypeParameters()
        {
            this.typeParameters.Clear();
            return this;
        }

        public InterfaceMethodBuilder ClearTypeConstraints()
        {
            this.typeConstraints.Clear();
            return this;
        }

        public InterfaceMethodBuilder WithTypeParameter(string name)
        {
            this.typeParameters.Add(name);
            return this;
        }

        public TypeConstraintBuilder WithTypeConstraint()
        {
            var typeConstraintBuilder = new TypeConstraintBuilder();
            this.typeConstraints.Add(typeConstraintBuilder);
            return typeConstraintBuilder;
        }

        public ParameterBuilder WithParameter()
        {
            var builder = new ParameterBuilder();
            this.parameters.Add(builder);
            return builder;
        }

        protected override void Build(MethodNode node)
        {
            base.Build(node);

            node.Parameters = this.parameters.Map(p => p.Build()).ToArray();
            node.TypeParameters = this.typeParameters.ToArray();
            node.TypeConstraints = this.typeConstraints.Map(t => t.Build()).ToArray();
        }
    }
}