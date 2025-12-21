using System.Collections.Generic;

namespace YetAnotherECommerce.Shared.Abstractions.Messages;

public interface IMessageEnvelope
{
    IDictionary<string, string> Metadata { get; }
    IMessage Payload { get; }
}