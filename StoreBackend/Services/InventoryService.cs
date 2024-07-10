using StoreBackend.Models;
using StoreBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBackend.Services
{
    // Service class for inventory-related operations
    public class InventoryService
    {
        private readonly IRepository<InventoryTransaction> _transactionRepository;

        // Constructor to initialize the service with a repository for inventory transactions
        public InventoryService(IRepository<InventoryTransaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // Calculates and returns the current stock level for a given inventory item in a warehouse
        public int GetStockLevel(int inventoryItemId, int warehouseId)
        {
            // Calculate total purchases of the inventory item in the warehouse
            var purchases = _transactionRepository
                .GetAll()
                .Where(t => t.InventoryItemID == inventoryItemId && t.WarehouseID == warehouseId && t.TransactionType == 1)
                .Sum(t => t.Qty);

            // Calculate total sales of the inventory item in the warehouse
            var sales = _transactionRepository
                .GetAll()
                .Where(t => t.InventoryItemID == inventoryItemId && t.WarehouseID == warehouseId && t.TransactionType == 2)
                .Sum(t => t.Qty);

            // Calculate current stock level (purchases - sales)
            return purchases - sales;
        }
    }
}
