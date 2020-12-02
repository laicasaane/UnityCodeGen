namespace UnityCodeGen.AbstractSyntaxTree
{
    public class SimpleType
    {
        public string Namespace { get; set; }

        public string Name { get; set; }

        public bool IsExplicit { get; set; }

        public static SimpleType Create(string name)
        {
            return Create(string.Empty, name);
        }

        public static SimpleType Create(string @namespace, string name, bool isExplicit = false)
        {
            return new SimpleType {
                Namespace = @namespace,
                Name = name,
                IsExplicit = isExplicit
            };
        }
    }
}