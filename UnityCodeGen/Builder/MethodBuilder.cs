using System.Collections.Generic;
using System.Linq;
using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Extensions;

namespace UnityCodeGen.Builder
{
    public class MethodBuilder : MemberBuilder<MethodBuilder, MethodNode>
    {
        private bool isStatic;
        private bool isAbstract;
        private bool isVirtual;
        private bool isPartial;
        private readonly MethodBodyBuilder body = new MethodBodyBuilder();
        private readonly List<ParameterBuilder> parameters = new List<ParameterBuilder>();
        private readonly List<string> typeParameters = new List<string>();
        private readonly List<TypeConstraintBuilder> typeConstraints = new List<TypeConstraintBuilder>();

        public MethodBuilder ClearParameters()
        {
            this.parameters.Clear();
            return this;
        }

        public MethodBuilder ClearTypeParameters()
        {
            this.typeParameters.Clear();
            return this;
        }

        public MethodBuilder ClearTypeConstraints()
        {
            this.typeConstraints.Clear();
            return this;
        }

        public MethodBuilder WithTypeParameter(string name)
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

        public MethodBuilder IsStatic(bool value)
        {
            this.isStatic = value;
            return this;
        }

        public MethodBuilder IsAbstract(bool value)
        {
            this.isAbstract = value;
            return this;
        }

        public MethodBuilder IsVirtual(bool value)
        {
            this.isVirtual = value;
            return this;
        }

        public MethodBuilder IsPartial(bool value)
        {
            this.isPartial = value;
            return this;
        }

        public ParameterBuilder WithParameter()
        {
            var builder = new ParameterBuilder();
            this.parameters.Add(builder);
            return builder;
        }

        public MethodBodyBuilder WithBody()
        {
            return this.body;
        }

        protected override void Build(MethodNode node)
        {
            base.Build(node);

            node.IsAbstract = isAbstract;
            node.IsStatic = isStatic;
            node.IsVirtual = isVirtual;
            node.IsPartial = isPartial;
            node.Parameters = this.parameters.Map(p => p.Build()).ToArray();
            node.TypeParameters = this.typeParameters.ToArray();
            node.TypeConstraints = this.typeConstraints.Map(t => t.Build()).ToArray();
            node.Body = this.body.Build();
        }
    }
}