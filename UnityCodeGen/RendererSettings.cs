using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen
{
    public struct RendererSettings
    {
        public LineEndingType LineEnding;
        public BlankLineSettings BlankLine;

        public static RendererSettings Default { get; } = new RendererSettings { LineEnding = LineEndingType.LF };
    }

    public struct BlankLineSettings
    {
        public bool Using;
        public bool Namespace;
        public bool Type;
        public bool Field;
        public bool Property;
        public bool Method;
        public bool Constraint;
    }
}