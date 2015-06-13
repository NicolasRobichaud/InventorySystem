using InventorySystem.Data.Manager;
using InventorySystem.Data.Repositories;
using Microsoft.Framework.DependencyInjection;

namespace InventorySystem.Web.App_Start
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddSingleton<IDocumentStoreManager, DocumentStoreManager>();
            services.AddScoped<BaseRepository>();
        }
    }
}
