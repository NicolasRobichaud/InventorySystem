using InventorySystem.Data.Entity;
using InventorySystem.Data.Manager;
using Raven.Client;
using System.Threading.Tasks;

namespace InventorySystem.Data.Repositories
{
    public class BaseRepository
    {
        private readonly DocumentStoreManager _store;

        public BaseRepository(DocumentStoreManager store)
        {
            _store = store;
        }

        public async Task AddNewItemAsync<T>(T entity) where T : BaseEntity
        {
            using (var session = _store.Store.OpenAsyncSession())
            {
                await session.StoreAsync(entity);
                await session.SaveChangesAsync();
            }
        }
    }
}
