using Microsoft.AspNet.Builder;

namespace InventorySystem.Web.App_Start
{
    public static class RouteConfig
    {
        public static void ConfigureRoutes(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "Building - Add",
                //    template: "building/add",
                //    defaults: new
                //    {
                //        controller = "Building",
                //        action = "AddBuildingFormAsync"
                //    });
                
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Building}/{action=AddBuildingFormAsync}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }
    }
}
