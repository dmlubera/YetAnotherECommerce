namespace YetAnotherECommerce.Modules.Identity.Api.Models.Requests;

public record ChangePasswordRequest(string CurrentPassword, string NewPassword);
