using StoreBackend.Models;
using StoreBackend.Repositories;
using System;
using System.Linq;

namespace StoreBackend.Services
{
    public class InventoryService
    {
        private readonly IRepository<InventoryTransaction> _transactionRepository;
        private readonly IRepository<InventoryStock> _stockRepository;

        public InventoryService(IRepository<InventoryTransaction> transactionRepository, IRepository<InventoryStock> stockRepository)
        {
            _transactionRepository = transactionRepository;
            _stockRepository = stockRepository;
        }

        // Calculates and returns the current stock level for a given inventory item in a warehouse
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

        // Adds a new transaction and updates the inventory stock accordingly
        public void AddTransaction(InventoryTransaction transaction)
        {
            _transactionRepository.Insert(transaction);
            _transactionRepository.Save();
            UpdateInventoryStock(transaction);
        }

        // Updates an existing transaction and adjusts the inventory stock accordingly
        public void UpdateTransaction(InventoryTransaction transaction)
        {
            var existingTransaction = _transactionRepository.GetById(transaction.TransactionID);
            if (existingTransaction != null)
            {
                RevertInventoryStock(existingTransaction); // Revert the existing transaction's impact on stock
                _transactionRepository.Update(transaction);
                _transactionRepository.Save();
                UpdateInventoryStock(transaction); // Apply the updated transaction's impact on stock
            }
        }

        // Deletes a transaction and adjusts the inventory stock accordingly
        public void DeleteTransaction(int transactionId)
        {
            var transaction = _transactionRepository.GetById(transactionId);
            if (transaction != null)
            {
                RevertInventoryStock(transaction); // Revert the transaction's impact on stock
                _transactionRepository.Delete(transactionId);
                _transactionRepository.Save();
            }
        }

        // Updates the inventory stock based on the given transaction
        private void UpdateInventoryStock(InventoryTransaction transaction)
        {
            var stock = _stockRepository.GetAll()
                .FirstOrDefault(s => s.InventoryItemID == transaction.InventoryItemID && s.WarehouseID == transaction.WarehouseID);

            if (stock != null)
            {
                if (transaction.TransactionType == 1) // Purchase
                {
                    stock.Quantity += transaction.Qty;
                    decimal totalCost = stock.Quantity * stock.MovingAverageCost + transaction.Qty * transaction.Cost.Value;
                    stock.MovingAverageCost = totalCost / stock.Quantity;
                }
                else if (transaction.TransactionType == 2) // Sale
                {
                    stock.Quantity -= transaction.Qty;
                }

                _stockRepository.Update(stock);
                _stockRepository.Save();
            }
            else
            {
                if (transaction.TransactionType == 1) // Purchase
                {
                    stock = new InventoryStock
                    {
                        InventoryItemID = transaction.InventoryItemID,
                        WarehouseID = transaction.WarehouseID,
                        Quantity = transaction.Qty,
                        MovingAverageCost = transaction.Cost.Value
                    };

                    _stockRepository.Insert(stock);
                    _stockRepository.Save();
                }
            }
        }

        // Reverts the inventory stock based on the given transaction
        private void RevertInventoryStock(InventoryTransaction transaction)
        {
            var stock = _stockRepository.GetAll()
                .FirstOrDefault(s => s.InventoryItemID == transaction.InventoryItemID && s.WarehouseID == transaction.WarehouseID);

            if (stock != null)
            {
                if (transaction.TransactionType == 1) // Purchase
                {
                    stock.Quantity -= transaction.Qty;
                    if (stock.Quantity == 0)
                    {
                        stock.MovingAverageCost = 0;
                    }
                }
                else if (transaction.TransactionType == 2) // Sale
                {
                    stock.Quantity += transaction.Qty;
                }

                _stockRepository.Update(stock);
                _stockRepository.Save();
            }
        }

        // Calculates and returns the moving average cost for a given inventory item in a warehouse
        public decimal GetMovingAverageCost(int inventoryItemId, int warehouseId)
        {
            var stock = _stockRepository.GetAll()
                .FirstOrDefault(s => s.InventoryItemID == inventoryItemId && s.WarehouseID == warehouseId);

            if (stock != null)
            {
                return stock.MovingAverageCost;
            }

            return 0m;
        }
    }
}
