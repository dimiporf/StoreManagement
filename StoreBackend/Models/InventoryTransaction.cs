using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StoreBackend.Models
{
    public class InventoryTransaction
    {
        [Key]
        public int TransactionID { get; set; }
        public int TransactionType { get; set; } // 1 for purchases, 2 for sales
        public DateTime TransactionDate { get; set; }
        public int InventoryItemID { get; set; }
        public int WarehouseID { get; set; }
        public int Qty { get; set; }
        public decimal? Cost { get; set; }
        public decimal? SalePrice { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
