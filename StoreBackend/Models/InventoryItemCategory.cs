using System;

namespace StoreBackend.Models
{
    public class InventoryItemCategory
    {
        // Primary key for the InventoryItemCategory entity
        public int InventoryItemCategoryID { get; set; }

        // Description of the inventory item category
        public string InventoryItemCategoryDescription { get; set; }
    }
}
