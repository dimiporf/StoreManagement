using StoreBackend.Models;
using StoreBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreBackend.Services
{
    public class InventoryService
    {
        private readonly IRepository<InventoryTransaction> _transactionRepository;

        public InventoryService(IRepository<InventoryTransaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public int GetStockLevel(int inventoryItemId, int warehouseId)
        {
            var purchases = _transactionRepository
                .GetAll()
                .Where(t => t.InventoryItemID == inventoryItemId && t.WarehouseID == warehouseId && t.TransactionType == 1)
                .Sum(t => t.Qty);

            var sales = _transactionRepository
                .GetAll()
                .Where(t => t.InventoryItemID == inventoryItemId && t.WarehouseID == warehouseId && t.TransactionType == 2)
                .Sum(t => t.Qty);

            return purchases - sales;
        }
    }
}
