using System;

namespace DDDExample.Cqrs.Event.Product
{
    public class ProductDeleted : SharedKernel.Cqrs.Event.Event
    {
        public ProductDeleted(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; private set; }
    }
}
