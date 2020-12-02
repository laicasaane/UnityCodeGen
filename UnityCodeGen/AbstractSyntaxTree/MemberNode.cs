namespace UnityCodeGen.AbstractSyntaxTree
{
    public abstract class MemberNode
    {
        public string Name { get; set; }

        public SimpleType ReturnType { get; set; }

        public AccessType Visibility { get; set; }
    }
}