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

        // Adds a new transaction and updates the inventory stock accordingly
        public void AddTransaction(InventoryTransaction transaction)
        {
            _transactionRepository.Insert(transaction); // Insert the new transaction into the repository
            _transactionRepository.Save(); // Save changes to the transaction repository
            UpdateInventoryStock(transaction); // Update the inventory stock based on the new transaction
        }

        // Updates an existing transaction and adjusts the inventory stock accordingly
        public void UpdateTransaction(InventoryTransaction transaction)
        {
            var existingTransaction = _transactionRepository.GetById(transaction.TransactionID);
            if (existingTransaction != null)
            {
                // Create a copy of the existing transaction to preserve the original values
                var oldTransaction = new InventoryTransaction
                {
                    TransactionID = existingTransaction.TransactionID,
                    InventoryItemID = existingTransaction.InventoryItemID,
                    WarehouseID = existingTransaction.WarehouseID,
                    TransactionType = existingTransaction.TransactionType,
                    Qty = existingTransaction.Qty,
                    Cost = existingTransaction.Cost,
                    SalePrice = existingTransaction.SalePrice,
                    TransactionDate = existingTransaction.TransactionDate
                };

                // Revert the original transaction's impact on stock
                RevertInventoryStock(oldTransaction);

                // Update the existing transaction with the new values
                existingTransaction.InventoryItemID = transaction.InventoryItemID;
                existingTransaction.WarehouseID = transaction.WarehouseID;
                existingTransaction.TransactionType = transaction.TransactionType;
                existingTransaction.Qty = transaction.Qty;
                existingTransaction.Cost = transaction.Cost;
                existingTransaction.SalePrice = transaction.SalePrice;
                existingTransaction.TransactionDate = transaction.TransactionDate;

                // Save the changes to the transaction repository
                _transactionRepository.Update(existingTransaction);
                _transactionRepository.Save();

                // Apply the updated transaction's impact on stock
                UpdateInventoryStock(existingTransaction);
            }
        }


        // Deletes a transaction and adjusts the inventory stock accordingly
        public void DeleteTransaction(int transactionId)
        {
            var transaction = _transactionRepository.GetById(transactionId);
            if (transaction != null)
            {
                RevertInventoryStock(transaction); // Revert the transaction's impact on stock
                _transactionRepository.Delete(transactionId); // Delete the transaction from the repository
                _transactionRepository.Save(); // Save changes to the transaction repository
            }
        }

        /* Updates the inventory stock based on the given transaction and recalculates the moving average cost.
         
        // Algorithm:
        // 1. Retrieve the current stock for the given inventory item and warehouse.
        // 2. Calculate the current total cost of the stock (quantity * moving average cost).
        // 3. Depending on the transaction type (purchase or sale):
        //    - Adjust the stock quantity.
        //    - Update the current total cost based on the transaction quantity and cost.
        // 4. Recalculate the moving average cost:
        //    - If stock quantity is greater than 0, update moving average cost as (current total cost / quantity).
        //    - If stock quantity drops to 0 or below, reset moving average cost to 0.
        // 5. Update the stock entity in the repository with the new quantity and moving average cost.
        // 6. Save changes to the repository. */
        private void UpdateInventoryStock(InventoryTransaction transaction)
        {
            var stock = _stockRepository.GetAll()
                .FirstOrDefault(s => s.InventoryItemID == transaction.InventoryItemID && s.WarehouseID == transaction.WarehouseID);

            if (stock != null)
            {
                // Calculate current total cost of stock
                decimal currentTotalCost = stock.Quantity * stock.MovingAverageCost;

                // Update stock quantity based on transaction type
                if (transaction.TransactionType == 1) // Purchase
                {
                    stock.Quantity += transaction.Qty;
                    currentTotalCost += transaction.Qty * transaction.Cost.Value;
                }
                else if (transaction.TransactionType == 2) // Sale
                {
                    stock.Quantity -= transaction.Qty;
                    // Ensure we don't go negative on stock
                    if (stock.Quantity < 0)
                    {
                        throw new InvalidOperationException("Insufficient stock for sale.");
                    }
                    currentTotalCost -= transaction.Qty * stock.MovingAverageCost;
                }

                // Update moving average cost
                if (stock.Quantity > 0)
                {
                    stock.MovingAverageCost = currentTotalCost / stock.Quantity;
                }
                else
                {
                    stock.MovingAverageCost = 0; // If no stock, reset cost to 0
                }

                // Update stock entity in repository
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
                else
                {
                    // Handle case where stock does not exist and transaction is a sale
                    throw new InvalidOperationException("Stock not found for sale transaction.");
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
                // Calculate current total cost of stock
                decimal currentTotalCost = stock.Quantity * stock.MovingAverageCost;

                // Revert stock quantity based on transaction type
                if (transaction.TransactionType == 1) // Purchase
                {
                    stock.Quantity -= transaction.Qty;
                    // Ensure we don't go negative on stock
                    if (stock.Quantity < 0)
                    {
                        throw new InvalidOperationException("Insufficient stock to revert purchase.");
                    }
                    currentTotalCost -= transaction.Qty * transaction.Cost.Value;
                }
                else if (transaction.TransactionType == 2) // Sale
                {
                    stock.Quantity += transaction.Qty;
                    currentTotalCost += transaction.Qty * stock.MovingAverageCost;
                }

                // Update moving average cost
                if (stock.Quantity > 0)
                {
                    stock.MovingAverageCost = currentTotalCost / stock.Quantity;
                }
                else
                {
                    stock.MovingAverageCost = 0; // If no stock, reset cost to 0
                }

                // Update stock entity in repository
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

            return 0m; // Return 0 if no stock entry found
        }
    }
}
