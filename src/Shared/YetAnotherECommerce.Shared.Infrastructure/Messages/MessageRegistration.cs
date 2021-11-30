using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class MessageRegistration
    {
        public Type Type { get; set; }
        public Func<IMessage, Task> Action { get; set; }
        public string Key => Type.Name;

        public MessageRegistration(Type type, Func<IMessage, Task> action)
        {
            Type = type;
            Action = action;
        }
    }
}