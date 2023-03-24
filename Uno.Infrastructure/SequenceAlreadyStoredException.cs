using System;

namespace Uno.Infrastructure;

public class SequenceAlreadyStoredException : Exception
{
    public SequenceAlreadyStoredException() : base()
    {
    }

    public SequenceAlreadyStoredException(string message) : base(message)
    {
    }

    public SequenceAlreadyStoredException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
