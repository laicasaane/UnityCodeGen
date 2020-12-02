using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public class PropertyBuilder : MemberBuilder<PropertyBuilder, PropertyNode>
    {
        private bool isAbstract;
        private PropertyModeType mode;
        private AccessType setVisibility;

        public PropertyBuilder IsAbstract(bool value)
        {
            this.isAbstract = value;
            return this;
        }

        public PropertyBuilder WithMode(PropertyModeType mode)
        {
            this.mode = mode;
            return this;
        }

        public PropertyBuilder WithSetVisibility(AccessType setVisibility)
        {
            this.setVisibility = setVisibility;
            return this;
        }

        protected override void Build(PropertyNode node)
        {
            base.Build(node);

            node.IsAbstract = this.isAbstract;
            node.Mode = this.mode;
            node.SetVisibility = this.setVisibility;
        }
    }
}