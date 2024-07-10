using StoreBackend.Models;
using StoreBackend.Repositories;
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

        // Repository injection for each entity
        public IRepository<Warehouse> WarehouseRepository { get; set; }
        public IRepository<InventoryItemCategory> InventoryItemCategoryRepository { get; set; }
        public IRepository<InventoryItem> InventoryItemRepository { get; set; }
        public IRepository<InventoryTransaction> InventoryTransactionRepository { get; set; }

        // Constructor to initialize DbContext with connection string name
        public WarehouseContext() : base("name=WarehouseContext")
        {
            WarehouseRepository = new Repository<Warehouse>(this);
            InventoryItemCategoryRepository = new Repository<InventoryItemCategory>(this);
            InventoryItemRepository = new Repository<InventoryItem>(this);
            InventoryTransactionRepository = new Repository<InventoryTransaction>(this);
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
