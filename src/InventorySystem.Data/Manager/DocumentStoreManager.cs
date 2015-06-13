using Raven.Client;
using Raven.Client.Document;
using System;

namespace InventorySystem.Data.Manager
{
    public interface IDocumentStoreManager
    {
        IDocumentStore Store { get; }
    }

    public class DocumentStoreManager : IDocumentStoreManager, IDisposable
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
