using System.Collections.Generic;
using System.Linq;
using DDDExample.Data.Context;
using DDDExample.Domain.Core;
using DDDExample.Domain.Core.Event;
using DDDExample.Domain.Event;
using DDDExample.SharedKernel.Cqrs.Event;

namespace DDDExample.Data.EventSourcing
{
    public class EventRespoitory : IEventRepository
    {
        private readonly EventSourcingContext _eventSourcingContext;
        private readonly IUser _user;

        public EventRespoitory(EventSourcingContext eventSourcingContext, IUser user)
        {
            _eventSourcingContext = eventSourcingContext;
            _user = user;
        }

        public void Save(Event @event)
        {
            _eventSourcingContext.StoredEvent.Add(new StoredEvent(@event, _user.Name));
            _eventSourcingContext.SaveChanges();
        }

        public IReadOnlyList<CustomerHistoryData> GetAllHistories()
        {
            return CustomerHistoryHelper.ToJavaScriptCustomerHistory(_eventSourcingContext.StoredEvent.ToList());
        }
    }
}
