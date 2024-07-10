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
        // Test the database connection before proceeding
        if (TestDatabaseConnection())
        {
            Console.WriteLine("Database connection successful.");
            SeedDatabase(); // Seed initial data if not already present
        }
        else
        {
            Console.WriteLine("Failed to connect to the database.");
        }

        Console.ReadLine(); // Keep the console window open
    }

    // Method to test the database connection
    static bool TestDatabaseConnection()
    {
        try
        {
            using (var context = new WarehouseContext())
            {
                // Open the database connection explicitly
                context.Database.Connection.Open();
                // Close the connection immediately after opening to test connectivity
                context.Database.Connection.Close();
            }
            return true; // Connection successful
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false; // Connection failed
        }
    }

    // Method to seed initial data into the database
    static void SeedDatabase()
    {
        using (var context = new WarehouseContext())
        {
            // Seed warehouses if none exist
            if (!context.Warehouses.Any())
            {
                context.Warehouses.Add(new Warehouse { WarehouseDescription = "Main Warehouse" });
                context.SaveChanges();
            }

            // Seed inventory item categories if none exist
            if (!context.InventoryItemCategories.Any())
            {
                context.InventoryItemCategories.Add(new InventoryItemCategory { InventoryItemCategoryDescription = "Electronics" });
                context.SaveChanges();
            }

            // Seed inventory items if none exist
            if (!context.InventoryItems.Any())
            {
                var category = context.InventoryItemCategories.First();
                context.InventoryItems.Add(new InventoryItem { InventoryItemCategoryID = category.InventoryItemCategoryID, InventoryItemDescription = "Laptop" });
                context.SaveChanges();
            }

            // Seed inventory transactions if none exist
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
