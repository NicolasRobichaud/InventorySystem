using System;
using System.Collections.Generic;

namespace InventorySystem.Data.Entity
{
    public class BaseInventoryEntity : BaseEntity
    {
        public string Name { get; set; }
        public SeriesEntity Series { get; set; }
        public string Description { get; set; }
        public PriceEntity Price { get; set; }
        public DateTime? Introduced { get; set; }
        public DateTime? Retired { get; set; }
        public string ImageUrl { get; set; }
        public List<ConnectionEntity> Connections { get; set; } = new List<ConnectionEntity>();
    }
}
