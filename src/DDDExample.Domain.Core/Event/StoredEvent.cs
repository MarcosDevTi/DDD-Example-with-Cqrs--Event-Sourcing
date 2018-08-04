using System;
using Newtonsoft.Json;

namespace DDDExample.Domain.Core.Event
{
    public class StoredEvent : SharedKernel.Cqrs.Event.Event
    {
        protected StoredEvent()
        {

        }
        public StoredEvent(SharedKernel.Cqrs.Event.Event theEvent, string user)
        {
            var jsonEvent = JsonConvert.SerializeObject(theEvent);
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = jsonEvent;
            User = user;
        }

        // EF Constructor
        protected StoredEvent(SharedKernel.Cqrs.Event.Event @event) { }

        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}
