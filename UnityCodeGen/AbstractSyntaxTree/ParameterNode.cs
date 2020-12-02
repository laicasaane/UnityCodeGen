namespace UnityCodeGen.AbstractSyntaxTree
{
    public class ParameterNode
    {
        public SimpleType Type { get; set; }

        public string Name { get; set; }

        public ParamModifierType Modifier { get; set; }

        public bool HasDefault { get; set; }

        public string CustomDefault { get; set; }
    }
}