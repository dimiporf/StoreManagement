using StoreBackend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBackend.Data
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
