using InventorySystem.Data.Entity;
using Raven.Client;
using Raven.Client.Linq;
using System;
using System.Threading.Tasks;

namespace InventorySystem.Data.Repositories
{
    public class BaseRepository
    {
        private readonly IAsyncDocumentSession _session;

        public BaseRepository(IAsyncDocumentSession session)
        {
            _session = session;
        }

        public async Task CreateAsync<T>(T entity) where T : BaseEntity
        {
            await _session.StoreAsync(entity);
            await _session.SaveChangesAsync();
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
                return await _session.LoadAsync<T>(id);
            }
            return null;
        }

        public IRavenQueryable<T> Query<T>()
        {
            return _session.Query<T>();
        }

        public async Task<bool> UpdateAsync<T>(Guid id, Action<T> update) where T : BaseEntity
        {
            var document = await _session.LoadAsync<T>(id);

            if (document != null)
            {
                update(document);
                await _session.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
