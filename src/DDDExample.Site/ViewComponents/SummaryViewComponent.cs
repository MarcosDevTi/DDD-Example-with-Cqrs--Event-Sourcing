using System.Threading.Tasks;
using DDDExample.Domain.Core.DomainNotifications;
using Microsoft.AspNetCore.Mvc;

namespace DDDExample.Site.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly IDomainNotification _notifications;

        public SummaryViewComponent(IDomainNotification notifications)
        {
            _notifications = notifications;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notifications.GetNotifications());
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Value));

            return View();
        }
    }
}
