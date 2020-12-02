using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public class InterfacePropertyBuilder : InterfaceMemberBuilder<InterfacePropertyBuilder, PropertyNode>
    {
        private PropertyModeType mode;

        public InterfacePropertyBuilder WithMode(PropertyModeType mode)
        {
            this.mode = mode;
            return this;
        }

        protected override void Build(PropertyNode node)
        {
            base.Build(node);

            node.Mode = this.mode;
            node.SetVisibility = AccessType.Public;
        }
    }
}