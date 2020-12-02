using System.Collections.Generic;
using UnityCodeGen.AbstractSyntaxTree;

namespace UnityCodeGen.Builder
{
    public class TypeConstraintBuilder
    {
        private string name;
        private ConstraintBaseType baseType;
        private bool hasDefaultConstructor;
        private readonly List<Constraint> constraints = new List<Constraint>();

        public TypeConstraintBuilder ClearConstraints()
        {
            this.constraints.Clear();
            return this;
        }

        public TypeConstraintBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public TypeConstraintBuilder WithBase(ConstraintBaseType @base)
        {
            this.baseType = @base;
            return this;
        }

        public TypeConstraintBuilder WithConstraint(string constraint)
        {
            this.constraints.Add(new StringConstraint { Value = constraint });
            return this;
        }

        public TypeConstraintBuilder WithConstraint(string @namespace, string type, bool isExplicit = false)
        {
            this.constraints.Add(new TypeNameConstraint {
                Value = SimpleType.Create(@namespace, type, isExplicit)
            });

            return this;
        }

        public TypeConstraintBuilder WithConstraint<T>(bool isExplicit = false)
        {
            this.constraints.Add(new TypeNameConstraint { Value = typeof(T).ToSimple(isExplicit) });
            return this;
        }

        public TypeConstraintBuilder HasDefaultConstructor(bool value)
        {
            this.hasDefaultConstructor = value;
            return this;
        }

        public TypeConstraintNode Build()
        {
            return new TypeConstraintNode {
                Name = name,
                Base = baseType,
                Constraints = this.constraints.ToArray(),
                HasDefaultConstructor = hasDefaultConstructor
            };
        }
    }
}