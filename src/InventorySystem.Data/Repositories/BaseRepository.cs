using InventorySystem.Data.Entity;
using InventorySystem.Data.Helper;
using InventorySystem.Data.Manager;
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

        public async Task CreateOrUpdateAsync<T>(Guid? id, Action<T> update, Func<T> create) where T : BaseEntity
        {
            if (id.HasValue)
            {
                //Update
                //If the id does not match any document, replace it by null to create a new document.
                id = await UpdateAsync(id.Value, update) ? id : null;
            }

            if (!id.HasValue)
            {
                //Create
                await CreateAsync(create());
            }
        }

        public async Task<T> LoadAsync<T>(Guid? id) where T : BaseEntity
        {
            if (id.HasValue)
            {
                using (var session = _database.Store.OpenAsyncSession())
                {
                    return await session.LoadAsync<T>(id);
                }
            }
            return null;
        }
        
        public async Task<bool> UpdateAsync<T>(Guid id, Action<T> update) where T : BaseEntity
        {
            using (var session = _database.Store.OpenAsyncSession())
            {
                var document = await session.LoadAsync<T>(id);

                if (document != null)
                {
                    update(document);
                    await session.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }
    }
}
