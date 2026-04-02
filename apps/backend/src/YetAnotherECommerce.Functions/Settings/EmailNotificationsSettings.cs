namespace YetAnotherECommerce.Functions.Settings;

public record EmailNotificationsSettings
{
    public required string NoReplyEmailAddress { get; init; }
}