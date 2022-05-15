using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Entities;

namespace YetAnotherECommerce.Modules.Users.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}