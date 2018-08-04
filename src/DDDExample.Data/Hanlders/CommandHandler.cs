using DDDExample.Data.Context;
using DDDExample.Domain.Core;
using DDDExample.Domain.Core.DomainNotifications;
using DDDExample.Domain.Event;
using DDDExample.SharedKernel.Cqrs.Command;
using DDDExample.SharedKernel.Cqrs.Event;
using Microsoft.EntityFrameworkCore;

namespace DDDExample.Data.Hanlders
{
    public abstract class CommandHandler<T> where T : Entity
    {
        private readonly DddExampleContext _architectureContext;
        private readonly IDomainNotification _notifications;
        private readonly IEventRepository _eventRepository;

        protected CommandHandler(DddExampleContext architectureContext, IDomainNotification notifications, IEventRepository eventRepository)
        {
            _architectureContext = architectureContext;
            _notifications = notifications;
            _eventRepository = eventRepository;
        }

        protected DbSet<T> Db() => _architectureContext.Set<T>();

        protected void ValidateCommand(Command cmd)
        {
            if (cmd.IsValid()) return;
            foreach (var error in cmd.ValidationResult.Errors)
                AddNotification(new DomainNotification(cmd.MessageType, error.ErrorMessage));
        }

        protected void Commit(Event evet)
        {
            if (_notifications.HasNotifications()) return;
            if (_architectureContext.SaveChanges() > 0)

                _eventRepository.Save(evet);
            else
            AddNotification(new DomainNotification("Commit", "We had a problem during saving your data."));
        }

        protected void AddNotification(DomainNotification notification)
        {
            _notifications.Add(notification);
        }
    }
}
