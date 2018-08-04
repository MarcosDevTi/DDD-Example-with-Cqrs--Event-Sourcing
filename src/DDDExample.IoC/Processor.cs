using System;
using System.Collections.Generic;
using System.Text;
using DDDExample.SharedKernel.Cqrs;
using DDDExample.SharedKernel.Cqrs.Command;
using DDDExample.SharedKernel.Cqrs.Query;

namespace DDDExample.IoC
{
    public sealed class Processor : IProcessor
    {
        private readonly IServiceProvider _service;

        public Processor(IServiceProvider service) => _service = service;

        public TResult Process<TResult>(IQuery<TResult> query) =>
            Gethandle(typeof(IQueryHandler<,>), query.GetType(), typeof(TResult)).Handle((dynamic)query);

        public void Send<TCommand>(TCommand command) where TCommand : Command =>
            Gethandle(typeof(ICommandHandler<>), command.GetType()).Handle(command);

        private dynamic Gethandle(Type handle, params Type[] types) =>
            _service.GetService(handle.MakeGenericType(types));
    }
}
