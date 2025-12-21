using System.Collections.Generic;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

public interface IMessageRegistry
{
    IEnumerable<MessageRegistration> GetRegistrations(string key);
}