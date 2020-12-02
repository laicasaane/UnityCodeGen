using System.Collections.Generic;

namespace UnityCodeGen.AbstractSyntaxTree
{
    public class UsingCache
    {
        public Dictionary<string, UsingNode> Map { get; }

        public List<string> Namespaces { get; }

        public List<UsingNode> Usings { get; }

        public UsingCache()
        {
            this.Map = new Dictionary<string, UsingNode>();
            this.Namespaces = new List<string>();
            this.Usings = new List<UsingNode>();
        }

        public void Clear()
        {
            this.Map.Clear();
            this.Namespaces.Clear();
            this.Usings.Clear();
        }
        public bool IsUsingNamespace(string @namespace)
        {
            return this.Map.ContainsKey(@namespace);
        }

        public bool IsUsingNamespace(SimpleType type)
        {
            if (string.IsNullOrWhiteSpace(type?.Namespace))
                return false;

            return IsUsingNamespace(type.Namespace);
        }

        public bool IsUsingNamespace(UsingNode node)
        {
            if (string.IsNullOrWhiteSpace(node?.Namespace))
                return false;

            return IsUsingNamespace(node.Namespace);
        }
    }
}