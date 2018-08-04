using System;
using DDDExample.SharedKernel.Cqrs.Command;

namespace DDDExample.SharedKernel.Cqrs.Event
{
    public class Message : ICommand
    {
        public Message()
        {
            MessageType = GetType().Name;

        }

        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
    }
}
