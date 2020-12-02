namespace UnityCodeGen.AbstractSyntaxTree
{
    public abstract class TypeGenericNode : TypeNode
    {
        public string[] TypeParameters { get; set; }

        public TypeConstraintNode[] TypeConstraints { get; set; }
    }
}