using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public class ClassBuilder : TypeStructClassBuilder<ClassBuilder, ClassNode>
    {
        private bool isPartial;
        private bool isStatic;
        private ClassModifierType modifier;

        public ClassBuilder IsPartial(bool value)
        {
            this.isPartial = value;
            return this;
        }

        public ClassBuilder IsStatic(bool value)
        {
            this.isStatic = value;
            return this;
        }

        public ClassBuilder WithModifier(ClassModifierType modifier)
        {
            this.modifier = modifier;
            return this;
        }

        protected override void Build(ClassNode node)
        {
            base.Build(node);

            node.IsPartial = isPartial;
            node.IsStatic = isStatic;
            node.Modifier = modifier;
        }
    }
}