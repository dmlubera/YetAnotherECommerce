using MongoDB.Driver;
using System.Threading.Tasks;

namespace YetAnotherECommerce.Tests.Shared.Initializers
{
    public interface IMongoDbSeeder
    {
        public Task Seed(IMongoDatabase database, string collectioName);
    }
}