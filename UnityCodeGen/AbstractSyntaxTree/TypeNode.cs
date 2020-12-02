namespace UnityCodeGen.AbstractSyntaxTree
{
    public abstract class TypeNode
    {
        public string Name { get; set; }

        public AccessType Visibility { get; set; }
    }
}