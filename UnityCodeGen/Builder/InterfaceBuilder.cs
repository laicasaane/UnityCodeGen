using System.Collections.Generic;
using System.Linq;
using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Extensions;

namespace UnityCodeGen.Builder
{
    public class InterfaceBuilder : TypeGenericBuilder<InterfaceBuilder, InterfaceNode>
    {
        private readonly List<MemberBuilder> members = new List<MemberBuilder>();

        public InterfaceBuilder ClearMembers()
        {
            this.members.Clear();
            return this;
        }

        public InterfacePropertyBuilder WithProperty()
        {
            var propertyBuilder = new InterfacePropertyBuilder();
            this.members.Add(propertyBuilder);
            return propertyBuilder;
        }

        public InterfaceMethodBuilder WithMethod()
        {
            var methodBuilder = new InterfaceMethodBuilder();
            this.members.Add(methodBuilder);
            return methodBuilder;
        }

        protected override void Build(InterfaceNode node)
        {
            base.Build(node);

            node.Members = this.members.Map(m => m.Build()).ToArray();
        }
    }
}