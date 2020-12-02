using System.Collections.Generic;
using System.Linq;
using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Extensions;

namespace UnityCodeGen.Builder
{
    public abstract class TypeStructClassBuilder<TBuilder, TNode> : TypeGenericBuilder<TBuilder, TNode>
        where TBuilder : TypeStructClassBuilder<TBuilder, TNode>
        where TNode : TypeStructClassNode, new()
    {
        private readonly List<ConstructorBuilder> constructors = new List<ConstructorBuilder>();
        private readonly List<MemberBuilder> members = new List<MemberBuilder>();

        public TBuilder ClearConstructors()
        {
            this.constructors.Clear();
            return this.Self;
        }

        public TBuilder ClearMembers()
        {
            this.members.Clear();
            return this.Self;
        }

        public ConstructorBuilder WithConstructor()
        {
            var constructorBuilder = new ConstructorBuilder();
            this.constructors.Add(constructorBuilder);
            return constructorBuilder;
        }

        public PropertyBuilder WithProperty()
        {
            var propertyBuilder = new PropertyBuilder();
            this.members.Add(propertyBuilder);
            return propertyBuilder;
        }

        public FieldBuilder WithField()
        {
            var fieldBuilder = new FieldBuilder();
            this.members.Add(fieldBuilder);
            return fieldBuilder;
        }

        public MethodBuilder WithMethod()
        {
            var methodBuilder = new MethodBuilder();
            this.members.Add(methodBuilder);
            return methodBuilder;
        }

        protected override void Build(TNode node)
        {
            base.Build(node);

            node.Constructors = this.constructors.Map(c => c.Build()).ToArray();
            node.Members = this.members.Map(m => m.Build()).ToArray();
        }
    }
}