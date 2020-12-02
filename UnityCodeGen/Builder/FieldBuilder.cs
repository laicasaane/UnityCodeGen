using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public class FieldBuilder : MemberBuilder<FieldBuilder, FieldNode>
    {
        private bool isReadOnly;

        public FieldBuilder IsReadOnly(bool value)
        {
            this.isReadOnly = value;
            return this;
        }

        protected override void Build(FieldNode node)
        {
            base.Build(node);

            node.IsReadOnly = this.isReadOnly;
        }
    }
}