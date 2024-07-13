using System;
using System.ComponentModel.DataAnnotations;

namespace StoreBackend.Models
{
    public class InventoryStock
    {
        [Key]
        public int InventoryStockID { get; set; }

        // Foreign key referencing the InventoryItem entity
        public int InventoryItemID { get; set; }

        // Foreign key referencing the Warehouse entity
        public int WarehouseID { get; set; }

        // Quantity of items in stock
        public int Quantity { get; set; }

        // Moving average cost per item
        public decimal MovingAverageCost { get; set; }

        // Navigation property to represent the relationship with InventoryItem
        public virtual InventoryItem InventoryItem { get; set; }

        // Navigation property to represent the relationship with Warehouse
        public virtual Warehouse Warehouse { get; set; }
    }
}
