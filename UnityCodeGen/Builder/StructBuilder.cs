using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public class StructBuilder : TypeStructClassBuilder<StructBuilder, StructNode>
    {
        private bool isReadOnly;

        public StructBuilder IsReadOnly(bool value)
        {
            this.isReadOnly = value;
            return this;
        }

        protected override void Build(StructNode node)
        {
            base.Build(node);

            node.IsReadOnly = this.isReadOnly;
        }
    }
}