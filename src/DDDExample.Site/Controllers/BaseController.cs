using DDDExample.Domain.Core.DomainNotifications;
using Microsoft.AspNetCore.Mvc;

namespace DDDExample.Site.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDomainNotification _notifications;

        public BaseController(IDomainNotification notifications)
        {
            _notifications = notifications;
        }

        public bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }
    }
}
