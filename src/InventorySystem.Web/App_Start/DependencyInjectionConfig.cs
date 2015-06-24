using InventorySystem.Data.Manager;
using InventorySystem.Data.Repositories;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System.Reflection;

namespace InventorySystem.Web.App_Start
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigureDependencyInjection(IServiceCollection services, Container container)
        {
            services.AddInstance<IControllerActivator>(new SimpleInjectorControllerActivator(container));

            var database = new DocumentStoreManager();
            container.Register(() => database.Store.OpenAsyncSession());

            container.Register<BaseRepository>();
        }

        public static void RegisterControllers(IApplicationBuilder app, Container container)
        {
            // Register ASP.NET controllers
            var provider = app.ApplicationServices.GetRequiredService<IControllerTypeProvider>();
            foreach (TypeInfo type in provider.ControllerTypes)
            {
                var registration = Lifestyle.Transient.CreateRegistration(type, container);
                container.AddRegistration(type, registration);
                registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "ASP.NET disposes controllers.");
            }
        }
    }
}
