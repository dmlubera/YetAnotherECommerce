using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Messages;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages;

public class MessageRegistration(Type type, Func<IMessage, Task> action)
{
    public Type Type { get; set; } = type;
    public Func<IMessage, Task> Action { get; set; } = action;
    public string Key => Type.Name;
}