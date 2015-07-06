using InventorySystem.Data.Entity;
using Raven.Client;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper;

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

        public async Task CreateOrUpdateAsync<T>(Guid? id, object model) where T : BaseEntity
        {
            if (id.HasValue)
            {
                //Update
                //If the id does not match any document, replace it by null to create a new document.
                id = await UpdateAsync<T>(id.Value, model) ? id : null;
            }

            if (!id.HasValue)
            {
                //Create
                await CreateAsync(Mapper.Map<T>(model));
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

        public async Task<IList<T>> ToListAsync<T>(Func<IRavenQueryable<T>> query)
        {
            return await Raven.Client.LinqExtensions.ToListAsync(query());
        }

        public async Task<bool> UpdateAsync<T>(Guid id, object model) where T : BaseEntity
        {
            var document = await _session.LoadAsync<T>(id);

            if (document != null)
            {
                Mapper.Map(model, document);
                await _session.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
