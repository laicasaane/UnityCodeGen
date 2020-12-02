namespace UnityCodeGen.AbstractSyntaxTree
{
    public class TypeConstraintNode
    {
        public string Name { get; set; }

        public ConstraintBaseType Base { get; set; }

        public Constraint[] Constraints { get; set; }

        public bool HasDefaultConstructor { get; set; }
    }
}