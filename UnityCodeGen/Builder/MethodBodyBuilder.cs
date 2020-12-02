using System.Collections.Generic;
using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public class MethodBodyBuilder
    {
        private bool hasBody;
        private readonly List<string> lines = new List<string>();

        public MethodBodyBuilder ClearLines()
        {
            this.lines.Clear();
            return this;
        }

        public MethodBodyBuilder WithLine(string format, params object[] args)
        {
            this.lines.Add(string.Format(format, args));
            this.hasBody = true;
            return this;
        }

        public MethodBodyBuilder HasBody(bool value)
        {
            this.hasBody = value;

            if (!value)
                this.lines.Clear();

            return this;
        }

        public MethodBodyNode Build()
        {
            return new MethodBodyNode {
                HasBody = hasBody,
                Lines = this.lines.ToArray(),
            };
        }
    }
}