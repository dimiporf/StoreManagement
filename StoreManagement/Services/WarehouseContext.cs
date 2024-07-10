using StoreManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace StoreManagement.Services
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventoryItemCategory> InventoryItemCategories { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }

        public WarehouseContext() : base("name=WarehouseContext")
        {
        }
    }
}
