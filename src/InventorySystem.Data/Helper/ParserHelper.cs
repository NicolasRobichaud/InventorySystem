using System;

namespace InventorySystem.Data.Helper
{
    public static class ParserHelper
    {
        public static Guid? ToGuid(string value)
        {
            Guid id;
            if(Guid.TryParse(value, out id))
            {
                return id;
            }
            return null;
        }
    }
}
