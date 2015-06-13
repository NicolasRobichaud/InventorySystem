using InventorySystem.Data.Entity;
using InventorySystem.Data.Manager;
using Raven.Client;
using System;
using System.Threading.Tasks;

namespace InventorySystem.Data.Repositories
{
    public class BaseRepository
    {
        private readonly IDocumentStoreManager _database;

        public BaseRepository(IDocumentStoreManager database)
        {
            _database = database;
        }

        public async Task CreateAsync<T>(T entity) where T : BaseEntity
        {
            using (var session = _database.Store.OpenAsyncSession())
            {
                await session.StoreAsync(entity);
                await session.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateAsync<T>(Guid id, Action<T> update) where T : BaseEntity
        {
            using (var session = _database.Store.OpenAsyncSession())
            {
                var company = await session.LoadAsync<T>(id);

                if(company != null)
                {
                    update(company);
                    await session.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }
    }
}
