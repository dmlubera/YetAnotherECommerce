using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YetAnotherECommerce.Shared.Abstractions.Mongo;
using YetAnotherECommerce.Tests.Shared.Helpers;
using YetAnotherECommerce.Tests.Shared.Initializers;

namespace YetAnotherECommerce.Tests.Shared
{
    public class MongoDbFixture<TMongoSettings, TEntity> : IDisposable where TMongoSettings : IMongoSettings, new() where TEntity : IDocument
    {
        private readonly IMongoClient _client;
        private readonly IMongoCollection<TEntity> _collection;
        private readonly string _databaseName;
        private readonly IMongoDatabase _database;
        protected bool _disposed = false;

        public MongoDbFixture(string collectionName)
        {
            var options = OptionsHelper.GetOptions<TMongoSettings>();
            _client = new MongoClient(options.ConnectionString);
            _databaseName = options.DatabaseName;
            _database = _client.GetDatabase(_databaseName);
            _collection = _database.GetCollection<TEntity>(collectionName);
        }

        public void InitializeAsync(IMongoDbSeeder seeder)
            => seeder.Seed(_database).GetAwaiter().GetResult();

        public async Task<TEntity> GetAsync(Guid id)
            => await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
            => await _collection.Find(filter).FirstOrDefaultAsync();

        public async Task InsertAsync(TEntity entity)
            => await _collection.InsertOneAsync(entity);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _client.DropDatabase(_databaseName);

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
