namespace UnityCodeGen.AbstractSyntaxTree
{
    public class ClassNode : TypeStructClassNode
    {
        public bool IsPartial { get; set; }

        public bool IsStatic { get; set; }

        public ClassModifierType Modifier { get; set; }
    }
}