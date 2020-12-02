using System.Collections.Generic;
using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public class EnumBuilder : TypeBuilder<EnumBuilder, EnumNode>
    {
        private readonly List<string> options = new List<string>();

        public EnumBuilder ClearOptions()
        {
            this.options.Clear();
            return this;
        }

        public EnumBuilder WithOption(string optionName)
        {
            this.options.Add(optionName);
            return this;
        }

        protected override void Build(EnumNode node)
        {
            base.Build(node);

            node.Options = this.options.ToArray();
        }
    }
}