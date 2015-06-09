namespace InventorySystem.Data.Entity
{
    public class PriceEntity : BaseEntity
    {
        public double? UsDollar { get; set; }
        public double? CanadianDollar { get; set; }
        public double? Paid { get; set; }
    }
}
