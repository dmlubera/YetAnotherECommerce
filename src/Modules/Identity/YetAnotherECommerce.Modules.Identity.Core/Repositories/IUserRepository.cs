﻿using System;
using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Identity.Core.Entities;

namespace YetAnotherECommerce.Modules.Identity.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(Guid id);
        Task UpdateAsync(User user);
        Task<bool> CheckIfEmailIsInUseAsync(string email);
    }
}