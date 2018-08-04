using System;
using System.Linq;
using AutoMapper;
using DDDExample.Cqrs.Command.Product;
using DDDExample.Cqrs.Event.Product;
using DDDExample.Data.Context;
using DDDExample.Domain.Core.DomainNotifications;
using DDDExample.Domain.Event;
using DDDExample.SharedKernel.Cqrs.Command;

namespace DDDExample.Data.Hanlders.Product
{
    public class ProductCommandHandler : CommandHandler<Domain.Models.Product>,
        ICommandHandler<CreateProduct>,
        ICommandHandler<UpdateProduct>,
        ICommandHandler<DeleteProduct>
    {
        public ProductCommandHandler(
            DddExampleContext architectureContext,
            IDomainNotification notifications,
            IEventRepository eventRepository) : base(architectureContext, notifications, eventRepository)
        {
        }


        public void Handle(CreateProduct command)
        {
            ValidateCommand(command);

            var customer = Mapper.Map<Domain.Models.Product>(command);


            if (Db().Any(x => x.Name == customer.Name))
            {
                AddNotification(new DomainNotification(
                    command.MessageType, "The Product Name has already been taken."));
                return;
            }

            Db().Add(customer);

            Commit(Mapper.Map<ProductCreated>(command));
        }

        public void Handle(UpdateProduct command)
        {
            ValidateCommand(command);

            var product = Mapper.Map<Domain.Models.Product>(command);

            var getProducts = Db().Where(x => x.Name == command.Name);

            //check already been taken name from other product and send notification
            Db().Update(product);
            Commit(Mapper.Map<ProductUpdated>(command));
        }

        public void Handle(DeleteProduct command)
        {
            ValidateCommand(command);
            Db().Remove(GetById(command.Id));
            Commit(new ProductDeleted(command.Id));
        }

        private Domain.Models.Product GetById(Guid id) => Db().Find(id);
    }
}
