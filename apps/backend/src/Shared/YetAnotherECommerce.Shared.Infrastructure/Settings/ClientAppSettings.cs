namespace YetAnotherECommerce.Shared.Infrastructure.Settings;

public class ClientAppSettings
{
    public string BaseUrl {get; set; }
    public Paths Paths {get; set; }
}

public class Paths
{
    public string EmailConfirmation { get; set; }
}