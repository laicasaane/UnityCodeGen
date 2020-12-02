namespace UnityCodeGen.AbstractSyntaxTree
{
    public abstract class TypeStructClassNode : TypeGenericNode
    {
        public ConstructorNode[] Constructors { get; set; }

        public MemberNode[] Members { get; set; }
    }
}