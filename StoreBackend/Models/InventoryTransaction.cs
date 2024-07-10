using System;
using System.ComponentModel.DataAnnotations;

namespace StoreBackend.Models
{
    public class InventoryTransaction
    {
        [Key]
        public int TransactionID { get; set; }

        // Type of transaction: 1 for purchases, 2 for sales
        public int TransactionType { get; set; }

        // Date and time when the transaction occurred
        public DateTime TransactionDate { get; set; }

        // Foreign key referencing the InventoryItem entity
        public int InventoryItemID { get; set; }

        // Foreign key referencing the Warehouse entity
        public int WarehouseID { get; set; }

        // Quantity of items involved in the transaction
        public int Qty { get; set; }

        // Cost per item for purchase transactions
        public decimal? Cost { get; set; }

        // Sale price per item for sale transactions
        public decimal? SalePrice { get; set; }

        // Navigation property to represent the relationship with InventoryItem
        public virtual InventoryItem InventoryItem { get; set; }

        // Navigation property to represent the relationship with Warehouse
        public virtual Warehouse Warehouse { get; set; }

        // Properties for calculated totals
        public decimal? TotalCost
        {
            get
            {
                if (TransactionType == 1)
                    return Qty * Cost;
                else
                    return 0; // or handle differently if TransactionType is not 1
            }
        }

        public decimal? TotalSale
        {
            get
            {
                if (TransactionType == 2)
                    return Qty * SalePrice;
                else
                    return 0; // or handle differently if TransactionType is not 2
            }
        }
    }
}
