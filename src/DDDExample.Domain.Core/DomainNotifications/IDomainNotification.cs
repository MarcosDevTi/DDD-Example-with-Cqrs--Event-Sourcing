using System.Collections.Generic;

namespace DDDExample.Domain.Core.DomainNotifications
{
    public interface IDomainNotification
    {
        void Add(DomainNotification domainNotification);
        List<DomainNotification> GetNotifications();
        bool HasNotifications();
        void Dispose();
    }
}
