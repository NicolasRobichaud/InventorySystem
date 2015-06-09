using Raven.Client;
using Raven.Client.Document;
using System;

namespace InventorySystem.Data.Manager
{
    public class DocumentStoreManager : IDisposable
    {
        private Lazy<IDocumentStore> store;

        public IDocumentStore Store
        {
            get { return store.Value; }
        }

        public DocumentStoreManager()
        {
            store = new Lazy<IDocumentStore>(CreateStore);
        }
        
        public void Dispose()
        {
            Store.Dispose();
        }


        private IDocumentStore CreateStore()
        {
            return new DocumentStore()
            {
                Url = "http://raven.local.com",
                DefaultDatabase = "InventorySystem"
            }.Initialize();
        }
    }
}
