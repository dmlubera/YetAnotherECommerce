using YetAnotherECommerce.Shared.Abstractions.Notifications;

namespace YetAnotherECommerce.Shared.Contracts.Notifications.Identity;

public record PasswordResetRequested(string Email, string PasswordResetLink) : INotification;