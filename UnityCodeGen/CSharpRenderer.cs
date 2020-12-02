using System.Text;
using UnityCodeGen.AbstractSyntaxTree;
using UnityCodeGen.Extensions;

namespace UnityCodeGen
{
    public class CSharpRenderer : AbstractSyntaxTreeVisitor
    {
        private readonly RendererSettings settings;
        private readonly StringBuilder result;
        private int indentLevel = 0;

        public CSharpRenderer(RendererSettings? settings = null)
        {
            this.settings = settings ?? RendererSettings.Default;
            this.result = new StringBuilder();
        }

        public string Render(AbstractSyntaxNode ast)
        {
            this.result.Clear();

            VisitAstNode(ast);

            return this.result.ToString();
        }

        protected override void VisitAstNode(AbstractSyntaxNode ast)
        {
            if (ast == null)
                return;

            PrepareUsings(ast);
            VisitUsings(ast.UsingCache);

            if (ast.UsingCache.Usings.Count > 0 ||
                !this.settings.BlankLine.Using)
                AppendLine();

            ast.Types?.ForEach(t => VisitTypeNode(t));
            ast.Namespaces?.ForEach(n => VisitNamespaceNode(n));
        }

        protected override void VisitUsingNode(UsingNode node)
        {
            if (node == null)
                return;

            AppendIndentation();
            Append("using ");
            Append(node.Namespace);
            AppendEndOfStatement();

            if (this.settings.BlankLine.Using)
                AppendLine();

            base.VisitUsingNode(node);
        }

        protected override void VisitNamespaceNode(NamespaceNode node)
        {
            if (node == null)
                return;

            AppendIndentation();
            Append("namespace ");
            Append(node.Name);
            AppendLine();
            AppendLine("{");
            ++this.indentLevel;

            PrepareUsings(node);
            VisitUsings(node.UsingCache);

            if (node.UsingCache.Usings.Count > 0 &&
                !this.settings.BlankLine.Using)
                AppendLine();

            node.Types?.ForEach(t => VisitTypeNode(t));

            if (!this.settings.BlankLine.Type)
                AppendLine();

            node.Namespaces?.ForEach(n => VisitNamespaceNode(n));

            --this.indentLevel;
            AppendLine("}");

            if (this.settings.BlankLine.Namespace)
                AppendLine();
        }

        protected override void VisitTypeStructClassNode(TypeStructClassNode node)
        {
            switch (node)
            {
                case StructNode s:
                    VisitStructNode(s);
                    break;

                case ClassNode c:
                    VisitClassNode(c);
                    break;
            }
        }

        protected override void VisitGenericTypeParameters(TypeGenericNode node)
        {
        }

        protected void AppendTypeParameters(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                return;

            Append("<");

            for (var i = 0; i < parameters.Length; i++)
            {
                if (i != 0)
                {
                    Append(", ");
                }
                Append(parameters[i]);
            }

            Append(">");
        }

        protected void AppendTypeConstraints(TypeConstraintNode[] nodes)
        {
            if (nodes == null || nodes.Length == 0)
                return;

            AppendLine();

            ++this.indentLevel;

            for (var i = 0; i < nodes.Length; i++)
            {
                VisitTypeConstraint(nodes[i]);
            }

            --this.indentLevel;
        }

        protected void AppendTypeGeneric(TypeGenericNode node)
        {
            if (node == null)
                return;

            AppendTypeParameters(node.TypeParameters);
            AppendTypeConstraints(node.TypeConstraints);
        }

        protected void VisitStructNode(StructNode node)
        {
            if (node == null)
                return;

            AppendIndentation();
            Append(node.Visibility);

            if (node.IsReadOnly)
                Append("readonly ");

            Append("struct ");
            Append(node.Name);

            AppendTypeGeneric(node);
            AppendLine();

            AppendLine("{");
            ++this.indentLevel;

            base.VisitTypeStructClassNode(node);

            --this.indentLevel;
            AppendLine("}");
            AppendLine();

            if (this.settings.BlankLine.Type)
                AppendLine();
        }

