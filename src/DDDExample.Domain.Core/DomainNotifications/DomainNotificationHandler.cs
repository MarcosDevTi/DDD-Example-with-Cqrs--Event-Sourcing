using System.Collections.Generic;
using System.Linq;

namespace DDDExample.Domain.Core.DomainNotifications
{
    public class DomainNotificationHandler : IDomainNotification
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Add(DomainNotification domainNotification)
        {
            _notifications.Add(domainNotification);
        }

        public List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }
    }
}