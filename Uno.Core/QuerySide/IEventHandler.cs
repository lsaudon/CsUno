using Uno.Core.CommandSide.Events;

namespace Uno.Core.QuerySide;

public interface IEventHandler
{
    void Handle(IDomainEvent evt);
}