        protected void VisitClassNode(ClassNode node)
        {
            if (node == null)
                return;

            AppendIndentation();
            Append(node.Visibility);

            switch (node.Modifier)
            {
                case ClassModifierType.Abstract: Append("abstract "); break;
                case ClassModifierType.Sealed: Append("sealed "); break;
            }

            if (node.IsStatic)
                Append("static ");

            if (node.IsPartial)
                Append("partial ");

            Append("class ");
            Append(node.Name);

            AppendTypeGeneric(node);
            AppendLine();

            AppendLine("{");
            ++this.indentLevel;

            base.VisitTypeStructClassNode(node);

            --this.indentLevel;
            AppendLine("}");
            AppendLine();

            if (this.settings.BlankLine.Type)
                AppendLine();
        }

        protected override void VisitConstructorNode(string name, ConstructorNode node)
        {
            if (string.IsNullOrWhiteSpace(name) || node == null)
                return;

            AppendIndentation();
            Append(node.Visibility);

            Append(name);
            Append("(");

            if (node.Parameters != null)
            {
                var isFirst = true;
                foreach (var p in node.Parameters)
                {
                    if (!isFirst)
                        Append(", ");

                    VisitParameterNode(p);

                    isFirst = false;
                }
            }

            Append(")");
            AppendLine();

            AppendLine("{");
            ++this.indentLevel;

            VisitMethodBody(node.Body);

            --this.indentLevel;
            AppendLine("}");
            AppendLine();

            if (this.settings.BlankLine.Method)
                AppendLine();
        }

        protected override void VisitFieldNode(FieldNode node)
        {
            if (node == null)
                return;

            AppendIndentation();

            Append(node.Visibility);

            if (node.IsReadOnly)
                Append("readonly ");

            Append(node.ReturnType);
            Append(" ");
            Append(node.Name);
            AppendEndOfStatement();

            if (this.settings.BlankLine.Field)
                AppendLine();
        }

        protected override void VisitPropertyNode(PropertyNode node)
        {
            if (node == null)
                return;

            AppendIndentation();

            Append(node.Visibility);
            Append(node.ReturnType);
            Append(" ");
            Append(node.Name);
            Append(" { ");

            if (node.Mode == PropertyModeType.Default ||
                node.Mode == PropertyModeType.GetOnly)
            {
                Append("get; ");
            }

            if (node.Mode != PropertyModeType.GetOnly)
            {
                if (node.Mode == PropertyModeType.Default ||
                    node.Mode == PropertyModeType.SetOnly)
                {
                    if (node.Visibility != node.SetVisibility)
                        Append(node.SetVisibility);

                    Append("set; ");
                }
            }

            Append("}");
            AppendLine();

            if (this.settings.BlankLine.Property)
                AppendLine();
        }

        protected override void VisitMethodNode(MethodNode node)
        {
            if (node == null)
                return;

            AppendIndentation();
            Append(node.Visibility);

            if (node.IsStatic)
                Append("static ");

            if (node.IsAbstract)
                Append("abstract ");

            if (node.IsVirtual)
                Append("virtual ");

            if (node.IsPartial)
                Append("partial ");

            if (string.IsNullOrWhiteSpace(node.ReturnType?.Name))
                Append("void");
            else
                Append(node.ReturnType.Name);

            Append(" ");
            Append(node.Name);
            AppendTypeParameters(node.TypeParameters);
            Append("(");

            if (node.Parameters != null)
            {
                var isFirst = true;
                foreach (var p in node.Parameters)
                {
                    if (!isFirst)
                        Append(", ");

                    VisitParameterNode(p);

                    isFirst = false;
                }
            }

            Append(")");
            AppendTypeConstraints(node.TypeConstraints);

            if (node.IsPartial)
            {
                AppendEndOfStatement();
            }
            else
            {
                if (node.Body != null && node.Body.HasBody)
                {
                    AppendLine("{");
                    ++this.indentLevel;

                    VisitMethodBody(node.Body);

                    --this.indentLevel;
                    AppendLine("}");
                    AppendLine();
                }
                else
                {
                    AppendEndOfStatement();
                }
            }

            if (this.settings.BlankLine.Method)
                AppendLine();
        }

