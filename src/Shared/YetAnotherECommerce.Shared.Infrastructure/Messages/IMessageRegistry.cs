using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public interface IMessageRegistry
    {
        IEnumerable<MessageRegistration> GetRegistrations(string key);
        void AddRegistration(Type type, Func<IMessage, Task> action);
    }
}