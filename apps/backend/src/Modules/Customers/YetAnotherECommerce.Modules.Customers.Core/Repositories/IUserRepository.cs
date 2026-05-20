using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Customers.Core.Entities;

namespace YetAnotherECommerce.Modules.Customers.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}