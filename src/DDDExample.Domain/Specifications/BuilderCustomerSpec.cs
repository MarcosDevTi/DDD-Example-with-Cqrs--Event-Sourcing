using System;
using System.Linq.Expressions;
using DDDExample.SharedKernel.Specification;

namespace DDDExample.Domain.Specifications
{
    public class SpecGeneric<T>
    {
        private Specification<T> Spec { get; set; }

        public SpecGeneric<T> AddSpec(Specification<T> spec)
        {
            Spec.And(spec);
            return this;
        }

        public SpecGeneric<T> OrSpec(Specification<T> spec)
        {
            Spec.And(spec);
            return this;
        }

        public Expression<Func<T, bool>> Build()
        {
            return Spec.ToExpression();
        }
    }

}
