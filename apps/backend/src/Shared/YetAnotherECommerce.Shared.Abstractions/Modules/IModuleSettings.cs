namespace YetAnotherECommerce.Shared.Abstractions.Modules
{
    public interface IModuleSettings
    {
        string CollectionName { get; set; }
        string DatabaseName { get; set; }
    }
}