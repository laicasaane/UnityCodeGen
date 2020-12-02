using System.Collections.Generic;
using System.Linq;
using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Extensions;

namespace UnityCodeGen.Builder
{
    public class AbstractSyntaxBuilder
    {
        private readonly List<UsingBuilder> usings = new List<UsingBuilder>();
        private readonly List<TypeBuilder> types = new List<TypeBuilder>();
        private readonly List<NamespaceBuilder> namespaces = new List<NamespaceBuilder>();

        public AbstractSyntaxBuilder ClearUsings()
        {
            this.usings.Clear();
            return this;
        }

        public AbstractSyntaxBuilder ClearTypes()
        {
            this.types.Clear();
            return this;
        }

        public AbstractSyntaxBuilder ClearNamespaces()
        {
            this.namespaces.Clear();
            return this;
        }

        public UsingBuilder WithUsing()
        {
            var usingBuilder = new UsingBuilder();
            this.usings.Add(usingBuilder);
            return usingBuilder;
        }

        public EnumBuilder WithEnum()
        {
            var enumBuilder = new EnumBuilder();
            this.types.Add(enumBuilder);
            return enumBuilder;
        }

        public InterfaceBuilder WithInterface()
        {
            var interfaceBuilder = new InterfaceBuilder();
            this.types.Add(interfaceBuilder);
            return interfaceBuilder;
        }

        public StructBuilder WithStruct()
        {
            var structBuilder = new StructBuilder();
            this.types.Add(structBuilder);
            return structBuilder;
        }

        public ClassBuilder WithClass()
        {
            var classBuilder = new ClassBuilder();
            this.types.Add(classBuilder);
            return classBuilder;
        }

        public NamespaceBuilder WithNamespace()
        {
            var namespaceBuilder = new NamespaceBuilder();
            this.namespaces.Add(namespaceBuilder);
            return namespaceBuilder;
        }

        public AbstractSyntaxNode Build()
        {
            return new AbstractSyntaxNode {
                Usings = this.usings.Map(u => u.Build()).ToArray(),
                Types = this.types.Map(t => t.Build()).ToArray(),
                Namespaces = this.namespaces.Map(n => n.Build()).ToArray(),
            };
        }
    }
}