using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBackend.Models
{
    public class InventoryItem
    {
        public int InventoryItemID { get; set; }
        public int InventoryItemCategoryID { get; set; }
        public string InventoryItemDescription { get; set; }
        public virtual InventoryItemCategory InventoryItemCategory { get; set; }
    }
}