        protected override void VisitTypeConstraint(TypeConstraintNode node)
        {
            if (node == null)
                return;

            AppendIndentation();
            Append("where ");
            Append(node.Name);
            Append(" : ");

            switch (node.Base)
            {
                case ConstraintBaseType.Struct:
                    Append("struct");
                    break;

                case ConstraintBaseType.Class:
                    Append("class");
                    break;

                case ConstraintBaseType.NullableClass:
                    Append("class?");
                    break;

                case ConstraintBaseType.NotNull:
                    Append("notnull");
                    break;

                case ConstraintBaseType.Unmanaged:
                    Append("unmanaged");
                    break;

                case ConstraintBaseType.Enum:
                    Append("struct, System.Enum");
                    break;
            }

            if (node.Base != ConstraintBaseType.None)
            {
                if (node.Constraints.Length > 0)
                    Append(", ");
            }

            void TryAppendComma(int index)
            {
                if (index > 0)
                    Append(", ");
            }

            for (var i = 0; i < node.Constraints.Length; i++)
            {
                switch (node.Constraints[i])
                {
                    case TypeNameConstraint t:
                    {
                        TryAppendComma(i);
                        Append(t.Value);

                        break;
                    }

                    case StringConstraint s:
                    {
                        TryAppendComma(i);
                        Append(s.Value);
                        break;
                    }
                }
            }

            if (node.HasDefaultConstructor &&
                node.Base != ConstraintBaseType.Struct &&
                node.Base != ConstraintBaseType.Unmanaged)
            {
                Append(", new()");
            }

            AppendLine();

            if (this.settings.BlankLine.Constraint)
                AppendLine();
        }

        protected override void VisitParameterNode(ParameterNode node)
        {
            if (node == null)
                return;

            switch (node.Modifier)
            {
                case ParamModifierType.Ref: Append("ref "); break;
                case ParamModifierType.Out: Append("out "); break;
                case ParamModifierType.Params: Append("params "); break;
                case ParamModifierType.In: Append("in "); break;
            }

            Append(node.Type);

            if (node.Modifier == ParamModifierType.Params)
                Append("[]");

            Append(" ");
            Append(node.Name);

            if (node.HasDefault &&
                node.Modifier != ParamModifierType.Params &&
                node.Modifier != ParamModifierType.Out)
            {
                if (string.IsNullOrWhiteSpace(node.CustomDefault))
                    Append(" = default");
                else
                {
                    Append(" = ");
                    Append(node.CustomDefault);
                }
            }

            base.VisitParameterNode(node);
        }

        protected override void VisitMethodBody(MethodBodyNode node)
        {
            if (node == null)
                return;

            foreach (var line in node.Lines)
            {
                AppendLine(line);
            }
        }

        protected override void VisitEnumNode(EnumNode node)
        {
            if (node == null)
                return;

            AppendIndentation();
            Append(node.Visibility);

            Append("enum ");
            Append(node.Name);
            AppendLine();

            AppendLine("{");
            ++this.indentLevel;

            base.VisitEnumNode(node);

            --this.indentLevel;
            AppendLine("}");
            AppendLine();
        }

        protected override void VisitEnumOption(string option)
        {
            if (string.IsNullOrWhiteSpace(option))
                return;

            AppendIndentation();

            Append(option);
            Append(",");

            AppendLine();

            base.VisitEnumOption(option);
        }

        protected void Append(SimpleType value)
        {
            if (value == null)
                return;

            if (string.IsNullOrWhiteSpace(value.Name))
                return;

            if (!value.IsExplicit ||
                string.IsNullOrWhiteSpace(value.Namespace))
            {
                Append(value.Name);
                return;
            }

            Append(value.Namespace);
            Append(".");
            Append(value.Name);
        }

        protected void AppendIndentation()
        {
            for (var i = 0; i < this.indentLevel; i++)
                this.result.Append("    ");
        }

        protected void Append(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            this.result.Append(value);
        }

        protected void AppendLine()
        {
            this.result.Append(this.settings.LineEnding.Get());
        }

        protected void AppendLine(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                AppendIndentation();
                Append(value);
            }

            AppendLine();
        }

        protected void AppendEndOfStatement()
        {
            this.result.Append(";");
            AppendLine();
        }

        protected void Append(AccessType accessType, bool appendSpace = true)
        {
            switch (accessType)
            {
                case AccessType.Private:
                    this.result.Append("private");
                    break;

                case AccessType.Protected:
                    this.result.Append("protected");
                    break;

                case AccessType.Internal:
                    this.result.Append("internal");
                    break;

                case AccessType.Public:
                    this.result.Append("public");
                    break;
            }

            if (appendSpace)
                this.result.Append(" ");
        }
    }
}