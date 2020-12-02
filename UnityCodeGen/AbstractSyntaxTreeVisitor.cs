using System.Collections.Generic;
using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Extensions;

namespace UnityCodeGen
{
    /// <summary>
    /// Visitor pattern for the Abstract Syntax Tree (AST)
    /// On each node where Visit is called, then Walk whereby Walk invokes Visit on the node's children
    /// Override the Visit methods to process a given node and don't forget to call the base Method.
    /// </summary>
    public abstract class AbstractSyntaxTreeVisitor
    {
        protected virtual void VisitAstNode(AbstractSyntaxNode ast)
        {
            PrepareUsings(ast);
            VisitUsings(ast.UsingCache);
            WalkAstNode(ast);
        }

        protected virtual void PrepareUsings(AbstractSyntaxNode ast)
        {
            if (ast == null)
                return;

            ast.UsingCache.Clear();
            ast.Usings?.ForEach(u => PrepareUsings(ast.UsingCache, u));
            ast.Types?.ForEach(t => PrepareUsings(ast.UsingCache, t));
        }

        protected virtual void VisitUsings(UsingCache cache)
        {
            if (cache == null)
                return;

            cache.Namespaces.AddRange(cache.Map.Keys);
            cache.Namespaces.Sort();

            for (var i = 0; i < cache.Namespaces.Count; i++)
            {
                if (cache.Map.TryGetValue(cache.Namespaces[i], out var node))
                    cache.Usings.Add(node);
            }

            cache.Usings.ForEach(u => VisitUsingNode(u));
        }

        protected void WalkAstNode(AbstractSyntaxNode ast)
        {
            if (ast == null)
                return;

            ast.Types?.ForEach(t => VisitTypeNode(t));
            ast.Namespaces?.ForEach(n => VisitNamespaceNode(n));
        }

        private void PrepareUsings(UsingCache cache, UsingNode node)
        {
            if (node == null)
                return;

            if (cache.IsUsingNamespace(node))
                return;

            if (string.IsNullOrWhiteSpace(node.Namespace))
                return;

            cache.Map[node.Namespace] = node;
        }

        protected void PrepareUsings(NamespaceNode node)
        {
            if (node == null)
                return;

            node.UsingCache.Clear();
            node.Usings?.ForEach(u => PrepareUsings(node.UsingCache, u));
            node.Types?.ForEach(t => PrepareUsings(node.UsingCache, t));
        }

        private void PrepareUsings(UsingCache cache, TypeNode node)
        {
            if (node is TypeGenericNode g)
                g.TypeConstraints?.ForEach(c => PrepareUsings(cache, c));

            switch (node)
            {
                case InterfaceNode i:
                    i.Members?.ForEach(m => PrepareUsings(cache, m));
                    break;

                case TypeStructClassNode sc:
                    sc.Members?.ForEach(m => PrepareUsings(cache, m));
                    break;
            }
        }

        private void PrepareUsings(UsingCache cache, MemberNode node)
        {
            if (node == null)
                return;

            PrepareUsings(cache, node.ReturnType);

            if (node is MethodNode methodNode)
            {
                methodNode.TypeConstraints?.ForEach(t => PrepareUsings(cache, t));
                methodNode.Parameters?.ForEach(p => PrepareUsings(cache, p));
            }
        }

        private void PrepareUsings(UsingCache cache, TypeConstraintNode node)
        {
            if (node == null)
                return;

            node.Constraints?.ForEach(c => PrepareUsings(cache, c));
        }

        private void PrepareUsings(UsingCache cache, Constraint constraint)
        {
            if (constraint is TypeNameConstraint typeName)
                PrepareUsings(cache, typeName.Value);
        }

        private void PrepareUsings(UsingCache cache, ParameterNode node)
        {
            if (node == null)
                return;

            PrepareUsings(cache, node.Type);
        }

        private void PrepareUsings(UsingCache cache, SimpleType type)
        {
            if (type == null || type.IsExplicit)
                return;

            if (cache.IsUsingNamespace(type))
                return;

            if (string.IsNullOrWhiteSpace(type.Namespace))
                return;

            cache.Map[type.Namespace] = new UsingNode { Namespace = type.Namespace };
        }

        protected virtual void VisitUsingNode(UsingNode node)
        {
        }

        protected virtual void VisitNamespaceNode(NamespaceNode node)
        {
            PrepareUsings(node);
            VisitUsings(node.UsingCache);
            WalkNamespaceNode(node);
        }

        protected void WalkNamespaceNode(NamespaceNode node)
        {
            if (node == null)
                return;

            node.Types?.ForEach(t => VisitTypeNode(t));
            node.Namespaces?.ForEach(n => VisitNamespaceNode(n));
        }

