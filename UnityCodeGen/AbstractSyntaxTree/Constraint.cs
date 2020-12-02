namespace UnityCodeGen.AbstractSyntaxTree
{
    public abstract class Constraint
    {
    }

    public class TypeNameConstraint : Constraint
    {
        public SimpleType Value { get; set; }
    }

    public class StringConstraint : Constraint
    {
        public string Value { get; set; }
    }
}