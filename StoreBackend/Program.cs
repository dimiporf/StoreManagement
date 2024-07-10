using StoreBackend.Data;
using StoreBackend.Models;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        if (TestDatabaseConnection())
        {
            Console.WriteLine("Database connection successful.");
            SeedDatabase();
        }
        else
        {
            Console.WriteLine("Failed to connect to the database.");
        }
        Console.ReadLine(); // Keep the console window open
    }

    static bool TestDatabaseConnection()
    {
        try
        {
            using (var context = new WarehouseContext())
            {
                context.Database.Connection.Open();
                context.Database.Connection.Close();
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    static void SeedDatabase()
    {
        using (var context = new WarehouseContext())
        {
            // Seed data
            if (!context.Warehouses.Any())
            {
                context.Warehouses.Add(new Warehouse { WarehouseDescription = "Main Warehouse" });
                context.SaveChanges();
            }

            if (!context.InventoryItemCategories.Any())
            {
                context.InventoryItemCategories.Add(new InventoryItemCategory { InventoryItemCategoryDescription = "Electronics" });
                context.SaveChanges();
            }

            if (!context.InventoryItems.Any())
            {
                var category = context.InventoryItemCategories.First();
                context.InventoryItems.Add(new InventoryItem { InventoryItemCategoryID = category.InventoryItemCategoryID, InventoryItemDescription = "Laptop" });
                context.SaveChanges();
            }

            if (!context.InventoryTransactions.Any())
            {
                var warehouse = context.Warehouses.First();
                var item = context.InventoryItems.First();
                context.InventoryTransactions.Add(new InventoryTransaction
                {
                    TransactionType = 1,
                    TransactionDate = DateTime.Now,
                    InventoryItemID = item.InventoryItemID,
                    WarehouseID = warehouse.WarehouseID,
                    Qty = 10,
                    Cost = 1000m
                });
                context.SaveChanges();
            }
            Console.WriteLine("Database has been seeded with test data.");
        }
    }
}
