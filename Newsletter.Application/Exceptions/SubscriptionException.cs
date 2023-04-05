namespace Newsletter.Application.Exceptions;

[Serializable]
public class SubscriptionException : ApplicationException
{
    public string Details { get; }

    public SubscriptionException(string message) : base(message)
    {

    }

    public SubscriptionException(string message, Exception inner) : base(message, inner)
    {

    }

    protected SubscriptionException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    public SubscriptionException(string message, string details) : base(message)
    {
        Details = details;
    }
}
