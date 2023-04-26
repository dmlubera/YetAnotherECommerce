using System;

namespace YetAnotherECommerce.Shared.Infrastructure.Messages
{
    public class ServiceBusSubscriptionAttribute : Attribute
    {
        public string Name { get; }

        public ServiceBusSubscriptionAttribute(string name)
        {
            Name = name;
        }
    }
}