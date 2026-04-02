using YetAnotherECommerce.Shared.Abstractions.Notifications;

namespace YetAnotherECommerce.Shared.Contracts.Notifications.Identity;

public record UserRegistered(string Email) : INotification;