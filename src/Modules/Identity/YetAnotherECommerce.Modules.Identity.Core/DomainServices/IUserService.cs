using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Entities;

namespace YetAnotherECommerce.Modules.Identity.Core.DomainServices
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string email, string password, string role);
        Task ChangePasswordAsync(Guid userId, string password);
    }
}