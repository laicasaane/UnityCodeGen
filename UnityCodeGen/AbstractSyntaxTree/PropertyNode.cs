namespace UnityCodeGen.AbstractSyntaxTree
{
    public class PropertyNode : MemberNode
    {
        public bool IsAbstract { get; set; }

        public PropertyModeType Mode { get; set; }

        public AccessType SetVisibility { get; set; }
    }
}