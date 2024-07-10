using System;

namespace StoreBackend.Models
{
    public class InventoryItem
    {
        // Primary key for the InventoryItem entity
        public int InventoryItemID { get; set; }

        // Foreign key referencing the InventoryItemCategory entity
        public int InventoryItemCategoryID { get; set; }

        // Description of the inventory item
        public string InventoryItemDescription { get; set; }

        // Navigation property to represent the relationship with InventoryItemCategory
        public virtual InventoryItemCategory InventoryItemCategory { get; set; }
    }
}