        protected virtual void VisitTypeNode(TypeNode node)
        {
            WalkTypeNode(node);
        }

        protected void WalkTypeNode(TypeNode node)
        {
            switch (node)
            {
                case EnumNode e:
                    VisitEnumNode(e);
                    break;

                case TypeGenericNode g:
                    VisitTypeGenericNode(g);
                    break;
            }
        }

        protected virtual void VisitTypeGenericNode(TypeGenericNode node)
        {
            WalkTypeGenericNode(node);
        }

        protected void WalkTypeGenericNode(TypeGenericNode node)
        {
            if (node == null)
                return;

            switch (node)
            {
                case InterfaceNode i:
                    VisitInterfaceNode(i);
                    break;

                case TypeStructClassNode sc:
                    VisitTypeStructClassNode(sc);
                    break;
            }

            VisitGenericTypeParameters(node);
        }

        protected virtual void VisitGenericTypeParameters(TypeGenericNode node)
        {
            if (node == null)
                return;

            node.TypeParameters?.ForEach(t => VisitTypeParameter(t));
            node.TypeConstraints.ForEach(t => VisitTypeConstraint(t));
        }

        protected virtual void VisitInterfaceNode(InterfaceNode node)
        {
            WalkInterfaceNode(node);
        }

        protected void WalkInterfaceNode(InterfaceNode node)
        {
            if (node == null)
                return;

            var properties = new List<PropertyNode>();
            var methods = new List<MethodNode>();

            node.Members?.ForEach(m => PrepareMembers(m, properties, null, methods));

            properties.ForEach(p => VisitPropertyNode(p));
            methods.ForEach(m => VisitMethodNode(m));
        }

        protected virtual void VisitTypeStructClassNode(TypeStructClassNode node)
        {
            WalkTypeStructClassNode(node);
        }

        protected void WalkTypeStructClassNode(TypeStructClassNode node)
        {
            if (node == null)
                return;

            var properties = new List<PropertyNode>();
            var fields = new List<FieldNode>();
            var methods = new List<MethodNode>();

            node.Members?.ForEach(m => PrepareMembers(m, properties, fields, methods));

            properties.ForEach(p => VisitPropertyNode(p));
            fields.ForEach(f => VisitFieldNode(f));

            node.Constructors?.ForEach(c => VisitConstructorNode(node.Name, c));

            methods.ForEach(m => VisitMethodNode(m));
        }

        protected void PrepareMembers(MemberNode node, List<PropertyNode> properties,
                                      List<FieldNode> fields, List<MethodNode> methods)
        {
            switch (node)
            {
                case PropertyNode p: properties?.Add(p); break;
                case FieldNode f: fields?.Add(f); break;
                case MethodNode m: methods?.Add(m); break;
            }
        }

        protected virtual void VisitConstructorNode(string name, ConstructorNode node)
        {
            WalkConstructorNode(node);
        }

        protected void WalkConstructorNode(ConstructorNode node)
        {
            if (node == null)
                return;

            node.Parameters?.ForEach(p => VisitParameterNode(p));
            VisitMethodBody(node.Body);
        }

        protected virtual void VisitPropertyNode(PropertyNode node)
        {
        }

        protected virtual void VisitFieldNode(FieldNode node)
        {
        }

        protected virtual void VisitMethodNode(MethodNode node)
        {
            WalkMethodNode(node);
        }

        protected virtual void VisitTypeParameter(string name)
        {
        }

        protected virtual void VisitTypeConstraint(TypeConstraintNode node)
        {
        }

        protected void WalkMethodNode(MethodNode node)
        {
            if (node == null)
                return;

            node.TypeParameters?.ForEach(t => VisitTypeParameter(t));
            node.TypeConstraints.ForEach(t => VisitTypeConstraint(t));
            node.Parameters?.ForEach(p => VisitParameterNode(p));

            VisitMethodBody(node.Body);
        }

        protected virtual void VisitParameterNode(ParameterNode node)
        {
        }

        protected virtual void VisitMethodBody(MethodBodyNode node)
        {
        }

        protected virtual void VisitEnumNode(EnumNode node)
        {
            WalkEnumNode(node);
        }

        protected void WalkEnumNode(EnumNode node)
        {
            if (node == null)
                return;

            node.Options?.ForEach(o => VisitEnumOption(o));
        }

        protected virtual void VisitEnumOption(string option)
        {
        }

        protected class UsingNodeMap : Dictionary<string, UsingNode> { }
    }
}