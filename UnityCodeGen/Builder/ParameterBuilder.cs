using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public class ParameterBuilder
    {
        public string Name => this.name;

        private SimpleType type;
        private string name;
        private ParamModifierType modifier;
        private bool hasDefault;
        private string customDefault;

        public ParameterBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public ParameterBuilder WithModifier(ParamModifierType modifier)
        {
            this.modifier = modifier;
            return this;
        }

        public ParameterBuilder WithType(string type)
        {
            this.type = SimpleType.Create(type);
            return this;
        }

        public ParameterBuilder WithType(string @namespace, string type, bool isExplicit = false)
        {
            this.type = SimpleType.Create(@namespace, type, isExplicit);
            return this;
        }

        public ParameterBuilder WithType(SimpleType type)
        {
            this.type = type;
            return this;
        }

        public ParameterBuilder WithType<T>()
        {
            this.type = typeof(T).ToSimple();
            return this;
        }

        public ParameterBuilder WithDefault(string customDefault = null)
        {
            this.hasDefault = true;
            this.customDefault = customDefault;
            return this;
        }

        public ParameterNode Build()
        {
            return new ParameterNode {
                Name = name,
                Type = type,
                Modifier = modifier,
                HasDefault = hasDefault,
                CustomDefault = this.customDefault ?? string.Empty
            };
        }
    }
}