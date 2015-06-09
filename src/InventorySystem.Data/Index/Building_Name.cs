using System.Linq;
using InventorySystem.Data.Entity;
using Raven.Client.Indexes;

namespace InventorySystem.Data.Index
{
    public class Building_Name : AbstractIndexCreationTask<BuildingEntity>
    {
        public class Result
        {
            public string Name { get; set; }
        }

        public Building_Name()
        {
            Map = buildings => from building in buildings
                               select new
                               {
                                   building.Name
                               };
        }
    }
}
