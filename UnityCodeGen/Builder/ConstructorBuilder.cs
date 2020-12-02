using System.Collections.Generic;
using System.Linq;
using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Extensions;

namespace UnityCodeGen.Builder
{
    public class ConstructorBuilder
    {
        private AccessType visibility;
        private readonly MethodBodyBuilder body = new MethodBodyBuilder();
        private readonly List<ParameterBuilder> parameters = new List<ParameterBuilder>();

        public ConstructorBuilder ClearParameters()
        {
            this.parameters.Clear();
            return this;
        }

        public ConstructorBuilder WithVisibility(AccessType visibility)
        {
            this.visibility = visibility;
            return this;
        }

        public ParameterBuilder WithParameter()
        {
            var builder = new ParameterBuilder();
            this.parameters.Add(builder);
            return builder;
        }

        public MethodBodyBuilder WithBody()
        {
            return this.body;
        }

        public ConstructorNode Build()
        {
            return new ConstructorNode {
                Visibility = visibility,
                Parameters = this.parameters.Map(p => p.Build()).ToArray(),
                Body = this.body.Build(),
            };
        }
    }
}