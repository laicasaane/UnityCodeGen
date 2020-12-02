namespace UnityCodeGen.AbstractSyntaxTree
{
    public class AbstractSyntaxNode
    {
        public UsingNode[] Usings { get; set; }

        public TypeNode[] Types { get; set; }

        public NamespaceNode[] Namespaces { get; set; }

        public UsingCache UsingCache { get; } = new UsingCache();
    }
}