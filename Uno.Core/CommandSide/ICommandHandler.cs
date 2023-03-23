using Uno.Core.CommandSide.Commands;

namespace Uno.Core.CommandSide
{
    public interface ICommandHandler<in TCommand> : IDomainCommand
            where TCommand : notnull, IDomainCommand
    {
        void Handle(TCommand command);
    }
}
