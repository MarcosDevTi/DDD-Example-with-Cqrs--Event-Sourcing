using System;
using DDDExample.SharedKernel.Cqrs.Event;
using FluentValidation.Results;

namespace DDDExample.SharedKernel.Cqrs.Command
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }
        protected Command() => Timestamp = DateTime.Now;
        public abstract bool IsValid();
    }
}
