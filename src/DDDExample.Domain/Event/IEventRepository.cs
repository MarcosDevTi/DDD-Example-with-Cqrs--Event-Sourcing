using System.Collections.Generic;

namespace DDDExample.Domain.Event
{
    public interface IEventRepository
    {
        void Save(SharedKernel.Cqrs.Event.Event @event);
        IReadOnlyList<CustomerHistoryData> GetAllHistories();
    }
}
