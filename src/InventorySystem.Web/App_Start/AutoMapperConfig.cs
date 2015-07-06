using AutoMapper;
using InventorySystem.Data.Entity;
using InventorySystem.Web.ViewModel.Building;
using InventorySystem.Web.ViewModel.MapperProfiles;

namespace InventorySystem.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void InitializeAutoMapper()
        {
            Mapper.AddProfile<EditFormProfile>();
        }
    }
}
