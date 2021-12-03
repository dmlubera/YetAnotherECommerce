using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    internal sealed class MessageRegistry : IMessageRegistry
    {
        private readonly List<MessageRegistration> registrations = new();

        public void AddRegistration(Type type, Func<IMessage, Task> action)
        {
            if (string.IsNullOrWhiteSpace(type.Namespace))
                throw new InvalidOperationException("Missing namespace");

            var registration = new MessageRegistration(type, action);
            registrations.Add(registration);
        }

        public IEnumerable<MessageRegistration> GetRegistrations(string key)
            => registrations.Where(x => x.Key == key);
    }
}