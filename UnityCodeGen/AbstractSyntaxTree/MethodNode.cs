namespace UnityCodeGen.AbstractSyntaxTree
{
    public class MethodNode : MemberNode
    {
        public bool IsStatic { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsVirtual { get; set; }

        public bool IsPartial { get; set; }

        public MethodBodyNode Body { get; set; }

        public ParameterNode[] Parameters { get; set; }

        public string[] TypeParameters { get; set; }

        public TypeConstraintNode[] TypeConstraints { get; set; }
    }
}