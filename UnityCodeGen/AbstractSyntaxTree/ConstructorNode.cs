namespace UnityCodeGen.AbstractSyntaxTree
{
    public class ConstructorNode
    {
        public AccessType Visibility { get; set; }

        public MethodBodyNode Body { get; set; }

        public ParameterNode[] Parameters { get; set; }
    }
}