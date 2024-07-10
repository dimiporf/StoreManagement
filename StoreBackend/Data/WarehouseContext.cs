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
        // DbSet properties representing database tables
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<InventoryItemCategory> InventoryItemCategories { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }

        // Constructor to initialize DbContext with connection string name
        public WarehouseContext() : base("name=WarehouseContext")
        {
            // The "name=WarehouseContext" corresponds to the connection string name in the configuration file
        }

        // Optional: Customize model configuration in OnModelCreating method
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base method to keep default behavior

            // Additional configuration can be added here using modelBuilder
            // This method is typically used to configure entity mappings and relationships
        }
    }
}
