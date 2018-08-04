using DDDExample.SharedKernel.Cqrs.Query;

namespace DDDExample.SharedKernel.Cqrs
{
    public interface IProcessor
    {
        TResult Process<TResult>(IQuery<TResult> query);
        void Send<TCommand>(TCommand command) where TCommand : Command.Command;
    }
}
