using InventorySystem.Data.Manager;
using Raven.Client.Indexes;

namespace InventorySystem.Web.App_Start
{
    public static class RavenDbConfig
    {
        public static void InitializeIndexes()
        {
            using (var store = new DocumentStoreManager())
            {
                IndexCreation.CreateIndexes(typeof(DocumentStoreManager).Assembly, store.Store);
            }
        }
    }
}
