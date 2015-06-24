using System;

namespace InventorySystem.Data.Entity
{
    public class SeriesEntity : BaseEntity
    {
        public string Name { get; set; }
        public Guid BrandId { get; set; }
    }
}
