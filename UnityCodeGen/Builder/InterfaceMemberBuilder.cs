using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public abstract class InterfaceMemberBuilder<TBuilder, TNode> : MemberBuilder<TBuilder, TNode>
        where TBuilder : InterfaceMemberBuilder<TBuilder, TNode>
        where TNode : MemberNode, new()
    {
        protected override void Build(TNode node)
        {
            base.Build(node);

            node.Visibility = AccessType.Public;
        }
    }
}