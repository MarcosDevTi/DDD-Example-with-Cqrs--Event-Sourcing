using System;

namespace DDDExample.Cqrs.Event.Customer
{
    public class CustomerDeleted : SharedKernel.Cqrs.Event.Event
    {
        public CustomerDeleted(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
