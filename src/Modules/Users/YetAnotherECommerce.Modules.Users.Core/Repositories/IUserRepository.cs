using System.Threading.Tasks;
using YetAnotherECommerce.Modules.Users.Core.Entities;

namespace YetAnotherECommerce.Modules.Users.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}