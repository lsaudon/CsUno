using System;
using System.Collections.Generic;
using Uno.Core.CommandSide.Events;

namespace Uno.Core.CommandSide;

public abstract class DecisionProjectionBase
{
    private readonly Dictionary<Type, Action<IDomainEvent>> _handlersByType = new();

    public void Apply(IDomainEvent evt)
    {
        if (evt is not null && _handlersByType.TryGetValue(evt.GetType(), out Action<IDomainEvent>? apply))
        {
            apply(evt);
        }
    }

    protected void AddHandler<T>(Action<T> apply)
            where T : notnull, IDomainEvent
    {
        _handlersByType.Add(typeof(T), o => apply((T)o));
    }
}
