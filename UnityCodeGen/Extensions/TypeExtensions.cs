using System;

namespace UnityCodeGen.AbstractSyntaxTree
{
    public static class TypeExtensions
    {
        public static SimpleType ToSimple(this Type self, bool isExplicit = false)
        {
            return SimpleType.Create(self.Namespace, self.Name, isExplicit);
        }
    }
}