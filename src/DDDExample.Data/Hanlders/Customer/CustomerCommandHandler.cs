using System;
using System.Linq;
using AutoMapper;
using DDDExample.Cqrs.Command.Customer;
using DDDExample.Cqrs.Event.Customer;
using DDDExample.Data.Context;
using DDDExample.Domain.Core.DomainNotifications;
using DDDExample.Domain.Event;
using DDDExample.SharedKernel.Cqrs.Command;

namespace DDDExample.Data.Hanlders.Customer
{
    public class CustomerCommandHandler : CommandHandler<Domain.Models.Customer>,
        ICommandHandler<CreateCustomer>,
        ICommandHandler<UpdateCustomer>,
        ICommandHandler<DeleteCustomer>
    {
        public CustomerCommandHandler(
            DddExampleContext architectureContext,
            IDomainNotification notifications,
            IEventRepository eventRepository) : base(architectureContext, notifications, eventRepository)
        {
        }

        public void Handle(CreateCustomer command)
        {
            ValidateCommand(command);

            var customer = Mapper.Map<Domain.Models.Customer>(command);


            if (Db().Any(x => x.Email == customer.Email))
            {
                AddNotification(new DomainNotification(
                    command.MessageType, "The customer e-mail has already been taken."));
                return;
            }

            Db().Add(customer);

            Commit(Mapper.Map<CustomerCreated>(command));
        }

        public void Handle(UpdateCustomer command)
        {
            //validate command
            //convert obj
            //exists

            ValidateCommand(command);

            var customer = Mapper.Map<Domain.Models.Customer>(command);
            var existingCustomer = Db().Any(x => x.Email == customer.Email);

            if (existingCustomer)
            {
                AddNotification(new DomainNotification(command.MessageType, "The customer e-mail has already been taken."));
            }

            Db().Update(customer);

            Commit(Mapper.Map<CustomerUpdated>(command));
        }

        public void Handle(DeleteCustomer command)
        {
            ValidateCommand(command);

            Db().Remove(GetById(command.Id.Value));
            Commit(new CustomerDeleted(command.Id.Value));
        }

        private Domain.Models.Customer GetById(Guid id) => Db().Find(id);
    }
}
