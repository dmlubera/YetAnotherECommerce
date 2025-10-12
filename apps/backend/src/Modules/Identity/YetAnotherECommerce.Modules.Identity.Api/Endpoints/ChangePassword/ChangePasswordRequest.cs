namespace YetAnotherECommerce.Modules.Identity.Api.Endpoints.ChangePassword;

public record ChangePasswordRequest(string CurrentPassword, string NewPassword);