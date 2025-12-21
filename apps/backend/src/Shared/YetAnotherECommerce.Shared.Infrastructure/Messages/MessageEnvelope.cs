using System.Collections.Generic;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

public record MessageEnvelope(IDictionary<string, string> Metadata, IMessage Payload) : IMessageEnvelope;